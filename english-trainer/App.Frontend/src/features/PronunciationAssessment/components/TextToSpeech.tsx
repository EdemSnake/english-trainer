// features/PronunciationAssessment/components/TextToSpeech.tsx
import React, { useState, useEffect } from 'react';
import * as signalR from '@microsoft/signalr';
import { SIGNALR_HUB_URL, TTS_SPEAK_API_URL, AUDIO_FILES_EXTERNAL_BASE_URL } from '../../../config';

interface TextToSpeechProps {
    text: string;
}

export const TextToSpeech: React.FC<TextToSpeechProps> = ({ text }) => {
    const [audioUrl, setAudioUrl] = useState<string | null>(null);
    //const [connection, setConnection] = useState<signalR.HubConnection | null>(null);

    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl(SIGNALR_HUB_URL) // Use imported variable
            .withAutomaticReconnect()
            .build();
        connection.start()
            .then(() => console.log('SignalR Connected.'))
            .catch((err: any) => console.error('SignalR Connection Error: ', err));

        connection.on("ReceiveAudioUrl", (url: string) => {
            setAudioUrl(url);
        });

        return () => {
            connection.off("ReceiveAudioUrl", (url: string) => {
                setAudioUrl(url);
            });
            connection.stop();
        }
    }, []);


    const handleSpeak = async () => {
        try {
            await fetch(TTS_SPEAK_API_URL, { // Use imported variable
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
                <audio controls src={`${AUDIO_FILES_EXTERNAL_BASE_URL}${audioUrl}`} autoPlay>
                    Your browser does not support the audio element.
                </audio>
            )}
        </div>
    );
};