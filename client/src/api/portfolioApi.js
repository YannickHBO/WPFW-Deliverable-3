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

/** @returns {Promise<Array<{id:number,title:string,content:string,publishedDate:string,images:Array<{url:string,alt:string}>}>>} publishedDate e.g. "2026-09-14T11:28:13" */
export const getBlogPosts = (signal) => getJson('/api/blogposts', signal);

/** Formats an API date ("2026-06-14", or the date part of "2026-06-14T10:30:00") as "June 14, 2026" without timezone shifts. */
export function formatDate(isoDate) {
  const [year, month, day] = isoDate.slice(0, 10).split('-').map(Number);
  return new Date(year, month - 1, day).toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'long',
    day: 'numeric'
  });
}

/** Formats an API date ("2024-12-01") as "December 2024". Used for projects, whose dates are month precision. */
export function formatMonthYear(isoDate) {
  const [year, month] = isoDate.slice(0, 7).split('-').map(Number);
  return new Date(year, month - 1, 1).toLocaleDateString('en-US', { year: 'numeric', month: 'long' });
}

/** Formats "2026-09-14T11:28:13" as "11:28" (24-hour clock), exactly as stored. */
export function formatTime(isoDateTime) {
  return isoDateTime.slice(11, 16);
}
