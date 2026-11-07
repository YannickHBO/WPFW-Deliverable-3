import { Link } from 'react-router-dom';
import useDocumentTitle from '../hooks/useDocumentTitle.js';

export default function NotFoundPage() {
  useDocumentTitle('Page not found');

  return (
    <main className="page-content">
      <h1>Page not found</h1>
      <p>
        This page does not exist. <Link to="/" style={{ textDecoration: 'underline' }}>Go back home</Link>.
      </p>
    </main>
  );
}
