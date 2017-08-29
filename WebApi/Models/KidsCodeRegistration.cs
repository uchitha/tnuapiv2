using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class KidsCodeRegistration
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string ParentsName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public KidsCodeCourse Course { get; set; }
        public string Comment { get; set; }

    }

    public enum KidsCodeCourse
    {
        None=0,
        Inspire,
        Upskill,
        Master,
    }
}
