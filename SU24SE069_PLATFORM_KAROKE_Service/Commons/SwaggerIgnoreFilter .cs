using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace SU24SE069_PLATFORM_KAROKE_Service.Commons
{
    public class SwaggerIgnoreFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null || !schema.Properties.Any())
                return;

            var ignoredProperties = context.Type.GetProperties()
                .Where(prop => prop.GetCustomAttribute<SwaggerIgnoreAttribute>() != null);

            foreach (var ignoredProperty in ignoredProperties)
            {
                var propertyName = char.ToLowerInvariant(ignoredProperty.Name[0]) + ignoredProperty.Name.Substring(1);
                if (schema.Properties.ContainsKey(propertyName))
                {
                    schema.Properties.Remove(propertyName);
                }
            }
        }
    }
}
