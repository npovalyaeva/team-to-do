using System;
using System.Collections.Generic;
using System.Text;

namespace TeamToDo.Models
{
    public class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        public int AccessLevel { get; set; }
    }
}
