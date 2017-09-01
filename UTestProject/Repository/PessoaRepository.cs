using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTestProject.Class;
using Core.BaseRepository;
using Dapper;
using System.Data.SqlClient;
using Dommel;
using Core.Connection;

namespace UTestProject.Repository
{
    public class PessoaRepository : RepositoryBase<Pessoa>
    {
        private SqlConnectionCore _connection;

        public PessoaRepository(SqlConnectionCore connection) : base(connection) { _connection = connection; }        

        public Pessoa GetPessoaJose()
        {
            return _connection.Select<Pessoa>(p => p.Nome_pessoa == "José").FirstOrDefault();
        }        

        public Pessoa GetByName(string name)
        {
            return _connection.Select<Pessoa>(p => p.Nome_pessoa == name).FirstOrDefault();
        }

        public long ProximoId()
        {
            return _connection.GetAll<Pessoa>().Max(m => m.Id) + 1;
        }

        public IEnumerable<Pessoa> GetPessoa_Dependentes()
        {
            return _connection.SelectJoin<Pessoa, Dependente, Pessoa>((pessoa, dependente) => {

                pessoa.Dependentes.Add(dependente);

                return pessoa;
            });
        }

        public IEnumerable<Pessoa> GetPessoa_Trabalhos()
        {
            return _connection.SelectJoin<Pessoa, Trabalho, Pessoa>((pessoa, trabalho) => {

                pessoa.Trabalhos.Add(trabalho);

                return pessoa;

            });    
        }
        
    }
}
