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
    internal class ListUnderKategoriUtstyr
    {
        private List<UnderKategoriUtstyr> OListUnderKategori = new List<UnderKategoriUtstyr>();

        public void HentSQLUnderKategori()
        {
            /*
             * Kobler seg til SQL databasen og laster ned Underkategoriene fra SQL til listen over OListUnderKategori
             */
            string Tabell = "UnderKategoriUtstyr";
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
                                UnderKategoriUtstyr OUnderKategori = new UnderKategoriUtstyr()
                                {
                                    IDUnderkategori = Oleser.GetInt32(0),
                                    IDKategori = Oleser.GetInt32(1),
                                    UnderKategori = Oleser.GetString(2),
                                };
                                OListUnderKategori.Add(OUnderKategori);
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
        public void LagreSqlUnderkategoriListe(string Underkategori, int IDKategori)
        {
            /*
             *Lagrer ny SLA Leverandør i SQL databasen og legger den til listen med underkategorier
             */
            if ((!string.IsNullOrEmpty(Underkategori)) && (!string.IsNullOrWhiteSpace(Underkategori)))
            {
                int NyIDUnderKategoriUtstyr = this.IDSisteElementetUnderkategoriListe() + 1;
                UnderKategoriUtstyr OUnderKategoriUtstyr = new UnderKategoriUtstyr();
                OUnderKategoriUtstyr.IDUnderkategori = NyIDUnderKategoriUtstyr;
                OUnderKategoriUtstyr.IDKategori = IDKategori;
                OUnderKategoriUtstyr.UnderKategori = Underkategori;

            }
            else
            {
                MessageBox.Show("Feltet for Underkategori må fylles ut");
            }
        }

        public UnderKategoriUtstyr SoekUnderKategoriUtstyrID(int IDUnderKategori)
        /*
         * Søker opp og finner OKategori ved hjelp av ID på bygning
         */
        {
            UnderKategoriUtstyr OUnderKategori = new UnderKategoriUtstyr();
            try
            {
                if (OListUnderKategori.Any())
                {
                    OUnderKategori = OListUnderKategori.Find(OListUnderKategorien => OListUnderKategorien.IDUnderkategori.Equals(IDUnderKategori));
                }
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.Message);
            }

            return OUnderKategori;
        }

        public UnderKategoriUtstyr SoekUnderkategoriVedNavn(string Underkategori)
        /*
         * Søker opp og finner objektet ved hjelp av navnet.
         */
        {
            UnderKategoriUtstyr OUnderKategori = new UnderKategoriUtstyr();
            try
            {
                if (OListUnderKategori.Any())
                {
                    OUnderKategori = OListUnderKategori.Find(OUnderKategorien => OUnderKategorien.UnderKategori.Equals(Underkategori));
                }
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.Message);
            }


            return OUnderKategori;
        }

        public UnderKategoriUtstyr SoekUnderKategoriIndeks(int Indeks)
        {
            return OListUnderKategori[Indeks];
        }

        public int MaxLengdeSLALeveandoerList()
        /*
         * Finner hvor mange Objekter som er i listen.
         */
        {
            return OListUnderKategori.Count;
        }

        public int IDSisteElementetUnderkategoriListe()
        /*
         * Henter ID på siste element i listen
         */
        {
            int Returverdi = 0;
            try
            {
                if (OListUnderKategori.Any())
                {
                    Returverdi = OListUnderKategori.LastOrDefault().IDUnderkategori;
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
