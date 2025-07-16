# app/tts_engine.py
import os
import uuid
import tempfile
import soundfile as sf
from kokoro import KPipeline
from .config import VOICES, AUDIO_OUTPUT_PATH
import numpy as np

# Инициализируем пайплайны один раз при запуске
pipelines = {
    voice["name"]: KPipeline(lang_code=voice["lang_code"], device="cpu")
    for voice in VOICES
}

def text_to_speech(text: str, voice_name: str) -> str:
    if voice_name not in pipelines:
        raise ValueError(f"Voice '{voice_name}' not found")

    pipeline = pipelines[voice_name]

    # Создаем директорию для выходных файлов
    os.makedirs(AUDIO_OUTPUT_PATH, exist_ok=True)

    # Генерируем уникальное имя файла
    wav_filename = f"{uuid.uuid4()}.wav"
    wav_path = os.path.join(AUDIO_OUTPUT_PATH, wav_filename)

    try:
        # ⚠️ Получаем генератор аудио-фрагментов
        generator = pipeline(text, voice=voice_name)

        # Собираем все аудио-фрагменты в список
        audio_chunks = []
        for _, _, audio in generator:
            audio_chunks.append(audio)

        # Склеиваем в один массив
        import numpy as np
        samples = np.concatenate(audio_chunks)

        # Приводим к нужному формату
        if samples.ndim > 1:
            samples = samples[:, 0]  # моно

        if samples.dtype != np.float32:
            samples = samples.astype(np.float32)

        # Записываем в файл
        sf.write(wav_path, samples, 24000)  # 24000 Гц — стандарт в kokoro

        if not os.path.exists(wav_path):
            raise RuntimeError("Failed to generate audio file")

        return wav_path

    except Exception as e:
        if os.path.exists(wav_path):
            os.remove(wav_path)
        raise RuntimeError(f"TTS generation failed: {e}")
    if voice_name not in pipelines:
        raise ValueError(f"Voice '{voice_name}' not found")

    pipeline = pipelines[voice_name]

    # Создаем директорию для выходных файлов
    os.makedirs(AUDIO_OUTPUT_PATH, exist_ok=True)

    # Генерируем уникальное имя файла
    wav_filename = f"{uuid.uuid4()}.wav"
    wav_path = os.path.join(AUDIO_OUTPUT_PATH, wav_filename)

    try:
        # Генерируем аудиоданные
        waveform = pipeline(text)
        
        # Конвертируем в numpy если это torch tensor
        if hasattr(waveform, 'numpy'):
            samples = waveform.numpy()
        elif isinstance(waveform, (list, tuple, range, map, zip)):
            samples = np.array(list(waveform))
        elif hasattr(waveform, '__iter__'):
            samples = np.array(list(waveform))  # генератор
        else:
            samples = np.array(waveform)  # последний шанс

        # Убеждаемся что это одномерный массив (моно)
        if len(samples.shape) > 1:
            # Если стерео, берем только первый канал
            samples = samples[:, 0]
        
        # Нормализуем значения если нужно
        if samples.dtype != 'float32':
            samples = samples.astype('float32')

        # Используем soundfile вместо scipy - он проще и легче
        sf.write(wav_path, samples, 22050)
        
        if not os.path.exists(wav_path):
            raise RuntimeError("Failed to generate audio file")
            
        return wav_path
        
    except Exception as e:
        # Очищаем файл при ошибке
        if os.path.exists(wav_path):
            os.remove(wav_path)
        raise RuntimeError(f"TTS generation failed: {e}")