using ArealogUtstyr.ListStructure.Objects;
using ArealogUtstyr.Logic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArealogUtstyr.ListStructure.Lists
{
    internal class ListRom
    {
        private List<Rom> OListRom = new List<Rom>();


        public void HentSQLRomoversikt()
        {
            /*
             * Kobler seg til SQL databasen og laster ned SLALeverandørene fra SQL til listen over OListSLALeverandoer
             */
            string Tabell = "Romoversikt";
            SqlController OSqlController = new SqlController();
            try
            {

                using (MySqlConnection OKontakt = OSqlController.EtablerSqlKontakt())
                {
                    OKontakt.Open();
                    string Hent = $"SELECT * FROM {Tabell}";
                    using (MySqlCommand OKommando = OSqlController.SqlKommandoHent(OKontakt, Hent))
                    {
                        using (MySqlDataReader Oleser = OKommando.ExecuteReader())
                        {
                            while (Oleser.Read())
                            {
                                Rom ORom = new Rom
                                {
                                    Romnr = Oleser.GetString(0),
                                    IDBygning = Oleser.GetInt32(1),
                                    Romtype = Oleser.GetString(2),
                                    AntallArbeidsplasser = Oleser.GetInt32(3),
                                    NettoAreal = Oleser.GetFloat(4),
                                    Cleandesk = Oleser.GetBoolean(5),
                                    Kvadratmeterpris= Oleser.GetFloat(6),
                                };
                                OListRom.Add(ORom);
                            }
                        }
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LagreSQLRomoversiktList(string SLALeverandoeren, string Kontaktperson, string Epost)
        {
            /*
             *Lagrer ny SLA Leverandør i SQL databasen og legger den til listen med SLA leverandører
             */
            if ((!string.IsNullOrEmpty(SLALeverandoeren)) && (!string.IsNullOrWhiteSpace(SLALeverandoeren)))
            {
                int NyIDSLA = this.IDSisteElementetRom() + 1;
                Rom ORom = new Rom();
                ORom.IDSLA = NyIDSLA;
                ORom.SLALeverandoerNavn = SLALeverandoeren;
                ORom.Kontaktperson = Kontaktperson;
                ORom.Epost = Epost;
                OListRom.Add(ORom);
                ORom.LagreSLALeverandoerSql(ORom);
            }
            else
            {
                MessageBox.Show("Feltet for SLA leverandør må fylles ut");
            }
        }

        public string IDSisteElementetRom()
        /*
         * Henter ID på siste element i listen
         */
        {
            string Returverdi = string.Empty;
            try
            {
                if (OListRom.Any())
                {
                    Returverdi = OListRom.LastOrDefault().Romnr;
                }
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.Message);
            }
            return Returverdi;
        }
    }
}
