using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using acheoOnibusAPI.Models;

namespace acheoOnibusAPI.Controllers
{
    public class ViagensController : ApiController
    {
        private dbAcheoOnibusEntities db = new dbAcheoOnibusEntities();

        // GET: api/Viagens
        public IQueryable<getViagens> GetgetViagens()
        {
            return db.getViagens;
        }

        // GET: api/Viagens/5
        [ResponseType(typeof(getViagens))]
        public IHttpActionResult GetgetViagens(int id)
        {
            getViagens getViagens = db.getViagens.Find(id);
            if (getViagens == null)
            {
                return NotFound();
            }

            return Ok(getViagens);
        }

        // PUT: api/Viagens/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutgetViagens(int id, getViagens getViagens)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != getViagens.idViagem)
            {
                return BadRequest();
            }

            db.Entry(getViagens).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!getViagensExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Viagens
        [ResponseType(typeof(getViagens))]
        public IHttpActionResult PostgetViagens(getViagens getViagens)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.getViagens.Add(getViagens);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = getViagens.idViagem }, getViagens);
        }

        // DELETE: api/Viagens/5
        [ResponseType(typeof(getViagens))]
        public IHttpActionResult DeletegetViagens(int id)
        {
            getViagens getViagens = db.getViagens.Find(id);
            if (getViagens == null)
            {
                return NotFound();
            }

            db.getViagens.Remove(getViagens);
            db.SaveChanges();

            return Ok(getViagens);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool getViagensExists(int id)
        {
            return db.getViagens.Count(e => e.idViagem == id) > 0;
        }
    }
}