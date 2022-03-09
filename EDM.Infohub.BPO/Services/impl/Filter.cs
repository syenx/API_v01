using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDM.Infohub.BPO.Services.impl
{
    public class Filter
    {
        private IConfiguration _configuration;

        private List<string> elementosFiltrantes { get; set; }

        public Filter(IConfiguration configuration)
        {
            _configuration = configuration;
            elementosFiltrantes = _configuration[$"Filtro"].Split(new char[] { ',' }).ToList();
        }

        public void PrintConfig()
        {
            foreach (string elemento in elementosFiltrantes)
            {
                Console.WriteLine(elemento);
            }
        }

        public bool FiltrarMensagem(string tipoMensagem)
        {
            var processaMensagem = false;
            foreach (string filtro in elementosFiltrantes)
            {
                processaMensagem = processaMensagem ? processaMensagem : tipoMensagem == filtro;
            }

            return processaMensagem;
        }
    }
}
