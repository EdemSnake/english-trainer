import { useTextPassageStore } from '../model/textPassageStore';

const API_URL = '/api/TextPassages'; // Your API endpoint for text passages

export const saveTextPassage = async (textPassageId: string, textToRead: string, isTextSaved: boolean) => {
  const method = isTextSaved ? 'PUT' : 'POST';

  try {
    const response = await fetch(API_URL, {
      method: method,
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        id: textPassageId,
        text: textToRead,
      }),
    });

    if (response.ok) {
      const savedData = { id: textPassageId, text: textToRead };
      console.log('Text saved successfully!', savedData);
      useTextPassageStore.getState().setIsTextSaved(true);
      useTextPassageStore.getState().setTextPassageId(savedData.id);
      return savedData;
    } else {
      console.error('Failed to save text:', response.statusText);
      throw new Error('Failed to save text');
    }
  } catch (error) {
    console.error('Error saving text:', error);
    throw error;
  }
};
