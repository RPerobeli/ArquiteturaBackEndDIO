using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursosAPI.Repository.Entities
{
    public class Curso
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public int idUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
