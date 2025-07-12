import React, { useState } from 'react';
import { AudioRecorder } from '@/features/PronunciationAssessment/components/AudioRecorder';
import { TextPassageEditor } from '@/entities/TextPassage/ui/TextPassageEditor';
import { AssessmentResultDisplay } from '@/features/AssessmentResultDisplay/AssessmentResultDisplay';
import { useTextPassageStore } from '@/entities/TextPassage/model/textPassageStore';

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