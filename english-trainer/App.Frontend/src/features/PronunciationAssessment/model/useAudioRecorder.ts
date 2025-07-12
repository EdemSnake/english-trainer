import { useState, useRef, useEffect } from 'react';
import { encodeWAVWithResample } from './wavEncoder';


interface UseAudioRecorderProps {
    referenceText: string;
    textPassageId: string;
}

export const useAudioRecorder = ({ referenceText, textPassageId }: UseAudioRecorderProps) => {
    const [recording, setRecording] = useState(false);
    const [audioBlob, setAudioBlob] = useState<Blob | null>(null);
    const [assessmentResult, setAssessmentResult] = useState<Record<string, unknown> | null>(null);

    const mediaRecorderRef = useRef<MediaRecorder | null>(null);
    const audioChunksRef = useRef<Blob[]>([]);

    const startRecording = async () => {
        try {
            const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
            mediaRecorderRef.current = new MediaRecorder(stream);
            audioChunksRef.current = [];

            mediaRecorderRef.current.ondataavailable = (event) => {
                audioChunksRef.current.push(event.data);
            };

            mediaRecorderRef.current.onstop = async () => {
                const audioContext = new AudioContext();
                const blob = new Blob(audioChunksRef.current, { type: mediaRecorderRef.current?.mimeType });
                const arrayBuffer = await blob.arrayBuffer();
                const audioBuffer = await audioContext.decodeAudioData(arrayBuffer);
                const wavBlob = await encodeWAVWithResample(audioBuffer);
                setAudioBlob(wavBlob);
                await audioContext.close();
                stream.getTracks().forEach(track => track.stop());
                mediaRecorderRef.current = null;
            };

            mediaRecorderRef.current.start();
            setRecording(true);
            setAudioBlob(null);
            setAssessmentResult(null);
        } catch (error) {
            console.error('Error accessing microphone:', error);
        }
    };

    const stopRecording = () => {
        if (mediaRecorderRef.current && recording) {
            mediaRecorderRef.current.stop();
            setRecording(false);
        }
    };

    const clearRecording = () => {
        setAudioBlob(null);
        setRecording(false);
        audioChunksRef.current = [];
        setAssessmentResult(null);
    };

    const sendAudio = async () => {
        if (!audioBlob) return;

        const formData = new FormData();
        formData.append('UserId', '00000000-0000-0000-0000-000000000001');
        formData.append('TextPassageId', textPassageId);
        formData.append('ReferenceText', referenceText);
        formData.append('AudioFile', audioBlob, 'recording.wav');

        try {
            const response = await fetch('/api/PronunciationAssessment', {
                method: 'POST',
                body: formData,
            });

            if (response.ok) {
                const result = await response.json();
                setAssessmentResult(result);
            } else {
                setAssessmentResult({ error: response.statusText });
            }
        } catch (error) {
            setAssessmentResult({ error: (error as Error).message });
        }
    };

    useEffect(() => {
        return () => {
            if (mediaRecorderRef.current) {
                mediaRecorderRef.current.stream.getTracks().forEach(track => track.stop());
                mediaRecorderRef.current = null;
            }
        };
    }, []);

    return {
        recording,
        audioBlob,
        assessmentResult,
        startRecording,
        stopRecording,
        clearRecording,
        sendAudio,
    };


}