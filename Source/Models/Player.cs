using System;

namespace Source.Models
{
    public class Player : EntityBase
    {
        public long? SoccerTeam { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public int SkillLevel { get; set; }
        public decimal? Salary { get; set; }
        public bool IsCaptain { get; set; } = false;
    }
}
