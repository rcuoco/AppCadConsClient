using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace AppCadConsClient.Core.Domain
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public int IdCity { get; set; }
        public int IdUF { get; set; }
        public string CEP { get; set; }
    }

    public class ClienteRepository : RepositoryBase
    {
        private readonly string _connectionString;

        public ClienteRepository()
        {
            _connectionString = ConnectionString();
        }

        // Método para cadastrar um cliente
        public bool InserirCliente(Cliente cliente)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("InserirCliente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@CPF", cliente.CPF);
                    command.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@Rua", cliente.Rua);
                    command.Parameters.AddWithValue("@Numero", cliente.Numero);
                    command.Parameters.AddWithValue("@Bairro", cliente.Bairro);
                    command.Parameters.AddWithValue("@idCity", cliente.IdCity);
                    command.Parameters.AddWithValue("@IdUF", cliente.IdUF);
                    command.Parameters.AddWithValue("@CEP", cliente.CEP);

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                connection.Close();
                connection.Dispose();
            }
            return rowsAffected > 0;
        }

        // Método para buscar todos os clientes
        public List<Cliente> ObterTodosClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("ObterTodosClientes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente
                            {
                                IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                Nome = reader["Nome"].ToString(),
                                Email = reader["Email"].ToString(),
                                CPF = reader["CPF"].ToString(),
                                DataNascimento = Convert.ToDateTime(reader["DataNascimento"]),
                                Rua = reader["Rua"].ToString(),
                                Numero = int.Parse(reader["Numero"].ToString()),
                                Bairro = reader["Bairro"].ToString(),
                                IdCity = int.Parse(reader["IdCity"].ToString()),
                                IdUF = int.Parse(reader["IdUF"].ToString()),
                            };
                            clientes.Add(cliente);
                        }
                    }
                }
            }

            return clientes;
        }

        // Método para atualizar um cliente
        public bool AtualizarCliente(Cliente cliente)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("AtualizarCliente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@CPF", cliente.CPF);
                    command.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
                    command.Parameters.AddWithValue("@Rua", cliente.Rua);
                    command.Parameters.AddWithValue("@Numero", cliente.Numero);
                    command.Parameters.AddWithValue("@Bairro", cliente.Bairro);
                    command.Parameters.AddWithValue("@idCyty", cliente.IdCity);
                    command.Parameters.AddWithValue("@IdUF", cliente.IdUF);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Método para excluir um cliente
        public bool ExcluirCliente(int idCliente)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("ExcluirCliente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdCliente", idCliente);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Método para buscar um clientes
        public List<Cliente> ObterClientes(string nome = "", string datade = "", string dataate = "", string CPF = "")
        {
            List<Cliente> clientes = new List<Cliente>();
            Cliente client = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("ObterClientes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@nome", "%" + nome.Trim().Replace(" ","%") + "%");
                    command.Parameters.AddWithValue("@datade", datade);
                    command.Parameters.AddWithValue("@datate", (dataate == null? datade : dataate));
                    command.Parameters.AddWithValue("@CPF", CPF);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            client = new Cliente
                            {
                                IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                Nome = reader["Nome"].ToString(),
                                Email = reader["Email"].ToString(),
                                CPF = reader["CPF"].ToString(),
                                DataNascimento = Convert.ToDateTime(reader["DataNascimento"]),
                                Rua = reader["Rua"].ToString(),
                                Numero = int.Parse(reader["Numero"].ToString()),
                                Bairro = reader["Bairro"].ToString(),
                                IdCity = int.Parse(reader["IdCity"].ToString()),
                                IdUF = int.Parse(reader["IdUF"].ToString()),
                            };
                            clientes.Add(client);
                        }
                    }
                }
            }

            return clientes;
        }
        // Método para buscar um cliente por ID
        public Cliente ObterClientePorId(int idCliente)
        {
            Cliente cliente = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("ObterClientePorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdCliente", idCliente);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                Nome = reader["Nome"].ToString(),
                                Email = reader["Email"].ToString(),
                                CPF = reader["CPF"].ToString(),
                                DataNascimento = Convert.ToDateTime(reader["DataNascimento"]),
                                Rua = reader["Rua"].ToString(),
                                Numero = int.Parse(reader["Numero"].ToString()),
                                Bairro = reader["Bairro"].ToString(),
                                IdCity = int.Parse(reader["IdCity"].ToString()),
                                IdUF = int.Parse(reader["IdUF"].ToString()),
                            };
                        }
                    }
                }
            }

            return cliente;
        }
        // Método para validar um cliente por CPF
        public bool ObterClientePorCPF(string cpf)
        {
            bool retorno = false;
            Cliente cliente = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("ObterClientePorCPF", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cpf", cpf);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                Nome = reader["Nome"].ToString(),
                                Email = reader["Email"].ToString(),
                                CPF = reader["CPF"].ToString(),
                                DataNascimento = Convert.ToDateTime(reader["DataNascimento"]),
                                Rua = reader["Rua"].ToString(),
                                Numero = int.Parse(reader["Numero"].ToString()),
                                Bairro = reader["Bairro"].ToString(),
                                CEP = reader["CEP"].ToString(),
                                IdCity = int.Parse(reader["IdCity"].ToString()),
                                IdUF = int.Parse(reader["IdUF"].ToString()),
                            };
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                    if (cliente != null)
                    {
                        retorno = true;
                    }
                }
            }

            return retorno;
        }
    }
}
