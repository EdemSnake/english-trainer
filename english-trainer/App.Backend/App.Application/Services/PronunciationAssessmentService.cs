using App.Application.Interfaces;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.PronunciationAssessment;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class PronunciationAssessmentService : IPronunciationAssessmentService
    {
        private readonly IConfiguration _configuration;

        public PronunciationAssessmentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> AssessPronunciationAsync(byte[] audioData, string referenceText)
        {
            var speechConfig = SpeechConfig.FromSubscription(
                _configuration["SpeechService:SubscriptionKey"],
                _configuration["SpeechService:Region"]);

            using var audioInputStream = AudioInputStream.CreatePushStream();
            using var audioConfig = AudioConfig.FromStreamInput(audioInputStream);

            using var recognizer = new SpeechRecognizer(speechConfig, audioConfig);
            var pronunciationAssessmentConfig = new PronunciationAssessmentConfig(
                referenceText,
                GradingSystem.HundredMark,
                Granularity.Phoneme,
                true);

            pronunciationAssessmentConfig.ApplyTo(recognizer);

            audioInputStream.Write(audioData);
            audioInputStream.Close();

            var result = await recognizer.RecognizeOnceAsync();

            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                return result.Properties.GetProperty(PropertyId.SpeechServiceResponse_JsonResult);
            }
            else if (result.Reason == ResultReason.NoMatch)
            {
                throw new Exception("NOMATCH: Speech could not be recognized.");
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = CancellationDetails.FromResult(result);
                throw new Exception($"CANCELED: Reason={cancellation.Reason}. ErrorDetails={cancellation.ErrorDetails}");
            }

            return null;
        }
    }
}