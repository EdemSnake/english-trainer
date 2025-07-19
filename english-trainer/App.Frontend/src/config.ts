// src/config.ts

export const BACKEND_BASE_URL = 'http://backend:8080';
export const SIGNALR_HUB_PATH = '/ttsHub';
export const TTS_SPEAK_API_PATH = '/api/tts/speak';

export const SIGNALR_HUB_URL = `${BACKEND_BASE_URL}${SIGNALR_HUB_PATH}`;
export const TTS_SPEAK_API_URL = `${BACKEND_BASE_URL}${TTS_SPEAK_API_PATH}`;

export const AUDIO_FILES_EXTERNAL_BASE_URL = 'http://localhost:8081';
