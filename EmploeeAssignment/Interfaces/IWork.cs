using EmploeeAssignment.Dto;

namespace EmploeeAssignment.Interfaces
{
    public interface IWork
    {
        Task<WorkInfoDto> AddWorkInfo(WorkInfoDto workInfoDto);
        Task<List<WorkInfoDto>> GetAllWorkInfo();
        Task<WorkInfoDto> GetWorkInfoByUId(string UId);
        Task<WorkInfoDto> UpdateWorkInfo(WorkInfoDto workInfoDto);
        Task<string> DeleteWorkInfo(string UId);
    }
}
