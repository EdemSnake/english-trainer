using App.Application.Interfaces;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.PronunciationAssessment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; // Added for ILogger
using System;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class PronunciationAssessmentService : IPronunciationAssessmentService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PronunciationAssessmentService> _logger; // Injected ILogger

        public PronunciationAssessmentService(IConfiguration configuration, ILogger<PronunciationAssessmentService> logger)
        {
            _configuration = configuration;
            _logger = logger; // Assigned injected logger
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
                _logger.LogInformation("Speech recognized successfully."); // Log success
                return result.Properties.GetProperty(PropertyId.SpeechServiceResponse_JsonResult);
            }
            else if (result.Reason == ResultReason.NoMatch)
            {
                _logger.LogWarning("NOMATCH: Speech could not be recognized."); // Log warning
                // Depending on desired behavior, you might throw an exception or return null
                return null; 
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = CancellationDetails.FromResult(result);
                _logger.LogError("CANCELED: Reason={Reason}. ErrorDetails={ErrorDetails}", cancellation.Reason, cancellation.ErrorDetails); // Log error
                throw new Exception($"CANCELED: Reason={cancellation.Reason}. ErrorDetails={cancellation.ErrorDetails}");
            }

            return null;
        }
    }
}