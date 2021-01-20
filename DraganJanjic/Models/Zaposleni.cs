using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DraganJanjic.Models
{
    public class Zaposleni
    {
        public int Id { get; set; }
        [Required]
        [StringLength(70)]
        public string ImeIPrezime { get; set; }
        [Required]
        [StringLength(50)]
        public string Rola { get; set; }
        [Range(1960,1999)]
        public int? GodinaRodjenja { get; set; }
        [Required]
        [Range(2010, 2020)]
        public int GodinaZaposlenja { get; set; }
        [Required]
        [Range(251d, 9999d)]
        public decimal Plata { get; set; }
        public OrganizacionaJedinica Jedinica { get; set; }
        public int JedinicaId { get; set; }

    }
}