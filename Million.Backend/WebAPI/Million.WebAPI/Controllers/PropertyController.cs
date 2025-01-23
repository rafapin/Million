using MediatR;
using Microsoft.AspNetCore.Mvc;
using Million.Application.Features.Properties.Commands.CreateProperty;
using Million.Application.Features.Properties.Queries.GetProperties;
using Million.Application.Features.Properties.Queries.GetPropertyById;
using Million.Application.Features.Properties.ViewModels;
using Million.WebAPI.Requests;

namespace Million.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropertyController(IMediator mediator) : ControllerBase
    {      
        [HttpPost]
        public async Task<bool> Create([FromForm]CreatePropertyRequest request)
            => await mediator.Send(await request.ToCommand());

        [HttpGet]
        public async Task<List<PropertyVm>> GetAll([FromQuery]GetPropertiesQuery request)
            => await mediator.Send(request);

        [HttpGet("{id}")]
        public async Task<PropertyDetailVm> GetById(string id)
            => await mediator.Send(new GetPropertyByIdQuery { Id = id });
    }
}
