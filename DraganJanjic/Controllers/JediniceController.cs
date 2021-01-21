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
    public class JediniceController : ApiController
    {
        private IOrganizacionaJedinicaRepository _repository { get; set; }

        public JediniceController(IOrganizacionaJedinicaRepository repository)
        {
            _repository = repository;
        }

        //GET api/jedinice
        /// <summary>
        /// Get all OrganizacionaJedinica
        /// </summary>
        [ResponseType(typeof(IEnumerable<OrganizacionaJedinica>))]
        public IEnumerable<OrganizacionaJedinica> Get()
        {
            return _repository.GetAll();
        }

        //GET api/jedinice/1
        /// <summary>
        /// Get one OrganizacionaJedinica by ID
        /// </summary>
        [ResponseType(typeof(OrganizacionaJedinica))]
        public IHttpActionResult Get(int id)
        {
            OrganizacionaJedinica jedinica = _repository.GetById(id);

            if(jedinica == null)
            {
                return NotFound();
            }

            return Ok(jedinica);
        }

        //GET api/tradicija
        /// <summary>
        /// Get two OrganizacionaJedinica with the greatest and the lowest value of GodinaOsnivanja, sorted ascending by value
        /// </summary>
        [ResponseType(typeof(IEnumerable<OrganizacionaJedinica>))]
        [Route("api/tradicija")]
        public IEnumerable<OrganizacionaJedinica> GetTradicija()
        {
            return _repository.GetTradicija();
        }
    }
}
