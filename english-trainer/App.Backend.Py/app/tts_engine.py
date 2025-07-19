# app/tts_engine.py
import os
import uuid
import tempfile
import soundfile as sf
# from kokoro import KPipeline # Temporarily commented out
from .config import VOICES, AUDIO_OUTPUT_PATH
import numpy as np

# Инициализируем пайплайны один раз при запуске
# pipelines = {
#     voice["name"]: KPipeline(lang_code=voice["lang_code"], device="cpu")
#     for voice in VOICES
# }

def text_to_speech(text: str, voice_name: str) -> str:
    print(f"[TTS Engine] Generating dummy audio for text: {text} with voice: {voice_name}")
    # Simulate audio generation
    dummy_audio_url = f"http://example.com/audio/{uuid.uuid4()}.wav"
    return dummy_audio_url
