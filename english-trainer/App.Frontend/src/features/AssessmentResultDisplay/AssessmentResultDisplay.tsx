import React from 'react';
import styles from './AssessmentResultDisplay.module.scss';

interface AssessmentResultDisplayProps {
  result: any;
}

export const AssessmentResultDisplay: React.FC<AssessmentResultDisplayProps> = ({ result }) => {
  if (!result) {
    return null;
  }

  return (
    <div className={styles.assessmentResult}>
      <h2>Assessment Result:</h2>
      <pre>{JSON.stringify(result, null, 2)}</pre>
    </div>
  );
};
