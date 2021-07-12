using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Models.Requests
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        [Required(ErrorMessage = "El formulario es requerido")]
        public int FormId { get; set; }
        [Required(ErrorMessage = "El tipo de cuestionario es requerido")]
        public int QuestionTypeId { get; set; }
        public string QuestionType { get; set; }
        [Required(ErrorMessage = "El enunciado es requerido")]
        public string Title { get; set; }
        public List<QuestionOptionDto> QuestionOptions { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
