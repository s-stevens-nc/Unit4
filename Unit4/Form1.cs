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

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Welcome to the sportsday!");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        // Method to change options depending on if a team is indivudual or team
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            Control[] controls = new Control[] { IndividualNameLabel, TeamNamelabel, team_members, TeamBox1, TeamBox2, TeamBox3, TeamBox4, TeamBox5, TeamLabel1, TeamLabel2, TeamLabel3, TeamLabel4, TeamLabel5};
            for (int i = 0; i < controls.Length; i++) controls[i].Visible = !individualbox.Checked;
            label3.Text = individualbox.Checked ? "Individual name" : "Team name";
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

            if (isIndividual)
            {
                int count = 0;
                foreach (Team team in Teams)
                {
                    if (team.IsIndividual) count++;
                }
                if (count >= 20)
                {
                    DialogResult dr = MessageBox.Show(String.Format("There are already 20 individuals registered\nCannot add {0}", name), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            RegisterTeam(name, isIndividual, singleEvent);

            textBox2.Text = "";
            individualbox.Checked = false;
            checkBox2.Checked = false;
        }

        // Method to refresh leaderboard
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

                // Create a new row
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

            // Refresh leaderboard
            dataGridView1.Columns.Clear();
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

        // Method to fill a combobox with every incomplete event
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

        // Method to fill Combobox with teams
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

        // Method to get team from ID, return Null if none
        private Team GetTeamFromID(int id)
        {
            foreach (Team team in Teams)
            {
                if (team.ID == id)
                {
                    return team;
                }
            }

            return null;
        }

        // Method to change teams in results table based on selected event
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

        // Method to change the events in combobox when selected team has changed
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

        // Method to join selected team to selected event
        private void button11_Click(object sender, EventArgs e)
        {
            int teamIdx = -1;
            for (int i = 0; i < Teams.Count; i++)
            {
                if (Teams[i].Name == (string)comboBox2.SelectedItem)
                {
                    teamIdx = i;
                }
            }

            // If team not found
            if (teamIdx == -1)
            {
                MessageBox.Show(String.Format("Could not found a team named \"{0}\"", comboBox2.Text), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // If event doesnt exist
            if (comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show(String.Format("Could not found a event named \"{0}\"", comboBox3.Text), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
                System.Windows.Forms.MessageBox.Show(String.Format("{0} have already entered 5 events", Teams[teamIdx].Name));
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


        // Method to move selected team up and down in the result table
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

            if ((index == 0 && direction == -1) || (index == AddResultTable.Rows.Count - 1 && direction == 1)) return;

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

        // Method to submit results
        private void button13_Click(object sender, EventArgs e)
        {
            int eventId = new int();
            int eventIdx = new int();
            int i = 0;
            foreach (Event event_ in Events)
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

        private void button15_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Welcome to the sports day software, this is your main menu." + "\n" +
                "Click on any button to take you to the page indicated");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the page for adding contestants." + "\n" + "" +
                "If you want to join as a solo compettitor please tick the Are you entering as an idividual checkbox" + "\n" +
                " If you only wish to enter for one event, please tick the Are you participating in only one event checkbox");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("thanks for adding another event!");


        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Now you have added a team (or an individual) its time to choose an event \n " +
                "click refresh teams and then choose your name from the drop down \n " +
                "Then press the select event dropdown and select what you want to join \n " +
                "Finally press Join Event");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Here you can add events to the sportsday.");
        }
    }
}
    