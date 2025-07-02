import { Link } from 'react-router-dom';
import styles from './Header.module.scss';

export const Header = () => {
  return (
    <header className={styles.header}>
      <div className={styles.container}>
        <nav className={styles.header__nav}>
          <Link to="/">Home</Link>
          <Link to="/assessment">Assessment</Link>
        </nav>
      </div>
    </header>
  );
};