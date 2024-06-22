using AutoMapper;
using EmploeeAssignment.Common;
using EmploeeAssignment.CosmosDb;
using EmploeeAssignment.Dto;
using EmploeeAssignment.Entities;
using EmploeeAssignment.Interfaces;
using Newtonsoft.Json;




namespace EmploeeAssignment.Service
{
    public class EmployeeBas : IEmployeeBas
    {
        public readonly ICosmosService _cismosService;
        public readonly IMapper _mapper;
        public EmployeeBas(ICosmosService cosmosService, IMapper mapper)
        {
            _cismosService = cosmosService;
            _mapper = mapper;
        }

       public async Task<EmployeeBasDto> AddEmployee(EmployeeBasDto employeeBasDto)
        {

             var employee = _mapper.Map<EmployeeBasEntity>(employeeBasDto);
             employee.Initialize(true, Credentials.docType, "Mahesh", "Mahesh");
            var response = await _cismosService.AddEmployee(employee);
            var responseModel = _mapper.Map<EmployeeBasDto>(response);
            return responseModel;
       }
        public async Task<List<EmployeeBasDto>> GetAllEmployee()
        {
            var response = await _cismosService.GetAllEmployee();
            List<EmployeeBasDto> employeeBas = new List<EmployeeBasDto>();
            foreach (var employee in response)
            {
                 var responseModel = _mapper.Map<EmployeeBasDto>(employee);
                 employeeBas.Add(responseModel);
            }
            return employeeBas;
        }
        public async Task<EmployeeBasDto> GetEmployeeId(string UId)
        {
            var response = await _cismosService.GetEmployeeId(UId);
            var responseModel = _mapper.Map<EmployeeBasDto>(response);
            return responseModel;
        }
        public async Task<EmployeeBasDto> UpdateEmployee(EmployeeBasDto employeeBasDto)
        {
            var exeemployee = await _cismosService.GetEmployeeId(employeeBasDto.UId);
            exeemployee.Active = false;
            await _cismosService.RePlaceasync(exeemployee);
            exeemployee.Initialize(false, Credentials.docType, "O", "O");
            _mapper.Map(employeeBasDto,exeemployee);
             var response = await _cismosService.AddEmployee(exeemployee);  
             return _mapper.Map<EmployeeBasDto>(response);
        }
    
        public async Task<string> DeleteEmployee(string UId)
        {
            var response = await _cismosService.GetEmployeeId(UId);
             response.Active = false;
             response.Archived = true;
            await _cismosService.RePlaceasync(response);
            response.Initialize(false, Credentials.docType, "Sanjay", "Sanjay");
            response.Active = false;
            //var res = await _cismosService.AddEmployee(response);
            return "Deleted Successfully";
        }

        public async Task<List<EmployeeBasDto>> GetAllEmployeeByRole(string Role)
        {
            var allEmployee = await GetAllEmployee();

            return allEmployee.FindAll(x => x.Role == Role);
            
        }

        public async Task<StudentFilterCriteria> GetAllEmployeeByPagination(StudentFilterCriteria studentFilterCriteria)
        {

            StudentFilterCriteria studentFilterCriteria1 = new StudentFilterCriteria();
            var checkFilter = studentFilterCriteria.Filters.Any(a => a.FieldName == "role");
            var role = "";
            if (checkFilter)
            {
                role = studentFilterCriteria.Filters.Find(a => a.FieldName == "role").FieldValue;
            }

              var employees = await GetAllEmployee();
            var filteredRecords = employees.FindAll(a =>  a.Role == role);
           

             studentFilterCriteria1.TotalCount = employees.Count;
            studentFilterCriteria1.Page = studentFilterCriteria.Page;
            studentFilterCriteria1.PageSize = studentFilterCriteria.PageSize;

             var skip = studentFilterCriteria.PageSize * (studentFilterCriteria.Page - 1);
             employees = employees.Skip(skip).Take(studentFilterCriteria.PageSize).ToList();
            foreach (var employee in filteredRecords)
            {
                studentFilterCriteria1.Employees.Add(employee);
            }
            return studentFilterCriteria1;
        }

        public async Task<StudentModel> AddStudentByMakePostRequest(StudentModel studentModel)
        {
           var serializeObject = JsonConvert.SerializeObject(studentModel);
            var requestObject = await HttpCommonHelper.MakePostRequest(Credentials.StudentUrl,Credentials.AddStudentEndPoint,serializeObject);
            var responseObject = JsonConvert.DeserializeObject<StudentModel>(requestObject);
            return responseObject;
        }

        public async Task<List<StudentModel>> GetStudentByMakeGetRequest()
        {
            var requestObj = await HttpCommonHelper.MakeGetRequest(Credentials.StudentUrl, Credentials.GetStudentEndPoint);
            return JsonConvert.DeserializeObject<List<StudentModel>>(requestObj);   
        }


    }
}
