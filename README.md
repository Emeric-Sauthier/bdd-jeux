# BDD – Projet Jeux

**Contributeur :** SAUTHIER Emeric

## Choix des jeux

Trois jeux sont implémentés (minimum imposé par le sujet) :

| Jeu | Variante retenue |
|-----|------------------|
| **TicTacToe** (Morpion) | Grille 3×3, 2 joueurs |
| **Fléchettes** | **301**, sortie sur double (*double-out*), gestion du *bust* |
| **Mastermind** | Classique : 6 couleurs, code de 4 pions, doublons autorisés, 10 essais |

---

## 1. Les jeux

Pour chaque jeu : **principe & règles**, **vocabulaire métier** (langage ubiquitaire réutilisé
dans les scénarios) et **stratégie de cas de tests** (nominaux / limites / erreurs).

### 1.1 TicTacToe (Morpion)

**Principe & règles**  
Grille 3×3, deux joueurs (`X` et `O`), `X` commence. À son tour, un
joueur place son symbole sur une case vide. La partie est gagnée dès que trois symboles
identiques sont alignés (ligne, colonne ou diagonale). Si la grille est remplie sans
alignement, la partie est nulle.

**Vocabulaire métier :** grille, case, symbole, joueur, tour, alignement, victoire, match nul.

**Stratégie de cas de tests**
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

### 1.2 Fléchettes — 301 (double-out)

**Principe & règles :**  
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

**Stratégie de cas de tests**
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

### 1.3 Mastermind (6 couleurs / 4 positions)

**Principe & règles :**  
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

**Stratégie de cas de tests**
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

---

## 2. Documentation & justification des choix

### 2.1 Analyse & justification des scénarios

**Identification des cas de test**  
Pour chaque jeu, la couverture est bâtie sur trois familles
(détaillées au §1) : 
- les **cas nominaux** (déroulé attendu et condition de victoire)
- les **cas limites** (frontières des règles : dernier coup, checkouts, doublons, dernier essai)
- les **cas d'erreur** (entrées invalides et actions interdites)

Cette grille garantit qu'au-delà du
« chemin heureux », les règles subtiles (double-out et *bust* aux fléchettes, comptage avec
doublons au Mastermind) sont explicitement spécifiées.

**Priorisation des scénarios**  
L'ordre de développement suit la logique BDD :  
1. **Scénarios nominaux** (initialisation d'une partie, déroulé normal, condition de victoire de
chaque jeu), qui fixent l'API métier
2. **Cas limites** (règles plus fines)
3. **Cas d'erreur** (jeu après la partie, non respect d'une règle)

### 2.2 Architecture & représentation des données

**Découpage** La solution sépare la bibliothèque métier (`JeuxLibrairy/`, assembly `JeuxLibrary`)
des tests (`JeuxTest/`). Dans la bibliothèque, un dossier `Common/` contient les propriétés partagées des jeux (états, joueurs, erreurs, logique de base) et un
dossier par jeu (`TicTacToe/`, `Darts/`, `Mastermind/`) contient le spécifique, dans les dossiers `Enums/`, `Exceptions/`
et `Model/`. Aucun jeu ne dépend d'un autre.

**Abstractions**  
`IGame` expose le strict état commun aux trois jeux — `State`, `Winner`,
`TurnTo`. Cette interface correspond à un jeu classique tour par tour.  
L'interface `IScoredGame` l'étend pour les jeux à score (`Scores`, `GetScore`, `SetScore`), les fléchettes notamment.  
`Play` n'est volontairement **pas** dans `IGame` : chaque jeu a une signature d'action propre
(`Play(player, x, y)`, `Play(player, secteur, multiplicateur)`, `Play(proposition)`), et l'imposer
aurait conduit à un paramètre fourre-tout.

**Représentation des données**

| Donnée | Représentation | Justification |
|--------|----------------|---------------|
| Grille du morpion | `char[3,3]`, sentinelle `'.'` | indexation `[ligne, colonne]` directe, lecture immédiate en debug |
| Symbole du joueur | `Dictionary<Player, char>` | la règle « X commence » reste dans l'énumération `Player`, pas dans le symbole |
| Scores des fléchettes | `Dictionary<Player, int>` | un seul point d'accès quel que soit le joueur |
| Multiplicateur | `enum Multiplier { Simple = 1, Double = 2, Triple = 3 }` | la valeur *est* le facteur : `Points => Sector * (int)Multiplier` |
| Fléchette | objet `Dart` validant à la construction | un `Dart` existant est toujours un lancer légal |
| Code / proposition Mastermind | `Color[]` et `ProposalResult[]` de longueur 4 | positions préservées, comparaison position par position |
| État de partie | `enum GameState { Pending, InProgress, Win, Draw, Lose }` | couvre les trois jeux, dont la phase de pose du code du Mastermind |

**Gestion des erreurs**  
Les règles violées lèvent des **exceptions métier typées** plutôt que des codes de retour. Chaque cas d'erreur
du §1 correspond ainsi à un type précis, directement assertable dans un step
« *an error should be thrown because …* ». L'assertion est donc effectuée sur le type de l'erreur, plutôt que sur le message.

### 2.3 Stratégie BDD & bonnes pratiques

**Langage ubiquitaire**  
Les règles sont énoncées en français dans ce document, tandis que le code
et les scénarios Gherkin sont en anglais, pour éviter tout décalage entre un identifiant et le
terme qu'il porte. La correspondance est explicite :

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

Les mêmes mots se retrouvent d'un bout à l'autre de la chaîne : `player1 throws a dart and makes a
double 20` se lit comme la règle.

**Réutilisabilité**  
Les scénarios s'appuient sur trois mécanismes :
- un **contexte partagé** `GameStepsContext` (jeu courant + dernière exception), injecté par
  construction dans chaque classe de steps
- des **steps partagés** écrits une seule fois pour les différentes abstractions : `GameStepsDefinition`
  pour `IGame`, et
  `ScoredGameStepsDefinition` pour `IScoredGame`. Les steps spécifiques à chaque jeu portent uniquement sur la logique spécifique à celui-ci.
- des **transformations d'arguments** (`StepTransformations`) qui convertissent `player1`/`player2`
  en `Player` et `simple|double|triple` en `Multiplier`, une fois pour toutes. Cela permet de limiter le nombre de steps en factorisant par joueur, et par multiplicateur (pour les fléchettes).

À cela s'ajoutent les `Background` pour l'initialisation de partie et les `Scenario Outline` pour
les familles de cas (checkouts, busts, propositions incorrectes), qui évitent la duplication de
scénarios quasi identiques.

**Maintenance**  
Ajouter un jeu consiste à créer un dossier et à implémenter `IGame` (ou
`IScoredGame`) : les assertions de fin de partie, la gestion d'exception et le contexte de test sont
acquis.  
La présence de scénarios de test permet de s'assurer que les fonctionnalités fonctionnent. Suite à une modification de code, si l'un des test échoue, la modification en est la cause.

---