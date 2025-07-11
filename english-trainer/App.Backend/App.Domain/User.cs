﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain
{
    public class User
    {
        public Guid Id { get; set; }                
        public string Username { get; set; }        
        public string Email { get; set; }           
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<AssessmentResult> AssessmentResults { get; set; } = new List<AssessmentResult>();
    }
}
