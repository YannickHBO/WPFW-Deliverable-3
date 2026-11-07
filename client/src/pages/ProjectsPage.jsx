import { useState } from 'react';
import { formatMonthYear, getProjects } from '../api/portfolioApi.js';
import TagList from '../components/TagList.jsx';
import useApiData from '../hooks/useApiData.js';
import useDocumentTitle from '../hooks/useDocumentTitle.js';

export default function ProjectsPage() {
  useDocumentTitle('Projects');
  const { data: projects, status } = useApiData(getProjects);
  const [query, setQuery] = useState('');

  const loadFailed = status === 'error';
  const normalizedQuery = query.trim().toLocaleLowerCase();
  const visibleProjects = (projects ?? []).filter((project) =>
    `${project.category} ${project.title} ${project.description} ${(project.tags ?? []).join(' ')}`
      .toLocaleLowerCase()
      .includes(normalizedQuery)
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
          <article className="content-card project-card" key={project.id}>
            <header className="project-card__header">
              <h2>{project.title}</h2>
              <div className="project-card__badges">
                {project.inProgress && <span className="badge badge--progress">in progress</span>}
                {/* Closed source unless the project has a public GitHub link. */}
                {!project.gitHubUrl && <span className="badge badge--closed">closed source</span>}
                {project.gitHubUrl && (
                  <a className="badge badge--link" href={project.gitHubUrl} target="_blank" rel="noreferrer">
                    open source <span aria-hidden="true">↗</span>
                    <span className="is-visually-hidden">: view on GitHub (opens in a new tab)</span>
                  </a>
                )}
              </div>
            </header>

            <p className="project-card__subtitle">
              {project.category} · {project.inProgress ? 'started ' : ''}
              <time dateTime={project.date.slice(0, 7)}>{formatMonthYear(project.date)}</time>
            </p>

            <p className="project-card__description">{project.description}</p>

            <TagList tags={project.tags} />
          </article>
        ))}
      </div>
    </main>
  );
}
