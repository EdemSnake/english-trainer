import React from 'react';
import { useTextPassageStore } from '../model/textPassageStore';
import { saveTextPassage } from '../api/textPassageApi';
import styles from './TextPassageEditor.module.scss';

export const TextPassageEditor = () => {
  const { textToRead, textPassageId, isTextSaved, setTextToRead } = useTextPassageStore();

  const handleSaveText = async () => {
    console.log('Attempting to save text:', { id: textPassageId, text: textToRead });
    try {
      await saveTextPassage(textPassageId, textToRead, isTextSaved);
    } catch (error) {
      // Error handling is done in the API function, but you can add more here if needed
    }
  };

  return (
    <>
      <p>Read the following text aloud:</p>
      <textarea
        className={styles.textToReadArea}
        rows={5}
        cols={50}
        value={textToRead}
        onChange={(e) => setTextToRead(e.target.value)}
      />
      <button onClick={handleSaveText} className={styles.saveButton}>
        {isTextSaved ? 'Update Text' : 'Save Text for History'}
      </button>
    </>
  );
};
