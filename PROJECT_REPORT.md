# Project Report: Maze Hunter Evolution

## About Course

### 1. Theoretical Foundations of Game Development

#### The Concept of the "Game Loop"

At the heart of every digital game, including "Maze Hunter," is the Game Loop. Unlike traditional software that waits for user input, a game is a continuous cycle of three steps:

1. Process Input: Detecting if the player pressed a key or used the game controls.
2. Update Game State: Moving the player character, updating AI ghosts, checking collisions with pellets and obstacles, and updating the score.
3. Render: Drawing the updated positions of the player, ghosts, pellets, and maze walls on the screen.

In this project, Unity handles the loop through the Update() and FixedUpdate() methods, ensuring smooth character movement and ghost AI pathfinding at consistent 60 frames per second.

#### Coordinate Systems and Spatial Logic

Although the gameplay of this project operates in a 3D space (X, Y, and Z axes), the actual game mechanics are confined to a 2.5D plane (horizontal movement). This allows for:

- Vector Mathematics: Using Vector3 to calculate directional movement for both the player and AI ghosts.
- Rigidbody Physics: Implementing kinematic rigidbodies to manage collision detection and smooth movement without unnecessary physics calculations.
- Spatial Awareness: Managing depth layers to ensure UI overlays remain visible above the gameplay area.

#### Object-Oriented Programming (OOP) in Games

Game development is heavily reliant on OOP principles. In "Maze Hunter," every interactive element is an "Object":

- Encapsulation: The PlayerController script hides complex input handling and movement logic from other scripts.
- Inheritance: Every script inherits from MonoBehaviour, providing access to Unity's built-in lifecycle methods and physics callbacks.
- Polymorphism: The GhostAI system treats all ghosts as generic entities with configurable behaviors, yet executes unique AI logic based on their type (Blinky, Pinky, Inky, Clyde).

#### Component-Based Architecture

Modern game development has shifted from strict inheritance to a Component-Based System. This project demonstrates this philosophy through:

- Transform Component: Managing the position, rotation, and scale of the player, ghosts, and maze elements.
- Rigidbody & Collider: Utilizing kinematic rigidbodies and box/sphere colliders configured as triggers to detect collisions with pellets, power-ups, and walls.
- Audio Source Component: Attaching audio managers that play sound effects for pellet collection, power-up activation, and ghost encounters.

---

## Connectivity: Connecting Theory to the Maze Hunter Project

### Data Structures: Dictionary and Array Implementation

The critical connection between Game Development theory and this project is the use of multiple data structures:

- 2D Array for Maze Blueprint: Stores the maze layout as a grid (e.g., `int[,] mazeGrid`), where specific values represent walls, paths, and spawn points.
- Dictionary for Ghost States: Maps each ghost by type (Blinky, Pinky, Inky, Clyde) to their behavioral state (chase, scatter, frightened).
- List<T> for Pellet Tracking: Maintains a dynamic collection of pellet positions and status (collected/active).

Logic: When a pellet is consumed, it's marked as inactive or removed from the list, updating both the score and the "pellets remaining" counter. This is an O(1) lookup for collected pellets and O(n) for population-wide updates.

Efficiency: The procedural maze generation system only spawns walls and pellets during initialization, avoiding frame-by-frame instantiation overhead.

### Algorithmic Pathfinding: AI Decision-Making

A core pillar of this project is implementing intelligent ghost behavior:

- Scatter Behavior: Ghosts patrol designated corners in a random or deterministic pattern.
- Chase Behavior: When the player is within `chaseDistance`, ghosts modify their behavior to pursue the player.
- Frightened State: Activated when the player collects a power pellet, allowing temporary invulnerability and ghost vulnerabilities.

Time Complexity: The AI recalculates direction every `changeDirectionTime` seconds, avoiding per-frame pathfinding computations that would impact performance.

### UI and State Management

A professional game requires robust state management:

- State 1 (Main Menu): The initial state where game logic is idle, and the player interacts with buttons to start or view settings.
- State 2 (Active Play): The core loop where player input moves the character, AI controls ghosts, collision detection processes pellet collection and ghost encounters, and scores update in real-time.
- State 3 (Level Progression): Upon collecting all pellets, the game transitions to the next level with increased difficulty.
- State 4 (Game Over): Triggered by ghost collision, displaying the final score and options to restart or return to the main menu.

### Data Persistence via PlayerPrefs

A core pillar of game design is "Progression." By using PlayerPrefs, this project connects the player's performance to long-term goals:

- Serialization: Converting the integer score and level progress into a format that can be written to device storage.
- Retrieval: Reading high score data during the Start() method of the Main Menu to display persistent achievements.
- Profile System: Maintaining player progress across multiple sessions and game restarts.

---

## System Requirements

### A. Software Requirements

- Operating System: Windows 10/11 (64-bit), macOS 10.13+, or Linux.
- Game Engine: Unity 6 LTS or higher.
- Script Editor: Visual Studio 2022 or VS Code with C# extension.
- Version Control: Git / GitHub Desktop.
- Design Tools: Blender or ProBuilder (for 3D maze modeling), Figma (for UI layout).

### B. Hardware Requirements

- Processor: Intel Core i5 (8th Gen) or AMD Ryzen 5 equivalent.
- Memory: 8 GB RAM minimum.
- Graphics: NVIDIA GTX 1050 / AMD equivalent or higher (DirectX 12 support).
- Storage: 1 GB for project files; 10 GB for Unity Editor installation.

---

## Introduction to Project

### Project Overview

"Maze Hunter 3D" is a modern reimagining of the classic 1980s arcade game Pac-Man. While the game maintains the nostalgic top-down perspective and pellet-collection gameplay, it is built within the Unity 3D engine (Version 6 LTS) to take advantage of 3D rendering, spatial audio, and advanced component-based architecture. This approach combines 3D assets with 2D gameplay logic, often referred to as 2.5D development.

### Purpose

The primary purpose of this project is to demonstrate the practical application of core Computer Science principles within a game development context. This includes:

- Implementing efficient data structures (arrays for maze data, lists for dynamic game elements, dictionaries for AI state management).
- Mastering the MVC (Model-View-Controller) design pattern for UI management and game state separation.
- Understanding the Unity Lifecycle (Awake, Start, Update, FixedUpdate, OnTriggerEnter).
- Implementing intelligent AI algorithms for non-player character behavior.

The project aims to provide an engaging user experience through smooth controls, responsive audio feedback, and persistent progression systems.

### Scope

The scope of this project encompasses:

- Player Mechanics: Responsive 4-directional movement controls with smooth acceleration and deceleration.
- AI Ghost System: Multiple ghosts with distinct personalities and behavioral patterns (chase, scatter, frightened states).
- Dynamic Maze Generation: Procedurally generated or hand-crafted maze layouts with adjustable difficulty.
- Pellet Collection Mechanics: Pellets grant points; power pellets grant temporary invulnerability and reverse the AI ghost behavior.
- Obstacle Management: Maze walls serve as collision boundaries that affect both player and ghost movement.
- UI/UX Design: A comprehensive menu system including Main Menu, level selection, pause menu, and Game Over state.
- Data Persistence: Implementation of local storage to track high scores across gaming sessions.

### Technology for Enhancement

The project leverages:

- Kinematic Rigidbodies: For smooth player and ghost movement without traditional physics overhead.
- Universal Render Pipeline (URP): For optimized performance on mobile and lower-end devices.
- C# Scripting: For backend logic, AI algorithms, and game state management.
- TextMeshPro: For high-definition UI typography and HUD elements.
- Audio Mixer System: Modular audio management for background music and sound effects (SFX).

---

## Objectives

The development of the "Maze Hunter 3D" project was guided by the following core technical and academic objectives:

- Modular Architecture Design: Implement a modular system using C# classes that decouple game logic (movement, collision, scoring) from UI management, ensuring code is maintainable and testable.

- Intelligent AI Implementation: Design and implement multiple ghost AI systems with distinct behavioral algorithms, including chase, scatter, and frightened states, demonstrating advanced pathfinding concepts.

- Dynamic Maze Generation: Utilize procedural generation techniques or manual layouts to create varied, replayable game environments with adjustable difficulty scaling.

- Collision System Optimization: Master Unity's Physics engine by configuring kinematic rigidbodies, triggers, and layer masks to efficiently detect collisions between players, ghosts, pellets, and walls.

- Input System Abstraction: Create a unified input system that handles both keyboard input (for PC testing) and potential mobile UI button input (for Android deployment), ensuring consistent control schemes.

- State Machine Implementation: Design a robust game state system that manages transitions between Main Menu, Active Play, Pause, Level Progression, and Game Over states.

- Data Persistence and Storage: Implement a reliable save system using PlayerPrefs to serialize player progress, high scores, and level achievements.

- Performance Optimization: Optimize rendering, AI calculations, and collision detection to maintain high frame rates even with multiple ghosts and a complex maze environment.

- Audio Engineering and Feedback: Integrate responsive audio systems that provide immediate acoustic feedback for player actions, pellet collection, power-up activation, and ghost encounters.

---

## Implementation (Modules and Technical Code)

The implementation of "Maze Hunter 3D" is structured into five core modules. This modular approach ensures that physics, user interface, AI systems, and game logic remain decoupled and efficient.

### Module 1: Player Movement and Input System

This module manages the player character's movement in response to user input. It uses Vector3 calculations and smooth acceleration/deceleration to create responsive controls.

Key Components:
- PlayerController.cs: Handles input detection and movement physics.
- InputDirection System: Stores the current and next intended direction, allowing for queued movement.
- Collision Checking: Uses raycasts to validate movement against maze walls before committing position changes.

Key Logic:

```csharp
void Move()
{
    // Check for collision in intended direction
    if (!Physics.Raycast(transform.position, inputDirection, collisionCheckDistance))
    {
        currentDirection = inputDirection;
    }
    
    // Apply velocity in allowed direction
    Vector3 targetVelocity = currentDirection * moveSpeed;
    rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
}
```

### Module 2: Ghost AI and Behavioral Logic

This module implements the intelligent behavior system for multiple ghosts. Each ghost has distinct personalities based on the classic Pac-Man model:

- Blinky (Red): Direct chase behavior, always pursues the player.
- Pinky (Pink): Ambush tactics, leads the player's movement.
- Inky (Cyan): Complex routing, factors in other ghost positions.
- Clyde (Orange): Alternates between chase and scatter, creating unpredictable patterns.

Key Components:
- GhostAI.cs: Implements individual ghost logic and state transitions.
- Ghost Type Enumeration: Defines behavioral profiles for each ghost.
- State Management: Tracks chase, scatter, and frightened states.

Key Logic:

```csharp
void UpdateAI()
{
    if (isVulnerable)
    {
        HandleFrightenedState();
        return;
    }
    
    float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
    
    if (distanceToPlayer < chaseDistance)
    {
        // Chase mode
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        moveDirection = GetAlignedDirection(directionToPlayer);
    }
    else
    {
        // Scatter mode - return to designated corner
        HandleScatterBehavior();
    }
}
```

### Module 3: Collision Detection and Game Logic

This module handles all collision interactions between the player, ghosts, and environmental elements.

Collision Types:
- Pellet Collision: Increases score, triggers audio feedback, updates pellets collected.
- Power Pellet Collision: Activates frightened state for all ghosts, reverses ghost priorities.
- Ghost Collision: In normal state = Game Over; in frightened state = Ghost elimination and bonus points.
- Wall Collision: Prevents movement in that direction, handled by raycasts in PlayerController.

Key Components:
- PlayerCollisionHandler.cs: Processes collision events.
- PowerPelletManager.cs: Manages frightened state duration and effects.
- PelletSetup.cs: Handles pellet collection and tracking.

### Module 4: Game State Management and UI

This module controls the overall game flow and manages UI state transitions.

States:
- Main Menu: Display start button, settings, and exit options.
- Level Active: Gameplay is running; player controls active.
- Paused: Game logic paused; resume/menu options displayed.
- Level Complete: All pellets collected; transition to next level.
- Game Over: Display final score and restart/menu options.

Key Components:
- GameManager.cs: Singleton managing overall game state.
- GameplayManager.cs: Tracks score, lives, level progression.
- UIManager.cs: Controls HUD and menu panel visibility.
- MainMenuManager.cs: Handles main menu interactions.

Key Logic:

```csharp
public enum GameState { MainMenu, Playing, Paused, LevelComplete, GameOver }

void TransitionState(GameState newState)
{
    currentState = newState;
    
    switch(newState)
    {
        case GameState.Playing:
            Time.timeScale = 1f;
            uiManager.ShowGameplayUI();
            break;
        case GameState.Paused:
            Time.timeScale = 0f;
            uiManager.ShowPauseMenu();
            break;
        case GameState.GameOver:
            Time.timeScale = 0f;
            uiManager.ShowGameOverUI(score);
            break;
    }
}
```

### Module 5: Maze Generation and Level Management

This module handles procedural or hand-crafted maze creation and level progression.

Key Components:
- MazeGenerator.cs: Creates maze structure from blueprint or procedurally.
- LevelManager.cs: Manages level transitions and difficulty scaling.
- DifficultyManager.cs: Adjusts ghost speed, AI aggressiveness, and pellet availability per level.

Key Logic:

```csharp
void GenerateMaze(int[,] mazeBlueprint)
{
    for (int x = 0; x < mazeBlueprint.GetLength(0); x++)
    {
        for (int y = 0; y < mazeBlueprint.GetLength(1); y++)
        {
            if (mazeBlueprint[x, y] == 1) // Wall
            {
                InstantiateWall(new Vector3(x, 0, y));
            }
            else if (mazeBlueprint[x, y] == 2) // Pellet spawn
            {
                InstantiatePellet(new Vector3(x, 0, y));
            }
        }
    }
}
```

---

## Output and Game Features

### Main Menu Scene
- Clean, intuitive interface with "Start Game," "Settings," and "Exit" buttons.
- Display high score from previous sessions using PlayerPrefs.

### Gameplay Scene
- Dynamic maze with player character and multiple ghost enemies.
- Real-time HUD displaying current score, lives remaining, and level number.
- Pellet collection visual feedback with audio cues.

### Pause Menu
- Resume game, adjust audio settings, or return to main menu.

### Game Over Screen
- Final score display and comparison with high score.
- Options to restart the level or return to main menu.

### Level Progression
- Completed level transitions to next level with increased ghost speed and AI aggressiveness.
- Milestone rewards for completing multiple levels consecutively.

---

## Future Enhancements

- Multiplayer Mode: Local co-op where two players navigate the maze together or in competitive mode.
- Ghost Personality Skins: Unlock cosmetic variations for each ghost type.
- Power-Up System Expansion: Additional temporary effects (speed boost, wall phasing, ghost freezing).
- Procedural Difficulty Scaling: Algorithmic maze generation with adjustable difficulty curves.
- Mobile Optimization: Virtual joystick controls and haptic feedback for Android/iOS.
- Leaderboard Integration: Cloud-based high scores using Firebase for global rankings.
- Advanced Analytics: Track player behavior patterns and optimize difficulty balancing.
- Level Editor: In-game or companion tool to create custom maze layouts.
- Story Mode: Campaign with progressive narrative and boss encounters.
- Sound Design Enhancement: Adaptive soundtrack that responds to game state and intensity.

---

## Conclusion

The development of Maze Hunter 3D has been a comprehensive exercise in applying core principles of the Bachelor of Computer Applications (BCA) curriculum to a real-world software project. By successfully integrating complex C# logic, Unity's component-based architecture, intelligent AI systems, and data persistence via PlayerPrefs, the project demonstrates a professional-grade understanding of game development.

The transition from classic 2D arcade mechanics to a modern 3D engine highlights the versatility and scalability of the Unity platform. This project not only achieves the primary objective of creating an entertaining and challenging gaming experience but also serves as a robust foundation for more advanced features like multiplayer networking, cloud-based leaderboards, and sophisticated procedural generation systems.

The successful completion of this project signifies a readiness to tackle larger-scale software engineering challenges in the gaming industry, including team-based development, version control workflows, and the integration of third-party systems and services.

---

## References

1. Unity Documentation: Physics Colliders, Input System, and State Management.
2. "Game Programming Patterns" by Robert Nystrom.
3. "Game AI Pro" by Steve Rabin.
4. Classic Arcade Game Design Principles - Pac-Man Architecture Analysis.
5. Alliance University BCA Game Development Curriculum Guides.

---

## GitHub Repository Link

[Maze Hunter 3D - Game Development Project](https://github.com/yourusername/maze-hunter-3d)

---

Project Status: In Development  
Last Updated: April 2026  
Lead Developer: [Your Name]
