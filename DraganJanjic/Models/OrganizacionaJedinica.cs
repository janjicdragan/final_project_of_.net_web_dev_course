using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DraganJanjic.Models
{
    public class OrganizacionaJedinica
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Ime { get; set; }
        [Range(2010,2019)]
        public int GodinaOsnivanja { get; set; }
    }
}