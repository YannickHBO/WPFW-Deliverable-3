import { useEffect } from 'react';
import { Link, Outlet, useLocation } from 'react-router-dom';
import NavMenu from './NavMenu.jsx';

// Page order for the scroll cue: each page links to the next one, looping back to home.
const pages = [
  { path: '/', bodyClass: 'page-landing', label: 'Home' },
  { path: '/whoami', bodyClass: 'page-whoami', label: 'Who Am I' },
  { path: '/blog', bodyClass: 'page-blog', label: 'Blog' },
  { path: '/projects', bodyClass: 'page-projects', label: 'Projects' },
  { path: '/contact', bodyClass: 'page-contact', label: 'Contact' }
];

export default function Layout() {
  const { pathname } = useLocation();
  const index = pages.findIndex((page) => page.path === pathname);
  const current = pages[index] ?? pages[0];
  const next = pages[(Math.max(index, 0) + 1) % pages.length];

  // The page gradients in styles.css are keyed on a class on <body>.
  useEffect(() => {
    pages.forEach((page) => document.body.classList.remove(page.bodyClass));
    document.body.classList.add(current.bodyClass);
    window.scrollTo(0, 0);
  }, [current.bodyClass]);

  return (
    <>
      <Link to="/" className="brand-tag" aria-label="Go to the home page">
        <i>WPFWDesigns</i>
      </Link>

      <NavMenu />

      <div className="route-fade" key={pathname}>
        <Outlet />
      </div>

      <footer className="site-footer">
        <p>© 2026 WPFWDesigns. All rights reserved.</p>
      </footer>

      <Link className="scroll-cue" to={next.path} aria-label={`Continue to ${next.label}`} />
    </>
  );
}
