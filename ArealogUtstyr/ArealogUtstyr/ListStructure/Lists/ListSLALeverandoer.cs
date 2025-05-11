using ArealogUtstyr.ListStructure.Objects;
using ArealogUtstyr.Logic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArealogUtstyr.ListStructure.Lists
{
    internal class ListSLALeverandoer
    {
        private List<SLALeverandoer> OListSLALeverandoer = new List<SLALeverandoer>();

        public void HentSQLSLALeverandoer()
        {
            /*
             * Kobler seg til SQL databasen og laster ned SLALeverandørene fra SQL til listen over OListSLALeverandoer
             */
            string Tabell = "SLALeverandoer";
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
                                SLALeverandoer OSLALeverandoer = new SLALeverandoer
                                {
                                    IDSLA = Oleser.GetInt32(0),
                                    SLALeverandoerNavn = Oleser.GetString(1),
                                    Kontaktperson = Oleser.GetString(2),
                                    Epost= Oleser.GetString(3)
                                };
                                OListSLALeverandoer.Add(OSLALeverandoer);
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

        public void LagreSQLSLALeverandoerList(string SLALeverandoeren, string Kontaktperson, string Epost)
        {
            /*
             *Lagrer ny SLA Leverandør i SQL databasen og legger den til listen med SLA leverandører
             */
            if ((!string.IsNullOrEmpty(SLALeverandoeren)) && (!string.IsNullOrWhiteSpace(SLALeverandoeren)))
            {
                int NyIDSLA = this.IDSisteElementetSLALeverandoerListe()+1;
                SLALeverandoer OSLALeverandoer = new SLALeverandoer();
                OSLALeverandoer.IDSLA = NyIDSLA;
                OSLALeverandoer.SLALeverandoerNavn = SLALeverandoeren;
                OSLALeverandoer.Kontaktperson= Kontaktperson;
                OSLALeverandoer.Epost= Epost;
                OListSLALeverandoer.Add(OSLALeverandoer);
                OSLALeverandoer.LagreSLALeverandoerSql(OSLALeverandoer);
            }
            else
            {
                MessageBox.Show("Feltet for SLA leverandør må fylles ut");
            }
        }

        public void OppdatereSQLSLALeverandorer(int IDSLA, string SLALeverandoeren, string Kontaktperson, string Epost)
        {
            /*
             *Oppdatere verdiene i SQL databasen og listen med SLA leverandører.
             */
            if((IDSLA >= 0)&&(IDSLA <= this.IDSisteElementetSLALeverandoerListe()))
            {
                SLALeverandoer OSLALeverandoeren = new SLALeverandoer();
                OSLALeverandoeren = this.SoekSLALeverandoerVedID(IDSLA);

                if((!string.IsNullOrEmpty(SLALeverandoeren)) && (!string.IsNullOrWhiteSpace(SLALeverandoeren))&&(!SLALeverandoeren.ToLower().Equals(OSLALeverandoeren.SLALeverandoerNavn.ToLower())))
                {
                    OSLALeverandoeren.SLALeverandoerNavn = SLALeverandoeren;
                    this.SoekSLALeverandoerVedID(IDSLA).SLALeverandoerNavn = SLALeverandoeren;
                }
                if((!string.IsNullOrEmpty(Kontaktperson)) && (!string.IsNullOrWhiteSpace(Kontaktperson)) && (!Kontaktperson.ToLower().Equals(OSLALeverandoeren.Kontaktperson.ToLower())))
                {
                    OSLALeverandoeren.Kontaktperson = Kontaktperson;
                    this.SoekSLALeverandoerVedID(IDSLA).Kontaktperson = Kontaktperson;
                }
                if ((!string.IsNullOrEmpty(Epost)) && (!string.IsNullOrWhiteSpace(Epost)) && (!Epost.ToLower().Equals(OSLALeverandoeren.Epost.ToLower())))
                {
                    OSLALeverandoeren.Epost = Epost;
                    this.SoekSLALeverandoerVedID(IDSLA).Epost = Epost;
                }              
                OSLALeverandoeren.OppdatereSLALeverandoerSql(OSLALeverandoeren);
            }
            else
            {
                MessageBox.Show("ID for SLA leverandør er ugyldig. Kan ikke være 0, negativ eller større enn " + this.MaxLengdeSLALeveandoerList());
            }

        }

        public void SletteSQLSLALeverandoerer(int IDSLA, string SLALeverandoeren)
        {
            SLALeverandoer OSLALeverandoer = new SLALeverandoer();
            if((IDSLA >= 0)&& (IDSLA <= this.IDSisteElementetSLALeverandoerListe()))
            {
                OSLALeverandoer = this.SoekSLALeverandoerVedID(IDSLA);
                OListSLALeverandoer.Remove(OSLALeverandoer);
                OSLALeverandoer.SletteSLALeverandoerSql(OSLALeverandoer);
            }
            else if (!string.IsNullOrEmpty(SLALeverandoeren))
            {
                MessageBox.Show("Det er ikke lagt inn gyldig SLA ID eller kategori navn, navnet kan være skrevet feil også.");
            }
        }

        public SLALeverandoer SoekSLALeverandoerVedID(int IDSLA)
        /*
         * Søker opp og finner OKategori ved hjelp av ID på bygning
         */
        {
            SLALeverandoer OSLALeverandoer = new SLALeverandoer();
            try
            {
                if (OListSLALeverandoer.Any())
                {
                    OSLALeverandoer = OListSLALeverandoer.Find(OSLALeverandoeren => OSLALeverandoeren.IDSLA.Equals(IDSLA));
                }
            }
            catch(NullReferenceException e)
            {
                MessageBox.Show(e.Message);
            }

            return OSLALeverandoer;
        }

        public SLALeverandoer SoekSLALeverandoerVedNavn(string SLALeverandoerNavn)
        /*
         * Søker opp og finner objektet ved hjelp av navnet.
         */
        {
            SLALeverandoer OLeverandoer = new SLALeverandoer();
            try
            {
                if(OListSLALeverandoer.Any())
                {
                    OLeverandoer = OListSLALeverandoer.Find(OSLALeverandoeren => OSLALeverandoeren.SLALeverandoerNavn.Equals(SLALeverandoerNavn));
                }
            }
            catch(NullReferenceException e)
            {
                MessageBox.Show(e.Message);
            }


            return OLeverandoer;
        }

        public SLALeverandoer SoekSLALeverandoerListIndeks(int Indeks)
        {
            return OListSLALeverandoer[Indeks];
        }

        public int MaxLengdeSLALeveandoerList()
        /*
         * Finner hvor mange Objekter som er i listen.
         */
        {
            return OListSLALeverandoer.Count;
        }

        public int IDSisteElementetSLALeverandoerListe()
        /*
         * Henter ID på siste element i listen
         */
        {
            int Returverdi=0;
            try
            {
                if (OListSLALeverandoer.Any())
                {
                    Returverdi = OListSLALeverandoer.LastOrDefault().IDSLA;
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
