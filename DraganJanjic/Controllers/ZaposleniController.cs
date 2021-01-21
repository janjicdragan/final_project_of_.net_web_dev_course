using DraganJanjic.Interfaces;
using DraganJanjic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace DraganJanjic.Controllers
{
    public class ZaposleniController : ApiController
    {
        private IZaposleniRepository _repository { get; set; }

        public ZaposleniController(IZaposleniRepository repository)
        {
            _repository = repository;
        }

        //GET api/zaposleni
        /// <summary>
        /// Get all Zaposleni, sorted by GodinaZaposlenja ascending
        /// </summary>
        [ResponseType(typeof(IEnumerable<Zaposleni>))]
        public IEnumerable<Zaposleni> Get()
        {
            return _repository.GetAll();
        }

        //GET api/zaposleni/1
        /// <summary>
        /// Get one Zaposleni by ID
        /// </summary>
        [ResponseType(typeof(Zaposleni))]
        public IHttpActionResult Get(int id)
        {
            Zaposleni zaposleni = _repository.GetById(id);
            
            if(zaposleni == null)
            {
                return NotFound();
            }

            return Ok(zaposleni);
        }

        //POST api/zaposleni
        /// <summary>
        /// Add new Zaposleni
        /// </summary>
        [ResponseType(typeof(Zaposleni))]
        [Authorize]
        public IHttpActionResult Post(Zaposleni zaposleni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(zaposleni);
            return CreatedAtRoute("DefaultApi", new { id = zaposleni.Id }, _repository.GetById(zaposleni.Id));
        }

        //PUT api/zaposleni/1
        /// <summary>
        /// Update existing Zaposleni
        /// </summary>
        [ResponseType(typeof(Zaposleni))]
        public IHttpActionResult Put(int id, Zaposleni zaposleni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != zaposleni.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(zaposleni);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(_repository.GetById(zaposleni.Id));
        }

        //DELETE api/zaposleni/1
        /// <summary>
        /// Delete existing Nezaposleni
        /// </summary>
        [ResponseType(typeof(void))]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            Zaposleni zaposleni = _repository.GetById(id);

            if (zaposleni == null)
            {
                return NotFound();
            }

            _repository.Delete(zaposleni);
            return Ok();
        }

        //GET api/brojnost
        /// <summary>
        /// Get OrganizacionaJedinica with number of Zaposleni sorted in descending orded by number of Zaposleni
        /// </summary>
        [Route("api/brojnost")]
        [ResponseType(typeof(IEnumerable<JedinicaBrZaposlenihDTO>))]
        public IEnumerable<JedinicaBrZaposlenihDTO> GetBrojnost()
        {
            return _repository.GetBrojnost();
        }

        //POST api/plate
        /// <summary>
        /// Get OrganizacionaJedinica with average value of ProsecnaPlata that is between two given values, sorted in ascending orded by average value
        /// </summary>
        [Route("api/plate")]
        [ResponseType(typeof(IEnumerable<JedinicaProsecnaPlataDTO>))]
        public IEnumerable<JedinicaProsecnaPlataDTO> PostJediniceIPlate(PlataFilter filter)
        {
            return _repository.GetJediniceByPlata(filter);
        }

        //GET api/zaposleni?rodjenje=1
        /// <summary>
        /// Get all Zaposleni with GodinaRodjenja value greater that the given value, sorted ascending by GodinaRodjenja
        /// </summary>
        [ResponseType(typeof(IEnumerable<Zaposleni>))]
        public IEnumerable<Zaposleni> GetByRodjenje(int rodjenje)
        {
            return _repository.GetByRodjenje(rodjenje);
        }

        //POST api/pretraga
        /// <summary>
        /// Get all Zaposleni where Plata value is between two given values, sorted descending by Plata
        /// </summary>
        [Authorize]
        [Route("api/pretraga")]
        [ResponseType(typeof(IEnumerable<Zaposleni>))]
        public IEnumerable<Zaposleni> PostFilterZaposleniByPlata(PlataFilter2 filter)
        {
            return _repository.GetZaposleniByPlata(filter);
        }
    }
}
