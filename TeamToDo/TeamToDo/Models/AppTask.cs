using System;
using System.Collections.Generic;
using System.Text;

namespace TeamToDo.Models
{
    public class AppTask
    {
        public string Id { get; set; }

        public string TaskTitle { get; set; }

        public string Priority { get; set; }

        public string Role { get; set; }

        public DateTime Deadline { get; set; }

        public string PriorityColor { get; set; }

        public string DeadlineColor { get; set; }
    }
}
