using Core.BaseRepository;
using Core.Connection;
using System.Collections.Generic;
using UTestProject.Class;

namespace UTestProject.Repository
{
    public class DependenteRepository : RepositoryBase<Dependente>
    {
        SqlConnectionCore _sqlConnectionCore;

        public DependenteRepository(SqlConnectionCore sqlConnectionCore) : base(sqlConnectionCore)
        {
            _sqlConnectionCore = sqlConnectionCore;
        }

        public IEnumerable<Dependente> GetDependentes_Pessoas()
        {
            return _sqlConnectionCore.SelectJoin<Dependente, Pessoa, Dependente>((dependente, pessoa) => {

                dependente.pessoa = new Pessoa() { Id = pessoa.Id, Nome_pessoa = pessoa.Nome_pessoa };

                return dependente;

            });
        }
    }
}
