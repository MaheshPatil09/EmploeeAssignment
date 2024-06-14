using EmploeeAssignment.Dto;

namespace EmploeeAssignment.Interfaces
{
    public interface IEmployeeBas
    {
        Task<EmployeeBasDto> AddEmployee(EmployeeBasDto employeeBasDto);
        Task<List<EmployeeBasDto>> GetAllEmployee();
        Task<EmployeeBasDto> GetEmployeeId(string UId);
        Task<EmployeeBasDto> UpdateEmployee(EmployeeBasDto employeeBasDto);
        Task<string> DeleteEmployee(string UId);
    }
}
