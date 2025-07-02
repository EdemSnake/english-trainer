import React, { useState } from 'react';
import { AudioRecorder } from '@/features/PronunciationAssessment/components/AudioRecorder';
import styles from './AssessmentPage.module.scss';
import { v4 as uuidv4 } from 'uuid';

export const AssessmentPage = () => {
  const [textToRead, setTextToRead] = useState('London is the capital of Great Britain.');
  const [textPassageId, setTextPassageId] = useState(uuidv4());
  const [isTextSaved, setIsTextSaved] = useState(false);
  const [assessmentResult, setAssessmentResult] = useState<any>(null); // State to store assessment result

  const handleSaveText = async () => {
    // In a real application, you would send this to your backend's TextPassages API
    // The backend would then return the actual TextPassageId (new or existing)
    console.log('Attempting to save text:', { id: textPassageId, text: textToRead });

    try {
      // Simulate API call
      const response = await fetch('/api/TextPassages', {
        method: isTextSaved ? 'PUT' : 'POST', // Use PUT if already saved, POST if new
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          id: textPassageId,
          text: textToRead,
        }),
      });

      if (response.ok) {
        // Simulate backend response for now
        const savedData = { id: textPassageId, text: textToRead }; // Backend would return this
        console.log('Text saved successfully!', savedData);
        
        // Update textPassageId if it was a new save and backend returned a new ID
        // For simulation, we assume the backend confirms the ID or provides a new one
        if (!isTextSaved) {
          setTextPassageId(savedData.id);
        }
        setIsTextSaved(true);
      } else {
        console.error('Failed to save text:', response.statusText);
      }
    } catch (error) {
      console.error('Error saving text:', error);
    }
  };

  const handleAssessmentResult = (result: any) => {
    setAssessmentResult(result);
  };

  return (
    <div>
      <h1>Pronunciation Assessment</h1>
      <p>Read the following text aloud:</p>
      <textarea
        className={styles.textToReadArea}
        rows={5}
        cols={50}
        value={textToRead}
        onChange={(e) => {
          setTextToRead(e.target.value);
          setIsTextSaved(false); // Mark as unsaved if text is modified
        }}
      />
      <button onClick={handleSaveText} className={styles.saveButton}>
        {isTextSaved ? 'Update Text' : 'Save Text for History'}
      </button>
      <AudioRecorder
        referenceText={textToRead}
        textPassageId={textPassageId}
        onAssessmentResult={handleAssessmentResult}
      />

      {assessmentResult && (
        <div className={styles.assessmentResult}>
          <h2>Assessment Result:</h2>
          <pre>{JSON.stringify(assessmentResult, null, 2)}</pre>
        </div>
      )}
    </div>
  );
};