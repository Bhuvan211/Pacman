# Maze Hunter 3D

A modern reimagining of the classic Pac-Man arcade game, built with Unity 6 LTS using 2.5D development approach.

## Project Overview

**Maze Hunter 3D** is an educational game development project demonstrating core Computer Science principles applied to game development. The game maintains the nostalgic top-down perspective and pellet-collection gameplay while leveraging 3D rendering, spatial audio, and advanced component-based architecture.

## Features

- **Player Mechanics**: Responsive 4-directional movement controls with smooth acceleration/deceleration
- **AI Ghost System**: Multiple ghosts (Blinky, Pinky, Inky, Clyde) with distinct personalities and behavioral patterns
- **Dynamic Maze Generation**: Procedurally generated or hand-crafted maze layouts with adjustable difficulty
- **Pellet Collection**: Standard pellets for points; power pellets for temporary invulnerability
- **State Management**: Main Menu, Active Play, Pause, Level Progression, and Game Over states
- **Data Persistence**: High score tracking using PlayerPrefs
- **Cross-Platform Support**: Designed for PC and mobile deployment

## System Requirements

### Software Requirements
- **Operating System**: Windows 10/11 (64-bit), macOS 10.13+, or Linux
- **Game Engine**: Unity 6 LTS or higher
- **Script Editor**: Visual Studio 2022 or VS Code with C# extension
- **Version Control**: Git / GitHub Desktop

### Hardware Requirements
- **Processor**: Intel Core i5 (8th Gen) or AMD Ryzen 5 equivalent
- **Memory**: 8 GB RAM minimum
- **Graphics**: NVIDIA GTX 1050 / AMD equivalent or higher (DirectX 12 support)
- **Storage**: 1 GB for project files; 10 GB for Unity Editor installation

## Project Structure

```
Assets/
├── Scripts/           # C# game logic and controllers
├── Scenes/           # Unity scenes (MainMenu, Level_01)
├── Prefabs/          # Reusable game objects
├── Textures/         # Visual assets
├── Materials/        # Material definitions
├── Audio/            # Sound files
├── Resources/        # Dynamic asset loading
└── Mesh/             # 3D models

ProjectSettings/      # Unity project configuration
Packages/             # External dependencies
```

## Core Modules

### Module 1: Player Movement and Input System
Handles player character movement with collision detection against maze walls using raycasts.

### Module 2: Ghost AI and Behavioral Logic
Implements intelligent ghost behavior with chase, scatter, and frightened states based on classic Pac-Man AI.

### Module 3: Collision Detection and Game Logic
Processes collisions between player, ghosts, pellets, and environmental elements.

### Module 4: Game State Management and UI
Controls game flow and manages UI state transitions (Main Menu, Play, Pause, Game Over).

### Module 5: Maze Generation and Level Management
Handles maze creation and level progression with difficulty scaling.

## Getting Started

1. **Clone the Repository**
   ```bash
   git clone https://github.com/Bhuvan211/Pacman.git
   cd Pacman
   ```

2. **Open in Unity**
   - Open Unity Hub
   - Click "Open Project"
   - Select the project folder
   - Wait for asset import to complete

3. **Play the Game**
   - Open `MainMenu` scene from Assets/Scenes/
   - Press Play button or Ctrl+P

## Development Notes

- The project uses kinematic rigidbodies for smooth movement without traditional physics overhead
- Ghost AI recalculates direction at configurable intervals to optimize performance
- Maze data stored as 2D arrays for efficient collision detection
- UI system uses TextMeshPro for high-definition typography

## Code Examples

### Player Movement
```csharp
void Move()
{
    if (!Physics.Raycast(transform.position, inputDirection, collisionCheckDistance))
    {
        currentDirection = inputDirection;
    }
    
    Vector3 targetVelocity = currentDirection * moveSpeed;
    rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
}
```

### Ghost AI
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
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        moveDirection = GetAlignedDirection(directionToPlayer);
    }
    else
    {
        HandleScatterBehavior();
    }
}
```

## Future Enhancements

- Multiplayer Mode (Local co-op)
- Ghost Personality Skins
- Power-Up System Expansion
- Procedural Difficulty Scaling
- Mobile Optimization with Virtual Joystick
- Firebase Leaderboard Integration
- Advanced Analytics
- Custom Level Editor
- Story Mode Campaign
- Adaptive Soundtrack

## Performance

- Maintained 60 FPS on target hardware
- Optimized AI calculations with configurable update intervals
- Efficient collision detection using layer masks
- File size: ~1 GB (including Unity Editor cache)

## References

1. Unity Documentation: Physics Colliders, Input System, and State Management
2. "Game Programming Patterns" by Robert Nystrom
3. "Game AI Pro" by Steve Rabin
4. Classic Arcade Game Design Principles - Pac-Man Architecture Analysis
5. Alliance University BCA Game Development Curriculum Guides

## License

This project is part of the BCA Game Development Curriculum at Alliance University.

## Documentation

For detailed project documentation, see [PROJECT_REPORT.md](PROJECT_REPORT.md)

## Author

**Bhuvan** - Game Development Student

---

**Project Status**: In Development  
**Last Updated**: April 2026  
**Engine**: Unity 6 LTS
