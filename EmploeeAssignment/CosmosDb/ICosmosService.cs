using EmploeeAssignment.Dto;
using EmploeeAssignment.Entities;

namespace EmploeeAssignment.CosmosDb
{
    public interface ICosmosService
    {
        Task<EmployeeBasEntity> AddEmployee(EmployeeBasEntity entity);
        Task<List<EmployeeBasEntity>> GetAllEmployee();
        Task<EmployeeBasEntity> GetEmployeeId(string UId);
        //Task<EmployeeBasEntity> UpdateEmployee(EmployeeBasEntity entity);
        //Task<EmployeeBasEntity> DeleteEmployee(EmployeeBasEntity entity);
        Task RePlaceasync(dynamic entity);

        Task<WorkEntity> AddWorkInfo(WorkEntity workEntity);
        Task<List<WorkEntity>> GetAllWorkInfo();
        Task<WorkEntity> GetWorkInfoByUId(string UId);
        //Task<string> DeleteWorkInfo(string UId);

        Task<PersonalEntity> AddPersonal(PersonalEntity personalEntity);
        Task<List<PersonalEntity>> GetAllPersonal();
        Task<PersonalEntity> GetPersonalId(string UId);

        Task<IdentityEntity> AddIdentity(IdentityEntity identityEntity);

        Task<List<IdentityEntity>> GetAllIdentity();
        Task<IdentityEntity> GetIdentityId(string UId);
    }
}
