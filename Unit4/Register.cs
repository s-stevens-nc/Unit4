﻿using System;
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

        public struct Team
        {
            string Name;
            bool IsIndividual;
            int Points;
            int ID;
            bool SingleEvent;
        }

        public struct Event
        {
            string Name;
            bool IsIndividual;
            int ID;
            bool Done;
            List<int> ParticipantsIDs;
        }

    }
}
