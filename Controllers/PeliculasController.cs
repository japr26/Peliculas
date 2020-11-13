using PagedList;
using Peliculas.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            ViewBag.paged = ctx.Peliculas.OrderByDescending(x => x.idPelicula).ToList().ToPagedList(page ?? 1, 5);

            return View(peliculas);
        }

        public ActionResult Editar(int idPelicula)
        {
            pelicula = ctx.Peliculas.Where(x => x.idPelicula == idPelicula).SingleOrDefault();

            ViewBag.Categorias = ctx.Categoria.ToList();

            return View(pelicula);
        }

        //public JsonResult Editar2(int idPelicula)
        //{

        //    pelicula = ctx.Peliculas.Where(x => x.idPelicula == idPelicula).SingleOrDefault();

        //    return Json(pelicula, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Guardar(Peliculas.Models.Peliculas model, HttpPostedFileBase rutaImagen)
        {
            ViewBag.Categorias = ctx.Categoria.ToList();

            if (model.rutaImagen == null && rutaImagen == null){
                ModelState.AddModelError("rutaImagen", "Debe cargar la imagen");
                return View("Editar", model);
            }

            if (ModelState.IsValid)
            {
                var anos = ctx.RestriccionPorAno.Where(x => x.idCategoria == model.idCategoria).ToList();

                var result = (from a in anos where a.ano == model.ano select a.ano).Count();

                string path;
                string fileName;
                string fullPath;
                var ano = model.ano;

                if (ano < 1920)
                {
                    ModelState.AddModelError("ano","Debe agregar un año mayor a 1920");

                    if (model.rutaImagen == "System.Web.HttpPostedFileWrapper")
                    {
                        ModelState.AddModelError("rutaImagen", "Debe cargar la imagen");

                        return View("Editar", model);
                    }

                    return View("Editar",model);
                }
                else if(ano > DateTime.Today.Year)
                {
                    ModelState.AddModelError("ano", "El año no puede ser mayor que el año actual");

                    if (model.rutaImagen == "System.Web.HttpPostedFileWrapper")
                    {
                        ModelState.AddModelError("rutaImagen", "Debe cargar la imagen");

                        return View("Editar", model);
                    }

                    return View("Editar",model);
                }
                else if(result > 0)
                {
                    ModelState.AddModelError("ano", "En este año no es posible registrar la categoria seleccionada");

                    if (model.rutaImagen == "System.Web.HttpPostedFileWrapper")
                    {
                        ModelState.AddModelError("rutaImagen", "Debe cargar la imagen");

                        return View("Editar", model);
                    }

                    return View("Editar", model);
                }
                else
                {

                    if (rutaImagen != null)
                    {
                        path = Server.MapPath("~/imagenes");
                        fileName = Path.GetFileName(rutaImagen.FileName);

                        fullPath = Path.Combine(path, fileName);
                        rutaImagen.SaveAs(fullPath);
                        if (fileName != null)
                        {
                            model.rutaImagen = fileName;
                        }

                    }
                    
                    if (model.rutaImagen == "System.Web.HttpPostedFileWrapper")
                    {
                        ModelState.AddModelError("rutaImagen", "Debe cargar la imagen");

                        return View("Editar", model);
                    }
                    else if (model.idPelicula > 0)
                    {
                        ctx.Entry(model).State = EntityState.Modified;
                        ctx.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ctx.Entry(model).State = EntityState.Added;
                        ctx.SaveChanges();

                        return RedirectToAction("Index");
                    }
                }

            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Eliminar(int idPelicula, Peliculas.Models.Peliculas model)
        {
            //Peliculas.Models.Peliculas model;

            pelicula.idPelicula = idPelicula;
            ctx.Entry(model).State = EntityState.Deleted;
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}