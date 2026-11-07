import { useEffect, useRef, useState } from 'react';

// On PC only this many photos are shown in the post; the rest are in the viewer.
const PC_PREVIEW_COUNT = 2;

/**
 * Photos for a blog post.
 * - PC: the first two photos side by side; the second one says "+N more" when there are more.
 * - Phone: a swipeable gallery (CSS scroll-snap) with all photos and dots to jump between them.
 * - Everywhere: clicking a photo opens it full size in a viewer (native <dialog>) to browse all photos.
 */
export default function BlogGallery({ images, title }) {
  const trackRef = useRef(null);
  const dialogRef = useRef(null);
  const [activeIndex, setActiveIndex] = useState(0);
  const [viewerIndex, setViewerIndex] = useState(null);
  // Width/height ratio per photo, measured on load. Used to give the PC preview row one shared height.
  const [aspects, setAspects] = useState({});

  // Open the native dialog when a photo is selected. showModal() gives focus trapping and Esc-to-close.
  useEffect(() => {
    const dialog = dialogRef.current;
    if (viewerIndex !== null && dialog && !dialog.open) dialog.showModal();
  }, [viewerIndex]);

  if (!images?.length) return null;

  const count = images.length;
  const hiddenOnPc = Math.max(count - PC_PREVIEW_COUNT, 0);
  const aspectOf = (index) => aspects[index] ?? 1;
  const previewRowAspect = images
    .slice(0, PC_PREVIEW_COUNT)
    .reduce((sum, _image, index) => sum + aspectOf(index), 0);

  const handleImageLoad = (index, event) => {
    const { naturalWidth, naturalHeight } = event.currentTarget;
    if (naturalHeight > 0) setAspects((current) => ({ ...current, [index]: naturalWidth / naturalHeight }));
  };

  const handleScroll = () => {
    const track = trackRef.current;
    const itemWidth = track.firstElementChild?.offsetWidth ?? track.clientWidth;
    setActiveIndex(Math.round(track.scrollLeft / itemWidth));
  };

  const scrollToImage = (index) => {
    const track = trackRef.current;
    const item = track.children[index];
    const reduceMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches;
    track.scrollTo({ left: item.offsetLeft - track.offsetLeft, behavior: reduceMotion ? 'auto' : 'smooth' });
  };

  const showInViewer = (index) => setViewerIndex((index + count) % count);

  const handleViewerKeyDown = (event) => {
    if (count < 2) return;
    if (event.key === 'ArrowRight') showInViewer(viewerIndex + 1);
    if (event.key === 'ArrowLeft') showInViewer(viewerIndex - 1);
  };

  const viewerImage = viewerIndex !== null ? images[viewerIndex] : null;

  return (
    <div className={`blog-gallery${count === 1 ? ' blog-gallery--single' : ''}`}>
      {/* Focusable so keyboard users can scroll the gallery on phones with the arrow keys. */}
      <ul
        className="blog-gallery__track"
        ref={trackRef}
        onScroll={handleScroll}
        tabIndex={count > 1 ? 0 : undefined}
        aria-label={`Photos for ${title}`}
        style={{ '--row-aspect': previewRowAspect }}
      >
        {images.map((image, index) => {
          const isExtra = index >= PC_PREVIEW_COUNT; // hidden on PC via CSS, visible on phones
          const showMoreBadge = index === PC_PREVIEW_COUNT - 1 && hiddenOnPc > 0;

          return (
            <li
              className={`blog-gallery__item${isExtra ? ' blog-gallery__item--extra' : ''}`}
              key={image.url}
              style={{ '--aspect': aspectOf(index) }}
            >
              <button type="button" className="blog-gallery__open" onClick={() => showInViewer(index)}>
                <img
                  className="blog-gallery__image"
                  src={image.url}
                  alt={image.alt}
                  loading="lazy"
                  onLoad={(event) => handleImageLoad(index, event)}
                />
                {showMoreBadge && (
                  <span className="blog-gallery__more" aria-hidden="true">
                    +{hiddenOnPc} more
                  </span>
                )}
                <span className="is-visually-hidden">
                  {' '}(photo {index + 1} of {count}, open full size
                  {showMoreBadge ? ` to browse all ${count} photos` : ''})
                </span>
              </button>
            </li>
          );
        })}
      </ul>

      {/* Only shown on phones (see styles.css), where one photo is visible at a time. */}
      {count > 1 && (
        <div className="blog-gallery__dots">
          {images.map((image, index) => (
            <button
              key={image.url}
              type="button"
              className="blog-gallery__dot"
              aria-label={`Show photo ${index + 1} of ${count}`}
              aria-current={index === activeIndex ? 'true' : undefined}
              onClick={() => scrollToImage(index)}
            />
          ))}
        </div>
      )}

      <dialog
        ref={dialogRef}
        className="photo-viewer"
        aria-label={`Photo viewer: ${title}`}
        onClose={() => setViewerIndex(null)}
        onKeyDown={handleViewerKeyDown}
        // Clicking the dark backdrop (the dialog element itself) closes the viewer.
        onClick={(event) => {
          if (event.target === dialogRef.current) dialogRef.current.close();
        }}
      >
        {viewerImage && (
          <figure className="photo-viewer__figure">
            <img className="photo-viewer__image" src={viewerImage.url} alt={viewerImage.alt} />
            <figcaption className="photo-viewer__bar">
              {count > 1 && (
                <button type="button" className="photo-viewer__button" onClick={() => showInViewer(viewerIndex - 1)}>
                  <span aria-hidden="true">←</span> Prev
                </button>
              )}
              <span className="photo-viewer__counter" aria-live="polite">
                {viewerIndex + 1} / {count}
              </span>
              {count > 1 && (
                <button type="button" className="photo-viewer__button" onClick={() => showInViewer(viewerIndex + 1)}>
                  Next <span aria-hidden="true">→</span>
                </button>
              )}
              <button
                type="button"
                className="photo-viewer__button photo-viewer__close"
                onClick={() => dialogRef.current.close()}
                autoFocus
              >
                Close <span aria-hidden="true">✕</span>
              </button>
            </figcaption>
          </figure>
        )}
      </dialog>
    </div>
  );
}
