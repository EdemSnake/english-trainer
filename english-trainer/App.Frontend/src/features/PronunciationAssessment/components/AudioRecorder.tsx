import React, { useEffect } from 'react';
import { useAudioRecorder } from '../model/useAudioRecorder';
import styles from './AudioRecorder.module.scss';

interface AudioRecorderProps {
  referenceText: string;
  textPassageId: string;
  onAssessmentResult: (result: any) => void;
}

export const AudioRecorder: React.FC<AudioRecorderProps> = ({ referenceText, textPassageId, onAssessmentResult }) => {
  const {
    recording,
    audioBlob,
    assessmentResult,
    startRecording,
    stopRecording,
    clearRecording,
    sendAudio,
  } = useAudioRecorder({ referenceText, textPassageId });

  useEffect(() => {
    if (assessmentResult) {
      onAssessmentResult(assessmentResult);
    }
  }, [assessmentResult, onAssessmentResult]);

  return (
    <div className={styles.audioRecorderContainer}>
      <div className={styles.controls}>
        <button
          onClick={recording ? stopRecording : startRecording}
          className={`${styles.button} ${recording ? styles.recordingButton : ''}`}
        >
          {recording ? 'Stop Recording' : 'Start Recording'}
        </button>

        {recording && <div className={styles.recordingIndicator}></div>}

        {audioBlob && (
          <>
            <button onClick={sendAudio} className={styles.button}>Send Audio</button>
            <button onClick={clearRecording} className={styles.button}>Clear Recording</button>
          </>
        )}
      </div>

      {audioBlob && (
        <div className={styles.audioPlayer}>
          <audio src={URL.createObjectURL(audioBlob)} controls onLoadedMetadata={(e) => URL.revokeObjectURL((e.target as HTMLAudioElement).src)} />
        </div>
      )}
    </div>
  );
};