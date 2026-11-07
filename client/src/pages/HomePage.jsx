import { Link } from 'react-router-dom';
import useDocumentTitle from '../hooks/useDocumentTitle.js';

export default function HomePage() {
  useDocumentTitle('Home');

  return (
    <main className="page-content">
      <h1>Welcome to WPFWDesigns</h1>
      <p>
        This is my portfolio as a programmer. Under <Link to="/projects" className="text-link">Projects</Link> you can
        search through the things I've built, the <Link to="/blog" className="text-link">Blog</Link> follows the
        development of this site commit by commit (plus the odd milestone like a marathon or a mountain), and{' '}
        <Link to="/whoami" className="text-link">Whoami</Link> tells you a bit more about me. Want to get in touch? Use
        the <Link to="/contact" className="text-link">Contact</Link> page.
      </p>
      <p>
        The site itself is part of the portfolio: a React frontend that loads its projects and blog posts from my own
        ASP.NET Core Web API, which stores them in SQL Server.
      </p>

      <section>
        <h2>WCAG Compliance</h2>
        <p>
          The Web Content Accessibility Guidelines (WCAG) make sure a website works for everyone, including people who
          use a screen reader, navigate with a keyboard or have low vision. That's why I build with semantic HTML, clear
          labels and ARIA attributes, visible focus outlines and enough colour contrast in both light and dark mode.
          I also respect your system's reduced-motion setting, and form errors are announced instead of only shown in red.
        </p>
      </section>

      <section>
        <h2>AI Disclosure</h2>
        <p>
          This website was built with help from AI. I believe engineers can use AI assistance to get projects to
          production faster, but only when it's used responsibly: with a strict spec, many rounds of prompting and close
          review, so the result still matches the original intention. Used that way, handing mundane tasks to a coding
          agent frees up time to dig into documentation and develop other relevant skills. For confidential code or data
          I use my own local AI setup, so nothing leaves my machine.
        </p>
      </section>
    </main>
  );
}
