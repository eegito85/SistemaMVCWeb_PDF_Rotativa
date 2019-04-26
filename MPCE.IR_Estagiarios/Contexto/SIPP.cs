using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MPCE.IR_Estagiarios.Contexto
{
    public class SIPP
    {

        public decimal CalcularRendimentosTributaveis(int anoRetencao, int matricula, string conexao)
        {
            decimal rendimentoTributavel = 0;
            string sQuery = "";
            int digitoVerificadorEstgiario = matricula / 10000000;
            string resultado = "";

            if(digitoVerificadorEstgiario == 8)
            {
                try
                {
                    sQuery = "SELECT"
                       + " SUM(VL_CODFIN) AS rendimentos"
                       + " FROM SIPP..HSTFOLHA"
                       + " WHERE CD_MATRIC = @MATRICULA"
                       + " AND CD_CODFIN IN(197, 551)"
                       + " AND(CD_FOLHA = '" + (anoRetencao - 1).ToString() + "12M' "
                       + " OR SUBSTRING(CD_FOLHA, 1, 4) = '" + anoRetencao.ToString() + "')";

                    if (anoRetencao != 2018)
                    {
                        sQuery = sQuery + "AND CD_FOLHA <> '" + anoRetencao +"12M'";
                    }

                    SqlConnection sqlConnection1 = new SqlConnection(conexao);
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;

                    cmd.CommandText = sQuery;
                    cmd.Parameters.AddWithValue("@MATRICULA", matricula);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        resultado = reader["rendimentos"].ToString();
                    }

                    sqlConnection1.Close();

                    if (resultado != "") { rendimentoTributavel = Convert.ToDecimal(resultado); }
                }
                catch (Exception e) { throw; }
            }

            return rendimentoTributavel;
        }


        public decimal CalcularAuxilioTransporte(int anoRetencao, int matricula, string conexao)
        {
            decimal auxilioTransporte = 0;
            string sQuery = "";
            int digitoVerificadorEstgiario = matricula / 10000000;
            string resultado = "";

            if (digitoVerificadorEstgiario == 8)
            {
                try
                {
                    sQuery = "SELECT"
                       + " SUM(VL_CODFIN) AS auxTransporte"
                       + " FROM HSTFOLHA"
                       + " WHERE CD_MATRIC = @MATRICULA"
                       + " AND CD_CODFIN IN(249, 110)"
                       + " AND(CD_FOLHA = '" + (anoRetencao - 1).ToString() + "12M' "
                       + " OR SUBSTRING(CD_FOLHA, 1, 4) = '" + anoRetencao.ToString() + "')";

                    if (anoRetencao != 2018)
                    {
                        sQuery = sQuery + "AND CD_FOLHA <> '" + anoRetencao + "12M'";
                    }

                    SqlConnection sqlConnection1 = new SqlConnection(conexao);
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;

                    cmd.CommandText = sQuery;
                    cmd.Parameters.AddWithValue("@MATRICULA", matricula);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        resultado = reader["auxTransporte"].ToString();
                    }

                    sqlConnection1.Close();

                    if (resultado != "") { auxilioTransporte = Convert.ToDecimal(resultado); }
                }
                catch (Exception e) { throw; }
            }

            return auxilioTransporte;
        }

        public string RetornarNomeEstagiario(int matricula, string conexao)
        {
            string nome = "";
            string sQuery = "";
            int digitoVerificadorEstgiario = matricula / 10000000;

            if (digitoVerificadorEstgiario == 8)
            {
                try
                {
                    sQuery = "SELECT"
                       + " NM_SERVIDOR AS nome"
                       + " FROM SIRH..SERVIDOR"
                       + " WHERE CD_MATRIC = @MATRICULA";

                    SqlConnection sqlConnection1 = new SqlConnection(conexao);
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;

                    cmd.CommandText = sQuery;
                    cmd.Parameters.AddWithValue("@MATRICULA", matricula);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        nome = reader["nome"].ToString();
                    }

                    sqlConnection1.Close();
                }
                catch (Exception e) { throw; }
            }

            return nome;
        }

        public string RetornarCPFEstagiario(int matricula, string conexao)
        {
            string cpf = "";
            string sQuery = "";
            int digitoVerificadorEstgiario = matricula / 10000000;

            if (digitoVerificadorEstgiario == 8)
            {
                try
                {
                    sQuery = "SELECT"
                       + " NR_CPF AS cpf"
                       + " FROM SIRH..SERVIDOR"
                       + " WHERE CD_MATRIC = @MATRICULA";

                    SqlConnection sqlConnection1 = new SqlConnection(conexao);
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;

                    cmd.CommandText = sQuery;
                    cmd.Parameters.AddWithValue("@MATRICULA", matricula);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        cpf = reader["cpf"].ToString();
                    }

                    sqlConnection1.Close();
                }
                catch (Exception e) { throw; }
            }

            return cpf;
        }
    }
}
