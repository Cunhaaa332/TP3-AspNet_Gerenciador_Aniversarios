using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TP3_ASPNet.Models;

namespace TP3_ASPNet.Repositorio {
    public class RepositorioPessoa {
        private string ConnectionString { get; set; }

        public RepositorioPessoa(IConfiguration configuration) {
            this.ConnectionString = configuration.GetConnectionString("PessoaConnection");
        }

        public void Salvar(PessoaModel pessoa) {
            using (var connection = new SqlConnection(this.ConnectionString)) {

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "INSERT INTO ANIVERSARIANTE(Id, Nome, SobreNome, Birth) VALUES (@P1, @P2, @P3, @P4)";
                sqlCommand.Parameters.AddWithValue("P1", pessoa.Id);
                sqlCommand.Parameters.AddWithValue("P2", pessoa.Nome);
                sqlCommand.Parameters.AddWithValue("P3", pessoa.SobreNome);
                sqlCommand.Parameters.AddWithValue("P4", pessoa.birth);

                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

            public void Editar(PessoaModel pessoa) {
                using (var connection = new SqlConnection(this.ConnectionString)) {

                var sql = @"UPDATE ANIVERSARIANTE
                                 SET Nome = @P2,
                                 SobreNome = @P3           
                                 Birth = @P4
                                 WHERE Id = @P1";

                    if (connection.State != System.Data.ConnectionState.Open)
                        connection.Open();

                    SqlCommand sqlCommand = connection.CreateCommand();
                    sqlCommand.CommandText = sql;
                    sqlCommand.Parameters.AddWithValue("P1", pessoa.Id);
                    sqlCommand.Parameters.AddWithValue("P2", pessoa.Nome);
                    sqlCommand.Parameters.AddWithValue("P3", pessoa.SobreNome);
                    sqlCommand.Parameters.AddWithValue("P4", pessoa.birth);

                    sqlCommand.ExecuteNonQuery();

                    connection.Close();
                }
            }
    }
}
