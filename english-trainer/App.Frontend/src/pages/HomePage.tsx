import { TextFetcher } from '@/features/TextFetcher';

export const HomePage = () => {
  return (
    <div>
      <h1>Welcome to English Trainer!</h1>
      <p>This is the home page.</p>
      <TextFetcher />
    </div>
  );
};
