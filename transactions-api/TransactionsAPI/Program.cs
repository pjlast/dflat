using Transactions.APIV1;
using Transactions.Store;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SupportNonNullableReferenceTypes();
    options.UseAllOfToExtendReferenceSchemas();
    options.SchemaFilter<Schema.RequireNonNullablePropertiesSchemaFilter>();
});

builder.Services.AddSingleton<IStore>(new InMemoryStore());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGroup("/api/v1/transactions").MapTransactionsAPI().WithOpenApi();

app.Run();

public partial class Program { }

namespace Schema
{
    public class RequireNonNullablePropertiesSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            ArgumentNullException.ThrowIfNull(schema);
            if (schema.Properties is null)
                return;

            var nonNullableProperties = schema
                .Properties.Where(x => !x.Value.Nullable)
                .Select(x => x.Key);

            // If property isn't explicitly declared as nullable, it is assumed to be required.
            foreach (var property in nonNullableProperties)
            {
                schema.Required.Add(property);
            }
        }
    }
}
