using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArealogUtstyr.Logic;
using MySql.Data.MySqlClient;
using System.Windows.Data;
using System.Windows;

namespace ArealogUtstyr.ListStructure.Objects
{
    internal class Bygning
    {
        public int IDBygning { get; set; }
        public string BygningNavn { get; set; } = string.Empty;

        public void LagreByggSql(Bygning OBygning)
        {
            try
            {
                string Tabell = "Bygning";
                string SqlKommando = $"INSERT INTO {Tabell} (IDBygning, BygningNavn) VALUES (@ID, @Name)";
                SqlController OSqlContoller = new SqlController();

                using (MySqlConnection Okontakt = OSqlContoller.EtablerSqlKontakt())
                {
                    Okontakt.Open();
                    using (MySqlCommand OSqlKommando = OSqlContoller.SqlKommandoEndringer(SqlKommando, Okontakt))
                    {
                        OSqlKommando.Parameters.AddWithValue("@ID", OBygning.IDBygning);
                        OSqlKommando.Parameters.AddWithValue("@Name", OBygning.BygningNavn);
                        OSqlKommando.ExecuteNonQuery();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void OppdatereByggSql(Bygning OBygning)
        {
            try
            {
                string Tabell = "Bygning";
                string SqlKommando = $"UPDATE {Tabell} SET BygningNavn=@Name WHERE IDBygning = @ID";
                SqlController OSqlContoller = new SqlController();

                using (MySqlConnection Okontakt = OSqlContoller.EtablerSqlKontakt())
                {
                    Okontakt.Open();
                    using (MySqlCommand OSqlKommando = OSqlContoller.SqlKommandoEndringer(SqlKommando, Okontakt))
                    {
                        OSqlKommando.Parameters.AddWithValue("@ID", OBygning.IDBygning);
                        OSqlKommando.Parameters.AddWithValue("@Name", OBygning.BygningNavn);
                        OSqlKommando.ExecuteNonQuery();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SletteByggSql(Bygning OBygning)
        {
            try
            {
                string Tabell = "Bygning";
                string SqlKommando = $"DELETE FROM {Tabell} WHERE IDBygning=@ID";
                SqlController OSqlController = new SqlController();

                using (MySqlConnection OKontakt = OSqlController.EtablerSqlKontakt())
                {
                    OKontakt.Open();
                    using (MySqlCommand OSqlKommando = OSqlController.SqlKommandoEndringer(SqlKommando, OKontakt))
                    {
                        OSqlKommando.Parameters.AddWithValue("@ID", OBygning.IDBygning);
                        OSqlKommando.ExecuteNonQuery();
                    }
                }
            }
            catch(MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
