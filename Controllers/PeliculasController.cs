using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;
using Peliculas.Models;
using PagedList;
using PagedList.Mvc;

namespace Peliculas.Controllers
{
    public class PeliculasController : Controller
    {
        // GET: Peliculas

        private PeliculasContext ctx = new PeliculasContext();
        private Peliculas.Models.Peliculas pelicula = new Peliculas.Models.Peliculas();

        public ActionResult Index(int? page)
        {
           Peliculas.Models.Peliculas peliculas = new Peliculas.Models.Peliculas();

           // ViewBag.Peliculas = ctx.Peliculas.ToList();
            ViewBag.Categorias = ctx.Categoria.ToList();
            ViewBag.paged = ctx.Peliculas.ToList().ToPagedList(page ?? 1, 5);

            return View(peliculas);
        }

        public ActionResult Editar(int idPelicula)
        {
            ViewBag.Peliculas = ctx.Peliculas.ToList();
            ViewBag.Categorias = ctx.Categoria.ToList();

            pelicula = ctx.Peliculas.Where(x => x.idPelicula == idPelicula).SingleOrDefault();

            return View(pelicula);
        }

        //public JsonResult Editar2(int idPelicula)
        //{

        //    pelicula = ctx.Peliculas.Where(x => x.idPelicula == idPelicula).SingleOrDefault();

        //    return Json(pelicula, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Guardar(Peliculas.Models.Peliculas model, HttpPostedFileBase rutaImagen)
        {

            if (ModelState.IsValid)
            {
                string path = Server.MapPath("~/imagenes");
                string fileName = Path.GetFileName(rutaImagen.FileName);

                string fullPath = Path.Combine(path, fileName);
                rutaImagen.SaveAs(fullPath);
                if (fileName != null)
                {
                    model.rutaImagen = fileName;
                }

                if (model.idPelicula > 0)
                {
                    ctx.Entry(model).State = EntityState.Modified;
                    ctx.SaveChanges();

                    return Redirect("~/Peliculas/Index");
                }
                else
                {
                    ctx.Entry(model).State = EntityState.Added;
                    ctx.SaveChanges();

                    return Redirect("~/Peliculas/Index");
                }

            }
            else
            {
                return Redirect("~/Peliculas/Index");
            }
        }

        public ActionResult Eliminar(int idPelicula, Peliculas.Models.Peliculas model)
        {
            //Peliculas.Models.Peliculas model;

            pelicula.idPelicula = idPelicula;
            ctx.Entry(model).State = EntityState.Deleted;
            ctx.SaveChanges();

            return Redirect("~/Peliculas/Index");
        }
    }
}