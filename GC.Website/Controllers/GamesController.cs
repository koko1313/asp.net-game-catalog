using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GC.Data.Context;
using GC.Data.Entities;
using PagedList;

namespace GC.Website.Controllers
{
    public class GamesController : Controller
    {
        private GameCatalogDbContext db = new GameCatalogDbContext();

        // GET: Games
        public ActionResult Index(int? page, string titleSearch, string sortOrder)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            IQueryable<Game> games = db.Games.AsQueryable();

            ViewBag.TitleSearch = titleSearch;
            if (!String.IsNullOrEmpty(titleSearch))
            {
                games = games.Where(x => x.Name.Contains(titleSearch));
            }

            ViewBag.CurrentSortParm = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.GenreSortParm = sortOrder == "genre_asc" ? "genre_desc" : "genre_asc";
            ViewBag.RatingSortParm = sortOrder == "rating_asc" ? "rating_desc" : "rating_asc";
            switch (sortOrder)
            {
                case "name_desc":
                    games = games.OrderByDescending(x => x.Name);
                    break;
                case "genre_asc":
                    games = games.OrderBy(x => x.Genre.GenreName);
                    break;
                case "genre_desc":
                    games = games.OrderByDescending(x => x.Genre.GenreName);
                    break;
                case "rating_asc":
                    games = games.OrderBy(x => x.Rating.RatingValue);
                    break;
                case "rating_desc":
                    games = games.OrderByDescending(x => x.Rating.RatingValue);
                    break;
                default:
                    games = games.OrderBy(x => x.Name);
                    break;
            }

            return View(games.ToPagedList(pageNumber, pageSize));
        }

        // GET: Movies/Details/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Movies/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Ratings = new SelectList(db.Ratings, "RatingId", "RatingValue");
            ViewBag.Genres = new SelectList(db.Genres, "GenreId", "GenreName");

            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "GameId,Name,ReleaseYear,GenreId,RatingId")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ratings = new SelectList(db.Ratings, "RatingId", "RatingValue");
            ViewBag.Genres = new SelectList(db.Genres, "GenreId", "GenreName");

            return View(game);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            ViewBag.Ratings = new SelectList(db.Ratings, "RatingId", "RatingValue");
            ViewBag.Genres = new SelectList(db.Genres, "GenreId", "GenreName");

            return View(game);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "GameId,Name,ReleaseYear,GenreId,RatingId")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ratings = new SelectList(db.Ratings, "RatingId", "RatingValue");
            ViewBag.Genre = new SelectList(db.Genres, "GenreId", "GenreName");

            return View(game);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
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