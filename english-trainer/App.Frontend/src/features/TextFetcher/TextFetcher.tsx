import React, { useState, useEffect } from 'react';

interface TextExample {
  id: number;
  title: string;
}

export const TextFetcher = () => {
  const [texts, setTexts] = useState<TextExample[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchTexts = async () => {
      try {
        // Fetch from your backend's test API
        const response = await fetch('/api/Test/texts');
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data: TextExample[] = await response.json();
        setTexts(data);
      } catch (err) {
        setError((err as Error).message);
      } finally {
        setLoading(false);
      }
    };

    fetchTexts();
  }, []);

  if (loading) {
    return <div>Loading text examples...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

  return (
    <div>
      <h2>Fetched Text Examples (from Your Backend)</h2>
      <ul>
        {texts.map((text) => (
          <li key={text.id}><strong>{text.title}</strong></li>
        ))}
      </ul>
    </div>
  );
};