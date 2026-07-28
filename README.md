# BDD – Projet Jeux

**Contributeur :** SAUTHIER Emeric

## Choix des jeux

Trois jeux sont implémentés (minimum imposé par le sujet) :

| Jeu | Variante retenue |
|-----|------------------|
| **TicTacToe** (Morpion) | Grille 3×3, 2 joueurs |
| **Fléchettes** | **301**, sortie sur double (*double-out*), gestion du *bust* |
| **Mastermind** | 6 couleurs, code de 4 pions, doublons autorisés, 10 essais |


## 1. Les jeux : règles et vocabulaire métier

Pour chaque jeu : **principe & règles** telles qu'implémentées, et **vocabulaire métier** réutilisé
tel quel dans les scénarios. La stratégie de couverture est traitée au §2.1.

### 1.1 TicTacToe (Morpion)

**Principe & règles**  
Grille 3×3, deux joueurs (`X` et `O`), `X` commence. À son tour, un
joueur place son symbole sur une case vide. La partie est gagnée dès que trois symboles
identiques sont alignés (ligne, colonne ou diagonale). Si la grille est remplie sans
alignement, la partie est nulle. La détection de victoire est évaluée **avant** celle du match nul :
un 9e coup gagnant donne une victoire, pas une partie nulle.

**Vocabulaire métier :** grille, case, symbole, joueur, tour, alignement, victoire, match nul.

### 1.2 Fléchettes — 301 (double-out)

**Principe & règles**  
Deux joueurs partent chacun de **301** points. À chaque tour, un joueur
lance une **volée de 3 fléchettes** ; chaque fléchette rapporte des points selon le secteur
touché et le multiplicateur (**simple**, **double**, **triple**). Le total de la volée est
**soustrait** du score, et la main passe au joueur suivant dès la 3e fléchette.

Secteurs valides :
- **1 à 20**, avec les trois multiplicateurs ;
- **25** (*bull*) en simple (25 points) ou en double (*double-bull*, 50 points)
- **0** : fléchette **manquée**, qui ne retire aucun point.

Règles spécifiques :
- **Double-out** : pour gagner, il faut atteindre **exactement 0** en terminant sur un **double**
  (ou un double-bull).
- **Bust** : si un lancer fait passer le score en dessous de 0, exactement à 1, ou à 0 sans
  double final, la volée est **annulée** : le score revient à sa valeur en début de tour et la
  main passe au joueur suivant.
- **Sortie sur un double uniquement** : une fléchette qui amène le score à 0 autrement que par
  un double (simple, triple, ou bull simple = 25) est un *bust*.

**Vocabulaire métier** : volée, secteur, simple / double / triple, bull, double-bull, checkout,
double-out, bust, fléchette manquée (secteur 0).

### 1.3 Mastermind (6 couleurs / 4 positions)

**Principe & règles**  
La partie se déroule en deux temps. Le **poseur de code** définit d'abord un **code secret** de
4 pions à partir de **6 couleurs** (les doublons sont autorisés). La partie ne démarre qu'à ce
moment, et le code **ne peut plus être modifié** ensuite. Le **codebreaker** dispose alors de
**10 manches** pour le retrouver.

À chaque **essai**, le codebreaker propose une combinaison de 4 pions. Le jeu renvoie, **pour
chaque position de la proposition**, l'un des trois indices suivants :
- **bien placé** (bonne couleur **et** bonne position) ;
- **mal placé** (bonne couleur, mauvaise position) ;
- **faux** (couleur absente du code, ou déjà appariée).

Le comptage apparie chaque pion **une seule fois**, ce qui rend le calcul subtil en présence de
doublons. Le codebreaker **gagne** dès 4 pions bien placés ; il **perd** si les **10 essais** sont
épuisés sans trouver.


**Vocabulaire métier** : code secret, poseur de code, codebreaker, proposition (combinaison), pion,
couleur, essai (*manche*), indice, bien placé, mal placé, faux.

---

## 2. Analyse et justification des scénarios

### 2.1 Identification des cas de test

**Grille de lecture commune**  
Pour chaque jeu, la couverture est bâtie sur trois familles :
- les **cas nominaux** : le déroulé attendu et la condition de victoire
- les **cas limites** : les frontières exactes des règles (dernier coup, dernière manche, valeurs qui basculent d'un état à l'autre)
- les **cas d'erreur** : les entrées invalides et les actions interdites.

#### TicTacToe

- *Cas nominaux* : 
    - Coups alternés valides
    - Victoire par ligne
    - Victoire par colonne
    - Victoire par diagonale (les deux sens)
    - Match nul
- *Cas limites* :
    - Victoire obtenue au 9e (dernier) coup
- *Cas d'erreurs* : 
    - Jouer sur une case déjà occupée
    - Jouer hors de la grille
    - Jouer deux fois de suite
    - Jouer après la fin de partie


| Famille | Scénarios |
|---------|-----------|
| Nominaux | `Alternate plays`, `Player1 wins on a row`, `Player1 wins on a column`, `Player2 wins on a diagonal (left to right)`, `Player2 wins on a diagonal (right to left)`, `Draw (grid filled)` |
| Limites | `Player1 wins at the last move` |
| Erreurs | `Cannot play on occupied cell`, `Cannot play outside of the grid`, `Cannot play two times in a row`, `Cannot play when game is over` |

#### Fléchettes

- *Cas nominaux* : 
    - Les deux joueurs commencent avec 301 points
    - Soustraction d'une volée
    - Alternance des deux joueurs
    - Fléchette manquée : aucun point retiré
    - Le bull simple vaut 25 points
    - Checkout gagnant : dernière fléchette sur un double
- *Cas limites* : 
    - Checkout minimal (reste 2 → D1)
    - Checkout terminé sur un double-bull
    - Score ramené exactement à 1 (*bust*)
    - 0 atteint sur un simple ou un triple (*bust*)
    - Bust à la 2e ou 3e fléchette
- *Cas d'erreurs* : 
    - Secteur invalide (au-delà de 20, hors bull)
    - Multiplicateur invalide (triple bull)
    - 4e fléchette dans une volée = lancer **hors tour**
    - Jeu après la victoire

| Famille | Scénarios |
|---------|-----------|
| Nominaux | `Players have a score of 301 at game debut`, `A volley is substracted from player's score`, `Alternate plays`, `Missed darts score zero`, `A bull scores 25`, `Make a double on the last throw to win` |
| Limites | `Checkouts` (3 exemples : checkout classique, checkout au double-bull, checkout minimal reste 2 → D1), `Bust scenarios` (4 exemples : score ramené à 1, 0 sur un simple, 0 sur un triple) |
| Erreurs | `Cannot throw at an invalid sector`, `Cannot make a triple bull`, `Cannot throw a fourth dart in the same volley`, `Cannot play after the game is over` |

#### Mastermind

- *Cas nominaux* :
    - Proposition entièrement fausse
    - Mélange de pions bien et mal placés
    - Code trouvé (4 pions bien placés → victoire)
- *Cas limites* : 
    - Doublons dans le code et/ou dans la proposition (exactitude du comptage)
    - Victoire au 10e (dernier) essai
    - Défaite après 10 essais
- *Cas d'erreurs* :
    - Longueur de code incorrecte
    - Couleur hors palette
    - Modification du code secret après le démarrage de la partie
    - Proposition avant qu'un code secret soit défini
    - Proposition de longueur incorrecte
    - Couleur hors palette
    - Jeu après la fin de partie

| Famille | Scénarios |
|---------|-----------|
| Nominaux | `Incorrect answers` (3 exemples : aucune couleur commune, mélange bien/mal placés, proposition à doublons), `Code found` |
| Limites | `Last round win`, `Lose after 10 rounds` |
| Erreurs — pose du code | `Wrong code length (set code)`, `Wrong color (set code)`, `Cannot change the secret code once the game has started` |
| Erreurs — codebreaker | `Cannot propose before the secret code is set`, `Wrong code length (proposition)`, `Wrong color (proposition)`, `Cannot play after the game is over` |

### 2.2 Priorisation des scénarios

Les **scénarios critiques** correspondent au fonctionnement de base des jeux :

1. l'**initialisation** d'une partie
2. l'**action de base et l'alternance des tours**
3. la **condition de victoire**

Les **scénarios limites** correspondent aux règles spécifiques des jeux, sortant du déroulement idéal d'une partie. Il s'agit de règles, parfois subtiles, qui peuvent se montrer ambigues ou être propice à l'erreur.  
Par exemple : le *double-out* et le *bust* aux fléchettes,
la gestion des doublons au Mastermind, la priorité victoire/match nul au morpion.
Ces scénarios ont été écrits avant le développement, afin de fixer l'attendu et limiter les erreurs d'interprétation et d'implémentation.

Les **scénarios secondaires** correspondent aux cas d'erreur. Ils ont été écrits en dernier, car ils représentent des cas qui découlent des cas nominaux et limites. Ils garantissent que les règles et le cycle de vie des jeux soient respectés.

L'application de la méthode BDD, et la priorisation des scénarios de test ont permis de guider le développement et de contrôler la bonne implémentation de la logique métier pour chaque jeu.

---

## 3. Architecture et représentation des données

### 3.1 Lisibilité des données de test

**Background**  
Chaque feature définit un **`Background`**, afin de mutualiser l'initialisation du jeu concerné, pour tous les scénarios de test renseignés. 

Exemple pour le Mastermind :
```gherkin
Background:
	Given start mastermind game
	And the secret code is "White Black White Green"
```

Cette décision **améliore la lisibilité** des scénarios, qui exécutent uniquement les actions correspondantes, sans gérer l'initialisation. Les scénarios nécessitant une initialisation (ex : pose d'un code au Mastermind) le déclare explicitement.

**Scenario Outline**  
Lors de la rédaction des scénarios, certains avaient la **même structure**, mais n'utilisaient pas les mêmes valeurs. Afin d'**améliorer la lisibilité** des scénarios, ils ont été factorisés grâce auX **`Scenario Outline`**. Les **données de test**, sont renseignées dans une table, sous la balise **`Examples`**, mettant ainsi en évidence les différences entre les cas de test.

Exemple pour le bust aux fléchettes :
```gherkin
Scenario Outline: Bust scenarios
	Given player1 has a score of <score>
	When player1 throws a dart and makes a <multiplier> <sector>
	Then player1 should have a score of <score>
	And should be turn of player2
	Examples: 
	| score  | sector  | multiplier |
	|     2  |      1  | simple     |
	|     25 |     25  | simple     |
	|     2  |      2  | simple     |
	|     3  |      1  | triple     |
	|     3  |      10 | triple     |
```

Dans cet exemple, les scénarios de *bust* sont centralisés, et l'on distingue facilement quels cas sont renseignés. De plus, pour ajouter un cas de *bust*, il suffit de rajouter une ligne au tableau.  
Le même
raisonnement s'applique aux `Checkouts` (fléchettes) et aux `Incorrect answers` (Mastermind).

**Raccourci pour les tests**  
Le step `Given player1 has a score of 40` affecte directement un score de 40 au joueur 1, afin de se concentrer sur la règle testée, et limiter le nombre d'étape pour atteindre l'assertion de celle-ci.

Exemple pour le test d'un checkout avec le step :
```gherkin
Scenario: Make a double on the last throw to win
	Given player1 has a score of 40
	When player1 throws a dart and makes a double 20
	Then player1 should have a score of 0
	And player1 should win
```

Exemple pour le test d'un checkout sans le step :
```gherkin
Scenario: Make a double on the last throw to win
	When player1 throws a dart and makes a triple 20
  And player1 throws a dart and makes a triple 20
  And player1 throws a dart and makes a triple 20
  And player2 throws a dart and makes a triple 20
  And player2 throws a dart and makes a triple 20
  And player2 throws a dart and makes a triple 20
	And player1 throws a dart and makes a triple 20
  And player1 throws a dart and makes a simple 10
  And player1 throws a dart and makes a simple 11
  And player2 throws a dart and makes a triple 10
  And player2 throws a dart and makes a triple 10
  And player2 throws a dart and makes a triple 10
  And player1 throws a dart and makes a double 20
	Then player1 should have a score of 0
	And player1 should win
```

### 3.2 Extensibilité

**Ajout d'un jeu**  
Pour ajouter un jeu, il suffit de :
- Créer un dossier pour celui-ci
- Créer la classe implémentant une interface de jeu (`IGame` ou `IScoredGame`)
- Créer les éléments spécifiques (enums, exceptions, modèles)
- Créer la classe de steps
- Créer le fichier `.feature`

Il peut être nécessaire de créer une nouvelle interface, afin de prendre en compte un autre type de jeu. Dans ce cas ci, il faudrait créer une classe steps afin de tester ses fonctionnalités spécifiques.

Pour les jeux proposés dans le sujet (**tennis** et **bowling**), il faudrait certainement implémenter l'interface `IScoredGame`, afin que les jeux héritent de la gestion des scores.

**Modifier une règle existante**  
Les paramètres de règle sont isolés en constantes dans les classes respectives des jeux.

Exemples :
| Modification | Point d'entrée |
|--------------|----------------|
| Passer les fléchettes en 501 | `Darts.StartScore` |
| Autoriser 12 essais au Mastermind | `Mastermind.MaxRound` |
| Code secret de 5 pions | `Mastermind.CodeLength` |

**Limite d'extensibilité**  
La taille de la grille de morpion n'est pas paramétrable, malgré la
constante `BoardDimension`. En effet, le tableau est initialisé en `char[3,3]`, et la détection de diagonale
énumère les indices `0`, `1`, `2` en dur.  
Modifier la taille de la grille impliquerait donc de modifier la logique de vérification.

---

## 4. Stratégie BDD et bonnes pratiques

### 4.1 Langage ubiquitaire

Les scénarios sont écrits avec les mots des règles du §1, sans terme technique. La règle du
*double-out* se relit directement dans le scénario qui la spécifie :

```gherkin
Given player1 has a score of 40
When player1 throws a dart and makes a double 20
Then player1 should have a score of 0
And player1 should win
```

Aucun mot de ce scénario n'appartient au vocabulaire du développeur : *score*, *throws a dart*,
*double 20*, *win* sont ceux d'un joueur de fléchettes. Il en va de même au Mastermind, où les
acteurs et les indices portent leur nom métier :

```gherkin
Given the secret code is "White Black White Green"
When the codebreaker proposes "Black White Blue Green"
Then the result of the proposition should be "Misplaced Misplaced Wrong WellPlaced"
```

Voici la correspondance entre les termes français, utilisés dans ce document, et les termes anglais, utilisés dans le code :

| Français (document) | Anglais (code et scénarios) |
|---------------------|-----------------------------|
| grille / case | `Board` / cell |
| tour, à qui de jouer | `TurnTo` |
| volée, fléchette | volley, `Dart` |
| secteur, multiplicateur | `Sector`, `Multiplier` |
| bust, checkout | *bust*, *checkout* (termes conservés) |
| essai / manche | `Round` |
| bien placé / mal placé / faux | `WellPlaced` / `Misplaced` / `Wrong` |
| poseur de code / codebreaker | code setter / `codebreaker` |

### 4.2 Réutilisabilité : steps communs vs. spécifiques

**Steps communs**  
Les jeux implémentent les interfaces suivantes : `IGame` ou `IScoredGame`. Les jeux partagent donc des fonctionnalités grâce à ces interfaces.
Les steps concernant les fonctionnalités de ces interfaces ont été centralisés dans les classes suivantes : `GameStepsDefinition` pour `IGame`, et `ScoredGameStepsDefinition` pour `IScoredGame`.
Cela évite donc d'écrire, pour chaque jeu, les steps : `Then player1 should win`, `Then an error should be thrown because the game is over`, etc.
Les classes de steps sont donc réduites à la logique stricte du jeu.

**Contexte partagé**  
Un objet `GameStepsContext`, contenant la partie courante et la dernière exception levée, est injecté par constructeur via l'injection de dépendances de Reqnroll.  
Ainsi, tous les scénarios possèdent leur contexte, et les steps communs manipulent la partie à travers son interface (`IGame` ou `IScoredGame`).

**Transformations d'arguments** `StepTransformations` convertit `player1`/ `player2` en `Player` et `simple|double|triple` en `Multiplier`.
Cela permet de réutiliser ces conversions dans les signatures de tous les steps, et de condenser plusieurs steps en un seul.

Exemples pour les steps de lancer de fléchette avec transformation :
```csharp
[When(@"^(player\d) throws a dart and makes a (simple|double|triple) (\d+)$")]
public void WhenPlayerThrowsDart(Player player, Multiplier multiplier, int sector)
{
  Play(player, sector, multiplier);
}

[When(@"^(player\d) throws a dart outside of the target$")]
public void WhenPlayerMissesDart(Player player)
{
  Play(player, 0, Multiplier.Simple);
}
```

Exemples pour les steps de lancer de fléchette sans transformation :
```csharp
[When("player1 throws a dart and makes a simple {int}")]
public void WhenPlayer1MakesSimple(int  value)
{
  Play(Player.Player1, value, Multiplier.Simple);
}

[When("player1 throws a dart and makes a double {int}")]
public void WhenPlayer1MakesDouble(int value)
{
  Play(Player.Player1, value, Multiplier.Double);
}

[When("player1 throws a dart and makes a triple {int}")]
public void WhenPlayer1MakesTriple(int value)
{
  Play(Player.Player1, value, Multiplier.Triple);
}

[When("player1 throws a dart outside of the target")]
public void WhenPlayer1ThrowsDartOutside()
{
  Play(Player.Player1, 0, Multiplier.Simple);
}

[When("player2 throws a dart and makes a simple {int}")]
public void WhenPlayer2MakesSimple(int value)
{
  Play(Player.Player2, value, Multiplier.Simple);
}

[When("player2 throws a dart and makes a double {int}")]
public void WhenPlayer2MakesDouble(int value)
{
  Play(Player.Player2, value, Multiplier.Double);
}

[When("player2 throws a dart and makes a triple {int}")]
public void WhenPlayer2MakesTriple(int value)
{
  Play(Player.Player2, value, Multiplier.Triple);
}

[When("player2 throws a dart outside of the target")]
public void WhenPlayer2ThrowsDartOutside()
{
  Play(Player.Player2, 0, Multiplier.Simple);
}
```

### 4.3 Maintenance

**Arborescence**   
L'arborescence de la librairie offre une lisibilité simple :
- Common => ce qui est utilisé par plusieurs jeux
- Darts => ce qui est relatif aus fléchettes uniquement
- Mastermind => ce qui est relatif au Mastermind uniquement 
- TicTacToe => ce qui est relatif au morpion uniquement

Cette arborescence est, en partie, reprise dans le projet de test. En effet, chaque jeu possède son fichier `.feature` et sa classe de steps.

Ce choix a été pris afin de faciliter la navigation dans les projets.

**Projet de tests**  
La présence d'un projet de tests, permet de garantir le fonctionnement des différents jeux. En effet, lors d'une modification de code, si l'un des scénarios est KO, alors il s'agit d'une erreur d'implémentation.

Les scénarios testent les règles des jeux, peu importe leur implémentation.

**Cas d'erreur**  
Les cas d'erreur testent le type de l'exception et non le message. Ainsi, si un message d'erreur est changé, alors le test continue de passer.

---