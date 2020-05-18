using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCCUERJMapas.Models
{
    public class CepContext
    {
        public string ConnectionString { get; set; }

        public CepContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public Cep ObterEnderecoCompletoCep(string cep)
        {
            Cep objCep = null;
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select * from ceps where cep = '"+cep.Replace("-","")+ "' LIMIT 1", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            objCep = new Cep
                            {
                                Bairro = reader["bairro"].ToString(),
                                DescricaoCep = reader["cep"].ToString(),
                                Endereco = reader["endereco"].ToString(),
                                EstadoMunicipio = reader["estado_municipio"].ToString(),
                                Id = Convert.ToInt32(reader["id_cep"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objCep;
        }
    }
}
