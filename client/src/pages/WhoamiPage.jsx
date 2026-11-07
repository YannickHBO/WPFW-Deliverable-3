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
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam dolor massa, mollis ut volutpat sed,
            accumsan in justo. Cras vestibulum ex vel velit lacinia lacinia. Nunc sagittis, felis ut tristique sodales,
            quam nibh imperdiet magna, ut mollis mi sem nec lorem. Etiam erat sapien, porta at neque vel, aliquam
            vestibulum enim.
          </p>
        </div>
      </div>
    </main>
  );
}
