using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SavingAPI.v1.Services
{
    public class MoqStorageService : IStorageService
    {

        private Dictionary<Guid, Entity> _storage;

        public MoqStorageService()
        {
            _storage = new Dictionary<Guid, Entity>();
        }

        public async Task CreateNew(Entity entity)
        {
            await Task.Run(()=> { 
                if (_storage.ContainsKey(entity.Id))
                {
                    throw new Exception("Duplicate key exception");
                }
                else 
                {
                    _storage[entity.Id] = entity;
                }
            });
        }

        public async Task<Entity> GetEntity(Guid id)
        {
            return await Task.Run(() => 
            {
                if (!_storage.ContainsKey(id))
                {
                    throw new KeyNotFoundException($"Key {id} wasn't found in storage");
                }
                else 
                {
                    return _storage[id];
                }
            });
        }
    }
}
