using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstMVC.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("EmployeeName")]
        [Required(ErrorMessage = "Employee Name is required.")]
        [StringLength(300)]
        public string EmpName { get; set; }

        [Required(ErrorMessage = "Employee Address is required.")]
        [StringLength(300)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(3000, 1000000, ErrorMessage = "Salary must be between 3000 and 1000000")]
        public double Salary { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Email Address")]


        [MaxLength(50)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }


    }
}
