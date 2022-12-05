namespace Business
{
    using Entities;
    using Repository;
    using System.Collections.Generic;

    public class RolesBusiness : IRoles
    {
        private IUnitOfWork _unit;
        public RolesBusiness(IUnitOfWork unit)
        {
            this._unit = unit;
        }

        public IEnumerable<Roles> Obtener()
        {
            var lstRoles = this._unit.GenericRepository<Roles>().Get();
            return lstRoles;
        }
    }
}
