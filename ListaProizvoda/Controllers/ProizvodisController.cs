using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ListaProizvoda.Models;

namespace ListaProizvoda.Controllers
{
    public class ProizvodisController : Controller
    {
        private ListaProizvodaContext db = new ListaProizvodaContext();

        // GET: Proizvodis
        public ActionResult Index()
        {
            return View(db.Proizvodis.ToList());
        }


        // Create Json file
        [HttpPost]
        public ActionResult Index(Proizvodi obj)
        {
            var proizvodis = db.Proizvodis.ToList();
 
            string jsondata = new JavaScriptSerializer().Serialize(proizvodis);
            string path = Server.MapPath("~/App_Data/");

            System.IO.File.WriteAllText(path + "output.json", jsondata);
            TempData["msg"] = "Json file Generated! check this in your App_Data folder";
            return RedirectToAction("Index");
        }
        // GET: Proizvodis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvodi proizvodi = db.Proizvodis.Find(id);
            if (proizvodi == null)
            {
                return HttpNotFound();
            }
            return View(proizvodi);
        }

        // GET: Proizvodis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proizvodis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,Opis,Proizvodjac,Dobavljac,Cena")] Proizvodi proizvodi)
        {
            if (ModelState.IsValid)
            {
                db.Proizvodis.Add(proizvodi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proizvodi);
        }

        // GET: Proizvodis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvodi proizvodi = db.Proizvodis.Find(id);
            if (proizvodi == null)
            {
                return HttpNotFound();
            }
            return View(proizvodi);
        }

        // POST: Proizvodis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv,Opis,Proizvodjac,Dobavljac,Cena")] Proizvodi proizvodi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proizvodi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proizvodi);
        }

        // GET: Proizvodis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvodi proizvodi = db.Proizvodis.Find(id);
            if (proizvodi == null)
            {
                return HttpNotFound();
            }
            return View(proizvodi);
        }

        // POST: Proizvodis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proizvodi proizvodi = db.Proizvodis.Find(id);
            db.Proizvodis.Remove(proizvodi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
