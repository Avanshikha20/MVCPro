using System.ComponentModel.DataAnnotations;

namespace CodeFirstMVC.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }

        [Required]
        [StringLength(100)]
        public string DeptName { get; set; }

        //Navigation Property
        public ICollection<Employee>? Employees { get; set; }

    }
}
