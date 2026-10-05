# Paddle Shrink

**Every goal you concede makes your paddle smaller. Concede four and it's over.**

A fast 2D arcade duel against an AI opponent, built in Unity 6 and playable in the browser on desktop and mobile.

**[▶ Play in browser on itch.io](https://tugruldurmazer.itch.io/paddle-shrink)**

## Features

- **Shrinking paddles:** each conceded goal shrinks the paddle through four width steps (100%, 78%, 58%, 40%); the fourth goal eliminates it
- **Angle control:** the ball's outgoing angle depends on where it hits the paddle, and its speed rises with every rally
- **Three AI difficulty levels:** Easy, Normal and Hard, each with its own speed, reaction delay and aiming error; the choice is saved between sessions
- **Game feel:** squash and stretch on paddle hits, camera shake, conceding-half flash and score punch animations
- **Audio:** nine sound effects, pitch-shifted paddle hits that follow ball speed, and a persistent mute toggle
- **Input:** touch, mouse and keyboard
- **Original visual identity:** custom UI sprites, logo and palette made for this version

## Controls

| Action | Input |
| --- | --- |
| Move paddle | Drag (touch or mouse), or A / D, or Left / Right arrows |
| Pause | Pause button in the top bar |

## Tech

- Unity 6 (6000.4.5f1), Universal Render Pipeline (2D)
- C#
- Unity Input System
- DOTween for animations
- TextMeshPro
- WebGL build

## Architecture

The game runs in a single scene, driven by a `GameManager` state machine with four states: `Menu`, `Playing`, `Paused` and `GameOver`. Systems communicate through static C# events instead of direct references, so gameplay, UI and audio stay decoupled:

- `Ball` raises `PaddleHit`, `WallHit` and `Served`
- `GameManager` raises `StateChanged`, `ScoreChanged` and `GoalScored`
- `UIManager`, `AudioManager`, `CameraShake` and `HalfFlash` subscribe in `OnEnable` and unsubscribe in `OnDisable`

The ball is a dynamic `Rigidbody2D` with continuous collision detection; paddles are kinematic bodies moved with `MovePosition`. Arena walls and goals are positioned from the camera's size at runtime, so the playfield fits any aspect ratio.

```
Assets/Script/
├── Core/       GameManager, GameState, Arena, DifficultySettings
├── Gameplay/   Ball, Paddle, PlayerPaddle, AIPaddle, GoalZone, CameraShake, HalfFlash
├── UI/         UIManager, MenuUI, HUDUI, PauseUI, ResultUI, UIClickSound
└── Audio/      AudioManager
```

## Getting Started

1. Clone the repository
   ```
   git clone https://github.com/durmazertugrul/PaddleShrink-2D-Mobile.git
   ```
2. Open the project with Unity **6000.4.5f1** (or a newer Unity 6 release)
3. Open `Assets/Scenes/Game.unity` and press Play

DOTween is included under `Assets/Plugins`, so no extra setup is needed.

## Version History

**v2.0:** Full rebuild in Unity 6 with a new architecture, physics-based ball, AI difficulty levels, sound, game feel animations, a new visual identity and a WebGL build.

**v1.0:** The original prototype, built at the start of my Unity learning path as an Android APK. It is preserved under the [`v1-prototype`](https://github.com/durmazertugrul/PaddleShrink-2D-Mobile/tree/v1-prototype) tag.

## Credits

- Font: [Rammetto One](https://fonts.google.com/specimen/Rammetto+One) by Sorkin Type, SIL Open Font License
- Tweening: [DOTween](https://dotween.demigiant.com/) by Demigiant
- Sound effects created with [jsfxr](https://sfxr.me/)
