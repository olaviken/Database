using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ArealogUtstyr.ListStructure.Lists;
using ArealogUtstyr.ListStructure.Objects;


namespace ArealogUtstyr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Koden under er foreløpig testkode som skal bli flyttet til logikk klasse
        ListKategoriUtstyr ListKategoriUtstyr = new ListKategoriUtstyr();
        ListBygninger Listbygninger = new ListBygninger();
        ListSLALeverandoer ListSLALeverandoerer = new ListSLALeverandoer();
        

        public MainWindow()
        {
            InitializeComponent();
            //Foreløpig testkode dette trenger å bli flyttet til logikk klasse
            Listbygninger.HentSQLbygning();
            ListKategoriUtstyr.HentSQLKategoriUtstyr();
            ListSLALeverandoerer.HentSQLSLALeverandoer();
            
            for(int i =0; i< Listbygninger.MaxLengdeBygninger(); i++)
            {
                Bygning OBygning = Listbygninger.SoekBygningIndeks(i);
                UtListeBygninger.Items.Add(OBygning);
            }

            for(int i = 0; i < ListKategoriUtstyr.MaxLengdeKategoriList(); i++)
            {
                KategoriUtstyr OKategori = ListKategoriUtstyr.SoekKategoriListIndeks(i);
                UtListeKategori.Items.Add(OKategori);
            }

            for(int i=0; i < ListSLALeverandoerer.MaxLengdeSLALeveandoerList(); i++)
            {
                SLALeverandoer OSLALeverandoer = ListSLALeverandoerer.SoekSLALeverandoerListIndeks(i);
                UtListeSLALeverandoer.Items.Add(OSLALeverandoer);
            }
        }

        private void TestLagreFunksjoner(object sender, RoutedEventArgs e)
        {
                string SLALeverandoer = InnSLaLeverandoer.Text;
                string Kontaktperson = InnKontaktperson.Text;
                string Epost = InnEpost.Text;
                ListSLALeverandoerer.LagreSQLSLALeverandoerList(SLALeverandoer,Kontaktperson,Epost);
                UtListeSLALeverandoer.Items.Clear();
                for (int i = 0; i < ListSLALeverandoerer.MaxLengdeSLALeveandoerList(); i++)
                {
                    SLALeverandoer OSLALeverandoer = ListSLALeverandoerer.SoekSLALeverandoerListIndeks(i);
                    UtListeSLALeverandoer.Items.Add(OSLALeverandoer);
                }
        }
        private void TestOppdatereFunksjoner(object sender, RoutedEventArgs e)
        {
            int IDSLA = Int32.Parse(InnIDSLA.Text);
            string SLALeverandoer = InnSLaLeverandoer.Text;
            string Kontaktperson = InnKontaktperson.Text;
            string Epost = InnEpost.Text;
            ListSLALeverandoerer.OppdatereSQLSLALeverandorer(IDSLA,SLALeverandoer,Kontaktperson,Epost);
            UtListeSLALeverandoer.Items.Clear();
            for (int i = 0; i < ListSLALeverandoerer.MaxLengdeSLALeveandoerList(); i++)
            {
                SLALeverandoer OSLALeverandoer = ListSLALeverandoerer.SoekSLALeverandoerListIndeks(i);
                UtListeSLALeverandoer.Items.Add(OSLALeverandoer);
            }
        }

        private void TestSletteFunksjoner(object sender, RoutedEventArgs e) 
        {
            int IDSLA = Int32.Parse(InnIDSLA.Text);
            string SLALeverandoer = InnSLaLeverandoer.Text;
            ListSLALeverandoerer.SletteSQLSLALeverandoerer(IDSLA,SLALeverandoer);
            UtListeSLALeverandoer.Items.Clear();
            for (int i = 0; i < ListSLALeverandoerer.MaxLengdeSLALeveandoerList(); i++)
            {
                SLALeverandoer OSLALeverandoer = ListSLALeverandoerer.SoekSLALeverandoerListIndeks(i);
                UtListeSLALeverandoer.Items.Add(OSLALeverandoer);
            }
        }

        private void RefreshLister(object sender, RoutedEventArgs e)
        {
            UtListeBygninger.Items.Clear();
            UtListeKategori.Items.Clear();
            
            
            for (int i = 0; i < Listbygninger.MaxLengdeBygninger(); i++)
            {
                Bygning OBygning = Listbygninger.SoekBygningIndeks(i);
                UtListeBygninger.Items.Add(OBygning);
            }

            for (int i = 0; i < ListKategoriUtstyr.MaxLengdeKategoriList(); i++)
            {
                KategoriUtstyr OKategori = ListKategoriUtstyr.SoekKategoriListIndeks(i);
                UtListeKategori.Items.Add(OKategori);
            }
        }
    }
}
