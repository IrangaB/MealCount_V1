using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;

namespace MealCount_V1
{
    public partial class MealUserRegistration : Form
    {
        public MealUserRegistration()
        {
            InitializeComponent();
        }

        SqlCommand Cmd;
        public string ConsString = "Data Source=MTX-SRV-FR001;Initial Catalog=Mtx_Meals;Integrated Security=True";

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //Checking the emplty values before saving triggers
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtEpf.Text == "")
            {
                MessageBox.Show("EPF cannot be blank");
                txtEpf.Select();
            }
            else if (txtFname.Text == "")
            {
                MessageBox.Show("First Name cannot be blank");
                txtFname.Select();
            }
            else if (txtLname.Text == "")
            {
                MessageBox.Show("Last Name cannot be blank");
                txtLname.Select();
            }
            else if (cmbDepartment.SelectedIndex == 0)
            {
                MessageBox.Show("Department is not selected");
            }
            else if (cmdPlant.SelectedIndex == 0) 
            {
                MessageBox.Show("Plant name is not selected");
            }
            else if(rdoActive.Checked == false && rdoResigned.Checked == false)
            {
                MessageBox.Show("Employee satus cannot be blank");
            }

            //Initiate saving fucntion

            else
            {
                SqlConnection Cons = new SqlConnection(ConsString);
                Cons.Open();

                try
                   
                {
                    string empActive;

                    if (rdoActive.Checked)
                    {
                        empActive = "1";
                    }
                    else
                    {
                        empActive = "0";
                    }
                    string newadddate = DateTime.Now.ToString("d");
                    //saving employee data into the db                  

                    SqlCommand InsertEmployee = new SqlCommand("insert into meal_users (emp_plant,emp_epf,emp_nfc,emp_fname,emp_lname,emp_department,emp_status,emp_addeddate) values ('" + cmdPlant.Text + "','" + txtEpf.Text + "','" + txtNfc.Text + "','" + txtFname.Text + "','" + txtLname.Text + "','" + cmbDepartment.Text + "','" + empActive + "','" + newadddate + "')", Cons);
                    SqlDataReader InsertEmpDr = InsertEmployee.ExecuteReader();
                    while(InsertEmpDr.Read())
                    {

                    }
                    InsertEmpDr.Close();
                    MessageBox.Show("Saved");

                    txtEpf.Text = "";
                    txtFname.Text = "";
                    txtLname.Text = "";
                    txtNfc.Text = "";
                    txtSearchEpf.Text = "";
                    cmdPlant.Text = "";
                    cmbDepartment.Text = "";
                    rdoActive.Checked = false;
                    rdoResigned.Checked = false;

                }
                catch(Exception exsaving)
                {
                    MessageBox.Show(exsaving.Message);
                }
                finally
                {
                    Cons.Close();
                }

            }
        }

        private void txtEpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtNfc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtSearchEpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // To make sure error proof

            txtEpf.Text = "";
            txtFname.Text = "";
            txtLname.Text = "";
            txtNfc.Text = "";
            cmdPlant.Text = "";
            cmbDepartment.Text = "";
            rdoActive.Checked = false;
            rdoResigned.Checked = false;
            
            //Checking the validity of the text box

            if(txtSearchEpf.Text == "")
            {
                MessageBox.Show("Cannot search without EPF number");
            }
            else
            {
                SqlConnection Cons = new SqlConnection(ConsString);
                Cons.Open();

                try
                {
                    dgEmpView.Rows.Clear();
                    txtSearchEpf.Select();
                    SqlCommand cmd = new SqlCommand("select * from meal_users where emp_epf = '" + txtSearchEpf.Text + "'", Cons);
                    SqlDataReader EmployeeDateReader = cmd.ExecuteReader();
                    while (EmployeeDateReader.Read())
                    {
                        int EmpDataRow = dgEmpView.Rows.Add();
                        dgEmpView.Rows[EmpDataRow].Cells[0].Value = EmployeeDateReader["emp_plant"].ToString();
                        dgEmpView.Rows[EmpDataRow].Cells[1].Value = EmployeeDateReader["emp_epf"].ToString();
                        String EmpFirstName = EmployeeDateReader["emp_fname"].ToString();
                        String EmpLastName = EmployeeDateReader["emp_lname"].ToString();
                        String ConcatName = EmpFirstName + " " + EmpLastName;
                        dgEmpView.Rows[EmpDataRow].Cells[2].Value = ConcatName;

                        String EmpActive = EmployeeDateReader["emp_status"].ToString();
                        if(EmpActive == "1")
                        {
                            dgEmpView.Rows[EmpDataRow].Cells[3].Value = "Active";
                        }
                        else
                        {
                            dgEmpView.Rows[EmpDataRow].Cells[3].Value = "Resigned";
                        }
                        
                    }

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
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            txtSearchEpf.Enabled = false;
            if(rdoNewActive.Checked == false && rdoNewResigned.Checked == false)
            {
                MessageBox.Show("Employee new status cannot be blank");
            }
            else
            {
                SqlConnection Cons =new SqlConnection(ConsString);
                Cons.Open();

                try
                {
                    String NewEmpStatus;

                    if(rdoNewActive.Checked)
                    {
                        NewEmpStatus = "1";
                    }else
                    {
                        NewEmpStatus = "0";
                    }

                    SqlCommand EmpStatusUpdate = new SqlCommand("update meal_users set emp_status = '" + NewEmpStatus + "' where emp_epf = '"+txtSearchEpf.Text+"'", Cons);
                    SqlDataReader NewDr = EmpStatusUpdate.ExecuteReader();
                    while (NewDr.Read())
                    {

                    }
                    MessageBox.Show("Employee Status Updated");
                    dgEmpView.Rows.Clear(); // clear the data grid
                    btnSearch.PerformClick(); // perform data grid search button
                    txtSearchEpf.Enabled = true; // enable text box

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
        }
    }
}
