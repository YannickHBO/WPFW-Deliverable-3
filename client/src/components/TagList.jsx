// Tag colours. Each name maps to a CSS class (.tag--<colour> in styles.css) that defines a dark shade
// for the light theme and a light shade for the dark theme, so tags stay readable (4.5:1+) in both.
const colours = ['blue', 'purple', 'red', 'green', 'yellow', 'orange', 'pink', 'teal', 'indigo', 'cyan', 'lime', 'slate'];

// Known skills get a fixed colour, so the same skill looks the same on every tile.
const knownTags = {
  react: 'blue',
  'asp.net core': 'purple',
  'sql server': 'red',
  sql: 'cyan',
  'c#': 'green',
  javascript: 'yellow',
  'html & css': 'orange',
  figma: 'pink',
  wcag: 'teal',
  kubernetes: 'indigo',
  docker: 'cyan',
  devops: 'slate',
  'local llm': 'lime',
  cybersecurity: 'red',
  'self-hosting': 'slate',
  'supply chain': 'orange',
  npm: 'pink'
};

/** Picks a colour for a tag: a fixed one for known skills, otherwise a stable one based on the name. */
function colourFor(tag) {
  const key = tag.trim().toLowerCase();
  if (knownTags[key]) return knownTags[key];

  let hash = 0;
  for (const char of key) hash = (hash * 31 + char.charCodeAt(0)) >>> 0;
  return colours[hash % colours.length];
}

export default function TagList({ tags, label = 'Skills used' }) {
  if (!tags?.length) return null;

  return (
    <ul className="tag-list" aria-label={label}>
      {tags.map((tag) => (
        <li className={`tag tag--${colourFor(tag)}`} key={tag}>
          {tag}
        </li>
      ))}
    </ul>
  );
}
