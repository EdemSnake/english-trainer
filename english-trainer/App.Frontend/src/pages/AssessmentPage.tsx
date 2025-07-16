import React, { useState } from 'react';
import { AudioRecorder } from '@/features/PronunciationAssessment/components/AudioRecorder';
import { TextPassageEditor } from '@/entities/TextPassage/ui/TextPassageEditor';
import { AssessmentResultDisplay } from '@/features/AssessmentResultDisplay/AssessmentResultDisplay';
import { useTextPassageStore } from '@/entities/TextPassage/model/textPassageStore';
import { TextToSpeech } from '@/features/PronunciationAssessment/components/TextToSpeech';

 const AssessmentPage = () => {
  const { textToRead, textPassageId } = useTextPassageStore();
  const [assessmentResult, setAssessmentResult] = useState<any>(null);

  const handleAssessmentResult = (result: any) => {
    setAssessmentResult(result);
  };

  return (
    <div>
      <h1>Pronunciation Assessment</h1>
      <TextPassageEditor />
      <TextToSpeech text={textToRead} />
      <AudioRecorder
        referenceText={textToRead}
        textPassageId={textPassageId}
        onAssessmentResult={handleAssessmentResult}
      />
      <AssessmentResultDisplay result={assessmentResult} />
    </div>
  );
};

export default AssessmentPage;