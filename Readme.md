# Wordle Cheat

## A super fun and addictive way to cheat at a super fun and addictive word game

### Core Concepts

- Dependency injection, Inversion of Control using Autofac.  
- Unit Testing and creating unit testable code using Moq.
- Demonstration of the four pillars of object oriented programming (Abstraction, Encapsulation, Inheritance, Polymorphism)
- Containerization using Docker.

### Usage

```
dotnet run WordleCheat.csproj
```

### Output
When exectuted, the user will first be prompted with 20 suggested opening words.  These words are all from the official wordle dictionary and are ranked by their Scrabble scores.  The lower the Scrabble score, the more common characters that word likely has, making it more highly ranked for the game of wordle.

The user will then be prompted to enter their guess.  For the purpose of demonstration, let's use historic wordle puzzle #99.  

The user will be presented with the best 20 suggested opening words: 

```

islet(5)   alone(5)   store(5)   solar(5)   ultra(5)
saute(5)   snout(5)   stair(5)   inert(5)   train(5)
atone(5)   loser(5)   alien(5)   unlit(5)   onset(5)
inter(5)   saint(5)   unset(5)   stein(5)   stale(5)
```

For this example, the user enters the word "alone" into both the wordle game and this command line: 

```
What was your next guess? alone
```

Next, the user will be prompted to enter the results of that guess. If the letter is green, enter that letter in CAPS.  If orange, enter in LOWERCASE. If gray, enter as an underscore.  So, the user would enter:

```
_lo__
```

The user would then be presented with up to 20 more guesses: 

```
Here are some of your best next guesses:
solid(6)   igloo(6)   moult(7)   pilot(7)   locus(7)
color(7)   could(8)   mogul(8)   logic(8)   lousy(8)
lorry(8)   folio(8)   world(9)   dolly(9)   would(9)
limbo(9)   golly(9)   godly(10)   oddly(10)   lowly(11)
```

Let's guess the first suggested word: 'solid'.  Then enter the results, in this case \_Ol__ (The O was green, the l was orange, the other letters were gray,) The next suggestions appear, there are only 8.

```
moult(7)   mogul(8)   lorry(8)   lowly(11)   lofty(11)
```

Again, let's guess the first suggested word: 'moult'.  And we have a winner.  

## Code Overview

### Program.cs
The entry point for the application.  Instantiates a _ContainerBuilder_ and registers numerous objects by interface type.  

### KnownLettersInput.cs
Object is used to store user input from resulting game play (the string "\_Ol__" in the example above.)  Exposes helpful functionality used throughout rule processing. 

### Wordlist.cs
Represents a List of WordListItems and kicks off rule processing.

### Rule Objects
All rule objects implement _IRuleObject_ and exposes a single public function: _Process(IKnownLettersInput, IWordListItem);_.  If this function returns false the word will be removed from consideration.

There are other many other exceptions, interfaces and unit tests that should be self-explainatory.

## Future
I may or may not port this game over to quordle.  That game is absolute madness, with four puzzels going all at once.  

I hope you use this app to cheat like crazy and impress your friends.  Keep this app a secret and flaunt your awesomeness. 
