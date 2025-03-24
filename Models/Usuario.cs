namespace TiendaCiclismo.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Email { get; set; }
        public Rol? Rol { get; set; }
    }
}
