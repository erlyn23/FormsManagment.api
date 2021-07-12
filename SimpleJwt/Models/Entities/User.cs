using System;
using System.Collections.Generic;

#nullable disable

namespace SimpleJwt.Models.Entities
{
    public partial class User
    {
        public User()
        {
            Forms = new HashSet<Form>();
            UserFormAnswers = new HashSet<UserFormAnswer>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfilePhoto { get; set; }

        public virtual ICollection<Form> Forms { get; set; }
        public virtual ICollection<UserFormAnswer> UserFormAnswers { get; set; }
    }
}
