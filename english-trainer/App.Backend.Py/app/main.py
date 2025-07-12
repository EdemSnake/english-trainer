# app/main.py

import os
from fastapi import FastAPI, HTTPException
from fastapi.responses import FileResponse
from pydantic import BaseModel
from .config import VOICES
from .tts_engine import text_to_speech

app = FastAPI()

class SpeakRequest(BaseModel):
    text: str
    voice: str

@app.get("/voices")
async def get_voices():
    """
    Returns a list of available voices.
    """
    return {"voices": [voice["name"] for voice in VOICES]}

@app.post("/speak")
async def speak(request: SpeakRequest):
    """
    Generates speech from text using the specified voice.
    """
    try:
        mp3_path = text_to_speech(request.text, request.voice)
        
        # Return the MP3 file and delete it after sending.
        return FileResponse(
            path=mp3_path,
            media_type="audio/mpeg",
            filename="output.mp3",
            background_tasks={"cleanup": os.remove, "args": [mp3_path]}
        )
    except ValueError as e:
        raise HTTPException(status_code=400, detail=str(e))
    except Exception as e:
        raise HTTPException(status_code=500, detail="An unexpected error occurred.")