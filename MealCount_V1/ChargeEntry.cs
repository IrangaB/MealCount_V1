using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MealCount_V1
{
    public partial class ChargeEntry : Form
    {
        public ChargeEntry()
        {
            InitializeComponent();
        }
        SqlCommand Cmd;
        public string ConsString = "Data Source=MTX-SRV-FR001;Initial Catalog=Mtx_Meals;Integrated Security=True";

        private void btnSave_Click(object sender, EventArgs e)
        {
            String chargeDate = DateTime.Today.ToString("d");
            SqlConnection Cons = new SqlConnection(ConsString);
            Cons.Open();

            try
            {
                //insert meal charges in to the system

                SqlCommand InsertMealCharges = new SqlCommand("insert into meal_charge (chage_date,lunch,dinner) values ('" + chargeDate + "','" + txtLunch.Text + "','" + txtDinner.Text + "')", Cons);
                SqlDataReader InsertMealDr = InsertMealCharges.ExecuteReader();
                while (InsertMealDr.Read())
                {

                }
                MessageBox.Show("Saved");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
            finally
            {
                Cons.Close();
            }
        }

        private void txtLunch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtDinner_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
