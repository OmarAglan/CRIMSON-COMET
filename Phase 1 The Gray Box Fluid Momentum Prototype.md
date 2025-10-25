## **CRIMSON COMET: Development Roadmap**

### **Phase 1: The "Gray Box" Fluid Momentum Prototype**

**Primary Goal:** To create a playable prototype that proves the core movement and control system is responsive, deep, and, most importantly, **fun**. At the end of this phase, flying the player's primitive shape around a test environment must be an inherently satisfying experience on its own.

**Philosophy:** Function over form. We will use only primitive shapes (cubes, spheres, planes). There will be **no modeling, no texturing, and no final art**. Every ounce of effort goes into the *feel* of the controller and the systems that support it.

**Technical Stack:**
*   **Engine:** Unity 6
*   **Rendering:** Universal Render Pipeline (URP)
*   **Input:** Unity's modern Input System package
*   **Language:** C#

**Estimated Duration:** 4 Weeks

---

### **Week 1: Foundation - Project Setup & Core Physics**

**Weekly Goal:** To have a player-controlled cube that can move in all six degrees of freedom (6DoF) using Rigidbody physics, driven by gamepad input.

*   **Step 1: Project Initialization**
    *   Create a new project in Unity Hub using the **Unity 6** editor.
    *   Select the **3D (URP)** template. This sets up the Universal Render Pipeline from the start, ensuring our project is optimized for performance.
    *   Go to `Window > Package Manager` and install the **Input System** package. Enable it when prompted.

*   **Step 2: Input Action Asset Setup**
    *   Create a `PlayerControls` Input Action Asset.
    *   Define the "Gameplay" Action Map.
    *   Create and bind all necessary Actions as defined in the GDD: `Move` (Vector2), `Look` (Vector2), `Thrust` (Button), `QuickBoost` (Button), `Ascend` (Button), `Descend` (Button), `Drift` (Button), `LockOn` (Button), `StanceSwitch` (Button), `FirePrimary` (Button), `FireSecondary` (Button).

*   **Step 3: Creating the Player "Comet"**
    *   Create a Cube 3D object and name it `Player`. To give it a clear "front," add a smaller, thinner Cube as a child object and position it like a cockpit.
    *   Add a **Rigidbody** component to the root `Player` object.
    *   In the Rigidbody settings: **uncheck `Use Gravity`** and set **`Angular Drag` to `2`** to prevent infinite spinning.

*   **Step 4: Scripting the Controller (Part 1 - The Basics)**
    *   Create a C# script named `PlayerController.cs` and attach it to the `Player` object.
    *   In the script, get a reference to the Rigidbody in the `Awake()` function.
    *   Using the generated C# class from your Input Action Asset, create functions to read the input values into variables (e.g., `private Vector2 moveInput;`).
    *   In the `FixedUpdate()` function (essential for physics), write the initial movement logic. Use `rb.AddRelativeForce()` to apply forces based on the `moveInput` and `ascend/descend` inputs. This will make the cube move relative to the direction it's facing.

*   **End of Week 1 Checkpoint:** You have a cube in an empty scene. Using a gamepad, you can make it fly forward, backward, strafe left/right, and move up/down. It feels basic, but it is physically simulated and responding to your controls.

---

### **Week 2: Dynamics - Implementing the "Awesome" Mechanics**

**Weekly Goal:** To implement the signature mechanics that define the "Fluid Momentum" system: the Quick Boost and the High-G Drift.

*   **Step 1: The Boost Gauge**
    *   In `PlayerController.cs`, create public float variables for `maxBoost`, `currentBoost`, `boostCost`, and `boostRechargeRate`.
    *   In the `Update()` function, handle the logic for passively recharging the `currentBoost` over time when it's not being used.

*   **Step 2: Scripting the Quick Boost (Circle/B)**
    *   In your input handler function for the `QuickBoost` action:
    *   Check if `currentBoost` is greater than the cost of a boost.
    *   If so, subtract the cost and apply a powerful, instantaneous force using `rb.AddRelativeForce(direction * force, ForceMode.Impulse)`. The `Impulse` ForceMode is critical for achieving the sharp, "kick" feeling.

*   **Step 3: Scripting the High-G Drift (L3)**
    *   This is a state-based mechanic. Create a private boolean variable `isDrifting`.
    *   Use the `performed` and `canceled` events of the `Drift` input action to set `isDrifting` to `true` or `false`.
    *   In `FixedUpdate()`, wrap your entire `AddRelativeForce` logic in an `if (!isDrifting)` block.
    *   **Result:** When the player holds L3, the script will stop applying new directional forces. The Rigidbody's existing velocity will carry it forward, but the player will be free to rotate and aim. This is the core of the signature mechanic.

*   **End of Week 2 Checkpoint:** The player cube now feels dynamic. You can perform sharp evasive dodges with the Quick Boost and maintain momentum while aiming freely with the High-G Drift. You can start to "feel" the flow between these two states.

---

### **Week 3: Context - Camera, Feedback, and Tuning**

**Weekly Goal:** To create a camera that can keep up with the action and to add the essential feedback that makes the mechanics feel impactful.

*   **Step 1: The Camera Controller**
    *   Create a new empty GameObject called `CameraController`. Create a new C# script with the same name and attach it.
    *   This script will handle all camera logic. It should have a `public Transform target` field, which you will drag your `Player` object into in the Inspector.
    *   In its `LateUpdate()` function, make the camera smoothly follow the player's position using `Vector3.Lerp()`.
    *   Use the `Look` input action values to rotate the camera around the player.

*   **Step 2: Parameter Tuning**
    *   Go through your `PlayerController.cs` script and make every important variable `public` or `[SerializeField]`. This includes `thrustForce`, `strafeForce`, `quickBoostForce`, `boostCost`, `boostRechargeRate`, etc.
    *   **Crucially, spend significant time in Play Mode tweaking these values in the Inspector.** Does the boost feel too weak? Does the drift feel too floaty? This step is what turns a functional controller into a *fun* one.

*   **Step 3: Adding "Juice" (Basic Feedback)**
    *   **VFX:** Create a new Particle System as a child of the `Player` cube to act as a simple thruster trail. Use code to enable/disable it when boosting. Create another particle effect that you can trigger to play once (`ParticleSystem.Play()`) at the player's position when a Quick Boost happens.
    *   **SFX:** Find free placeholder sounds online (e.g., from freesound.org). You'll need a "thruster loop," a "boost whoosh," and a "drift engage" sound. In your script, add an `AudioSource` component and call `audioSource.PlayOneShot(clip)` to trigger sounds for specific events.

*   **End of Week 3 Checkpoint:** The game is now much more readable. The camera works, the controls feel tighter after tuning, and you have basic audio-visual cues for your most important actions.

---

### **Week 4: The Playground - Environment and UI**

**Weekly Goal:** To build a test arena that pushes the controller to its limits and to add a minimal UI for critical gameplay information.

*   **Step 1: Building the "Gray Box" Arena**
    *   Create a new scene named `TestArena`.
    *   Using only primitive cubes, spheres, and planes, build a level. **Do not worry about making it look good.**
    *   Create a variety of challenges: long, narrow tunnels to test precision flying; large, open areas with floating spheres to test 3D combat maneuvering; and tight clusters of pillars to test the Quick Boost and Drift for navigation.

*   **Step 2: Minimalist Player HUD**
    *   Using Unity's UI Toolkit or a World Space Canvas, create a simple UI.
    *   At a minimum, it needs one element: a horizontal bar that visually represents the `currentBoost` relative to the `maxBoost`.
    *   Write the code to link the UI bar's fill amount to the `currentBoost` variable in your player script.

*   **Step 3: Refinement and Bug Squashing**
    *   Spend the rest of the week playtesting extensively in the new arena.
    *   Fix any bugs or weird physics interactions you find.
    *   Continue to refine the tuning parameters now that you have a proper environment to test them in.

*   **End of Phase 1 - Final Review:** You have a complete prototype scene. The core "Fluid Momentum" system is fully implemented and can be tested in a dedicated playground. The project's success is now determined by answering one question: **"Is it fun to simply fly this cube around this gray box for 10 minutes?"** If the answer is yes, Phase 1 is a success, and you have a solid foundation for the rest of the game.