**Core**
--------
Este repositório é uma extensão do Dapper, com o objetivo de utilização totalmente voltada à objetos.

Para utilizá-lo, basta incluir o Core.Dll em seu projeto.

**Forma de Utilização:**
Criar as classes conforme os atributos abaixo:

    [DBTable("tblPessoa")]
    public class Pessoa
    {
        [DBColumn("Id")]
        public long Id { get; set; }

        [DBColumn("Nome")]
        public string Nome_pessoa { get; set; }
    }
     
    [DBTable("tblDependente")]
    public class Dependente
    {
      [DBColumn("Id")]
        public long Id { get; set; }
  
      [DBColumn("Nome")]
        public string Nome { get; set; }  
      
      //Não utilizar Underline para Fk - Será corrigido no próximo release  
      [DBColumn("PessoaId")]
        public long PessoaId { get; set; } 

       //Objeto de navegação, não precisa de atributo (usado em joins)
       public Pessoa pessoa { get; set; } 
    }

Após a criação das classes, será necessário registrá-las no core, afim de realizar os mapeamentos, coloque esse exemplo de código na inicialização de seu projeto, no meu caso é global.asax (Projeto web em MVC):

     RegisterMappings.Register<Pessoa>();
     RegisterMappings.InitializeConfigMappings();

**Utilizando o BaseRepository do Core:**

O Core.Dll trabalha com uma extensão do SqlConnection chamado: SqlConnectionCore, nele temos todos os métodos necessários para realizar qualquer tipo de transação.

    RepositoryBase do Core:
    Possui os métodos padrões de Crud e seleção de dados:
     
 - GetAll()
 - Get(Id)
 - GetList()
 - Insert()
 - Update()
 - Delete()

Modo de utilização:

    public class PessoaRepository : RepositoryBase<Pessoa>
    {
       private SqlConnectionCore _connection;

        public PessoaRepository(SqlConnectionCore connection) 
        : base(connection) { _connection = connection; } 

      public Pessoa SelecionarPorNome(string nome)
      {
         return _connection.Select<Pessoa>(p => p.Nome_pessoa == name).FirstOrDefault();
      }          
    }

    
Utilizando o PessoaRepository  na aplicação:

    public class PessoaService
    {
      _sqlConnectionCore = new SqlConnectionCore("SuaStringDeConexao");

       private PessoaRepository _pessoaRepository;
      
      public List<Pessoa> GetList()
      {
         _pessoaRepository = new PessoaRepository(_sqlConnectionCore);
            return _pessoaRepository.GetAll().ToList();
      }
         
      public void InserirNovaPessoa()
      {
         Pessoa pessoa = new Pessoa(){ Id = 1, Nome_pessoa  = "Teste"};
         _pessoaRepository.Insert(pessoa);
      }            
    }

Utilizando Join: (Limitado até 7 tabelas, será disponibilizado mais)

    public class DependenteRepository : RepositoryBase<Dependente>
    {
        SqlConnectionCore _sqlConnectionCore;

        public DependenteRepository(SqlConnectionCore sqlConnectionCore) 
         : base(sqlConnectionCore)
        {
            _sqlConnectionCore = sqlConnectionCore;
        }

        public IEnumerable<Dependente> GetDependentes_Pessoas()
        {
           return _sqlConnectionCore.SelectJoin<Dependente, Pessoa, Dependente>((dependente, pessoa) => 
           {
                dependente.pessoa = new Pessoa() 
                { 
                  Id = pessoa.Id, 
                  Nome_pessoa = pessoa.Nome_pessoa 
                 };
                return dependente;
            });
        }
    }
    
    O ultimo parâmetro do método "SelectJoin" é a classe de saída, os outros serão apenas relações.


     

