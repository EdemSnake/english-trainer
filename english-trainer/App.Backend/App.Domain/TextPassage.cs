using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain
{
    public class TextPassage
    {
        public Guid Id { get; set; }                
        public string Content { get; set; }
        public string DifficultyLevel { get; set; }
        public string NativeSpeakerAudioUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<AssessmentResult> AssessmentResults { get; set; } = new List<AssessmentResult>();
    }
}
