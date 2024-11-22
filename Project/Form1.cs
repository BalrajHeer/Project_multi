using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Form1 : Form
    {
        internal enum Grids
        {
            Co,
            St,
            Enroll,
            Prog
        }

        internal static Form1 current;

        private Grids grid;

        private bool OKToChange = true;

        public Form1()
        {
            current = this;
            InitializeComponent();           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            new Form2();
            Form2.current.Visible = false;

            Text = "Employees & Projects";
            dataGridView1.Dock = DockStyle.Fill;
        }

       

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            if (Business.Courses.UpdateCourses() == -1)
            {
                
                bindingSource1.ResetBindings(false);
            }
        }

        private void bindingSource2_CurrentChanged(object sender, EventArgs e)
        {
            if (Business.Students.UpdateStudents() == -1)
            {
               
                bindingSource2.ResetBindings(false);
            }
        }

        private void menuStrip1_Click(object sender, EventArgs e)
        {
            
            OKToChange = true;
            Validate();
            if (grid==Grids.Co)
            {
                
                if (Business.Courses.UpdateCourses() == -1)
                {                    
                    OKToChange = false;
                }
            }
            else if (grid == Grids.St)
            {
                
                if (Business.Students.UpdateStudents() == -1)
                {
                    OKToChange = false;
                }               
            }
        }

        internal static void BLLMessage(string s)
        {
            MessageBox.Show("Business Layer: " + s);
        }

        internal static void DALMessage(string s)
        {
            MessageBox.Show("Data Layer: " + s);
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2.current.Start(Form2.Modes.INSERT,null);            
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection c = dataGridView1.SelectedRows;
            if (c.Count == 0)
            {
                MessageBox.Show("A line must be selected for update");
            }
            else if (c.Count > 1)
            {
                MessageBox.Show("Only one line must be selected for update");
            }
            else
            {                
                if (""+c[0].Cells["FinalGrade"].Value == "")
                {
                    Form2.current.Start(Form2.Modes.UPDATE, c);
                }
                else
                {
                    MessageBox.Show("To update this line, FinalGrade value must be removed first.");
                }                             
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection c = dataGridView1.SelectedRows;
            if (c.Count == 0)
            {
                MessageBox.Show("At least one line must be selected for deletion");
            }
            else 
            {
                List<string[]> lId = new List<string[]>();
                for (int i = 0; i < c.Count; i++)
                {       
                    lId.Add(new string[] { "" + c[i].Cells["CId"].Value,
                                           "" + c[i].Cells["StId"].Value });
                }
                Data.Enrollments.DeleteData(lId);
            }
        }

        private void evaluationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection c = dataGridView1.SelectedRows;
            if (c.Count == 0)
            {
                MessageBox.Show("A line must be selected for evaluation update");
            }
            else if (c.Count > 1)
            {
                MessageBox.Show("Only one line must be selected for update");
            }
            else
            {
                Form2.current.Start(Form2.Modes.FINALGRADE, c);
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Impossible to insert / update");
            e.Cancel = false;  
            OKToChange = false;
        }

        private void coursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OKToChange)
            {
                grid = Grids.Co;
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource1.DataSource = Data.Courses.GetCourses();
                bindingSource1.Sort = "CId";
                dataGridView1.DataSource = bindingSource1;

                dataGridView1.Columns["CName"].HeaderText = "Course Name";
                dataGridView1.Columns["CId"].DisplayIndex = 0;
                dataGridView1.Columns["CName"].DisplayIndex = 1;
                
            }
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OKToChange)
            {
                grid = Grids.St;
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource2.DataSource = Data.Students.GetStudents();
                bindingSource2.Sort = "StId";
                dataGridView1.DataSource = bindingSource2;

                dataGridView1.Columns["StName"].HeaderText = "Student Name";
                dataGridView1.Columns["StId"].DisplayIndex = 0;
                dataGridView1.Columns["StName"].DisplayIndex = 1;
                
            }
        }

        private void enrollmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OKToChange && (grid != Grids.Enroll))
            {
                grid = Grids.Enroll;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource3.DataSource = Data.Enrollments.GetDisplayEnrollments();
                bindingSource3.Sort = "CId, StId";    
                dataGridView1.DataSource = bindingSource3;
                

                dataGridView1.Columns["CName"].HeaderText = "Course Name";
                dataGridView1.Columns["StName"].HeaderText = "Student Name";
                dataGridView1.Columns["FinalGrade"].HeaderText = "Final Grade";
                dataGridView1.Columns["CId"].DisplayIndex = 0;
                dataGridView1.Columns["CName"].DisplayIndex = 1;
                dataGridView1.Columns["StId"].DisplayIndex = 2;
                dataGridView1.Columns["StName"].DisplayIndex = 3;
                dataGridView1.Columns["FinalGrade"].DisplayIndex = 4;
            }
        }

        private void programsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OKToChange)
            {
                grid = Grids.Prog;
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource4.DataSource = Data.Programs.GetPrograms();
                bindingSource4.Sort = "ProgId";
                dataGridView1.DataSource = bindingSource4;

                dataGridView1.Columns["ProgName"].HeaderText = "Program Name";
                dataGridView1.Columns["ProgId"].DisplayIndex = 0;
                dataGridView1.Columns["ProgName"].DisplayIndex = 1;
                
            }
        }
    }
}
