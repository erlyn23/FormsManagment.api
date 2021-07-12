using System;
using System.Collections.Generic;

#nullable disable

namespace SimpleJwt.Models.Entities
{
    public partial class QuestionOption
    {
        public int QuestionOptionId { get; set; }
        public int QuestionId { get; set; }
        public string Title { get; set; }

        public virtual Question Question { get; set; }
    }
}
