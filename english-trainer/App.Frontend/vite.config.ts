import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import path from 'path';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'),
    },
  },
  server: {
    host: '0.0.0.0',
    port: 3000,
    proxy: {
      '/api': {
        target: 'http://backend:8080',
        changeOrigin: true,
        secure: false,
      },
      '/ttsHub': {
        target: 'http://backend:8080',
        changeOrigin: true,
        secure: false,
        ws: true, // Enable WebSocket proxying for SignalR
      },
      '/app/audio': {
        target: 'http://backend:8080',
        changeOrigin: true,
        secure: false,
      },
    },
  },
});
