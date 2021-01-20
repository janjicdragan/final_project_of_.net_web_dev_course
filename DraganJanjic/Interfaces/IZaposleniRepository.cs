using DraganJanjic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraganJanjic.Interfaces
{
    public interface IZaposleniRepository
    {
        IEnumerable<Zaposleni> GetAll();
        Zaposleni GetById(int id);
        void Add(Zaposleni mod);
        void Update(Zaposleni mod);
        void Delete(Zaposleni mod);
        IEnumerable<JedinicaBrZaposlenihDTO> GetBrojnost();
        IEnumerable<JedinicaProsecnaPlataDTO> GetJediniceByPlata(PlataFilter filter);
        IEnumerable<Zaposleni> GetByRodjenje(int rodjenje);
        IEnumerable<Zaposleni> GetZaposleniByPlata(PlataFilter2 filter);
    }
}
