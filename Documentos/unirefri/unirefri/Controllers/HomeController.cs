using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using unirefri.Models;
using System.Data;
using System.Data.SqlClient;
using unirefri.Datos;
using ClosedXML.Excel;

namespace unirefri.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //---------------------Exportar--------------------------------------


        public IActionResult Exportar_Excel(string fechaInicio, string fechaFin)
        {
            DataTable tabla_precio = new DataTable();


            using (var conectar = new SqlConnection())
            {
                conectar.Open();
                using (var adapter = new SqlDataAdapter())
                {

                    adapter.SelectCommand = new SqlCommand("sp_reporte_cliente", conectar);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    adapter.SelectCommand.Parameters.AddWithValue("@FechaFin", fechaFin);



                    adapter.Fill(tabla_precio);
                }
            }



            using (var libro = new XLWorkbook())
            {

                tabla_precio.TableName = "PrecioDetalle";
                var hoja = libro.Worksheets.Add(tabla_precio);
                hoja.ColumnsUsed().AdjustToContents();

                using (var memoria = new MemoryStream())
                {

                    libro.SaveAs(memoria);

                    var nombreExcel = string.Concat("Reporte ", DateTime.Now.ToString(), ".xlsx");

                    return File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                }
            }



        }


        //--------------------------EMPLEADOS---------------------------------------

        EmpleadoDatos _EmpleadoDatos = new EmpleadoDatos();

        public IActionResult GuardarEmpleados()
        {
            //metodo devuelve a la vista

            return View();
        }


        [HttpPost]
        public IActionResult GuardarEmpleados(Empleado oempleado)
        {
            //metodo recibe el objeto en la BD

            if (!ModelState.IsValid)
                return View();
            var respuesta = _EmpleadoDatos.guardarEmpleado(oempleado);

            if (respuesta)
                return RedirectToAction("Empleado");
            else
                return View();

        }

        public IActionResult EditarEmpleados(int IdEmpleado)
        {
            //metodo devuelve a la vista
            var oempleado = _EmpleadoDatos.ObtenerEmpleado(IdEmpleado);
            return View(oempleado);
        }



        [HttpPost]
        public IActionResult EditarEmpleados(Empleado oempleado)
        {
            //metodo recibe el objeto en la BD

            if (!ModelState.IsValid)
                return View();


            var respuesta = _EmpleadoDatos.editarEmpleado(oempleado);

            if (respuesta)
                return RedirectToAction("Empleado");
            else
                return View();
        }

        public IActionResult EliminarEmpleados(int IdEmpleado)
        {
            //metodo devuelve a la vista
            var oempleado = _EmpleadoDatos.ObtenerEmpleado(IdEmpleado);
            return View(oempleado);
        }


        [HttpPost]
        public IActionResult EliminarEmpleados(Empleado oempleado)
        {
            //metodo recibe el objeto en la BD



            var respuesta = _EmpleadoDatos.eliminarEmpleado(oempleado.IdEmpleado);

            if (respuesta)
                return RedirectToAction("Empleado");
            else
                return View();
        }
        //------------------------  LISTA DE DATOS ----------------------------------------

        ListaDatos _ListaDatos = new ListaDatos();
        public IActionResult GuardarLista()
        {
            //metodo devuelve a la vista

            return View();
        }

        [HttpPost]
        public IActionResult GuardarLista(Lista olista)
        {
            //metodo recibe el objeto en la BD

            if (!ModelState.IsValid)
                return View();


            var respuesta = _ListaDatos.guardarLista(olista);

            if (respuesta)
                return RedirectToAction("Lista");
            else
                return View();

        }
        public IActionResult Editar(int IdLista)
        {
            //metodo devuelve a la vista
            var olista = _ListaDatos.ObtenerLista(IdLista);
            return View(olista);
        }

        [HttpPost]
        public IActionResult Editar(Lista olista)
        {
            //metodo recibe el objeto en la BD

            if (!ModelState.IsValid)
                return View();


            var respuesta = _ListaDatos.editarLista(olista);

            if (respuesta)
                return RedirectToAction("Lista");
            else
                return View();
        }


        public IActionResult Eliminar(int IdLista)
        {
            //metodo devuelve a la vista
            var olista = _ListaDatos.ObtenerLista(IdLista);
            return View(olista);
        }

        [HttpPost]
        public IActionResult Eliminar(Lista olista)
        {
            //metodo recibe el objeto en la BD

            var respuesta = _ListaDatos.eliminarLista(olista.IdLista);

            if (respuesta)
                return RedirectToAction("Lista");
            else
                return View();
        }

        //----------------------------Precio Detalle------------------------------------------------------

        PrecioDetalleDatos _PrecioDetalleDatos = new PrecioDetalleDatos();
        public IActionResult GuardarPrecioDetalle()
        {
            //metodo devuelve a la vista

            return View();
        }

        [HttpPost]
        public IActionResult GuardarPrecioDetalle(PrecioDetalle opreciod)
        {
            //metodo recibe el objeto en la BD

            if (!ModelState.IsValid)
                return View();


            var respuesta = _PrecioDetalleDatos.guardarPrecioDetalle(opreciod);

            if (respuesta)
                return RedirectToAction("PrecioDetalle");
            else
                return View();

        }
        public IActionResult EditarPrecioDetalle(int IdprecioDetalle)
        {
            //metodo devuelve a la vista
            var opreciod = _PrecioDetalleDatos.obtenerPrecioDetalle(IdprecioDetalle);
            return View(opreciod);
        }

        [HttpPost]
        public IActionResult EditarPrecioDetalle(PrecioDetalle opreciod)
        {
            //metodo recibe el objeto en la BD

            if (!ModelState.IsValid)
                return View();


            var respuesta = _PrecioDetalleDatos.editarPrecioDetalle(opreciod);

            if (respuesta)
                return RedirectToAction("PrecioDetalle");
            else
                return View();
        }
        public IActionResult EliminarPrecioDetalle(int IdprecioDetalle)
        {
            //metodo devuelve a la vista
            var opreciod = _PrecioDetalleDatos.obtenerPrecioDetalle(IdprecioDetalle);
            return View(opreciod);
        }

        [HttpPost]
        public IActionResult Eliminarinventario(PrecioDetalle opreciod)
        {
            //metodo recibe el objeto en la BD

            var resultado = _PrecioDetalleDatos.eliminarPrecioDetalle(opreciod.IdPrecioDetalle);

            if (resultado)
                return RedirectToAction("PrecioDetallle");
            else
                return View();
        }


        //----------------------------------------Producto Encabezado----------------------------------------------------------------------

        PrecioEncabezadoDatos _PrecioEncabezadoDatos = new PrecioEncabezadoDatos();

        public IActionResult GuardarPrecioEncabezado()
        {
            //metodo devuelve a la vista

            return View();
        }

        [HttpPost]
        public IActionResult GuardarPrecioEncabeazado(PrecioEncabezado oprecioenca)
        {
            //metodo recibe el objeto en la BD

            if (!ModelState.IsValid)
                return View();


            var resultado = _PrecioEncabezadoDatos.guardarPrecioEncabezado(oprecioenca);

            if (resultado)
                return RedirectToAction("PrecioEncabezado");
            else
                return View();
        }

        public IActionResult Editarinventario(int IdCorrelativo)
        { //metodo devuelve a la vista
            var oprecioenca = _PrecioEncabezadoDatos.obtenerPrecioEncabezado(IdCorrelativo);

            return View(oprecioenca);
        }

        [HttpPost]
        public IActionResult Editarinventario(PrecioEncabezado oprecioenca)
        {
            //metodo recibe el objeto en la BD
            if (!ModelState.IsValid)
                return View();

            var resultado = _PrecioEncabezadoDatos.editarPrecioEncabezado(oprecioenca);
            if (resultado)
                return RedirectToAction("PrecioEncabezado");
            else
                return View();
        }

        public IActionResult Eliminarinventario(int IdCorrelativo)
        {
            //metodo devuelve a la vista
            var oinve = _PrecioEncabezadoDatos.obtenerPrecioEncabezado(IdCorrelativo);
            return View(oinve);
        }

        [HttpPost]
        public IActionResult Eliminarinventario(PrecioEncabezado oprecioenca)
        {
            //metodo recibe el objeto en la BD

            var resultado = _PrecioEncabezadoDatos.eliminarPrecioEncabezado(oprecioenca.IdCorrelativo);

            if (resultado)
                return RedirectToAction("PrecioEncabezado");
            else
                return View();
        }


        ////---------------------------------------------------Producto------------------------------------------------------------------------------
        ProductoDatos _ProductoDatos = new ProductoDatos();

        public IActionResult GuardarProducto()
        {
            //metodo devuelve a la vista

            return View();
        }

        [HttpPost]
        public IActionResult GuardarProducto(Producto oproduto)
        {
            //metodo recibe el objeto en la BD

            if (!ModelState.IsValid)
                return View();


            var resultado = _ProductoDatos.guardarProducto(oproduto);

            if (resultado)
                return RedirectToAction("Producto");
            else
                return View();
        }

        public IActionResult EditarProducto(int IdProducto)
        {   //metodo devuelve a la vista
            var oproduto = _ProductoDatos.ObtenerProducto(IdProducto);

            return View(oproduto);
        }

        [HttpPost]
        public IActionResult EditarProducto(Producto oproduto)
        {
            //metodo recibe el objeto en la BD
            if (!ModelState.IsValid)
                return View();

            var resultado = _ProductoDatos.editarProducto(oproduto);
            if (resultado)
                return RedirectToAction("Producto");
            else
                return View();
        }

        public IActionResult EliminarProducto(int IdProducto)
        {
            //metodo devuelve a la vista
            var oproduto = _ProductoDatos.ObtenerProducto(IdProducto);
            return View(oproduto);
        }

        [HttpPost]
        public IActionResult EliminarProducto(Producto oproduto)
        {
            //metodo recibe el objeto en la BD

            var resultado = _ProductoDatos.eliminarProducto(oproduto.IdProducto);

            if (resultado)
                return RedirectToAction("Producto");
            else
                return View();
        }


        //------------------------------------------------------------------------------------------------------------------------------
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}