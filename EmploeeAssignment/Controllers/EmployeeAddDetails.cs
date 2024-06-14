using EmploeeAssignment.Dto;
using EmploeeAssignment.Interfaces;
using EmploeeAssignment.Service;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using System.Drawing;
using System.Net;
using System.Reflection;


namespace EmploeeAssignment.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class EmployeeAddDetails : Controller
    {
        public readonly IEmployeeBas _employeeBas;
        public readonly IWork _work;
        public readonly IPersonal _personal;
        public readonly IIdentity _identity;
        public EmployeeAddDetails(IEmployeeBas employeeBas, IWork work, IPersonal personal, IIdentity identity)
        {
            _employeeBas = employeeBas;
            _work = work;
            _personal = personal;
            _identity = identity;
        }
        [HttpPost]

        public async Task<EmployeeBasDto> AddEmployee(EmployeeBasDto employeeBasDto)
        {
            var response = await _employeeBas.AddEmployee(employeeBasDto);
            return response;
        }
        [HttpGet]

        public async Task<List<EmployeeBasDto>> GetAllEmployee()
        {
            var resposne = await _employeeBas.GetAllEmployee();
            return resposne;
        }
        [HttpGet]
        public async Task<EmployeeBasDto> GetEmployeeId(string UId)
        {
            var response = await _employeeBas.GetEmployeeId(UId);
            return response;
        }
        [HttpPost]
        public async Task<EmployeeBasDto> UpdateEmployee(EmployeeBasDto employeeBasDto)
        {
            var response = await _employeeBas.UpdateEmployee(employeeBasDto);
            return response;
        }
        [HttpPost]
        public async Task<string> DeleteEmployee(string  UId)
        {
            var response = await _employeeBas.DeleteEmployee(UId);
            return response;
        }

        [HttpPost]

        public async Task<WorkInfoDto> AddWorkInfo(WorkInfoDto workInfoDto)
        {
            var response = await _work.AddWorkInfo(workInfoDto);
            return response;
        }
        [HttpGet]
        public async Task<List<WorkInfoDto>> GetAllWorkInfo()
        {
            var response = await _work.GetAllWorkInfo();
            return response;
        }
        [HttpGet]

        public async Task<WorkInfoDto> GetWorkInfoByUId(string UId)
        {
            var response = await _work.GetWorkInfoByUId(UId);
            return response;
        }
        [HttpPost]

        public async Task<WorkInfoDto> UpdateWorkInfo(WorkInfoDto workInfoDto)
        {
            var response = await _work.UpdateWorkInfo(workInfoDto);
            return response;
        }
        [HttpPost]

        public async Task<string> DeleteWorkInfo(string UId)
        {
            var response = await _work.DeleteWorkInfo(UId);
            return response;
        }
        [HttpPost]

        public async Task<PersonalDto> AddPersonal(PersonalDto personalDto)
        {
            var response = await _personal.AddPersonal(personalDto);
            return response;
        }
        [HttpGet]

        public async Task<List<PersonalDto>> GetAllPersonal()
        {
            var resposne = await _personal.GetAllPersonal();
            return resposne;
        }
        [HttpGet]
        public async Task<PersonalDto> GetPersonalId(string UId)
        {
            var response = await _personal.GetPersonalId(UId);
            return response;
        }
        [HttpPost]
        public async Task<PersonalDto> UpdatePersonal(PersonalDto personalDto)
        {
            var response = await _personal.UpdatePersonal(personalDto);
            return response;
        }
        [HttpPost]
        public async Task<string> DeletePersonal(string UId)
        {
            var response = await _personal.DeletePersonal(UId);
            return response;
        }

        [HttpPost]

        public async Task<IdentityDto> AddIdentity(IdentityDto identityDto)
        {
            var response = await _identity.AddIdentity(identityDto);
            return response;
        }
        [HttpGet]

        public async Task<List<IdentityDto>> GetAllIdentity()
        {
            var resposne = await _identity.GetAllIdentity();
            return resposne;
        }
        [HttpGet]
        public async Task<IdentityDto> GetIdentityId(string UId)
        {
            var response = await _identity.GetIdentityId(UId);
            return response;
        }
        [HttpPost]
        public async Task<IdentityDto> UpdateIdentity(IdentityDto identityDto)
        {
            var response = await _identity.UpdateIdentity(identityDto);
            return response;
        }
        [HttpPost]
        public async Task<string> DeleteIdentity(string UId)
        {
            var response = await _identity.DeleteIdentity(UId);
            return response;
        }

        private string  GetStringFromCell(ExcelWorksheet worksheet , int row , int column)
        {
             var cellValue = worksheet.Cells[row, column].Value;
            return cellValue?.ToString()?.Trim();
        }
     

        [HttpPost]

        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
               if(file == null || file.Length == 0)
                {
                  return BadRequest("File is Empty or NULL");
                }
               var employees = new List<EmployeeBasDto>();
               ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var stream = new MemoryStream()) { 
                   file.CopyTo(stream);
                using (var package = new ExcelPackage(stream)) { 
                       var worksheet = package.Workbook.Worksheets[0];
                       var rowCount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++) {
                        var employee = new EmployeeBasDto
                        {
                            UId =       GetStringFromCell(worksheet, row, 1),
                             Salutory = GetStringFromCell(worksheet, row, 2),
                              FirstName = GetStringFromCell(worksheet, row, 3),
                              MiddleName = GetStringFromCell(worksheet, row, 4),
                              LastName = GetStringFromCell(worksheet, row, 5),
                              NickName = GetStringFromCell(worksheet, row, 6),
                              Email = GetStringFromCell(worksheet, row, 7),
                              Mobile = GetStringFromCell(worksheet, row, 8),
                             EmployeeID = GetStringFromCell(worksheet, row, 9),
                              Role = GetStringFromCell(worksheet, row, 10),
                              ReportingManagerUId = GetStringFromCell(worksheet, row, 11),
                               ReportingManagerName = GetStringFromCell(worksheet, row, 12),
                               Address = GetStringFromCell(worksheet, row, 13),
                                AlternateEmail = GetStringFromCell(worksheet, row, 14),
                                AlternateMobile = GetStringFromCell(worksheet, row, 15),
                        };
                        await AddEmployee(employee);
                        employees.Add(employee);
                    }
                }
            }
            return Ok((employees));
        }

        [HttpGet]

        public async Task<IActionResult> Export()
        {
            var employees = await _employeeBas.GetAllEmployee();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");

                worksheet.Cells[1, 1].Value = "UId";
                worksheet.Cells[1, 2].Value = " Salutory";
                worksheet.Cells[1, 3].Value = "  FirstName";
                worksheet.Cells[1, 4].Value = " MiddleName";
                worksheet.Cells[1, 5].Value = " LastName";
                worksheet.Cells[1, 6].Value = " NickName";
                worksheet.Cells[1, 7].Value = "Email";
                worksheet.Cells[1, 8].Value = " Mobile";
                worksheet.Cells[1, 9].Value = "EmployeeID";
                worksheet.Cells[1, 10].Value = "Role";
                worksheet.Cells[1, 11].Value = "ReportingManagerUId";
                worksheet.Cells[1, 12].Value = "ReportingManagerName";
                worksheet.Cells[1, 13].Value = "Address";
                worksheet.Cells[1, 14].Value = "AlternateEmail";
                worksheet.Cells[1, 15].Value = "AlternateMobile";

                using (var range = worksheet.Cells[1, 1, 1, 15])
                {
                      range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGreen);

                }
                for(int i = 0; i<employees.Count; i++)
                {
                    var employee = employees[i];
                    worksheet.Cells[i + 2, 1].Value = employee.UId;
                    worksheet.Cells[i + 2, 2].Value = employee.Salutory;
                    worksheet.Cells[i + 2, 3].Value = employee.FirstName;
                    worksheet.Cells[i + 2, 4].Value = employee.MiddleName;
                    worksheet.Cells[i + 2, 5].Value = employee.LastName;
                    worksheet.Cells[i + 2, 6].Value = employee.NickName;
                    worksheet.Cells[i + 2, 7].Value = employee.Email;
                    worksheet.Cells[i + 2, 8].Value = employee.Mobile;
                    worksheet.Cells[i + 2, 9].Value = employee.EmployeeID;
                    worksheet.Cells[i + 2, 10].Value = employee.Role;
                    worksheet.Cells[i + 2, 11].Value = employee.ReportingManagerUId;
                    worksheet.Cells[i + 2, 12].Value = employee.ReportingManagerName;
                    worksheet.Cells[i + 2, 13].Value = employee.Address;
                    worksheet.Cells[i + 2, 14].Value = employee.AlternateEmail;
                    worksheet.Cells[i + 2, 15].Value = employee.AlternateMobile;
                }
                var stream = new System.IO.MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;
                var fileName = "Employees.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

           }
        }
    }
}
