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
    internal class Rom
    {
        public string Romnr { get; set; } = string.Empty;
        public int IDBygning { get; set; }
        public string Romtype { get; set; } = string.Empty;
        public int AntallArbeidsplasser { get; set; }
        public float NettoAreal { get; set; }
        public bool Cleandesk { get; set; } = false;
        public float Kvadratmeterpris { get; set; }

        public float beregnHusleie()
        {
            return this.Kvadratmeterpris * this.NettoAreal;
        }

        public float snittLeiePrArbPlass()
        {
            return this.beregnHusleie() / this.AntallArbeidsplasser;
        }

        public void LagreRomSql(Rom ORom)
        {
            try
            {
                string Tabell = "Romoversikt";
                string SqlKommando = $"INSERT INTO {Tabell} (Romnr, IDBygning, Romtype, AntallArbeidsplasser, Areal(Netto), Cleandesk, GjennomsnittligKvadratmeterpris) VALUES (@Romnr, @IDBygning, @Romtype, @AntallArbeidsplasser, @Areal(Netto), @Cleandesk, @GjennomsnittligKvadratmeterpris)";
                SqlController OSqlContoller = new SqlController();

                using (MySqlConnection Okontakt = OSqlContoller.EtablerSqlKontakt())
                {
                    Okontakt.Open();
                    using (MySqlCommand OSqlKommando = OSqlContoller.SqlKommandoEndringer(SqlKommando, Okontakt))
                    {
                        OSqlKommando.Parameters.AddWithValue("@Romnr", ORom.Romnr);
                        OSqlKommando.Parameters.AddWithValue("@IDBygning", ORom.IDBygning);
                        OSqlKommando.Parameters.AddWithValue("@Romtype", ORom.Romtype);
                        OSqlKommando.Parameters.AddWithValue("@AntallArbeidsplasser", ORom.AntallArbeidsplasser);
                        OSqlKommando.Parameters.AddWithValue("@Areal(Netto)", ORom.Romtype);
                        OSqlKommando.Parameters.AddWithValue("@Cleandesk", ORom.Cleandesk);
                        OSqlKommando.Parameters.AddWithValue("@GjennomsnittligKvadratmeterpris", ORom.Kvadratmeterpris);
                        OSqlKommando.ExecuteNonQuery();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void OppdatereRomSql(Rom ORom)
        {
            try
            {
                string Tabell = "Romoversikt";
                string SqlKommando = $"UPDATE {Tabell} SET IDBygning=@IDBygning, RomType=@RomType, AntallArbeidsplasser=@AntallArbeidsplasser, Areal(Netto)=@NettoAreal, Cleandesk=@Cleandesk, GjennomsnittligKvadratmeterpris=@GjennomsnittligKvadratmeterpris WHERE Romnr = @Romnr";
                SqlController OSqlContoller = new SqlController();

                using (MySqlConnection Okontakt = OSqlContoller.EtablerSqlKontakt())
                {
                    Okontakt.Open();
                    using (MySqlCommand OSqlKommando = OSqlContoller.SqlKommandoEndringer(SqlKommando, Okontakt))
                    {
                        OSqlKommando.Parameters.AddWithValue("@Romnr", ORom.Romnr);
                        OSqlKommando.Parameters.AddWithValue("@IDBygning", ORom.IDBygning);
                        OSqlKommando.Parameters.AddWithValue("@RomType", ORom.Romtype);
                        OSqlKommando.Parameters.AddWithValue("@AntallArbeidsplasser", ORom.AntallArbeidsplasser);
                        OSqlKommando.Parameters.AddWithValue("@NettoAreal", ORom.NettoAreal);
                        OSqlKommando.Parameters.AddWithValue("@Cleandesk", ORom.Cleandesk);
                        OSqlKommando.Parameters.AddWithValue("@GjennomsnittligKvadratmeterpris", ORom.Kvadratmeterpris);
                        OSqlKommando.ExecuteNonQuery();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SletteRomSql(Rom ORom)
        {
            try
            {
                string Tabell = "Romoversikt";
                string SqlKommando = $"DELETE FROM {Tabell} WHERE Romnr=@Romnr";
                SqlController OSqlController = new SqlController();

                using (MySqlConnection OKontakt = OSqlController.EtablerSqlKontakt())
                {
                    OKontakt.Open();
                    using (MySqlCommand OSqlKommando = OSqlController.SqlKommandoEndringer(SqlKommando, OKontakt))
                    {
                        OSqlKommando.Parameters.AddWithValue("@Romnr", ORom.Romnr);
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
