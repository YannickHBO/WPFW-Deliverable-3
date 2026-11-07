import { formatDate, getBlogPosts } from '../api/portfolioApi.js';
import useApiData from '../hooks/useApiData.js';
import useDocumentTitle from '../hooks/useDocumentTitle.js';

export default function BlogPage() {
  useDocumentTitle('Blog');
  const { data: posts, status } = useApiData(getBlogPosts);

  return (
    <main className="page-content blog-page">
      <h1>Blog</h1>

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
                <time dateTime={post.publishedDate}>{formatDate(post.publishedDate)}</time>
              </p>
              <h2>{post.title}</h2>
              <p>{post.content}</p>
            </article>
          ))}
        </div>
      )}
    </main>
  );
}
