using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DraganJanjic.Models
{
    public class JedinicaBrZaposlenihDTO
    {
        public int Id { get; set; }
        public string Jedinica { get; set; }
        public int BrojZaposlenih { get; set; }
    }
}