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
        List<Team> Teams;
        List<Event> Events;

        public class Result
        {
            public int EventID;
            public int Score;
        }

        public class Team
        {
            public string Name;
            public bool IsIndividual;
            public int Points;
            public int ID;
            public bool SingleEvent;
            public List<Result> Results;
            public int EnteredEvents;
        }

        public class Event
        {
            public string Name;
            public bool IsIndividual;
            public int ID;
            public bool Done;
            public List<int> ParticipantsIDs;
        }

        public int AmountOfIndividuals()
        {
            int count = 0;
            foreach(Team team in Teams)
            {
                if (team.IsIndividual) count++;
            }
            return count;
        }

        // Method to create a new team
        public void RegisterTeam (string name, bool isIndividual, bool singleEvent)
        {
            Team _team = new Team(); // Since event NEEDS an underscore in Method ReisterEvent ive added an underscore here for consincy
            _team.Name = name;
            _team.SingleEvent = singleEvent;
            _team.IsIndividual = isIndividual;

            _team.Points = 0;

            _team.ID = Teams.Count;
            _team.Results = new List<Result>();
            if (isIndividual && AmountOfIndividuals() > 20) return;

            Teams.Add(_team);
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

        public int GetIndexOfTeam (int teamID, List<Team> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == teamID)
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
