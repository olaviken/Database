using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MySql.Data.MySqlClient;

using ArealogUtstyr.ListStructure.Objects;
using ArealogUtstyr.Logic;
using System;

namespace ArealogUtstyr.ListStructure.Lists
{
    internal class ListBygninger
    {
       /*
        * Funksjonene i denne Klassen. Hente og legge til data fra sql til listen til OListbygninger, oppdatere eksisterende data i databasen.
        */
        private List<Bygning> OListbygninger = new List<Bygning>();

        public void HentSQLbygning()
        {
          /*
           * Funksjonen får en SQL OKontakt streng og tabell. Kobler seg til SQL databasen og laster ned bygningene fra SQL til listen over OListbygninger
           */
           string Tabell = "Bygning";
           SqlController OSqlController = new SqlController();
            try
            {
                
                using (MySqlConnection OKontakt = OSqlController.EtablerSqlKontakt())
                {
                    OKontakt.Open();
                    string Hent = $"SELECT * FROM {Tabell}";
                    using (MySqlCommand OKommando = OSqlController.SqlKommandoHent(OKontakt,Hent))
                    {
                        using(MySqlDataReader Oleser = OKommando.ExecuteReader())
                        {
                            while (Oleser.Read())
                            {
                                Bygning OByggning = new Bygning
                                {
                                    IDBygning = Oleser.GetInt32(0),
                                    BygningNavn=Oleser.GetString(1)
                                };
                                OListbygninger.Add(OByggning);                         
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

        public void LagreSQLBygning(string BygningNavn)
        /*
         * Lagrer ny bygning først i SQL databasen, deretter lagres den i listen med bygninger. 
         */
        {
            if ((!string.IsNullOrEmpty(BygningNavn)) && (!string.IsNullOrWhiteSpace(BygningNavn)))
            {
                int NyIDBygning = this.IDSisteElementetBygninger() + 1;
                Bygning ONybygg = new Bygning();
                ONybygg.IDBygning = NyIDBygning;
                ONybygg.BygningNavn = BygningNavn;
                OListbygninger.Add(ONybygg);
                ONybygg.LagreByggSql(ONybygg);
            }
            else
            {
                MessageBox.Show("Feltet for navn på bygning er ikke fylt inn");
            }

        }

        public void OppdatereSQLbygning(int IDBygning, string BygningNavn)
        /*
         *Opdatere verdien i sql databasen og i listen med bygninger.
         */
        {
            if ((IDBygning >= 0) && (IDBygning <= this.IDSisteElementetBygninger()))
            {
                Bygning ONybygg = new Bygning();
                this.SoekBygningVedID(IDBygning).BygningNavn = BygningNavn;
                ONybygg.IDBygning = IDBygning;
                ONybygg.BygningNavn = BygningNavn;
                ONybygg.OppdatereByggSql(ONybygg);
            }
            else
            {
                MessageBox.Show("ID for bygninger er ugyldig. Kan ikke være 0, negativ eller større enn "+this.MaxLengdeBygninger());
            }

        }

        public void SletteSqlBygning(int IDBygning, string BygningNavn)
        {                        
            Bygning OSlettbygg = new Bygning();
            if((IDBygning >= 0)&&(IDBygning <= this.IDSisteElementetBygninger()))
            {
                OSlettbygg.IDBygning = IDBygning;
                OSlettbygg = this.SoekBygningVedID(IDBygning);
                OListbygninger.Remove(OSlettbygg);
                OSlettbygg.SletteByggSql(OSlettbygg);
            }
            else if(!string.IsNullOrEmpty(BygningNavn))
            {
                OSlettbygg.IDBygning = IDBygning;
                OSlettbygg = this.SoekBygningVedNavn(BygningNavn);
                OListbygninger.Remove(OSlettbygg);
                OSlettbygg.SletteByggSql(OSlettbygg);
            }
            else
            {
                MessageBox.Show("Det er ikke lagt inn gyldig ID på bygning eller bygning navn");
            }

        }

        public Bygning SoekBygningVedID(int Idbygning)
        /*
         * Søker opp og finner OBygg ved hjelp av ID på bygning
         */
        {
            Bygning OBygg = new Bygning();
            try
            {
                if (OListbygninger.Any())
                {
                    OBygg = OListbygninger.Find(OBygget => OBygget.IDBygning.Equals(Idbygning));
                }
            }
            catch(NullReferenceException e)
            {
                MessageBox.Show(e.Message);
            }
            
            return OBygg;            
        }

        public Bygning SoekBygningVedNavn(string bygningname)
        /*
         * Søker opp og finner OBygg ved hjelp av navnet på bygningen
         */
        {
            Bygning OBygg = new Bygning();
            try
            {
                if (OListbygninger.Any())
                {
                    OBygg = OListbygninger.Find(OBygget => OBygget.BygningNavn.Equals(bygningname));
                }
            }
            catch(NullReferenceException e)
            {
                MessageBox.Show(e.Message);
            }

            return OBygg;
        }

        public Bygning SoekBygningIndeks(int Indeks)
        {
            return OListbygninger[Indeks];
        }

        public int MaxLengdeBygninger()
        /*
         * Finner hvor mange OListbygninger som er i listen over OListbygninger.
         */
        {
            return OListbygninger.Count;
        }

        public int IDSisteElementetBygninger()
        /*
         * Henter ID på bygget som er siste element i listen
         */
        {
            int Returverdi=0;
            try
            {
                if (OListbygninger.Any())
                {
                    Returverdi = OListbygninger.LastOrDefault().IDBygning;
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
