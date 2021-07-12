using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Models.Requests
{
    public class AnswerDto
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public string Answer { get; set; }
    }
}
