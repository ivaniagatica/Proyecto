namespace unirefri.Models
{
	public class PrecioEncabezado
	{
		public int IdCorrelativo { get; set; }
		public string? FechaPrecioEncabezado { get; set; }
		public string? horaPrecioEncabezado { get; set; }
		public virtual Usuario? IdUsuario { get; set; }
	}
}
