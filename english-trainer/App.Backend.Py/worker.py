# worker.py

import pika
import json
from app.tts_engine import text_to_speech

def main():
    """
    Connects to RabbitMQ and processes TTS requests.
    """
    connection = pika.BlockingConnection(pika.ConnectionParameters('localhost'))
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
            
            audio_url = text_to_speech(text, voice)
            
            # Create the result with PascalCase to match the .NET consumer
            result = {
                'AudioUrl': audio_url
            }
            
            # Get the correlation_id from the incoming message
            correlation_id = properties.correlation_id

            # Publish the result to the tts_results queue
            channel.basic_publish(
                exchange='',
                routing_key='tts_results',
                properties=pika.BasicProperties(
                    correlation_id=correlation_id
                ),
                body=json.dumps(result)
            )
            if properties.reply_to:
                channel.basic_publish(
                    exchange='',
                    routing_key=properties.reply_to,
                    properties=pika.BasicProperties(
                        correlation_id=correlation_id,
                        content_type='application/json'
                    ),
                    body=json.dumps(result).encode()
                )
            else:
                # fallback для логирования или fire-and-forget
                print(" [!] reply_to отсутствует, сообщение не отправлено обратно")



            
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