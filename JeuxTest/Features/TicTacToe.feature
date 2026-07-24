Feature: TicTacToe

TicTacToe testing scenarios

Background: 
	Given start game

# ------------- Cas nominaux ------------ #
Scenario: Alternate plays
	When player1 puts 'X' at (1,1)
	Then should be turn of player2
	When player2 puts 'O' at (1,2)
	Then should be turn of player1

Scenario: Player1 wins on a row
	When player1 puts 'X' at (0,0)
	And player2 puts 'O' at (1,2)
	And player1 puts 'X' at (0,1)
	And player2 puts 'O' at (2,2)
	And player1 puts 'X' at (0,2)
	Then player1 should win

Scenario: Player1 wins on a column
	When player1 puts 'X' at (0,0)
	And player2 puts 'O' at (1,2)
	And player1 puts 'X' at (0,1)
	And player2 puts 'O' at (2,2)
	And player1 puts 'X' at (0,2)
	Then player1 should win

Scenario: Player2 wins on a diagonal (left to right)
	When player1 puts 'X' at (1,2)
	And player2 puts 'O' at (0,0)
	And player1 puts 'X' at (1,0)
	And player2 puts 'O' at (1,1)
	And player1 puts 'X' at (2,0)
	And player2 puts 'O' at (2,2)
	Then player2 should win

Scenario: Player2 wins on a diagonal (right to left)
	When player1 puts 'X' at (1,2)
	And player2 puts 'O' at (0,2)
	And player1 puts 'X' at (1,0)
	And player2 puts 'O' at (1,1)
	And player1 puts 'X' at (2,2)
	And player2 puts 'O' at (2,0)
	Then player2 should win

Scenario: Draw (grid filled)
    When player1 puts 'X' at (0,0)
    And player2 puts 'O' at (1,0)
    And player1 puts 'X' at (2,0)
    And player2 puts 'O' at (1,1)
    And player1 puts 'X' at (0,1)
    And player2 puts 'O' at (2,1)
    And player1 puts 'X' at (1,2)
    And player2 puts 'O' at (0,2)
    And player1 puts 'X' at (2,2)
    Then game should be a draw

# ------------- Cas limites ------------- #
Scenario: Player1 wins at the last move
	When player1 puts 'X' at (0,0)
    And player2 puts 'O' at (2,0)
    And player1 puts 'X' at (1,1)
    And player2 puts 'O' at (0,1)
    And player1 puts 'X' at (1,0)
    And player2 puts 'O' at (1,2)
    And player1 puts 'X' at (2,1)
    And player2 puts 'O' at (0,2)
    And player1 puts 'X' at (2,2)
    Then player1 should win

# ------------- Cas d'erreurs ----------- #
Scenario: Cannot play on occupied cell
    When player1 puts 'X' at (0,0)
    And player2 puts 'O' at (0,0)
    Then an error should be thrown because the cell is already occupied

Scenario: Cannot play outside of the grid
    When player1 puts 'X' at (3,0)
    Then an error should be thrown because the cell is out of grid

Scenario: Cannot play two times in a row
	When player1 puts 'X' at (1,1)
	And player1 puts 'X' at (1,2)
	Then an error should be thrown because the wrong player tried to play

Scenario: Cannot play when game is over
	When player1 puts 'X' at (0,0)
	And player2 puts 'O' at (1,2)
	And player1 puts 'X' at (0,1)
	And player2 puts 'O' at (2,2)
	And player1 puts 'X' at (0,2)
	Then player1 should win
	When player2 puts 'O' at (1,0)
	Then an error should be thrown because the game is over