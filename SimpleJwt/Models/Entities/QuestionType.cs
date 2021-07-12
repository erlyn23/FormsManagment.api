using System;
using System.Collections.Generic;

#nullable disable

namespace SimpleJwt.Models.Entities
{
    public partial class QuestionType
    {
        public QuestionType()
        {
            Questions = new HashSet<Question>();
        }

        public int QuestionTypeId { get; set; }
        public string QuestionType1 { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
