global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using OrgBloom.WebApi.Controllers.Commons;

namespace OrgBloom.WebApi.Controllers.Commons;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
}
