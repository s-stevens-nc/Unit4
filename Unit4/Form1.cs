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
            Events = new List<Event>();
            InitializeComponent();

            // Register Event (<string EventName> <boolean IsIndividual>)
            RegisterEvent("100m Race", true);
            RegisterEvent("200m Race", true);
            RegisterEvent("500m Race", true);
            RegisterEvent("1km Race", true);
            RegisterEvent("Hurdles Race", true);
            RegisterEvent("Long Jump", true);
            RegisterEvent("Egg and Spoon Race", true);
            RegisterEvent("Relay Race", false);
            RegisterEvent("Football", false);
            RegisterEvent("Basketball", false);
            RegisterEvent("VolleyBall", false);
            RegisterEvent("Hockey", false);
            RegisterEvent("Rugby", false);
            RegisterEvent("Handball", false);
            RegisterEvent("Dodgeball", false);

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
            dt.Columns.Add("Team", typeof(string));


            foreach (Event event_ in Events)
            {
                if (event_.Done) dt.Columns.Add(event_.Name, typeof(string));
            }

            dt.Columns.Add("Total Score", typeof(string));

            foreach (Team team in Teams)
            {
                int total = 0;
                foreach (Result result in team.Results)
                {
                    total += result.Score;
                }

                DataRow dr = dt.NewRow();
                dr["Team"] = team.Name;
                dr["Total Score"] = total;

                foreach (Event event_ in Events)
                {
                    if (event_.Done)
                    {
                        foreach (int id in event_.ParticipantsIDs)
                        {
                            if (id == team.ID)
                            {
                                foreach (Result result in team.Results)
                                {
                                    if (result.EventID == event_.ID)
                                    {
                                        dr[event_.Name] = result.Score;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                dt.Rows.Add(dr);
            }
            dataGridView1.Columns.Clear();
            //dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
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

        private void FillComboWithEvents(ComboBox cb)
        {
            cb.Items.Clear();
            foreach (Event eve in Events)
            {
                if (!eve.Done)
                {
                    cb.Items.Add(eve.Name);
                }
            }
        }

        private void FillComboWithTeams(ComboBox cb)
        {
            cb.Items.Clear();
            foreach (Team team in Teams)
            {
                cb.Items.Add(team.Name);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FillComboWithEvents(comboBox1);
        }

        private Team GetTeamFromID(int id)
        {
            foreach(Team team in Teams)
            {
                if (team.ID == id)
                {
                    return team;
                }
            }

            return null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Create table
            DataTable dt = new DataTable();
            dt.Columns.Add("Team", typeof(string));
            dt.Columns.Add("Points", typeof(string));

            // Get event
            Event selectedEvent = new Event();
            foreach (Event event_ in Events)
            {
                if (event_.Name == (string)comboBox1.SelectedItem)
                {
                    selectedEvent = event_;
                    break;
                }
            }

            int points = selectedEvent.ParticipantsIDs.Count;
            foreach (int id in selectedEvent.ParticipantsIDs)
            {
                DataRow dr = dt.NewRow();
                dr["Team"] = GetTeamFromID(id).Name;
                dr["Points"] = points--; // the "--" means the points varaible is decrementd after we used it
                dt.Rows.Add(dr);
            }
            AddResultTable.DataSource = dt;

        }

        private void button12_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            FillComboWithTeams(comboBox2);
        }

        private bool TeamIsNotAlreadyInEvent(Event event_, int teamId)
        {
            foreach (int id in event_.ParticipantsIDs)
            {
                if (id == teamId)
                {
                    return false;
                }
            }
            return true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            Team selectedTeam = new Team();
            foreach (Team t in Teams)
            {
                if (t.Name == (string)comboBox2.SelectedItem)
                {
                    selectedTeam = t;
                    break; // Break out since weve already found it
                }
            }

            foreach (Event event_ in Events)
            {
                if (TeamIsNotAlreadyInEvent(event_, selectedTeam.ID))
                {
                    if (event_.IsIndividual == selectedTeam.IsIndividual && !event_.Done) comboBox3.Items.Add(event_.Name);
                }
            }
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            int teamIdx = new int();
            for (int i=0; i<Teams.Count; i++)
            {
                if (Teams[i].Name == (string)comboBox2.SelectedItem)
                {
                    teamIdx = i;
                }
            }

            DialogResult dr = MessageBox.Show(String.Format("Are you sue you want to enter {0} into the {1} event", Teams[teamIdx].Name, (string)comboBox3.SelectedItem), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return;

            if (Teams[teamIdx].SingleEvent && Teams[teamIdx].EnteredEvents == 1)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("{0} are only permitted to enter 1 event", Teams[teamIdx].Name));
                return;
            }
            else if (Teams[teamIdx].EnteredEvents == 5)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("{0} are have already entered 5 events", Teams[teamIdx].Name));
                return;
            }

            foreach (Event event_ in Events)
            {
                if (event_.Name == (string)comboBox3.SelectedItem)
                {
                    event_.ParticipantsIDs.Add(Teams[teamIdx].ID);
                    Teams[teamIdx].EnteredEvents++;
                    break;
                }
            }

            comboBox2.ResetText();
            comboBox2.SelectedIndex = -1;

            comboBox3.ResetText();
            comboBox3.SelectedIndex = -1;
        }

        private void AddResultTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        // It doesnt actually move the row up or down, it just swaps the names
        private void MoveSelectedIndexUp(int direction)
        {
            int index;
            try
            {
                index = AddResultTable.SelectedRows[0].Index;
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return;
            }

            if ((index == 0 && direction == -1) || (index == AddResultTable.Rows.Count-1 && direction == 1)) return;

            string cahceName = (string)AddResultTable.Rows[index + direction].Cells["Team"].Value;

            AddResultTable.Rows[index + direction].Cells["Team"].Value = AddResultTable.Rows[index].Cells["Team"].Value;
            AddResultTable.Rows[index].Cells["Team"].Value = cahceName;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            MoveSelectedIndexUp(-1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MoveSelectedIndexUp(1);
        }

        
        private void button13_Click(object sender, EventArgs e)
        {
            int eventId = new int();
            int eventIdx = new int();
            int i = 0;
            foreach(Event event_ in Events)
            {
                if (event_.Name == (string)comboBox1.SelectedItem)
                {
                    if (event_.Done)
                    {
                        MessageBox.Show("Event has already been completed!");
                        return;
                    }
                    eventId = event_.ID;
                    eventIdx = i;
                    break;
                }
                i++;
            }

            foreach (DataGridViewRow dgvr in AddResultTable.Rows)
            {
                int teamIdx = new int();
                string teamName = (string)dgvr.Cells["Team"].Value;

                for (int ii = 0; ii < Teams.Count; ii++)
                {
                    if (Teams[ii].Name == teamName) { teamIdx = ii; break; }
                }

                Result newResult = new Result();
                newResult.EventID = eventId;
                newResult.Score = Convert.ToInt32(dgvr.Cells["Points"].Value);
                Teams[teamIdx].Results.Add(newResult);
            }

            Events[eventIdx].Done = true;
            FillComboWithEvents(comboBox1);
            comboBox1.ResetText();
            comboBox1.SelectedIndex = -1;
            AddResultTable.DataSource = new DataGridView();
        }

        private void label1_Click_4(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
