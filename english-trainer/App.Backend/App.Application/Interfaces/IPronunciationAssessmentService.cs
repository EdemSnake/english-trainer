using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IPronunciationAssessmentService
    {
        Task<string> AssessPronunciationAsync(byte[] audioData, string referenceText);
    }
}