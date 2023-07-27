# Raytracing-2D

Raytracing-2D is a simple application that contains a basic demonstration of a simplified raytracing algorithm. You control a circle sprite and cast out rays from your position which then intersect with objects in the scene.

## Main Features

- Best run directly within the editor so that you may change the raycast settings at will.
- Ability to cast up to 19 rays from the player position.
- Functional player movement system allowing for rotation and translation.
- Raycasts interact properly with the environment around them stopping as soon as they intersect with an obstacle.
- Well documented codebase with comments explaining variable placement and method function.

## Screenshots

![Gameplay](https://github.com/MilkeZa/raytracing-2d/tree/master/Assets/Art/Screenshots/raytracingGameplay.png)

![Early version settings](https://github.com/MilkeZa/raytracing-2d/tree/master/Assets/Art/Screenshots/earlyVersionSettings.png)

## Future Features & Optimizations

- In game menu allowing for the switching of parameters to avoid having to play within the editor.
- Listeners that detect player movement and only cast rays if the players position has changed. This would avoid constantly casting rays out when the player is stationary, wasting performance.
