using ArealogUtstyr.Logic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArealogUtstyr.ListStructure.Objects
{
    internal class SLALeverandoer
    {
        public int IDSLA { get; set; }
        public string SLALeverandoerNavn { get; set; } = string.Empty;
        public string Kontaktperson { get; set; } = string.Empty;
        public string Epost { get; set; } = string.Empty;

        public void LagreSLALeverandoerSql(SLALeverandoer OSLALeverandoer)
        {
            try
            {
                string Tabell = "SLALeverandoer";
                string SqlKommando = $"INSERT INTO {Tabell} (IDSLA, SLALeverandoer, Kontaktperson, Epost) VALUES (@IDSLA, @SLALeverandoer, @Kontaktperson, @Epost)";
                SqlController OSqlContoller = new SqlController();

                using (MySqlConnection Okontakt = OSqlContoller.EtablerSqlKontakt())
                {
                    Okontakt.Open();
                    using (MySqlCommand OSqlKommando = OSqlContoller.SqlKommandoEndringer(SqlKommando, Okontakt))
                    {
                        OSqlKommando.Parameters.AddWithValue("@IDSLA", OSLALeverandoer.IDSLA);
                        OSqlKommando.Parameters.AddWithValue("@SLALeverandoer", OSLALeverandoer.SLALeverandoerNavn);
                        OSqlKommando.Parameters.AddWithValue("@Kontaktperson", OSLALeverandoer.Kontaktperson);
                        OSqlKommando.Parameters.AddWithValue("@Epost", OSLALeverandoer.Epost);
                        OSqlKommando.ExecuteNonQuery();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void OppdatereSLALeverandoerSql(SLALeverandoer OSLALeverandoer)
        {
            try
            {
                string Tabell = "SLALeverandoer";
                string SqlKommando = $"UPDATE {Tabell} SET SLALeverandoer=@SLALeverandoer, Kontaktperson=@Kontaktperson, Epost=@Epost WHERE IDSLA = @IDSLA";
                SqlController OSqlContoller = new SqlController();

                using (MySqlConnection Okontakt = OSqlContoller.EtablerSqlKontakt())
                {
                    Okontakt.Open();
                    using (MySqlCommand OSqlKommando = OSqlContoller.SqlKommandoEndringer(SqlKommando, Okontakt))
                    {
                        OSqlKommando.Parameters.AddWithValue("@IDSLA", OSLALeverandoer.IDSLA);
                        OSqlKommando.Parameters.AddWithValue("@SLALeverandoer", OSLALeverandoer.SLALeverandoerNavn);
                        OSqlKommando.Parameters.AddWithValue("@Kontaktperson", OSLALeverandoer.Kontaktperson);
                        OSqlKommando.Parameters.AddWithValue("@Epost", OSLALeverandoer.Epost);
                        OSqlKommando.ExecuteNonQuery();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SletteSLALeverandoerSql(SLALeverandoer OSLALeverandoer)
        {
            try
            {
                string Tabell = "SLALeverandoer";
                string SqlKommando = $"DELETE FROM {Tabell} WHERE IDSLA=@IDSLA";
                SqlController OSqlController = new SqlController();

                using (MySqlConnection OKontakt = OSqlController.EtablerSqlKontakt())
                {
                    OKontakt.Open();
                    using (MySqlCommand OSqlKommando = OSqlController.SqlKommandoEndringer(SqlKommando, OKontakt))
                    {
                        OSqlKommando.Parameters.AddWithValue("@IDSLA", OSLALeverandoer.IDSLA);
                        OSqlKommando.ExecuteNonQuery();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
