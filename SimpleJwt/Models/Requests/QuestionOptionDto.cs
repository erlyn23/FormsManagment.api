using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Models.Requests
{
    public class QuestionOptionDto
    {
        public int QuestionOptionId { get; set; }
        [Required(ErrorMessage = "El cuestionario es requerido")]
        public int QuestionId { get; set; }
        [Required(ErrorMessage = "El título es requerido")]
        public string Title { get; set; }
    }
}
