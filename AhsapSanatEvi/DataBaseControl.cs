using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AhsapSanatEvi
{
    public static class DataBaseControl
    {
        private static readonly string connectionString = @"Data Source=DESKTOP-LDRM7BI\SQLEXPRESS;Initial Catalog=dbAhsapSanatEvi;Integrated Security=True;Encrypt=False";

        // Babamın Db Kodu: Data Source=BOLAT\SQLEXPRESS;Initial Catalog=dbAhsapSanatEvi;Integrated Security=True 
        // Benim Db Kodum: Data Source=DESKTOP-LDRM7BI\SQLEXPRESS;Initial Catalog=dbAhsapSanatEvi;Integrated Security=True;Encrypt=False

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
