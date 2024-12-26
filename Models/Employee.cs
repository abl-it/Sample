using System.Xml.Linq;

namespace Sample.Models
{
    public class Employee
    {
        public int emp_id { get; set; }
        public string fname { get; set; }
        public string minit { get; set; }
        public string lname { get; set; }
        public int job_id { get; set; } = 1;
        public int job_lvl { get; set; } = 10;
        public string pub_id { get; set; } = "9952";
        public DateTime hire_date { get; set; } = DateTime.Now;

    }
}
