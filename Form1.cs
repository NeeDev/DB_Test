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

namespace DBinit
{
    public partial class Form1 : Form
    {
        string sConString = "server=localhost;uid=sa;pwd=asd123;database=STOCK";
        
        SqlConnection scon;
        SqlCommand scom;

        public Form1()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            scon = new SqlConnection(sConString);
            scom = new SqlCommand();
            scom.Connection = scon;

            axKHOpenAPI1.CommConnect();
            axKHOpenAPI1.OnEventConnect += onConn;    
        }

        public void onConn(Object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            if(e.nErrCode == 0)
            {
                scon.Open();
                string result = axKHOpenAPI1.GetCodeListByMarket("10");
                string[] companys = result.Split(';');
                foreach (string s in companys)
                {
                    scom.CommandText = $"insert into KOSPI values ('{axKHOpenAPI1.GetMasterCodeName(s)}','{s}')";
                    scom.ExecuteNonQuery();
                }
                scon.Close();  
            }
        }


    }
}
