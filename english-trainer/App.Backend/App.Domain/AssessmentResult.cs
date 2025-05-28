using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain
{
    public class AssessmentResult
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid TextPassageId { get; set; }
        public TextPassage TextPassage { get; set; }

        public float AccuracyScore { get; set; }
        public float FluencyScore { get; set; }
        public float ProsodyScore { get; set; }
        public float PronunciationScore { get; set; }

        public string UserAudioUrl { get; set; }
        public string FullResultJson { get; set; }
        public DateTime AssessedAt { get; set; }


        public ICollection<WordScore> WordScores { get; set; } = new List<WordScore>();
    }
}
