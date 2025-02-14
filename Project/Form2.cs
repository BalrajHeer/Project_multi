﻿using System;
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
    public partial class Form2 : Form
    {
        internal enum Modes
        {
            INSERT,
            UPDATE,
            FINALGRADE
        }

        internal static Form2 current;

        private Modes mode = Modes.INSERT;

        private string[] assignInitial;

        public Form2()
        {
            current = this;
            InitializeComponent();
        }

        internal void Start(Modes m, DataGridViewSelectedRowCollection c)
        {
            mode = m;
            Text = "" + mode;

            comboBox1.DisplayMember = "CId";
            comboBox1.ValueMember = "CId";
            comboBox1.DataSource = Data.Courses.GetCourses();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;

            comboBox2.DisplayMember = "StId";
            comboBox2.ValueMember = "StId";
            comboBox2.DataSource = Data.Students.GetStudents();
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.SelectedIndex = 0;

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.Enabled = false;

            if (((mode == Modes.UPDATE) || (mode == Modes.FINALGRADE)) && (c!=null))
            {
                comboBox1.SelectedValue = c[0].Cells["CId"].Value;
                comboBox2.SelectedValue = c[0].Cells["StId"].Value;
                textBox3.Text = ""+c[0].Cells["FinalGrade"].Value;
                assignInitial = new string[] { (string)c[0].Cells["CId"].Value, (string)c[0].Cells["StId"].Value };
            }
            if (mode == Modes.UPDATE)
            {
                textBox3.Enabled = false;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
            }
            if (mode == Modes.FINALGRADE)
            {
                textBox3.Enabled = true;
                textBox3.ReadOnly = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
            }

            ShowDialog();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                var a = from r in Data.Courses.GetCourses().AsEnumerable()
                        where r.Field<string>("CId") == (string)comboBox1.SelectedValue
                        select new { Name = r.Field<string>("CName") };
                textBox1.Text = a.Single().Name;                
            }
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                var a = from r in Data.Students.GetStudents().AsEnumerable()
                        where r.Field<string>("StId") == (string)comboBox2.SelectedValue
                        select new { Name = r.Field<string>("StName") };
                textBox2.Text = a.Single().Name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int r = -1;
            if (mode == Modes.INSERT)
            {
                r= Data.Enrollments.InsertData(new string[] {(string)comboBox1.SelectedValue, (string)comboBox2.SelectedValue });
            }
            if (mode == Modes.UPDATE)
            {
                List<string[]> lId = new List<string[]>();
                lId.Add(assignInitial);

                r = Data.Enrollments.InsertData(new string[] { (string)comboBox1.SelectedValue, (string)comboBox2.SelectedValue });
               
                if (r == 0)
                {
                    r = Data.Enrollments.DeleteData(lId);
                }               
            }
            if (mode == Modes.FINALGRADE)
            {
                r = Business.Enrollments.UpdateFinalGrade(assignInitial,textBox3.Text);
            }

            if (r == 0) { Close(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
