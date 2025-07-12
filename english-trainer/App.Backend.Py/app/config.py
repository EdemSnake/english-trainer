# app/config.py

# A list of available voices for the TTS engine.
# Each voice is a dictionary containing its name, the speaker ID, and the language.
VOICES = [
    {"name": "en-us-amy", "speaker_id": "en-us-amy", "language": "en"},
    {"name": "en-gb-linda", "speaker_id": "en-gb-linda", "language": "en"},
]

# The path to the directory where temporary audio files will be stored.
# Using /tmp/ is a common convention for temporary files on Unix-like systems.
TMP_PATH = "/tmp"