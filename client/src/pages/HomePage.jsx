import useDocumentTitle from '../hooks/useDocumentTitle.js';

export default function HomePage() {
  useDocumentTitle('Home');

  return (
    <main className="page-content">
      <h1>Welcome to WPFWDesigns</h1>
      <p>
        Here you will find a description of the site; Lorem ipsum dolor sit amet, consectetur adipiscing elit.
        Pellentesque a nisl lacus. Pellentesque scelerisque finibus interdum. Mauris consectetur lectus vitae urna
        vestibulum, ut vestibulum nulla euismod. Nunc sed nisl ac ex dignissim aliquam. Duis lorem magna, mattis non
        placerat ac, iaculis vestibulum velit.
      </p>

      <section>
        <h2>WCAG Compliance</h2>
        <p>
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque a nisl lacus. Pellentesque scelerisque
          finibus interdum. Mauris consectetur lectus vitae urna vestibulum, ut vestibulum nulla euismod. Nunc sed nisl
          ac ex dignissim aliquam.
        </p>
      </section>
    </main>
  );
}
