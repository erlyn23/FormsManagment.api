using System;
using System.Collections.Generic;

#nullable disable

namespace SimpleJwt.Models.Entities
{
    public partial class UserFormAnswer
    {
        public int UserFormAnswerId { get; set; }
        public int UserId { get; set; }
        public int FormId { get; set; }

        public virtual Form Form { get; set; }
        public virtual User User { get; set; }
    }
}
