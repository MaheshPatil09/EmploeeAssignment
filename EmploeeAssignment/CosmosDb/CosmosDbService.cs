using Microsoft.Azure.Cosmos;
using EmploeeAssignment.Common;
using EmploeeAssignment.Entities;
using System.Net;
namespace EmploeeAssignment.CosmosDb
{
    public class CosmosDbService : ICosmosService
    {
        public readonly CosmosClient cosmosClient1;
        public readonly Container container;
        public CosmosDbService(CosmosClient cosmosClient)
        {
            container = cosmosClient.GetContainer(Credentials.databaseName, Credentials.containerName);

        }
        public async Task<EmployeeBasEntity> AddEmployee(EmployeeBasEntity entity)
        {
            var response = await container.CreateItemAsync(entity);
            return response;
        }
        public async Task<List<EmployeeBasEntity>> GetAllEmployee()
        {
            var response = container.GetItemLinqQueryable<EmployeeBasEntity>(true).Where(q => q.Active == true && q.Archived == false && q.DocumentType == Credentials.docType).ToList();
            return response;
        }
        public async Task<EmployeeBasEntity> GetEmployeeId(string Uid)
        {
            var response = container.GetItemLinqQueryable<EmployeeBasEntity>(true).Where(q => q.UId == Uid && q.Active == true && q.Archived == false && q.DocumentType == Credentials.docType).FirstOrDefault();
            return response;
        }

        public async Task RePlaceasync(dynamic entity)
        {
            var response = await container.ReplaceItemAsync(entity, entity.UId);
        }


        public async Task<WorkEntity> AddWorkInfo(WorkEntity entity)
        {
            var response = await container.CreateItemAsync(entity);
            return response;
        }
        public async Task<List<WorkEntity>> GetAllWorkInfo()
        {
            var response = container.GetItemLinqQueryable<WorkEntity>(true).Where(q => q.Active == true && q.Archived == false && q.DocumentType == Credentials.docType).ToList();
            return response;
        }
        public async Task<WorkEntity> GetWorkInfoByUId(string Uid)
        {
            var response = container.GetItemLinqQueryable<WorkEntity>(true).Where(q => q.UId == Uid && q.Active == true && q.Archived == false && q.DocumentType == Credentials.docType).FirstOrDefault();
            return response;
        }
        public async Task<PersonalEntity> AddPersonal(PersonalEntity personalEntity)
        {
            var response = await container.CreateItemAsync(personalEntity);
            return response;
        }
        public async Task<List<PersonalEntity>> GetAllPersonal()
        {
            var response = container.GetItemLinqQueryable<PersonalEntity>(true).Where(q => q.Active == true && q.Archived == false && q.DocumentType == Credentials.docType).ToList();
            return response;
        }
        public async Task<PersonalEntity> GetPersonalId(string Uid)
        {
            var response = container.GetItemLinqQueryable<PersonalEntity>(true).Where(q => q.UId == Uid && q.Active == true && q.Archived == false && q.DocumentType == Credentials.docType).FirstOrDefault();
            return response;
        }

        public async Task<IdentityEntity> AddIdentity(IdentityEntity identityEntity)
        {
            var response = await container.CreateItemAsync(identityEntity);
            return response;
        }
        public async Task<List<IdentityEntity>> GetAllIdentity()
        {
            var response = container.GetItemLinqQueryable<IdentityEntity>(true).Where(q => q.Active == true && q.Archived == false && q.DocumentType == Credentials.docType).ToList();
            return response;
        }
        public async Task<IdentityEntity> GetIdentityId(string Uid)
        {
            var response = container.GetItemLinqQueryable<IdentityEntity>(true).Where(q => q.UId == Uid && q.Active == true && q.Archived == false && q.DocumentType == Credentials.docType).FirstOrDefault();
            return response;
        }

    }
}

