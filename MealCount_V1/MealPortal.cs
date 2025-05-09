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
    public partial class MealPortal : Form
    {
        public MealPortal()
        {
            InitializeComponent();
        }

        private void MealPortal_Load(object sender, EventArgs e)
        {
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            String DomainTime = DateTime.Now.ToString("T");
            lblTime.Text = (DomainTime);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MealSwipe newMealSwipe = new MealSwipe();
            newMealSwipe.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MealUserRegistration NewMealUserRegistration = new MealUserRegistration();
            NewMealUserRegistration.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChargeEntry NewchargeEntry = new ChargeEntry();
            NewchargeEntry.Show();
        }
    }
}
