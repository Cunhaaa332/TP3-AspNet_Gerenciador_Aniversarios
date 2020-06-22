using Microsoft.AspNetCore.Authentication;
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
                sqlCommand.CommandText = "INSERT INTO ANIVERSARIANTE(Nome, SobreNome, Birth) VALUES (@P1, @P2, @P3)";
                sqlCommand.Parameters.AddWithValue("P1", pessoa.Nome);
                sqlCommand.Parameters.AddWithValue("P2", pessoa.SobreNome);
                sqlCommand.Parameters.AddWithValue("P3", pessoa.birth);

                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Editar(PessoaModel pessoa) {
            using (var connection = new SqlConnection(this.ConnectionString)) {

                var sql = @"UPDATE ANIVERSARIANTE
                                 SET Nome = @P2,
                                 SobreNome = @P3,      
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

        public void Deletar(PessoaModel pessoa) {
            using (var connection = new SqlConnection(this.ConnectionString)) {

                var sql = @"DELETE FROM ANIVERSARIANTE
                                 WHERE Id = @P1";

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.AddWithValue("P1", pessoa.Id);

                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        public List<PessoaModel> Listar() {

            List<PessoaModel> result = new List<PessoaModel>();
            using (var connection = new SqlConnection(this.ConnectionString)) {
                var sql = @"SELECT Id, Nome, SobreNome, Birth FROM ANIVERSARIANTE";

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = sql;

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read()) {
                    PessoaModel pessoa = new PessoaModel() {
                        Id = int.Parse(reader["Id"].ToString()),
                        Nome = reader["Nome"].ToString(),
                        SobreNome = reader["SobreNome"].ToString(),
                        birth = Convert.ToDateTime(reader["Birth"])
                    };
                    result.Add(pessoa);
                }
                foreach(var pessoa in result) {
                    pessoa.DiasRestantes = pessoa.QntosDiasFaltam();
                }
                connection.Close();
            }
            return result.OrderBy(pessoa => pessoa.DiasRestantes).ToList();
        }

        public PessoaModel GetById(int id) {

            List<PessoaModel> result = new List<PessoaModel>();
            using (var connection = new SqlConnection(this.ConnectionString)) {

                var sql = @"SELECT Id, Nome, SobreNome, Birth FROM ANIVERSARIANTE WHERE Id = @P1";

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.AddWithValue("P1", id);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read()) {
                    PessoaModel pessoa = new PessoaModel() {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = reader["Nome"].ToString(),
                        SobreNome = reader["SobreNome"].ToString(),
                        birth = Convert.ToDateTime(reader["Birth"])
                    };
                    result.Add(pessoa);
                }
                connection.Close();
            }
            return result.FirstOrDefault();
        }

        public List<PessoaModel> GetByName(string nome) {
            List<PessoaModel> result = new List<PessoaModel>();

            using (var connection = new SqlConnection(this.ConnectionString)) {

                var sql = @" SELECT Id, Nome, SobreNome, Birth FROM Aniversariante
                             WHERE Nome LIKE '%' + @P1 + '%' COLLATE SQL_Latin1_General_CP1_CI_AI OR SobreNome LIKE '%' + @P1 + '%' COLLATE SQL_Latin1_General_CP1_CI_AI
                ";

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.AddWithValue("P1", nome);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read()) {
                    PessoaModel pessoa = new PessoaModel() {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = reader["Nome"].ToString(),
                        SobreNome = reader["SobreNome"].ToString(),
                        birth = Convert.ToDateTime(reader["Birth"])
                    };

                    result.Add(pessoa);
                }

                connection.Close();
            }

            return result;
        }


    }
}
