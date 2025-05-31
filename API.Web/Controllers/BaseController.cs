using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace API.Web.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected Guid? CurrentUserId =>
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId)
            ? userId
            : null;
}