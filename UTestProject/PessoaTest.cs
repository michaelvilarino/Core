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

        SqlConnectionCore _sqlConnectionCore;

        public PessoaTest()
        {
             _sqlConnectionCore = new SqlConnectionCore(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);

            RegisterMappings.Register<Pessoa>();
            RegisterMappings.Register<Dependente>();
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
    }
}
