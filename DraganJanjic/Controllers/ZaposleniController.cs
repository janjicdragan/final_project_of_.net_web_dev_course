using DraganJanjic.Interfaces;
using DraganJanjic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DraganJanjic.Controllers
{
    public class ZaposleniController : ApiController
    {
        private IZaposleniRepository _repository { get; set; }

        public ZaposleniController(IZaposleniRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Zaposleni> Get()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            Zaposleni zaposleni = _repository.GetById(id);
            
            if(zaposleni == null)
            {
                return NotFound();
            }

            return Ok(zaposleni);
        }

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

        [Route("api/brojnost")]
        public IEnumerable<JedinicaBrZaposlenihDTO> GetBrojnost()
        {
            return _repository.GetBrojnost();
        }

        [Route("api/plate")]
        public IEnumerable<JedinicaProsecnaPlataDTO> PostJediniceIPlate(PlataFilter filter)
        {
            return _repository.GetJediniceByPlata(filter);
        }

        public IEnumerable<Zaposleni> GetByRodjenje(int rodjenje)
        {
            return _repository.GetByRodjenje(rodjenje);
        }

        [Authorize]
        [Route("api/pretraga")]
        public IEnumerable<Zaposleni> PostFilterZaposleniByPlata(PlataFilter2 filter)
        {
            return _repository.GetZaposleniByPlata(filter);
        }
    }
}
