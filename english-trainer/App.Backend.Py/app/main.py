# app/main.py
import traceback
import os
from fastapi import FastAPI, HTTPException, BackgroundTasks
from fastapi.responses import FileResponse
from pydantic import BaseModel
from .config import VOICES
from .tts_engine import text_to_speech
from fastapi import Request
from fastapi.responses import JSONResponse

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
    """Returns a list of available voices."""
    return {"voices": [voice["name"] for voice in VOICES]}

@app.post("/speak")
async def speak(request: SpeakRequest, background_tasks: BackgroundTasks):
    """Generates speech from text using the specified voice."""
    try:
        print(f"[API] Получен запрос на озвучку: text='{request.text[:50]}...', voice='{request.voice}'")
        # Генерируем WAV файл
        wav_path = text_to_speech(request.text, request.voice)
        print(f"[API] Аудиофайл сгенерирован: {wav_path}")
        
        # Планируем удаление файла после отправки
        background_tasks.add_task(os.remove, wav_path)

        # Возвращаем WAV файл напрямую
        return FileResponse(
            path=wav_path,
            media_type="audio/wav",  # Используем WAV вместо MP3
            filename="output.wav",
        )
    except ValueError as e:
        raise HTTPException(status_code=400, detail=str(e))
    except Exception as e:
        traceback.print_exc()
        raise HTTPException(status_code=500, detail="Internal server error")

# Дополнительно: health check для проверки готовности сервиса
@app.get("/health")
async def health_check():
    return {"status": "healthy"}