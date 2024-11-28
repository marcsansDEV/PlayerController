# Project Overview:

This project is a third-person player control system for a 3D game built using Unity and the Unity Input System. It includes multiple systems to handle complex player behaviors like movement, jumping, rolling, gliding, and state transitions. The code is structured into various components that manage the player’s actions and interactions with the environment, with a strong focus on flexibility and extensibility.

---

# Key Features:

## Player Movement System:

- **Walking, Rolling, and Gliding**: The player can walk, glide, and roll with distinct behaviors. Each movement type is managed through state transitions, ensuring that the player’s actions feel fluid and responsive.
- **State Machine**: The movement is handled by a finite state machine (FSM), which allows the player to switch between states like Walking, Rolling, and Gliding based on input and conditions in the game.
- **Input-driven Movement**: The player’s movement is driven by a combination of camera-relative movement (e.g., moving forward or strafing based on the camera's orientation) and input-based actions (e.g., jumping, rolling).

## Jumping and Double Jumping:

- The player can jump and perform a second jump while in the air (double jump). The height and behavior of each jump can be customized, providing dynamic control over how the player interacts with gravity and obstacles.

## Camera and Input Integration:

- **Third-Person Camera**: The game’s camera is used to influence movement directions, allowing the player to move relative to where the camera is facing.
- **Input Management**: The system uses Unity's Input System to read and process player inputs, enabling smooth and customizable controls for actions such as jumping, gliding, and rolling.
- **Camera-Aware Movement**: The movement input is transformed into world-space coordinates, meaning the player’s controls feel intuitive relative to the camera's position (i.e., moving forward, backward, left, and right based on the camera’s perspective).

## State-Driven Behavior:

- The player’s behavior (such as movement speed, acceleration, or gravity modifications) is dictated by the current state (e.g., Walking, Rolling, Gliding).
- **State Transitions**: Transitions between states are based on player input, environmental conditions, and logic specified in the state machine (e.g., the player can enter or exit the Gliding state based on the glide button input or whether the player is in the air).

## Modular and Expandable:

- **State Design**: Each action (like walking, gliding, etc.) is encapsulated in its own class (PlState, PlWalkState, PlGlideState, etc.), which allows for easy modification or addition of new states.
- **Customizable Input Handling**: The input system can be easily extended to include more player actions such as sprinting, crouching, interacting with objects, etc.

## Event System:

- The project uses Unity’s **UnityEvent** to trigger specific actions when entering or exiting states (e.g., triggering animations, sound effects, or additional logic during state transitions like jumping or landing).

---

# Key Components:

- **PlayerInputManager**: This component manages all player inputs, converting raw input values from the Unity Input System into actionable player movement and behavior (e.g., reading horizontal movement, glide, jump, etc.).
- **PlayerMoveFSM**: The movement state machine component that switches between different movement states like Walking, Ball Run, and Gliding. It uses the input data and environmental feedback to calculate and apply movement forces on the player.
- **PlayerJump**: This component handles the player's jumping logic, including regular and double jumps, while maintaining the ability to detect if the player is grounded or in the air.
- **PlState and Derived Classes**: These base and derived classes (e.g., PlWalkState, PlGlideState) represent different movement states. They define behaviors like how the player moves, accelerates, or glides depending on the current state.
- **PlayerRotation**: Manages how the player’s rotation is adjusted based on camera orientation and player input. This ensures the player always faces the right direction when moving or performing actions like rolling.

---

# Technical Details:

## State Machine Design:

A Finite State Machine (FSM) is used to control the player’s movement states. Each state (e.g., Walking, Gliding) defines specific behaviors (such as movement speed and gravity adjustments) and transitions (when the player can switch from one state to another).

- The FSM is built using a combination of abstract base classes (PlState) and specific implementations (e.g., PlWalkState, PlGlideState).

## Input Handling:

Unity's Input System is used to manage player input. The system is highly flexible and allows for easy remapping of controls and scaling the game for various input devices (e.g., keyboard, gamepad).

- Input is transformed from 2D (screen space) to 3D world space, ensuring that movement and camera angles are always aligned properly.

## Camera Integration:

The camera plays a key role in determining movement direction, especially in 3D space. The system reads the camera's orientation to calculate movement directions and adjust the player's position accordingly. This ensures a seamless third-person control experience.

## Physics & Gravity Modulation:

Gravity and velocity adjustments are handled based on the current state (e.g., gravity is reduced during gliding to allow for slow descent). Movement speed and acceleration can be customized per state, allowing for different behaviors (e.g., faster movement during a roll or slower during gliding).

---

# Use Cases:

- **3D Platformers**: This control system is perfect for 3D platforming games where the player needs to interact with the environment through jumping, gliding, and rolling.
- **Adventure Games**: Ideal for third-person adventure games where the player navigates various terrains, jumps over obstacles, and switches between different movement styles.
- **Action Games**: The system supports responsive and smooth character controls, essential for fast-paced action games involving complex movement mechanics.

---

# Conclusion:

This project provides a solid foundation for building a complex and dynamic player control system for a 3D game. By integrating Unity’s Input System, state-based movement logic, and camera-aware controls, it offers an intuitive and flexible system for handling player movement, jumping, gliding, and state transitions. The design is modular and scalable, allowing for easy extensions and adaptations for various gameplay mechanics.
