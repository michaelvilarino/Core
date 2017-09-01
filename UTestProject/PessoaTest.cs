using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTestProject.Repository;
using UTestProject.Map;
using System.Data.SqlClient;
using System.Configuration;
using UTestProject.Class;
using System.Collections.Generic;
using Core.Connection;
using System.Linq;

namespace UTestProject
{
    [TestClass]
    public class PessoaTest
    {
        private PessoaRepository _pessoaRepository;
        private DependenteRepository _dependenteRepository;
        private TrabalhoRepository _trabalhoRepository;

        SqlConnectionCore _sqlConnectionCore;

        public PessoaTest()
        {
             _sqlConnectionCore = new SqlConnectionCore(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);

            RegisterMappings.Register<Pessoa>();
            RegisterMappings.Register<Dependente>();
            RegisterMappings.Register<Trabalho>();
            RegisterMappings.InitializeConfigMappings();
        }

        [TestMethod]
        public void GetList()
        {
            using (SqlConnectionCore connection = new SqlConnectionCore(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString))
            {
                _pessoaRepository = new PessoaRepository(connection);
                var pessoas = _pessoaRepository.GetAll();
            }
        }

        [TestMethod]
        public void GetAll()
        {
            _pessoaRepository = new PessoaRepository(_sqlConnectionCore);
            var pessoaa = _pessoaRepository.GetPessoaJose();
        }

        [TestMethod]
        public void GetdependenteComParente()
        {
            _dependenteRepository = new DependenteRepository(_sqlConnectionCore);

            var result = _dependenteRepository.GetDependentes_Pessoas().ToList();
        }

        [TestMethod]
        public void GetPessoasDependentes()
        {
            _pessoaRepository = new PessoaRepository(_sqlConnectionCore);
            var result = _pessoaRepository.GetPessoa_Dependentes();
        }

        [TestMethod]
        public void InsertNewPessoa()
        {
            _pessoaRepository = new PessoaRepository(_sqlConnectionCore);

            var result = new Pessoa();

           // long proximoId = _pessoaRepository.ProximoId();

            Pessoa pessoa = new Pessoa() { };

           // pessoa.Id = proximoId;
            pessoa.Nome_pessoa = "teste25";

            //if(_pessoaRepository.GetByName(pessoa.Nome_pessoa) == null)
               result = _pessoaRepository.Inserir(pessoa);
        }

        [TestMethod]
        public void InsertDependente()
        {
            //_dependenteRepository = new DependenteRepository(_sqlConnectionCore);

            //Dependente dependente = new Dependente() {  Nome = "dependente1", pessoa_Id = 1 };

            //_dependenteRepository.Insert(dependente);
        }

        [TestMethod]
        public void getAllDependentesAndPessoas()
        {
            _dependenteRepository = new DependenteRepository(_sqlConnectionCore);

            List<Dependente> Dependentes = _dependenteRepository.GetDependentes_Pessoas().ToList();
        }

        [TestMethod]
        public void getPessoa_Trabalhos()
        {
            _trabalhoRepository = new TrabalhoRepository(_sqlConnectionCore);
            var result = _trabalhoRepository.GettrabalhosPessoas();
        }

        [TestMethod]
        public void GetAllTrabalhos()
        {
            _trabalhoRepository = new TrabalhoRepository(_sqlConnectionCore);

            var result = _trabalhoRepository.GetAll();
        }

        [TestMethod]
        public void GetPessoa_trabalho()
        {
            _pessoaRepository = new PessoaRepository(_sqlConnectionCore);
            var result = _pessoaRepository.GetPessoa_Trabalhos();
        }

        [TestMethod]
        public void CadastraTrabalho()
        {

            Trabalho trabalho   = new Trabalho();
            trabalho.Descricao  = "Juiz";
            trabalho.Salario    = 2000;
            trabalho.PessoaId   = 8;

            _trabalhoRepository = new TrabalhoRepository(_sqlConnectionCore);
            _trabalhoRepository.Insert(trabalho);

        }
    }
}
