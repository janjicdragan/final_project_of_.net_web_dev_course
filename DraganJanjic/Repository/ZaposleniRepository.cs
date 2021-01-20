using DraganJanjic.Interfaces;
using DraganJanjic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DraganJanjic.Repository
{
    public class ZaposleniRepository : IDisposable, IZaposleniRepository
    {
        ApplicationDbContext db = new ApplicationDbContext();

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Zaposleni> GetAll()
        {
            return db.Zaposleni.Include(z => z.Jedinica).OrderBy(z => z.GodinaZaposlenja);
        }

        public Zaposleni GetById(int id)
        {
            return db.Zaposleni.Include(z => z.Jedinica).FirstOrDefault(z => z.Id == id);
        }

        public void Add(Zaposleni zaposleni)
        {
            db.Zaposleni.Add(zaposleni);
            db.SaveChanges();
        }

        public void Update(Zaposleni zaposleni)
        {
            db.Entry(zaposleni).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
        }

        public void Delete(Zaposleni zaposleni)
        {
            db.Zaposleni.Remove(zaposleni);
            db.SaveChanges();
        }

        public IEnumerable<JedinicaBrZaposlenihDTO> GetBrojnost()
        {
            return db.Zaposleni
                .Include(z => z.Jedinica)
                .GroupBy(z => z.Jedinica, z => z.JedinicaId,
                (jedinica, brojZaposlenih) => new JedinicaBrZaposlenihDTO()
                {
                    Id = jedinica.Id,
                    Jedinica = jedinica.Ime,
                    BrojZaposlenih = brojZaposlenih.Count()
                }).OrderByDescending(j => j.BrojZaposlenih);
        }

        public IEnumerable<JedinicaProsecnaPlataDTO> GetJediniceByPlata(PlataFilter filter)
        {
            return db.Zaposleni
                 .Include(z => z.Jedinica)
                 .GroupBy(z => z.Jedinica, z => z.Plata,
                 (jedinica, plata) => new JedinicaProsecnaPlataDTO()
                 {
                     Id = jedinica.Id,
                     Jedinica = jedinica.Ime,
                     ProsecnaPlata = plata.Average()
                 }).Where(j => j.ProsecnaPlata > filter.Granica)
                 .OrderBy(j => j.ProsecnaPlata);
                
        }

        public IEnumerable<Zaposleni> GetByRodjenje(int rodjenje)
        {
            return db.Zaposleni
                .Include(z => z.Jedinica)
                .Where(z => z.GodinaRodjenja > rodjenje)
                .OrderBy(z => z.GodinaRodjenja);
        }

        public IEnumerable<Zaposleni> GetZaposleniByPlata(PlataFilter2 filter)
        {
            return db.Zaposleni
                 .Include(z => z.Jedinica)
                 .Where(z => z.Plata >= filter.Najmanje && z.Plata <= filter.Najvise)
                 .OrderByDescending(z => z.Plata);
        }
    }
}