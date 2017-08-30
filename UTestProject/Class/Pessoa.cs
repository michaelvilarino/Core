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
        [DBColumn("Id")]
        public long Id { get; set; }

        [DBColumn("Nome")]
        public string Nome_pessoa { get; set; }

    }
}
