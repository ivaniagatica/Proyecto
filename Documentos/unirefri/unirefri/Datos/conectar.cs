namespace unirefri.Datos
{
    public class conectar
    {
        private string CadenaSQL = string.Empty;

        public conectar()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();

            CadenaSQL = builder.GetSection("ConnectionStrings:CadenaSQL").Value;

        }

        public string getCadenaSQL()

        {
            return CadenaSQL;
        }
    }
}
