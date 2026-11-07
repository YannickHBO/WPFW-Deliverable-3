// Calls the ASP.NET Core Portfolio API. In development, Vite proxies /api to http://localhost:5080
// (see vite.config.js). Set VITE_API_URL to point at a different API host.
const baseUrl = import.meta.env.VITE_API_URL ?? '';

async function getJson(path, signal) {
  const response = await fetch(`${baseUrl}${path}`, { signal });
  if (!response.ok) throw new Error(`Request to ${path} failed with status ${response.status}`);
  return response.json();
}

/** @returns {Promise<Array<{id:number,title:string,description:string,category:string,gitHubUrl:string|null,date:string}>>} */
export const getProjects = (signal) => getJson('/api/projects', signal);

/** @returns {Promise<Array<{id:number,title:string,content:string,publishedDate:string}>>} */
export const getBlogPosts = (signal) => getJson('/api/blogposts', signal);

/** Formats an API date ("2026-06-14") as "June 14, 2026" without timezone shifts. */
export function formatDate(isoDate) {
  const [year, month, day] = isoDate.split('-').map(Number);
  return new Date(year, month - 1, day).toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'long',
    day: 'numeric'
  });
}
