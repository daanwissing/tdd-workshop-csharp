using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TennisKata
{
    public class TennisGame
    {
        private readonly IReadOnlyDictionary<int, string> _scorePrints = new Dictionary<int, string>
        {
            { 0, "Love" },
            { 1, "Fifteen" },
            { 2, "Thirty" },
            { 3, "Forty" }
        };

        private int scorePlayer1;
        private int scorePlayer2;

        private bool player1CanChallenge = true;
        private bool player2CanChallenge = true;
        private Player? winner;

        private Player? previousScorer;

        public string PrintScore()
        {
            if (scorePlayer1 >= 3 && scorePlayer2 == scorePlayer1)
            {
                return "Deuce";
            }

            if (winner.HasValue)
            {
                return $"Winner {winner.Value.GetDisplayName()}";
            }

            static bool hasAdvantage(int score) => score >= 4;
            if (hasAdvantage(scorePlayer1))
            {
                return $"Advantage {Player.Player1.GetDisplayName()}";
            }

            if (scorePlayer2 >= 4)
            {
                return $"Advantage {Player.Player2.GetDisplayName()}";
            }

            var player1Score = _scorePrints[scorePlayer1];
            string player2Score = scorePlayer1 == scorePlayer2 ? "All" : _scorePrints[scorePlayer2];
            return $"{player1Score} {player2Score}";
        }

        public void Player1Scores()
        {
            scorePlayer1++;
            previousScorer = Player.Player1;
            CheckWinner();
        }

        public void Player2Scores()
        {
            scorePlayer2++;
            previousScorer = Player.Player2;
            CheckWinner();
        }

        public void Reset()
        {
            scorePlayer1 = 0;
            scorePlayer2 = 0;
            winner = null;
        }

        private void CheckWinner()
        {
            static bool isAWinner(int score1, int score2) =>
                score1 >= 4 && score1 - score2 >= 2;

            if (isAWinner(scorePlayer1, scorePlayer2))
            {
                winner = Player.Player1;
            }

            if (isAWinner(scorePlayer2, scorePlayer1))
            {
                winner = Player.Player2;
            }
        }

        public void Challenge(bool sustained)
        {
            if (previousScorer == Player.Player1 && player2CanChallenge)
            {
                if (sustained)
                {
                    scorePlayer1--;
                    winner = null;
                    CheckWinner();
                }
                else
                    player2CanChallenge = false;
            }
            if (previousScorer == Player.Player2 && player1CanChallenge)
            {
                if (sustained)
                {
                    scorePlayer2--;
                    winner = null;
                    CheckWinner();
                }
                else
                    player1CanChallenge = false;
            }
        }
    }

    public enum Player
    {
        [Display(Name = "Player 1")]
        Player1,

        [Display(Name = "Player 2")]
        Player2,
    }

    public static class EnumExtensions
    {
        public static string GetDisplayName<T>(this T enumValue) where T : Enum
        {
            return enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .Name;
        }
    }
}