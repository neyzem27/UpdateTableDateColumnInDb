using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;

namespace HataDuzeltme
{
    public partial class Form1 : Form
    {
        // Getting connection string from App.config file
        string StrCon = ConfigurationManager.ConnectionStrings["hedefDb"].ToString();//silinecek db
        string anaDB = ConfigurationManager.ConnectionStrings["anaDB"].ToString();//ana db

        public Form1()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            // string XMlFile = txtFilePath.Text;
            try
            {
                if (!txtSeri.Text.Equals("") && !txtSira.Text.Equals(""))
                {
                    /* // Conversion Xml file to DataTable
                     DataTable dt = CreateDataTableXML(XMlFile);
                     if (dt.Columns.Count == 0)
                         dt.ReadXml(XMlFile);
                    */
                    // Creating Query for Table Creation
                    //string Query = CreateTableQuery(dt);  ----@"Data Source=192.168.1.219\SQLEXPJUMP;Initial Catalog=MikroDB_V16_004;User ID =sa;Password =Sa1234;Integrated Security=True"
                    SqlConnection con = new SqlConnection(StrCon);//
                    con.Open();

                    // Deletion of Table 2022-10-22 00:00:00.000
                    SqlCommand cmd = new SqlCommand("UPDATE " + anaDB + ".[dbo].[CARI_HESAP_HAREKETLERI] SET cha_belge_tarih='" + dateTimePicker1.Text+ "',cha_tarihi='" + dateTimePicker1.Text + "' where cha_evrak_tip=63 and cha_evrakno_seri='" + txtSeri.Text.ToUpper() + "' and cha_evrakno_sira in (" + txtSira.Text + ");", con);
                    int check = cmd.ExecuteNonQuery();

                    if (check != 0)
                    {
                        // Copy Data from DataTable to Sql Table

                        txtSeri.Text = "";
                        txtSira.Text = "";
                        MessageBox.Show("işlem başarıyla yapılmıştır.");
                    }
                    else
                    {
                        MessageBox.Show("Data düzeltmesi yapılmadı.2 sebepten olur.1)db ayarları 2)yanlış kod girilmesi");
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Lütfen seri ve sıra alanlarını doldurun.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("İşlem başarısız.Hata: " + ex);
            }

        }
    }
}
