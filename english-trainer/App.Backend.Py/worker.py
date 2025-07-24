import os
import pika
import json
import time
import requests
from app.tts_engine import text_to_speech

print("Worker script started")

def main():
    """
    Connects to RabbitMQ and processes TTS requests.
    """
    rabbitmq_host = os.environ.get('RABBITMQ_HOST', 'localhost')
    connection = None
    retries = 5
    while retries > 0:
        try:
            print(f"Attempting to connect to RabbitMQ at {rabbitmq_host}...")
            connection = pika.BlockingConnection(pika.ConnectionParameters(rabbitmq_host))
            print("Successfully connected to RabbitMQ.")
            break
        except pika.exceptions.AMQPConnectionError as e:
            print(f"Failed to connect to RabbitMQ: {e}. Retrying in 5 seconds...")
            time.sleep(5)
            retries -= 1
    
    if not connection:
        print("Could not connect to RabbitMQ after multiple retries. Exiting.")
        return

    channel = connection.channel()

    # Declare the exchange that MassTransit uses for the SpeakTextCommand
    # The exchange name is derived from the message type.
    exchange_name = 'App.Application.Features.TextToSpeech:SpeakTextCommand'
    channel.exchange_declare(exchange=exchange_name, exchange_type='fanout', durable=True)

    # Declare the queue and bind it to the exchange
    channel.queue_declare(queue='tts_requests', durable=True)
    channel.queue_bind(exchange=exchange_name, queue='tts_requests')
    
    channel.queue_declare(queue='tts_results', durable=True)

    def callback(ch, method, properties, body):
        """
        Callback function to process a message from the tts_requests queue.
        """
        try:
            data = json.loads(body)
            message = data['message']
            
            # Handle potential casing differences
            text = message.get('text') or message.get('Text')
            voice = message.get('voice') or message.get('Voice')

            print(f" [x] Received request for voice '{voice}': '{text}'")
            
            # Generate audio file and get the file path
            audio_file_path = text_to_speech(text, voice)
            
            # Extract just the filename from the full path
            filename = os.path.basename(audio_file_path)
            
            # Create URL that points to .NET backend serving the audio
            audio_url = f"http://localhost:8081/audio/{filename}"
            
            # Create the result with PascalCase to match the .NET consumer
            result = {
                'AudioUrl': audio_url
            }
            
            try:
                print(f": AudioUrl {result}")
                requests.post("http://backend:8080/api/tts/send", json=result)
            except Exception as e:
                print(f"[!] Failed to POST to backend SignalR hub: {e}")
            
            print(f" [x] Sent result: {audio_url}")

        except Exception as e:
            print(f" [!] Error processing message: {e}")

        finally:
            ch.basic_ack(delivery_tag=method.delivery_tag)

    channel.basic_consume(queue='tts_requests', on_message_callback=callback)

    print(' [*] Waiting for messages. To exit press CTRL+C')
    channel.start_consuming()

if __name__ == '__main__':
    main()