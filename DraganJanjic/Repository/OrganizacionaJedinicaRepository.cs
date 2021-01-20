using DraganJanjic.Interfaces;
using DraganJanjic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DraganJanjic.Repository
{
    public class OrganizacionaJedinicaRepository : IDisposable, IOrganizacionaJedinicaRepository
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

        IEnumerable<OrganizacionaJedinica> IOrganizacionaJedinicaRepository.GetAll()
        {
            return db.Jedinice;
        }

        OrganizacionaJedinica IOrganizacionaJedinicaRepository.GetById(int id)
        {
            return db.Jedinice.FirstOrDefault(j => j.Id == id);
        }

        public IEnumerable<OrganizacionaJedinica> GetTradicija()
        {

            IEnumerable<OrganizacionaJedinica> lista = db.Jedinice.OrderBy(j => j.GodinaOsnivanja);
            List<OrganizacionaJedinica> rezultat = new List<OrganizacionaJedinica>();
            
            rezultat.Add(lista.FirstOrDefault());
            rezultat.Add(lista.LastOrDefault());

            return rezultat.AsEnumerable();
        }
    }
}