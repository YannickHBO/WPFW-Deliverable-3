import { Link } from 'react-router-dom';
import useDocumentTitle from '../hooks/useDocumentTitle.js';
import profilePicture from '../profile-placeholder.png';

export default function WhoamiPage() {
  useDocumentTitle('Who Am I');

  return (
    <main className="page-content profile-page">
      <div className="profile-layout">
        <img className="profile-image" src={profilePicture} alt="Profile picture" />
        <div className="profile-copy">
          <h1>Who am I?</h1>
          <p>
            I'm Yannick, a programmer with a strong interest in cybersecurity solutions, data analysis and
            visualization: building software that keeps systems safe and turns raw data into insights people can
            actually read.
          </p>
          <p>
            Away from the keyboard you'll usually find me outside, because I'm an avid hiker and runner. This year I
            climbed Mount Fuji via the Sengen trail before the season had opened, a 15-hour, 42-kilometre day through
            the snow, and I ran the very first edition of The Hague Marathon in 3:35:59. You can read about both, with
            photos, on my <Link to="/blog" className="text-link">blog</Link>.
          </p>
        </div>
      </div>
    </main>
  );
}
