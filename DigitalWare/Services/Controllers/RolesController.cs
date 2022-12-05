using Business;
using Data;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Roles> Get()
        {
            ContextDB context = new ContextDB();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            RolesBusiness roles = new RolesBusiness(unitOfWork);
            return roles.Obtener().Take(10);
        }
    }
}
