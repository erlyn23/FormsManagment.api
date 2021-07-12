using System;
using System.Collections.Generic;

#nullable disable

namespace SimpleJwt.Models.Entities
{
    public partial class Form
    {
        public Form()
        {
            Questions = new HashSet<Question>();
            UserFormAnswers = new HashSet<UserFormAnswer>();
        }

        public int FormId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<UserFormAnswer> UserFormAnswers { get; set; }
    }
}
