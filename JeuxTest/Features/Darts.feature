Feature: Darts

Darts testing scenarios

Background:
	Given start darts game

# ------------- Cas nominaux ------------ #
Scenario: Players have a score of 301 at game debut
	Then player1 should have a score of 301
	And player2 should have a score of 301

Scenario: Alternate plays
	When player1 throws a dart and makes a simple 20
	And player1 throws a dart and makes a simple 20
	And player1 throws a dart and makes a simple 20
	Then should be turn of player2
	When player2 throws a dart and makes a simple 20
	And player2 throws a dart and makes a simple 20
	And player2 throws a dart and makes a simple 20
	Then should be turn of player1

Scenario: A volley is substracted from player's score
	When player1 throws a dart and makes a simple 20
	And player1 throws a dart and makes a double 20
	And player1 throws a dart and makes a triple 20
	Then player1 should have a score of 181

Scenario: Missed darts score zero
	When player1 throws a dart outside of the target
	And player1 throws a dart outside of the target
	And player1 throws a dart outside of the target
	Then player1 should have a score of 301

Scenario: Make a double on the last throw to win
	Given player1 has a score of 40
	When player1 throws a dart and makes a double 20
	Then player1 should have a score of 0
	And player1 should win

Scenario: A bull scores 25
	When player1 throws a dart and makes a simple 25
	Then player1 should have a score of 276

# ------------- Cas limites ------------- #
Scenario Outline: Checkouts
	Given player1 has a score of <score>
	When player1 throws a dart and makes a <multiplier1> <first>
	And player1 throws a dart and makes a <multiplier2> <second>
	And player1 throws a dart and makes a double <third>
	Then player1 should have a score of 0
	And player1 should win
	And should be turn of player1
	Examples:
	| score | first | multiplier1 | second | multiplier2 | third |
	|   100 |    20 | simple      |     20 | double      |    20 |
	|   120 |    20 | simple      |     25 | double      |    25 |
	|   122 |    20 | triple      |     20 | triple      |     1 |

Scenario Outline: Bust scenarios
	Given player1 has a score of <score>
	When player1 throws a dart and makes a <multiplier> <sector>
	Then player1 should have a score of <score>
	And should be turn of player2
	Examples: 
	| score  | sector | multiplier |
	|     2  |      1 | simple     |
	|     25 |     25 | simple     |
	|     2  |      2 | simple     |
	|     3  |      1 | triple     |

# ------------- Cas d'erreurs ----------- #
Scenario: Cannot throw at an invalid sector
    When player1 throws a dart and makes a simple 21
    Then an error should be thrown because the sector is invalid

Scenario: Cannot make a triple bull
    When player1 throws a dart and makes a triple 25
    Then an error should be thrown because the multiplier is invalid

Scenario: Cannot throw a fourth dart in the same volley
    When player1 throws a dart and makes a simple 20
    And player1 throws a dart and makes a simple 20
    And player1 throws a dart and makes a simple 20
    And player1 throws a dart and makes a simple 20
    Then an error should be thrown because the wrong player tried to play

Scenario: Cannot play after the game is over
    Given player1 has a score of 40
    When player1 throws a dart and makes a double 20
    Then player1 should win
    When player2 throws a dart and makes a simple 20
    Then an error should be thrown because the game is over