#  Maze Crawler
My first monogame project in years. This game will be about survivng as long as possible (and potentially escaping) a randomly-generated maze. 
You, the unlucky prisoner, will have to evade monstrosities lurking within the maze.

# List of technical features:
- True Top-down 2D game
- Keyboard and gamepad support
- Randomly generated maze:
	- From the list of pre-made prefabs
	- Collision Detection
- Fog of War and directional viewing (Darkwood Style)
- Advanced character control
	- Forward movement is faster than side and backward movement
	- Side leaning (to look around the corners)
	- Movement speed control
		- Sticks on gamepad
		- Scroll wheel on mouse
	- Dodge
- Stamina
- The lurking monster with simple intelligence (for now at least) 


# Sprint 1 Goal (Till 6th February)
- Character movement basics
	- Forward movement is faster than side and backward movement
	- Movement speed control
		- Sticks on gamepad
		- Scroll wheel on mouse
- Stamina
- A simple room with walls
- Collision Detection


# Commit Diary (Descending Order)
## b4
- Increase player speed from 1 to 3
- Add Aim Mode for mouse. By pressing a right mouse button (RBM), the movement will be centred around the cursor. Releasing RBM will return tank control to the player.
- Add Aim Mode for gamepad. By tilting Right Thumb Stick (RTS), the player will look in a respect to RTS direction while Left Thumb Stick (LTS) is used for moving.
- Apply speed reduction when moving sideways and backward.

## b3
- Add gamepad control for player
- Add mouse look for player
- Add side movement for player (focused on mouse position)
- Add an internal toggle for input type so that mouse position will not interfere with gamepad.


## b2
- Add player to the game
- Add keyboard control for player
- Add movement to player
- Add rotation to player


## b1
- Add Myra for the purposes of UI and menus