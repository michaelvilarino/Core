using Core.BaseRepository;
using Core.MappingClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTestProject.Class
{
    [DBTable("Pessoa")]
    public class Pessoa
    {
        [Key("Id")]
        public long Id { get; set; }

        [DBColumn("Nome")]
        public string Nome_pessoa { get; set; }

        public List<Trabalho> Trabalhos { get; set; }

        public List<Dependente> Dependentes { get; set; }

        public Pessoa()
        {
            Dependentes = new List<Dependente>();
            Trabalhos = new List<Trabalho>();
        }
    }
}
