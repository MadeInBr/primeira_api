using primeira_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data;
namespace primeira_api.Controllers
{
    public class MotoristasController : ApiController
    {
//---------------------------------Class de coneção com BD---------------------------------
        class Conexao
        {
            public Conexao()//metodo construtor
            {
                this.String_conexao = @"Data Source=NEPRE0012\SQLEXPRESS;Initial Catalog=EDDB000;Integrated Security=True";
            }

            public string String_conexao { get; private set; }

            public DataTable mysql_data_adapter(string Query_string)
            {
                DataTable dtb = new DataTable();

                SqlConnection conexao = new SqlConnection();
                conexao.ConnectionString = this.String_conexao;
                try
                {
                    conexao.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(Query_string, conexao);

                    adapter.Fill(dtb);

                    conexao.Dispose();
                    adapter.Dispose();
                }
                catch
                {
                }
                finally
                {
                    conexao.Close();
                }
                return dtb;
            }
            internal DataTable mysql_data_adapter(object sQL)
            {
                throw new NotImplementedException();
            }
            public bool execute_non_query(string Query_string)//serve para usar insert, updata e delete
            {
                try
                {
                    SqlConnection conexao = new SqlConnection();
                    conexao.ConnectionString = this.String_conexao;
                    conexao.Open();

                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = Query_string;
                    comando.Connection = conexao;
                    comando.ExecuteNonQuery();

                    conexao.Dispose();
                    comando.Dispose();

                    return true;
                }
                catch (Exception ex)
                {
                    Exception e = ex;

                    return false;
                }
                finally
                {
                }
            }
        }
//---------------------------------Class de coneção com BD---------------------------------


        private static List<Motorista> motoristas = new List<Motorista>();//criando o primeiro método de consulta
        
        public DataTable Get(string id) //busca um ID
        {
            Conexao con = new Conexao();
            string SQL = " Select m.id_motorista, m.nome, m.cpf, m.email " +
            " from MOTORISTA as m left join MOTORISTA_LOGIN as ml " +
            " on m.id_motorista = ml.id_motorista " +
            " where m.id_motorista like '" + id + "' ";

            DataTable dtt = new DataTable();
            dtt = con.mysql_data_adapter(SQL);///para busca -- select
           
            return dtt;
         }
        public void Post(string id, string Email) // insere um ID
        {
            if (!string.IsNullOrEmpty(id))
            {
                //motoristas.Add(new Motorista(id));
                string SQL = " Update MOTORISTA set email = '" + Email + "' where id_motorista = '" + id + "'";
                Conexao con = new Conexao();
                con.execute_non_query(SQL);
            
            }
        }
        public void Delete(string nome) // deleta um ID
        {
            motoristas.RemoveAt(motoristas.IndexOf(motoristas.First(x => x.Nome.Equals(nome))));
        }
    }
}
