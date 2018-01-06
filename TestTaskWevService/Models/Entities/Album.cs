using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTaskWevService.Models.Entities
{
    public class Album: Entity
    {
        public int UserId { get; set; }
        public string Title { get; set; }
    }
}