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
using System.Net.Http.Headers;

namespace MealCount_V1
{
    public partial class MealLogin : Form
    {
        public MealLogin()
        {
            InitializeComponent();
        }

        SqlCommand Cmd;
        public string ConsString = "Data Source=MTX-SRV-FR001;Initial Catalog=Mtx_Meals;Integrated Security=True";



        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //checking the login details from the DB

            SqlConnection Cons = new SqlConnection(ConsString);
            Cons.Open();

            try
            {
                Cmd = new SqlCommand("select * from Meal_Login where username = '"+txtusername.Text+"' and password = '"+txtupwd.Text+"'",Cons);
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                SqlDataReader userSqlDr = Cmd.ExecuteReader();
                while(userSqlDr.Read())
                {

                }
                int i = ds.Tables[0].Rows.Count;
                if(i==1)
                {
                    MealPortal NewMealPortal = new MealPortal();
                    NewMealPortal.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Username or Password is incorrect");
                    txtusername.Text = "";
                    txtupwd.Text = "";
                    this.ActiveControl = txtusername;
                }

            }
            catch (SqlException exlogin)
            {
                MessageBox.Show(exlogin.Message);
                Application.Exit();
            }
            finally
            {
                Cons.Close();
            }
            
            
            
            
        }

        private void MealLogin_Load(object sender, EventArgs e)
        {
                      
            

        }
    }
}
