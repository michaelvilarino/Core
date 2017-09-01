using Core.MappingClass;

namespace UTestProject.Class
{
    [DBTable("Dependente")]
    public class Dependente
    {
        [Key_AutoIncrement("Id")]
        public int Id { get; set; }

        [DBColumn("Nome")]
        public string Nome { get; set; }

        [DBColumnForeignKey("PessoaId")]
        public long PessoaId { get; set; }

        public Pessoa pessoa { get; set; }
    }
}
