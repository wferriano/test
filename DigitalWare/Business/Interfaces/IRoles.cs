namespace Business
{
    using Entities;
    using System.Collections.Generic;

    public interface IRoles
    {
        IEnumerable<Roles> Obtener();
    }
}
