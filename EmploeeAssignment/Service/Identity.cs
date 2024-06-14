using AutoMapper;
using EmploeeAssignment.Common;
using EmploeeAssignment.CosmosDb;
using EmploeeAssignment.Dto;
using EmploeeAssignment.Entities;
using EmploeeAssignment.Interfaces;

namespace EmploeeAssignment.Service
{
    public class Identity : IIdentity
    {
        public readonly ICosmosService _cismosService;
        public readonly IMapper _mapper;
        public Identity(ICosmosService cosmosService, IMapper mapper)
        {
            _cismosService = cosmosService;
            _mapper = mapper;
        }

        public async Task<IdentityDto> AddIdentity(IdentityDto identityDto)
        {

            var employee = _mapper.Map<IdentityEntity>(identityDto);
            employee.Initialize(true, Credentials.docType, "Mahesh", "Mahesh");
            var response = await _cismosService.AddIdentity(employee);
            var responseModel = _mapper.Map<IdentityDto>(response);
            return responseModel;
        }
        public async Task<List<IdentityDto>> GetAllIdentity()
        {
            var response = await _cismosService.GetAllIdentity();
            List<IdentityDto> employeeBas = new List<IdentityDto>();
            foreach (var employee in response)
            {
                var responseModel = _mapper.Map<IdentityDto>(employee);
                employeeBas.Add(responseModel);
            }
            return employeeBas;
        }
        public async Task<IdentityDto> GetIdentityId(string UId)
        {
            var response = await _cismosService.GetIdentityId(UId);
            var responseModel = _mapper.Map<IdentityDto>(response);
            return responseModel;
        }
        public async Task<IdentityDto> UpdateIdentity(IdentityDto identityDto)
        {
            var exeemployee = await _cismosService.GetIdentityId(identityDto.UId);
            exeemployee.Active = false;
            exeemployee.Archived = true;
            await _cismosService.RePlaceasync(exeemployee);
            exeemployee.Initialize(false, Credentials.docType, "Om", "Om");
            _mapper.Map(identityDto, exeemployee);
            var response = await _cismosService.AddIdentity(exeemployee);
            return _mapper.Map<IdentityDto>(response);
        }
        public async Task<string> DeleteIdentity(string UId)
        {
            var response = await _cismosService.GetIdentityId(UId);
            response.Active = false;
            response.Archived = true;
            await _cismosService.RePlaceasync(response);
            response.Initialize(false, Credentials.docType, "Sanjay", "Sanjay");
            response.Active = false;
            //var res = await _cismosService.AddIdentity(response);
            return "Deleted Successfully";
        }
    }
}
