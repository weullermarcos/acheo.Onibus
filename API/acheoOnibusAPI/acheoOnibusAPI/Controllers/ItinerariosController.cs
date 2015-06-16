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
    public class ItinerariosController : ApiController
    {
        private dbAcheoOnibusEntities db = new dbAcheoOnibusEntities();

        // GET: api/Itinerarios
        public IQueryable<getItinerarios> GetgetItinerarios()
        {
            return db.getItinerarios;
        }

        // GET: api/Itinerarios/5
        [ResponseType(typeof(getItinerarios))]
        public IHttpActionResult GetgetItinerarios(int id)
        {
            getItinerarios getItinerarios = db.getItinerarios.Find(id);
            if (getItinerarios == null)
            {
                return NotFound();
            }

            return Ok(getItinerarios);
        }

        // PUT: api/Itinerarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutgetItinerarios(int id, getItinerarios getItinerarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != getItinerarios.idItinerario)
            {
                return BadRequest();
            }

            db.Entry(getItinerarios).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!getItinerariosExists(id))
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

        // POST: api/Itinerarios
        [ResponseType(typeof(getItinerarios))]
        public IHttpActionResult PostgetItinerarios(getItinerarios getItinerarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.getItinerarios.Add(getItinerarios);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = getItinerarios.idItinerario }, getItinerarios);
        }

        // DELETE: api/Itinerarios/5
        [ResponseType(typeof(getItinerarios))]
        public IHttpActionResult DeletegetItinerarios(int id)
        {
            getItinerarios getItinerarios = db.getItinerarios.Find(id);
            if (getItinerarios == null)
            {
                return NotFound();
            }

            db.getItinerarios.Remove(getItinerarios);
            db.SaveChanges();

            return Ok(getItinerarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool getItinerariosExists(int id)
        {
            return db.getItinerarios.Count(e => e.idItinerario == id) > 0;
        }
    }
}