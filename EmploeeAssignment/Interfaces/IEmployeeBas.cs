using EmploeeAssignment.Dto;
using EmploeeAssignment.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmploeeAssignment.Interfaces
{
    public interface IEmployeeBas
    {
        Task<EmployeeBasDto> AddEmployee(EmployeeBasDto employeeBasDto);
        Task<List<EmployeeBasDto>> GetAllEmployee();
        Task<EmployeeBasDto> GetEmployeeId(string UId);
        Task<EmployeeBasDto> UpdateEmployee(EmployeeBasDto employeeBasDto);
        Task<string> DeleteEmployee(string UId);

        Task<List<EmployeeBasDto>> GetAllEmployeeByRole(string Role);

        Task<StudentFilterCriteria> GetAllEmployeeByPagination(StudentFilterCriteria studentFilterCriteria);

        Task<StudentModel> AddStudentByMakePostRequest(StudentModel studentModel);

        Task<List<StudentModel>> GetStudentByMakeGetRequest();
    }
}
