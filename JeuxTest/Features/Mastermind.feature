Feature: Mastermind

Mastermind testing scenarios

Background:
	Given start mastermind game
	And the secret code is "White Black White Green"

# ------------- Cas nominaux ------------ #
Scenario Outline: Incorrect answers
	When the codebreaker proposes <proposition>
	Then the result of the proposition should be <result>
	Examples:
	| proposition               | result                                  |
	| "Blue Blue Blue Blue"     | "Wrong Wrong Wrong Wrong"               |
	| "Black White Blue Green"  | "Misplaced Misplaced Wrong WellPlaced"  |
	| "Black White White White" | "Misplaced Misplaced WellPlaced Wrong"  |

Scenario: Code found
	When the codebreaker proposes "White Black White Green"
	Then the result of the proposition should be "WellPlaced WellPlaced WellPlaced WellPlaced"
	And player1 should win

# ------------- Cas limites ------------- #
Scenario: Last round win
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
	Given start mastermind game
	And the secret code is "White"
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