using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursosAPI.Repository.Entities
{
    public class Usuario
    {
        public int id { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
    }
}
