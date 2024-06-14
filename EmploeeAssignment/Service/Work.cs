using AutoMapper;
using EmploeeAssignment.Common;
using EmploeeAssignment.CosmosDb;
using EmploeeAssignment.Dto;
using EmploeeAssignment.Entities;
using EmploeeAssignment.Interfaces;

namespace EmploeeAssignment.Service
{
    public class Work : IWork
    {
        public readonly ICosmosService _cismosService;
        public readonly IMapper _mapper;

        public Work(ICosmosService cosmosService, IMapper mapper)
        {
            _cismosService = cosmosService;
            _mapper = mapper;
        }
        public async Task<WorkInfoDto> AddWorkInfo(WorkInfoDto workInfoDto)
        {

            var employee = _mapper.Map<WorkEntity>(workInfoDto);
            employee.Initialize(true, Credentials.docType, "Mahesh", "Mahesh");
            var response = await _cismosService.AddWorkInfo(employee);
            var responseModel = _mapper.Map<WorkInfoDto>(response);
            return responseModel;
        }
        public async Task<List<WorkInfoDto>> GetAllWorkInfo()
        {
            var response = await _cismosService.GetAllWorkInfo();
            List<WorkInfoDto> employeeBas = new List<WorkInfoDto>();
            foreach (var employee in response)
            {
                var responseModel = _mapper.Map<WorkInfoDto>(employee);
                employeeBas.Add(responseModel);
            }
            return employeeBas;
        }
        public async Task<WorkInfoDto> GetWorkInfoByUId(string UId)
        {
            var response = await _cismosService.GetWorkInfoByUId(UId);
            var responseModel = _mapper.Map<WorkInfoDto>(response);
            return responseModel;
        }
        public async Task<WorkInfoDto> UpdateWorkInfo(WorkInfoDto workInfoDto)
        {
            var exeemployee = await _cismosService.GetWorkInfoByUId(workInfoDto.UId);
            exeemployee.Active = false;
            exeemployee.Archived = true;
            await _cismosService.RePlaceasync(exeemployee);
            exeemployee.Initialize(false, Credentials.docType, "Prajwal", "Prajwal");
            _mapper.Map(workInfoDto, exeemployee);
            var response = await _cismosService.AddWorkInfo(exeemployee);
            return _mapper.Map<WorkInfoDto>(response);
        }
        public async Task<string> DeleteWorkInfo(string UId)
        {
            var response = await _cismosService.GetWorkInfoByUId(UId);
            response.Active = false;
            response.Archived = true;
            await _cismosService.RePlaceasync(response);
            response.Initialize(false, Credentials.docType, "Sanjay", "Sanjay");
            response.Active = false;
            //var res = await _cismosService.AddWorkInfo(response);
            return "Deleted Successfully";
        }
    }
}
