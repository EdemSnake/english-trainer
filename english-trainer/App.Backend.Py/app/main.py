from urllib.parse import urljoin

# app/main.py
import traceback
import os
import requests
from fastapi import FastAPI, HTTPException, Request
from fastapi.staticfiles import StaticFiles 
from fastapi.responses import JSONResponse
from pydantic import BaseModel
from .config import VOICES, AUDIO_OUTPUT_PATH  # <--- импорт пути для сохранения
from .tts_engine import text_to_speech

app = FastAPI()

@app.exception_handler(Exception)
async def global_exception_handler(request: Request, exc: Exception):
    traceback.print_exc()
    return JSONResponse(
        status_code=500,
        content={"detail": "An unexpected error occurred."}
    )

class SpeakRequest(BaseModel):
    text: str
    voice: str

@app.get("/voices")
async def get_voices():
    return {"voices": [voice["name"] for voice in VOICES]}

@app.post("/speak")
async def speak(request: SpeakRequest):
    try:
        print(f"[API] Получен запрос: text='{request.text[:50]}...', voice='{request.voice}'")

        # 1. Генерация аудиофайла на диск
        wav_path = text_to_speech(request.text, request.voice)
        filename = os.path.basename(wav_path)

        # 2. Создание относительной ссылки (внутри backend)
        relative_url = f"/audio/{filename}"
        print(f"[API] Относительная ссылка: {relative_url}")

        # 3. Создание абсолютной ссылки для фронтенда (через .NET backend)
        # Audio files will be served by .NET backend which has access to shared volume
        host = "http://localhost:8081"
        full_url = f"{host}/audio/{filename}"
        print(f"[API] Абсолютная ссылка для клиента: {full_url}")

        # 4. Отправка через SignalR хаб
        try:
            signalr_payload = { "audioUrl": full_url }
            backend_url = os.getenv("SIGNALR_HUB_FORWARD_URL", "http://backend:8080/api/tts/send")
            res = requests.post(backend_url, json=signalr_payload)
            print(f"[API] SignalR response: {res.status_code}")
        except Exception as e:
            print(f"[API] Ошибка отправки в SignalR endpoint: {e}")

        return JSONResponse(content={"status": "ok"})

    except ValueError as e:
        raise HTTPException(status_code=400, detail=str(e))
    except Exception as e:
        traceback.print_exc()
        raise HTTPException(status_code=500, detail="Internal server error")

@app.get("/health")
async def health_check():
    return {"status": "healthy"}

# 🔥 FastAPI будет отдавать все файлы из папки /app/audio
app.mount("/audio", StaticFiles(directory=AUDIO_OUTPUT_PATH), name="audio")
