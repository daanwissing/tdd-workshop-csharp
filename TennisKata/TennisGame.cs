namespace TennisKata
{
    public class TennisGame
    {
        private readonly IReadOnlyDictionary<int, string> _scorePrints = new Dictionary<int, string>
        {
            {0, "Love" },
            {1, "Fifteen" },
            {2, "Thirty" },
            {3, "Forty" }
        };

        private int score1;
        private int score2;

        private string? winner;

        public string PrintScore()
        {
            if (score1 >= 3 && score2 == score1)
            {
                return "Deuce";
            }

            if (!string.IsNullOrEmpty(winner))
            {
                return $"Winner {winner}";
            }

            if (score1 >= 4)
            {
                return "Advantage Player 1";
            }

            if (score2 >= 4)
            {
                return "Advantage Player 2";
            }

            var player1Score = _scorePrints[score1];
            string player2Score = score1 == score2 ? "All" : _scorePrints[score2];
            return $"{player1Score} {player2Score}";
        }

        public void Player1Scores()
        {
            score1++;
            CheckWinner();
        }

        public void Player2Scores()
        {
            score2++;
            CheckWinner();
        }

        private void CheckWinner()
        {
            if (score1 >= 4)
            {
                if (score1 - score2 >= 2)
                {
                    winner = "Player 1";
                }
            }

            if (score2 >= 4)
            {
                if (score2 - score1 >= 2)
                {
                    winner = "Player 2";
                }
            }
        }
    }
}