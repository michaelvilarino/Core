using Core.MappingClass;

namespace UTestProject.Class
{
    [DBTable("Trabalho")]
   public class Trabalho
    {
        [Key_AutoIncrement("Id")]
        public long Id { get; set; }

        [DBColumn("Descricao")]
        public string Descricao { get; set; }

        [DBColumn("Salario")]
        public float Salario { get; set; }

        [DBColumnForeignKey("IdPessoa")]
        public long PessoaId { get; set; }
    }
}
