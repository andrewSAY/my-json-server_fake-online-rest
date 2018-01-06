using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTaskWevService.Models.Entities
{
    public class User: Entity
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }        
    }
}