using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;
using Autofac;
using Effisoft.RookieBetting.Common.Models.NflFeed;
using Effisoft.RookieBetting.Infrastructure.Database;
using Effisoft.RookieBetting.Infrastructure.Repository;
using Effisoft.RookieBetting.SqlDataAccess;
using Effisoft.RookieBetting.SqlDataAccess.Core;
using Newtonsoft.Json;
using RestSharp;

namespace Effisoft.RookieBetting.NflLiveUpdateWebJob
{
    class Program
    {
        static readonly List<GameLiveFeed> Games = new List<GameLiveFeed>();
        private static string _content;
        private static string _previousContent;
        private static bool _noErrors = true;

        static IContainer ConfigureAutofac()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>()
                .WithParameter("connStringName", ConfigurationManager.ConnectionStrings["RookieBettingDbConnString"].ConnectionString);
            builder.RegisterType<ConferenceRepository>().As<IConferenceRepository>();
            builder.RegisterType<DivisionRepository>().As<IDivisionRepository>();
            builder.RegisterType<TeamRepository>().As<ITeamRepository>();
            builder.RegisterType<GameRepository>().As<IGameRepository>();

            return builder.Build();
        }

        static void Main(string[] args)
        {
            Console.Out.WriteLine(DateTime.Now + @": Live feed update process started");

            UpdateDbScores();
            Console.Out.WriteLine(DateTime.Now + @": First Update completed");

            Console.Out.WriteLine(DateTime.Now + @": Getting copy");
            _previousContent = GetLiveFeedResult();
            Console.Out.WriteLine(DateTime.Now + @": Copy obtained");

            Console.Out.WriteLine(DateTime.Now + @": Interval process started");
            do
            {
                Console.Out.WriteLine(DateTime.Now + @": Awaiting process execution");
                Thread.Sleep(600000);
                try
                {
                    UpdateDbScores();

                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(DateTime.Now + @": An error ocurred: " + ex.Message);
                    _noErrors = false;
                }
            } while (_noErrors);
        }

        private static void UpdateDbScores()
        {
            Console.Out.WriteLine(DateTime.Now + @": Getting live feed scores");
            _content = GetLiveFeedResult();
            Console.Out.WriteLine(DateTime.Now + @": Scores received");

            if (!_content.Equals(_previousContent))
            {
                Console.Out.WriteLine(DateTime.Now + @": Converting live feed to db game data");
                GetLiveFeedGames(_content);
                Console.Out.WriteLine(DateTime.Now + @": Live Feed data converted");

                var container = ConfigureAutofac();
                using (var scope = container.BeginLifetimeScope())
                {
                    var gameRepository = scope.Resolve<IGameRepository>();
                    Console.Out.WriteLine(DateTime.Now + @": Updating db game data");
                    UpdateDbGames(gameRepository);
                    Console.Out.WriteLine(DateTime.Now + @": Game data updated");
                }
            }
            else
            {
                Console.Out.WriteLine(DateTime.Now + @": Scores haven't changed");
            }
            Console.Out.WriteLine(DateTime.Now + @": Process completed");
        }

        private static string GetLiveFeedResult()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["NflLiveScoreFeedUrl"]);
            var request = new RestRequest { Method = Method.GET };
            var response = client.Execute(request);
            var content = response.Content;
            return content;
        }

        private static void UpdateDbGames(IGameRepository gameRepository)
        {
            foreach (var game in Games)
            {
                var dbGame = gameRepository.GetGame(new DateTime(Convert.ToInt32(game.GameId.Substring(0, 4)),
                    Convert.ToInt32(game.GameId.Substring(4, 2)),
                    Convert.ToInt32(game.GameId.Substring(6, 2))), game.HomeScore.Abbr, game.AwayScore.Abbr);
                if (game.AwayScore.Score.Final != 0)
                {
                    dbGame.AwayScore = game.AwayScore.Score.Final;
                    dbGame.HomeScore = game.HomeScore.Score.Final;
                }
                else
                {
                    dbGame.AwayScore = game.AwayScore.Score.FirstQuarter +
                                       game.AwayScore.Score.SecondQuarter +
                                       game.AwayScore.Score.ThirdQuarter +
                                       game.AwayScore.Score.FourthQuarter;
                    dbGame.HomeScore = game.HomeScore.Score.FirstQuarter +
                                       game.HomeScore.Score.SecondQuarter +
                                       game.HomeScore.Score.ThirdQuarter +
                                       game.HomeScore.Score.FourthQuarter;
                }
                gameRepository.UpdateGame(dbGame);
            }
        }

        private static void GetLiveFeedGames(string content)
        {
            content = "[" + content.Substring(1, content.Length - 2) + "]";
            var regEx = new Regex("\"\\d{10}\"\\:");
            foreach (var match in regEx.Matches(content))
            {
                content = content.Replace(match.ToString(), "\"GameId\":" + match.ToString().Replace(":", ","));
            }
            var splitContent = content.Split(new[] { "\"GameId\"" }, StringSplitOptions.None);
            for (var i = 1; i < splitContent.Length; i++)
            {
                var split = "\"GameId\"" + splitContent[i];
                split = split.Substring(0, split.Length - 1);
                split = split.Replace(@"{""home""", "\"HomeScore\"");
                split = split.Replace(@"""away""", "\"AwayScore\"");
                split = split.Replace(@"""1""", "\"FirstQuarter\"");
                split = split.Replace(@"""2""", "\"SecondQuarter\"");
                split = split.Replace(@"""3""", "\"ThirdQuarter\"");
                split = split.Replace(@"""4""", "\"FourthQuarter\"");
                split = split.Replace(@"""5""", "\"Overtime\"");
                split = split.Replace(@"""T""", "\"Final\"");
                split = split.Substring(0, split.IndexOf(",\"bp\"", StringComparison.Ordinal));
                split = split.Replace("null", "0");
                split = "{" + split + "}";
                Games.Add(JsonConvert.DeserializeObject<GameLiveFeed>(split));
            }
        }
    }
}