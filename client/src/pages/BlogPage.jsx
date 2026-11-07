import { formatDate, formatTime, getBlogPosts } from '../api/portfolioApi.js';
import BlogGallery from '../components/BlogGallery.jsx';
import useApiData from '../hooks/useApiData.js';
import useDocumentTitle from '../hooks/useDocumentTitle.js';

export default function BlogPage() {
  useDocumentTitle('Blog');
  const { data: posts, status } = useApiData(getBlogPosts);

  return (
    <main className="page-content blog-page">
      <h1>Blog</h1>
      <p className="page-intro">
        Most posts here are git commits from building this site: the commit message is the post and the commit time is
        the publish date. Every now and then I add a post by hand about a milestone away from the keyboard.
      </p>

      {status === 'loading' && <p className="search-status" role="status">Loading blog posts...</p>}
      {status === 'error' && (
        <p className="search-status is-error" role="alert">
          Blog posts are unavailable because the Portfolio API could not be reached.
        </p>
      )}
      {status === 'success' && posts.length === 0 && <p className="search-status">No blog posts yet.</p>}

      {status === 'success' && posts.length > 0 && (
        <div className="blog-feed">
          {posts.map((post) => (
            <article className="blog-entry" key={post.id}>
              <p className="blog-date">
                <time dateTime={post.publishedDate}>
                  {formatDate(post.publishedDate)}
                  <br />
                  {formatTime(post.publishedDate)}
                </time>
              </p>
              <h2>{post.title}</h2>
              <p>{post.content}</p>
              <BlogGallery images={post.images} title={post.title} />
            </article>
          ))}
        </div>
      )}
    </main>
  );
}
