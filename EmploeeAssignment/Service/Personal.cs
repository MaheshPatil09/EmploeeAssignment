using AutoMapper;
using EmploeeAssignment.Common;
using EmploeeAssignment.CosmosDb;
using EmploeeAssignment.Dto;
using EmploeeAssignment.Entities;
using EmploeeAssignment.Interfaces;

namespace EmploeeAssignment.Service
{
    public class Personal : IPersonal
    {
        public readonly ICosmosService _cismosService;
        public readonly IMapper _mapper;
        public Personal(ICosmosService cosmosService, IMapper mapper)
        {
            _cismosService = cosmosService;
            _mapper = mapper;
        }

        public async Task<PersonalDto> AddPersonal(PersonalDto personalDto)
        {

            var employee = _mapper.Map<PersonalEntity>(personalDto);
            employee.Initialize(true, Credentials.docType, "Mahesh", "Mahesh");
            var response = await _cismosService.AddPersonal(employee);
            var responseModel = _mapper.Map<PersonalDto>(response);
            return responseModel;
        }
        public async Task<List<PersonalDto>> GetAllPersonal()
        {
            var response = await _cismosService.GetAllPersonal();
            List<PersonalDto> employeeBas = new List<PersonalDto>();
            foreach (var employee in response)
            {
                var responseModel = _mapper.Map<PersonalDto>(employee);
                employeeBas.Add(responseModel);
            }
            return employeeBas;
        }
        public async Task<PersonalDto> GetPersonalId(string UId)
        {
            var response = await _cismosService.GetPersonalId(UId);
            var responseModel = _mapper.Map<PersonalDto>(response);
            return responseModel;
        }
        public async Task<PersonalDto> UpdatePersonal(PersonalDto personalDto)
        {
            var exeemployee = await _cismosService.GetPersonalId(personalDto.UId);
            exeemployee.Active = false;
            exeemployee.Archived = true;
            await _cismosService.RePlaceasync(exeemployee);
            exeemployee.Initialize(false, Credentials.docType, "Om", "Om");
            _mapper.Map(personalDto, exeemployee);
            var response = await _cismosService.AddPersonal(exeemployee);
            return _mapper.Map<PersonalDto>(response);
        }
        public async Task<string> DeletePersonal(string UId)
        {
            var response = await _cismosService.GetPersonalId(UId);
            response.Active = false;
            response.Archived = true;
            await _cismosService.RePlaceasync(response);
            response.Initialize(false, Credentials.docType, "Sanjay", "Sanjay");
            response.Active = false;
            //var res = await _cismosService.AddPersonal(response);
            return "Deleted Successfully";
        }
    }
}
