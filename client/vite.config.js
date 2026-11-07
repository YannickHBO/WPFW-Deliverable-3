import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    // Forward /api/* to the ASP.NET Core Portfolio API during development,
    // so the browser sees one origin and no CORS configuration is needed.
    proxy: {
      '/api': 'http://localhost:5080'
    }
  }
});
