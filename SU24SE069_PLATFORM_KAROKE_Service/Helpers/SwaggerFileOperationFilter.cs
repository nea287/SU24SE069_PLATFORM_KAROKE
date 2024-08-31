using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Helpers
{
    public class SwaggerFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Lọc các tham số kiểu IFormFile
            var fileParameters = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile));

            foreach (var parameter in fileParameters)
            {
                // Xóa tham số cũ khỏi Swagger
                operation.Parameters.Remove(operation.Parameters.First(p => p.Name == parameter.Name));

                // Thêm định dạng form-data cho file upload
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content =
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties =
                            {
                                [parameter.Name] = new OpenApiSchema
                                {
                                    Type = "string",
                                    Format = "binary"
                                }
                            }
                        }
                    }
                }
                };
            }
        }
    }


}
