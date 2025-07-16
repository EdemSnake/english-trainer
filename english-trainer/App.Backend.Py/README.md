# Python Backend

This directory contains the Python backend for the English Trainer application. It is intended to house microservices that will work in conjunction with the main .NET backend.

## Kokoro TTS Microservice

This microservice provides text-to-speech functionality using Kokoro TTS.

### Setup

1.  **Install Dependencies:**
    ```bash
    pip install -r requirements.txt
    ```

2.  **Run the API (Optional):**
    ```bash
    uvicorn app.main:app --reload
    ```

3.  **Run the Worker:**
    ```bash
    python worker.py
    ```

### API Endpoints (for direct access)

*   `GET /voices`: Returns a list of available voices.
*   `POST /speak`: Generates speech from text.
    *   **Body:** `{"text": "Hello, world!", "voice": "en-us-amy"}`