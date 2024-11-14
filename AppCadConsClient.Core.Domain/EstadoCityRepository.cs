using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCadConsClient.Core.Domain
{
    public class Estados
    {
        public int idUF { get; set; }
        public string UF { get; set; }
        public string Estado { get; set; }
    }
    [Serializable]
    public class Cidades
    {
        public int IdCity { get; set; }
        public int idUF { get; set; }
        public string IdCityUF { get; set; }
        public string Cidade { get; set; }
    }
    public class EstadoCityRepository : RepositoryBase
    {
        private readonly string _connectionString; 
        public EstadoCityRepository()
        {
            _connectionString = ConnectionString();
        }

        // Método para buscar todos os Estados
        public List<Estados> ListStates()
        {
            List<Estados> estados = new List<Estados>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("ObterTodosEstados", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Estados estado = new Estados
                            {
                                idUF = Convert.ToInt32(reader["idUF"]),
                                UF = reader["UF"].ToString(),
                                Estado = reader["Estado"].ToString()
                            };
                            estados.Add(estado);
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                }
            }

            return estados;
        }

        // Método para buscar todas as Cidades
        public List<Cidades> ListCitys()
        {
            List<Cidades> cidades = new List<Cidades>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("ObterTodasCidades", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cidades cidade = new Cidades
                            {
                                IdCity = Convert.ToInt32(reader["IdCity"]),
                                idUF = Convert.ToInt32(reader["idUF"]),
                                IdCityUF = reader["IdCity"].ToString() + '|' + reader["idUF"].ToString(),
                                Cidade = reader["Cidade"].ToString()
                            };
                            cidades.Add(cidade);
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                }
            }

            return cidades;
        }
    }
}
