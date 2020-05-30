using System;

namespace Source.Models
{
    public class SoccerTeam : EntityBase
    {
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
    }
}
