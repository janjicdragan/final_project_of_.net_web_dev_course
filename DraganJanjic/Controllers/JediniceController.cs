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
    public class JediniceController : ApiController
    {
        private IOrganizacionaJedinicaRepository _repository { get; set; }

        public JediniceController(IOrganizacionaJedinicaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<OrganizacionaJedinica> Get()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            OrganizacionaJedinica jedinica = _repository.GetById(id);

            if(jedinica == null)
            {
                return NotFound();
            }

            return Ok(jedinica);
        }

        [Route("api/tradicija")]
        public IEnumerable<OrganizacionaJedinica> GetTradicija()
        {
            return _repository.GetTradicija();
        }
    }
}
