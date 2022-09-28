# TDD Workshop

## Git url: 

- https://github.com/daanwissing/tdd-workshop-csharp.git  

## Tennis game requirement: 

> Implement a function that, given a tennis game, prints out the current score of the game.

## Tennis game context:

1.	Two players play against each other.
2.	On every play, one player can score a point.
3.	In the end, one of the two players win (i.e. no tie..

## Tennis game rules:

1.	Players start with zero points each game. Having zero points in a game is denote as “love”.
2.	When both players have the same score, the notation for this is “`\<score>` all”.
3.	When a player has 1 point, the score is noted as “fifteen”
4.	When a player has 2 points, the score is noted as “thirty”
5.	When a player has 3 points, the score is noted as “forty”
6.	When both players have 3 points, the total score is noted as “Deuce”
7.	When a player has 3 points and wins the play, he wins the game (“Winner player 1” or “Winner player 2”)
8.	When the score is “Deuce” the following rules apply:
    1.	The next player to win a play, has “Advantage”
    2.	When a player has “Advantage” and wins a play, the player wins the game.
    3.	When a player has “Advantage” but the other player wins the play, the score is back at “Deuce”

## Extra:

1.	Once a player has won a game, no points can be won anymore and printing the score will keep showing the same message.
2.	A player can challenge a point won by the opponent. If the umpire sustains the challenge, the previous point is negated. If the challenge is dismissed, the score stands.
3.	A player can only challenge once in a game.
4.	If a player’s challenge is sustained, he keeps his option to challenge.
5.	It is possible to reset a game, thereby setting the score of both players back to zero.
