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

        [Test]
        public void WhenNoScore_PrintReturnsLoveAll()
        {
            var result = game.PrintScore();
            result.Should().Be("Love All");
        }
    }
}