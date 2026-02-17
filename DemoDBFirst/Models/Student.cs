using System.ComponentModel.DataAnnotations;

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
}