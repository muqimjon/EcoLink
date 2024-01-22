global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using EcoLink.WebApi.Controllers.Commons;

namespace EcoLink.WebApi.Controllers.Commons;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
}
