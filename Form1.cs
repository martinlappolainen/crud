using NPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud_Lappolainen
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        EntityState objState = EntityState.Unchanged;
        public Form1()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'contactDBDataSet.Studendts' table. You can move, or remove it, as needed.
            this.studendtsTableAdapter.Fill(this.contactDBDataSet.Studendts);
            try
            {
                using (IDatabase db = new Database("cn"))
                {
                    studendtsBindingSource.DataSource = db.Fetch<Student>();
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }

        }

        private void MetroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void MetroTextBox3_Click(object sender, EventArgs e)
        {

        }

        private void MetroTextBox5_Click(object sender, EventArgs e)
        {

        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd= new OpenFileDialog() { Filter = "JPEG|*.jpg|PNG|*.png", ValidateNames = true })
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox2.Image = Image.FromFile(ofd.FileName);
                    Student obj = studendtsBindingSource.Current as Student;
                    if (obj != null)
                        obj.ImageUrl = ofd.FileName;
                }
        }
        void ClearInput()
        {
            txtFullname.Text = null;
            txtEmail.Text = null;
            txtAddress.Text = null;
            chkGender.Checked = false;
            txtBirthday.Text = null;
            pictureBox2.Image = null;
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            objState = EntityState.Added;
            pictureBox2.Image = null;
            pContainer.Enabled = true;
            studendtsBindingSource.Add(new Student());
            studendtsBindingSource.MoveLast();
            txtFullname.Focus();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            objState = EntityState.Changed;
            pContainer.Enabled = true;
            txtFullname.Focus();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            objState = EntityState.Deleted;
            if(MetroFramework.MetroMessageBox.Show(this,"Are you sure want to delete this record?","Message",MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
            {
                try
                {
                    Student obj = studendtsBindingSource.Current as Student;
                    if (obj !=null)
                    {
                        using (IDatabase db = new Database("cn"))
                        {
                            db.Execute("delete from Students where StudentID = @StudenID", new { StudentID = obj.StudentID });
                            studendtsBindingSource.RemoveCurrent();
                            pContainer.Enabled = false;
                            pictureBox2.Image = null;
                            objState = EntityState.Unchanged;
                        }
                    }
                }
                catch (Exception ex)
                {

                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
    }
}
