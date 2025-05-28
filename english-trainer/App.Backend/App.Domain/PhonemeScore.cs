using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain
{
    public class PhonemeScore
    {
        public Guid Id { get; set; }

        public Guid WordScoreId { get; set; }
        public WordScore WordScore { get; set; }

        public string Phoneme { get; set; }
        public float AccuracyScore { get; set; }
    }
}
