// features/PronunciationAssessment/components/TextToSpeech.tsx
import React, { useState, useEffect } from 'react';
import * as signalR from '@microsoft/signalr';

interface TextToSpeechProps {
    text: string;
}

export const TextToSpeech: React.FC<TextToSpeechProps> = ({ text }) => {
    const [audioUrl, setAudioUrl] = useState<string | null>(null);
    const [connection, setConnection] = useState<signalR.HubConnection | null>(null);

    useEffect(() => {
        const newConnection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5007/ttsHub") // Adjust the URL to your backend
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }, []);

    useEffect(() => {
        if (connection) {
            connection.start()
                .then(() => console.log('SignalR Connected.'))
                .catch((err: any) => console.error('SignalR Connection Error: ', err));

            connection.on("ReceiveAudioUrl", (url: string) => {
                setAudioUrl(url);
            });
        }

        return () => {
            connection?.stop();
        };
    }, [connection]);

    const handleSpeak = async () => {
        try {
            await fetch('http://localhost:5007/api/tts/speak', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ text, voice: 'af_heart' }), // You can make the voice selectable
            });
        } catch (error) {
            console.error('Error sending text to speak:', error);
        }
    };

    return (
        <div>
            <button onClick={handleSpeak} disabled={!text}>
                Speak my text
            </button>
            {audioUrl && (
                <audio controls src={`http://localhost:5007${audioUrl}`} autoPlay>
                    Your browser does not support the audio element.
                </audio>
            )}
        </div>
    );
};