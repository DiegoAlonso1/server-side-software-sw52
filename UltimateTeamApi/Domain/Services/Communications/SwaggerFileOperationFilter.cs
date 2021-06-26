using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using UltimateTeamApi.ExternalTools.Resources;
using Microsoft.VisualStudio.Services.Operations;

namespace UltimateTeamApi.Domain.Services.Communications
{
    public class SwaggerFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //var content = new Dictionary<string, OpenApiMediaType>().Add("file", );
            var fileParams = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType.FullName.Equals(typeof(SaveDriveFileResource).FullName));

            if (fileParams.Any() && fileParams.Count() == 1)
            {
                operation.Parameters = new List<OpenApiParameter>
                {
                    new OpenApiParameter
                    {
                        Name = fileParams.First().Name,
                        Schema = new OpenApiSchema
                        {
                            Type = "File",
                        },
                        Required = true,
                        AllowEmptyValue = false,
                        //In=(ParameterLocation)Enum.Parse(typeof(ParameterLocation),"formData"),
                        //@in="formaData"
                        
                        //Content = new Dictionary<string, OpenApiMediaType>()
                        
                        //Type = "file",
                        //In = ParameterLocation.
                    }
                };
                //operation.
                //operation.RequestBody = new OpenApiRequestBody
                //{
                //    Content = 
                //};
            }
        }
    }
}
