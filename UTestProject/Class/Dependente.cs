using Core.MappingClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTestProject.Class
{
    [DBTable("Dependente")]
    public class Dependente
    {
        [DBColumn("Id")]
        public int Id { get; set; }

        [DBColumn("Nome")]
        public string Nome { get; set; }

        [DBColumn("PessoaId")]
        public long PessoaId { get; set; }

        public Pessoa pessoa { get; set; }
    }
}
