using DraganJanjic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraganJanjic.Interfaces
{
    public interface IOrganizacionaJedinicaRepository
    {
        IEnumerable<OrganizacionaJedinica> GetAll();
        OrganizacionaJedinica GetById(int id);
        IEnumerable<OrganizacionaJedinica> GetTradicija();

    }
}
