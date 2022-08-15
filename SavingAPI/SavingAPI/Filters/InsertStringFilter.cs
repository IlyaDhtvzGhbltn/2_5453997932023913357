using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using DTO;
using System.Text.Json;

namespace SavingAPI.v1.Filters
{
    public class InsertStringFilter : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            string value = context.HttpContext.Request.Query["insert"];
            JObject entry;

            if (string.IsNullOrWhiteSpace(value)) 
            {
                throw new Exception("Empty value");
            }
            try
            {
                entry = JObject.Parse(value);
            }
            catch (Exception) 
            {
                throw new Exception("Invalid Json");
            }

            IEnumerable<JProperty> properties = entry.Properties().ToArray();

            if (properties.Count() < 3)
            {
                List<string> errors = new List<string>();

                if (!properties.Any(x => x.Name.ToLower() == "id")) 
                {
                    errors.Add("Id parameter missing in json;");
                }
                if (!properties.Any(x => x.Name.ToLower() == "operationdate")) 
                {
                    errors.Add("OperationDate parameter missing in json;");
                }
                if (!properties.Any(x => x.Name.ToLower() == "amount"))
                {
                    errors.Add("Amount parameter missing in json;");
                }

                throw new Exception(string.Concat(errors));
            }

            await next();
        }
    }
}
