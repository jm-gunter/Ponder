using System.Collections.Generic;

namespace Ponder.Models
{
    public class Round
    {
        public string Name { get; set; }

        public List<Question> Questions { get; set; }
    }
}