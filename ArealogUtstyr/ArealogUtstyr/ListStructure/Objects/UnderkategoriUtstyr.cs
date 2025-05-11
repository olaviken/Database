using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ArealogUtstyr.Logic;
using MySql.Data.MySqlClient;

namespace ArealogUtstyr.ListStructure.Objects
{
    internal class UnderKategoriUtstyr
    {
        public int IDUnderkategori { get; set; }
        public int IDKategori { get; set; }
        public string UnderKategori { get; set; } = string.Empty;

        public void LagreUnderKategoriSql(UnderKategoriUtstyr OUnderKategorien)
        {
            try
            {
                string Tabell = "UnderKategoriUtstyr";
                string SqlKommando = $"INSERT INTO {Tabell} (IDUnderKategori, IDKategori, UnderKategori) VALUES (@IDUnderKategori, @IDKategori, @UnderKategori)";
                SqlController OSqlContoller = new SqlController();

                using (MySqlConnection Okontakt = OSqlContoller.EtablerSqlKontakt())
                {
                    Okontakt.Open();
                    using (MySqlCommand OSqlKommando = OSqlContoller.SqlKommandoEndringer(SqlKommando, Okontakt))
                    {
                        OSqlKommando.Parameters.AddWithValue("@IDUnderKategori", OUnderKategorien.IDUnderkategori);
                        OSqlKommando.Parameters.AddWithValue("@IDKategori", OUnderKategorien.IDKategori);
                        OSqlKommando.Parameters.AddWithValue("@UnderKategori", OUnderKategorien.UnderKategori);
                        OSqlKommando.ExecuteNonQuery();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void OppdatereUnderKategoriSql(UnderKategoriUtstyr OUnderkategorien)
        {
            try
            {
                string Tabell = "UnderKategoriUtstyr";
                string SqlKommando = $"UPDATE {Tabell} SET IDKategori=@IDKategori, UnderKategori=@UnderKategori WHERE IDKategori = @IDKategori";
                SqlController OSqlContoller = new SqlController();

                using (MySqlConnection Okontakt = OSqlContoller.EtablerSqlKontakt())
                {
                    Okontakt.Open();
                    using (MySqlCommand OSqlKommando = OSqlContoller.SqlKommandoEndringer(SqlKommando, Okontakt))
                    {
                        OSqlKommando.Parameters.AddWithValue("@IDKategori", OUnderkategorien.IDUnderkategori);
                        OSqlKommando.Parameters.AddWithValue("@IDKategori", OUnderkategorien.IDKategori);
                        OSqlKommando.Parameters.AddWithValue("@Underkategori", OUnderkategorien.UnderKategori);
                        OSqlKommando.ExecuteNonQuery();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SletteUnderKategoriSql(UnderKategoriUtstyr OUnderkategorien)
        {
            try
            {
                string Tabell = "UnderKategoriUtstyr";
                string SqlKommando = $"DELETE FROM {Tabell} WHERE IDKategori=@IDUnderKategori";
                SqlController OSqlController = new SqlController();

                using (MySqlConnection OKontakt = OSqlController.EtablerSqlKontakt())
                {
                    OKontakt.Open();
                    using (MySqlCommand OSqlKommando = OSqlController.SqlKommandoEndringer(SqlKommando, OKontakt))
                    {
                        OSqlKommando.Parameters.AddWithValue("@IDUnderKategori", OUnderkategorien.IDUnderkategori);
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
