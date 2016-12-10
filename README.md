#Death from Aside
----------------


##What is it?

- A sidescrolling shooter with random level generation
- A wildly expandable platform with easy-to-build enemies, weapons, etc
- An experiment in modular design with an eye towards future expansion
- An attempt to bring the idea of mod-friendliness to a new genre
- A surprisingly fun and challenging infinite runner
- Our 393 final project!

##Version Information
You can find our version/github at https://github.com/ianlav/EECS393IsaacGameThing. 
This game version was built in Unity 5.4.1 and developed especially for Windows, though it
should run on other OS's as well.

##Installation
This project comes prepackaged with built versions. Just double-click the .exe (or equivalent) to launch!
Find these in the "Builds" folder. If you would like to view the source code in the editor, you 
will need to download Unity. If you'd like to view the source code otherwise, any IDE should do.

##Building
If you would like to build the project for yourself, you will need Unity. After loading the
project in the editor, go to File->Build & Run and select your OS. Once it opens, pick any
resolution and graphics setting. 

##Licensing
Please see the included license file

##How to Play
WASD to move, mouse to aim, space to jump. If you have the demo version, collecting an item 
pauses the game, press enter to continue. If you fall, you die. If you run out of health, you
die. If you're touched by the big monster on the left, you die. The goal is to get as far to
the right as possible.

##How to Win
There is no ultimate endgame, just get as far as you can and score as many points as possible.
Some tips:

- Shoot at the big, angry monster on your left. It'll drive it backwards.
- Score is based on distance, not kills, so don't waste time killing enemies
- The monster on the left gets faster and stronger, make sure you're picking up new guns to fight it
- The normal monsters get tougher, too.
- Watch out for platform effects or enemy bullets with effects attached!


##Planned Features

### UI
- in game displays
  - buffs/debuffs/status condition

### Score calculations
- running definition is just distance from the start (int cast of player's x value)
- Online score tracking

### Enemies
- improved melee AI

### Player
- animations

### Weapons
- sprite(s)
- variety (more weapon types)
- more bullet types
- Platform effects

### Items
- ** item spawning **
  - immediate
- consumable
  - effects that can be used by the player

### Platforms
- multiple sizes