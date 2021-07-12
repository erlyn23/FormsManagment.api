using System;
using System.Collections.Generic;

#nullable disable

namespace SimpleJwt.Models.Entities
{
    public partial class Answer
    {
        public int AnswerId { get; set; }
        public string Answer1 { get; set; }
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
