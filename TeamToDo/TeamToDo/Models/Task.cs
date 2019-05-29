using System;
using System.Collections.Generic;
using System.Text;

namespace TeamToDo.Models
{
    public class Task
    {
        public string Id { get; set; }

        public string TaskTitle { get; set; }

        public int Priority { get; set; }

        public int RoleId { get; set; }

        public DateTime Deadline { get; set; }

        public string User { get; set; }

        public string Username { get; set; }
    }
}
