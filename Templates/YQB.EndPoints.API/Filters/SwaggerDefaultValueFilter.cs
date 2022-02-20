using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace $safeprojectname$.Filters
{
    public class SwaggerDefaultValueFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Properties == null)
                return;

            foreach (var property in schema.Properties)
            {
                if (property.Value.Default != null && property.Value.Example is null)
                    property.Value.Example = property.Value.Default;
            }
        }
    }
}