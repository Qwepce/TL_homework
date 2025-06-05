import { useState, useEffect } from 'react';

export const useFetch = <T>(url: string): [T | undefined, boolean, string] => {
  const [data, setData] = useState<T | undefined>(undefined);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string>('');

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      setError('');

      const result = await fetch(url)
        .then((res) => {
          if (!res.ok) {
            throw new Error(`HTTP-request error. Status: ${res.status}`);
          }

          return res.json();
        })
        .then((data: T) => setData(data))
        .catch(() => setError('Could not get data from the server.'))
        .finally(() => setLoading(false));

      return result;
    };
    
    fetchData();
  }, [url]);

  return [data, loading, error];
};
