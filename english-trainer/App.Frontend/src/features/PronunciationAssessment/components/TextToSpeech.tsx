// features/PronunciationAssessment/components/TextToSpeech.tsx
import React, { useState, useEffect, useRef } from 'react';
import * as signalR from '@microsoft/signalr';
import {
    SIGNALR_HUB_URL,
    TTS_SPEAK_API_URL,
    AUDIO_FILES_EXTERNAL_BASE_URL,
} from '../../../config';

interface TextToSpeechProps {
    text: string;
}

export const TextToSpeech: React.FC<TextToSpeechProps> = ({ text }) => {
    const [audioUrl, setAudioUrl] = useState<string | null>(null);
    const [isAudioEmpty, setIsAudioEmpty] = useState(false);
    const audioRef = useRef<HTMLAudioElement>(null);

    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl(SIGNALR_HUB_URL)
            .withAutomaticReconnect()
            .build();

        connection
            .start()
            .then(() => console.log('SignalR Connected.'))
            .catch((err: any) =>
                console.error('SignalR Connection Error: ', err)
            );

        connection.on('ReceiveAudioUrl', (url: string) => {
            console.log('📥 Received audio URL from backend:', url);
            setAudioUrl(url);
            setIsAudioEmpty(false); // сбрасываем при новом аудио
        });

        connection.onclose((error) => {
            console.error('SignalR connection closed:', error);
        });

        connection.onreconnecting((error) => {
            console.log('SignalR reconnecting:', error);
        });

        connection.onreconnected((connectionId) => {
            console.log('SignalR reconnected:', connectionId);
        });

        return () => {
            connection.off('ReceiveAudioUrl');
            connection.stop();
        };
    }, []);

    useEffect(() => {
        const audio = audioRef.current;
        if (audio && audioUrl) {
            console.log('Audio element:', audio);
            console.log('Audio URL:', audioUrl);
            const handleMetadata = () => {
                console.log(`🎧 Audio duration: ${audio.duration} sec`);
                if (audio.duration === 0) {
                    console.warn('⚠️ Аудиофайл пустой!');
                    setIsAudioEmpty(true);
                }
            };

            const handleError = (e: Event) => {
                console.error('❌ Audio loading error:', e);
                setIsAudioEmpty(true);
            };

            const handleCanPlay = () => {
                console.log('✅ Audio can play');
                setIsAudioEmpty(false);
            };

            audio.addEventListener('loadedmetadata', handleMetadata);
            audio.addEventListener('error', handleError);
            audio.addEventListener('canplay', handleCanPlay);

            return () => {
                audio.removeEventListener('loadedmetadata', handleMetadata);
                audio.removeEventListener('error', handleError);
                audio.removeEventListener('canplay', handleCanPlay);
            };
        }
    }, [audioUrl]);

    const handleSpeak = async () => {
        try {
            await fetch(TTS_SPEAK_API_URL, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    text,
                    voice: 'af_heart', // при необходимости сделать динамическим
                }),
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
                <>
                    <audio ref={audioRef} controls src={audioUrl} autoPlay>
                        Your browser does not support the audio element.
                    </audio>
                    {isAudioEmpty && (
                        <div style={{ color: 'red', marginTop: '8px' }}>
                            ⚠️ Аудиофайл пустой или повреждён
                        </div>
                    )}
                </>
            )}
        </div>
    );
};
