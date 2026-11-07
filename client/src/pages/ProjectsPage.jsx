import { useState } from 'react';
import { formatDate, getProjects } from '../api/portfolioApi.js';
import useApiData from '../hooks/useApiData.js';
import useDocumentTitle from '../hooks/useDocumentTitle.js';

export default function ProjectsPage() {
  useDocumentTitle('Projects');
  const { data: projects, status } = useApiData(getProjects);
  const [query, setQuery] = useState('');

  const loadFailed = status === 'error';
  const normalizedQuery = query.trim().toLocaleLowerCase();
  const visibleProjects = (projects ?? []).filter((project) =>
    `${project.category} ${project.title} ${project.description}`.toLocaleLowerCase().includes(normalizedQuery)
  );

  let statusMessage = '';
  if (status === 'loading') statusMessage = 'Loading projects...';
  else if (loadFailed) statusMessage = 'Projects are unavailable because the Portfolio API could not be reached.';
  else if (visibleProjects.length === 0) statusMessage = 'No projects found.';
  else statusMessage = `Showing ${visibleProjects.length} of ${projects.length} projects.`;

  const statusClass = loadFailed
    ? 'search-status is-visually-hidden'
    : `search-status${status === 'success' && visibleProjects.length === 0 ? ' is-error' : ''}`;

  return (
    <main className="page-content card-page">
      <h1>Projects</h1>

      <div className="project-search">
        <label htmlFor="project-search-input">Search projects</label>
        <input
          id="project-search-input"
          type="search"
          className={loadFailed ? 'is-load-error' : undefined}
          placeholder={loadFailed ? 'Projects unavailable: API not reachable' : 'Search by title or keyword'}
          disabled={loadFailed}
          value={query}
          onChange={(event) => setQuery(event.target.value)}
          aria-controls="project-list"
          aria-describedby="project-search-status"
        />
      </div>
      <p className={statusClass} id="project-search-status" role="status" aria-live="polite">
        {statusMessage}
      </p>

      <div className="card-grid" id="project-list">
        {visibleProjects.map((project) => (
          <article className="content-card" key={project.id}>
            <p className="card-date">
              {project.category} · <time dateTime={project.date}>{formatDate(project.date)}</time>
            </p>
            <h2>{project.title}</h2>
            <p>{project.description}</p>
            {project.gitHubUrl && (
              <p className="card-link">
                <a href={project.gitHubUrl} target="_blank" rel="noreferrer">
                  View on GitHub<span className="is-visually-hidden"> (opens in a new tab)</span>
                </a>
              </p>
            )}
          </article>
        ))}
      </div>
    </main>
  );
}
