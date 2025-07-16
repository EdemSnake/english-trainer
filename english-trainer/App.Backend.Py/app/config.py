# app/config.py

# A list of available voices for the TTS engine.
# Each voice is a dictionary containing its name, the speaker ID, and the language.
VOICES = [
   {"name": "af_heart", "lang_code": "a"},
    {"name": "br_heart", "lang_code": "p"},
    {"name": "fr_claire", "lang_code": "f"},
]

# The path to the directory where temporary audio files will be stored.
# Using /tmp/ is a common convention for temporary files on Unix-like systems.
TMP_PATH = "/tmp"

# The path where the final MP3 files will be stored.
# This should be a publicly accessible directory in the .NET application.
AUDIO_OUTPUT_PATH = "english-trainer/App.Backend/App.WebApi/wwwroot/audio"