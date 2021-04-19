using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unit4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Teams = new List<Team>();
            Individuals = new List<Team>();
            Events = new List<Event>();
            InitializeComponent();

            RegisterEvent("100m Race", true);
            RegisterEvent("200m Race", true);
            RegisterEvent("1km Race", true);
            RegisterEvent("Hurdles Race", true);
            RegisterEvent("Relay Race", false);
            RegisterEvent("Fooball", false);

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Welcome to the sportsday!");
        }// this is going to display Welcome to sportday after save button clicked//

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // the name gets put in here//
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void AddTeams_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
            // Makes the team creation page disappear when the individual box is checked //

        {
            if (individualbox.Checked)
            {
               IndividualNameLabel.Visible= true;
                TeamNamelabel.Visible = false;
                team_members.Visible = false;
                TeamBox1.Visible = false;
                TeamBox2.Visible = false;
                TeamBox3.Visible = false;
                TeamBox4.Visible = false;
                TeamBox5.Visible = false;
                TeamLabel1.Visible = false;
                TeamLabel2.Visible = false;
                TeamLabel3.Visible = false;
                TeamLabel4.Visible = false;
                TeamLabel5.Visible = false;

            }
            else
            {
               IndividualNameLabel.Visible = false;
                TeamNamelabel.Visible = true;
                team_members.Visible = true;
                TeamBox1.Visible = true;
                TeamBox2.Visible = true;
                TeamBox3.Visible = true;
                TeamBox4.Visible = true;
                TeamBox5.Visible = true;
                TeamLabel1.Visible = true;
                TeamLabel2.Visible = true;
                TeamLabel3.Visible = true;
                TeamLabel4.Visible = true;
                TeamLabel5.Visible = true;
            }


        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {
            AddTeams.SelectedIndex = 3;
        }

        // Method to add team
        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Welcome to the sportsday!");
            // This message is shown when client click the add team button
            string name = textBox2.Text;
            bool isIndividual = individualbox.Checked;
            bool singleEvent = checkBox2.Checked;
           
           

            RegisterTeam(name, isIndividual, singleEvent);

            textBox2.Text = "";
            individualbox.Checked = false;
            checkBox2.Checked = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
   
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void label1_Click_3(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        // Refresh Leaderboard
        private void button1_Click_1(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            foreach (Team t in Teams) {
                
                dt .Columns.Add(new DataColumn(t.Name, typeof(string)));
            }
            dataGridView1.DataSource = dt;
        }

        // Refresh Events
        private void button3_Click_1(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Event", typeof(string));
            dt.Columns.Add("Type", typeof(string));

            foreach (Event _event in Events)
            {
                if (_event.Done) continue;
                DataRow dr = dt.NewRow();
                dr["Type"] = _event.IsIndividual ? "Solo" : "Individual";
                dr["Event"] = _event.Name;
                
                dt.Rows.Add(dr);
            }
            dataGridView2.DataSource = dt;
        }
    }
}
