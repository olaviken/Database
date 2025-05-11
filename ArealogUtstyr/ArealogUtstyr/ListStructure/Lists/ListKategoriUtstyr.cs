using ArealogUtstyr.ListStructure.Objects;
using ArealogUtstyr.Logic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ArealogUtstyr.ListStructure.Lists
{
    internal class ListKategoriUtstyr
    {
        private List<KategoriUtstyr> OListKategoriUtstyr = new List<KategoriUtstyr>();

        public void HentSQLKategoriUtstyr()
        {
            /*
             * Kobler seg til SQL databasen og laster ned bygningene fra SQL til listen over OListbygninger
             */
            string Tabell = "KategoriUtstyr";
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
                                KategoriUtstyr OKategoriUtstyr = new KategoriUtstyr
                                {
                                    IDKategori = Oleser.GetInt32(0),
                                    Kategori = Oleser.GetString(1)
                                };
                                OListKategoriUtstyr.Add(OKategoriUtstyr);
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

        public void LagreSQLKategoriUtstyrListe(string KategoriUtstyr)
        /*
         * Lagrer ny bygning først i SQL databasen, deretter lagres den i listen med bygninger. 
         */
        {
            if ((!string.IsNullOrEmpty(KategoriUtstyr)) && (!string.IsNullOrWhiteSpace(KategoriUtstyr)))
            {
                int NyIDKategori = this.IDSisteElementetKategoriListe() + 1;
                KategoriUtstyr OKategori = new KategoriUtstyr();
                OKategori.IDKategori = NyIDKategori;
                OKategori.Kategori = KategoriUtstyr;
                OListKategoriUtstyr.Add(OKategori);
                OKategori.LagreKategoriUtstyrSql(OKategori);
            }
            else
            {
                MessageBox.Show("Feltet for navn er ikke fylt ut");
            }
         
        }
        public void OppdatereSQLKategoriUtstyrListe(int IDKategori, string Kategori)
        /*
         *Opdatere verdien i sql databasen og i listen med bygninger.
         */
        {
            if ((IDKategori >= 0) && (IDKategori <= this.IDSisteElementetKategoriListe()))
            {
                KategoriUtstyr OKategori = new KategoriUtstyr();
                this.SoekKategoriVedID(IDKategori).Kategori = Kategori;
                OKategori.IDKategori = IDKategori;
                OKategori.Kategori = Kategori;
                OKategori.OppdatereKategoriSql(OKategori);
            }
            else
            {
                MessageBox.Show("ID for kategori er ugyldig. Kan ikke være 0, negativ eller større enn "+ this.MaxLengdeKategoriList());
            }

        }

        public void SletteSqlKategoriUtstyrListe(int IDKategori, string Kategori)
        {
            KategoriUtstyr OKategori = new KategoriUtstyr();
            if((IDKategori >= 0) && (IDKategori <=this.IDSisteElementetKategoriListe()))
            {
                OKategori = this.SoekKategoriVedID(IDKategori);
                OListKategoriUtstyr.Remove(OKategori);
                OKategori.SletteKategoriSql(OKategori);
            }
            else if (!string.IsNullOrEmpty(Kategori))
            {
                OKategori = this.SoekKategoriVedNavn(Kategori);
                OListKategoriUtstyr.Remove(OKategori);
                OKategori.SletteKategoriSql(OKategori);
            }
            else
            {
                MessageBox.Show("Det er ikke lagt inn gyldig ID på kategori eller kategori navn, eller navnet er skrevet feil.");
            }
        }

        public KategoriUtstyr SoekKategoriVedID(int IDKategori)
        /*
         * Søker opp og finner OKategori ved hjelp av ID på bygning
         */
        {
            KategoriUtstyr OKategori = new KategoriUtstyr();
            OKategori = OListKategoriUtstyr.Find(OKategorien => OKategorien.IDKategori.Equals(IDKategori));
            return OKategori;
        }

        public KategoriUtstyr SoekKategoriVedNavn(string Kategorien)
        /*
         * Søker opp og finner objektet ved hjelp av navnet.
         */
        {
            KategoriUtstyr Okategori = new KategoriUtstyr();
            Okategori = OListKategoriUtstyr.Find(OKategorien => OKategorien.Kategori.Equals(Kategorien));
            return Okategori;
        }

        public KategoriUtstyr SoekKategoriListIndeks(int Indeks)
        {
            return OListKategoriUtstyr[Indeks];
        }

        public int MaxLengdeKategoriList()
        /*
         * Finner hvor mange Objekter som er i listen.
         */
        {
            return OListKategoriUtstyr.Count;
        }

        public int IDSisteElementetKategoriListe()
        /*
         * Henter ID på siste element i listen
         */
        {
            int Returverdi=0;
            try
            {
                if (!(OListKategoriUtstyr.LastOrDefault() == null))
                {
                    Returverdi = OListKategoriUtstyr.LastOrDefault().IDKategori;
                }
            }
            catch(NullReferenceException e)
            {
                MessageBox.Show(e.Message);
            }          
            return Returverdi;
        }
    }
}
