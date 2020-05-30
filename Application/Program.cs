using Codenation.Challenge;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            SoccerTeamsManager repo = new SoccerTeamsManager();


            repo.AddTeam(1, "Corinthians", DateTime.Now, "Branco", "Preto");
            repo.AddTeam(2, "São Paulo", DateTime.Now, "Branco", "Vermelho");

            repo.AddPlayer(1,  1, "Jogador 1",  DateTime.Now, 89, 4001);
            repo.AddPlayer(2,  1, "Jogador 2",  DateTime.Now, 34, 4002);
            repo.AddPlayer(3,  1, "Jogador 3",  DateTime.Now, 98, 4500);
            repo.AddPlayer(4,  1, "Jogador 4",  DateTime.Now, 56, 4003);
            repo.AddPlayer(5,  1, "Jogador 5",  DateTime.Now, 54, 4004);
            repo.AddPlayer(6,  1, "Jogador 6",  new DateTime(1989,10,27), 65, 4005);
            repo.AddPlayer(7,  1, "Jogador 7",  DateTime.Now, 71, 4006);
            repo.AddPlayer(8,  1, "Jogador 8",  DateTime.Now, 73, 4007);
            repo.AddPlayer(9,  1, "Jogador 9",  DateTime.Now, 77, 4500);
            repo.AddPlayer(10, 1, "Jogador 10", DateTime.Now, 20, 4008);
            repo.AddPlayer(11, 1, "Jogador 11", DateTime.Now, 10, 4009);

            repo.AddPlayer(12, 2, "Jogador 1", DateTime.Now, 89,  3001);
            repo.AddPlayer(13, 2, "Jogador 2", DateTime.Now, 34,  3002);
            repo.AddPlayer(14, 2, "Jogador 3", DateTime.Now, 98,  3500);
            repo.AddPlayer(15, 2, "Jogador 4", new DateTime(1985,10,14), 56,  3003);
            repo.AddPlayer(16, 2, "Jogador 5", DateTime.Now, 54,  3004);
            repo.AddPlayer(17, 2, "Jogador 6", DateTime.Now, 100, 3005);
            repo.AddPlayer(18, 2, "Jogador 7", DateTime.Now, 71,  3006);
            repo.AddPlayer(19, 2, "Jogador 8", DateTime.Now, 73,  3007);
            repo.AddPlayer(20, 2, "Jogador 9", DateTime.Now, 77,  3500);
            repo.AddPlayer(21, 2, "Jogador 10", DateTime.Now, 20, 3008);
            repo.AddPlayer(22, 2, "Jogador 11", DateTime.Now, 10, 3009);

            repo.SetCaptain(8);
            repo.SetCaptain(18);

            var result1 = repo.GetTeamCaptain(1);
            var result2 = repo.GetTeamCaptain(2);

            var teams = repo.GetBestTeamPlayer(1);

            var p1 = repo.GetTeamPlayers(1);
            var p2 = repo.GetTeamPlayers(2);

            var color = repo.GetVisitorShirtColor(1, 2);
            var salary = repo.GetPlayerSalary(3);
            var MaxSalary1 = repo.GetHigherSalaryPlayer(1);
            var MaxSalary2 = repo.GetHigherSalaryPlayer(2);

            var best1 = repo.GetBestTeamPlayer(1);
            var best2 = repo.GetBestTeamPlayer(2);

            var best1n = repo.GetTopPlayers(10);

            var old1 = repo.GetOlderTeamPlayer(1);
            var old2 = repo.GetOlderTeamPlayer(2);

        }
    }
}
