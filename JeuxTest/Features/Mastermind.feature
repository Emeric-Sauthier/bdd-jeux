Feature: Mastermind

Mastermind testing scenarios

Background:
	Given start mastermind game

# ------------- Cas nominaux ------------ #
Scenario: Incorrect answer
	Given the secret code is "White White White White"
	When the codebreaker proposes "Black Black Black Black"
	Then the result of the proposition should be "Wrong Wrong Wrong Wrong"

Scenario: Code found
	Given the secret code is "White White White White"
	When the codebreaker proposes "White White White White"
	Then the result of the proposition should be "WellPlaced WellPlaced WellPlaced WellPlaced"
	And player1 should win

Scenario: Some wellplaced / wrong / misplaced
	Given the secret code is "White Black Red Green"
	When the codebreaker proposes "Red Black Blue Green"
	Then the result of the proposition should be "Misplaced WellPlaced Wrong WellPlaced"

# ------------- Cas limites ------------- #
Scenario: Duplicate colors
	Given the secret code is "White Black White Green"
	When the codebreaker proposes "Black White White White"
	Then the result of the proposition should be "Misplaced Misplaced WellPlaced Wrong"

Scenario: Last round win
	Given the secret code is "White Black White Green"
	When the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "White Black White Green"
	Then player1 should win
	And the result of the proposition should be "WellPlaced WellPlaced WellPlaced WellPlaced"

Scenario: Lose after 10 rounds
	Given the secret code is "White Black White Green"
	When the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	Then the codebreaker should loose

# ------------- Cas d'erreurs ----------- #
Scenario: Wrong code length (set code)
	Given the secret code is "White"
	Then an error should be throw because the code length is invalid
	Given the secret code is "White White White White White"
	Then an error should be throw because the code length is invalid

Scenario: Wrong code length (proposition)
	When the codebreaker proposes "White"
	Then an error should be throw because the code length is invalid
	When the codebreaker proposes "White White White White White"
	Then an error should be throw because the code length is invalid

Scenario: Wrong color (set code)
	Given the secret code is "Purple White White White"
	Then an error should be throw because one color is invalid

Scenario: Wrong color (proposition)
	When the codebreaker proposes "Purple White White White"
	Then an error should be throw because one color is invalid

Scenario: Cannot play after the game is over
	Given the secret code is "White Black White Green"
	When the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	And the codebreaker proposes "Black White White White"
	Then an error should be thrown because the game is over