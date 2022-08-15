using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SavingAPI.v1.Services
{
    public interface IStorageService
    {
        Task CreateNew(Entity entity);
        Task<Entity> GetEntity(Guid id);
    }
}
