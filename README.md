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

**Principe & règles.**  
Grille 3×3, deux joueurs (`X` et `O`), `X` commence. À son tour, un
joueur place son symbole sur une case vide. La partie est gagnée dès que trois symboles
identiques sont alignés (ligne, colonne ou diagonale). Si la grille est remplie sans
alignement, la partie est nulle.

**Vocabulaire métier :** grille, case, symbole, joueur, tour, alignement, victoire, match nul.

**Stratégie de cas de tests.**
- *Cas nominaux* : 
    - Coups alternés valides
    - Victoire par ligne
    - Victoire par colonne
    - Victoire par diagonale
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
touché (1 à 20) et le multiplicateur (**simple**, **double**, **triple**), plus le **bull** (25)
et le **double-bull** (50), ou 0 si la cible est manquée. Le total de la volée est **soustrait**
du score.

Règles spécifiques :
- **Double-out** : pour gagner, il faut atteindre **exactement 0** en terminant sur un **double**
  (ou un double-bull).
- **Bust** : si un lancer fait passer le score en dessous de 0, exactement à 1, ou à 0 sans
  double final, la volée est **annulée** : le score revient à sa valeur en début de tour et la
  main passe au joueur suivant.
- **Sortie sur un double uniquement** : une fléchette qui amène le score à 0 autrement que par
  un double (simple, triple, ou bull simple = 25) est un *bust*.

**Vocabulaire métier.** volée, secteur, simple / double / triple, bull, double-bull, checkout,
double-out, bust.

**Stratégie de cas de tests.**
- *Cas nominaux* : 
    - Soustraction d'une volée
    - Alternance des deux joueurs
    - Checkout gagnant : dernière fléchette sur un double
- *Cas limites* : 
    - Checkout minimal (reste 2 → D1)
    - Score à 1 (*bust*)
    - Dépassement sous 0 (*bust*)
    - 0 atteint sur un simple ou triple (*bust*)
- *Cas d'erreurs* : 
    - Secteur ou multiplicateur invalide
    - 4e fléchette dans une volée
    - Jeu après la victoire

### 1.3 Mastermind (6 couleurs / 4 positions)

**Principe & règles :**  
Un **code secret** de 4 pions est composé à partir de **6 couleurs**
(les doublons sont autorisés). À chaque **essai**, le joueur propose une combinaison de 4 pions.
Le jeu renvoie deux indices, **sans révéler les positions concernées** :
- le nombre de pions **bien placés** (bonne couleur **et** bonne position) ;
- le nombre de pions **mal placés** (bonne couleur, mauvaise position).

Le comptage apparie chaque pion **une seule fois**, ce qui rend le calcul subtil en présence de
doublons. Le joueur **gagne** dès 4 pions bien placés ; il **perd** si les **10 essais** sont
épuisés sans trouver.

**Vocabulaire métier.** code secret, proposition (combinaison), pion, couleur, essai, indice,
bien placé, mal placé.

**Stratégie de cas de tests.**
- *Cas nominaux* :
    - Proposition entièrement fausse (0 bien / 0 mal)
    - Code trouvé (4 bien placés → victoire)
    - Mélange de bien et mal placés
- *Cas limites* : 
    - Doublons dans le code et/ou dans la proposition (exactitude du comptage)
    - Victoire au 1er essai
    - Victoire au 10e (dernier) essai
    - Défaite après 10 essais
- *Erreurs* :
    - Proposition de longueur incorrecte
    - Couleur hors palette
    - Jeu après la fin de partie.

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

**TODO:** Compléter cette partie

### 2.3 Stratégie BDD & bonnes pratiques

**Langage ubiquitaire :** A compléter

**Réutilisabilité :** A compléter

**Maintenance.** A compléter