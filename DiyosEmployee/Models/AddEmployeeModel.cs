namespace DiyosEmployee.Models
{
    public class AddEmployeeModel
    {
        public int EmpId { get; set; }
        public string? EmpName { get; set; }
        public string? Gender { get; set; }
        public DateTime DOB { get; set; }
        public string? Place { get; set; }
       
    }
}
