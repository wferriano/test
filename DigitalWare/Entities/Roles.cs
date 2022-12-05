namespace Entities
{
    using System.ComponentModel.DataAnnotations;
    public class Roles
    {
        [Key]
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
    }
}
