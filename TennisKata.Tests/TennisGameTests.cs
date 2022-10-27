using FluentAssertions;

namespace TennisKata.Tests
{
    [TestFixture]
    public class TennisGameTests
    {
        private TennisGame game;

        [SetUp]
        public void SetUp()
        {
            game = new TennisGame();
        }

        [TestCaseSource(nameof(GiveTestCases))]
        public void GivenScoreForPlayers_ExpectedScoreIsPrinted(int scorePlayer1, int scorePlayer2, string expectedScore)
        {
            // Arrange
            SetScore(scorePlayer1, scorePlayer2);

            // Act
            var result = game.PrintScore();

            // Assert
            result.Should().Be(expectedScore);
        }

        [Test]
        public void GivenDeuce_WhenPlayer1Scores_AdvantagePlayer1()
        {
            // Arrange
            SetScore(3, 3);
            game.Player1Scores();

            // Act
            var result = game.PrintScore();

            // Assert
            result.Should().Be("Advantage Player 1");
        }

        [Test]
        public void GivenDeuce_WhenPlayer1ScoresAndPlayer2Scores_DeuceAgain()
        {
            // Arrange
            SetScore(3, 3);
            game.Player1Scores();
            game.Player2Scores();

            // Act
            var result = game.PrintScore();

            // Assert
            result.Should().Be("Deuce");
        }

        [Test]
        public void GivenDeuce_WhenPlayer2ScoresAndPlayer1Scores_DeuceAgain()
        {
            // Arrange
            SetScore(3, 3);
            game.Player2Scores();
            game.Player1Scores();

            // Act
            var result = game.PrintScore();

            // Assert
            result.Should().Be("Deuce");
        }

        [Test]
        public void GivenScore_WhenResetGame_ScoreIsLoveAll()
        {
            // Arrange
            SetScore(1, 3);

            // Act
            game.Reset();
            var result = game.PrintScore();

            // Assert
            result.Should().Be("Love All");
        }

        [Test]
        public void GivenScore_WhenChallengeSustained_ScoreReverted()
        {
            //
            game.Player1Scores();
            game.Challenge(true);

            // Act
            var result = game.PrintScore();

            // Assert
            result.Should().Be("Love All");
        }


        [Test]
        public void GivenScore_WhenChallengeRejected_ScoreNotReverted()
        {
            //
            game.Player1Scores();
            game.Challenge(false);

            // Act
            var result = game.PrintScore();

            // Assert
            result.Should().Be("Fifteen Love");
        }

        [Test]
        public void GivenP1ScoreAndNoChallenges_WhenChallenge_ScoreNotReverted()
        {
            //
            game.Player1Scores();
            game.Challenge(false);
            game.Player1Scores();
            game.Challenge(false);

            // Act
            var result = game.PrintScore();

            // Assert
            result.Should().Be("Thirty Love");
        }

        [Test]
        public void GivenP1ScoreAndNoChallenges_WhenChallengeSustained_ScoreNotReverted()
        {
            //
            game.Player1Scores();
            game.Challenge(false);
            game.Player1Scores();
            game.Challenge(true);

            // Act
            var result = game.PrintScore();

            // Assert
            result.Should().Be("Thirty Love");
        }


        [Test]
        public void GivenP2ScoreAndNoChallenges_WhenChallengeSustained_ScoreNotReverted()
        {
            //
            game.Player2Scores();
            game.Challenge(false);
            game.Player2Scores();
            game.Challenge(true);

            // Act
            var result = game.PrintScore();

            // Assert
            result.Should().Be("Love Thirty");
        }

        [Test]
        public void GivenP2Wins_WhenChallengeSustained_ScoreReverted()
        {
            // Arrange
            SetScore(0, 4);

            // Act
            game.Challenge(true);
            var result = game.PrintScore();

            // Assert
            result.Should().Be("Love Forty");
        }

        [Test]
        public void GivenGameWinner_WhenResetGame_ScoreIsLoveAll()
        {
            // Arrange
            SetScore(1, 4);

            // Act
            game.Reset();
            var result = game.PrintScore();

            // Assert
            result.Should().Be("Love All");
        }

        private void SetScore(int scorePlayer1, int scorePlayer2)
        {
            for (int i = 0; i < scorePlayer1; i++)
            {
                game.Player1Scores();
            }
            for (int i = 0; i < scorePlayer2; i++)
            {
                game.Player2Scores();
            }
        }

        private static object[] GiveTestCases()
        {
            return new object[]
            {
                new object[]{0,0,"Love All" },
                new object[]{0,1,"Love Fifteen" },
                new object[]{0,2,"Love Thirty" },
                new object[]{0,3,"Love Forty" },
                new object[]{1,0,"Fifteen Love" },
                new object[]{1,1,"Fifteen All" },
                new object[]{1,2,"Fifteen Thirty" },
                new object[]{1,3,"Fifteen Forty" },
                new object[]{2,0,"Thirty Love" },
                new object[]{2,1,"Thirty Fifteen" },
                new object[]{2,2,"Thirty All" },
                new object[]{2,3,"Thirty Forty" },
                new object[]{3,0,"Forty Love" },
                new object[]{3,1,"Forty Fifteen" },
                new object[]{3,2,"Forty Thirty" },
                new object[]{3,3,"Deuce" },
                new object[]{4,0,"Winner Player 1" },
                new object[]{4,1,"Winner Player 1" },
                new object[]{4,2,"Winner Player 1" },
                new object[]{4,3,"Winner Player 1" }, // Player 1 was the first to get to 4 points with > 1 point difference. Player 2 scoring after that should be ignored
                new object[]{0,4,"Winner Player 2" },
                new object[]{1,4,"Winner Player 2" },
                new object[]{2,4,"Winner Player 2" },
                new object[]{3,4,"Advantage Player 2" },
            };
        }
    }
}