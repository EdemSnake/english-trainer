using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain
{
    public class WordScore
    {
        public Guid Id { get; set; }
        public Guid AssessmentResultId { get; set; }
        public AssessmentResult AssessmentResult { get; set; }
        public string Word { get; set; }
        public float AccuracyScore { get; set; }
        public string ErrorType { get; set; }
        public int Offset { get; set; }             // позиция в миллисекундах
        public int Duration { get; set; }           // длительность в миллисекундах
        public ICollection<PhonemeScore> PhonemeScores { get; set; } = new List<PhonemeScore>();

    }
}
