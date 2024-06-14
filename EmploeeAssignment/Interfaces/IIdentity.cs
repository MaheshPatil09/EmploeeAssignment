using EmploeeAssignment.Dto;

namespace EmploeeAssignment.Interfaces
{
    public interface IIdentity
    {
        Task<IdentityDto> AddIdentity(IdentityDto identityDto);
        Task<List<IdentityDto>> GetAllIdentity();
        Task<IdentityDto> GetIdentityId(string UId);
        Task<IdentityDto> UpdateIdentity(IdentityDto identityDto);
        Task<string> DeleteIdentity(string UId);
    }
}
