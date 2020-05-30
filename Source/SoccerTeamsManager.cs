using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Codenation.Challenge.Exceptions;
using Source.Models;

namespace Codenation.Challenge
{
    public class SoccerTeamsManager : IManageSoccerTeams
    {
        #region Attributes
        private List<Player> players;
        private List<SoccerTeam> teams;
        #endregion

        #region Constructor
        public SoccerTeamsManager()
        {
            this.players = new List<Player>();
            this.teams = new List<SoccerTeam>();
        }
        #endregion

        #region Queryable
        private IQueryable<Player> QueryPlayer() => this.players.AsQueryable<Player>();
        private IQueryable<SoccerTeam> QuerySoccerTeam() => this.teams.AsQueryable<SoccerTeam>();
        #endregion

        #region Method Search
        private bool ExistsTeam(long teamId) => this.teams.Any(st => st.Id.Value == teamId);
        private bool ExistsPlayer(long playerId) => this.players.Any(p => p.Id.Value == playerId);
        private Player FindPlayer(long playerId) => this.players.Find(p => p.Id == playerId);
        private SoccerTeam FindSoccerTeam(long teamId) => this.teams.Find(p => p.Id == teamId);
        private List<SoccerTeam> FindSoccerTeam(Expression<Func<SoccerTeam, bool>> expression) => this.QuerySoccerTeam().Where(expression).ToList();
        private List<Player> FindPlayer(Expression<Func<Player, bool>> expression) => this.QueryPlayer().Where(expression).ToList();
        #endregion

        #region Method Repository
        public void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            if (this.ExistsTeam(id))
                throw new UniqueIdentifierException($"O Time {id} - {name} já está registrado!");

            var newSoccerTeam = new SoccerTeam
            {
                CreatedAt = createDate,
                Id = id,
                Name = name,
                PrimaryColor = mainShirtColor,
                SecondaryColor = secondaryShirtColor
            };

            this.teams.Add(newSoccerTeam);
        }

        public void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            if (this.ExistsPlayer(id))
                throw new UniqueIdentifierException($"O Jogado {id} - {name} já está registrado!");

            if (!this.ExistsTeam(teamId))
                throw new TeamNotFoundException($"Time {teamId} não foi encontrado!");

            var newPlayer = new Player
            {
                Id = id,
                Name = name,
                BirthDate = birthDate,
                Salary = salary,
                SkillLevel = skillLevel,
                SoccerTeam = teamId,
                IsCaptain = false
            };

            this.players.Add(newPlayer);
        }

        public void SetCaptain(long playerId)
        {
            if (!this.ExistsPlayer(playerId))
                throw new PlayerNotFoundException($"O Jogado {playerId} não foi encontrado!");

            var player = this.FindPlayer(playerId);
            player.IsCaptain = true;
            this.players.Remove(player);
            this.players.Add(player);
        }

        public long GetTeamCaptain(long teamId)
        {
            if (!this.ExistsTeam(teamId))
                throw new TeamNotFoundException($"Time {teamId} não foi encontrado!");

            Player player = this.FindPlayer(p => p.IsCaptain && p.SoccerTeam == teamId).FirstOrDefault();
            if (player == null)
                throw new CaptainNotFoundException($"Capitão do Time {teamId} não foi encontrado!");

            return player.Id.Value;
        }

        public string GetPlayerName(long playerId)
        {
            if (!this.ExistsPlayer(playerId))
                throw new PlayerNotFoundException($"Jogador {playerId} não foi encontrado!");

            return this.FindPlayer(st => st.Id.Value == playerId).First().Name;
        }

        public string GetTeamName(long teamId)
        {
            if (!this.ExistsTeam(teamId))
                throw new TeamNotFoundException($"Time {teamId} não foi encontrado!");

            return this.teams.Find(st => st.Id.Value == teamId).Name;
        }

        public List<long> GetTeamPlayers(long teamId)
        {
            if (!this.ExistsTeam(teamId))
                throw new TeamNotFoundException($"Time {teamId} não foi encontrado!");

            return this.FindPlayer(p => p.SoccerTeam.HasValue && p.SoccerTeam.Value == teamId).OrderBy(p => p.Id.Value).Select(p => p.Id.Value).ToList();
        }

        public long GetBestTeamPlayer(long teamId)
        {
            if (!this.ExistsTeam(teamId))
                throw new TeamNotFoundException($"Time {teamId} não foi encontrado!");

            if (!this.ExistsPlayer(teamId))
                throw new PlayerNotFoundException($"Time {teamId} sem jogadores registrados!");

            return this.FindPlayer(p => p.SoccerTeam.HasValue && p.SoccerTeam.Value == teamId).OrderByDescending(p => p.SkillLevel).ThenBy(p => p.Id).First().Id.Value;
        }

        public long GetOlderTeamPlayer(long teamId)
        {
            if (!this.ExistsTeam(teamId))
                throw new TeamNotFoundException($"Time {teamId} não foi encontrado!");

            return this.FindPlayer(st => st.SoccerTeam.HasValue && st.SoccerTeam.Value == teamId).OrderBy(p => p.BirthDate.Value).First().Id.Value;
        }

        public List<long> GetTeams() => this.teams.OrderBy(t => t.Id.Value).Select(t => t.Id.Value).ToList();

        public long GetHigherSalaryPlayer(long teamId)
        {
            if (!this.teams.Any(t => t.Id.Value == teamId))
            {
                throw new TeamNotFoundException($"Time {teamId} não foi encontrado!");
            }

            if (!this.players.Any(p => p.SoccerTeam.Value == teamId))
            {
                throw new PlayerNotFoundException($"Time {teamId} sem jogadores registrados!");
            }

            var player = this.players.Where(p => p.SoccerTeam.Value == teamId).OrderByDescending(x => x.Salary).ThenBy(x => x.Id).First();
            return player.Id.Value;
        }

        public decimal GetPlayerSalary(long playerId)
        {
            if (!this.ExistsPlayer(playerId))
                throw new PlayerNotFoundException($"Jogado {playerId} não foi encontrado!");

            return this.FindPlayer(p => p.Id.Value == playerId).First().Salary.Value;
        }

        public List<long> GetTopPlayers(int top) => this.players.OrderByDescending(p => p.SkillLevel).ThenBy(p => p.Id.Value).Take(top).Select(p => p.Id.Value).ToList();

        public string GetVisitorShirtColor(long teamId, long visitorTeamId)
        {
            if (!this.ExistsTeam(teamId))
                throw new TeamNotFoundException($"Time {teamId} não foi encontrado!");

            if (!this.ExistsTeam(visitorTeamId))
                throw new TeamNotFoundException($"Time {visitorTeamId} não foi encontrado!");

            SoccerTeam team1 = this.FindSoccerTeam(teamId);
            SoccerTeam team2 = this.FindSoccerTeam(visitorTeamId);

            return team1.PrimaryColor.Equals(team2.PrimaryColor) ? team2.SecondaryColor : team2.PrimaryColor;
        }
        #endregion
    }
}
