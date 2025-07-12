import { createBrowserRouter } from 'react-router-dom';
import { HomePage, AssessmentPage } from '../../pages';
import { MainLayout } from '../layouts/MainLayout';

export const appRouter = () => createBrowserRouter([
  {
    element: <MainLayout />,
    children: [
      {
        path: '/',
        element: <HomePage />,
      },
      {
        path: '/assessment',
        element: <AssessmentPage />,
      },
    ],
  },
]);
