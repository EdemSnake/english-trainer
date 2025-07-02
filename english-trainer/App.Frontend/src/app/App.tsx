
import { RouterProvider } from 'react-router-dom';
import { appRouter } from './providers/with-router';

const App = () => {
  return <RouterProvider router={appRouter()} />;
};

export default App;
