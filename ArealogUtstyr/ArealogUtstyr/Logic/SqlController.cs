using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ArealogUtstyr.Logic
{
    internal class SqlController
    {
        private static string DBadresse = "mysql.etellerannet.bedrift.no";//ikke reelle adresse. 
        private static string Database = "olavik_ArealUtstyr";
        private static string Bnavn = "LoginUserName"; //ikke reelle brukernavnet
        private static string Psw = "EnterPassword"; //ikke reelle passordet.

        private static string SqlKontaktStreng = $"Server={DBadresse}; Database={Database}; User Id={Bnavn}; Password={Psw};";

        public MySqlConnection EtablerSqlKontakt()
        {
            return new MySqlConnection(SqlKontaktStreng);
        }

        public MySqlCommand SqlKommandoHent(MySqlConnection MinSqlKontakt, string SqlKommando)
        {
            return new MySqlCommand(SqlKommando, MinSqlKontakt);
        }

        public MySqlCommand SqlKommandoEndringer(string SqlKommando, MySqlConnection MinSqlKontakt)
        {
            return new MySqlCommand(SqlKommando, MinSqlKontakt);
        }
    }
}
