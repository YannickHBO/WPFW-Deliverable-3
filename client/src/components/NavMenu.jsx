import { useEffect, useRef, useState } from 'react';
import { NavLink, useLocation } from 'react-router-dom';
import WeatherTheme from './WeatherTheme.jsx';

const links = [
  { to: '/whoami', label: 'Whoami' },
  { to: '/blog', label: 'Blog' },
  { to: '/projects', label: 'Projects' },
  { to: '/contact', label: 'Contact' }
];

export default function NavMenu() {
  const [isOpen, setIsOpen] = useState(false);
  const navRef = useRef(null);
  const toggleRef = useRef(null);
  const { pathname } = useLocation();

  // Close the menu after navigating.
  useEffect(() => setIsOpen(false), [pathname]);

  useEffect(() => {
    if (!isOpen) return undefined;

    const onClick = (event) => {
      if (!navRef.current.contains(event.target)) setIsOpen(false);
    };
    const onKeyDown = (event) => {
      if (event.key === 'Escape') {
        setIsOpen(false);
        toggleRef.current.focus();
      }
    };

    document.addEventListener('click', onClick);
    document.addEventListener('keydown', onKeyDown);
    return () => {
      document.removeEventListener('click', onClick);
      document.removeEventListener('keydown', onKeyDown);
    };
  }, [isOpen]);

  return (
    <nav className="top-nav" aria-label="Main navigation" ref={navRef}>
      <button
        ref={toggleRef}
        className="menu-toggle"
        type="button"
        aria-label="Toggle navigation"
        aria-expanded={isOpen}
        aria-controls="site-nav-menu"
        onClick={() => setIsOpen((open) => !open)}
      >
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
      </button>

      {/* The list stays mounted while closed (hidden via CSS), so the weather theme keeps its state. */}
      <ul className={`nav-menu${isOpen ? ' open' : ''}`} id="site-nav-menu">
        {links.map((link) => (
          <li key={link.to}>
            <NavLink to={link.to}>{link.label}</NavLink>
          </li>
        ))}
        <li className="nav-options">
          <span className="nav-options__label">Options</span>
          <WeatherTheme />
        </li>
      </ul>
    </nav>
  );
}
