using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiyosEmployee.Models.Domain
{
    public class EmployeeModel
    {
        public Guid Id { get; set; }
        public int EmpId { get; set; }
        public string? EmpName { get; set; }
        public string? Gender { get; set; }
        public DateTime DOB { get; set; }
        public string? Place { get; set; }
        public byte[]? Photo { get; set; }
    }
}
