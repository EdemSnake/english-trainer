# app/tts_engine.py

import os
import uuid
from kokoro_tts import KPipeline
from pydub import AudioSegment
from .config import VOICES, TMP_PATH

# A dictionary to hold the initialized TTS pipelines for each voice.
# This avoids re-initializing the model on every request, which would be slow.
pipelines = {
    voice["name"]: KPipeline(speaker_id=voice["speaker_id"], device="cpu")
    for voice in VOICES
}

def text_to_speech(text: str, voice_name: str) -> str:
    """
    Generates an audio file from the given text and voice name.

    Args:
        text: The text to convert to speech.
        voice_name: The name of the voice to use.

    Returns:
        The path to the generated MP3 file.
    """
    if voice_name not in pipelines:
        raise ValueError("Voice not found")

    pipeline = pipelines[voice_name]
    
    # Generate a unique filename for the temporary WAV file.
    wav_filename = f"{uuid.uuid4()}.wav"
    wav_path = os.path.join(TMP_PATH, wav_filename)

    # Generate the WAV audio from the text.
    pipeline.text_to_speech(text, wav_path)

    # Convert the WAV file to MP3 using pydub.
    mp3_filename = f"{uuid.uuid4()}.mp3"
    mp3_path = os.path.join(TMP_PATH, mp3_filename)
    
    audio = AudioSegment.from_wav(wav_path)
    audio.export(mp3_path, format="mp3")

    # Clean up the temporary WAV file.
    os.remove(wav_path)

    return mp3_path