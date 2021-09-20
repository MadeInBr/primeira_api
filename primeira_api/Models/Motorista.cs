using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace primeira_api.Models
{
    public class Motorista
    {
        public string Nome { get; set; }

        public Motorista(string texto)
        {
            this.Nome = texto;
        }
    }
}