using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTestProject.Repository;
using UTestProject.Map;
using System.Data.SqlClient;
using System.Configuration;
using UTestProject.Class;
using System.Collections.Generic;
using Core.Connection;
using System.Linq;
using System;

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

            _pessoaRepository     = new PessoaRepository(_sqlConnectionCore);
            _dependenteRepository = new DependenteRepository(_sqlConnectionCore);
            _trabalhoRepository   = new TrabalhoRepository(_sqlConnectionCore);

        }


        [TestMethod]
        public void InsertUsersLote()
        {            
            _sqlConnectionCore.BeginTransaction();

            try
            {
                for (int i = 0; i <= 100; i++)
                {
                    Pessoa pessoa = new Pessoa() { Nome_pessoa = "Michael_Teste " + i };

                    var Id = _pessoaRepository.Insert(pessoa);

                    Dependente dependente = new Dependente() { Nome = "dependente "+i, PessoaId =   (int)Id };

                    _dependenteRepository.Insert(dependente);
                }

                _sqlConnectionCore.CommitTransaction();
            }
            catch (System.Exception e)
            {
                _sqlConnectionCore.RollBackTransaction();
                throw e;
            }
            
        }

        [TestMethod]
        public void ListAllPessoas()
        {            
            var result = _pessoaRepository.GetAll();
        }

        [TestMethod]
        public void ListAllPessoas_dependentes()
        {
            var result = _pessoaRepository.GetPessoa_Dependentes();
        }

        [TestMethod]
        public void UpdatePessoa()
        {
            var pessoa = _pessoaRepository.GetByName("Michael_Teste 0");

            pessoa.Nome_pessoa = "Atualizado";

            _pessoaRepository.Update(pessoa);
        }

        [TestMethod]
        public void DeleteDependente()
        {
            var dependentes = _dependenteRepository.GetListWithPredicate(w => w.Id >= 19 && w.Id <= 30).ToList();

            foreach (var dep in dependentes)
            {
                _dependenteRepository.Delete(dep);
            }
        }
    }
}
