using AutoMapper;
using EmploeeAssignment.Common;
using EmploeeAssignment.CosmosDb;
using EmploeeAssignment.Dto;
using EmploeeAssignment.Entities;
using EmploeeAssignment.Interfaces;




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

    }
}
