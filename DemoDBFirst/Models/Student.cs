using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace DemoDBFirst.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        //[EmailAddress]
        //public string Email { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }
    }

    public class Status
    {
        [Required]
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }

    public class  studentdto
    {
        public int Id { get; set; }
        public string StatusName { get; set; }

    }
}