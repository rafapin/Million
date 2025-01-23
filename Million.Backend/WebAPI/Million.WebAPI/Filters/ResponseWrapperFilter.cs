using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Million.Domain.Features.Shared.Wrapper;
using System.Threading.Tasks;

public class ResponseWrapperFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult objectResult)
        {
            // Envolver la respuesta en ResponseWrapper
            var wrappedResponse = new ResponseWrapper<object>(objectResult.Value, true, "Success");
            objectResult.Value = wrappedResponse;

            // Ajustar el tipo de contenido para Swagger
            objectResult.DeclaredType = typeof(ResponseWrapper<object>);
        }
        else if (context.Result is EmptyResult)
        {
            // Manejar respuestas vacías
            context.Result = new ObjectResult(new ResponseWrapper<object>(null, true, "No content"))
            {
                StatusCode = 204
            };
        }

        await next();
    }
}
