﻿using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using KongOrange.Squares.Business;
using KongOrange.Squares.DataAccess;
using KongOrange.Squares.DomainClasses;
using KongOrange.Squares.WebInterface.Models;
using Microsoft.AspNet.Identity;

namespace KongOrange.Squares.WebInterface.Controllers
{
    [Authorize]
    public class SquareSetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SquareSets
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var squareSets = db.SquareSets.Where(o => o.UserId == userId).Include(o => o.Pieces);
            return View(await squareSets.ToListAsync());
        }

        // GET: SquareSets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SquareSet squareSet = await db.SquareSets.FindAsync(id);
            if (squareSet == null)
            {
                return HttpNotFound();
            }
            return View(squareSet);
        }

        // GET: SquareSets/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: SquareSets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SquareSetViewModel squareSetViewModel)
        {
            var squareSet = new SquareSet
            {
                Name = squareSetViewModel.Name,
                UserId = User.Identity.GetUserId()
            };

            if (ModelState.IsValid)
            {
                db.SquareSets.Add(squareSet);
                if (squareSetViewModel.Images != null && squareSetViewModel.Images.Any())
                {
                    var storage = new StorageFacade();
                    foreach (var image in squareSetViewModel.Images)
                    {
                        if (image.ContentLength > 0)
                        {
                            var url = storage.Store(image.FileName, image.InputStream, User.Identity.GetUserId());
                            var squareSetPiece = new SquareSetPiece
                            {
                                SquareSetId = squareSetViewModel.Id,
                                ImageUrl = url
                            };

                            db.SquareSetPieces.Add(squareSetPiece);
                        }
                    }
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = squareSet.Id });
            }

            return View(squareSet);
        }

        // GET: SquareSets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SquareSet squareSet = await db.SquareSets.FindAsync(id);
            if (squareSet == null)
            {
                return HttpNotFound();
            }

            return View(squareSet);
        }

        // POST: SquareSets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditSquareSetViewModel squareSetViewModel)
        {
            if (ModelState.IsValid)
            {
                var squareSet = db.SquareSets.First(o => o.Id == squareSetViewModel.Id);
                squareSet.Name = squareSetViewModel.Name;

                await db.SaveChangesAsync();
            }

            return RedirectToAction("Edit", new {id = squareSetViewModel.Id});
        }

        // GET: SquareSets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SquareSet squareSet = await db.SquareSets.FindAsync(id);
            if (squareSet == null)
            {
                return HttpNotFound();
            }
            return View(squareSet);
        }

        // POST: SquareSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SquareSet squareSet = await db.SquareSets.FindAsync(id);
            db.SquareSets.Remove(squareSet);
            await db.SaveChangesAsync();
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
