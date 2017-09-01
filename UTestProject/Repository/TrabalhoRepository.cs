using Core.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTestProject.Class;
using Core.Connection;

namespace UTestProject.Repository
{
    public class TrabalhoRepository : RepositoryBase<Trabalho>
    {
        private SqlConnectionCore _connection;
        public TrabalhoRepository(SqlConnectionCore sqlConnectionCore) : base(sqlConnectionCore)
        {
            _connection = sqlConnectionCore;
        }

        public IEnumerable<Trabalho> GettrabalhosPessoas()
        {
            var result = _connection.SelectJoin<Trabalho, Pessoa, Trabalho>((trabalho, pessoa) => {                

                return trabalho;
            });

            return result;
        }
    }
}
