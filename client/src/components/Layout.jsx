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
      <Link to="/" className="brand-logo" aria-label="WPFWDesigns, go to the home page">
        {/* Logo mark: a terminal console, black with a white border and a ">_" prompt. */}
        <svg className="brand-logo__mark" viewBox="0 0 40 40" aria-hidden="true" focusable="false">
          <rect x="1.25" y="1.25" width="37.5" height="37.5" rx="9" fill="#0b0f14" stroke="#ffffff" strokeWidth="2.5" />
          <path
            d="M11 13 L18 20 L11 27"
            fill="none"
            stroke="#ffffff"
            strokeWidth="3.2"
            strokeLinecap="round"
            strokeLinejoin="round"
          />
          <rect className="brand-logo__cursor" x="21" y="25" width="9" height="3.2" rx="1.6" fill="#ffffff" />
        </svg>
        <span className="brand-logo__text" aria-hidden="true">
          <span className="brand-logo__name">WPFW</span>
          <span className="brand-logo__sub">designs</span>
        </span>
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
