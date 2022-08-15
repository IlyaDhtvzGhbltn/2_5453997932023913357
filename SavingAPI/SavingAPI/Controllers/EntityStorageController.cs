using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SavingAPI.v1.Filters;
using SavingAPI.v1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;

namespace SavingAPI.v1.Controllers
{
    [Route("api/v1/storage/entity")]
    [ApiController]
    [AllowAnonymous]
    public class EntityStorageController : ControllerBase
    {

        private readonly IStorageService _storageService;

        public EntityStorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }


        [HttpPost]
        [InsertStringFilter]
        public async Task<CreateEntityResponse> CreateEntity([FromQuery]string insert) 
        {
            var entity = JsonConvert.DeserializeObject<Entity>(insert);
            await _storageService.CreateNew(entity);

            return new CreateEntityResponse(true, "Entity successfully created");
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<GetEntityResponse> GetEntity([FromRoute] Guid id) 
        {
            Entity entity = await _storageService.GetEntity(id);
            return new GetEntityResponse(true, "Entity exists in storage", entity);
        }

    }
}
