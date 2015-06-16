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
    public class OnibusController : ApiController
    {
        private dbAcheoOnibusEntities db = new dbAcheoOnibusEntities();

        // GET: api/Onibus
        public IQueryable<getOnibus> GetgetOnibus()
        {
            return db.getOnibus;
        }

        // GET: api/Onibus/5
        [ResponseType(typeof(getOnibus))]
        public IHttpActionResult GetgetOnibus(string id)
        {
            getOnibus getOnibus = db.getOnibus.Find(id);
            if (getOnibus == null)
            {
                return NotFound();
            }

            return Ok(getOnibus);
        }

        // PUT: api/Onibus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutgetOnibus(string id, getOnibus getOnibus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != getOnibus.numero)
            {
                return BadRequest();
            }

            db.Entry(getOnibus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!getOnibusExists(id))
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

        // POST: api/Onibus
        [ResponseType(typeof(getOnibus))]
        public IHttpActionResult PostgetOnibus(getOnibus getOnibus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.getOnibus.Add(getOnibus);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (getOnibusExists(getOnibus.numero))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = getOnibus.numero }, getOnibus);
        }

        // DELETE: api/Onibus/5
        [ResponseType(typeof(getOnibus))]
        public IHttpActionResult DeletegetOnibus(string id)
        {
            getOnibus getOnibus = db.getOnibus.Find(id);
            if (getOnibus == null)
            {
                return NotFound();
            }

            db.getOnibus.Remove(getOnibus);
            db.SaveChanges();

            return Ok(getOnibus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool getOnibusExists(string id)
        {
            return db.getOnibus.Count(e => e.numero == id) > 0;
        }
    }
}