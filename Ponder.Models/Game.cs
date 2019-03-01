using System;
using System.Collections.Generic;

namespace Ponder.Models
{
    public class Game : PonderObject
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Venue Venue { get; set; }
        public List<Round> Rounds { get; set; }
    }
}
