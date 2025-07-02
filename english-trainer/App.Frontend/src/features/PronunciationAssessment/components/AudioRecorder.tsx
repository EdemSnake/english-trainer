import React, { useState, useRef } from 'react';
import styles from './AudioRecorder.module.scss';

interface AudioRecorderProps {
  referenceText: string;
  textPassageId: string;
}

export const AudioRecorder: React.FC<AudioRecorderProps> = ({ referenceText, textPassageId }) => {
  const [recording, setRecording] = useState(false);
  const [audioBlob, setAudioBlob] = useState<Blob | null>(null);
  const [assessmentResult, setAssessmentResult] = useState<any>(null);
  // These are hardcoded for now, as they should not be user-facing
  const [userId] = useState('00000000-0000-0000-0000-000000000001'); 

  const mediaRecorderRef = useRef<MediaRecorder | null>(null);
  const audioChunksRef = useRef<Blob[]>([]);

  const writeString = (view: DataView, offset: number, string: string) => {
    for (let i = 0; i < string.length; i++) {
      view.setUint8(offset + i, string.charCodeAt(i));
    }
  };

  const encodeWAV = (samples: Float32Array, sampleRate: number): Blob => {
    const buffer = new ArrayBuffer(44 + samples.length * 2);
    const view = new DataView(buffer);

    writeString(view, 0, 'RIFF');
    view.setUint32(4, 36 + samples.length * 2, true);
    writeString(view, 8, 'WAVE');
    writeString(view, 12, 'fmt ');
    view.setUint32(16, 16, true);
    view.setUint16(20, 1, true);
    view.setUint16(22, 1, true);
    view.setUint32(24, sampleRate, true);
    view.setUint32(28, sampleRate * 2, true);
    view.setUint16(32, 2, true);
    view.setUint16(34, 16, true);
    writeString(view, 36, 'data');
    view.setUint32(40, samples.length * 2, true);

    let offset = 44;
    for (let i = 0; i < samples.length; i++, offset += 2) {
      const s = Math.max(-1, Math.min(1, samples[i]));
      view.setInt16(offset, s < 0 ? s * 0x8000 : s * 0x7FFF, true);
    }

    return new Blob([view], { type: 'audio/wav' });
  };

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
        const wavBlob = encodeWAV(audioBuffer.getChannelData(0), audioBuffer.sampleRate);
        setAudioBlob(wavBlob);
        stream.getTracks().forEach(track => track.stop());
      };

      mediaRecorderRef.current.start();
      setRecording(true);
      setAudioBlob(null); // Clear previous recording when starting a new one
      setAssessmentResult(null); // Clear previous result when starting new recording
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
    setAssessmentResult(null); // Clear result when clearing recording
  };

  const sendAudio = async () => {
    if (audioBlob) {
      const formData = new FormData();
      formData.append('UserId', userId);
      formData.append('TextPassageId', textPassageId);
      formData.append('ReferenceText', referenceText);
      formData.append('AudioFile', audioBlob, 'recording.wav');

      try {
        const response = await fetch('/api/PronunciationAssessment', {
          method: 'POST',
          body: formData,
        });

        if (response.ok) {
          const resultText = await response.text();
          const result = JSON.parse(resultText); // Parse the text response as JSON
          console.log('Audio sent successfully!', result);
          setAssessmentResult(result);
          // Don't clear the recording so the user can see the result
        } else {
          console.error('Failed to send audio:', response.statusText);
          setAssessmentResult({ error: response.statusText });
        }
      } catch (error) {
        console.error('Error sending audio:', error);
        setAssessmentResult({ error: (error as Error).message });
      }
    }
  };

  return (
    <div className={styles.audioRecorderContainer}>
      <div className={styles.controls}>
        <button
          onClick={recording ? stopRecording : startRecording}
          className={`${styles.button} ${recording ? styles.recordingButton : ''}`}
          disabled={recording && audioBlob !== null}
        >
          {recording ? 'Stop Recording' : 'Start Recording'}
        </button>

        {recording && <div className={styles.recordingIndicator}></div>}

        {audioBlob && (
          <button onClick={sendAudio} className={styles.button}>
            Send Audio
          </button>
        )}

        {audioBlob && (
          <button onClick={clearRecording} className={styles.button}>
            Clear Recording
          </button>
        )}
      </div>

      {audioBlob && (
        <div className={styles.audioPlayer}>
          <audio src={URL.createObjectURL(audioBlob)} controls />
        </div>
      )}
      {assessmentResult && (
        <div className={styles.resultContainer}>
          <h3>Assessment Result</h3>
          <pre>{JSON.stringify(assessmentResult, null, 2)}</pre>
        </div>
      )}
    </div>
  );
};
