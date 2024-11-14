using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCadConsClient.Core.Domain
{
    public abstract class RepositoryBase
    {

        #region Variaveis
        private static string _connectionString = string.Empty;

        #endregion

        #region Constructors
        public static string ConnectionString()
        {
            return Repository();
        }
        private static string Repository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Repository.MSSQL2024"].ConnectionString;
            return _connectionString;
        }

        #endregion Constructors
    }
}
