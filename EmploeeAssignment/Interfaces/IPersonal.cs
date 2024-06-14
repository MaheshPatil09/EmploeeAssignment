using EmploeeAssignment.Dto;

namespace EmploeeAssignment.Interfaces
{
    public interface IPersonal
    {
        Task<PersonalDto> AddPersonal(PersonalDto personalDto);
        Task<List<PersonalDto>> GetAllPersonal();
        Task<PersonalDto> GetPersonalId(string UId);
        Task<PersonalDto> UpdatePersonal(PersonalDto personalDto);
        Task<string> DeletePersonal(string UId);
    }
}
