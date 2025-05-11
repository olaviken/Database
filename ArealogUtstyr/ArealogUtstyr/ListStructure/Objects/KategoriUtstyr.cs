
using ArealogUtstyr.Logic;
using MySql.Data.MySqlClient;
using System.Windows;

namespace ArealogUtstyr.ListStructure.Objects
{
    internal class KategoriUtstyr
    {
        public int IDKategori { get; set; }
        public string Kategori { get; set; } = string.Empty;

        public void LagreKategoriUtstyrSql(KategoriUtstyr OKategorien)
        {
            try
            {
                string Tabell = "KategoriUtstyr";
                string SqlKommando = $"INSERT INTO {Tabell} (IDKategori, Kategori) VALUES (@IDKategori, @Kategori)";
                SqlController OSqlContoller = new SqlController();

                using (MySqlConnection Okontakt = OSqlContoller.EtablerSqlKontakt())
                {
                    Okontakt.Open();
                    using (MySqlCommand OSqlKommando = OSqlContoller.SqlKommandoEndringer(SqlKommando, Okontakt))
                    {
                        OSqlKommando.Parameters.AddWithValue("@IDKategori", OKategorien.IDKategori);
                        OSqlKommando.Parameters.AddWithValue("@Kategori", OKategorien.Kategori);
                        OSqlKommando.ExecuteNonQuery();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void OppdatereKategoriSql(KategoriUtstyr Okategorien)
        {
            try
            {
                string Tabell = "KategoriUtstyr";
                string SqlKommando = $"UPDATE {Tabell} SET Kategori=@Kategori WHERE IDKategori = @IDKategori";
                SqlController OSqlContoller = new SqlController();

                using (MySqlConnection Okontakt = OSqlContoller.EtablerSqlKontakt())
                {
                    Okontakt.Open();
                    using (MySqlCommand OSqlKommando = OSqlContoller.SqlKommandoEndringer(SqlKommando, Okontakt))
                    {
                        OSqlKommando.Parameters.AddWithValue("@IDKategori", Okategorien.IDKategori);
                        OSqlKommando.Parameters.AddWithValue("@Kategori", Okategorien.Kategori);
                        OSqlKommando.ExecuteNonQuery();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SletteKategoriSql(KategoriUtstyr Okategorien)
        {
            try
            {
                string Tabell = "KategoriUtstyr";
                string SqlKommando = $"DELETE FROM {Tabell} WHERE IDKategori=@IDKategori";
                SqlController OSqlController = new SqlController();

                using (MySqlConnection OKontakt = OSqlController.EtablerSqlKontakt())
                {
                    OKontakt.Open();
                    using (MySqlCommand OSqlKommando = OSqlController.SqlKommandoEndringer(SqlKommando, OKontakt))
                    {
                        OSqlKommando.Parameters.AddWithValue("@IDKategori", Okategorien.IDKategori);
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
