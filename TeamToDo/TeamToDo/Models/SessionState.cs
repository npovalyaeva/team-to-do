using System;
using System.Collections.Generic;
using System.Text;

namespace TeamToDo.Models
{
    public class SessionState
    {
        public static User CurrentUser { get; set; }
        
        public static int CurrentRoleId { get; set; }

        public static int CurrentAccessLevel { get; set; }
    }
}
