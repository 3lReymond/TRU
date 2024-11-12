using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Registro_Escuela.Datos;
using Registro_Escuela.Models;
using Registro_Escuela.Models.Datos;

namespace Registro_Escuela.Controllers
{
	public class MantenedorController : Controller
	{

		//CODIGO DE REGISTROS
		RegistroDatos _RegistroDatos = new RegistroDatos();
		public IActionResult Listar()
		{
			var Select = _RegistroDatos.Listar();
			return View(Select);
		}
		public IActionResult Guardar()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Guardar(Registro nRegistro)
		{
			if (!ModelState.IsValid)
				return View();


			var respuesta = _RegistroDatos.Guardar(nRegistro);
			if (respuesta)
				return RedirectToAction("Listar");

			else return View();
		}
		public IActionResult Editar(int ID_Usuario)
		{
			var nRegistro = _RegistroDatos.Obtener(ID_Usuario);
			return View(nRegistro);
		}
		[HttpPost]
		public IActionResult Editar(Registro nRegistro)
		{
			if (!ModelState.IsValid)
				return View();


			var respuesta = _RegistroDatos.Editar(nRegistro);
			if (respuesta)
				return RedirectToAction("Listar");

			else return View();
		}
		public IActionResult Eliminar(int ID_Usuario)
		{
			var nRegistro = _RegistroDatos.Obtener(ID_Usuario);
			return View(nRegistro);
		}
		[HttpPost]
		public IActionResult Eliminar(Registro nRegistro)
		{
			var respuesta = _RegistroDatos.Eliminar(nRegistro.ID_Usuario);
			if (respuesta)
				return RedirectToAction("Listar");

			else return View();
		}

		//CODIGO DE ACTIVIDADES

		ActividadesDatos _ActividadesDatos = new ActividadesDatos();

		public IActionResult VerActividades()
		{
			var SelectAct = _ActividadesDatos.VerActividades();
			return View(SelectAct);
		}

		public IActionResult GuardarAct()
		{


			return View();
		}
		[HttpPost]
		public IActionResult GuardarAct(Actividades nActividad)
		{
			var respuesta1 = _ActividadesDatos.GuardarAct(nActividad);
			if (respuesta1)
				return RedirectToAction("VerActividades");

			else return View();

		}
		public IActionResult EditarAct(int ID_Actividad)
		{
			var nActividad = _ActividadesDatos.ObtenerAct(ID_Actividad);
			return View(nActividad);
		}
		[HttpPost]
		public IActionResult EditarAct(Actividades nActividad)
		{
			if (!ModelState.IsValid)
				return View();


			var respuesta = _ActividadesDatos.EditarAct(nActividad);
			if (respuesta)
				return RedirectToAction("VerActividades");

			else return View();
		}
		public IActionResult EliminarAct(int ID_Actividad)
		{
			var nActividad = _ActividadesDatos.ObtenerAct(ID_Actividad);
			return View(nActividad);
		}
		[HttpPost]
		public IActionResult EliminarAct(Actividades nActividad)
		{
			var respuesta = _ActividadesDatos.EliminarAct(nActividad.ID_Actividad);
			if (respuesta)
				return RedirectToAction("VerActividades");

			else return View();
		}

		//CODIGO DE Materiales

		MaterialesDatos _MaterialesDatos = new MaterialesDatos();

		public IActionResult VerMateriales()
		{		
			var SelectAct = _MaterialesDatos.VerMateriales();
			return View(SelectAct);
		}

		public IActionResult GuardarMat()
		{


			return View();
		}
		[HttpPost]
		public IActionResult GuardarMat(Materiales nMateriales)
		{
			var respuesta1 = _MaterialesDatos.GuardarMat(nMateriales);
			if (respuesta1)
				return RedirectToAction("VerMateriales");

			else return View();

		}
		public IActionResult EditarMat(int ID_Materiales)
		{
			var nMateriales = _MaterialesDatos.ObtenerMat(ID_Materiales);
			return View(nMateriales);
		}
		[HttpPost]
		public IActionResult EditarMat(Materiales nMateriales)
		{
			if (!ModelState.IsValid)
				return View();


			var respuesta = _MaterialesDatos.EditarMat(nMateriales);
			if (respuesta)
				return RedirectToAction("VerMateriales");

			else return View();
		}
		public IActionResult EliminarMat(int ID_Materiales)
		{
			var nMateriales = _MaterialesDatos.ObtenerMat(ID_Materiales);
			return View(nMateriales);
		}
		[HttpPost]
		public IActionResult EliminarMat(Materiales nMateriales)
		{
			var respuesta = _MaterialesDatos.EliminarMat(nMateriales.ID_Materiales);
			if (respuesta)
				return RedirectToAction("VerMateriales");

			else return View();
		}

	}
}
