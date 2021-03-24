using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursosAPI.Models.Usuario
{
    public class UsuarioViewModelOutput
    {
        public int id { get; set; }
        public string login { get; set; }
        public string email { get; set; }
    }
}
