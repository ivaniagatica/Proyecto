namespace unirefri.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string? DescripcionProducto { get; set; }
        public string? FechaCreacionProducto { get; set; }
        public string? UsuarioCreacionProducto { get; set; }
        public string? FechaActualizacionProducto { get; set; }
        public string? UsuarioActualizacionProducto { get; set; }

        public PrecioDetalle? PrecioDetalle { get; set; }
    }
}
