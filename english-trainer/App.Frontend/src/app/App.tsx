import { RouterProvider } from 'react-router-dom';
import { withProviders } from './providers';

const { router } = withProviders();

const App = () => {
  return <RouterProvider router={router} />;
};

export default App;