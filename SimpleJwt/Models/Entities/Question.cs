using System;
using System.Collections.Generic;

#nullable disable

namespace SimpleJwt.Models.Entities
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            QuestionOptions = new HashSet<QuestionOption>();
        }

        public int QuestionId { get; set; }
        public int FormId { get; set; }
        public int QuestionTypeId { get; set; }
        public string Title { get; set; }

        public virtual Form Form { get; set; }
        public virtual QuestionType QuestionType { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<QuestionOption> QuestionOptions { get; set; }
    }
}
