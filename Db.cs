using Carrello.Modelli;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrello
{
    internal class Db
    {
        private SqlConnection GetConnection()
        {
            var connenctionString = "Server=GRETA-DESKTOP\\SQLEXPRESS;Database=Carrello;Trusted_Connection=True;";
            SqlConnection connection = new SqlConnection(connenctionString);
            connection.Open();
            return connection;
        }
       
        public void ConfermaOrdine(int idSessione, decimal totale)
        {
            var connection = GetConnection();
            SqlCommand cmd = connection.CreateCommand();
            SqlTransaction tran = connection.BeginTransaction();
            cmd.Connection = connection;
            cmd.Transaction = tran;
            try
            {
                
                cmd.CommandText = Resource1.CreaOrdine;
                cmd.Parameters.AddWithValue("@IdSessione", idSessione);
                cmd.Parameters.AddWithValue("@Totale", totale);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                cmd.CommandText = Resource1.SvuotaCarrello;
                cmd.Parameters.AddWithValue("@IdSessione", idSessione);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                idSessione = 0;
                cmd.CommandText = Resource1.RimuoviOrdini;
                cmd.Parameters.AddWithValue("@IdSessione", idSessione);
                cmd.ExecuteNonQuery();
                
                tran.Commit();

            }
            catch (Exception)
            {
                tran.Rollback();
                

                throw;
            }
                
                
                }


        public bool HaSessione(string IndirizzoIp)
        {
            var connection = GetConnection();
            var retVal = false;
            SqlDataReader sqlDataReader = null;
            try
            {
                var cmd = new SqlCommand(Resource1.HaSessione, connection);
                cmd.Parameters.AddWithValue("@IndirizzoIp", IndirizzoIp);
                sqlDataReader = cmd.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    var retCount = sqlDataReader.GetInt32(0);
                    if(retCount !=0)
                        retVal = true;
                }


            }
            catch (Exception)
            {
                throw;
             
            }
            return retVal;
        }

        public void CreaOrdine(int idSessione, decimal totale)
        {
            var connection = GetConnection();
            try
            {
                var cmd = new SqlCommand(Resource1.CreaOrdine, connection);
                cmd.Parameters.AddWithValue("@IdSessione", idSessione);
                cmd.Parameters.AddWithValue("@Totale", totale);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public Carrello.Modelli.Carrello GetCarrello(int idSessione)
        {
            var connection = GetConnection();
           
            SqlDataReader sqlDataReader = null;
            try
            {
                SqlCommand cmd = new SqlCommand(Resource1.GetCarrello, connection);
                cmd.Parameters.AddWithValue("@IdSessione", idSessione);
               
                sqlDataReader = cmd.ExecuteReader();
                var carrello = new Carrello.Modelli.Carrello();
                carrello.Articoli = new List<Articoli>();
                while (sqlDataReader.Read())
                {

                    
                    var articolo = new Articoli();  
                    carrello.Id = sqlDataReader.GetInt32(0);
                    carrello.IdSessione = sqlDataReader.GetInt32(5);
                    articolo.Descrizione = sqlDataReader.GetString(2);
                    articolo.Prezzo = sqlDataReader.GetDecimal(3);
                   carrello.Articoli.Add(articolo);

                }

                return carrello;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void CreaSessione (string IndirizzoIp) 
        {
            var connection = GetConnection ();
            try
            {
                var cmd = new SqlCommand(Resource1.CreaSessione, connection);
                cmd.Parameters.AddWithValue("@DataOra",DateTime.Now );
                cmd.Parameters.AddWithValue("@IndirizzoIp", IndirizzoIp );
                cmd.ExecuteNonQuery();
                connection.Close();

            }
            catch
            {

            }           
        }

        public Sessione GetSessione(string IndirizzoIp)
        {
            var connection = GetConnection();
            var retsessione = new Sessione();
            SqlDataReader sqlDataReader = null;
            try
            {
                SqlCommand cmd = new SqlCommand(Resource1.GetSessione, connection);
                cmd.Parameters.AddWithValue("@IndirizzoIp", IndirizzoIp);
                sqlDataReader =cmd.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    
                    retsessione.Id= sqlDataReader.GetInt32(0);
                    retsessione.IndirizzoIp = sqlDataReader.GetString(2);
                   
                }
                
                return retsessione;
            }
            catch (Exception)
            {

                throw;
            }
            return retsessione;
        }


        public void InserisciArticolo(int sessione, int idArticolo)
        {
            var connection = GetConnection();
            try
            {
                var cmd = new SqlCommand(Resource1.InserisciArticolo, connection);
                cmd.Parameters.AddWithValue("@IdSessione", sessione);
                cmd.Parameters.AddWithValue("@IdArticolo", idArticolo);
                cmd.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Articoli> GetArticoli()
        {
            var retVal = new List<Articoli>();  
            var connection = GetConnection();
            SqlDataReader sqlDataReader = null;
            try
            {
                SqlCommand cmd = new SqlCommand(Resource1.LeggiArticoli, connection);
                sqlDataReader = cmd.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    var articolo = new Articoli();
                    articolo.Id = sqlDataReader.GetInt32(0);
                    articolo.Descrizione=sqlDataReader.GetString(2);
                    articolo.Prezzo =sqlDataReader.GetDecimal(3);
                    retVal.Add(articolo);
                }

                return retVal;
            }
            catch (Exception)
            {

                throw;
            }

            return retVal;

        }

      

        public void ScriviDati()
        {


            var connection = GetConnection();

            try
            {
                var insertQuery = "";
                var cmd = new SqlCommand(insertQuery, connection);
                cmd.Parameters.AddWithValue("@Id", 4);
                cmd.Parameters.AddWithValue("@Descrizione", "Test 4");
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {

            }
        }

        public List<Sessione> UpdateDati()
        {
            var connection = GetConnection();
            SqlDataReader sqlDataReader = null;
            try
            {
                var items = new List<Sessione>();
                SqlCommand cmd;
                cmd = connection.CreateCommand();
                cmd.CommandText = "";
                cmd.CommandType = System.Data.CommandType.Text;
                sqlDataReader = cmd.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    var item = new Sessione();
                    item.Id = sqlDataReader.GetInt32(0);
                    item.DataOra = sqlDataReader.GetDateTime(1);
                    item.IndirizzoIp = sqlDataReader.GetString(2);
                    items.Add(item);
                }
                return items;

            }

            catch (Exception e)
            {
                Console.WriteLine($"Processing failed: {e.Message}");
            }
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                    sqlDataReader = null;
                }
                if (connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            return new List<Sessione>();



        }
        public void CancellaDati()
        {
            var connection = GetConnection();

            try
            {
                var deleteQuery = "";
                var cmd = new SqlCommand(deleteQuery, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {

            }
        }
    }
}
