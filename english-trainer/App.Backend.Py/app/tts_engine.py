import os
import uuid
import soundfile as sf
import numpy as np
from kokoro import KPipeline
from .config import VOICES, AUDIO_OUTPUT_PATH

# Предзагрузка пайплайнов
pipelines = {
    voice["name"]: KPipeline(lang_code=voice["lang_code"], device="cpu")
    for voice in VOICES
}

def text_to_speech(text: str, voice_name: str) -> str:
    if voice_name not in pipelines:
        raise ValueError(f"Voice '{voice_name}' not found")

    pipeline = pipelines[voice_name]
    generator = pipeline(text, voice=voice_name)

    audio_segments = []
    for _, _, audio in generator:
        audio_segments.append(audio)

    if not audio_segments:
        raise RuntimeError("No audio generated")

    # Склеиваем все сегменты
    full_audio = np.concatenate(audio_segments)

    # Сохраняем WAV
    filename = f"{uuid.uuid4()}.wav"
    path = os.path.join(AUDIO_OUTPUT_PATH, filename)
    sf.write(path, full_audio, samplerate=24000)
    print(f"[TTS Engine] Saved audio to: {path}")
    return path
