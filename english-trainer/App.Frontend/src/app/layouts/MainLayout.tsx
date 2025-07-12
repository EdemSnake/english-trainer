import { Outlet } from 'react-router-dom';
import { Header } from '@/widgets/Header';

export const MainLayout = () => {
  return (
    <div>
      <Header />
      <main>
        <Outlet />
      </main>
    </div>
  );
};
