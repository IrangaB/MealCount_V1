using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MealCount_V1
{
    public partial class MealSwipe : Form
    {
        public MealSwipe()
        {
            InitializeComponent();
           
        }

        DateTime CurrentTime = DateTime.Now;
        DateTime SetLunchTime = Convert.ToDateTime("02:31:00 PM");
        DateTime SetDinnerTime = Convert.ToDateTime("10:00:00 PM");
        private void timer1_Tick(object sender, EventArgs e)
        {
            String SwapTime = DateTime.Now.ToString("T");
            lblMealSwapTime.Text = SwapTime;
            int i = DateTime.Compare(CurrentTime, SetLunchTime);
            int ii = DateTime.Compare(SetDinnerTime, SetLunchTime);

            if(i>0)
            {
               // this.Close();
            }

        }
        public void TriggerClose()
        {


            int i = DateTime.Compare(CurrentTime, SetLunchTime);
            int ii = DateTime.Compare(CurrentTime, SetDinnerTime);
            if (i >= 0)
            {
                txtNfcdata.Text = "Sorry Lunch time is up.. ";
                txtNfcdata.Enabled = false;
                btnSaveSwipe.Enabled = false;

            }
            else if (ii > 0)
            {
                txtNfcdata.Text = "Sorry Dinner time is up.. ";
                txtNfcdata.Enabled = false;
                btnSaveSwipe.Enabled = false;
            }
            else
            {
                txtNfcdata.Enabled = true;
                btnSaveSwipe.Enabled = true;
            }
        }


        private void MealSwipe_Load(object sender, EventArgs e)
        {
           timer1.Start();                     
          // TriggerClose();

        }
              

        private void btnSave_Click(object sender, EventArgs e)
        {
            String Bcode = txtNfcdata.Text;
            MessageBox.Show(Bcode);

            try
            {




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }






        }

        private void txtNfcdata_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
