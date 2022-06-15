using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static E_SportManager.Common.GlobalConstants.Administrator;

namespace E_SportManager.Areas.Admin.Controllers
{
    [Area(AdministratorUsername)]
    [Authorize(Roles =AdministratorRoleName)]
    public abstract class AdminController : Controller
    {
    }
}
