using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
In visual studio this appears as a GUI since its a partial class of Form1
Please do not edit the "GUI", This is just code
 */

namespace Unit4
{
    partial class Form1
    {

        // Even though indivusuals and teams are both team structures, we are keeing them in seperate lists
        static List<Team> Individuals;
        List<Team> Teams;
        List<Event> Events;

        public class Team
        {
            public string Name;
            public bool IsIndividual;
            public int Points;
            public int ID;
            public bool SingleEvent;
        }

        public class Event
        {
            public string Name;
            public bool IsIndividual;
            public int ID;
            public bool Done;
            public List<int> ParticipantsIDs;
        }

        // Method to create a new team
        public void RegisterTeam (string name, bool isIndividual, bool singleEvent)
        {
            Team _team = new Team(); // Since event NEEDS an underscore in Method ReisterEvent ive added an underscore here for consincy
            _team.Name = name;
            _team.SingleEvent = singleEvent;
            _team.IsIndividual = isIndividual;

            _team.Points = 0;

            _team.ID = Teams.Count + Individuals.Count;

            if (isIndividual && Individuals.Count < 20)
            {
                Individuals.Add(_team);
            } else if (!isIndividual)
            {
                Teams.Add(_team);
            }
        }

        // Method to create a new event
        public void RegisterEvent(string name, bool isIndividual)
        {
            Event _event = new Event (); // event has an underscore since event is a reserved varaible in C#... GRRR
            _event.Name = name;
            _event.IsIndividual = isIndividual;
            _event.ID = Events.Count;
            _event.ParticipantsIDs = new List<int>();

            Events.Add(_event);
        }

        // Method to enroll a team into an event if elligble
        public int EnrollTeam (int eventID, Team team)
        {

            // Check team is not in more than 1 or 5 evets
            int count = 0;
            foreach (Event e in Events)
            {
                for (int i = 0; i<e.ParticipantsIDs.Count; i++)
                {
                    if (e.ParticipantsIDs[i] == team.ID) count++;
                }
            }
            // Return if team is trying to enter more events than they should be
            if (team.SingleEvent && count > 1) return -1;
            else if (!team.SingleEvent && count > 5) return -1;

            Events[eventID].ParticipantsIDs.Add(team.ID);
            return 0;
        }

        public void RegsiterResult(List<int> winners, int eventID, bool isIndividual)
        {
            for (int i=0; i<winners.Count; i++)
            {
                // Get index of team or person
                int index = 0;
                if (isIndividual)
                {
                    for (int j=0; j<Individuals.Count; j++)
                    {
                        if (Individuals[i].ID == winners[i])
                        {
                            index = j;
                            break;
                        }
                    }
                    Individuals[index].Points += (winners.Count - i);
                } else
                {
                    for (int j = 0; j < Teams.Count; j++)
                    {
                        if (Teams[i].ID == winners[i])
                        {
                            index = j;
                            break;
                        }
                    }
                    Teams[index].Points += (winners.Count - i);
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(831, 534);
            this.Name = "Form1";
            this.Text = "Registration form";
            this.ResumeLayout(false);

        }
    }
}
