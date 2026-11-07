import { useEffect, useState } from 'react';

/**
 * Loads data once from an API function that accepts an AbortSignal.
 * Returns { data, status } where status is 'loading' | 'success' | 'error'.
 */
export default function useApiData(load) {
  const [state, setState] = useState({ data: null, status: 'loading' });

  useEffect(() => {
    const controller = new AbortController();
    load(controller.signal)
      .then((data) => setState({ data, status: 'success' }))
      .catch((error) => {
        if (error.name !== 'AbortError') setState({ data: null, status: 'error' });
      });
    return () => controller.abort();
  }, [load]);

  return state;
}
