// src/config.ts

export const SIGNALR_HUB_PATH = '/ttsHub';
export const TTS_SPEAK_API_PATH = '/api/tts/speak';

// Backend URLs - using localhost:8081 for the .NET backend
export const BACKEND_BASE_URL = 'http://localhost:8081';
export const SIGNALR_HUB_URL = `${BACKEND_BASE_URL}${SIGNALR_HUB_PATH}`;
export const TTS_SPEAK_API_URL = TTS_SPEAK_API_PATH;

// Audio files are served by the .NET backend at /audio/
export const AUDIO_FILES_EXTERNAL_BASE_URL = '/audio/';
