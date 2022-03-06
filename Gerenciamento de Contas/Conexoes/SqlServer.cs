using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Gerenciamento_de_Contas.Conexoes
{
    public class SqlServer
    {
        private readonly SqlConnection _conexao;

        public SqlServer()
        {
            this._conexao = new SqlConnection(@"Server=DESKTOP-05IP2B2\SQLEXPRESS ;Database=Contas;User Id=sa;Password=Pardini2021!;");
        }

        public void InserirConta(Entidades.Contas conta)
        {
            try
            {
                _conexao.Open();

                string sql = @"INSERT INTO Contas
                                (nomeConta, valorConta)
                                VALUES
                                (@nomeConta, @valorConta);";

                using(SqlCommand cmd = new SqlCommand(sql, _conexao))
                {
                    cmd.Parameters.AddWithValue("Id", conta.Id);
                    cmd.Parameters.AddWithValue("nomeConta", conta.nomeConta);
                    cmd.Parameters.AddWithValue("valorConta", conta.valorConta);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }
        }

        public bool VerificarExistenciaConta(string nome)
        {
            try
            {
                _conexao.Open();

                string query = @"select Count(nomeConta) AS total 
                                 from Contas WHERE nomeConta = @nomeConta;";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@nomeConta", nome);

                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            finally
            {
                _conexao.Close();
            }
        }

        public void AtualizarConta(Entidades.Contas conta)
        {
            try
            {
                _conexao.Open();

                string query = @"UPDATE Contas
                                   SET nomeConta = @nomeConta
                                      ,valorConta = @valorConta
                                      
                                 WHERE nomeConta = @nomeConta";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("nomeConta", conta.nomeConta);
                    cmd.Parameters.AddWithValue("valorConta", conta.valorConta);

                    cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                _conexao.Close();
            }
        }


        public void DeletarConta(Entidades.Contas conta)
        {
            try
            {
                _conexao.Open();

                string query = @"DELETE FROM Contas
                                 WHERE Id = @Id";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Id", conta.Id);
                    cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                _conexao.Close();
            }
        }

       
        public List<Entidades.Contas> ListarConta()
        {
            var contas = new List<Entidades.Contas>();
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Contas";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var conta = new Entidades.Contas();

                        conta.Id = (int)rdr["Id"];
                        conta.nomeConta = rdr["nomeConta"].ToString();
                        conta.valorConta = Convert.ToDecimal(rdr["valorConta"]);
                        



                        contas.Add(conta);
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }

            return contas;
        }


        public Entidades.Contas SelecionarConta(string nome)
        {
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Contas
                                 WHERE nomeConta = @nomeConta";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@nomeConta", nome);
                    var rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        var conta = new Entidades.Contas();
                        conta.nomeConta = rdr["nomeConta"].ToString();
                        conta.valorConta = Convert.ToDecimal(rdr["valorConta"]);


                        return conta;
                    }
                    else
                    {
                        throw new InvalidOperationException("nomeContas " + nome + " não encontrado!");
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }
        }



    }
}
