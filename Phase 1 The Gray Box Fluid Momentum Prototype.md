# **CRIMSON COMET: Phase 1 Extended Development Roadmap**

## **Phase 1: The "Gray Box" Fluid Momentum Prototype**

### **📋 Phase Overview**

**Primary Goal:** To create a playable prototype that proves the core movement and control system is responsive, deep, and, most importantly, **fun**. At the end of this phase, flying the player's primitive shape around a test environment must be an inherently satisfying experience on its own.

**Philosophy:** Function over form. We will use only primitive shapes (cubes, spheres, planes). There will be **no modeling, no texturing, and no final art**. Every ounce of effort goes into the *feel* of the controller and the systems that support it.

**Technical Stack:**
- **Engine:** Unity 6 (fallback: Unity 2022.3 LTS if stability issues)
- **Rendering:** Universal Render Pipeline (URP)
- **Input:** Unity's modern Input System package (v1.7.0+)
- **Language:** C#
- **Version Control:** Git (initialize immediately)

**Estimated Duration:** 4 Weeks (160 hours total / 40 hours per week)

**Critical Success Criteria:**
- ✅ 6DoF movement feels responsive (<50ms input latency)
- ✅ Quick Boost provides satisfying "kick" sensation
- ✅ High-G Drift clearly preserves momentum while allowing free aim
- ✅ Player can navigate test arena for 10+ minutes without frustration
- ✅ External playtester says "I want to keep flying this cube"
- ✅ Stable 60fps in test arena

**Phase Failure Conditions (Requires Re-evaluation):**
- ❌ Controls feel "floaty" or unresponsive after 2 weeks of tuning
- ❌ Rigidbody physics cause unpredictable/chaotic behavior
- ❌ Quick Boost or Drift mechanics don't feel meaningfully different
- ❌ Performance drops below 45fps in empty scene

---

## **🗓️ Week 1: Foundation - Project Setup & Core Physics**

**Weekly Goal:** To have a player-controlled cube that can move in all six degrees of freedom (6DoF) using Rigidbody physics, driven by gamepad input.

**Total Time Budget:** 40 hours  
**Daily Breakdown:** 8 hours/day × 5 days

---

### **📅 Day 1: Project Infrastructure (8 hours)**

#### **Morning Session (4 hours): Unity Project Setup**

**Task 1.1: Create New Unity Project** *(30 minutes)*
- Launch Unity Hub
- Click "New Project"
- Select **Unity 6** (or 2022.3 LTS if 6 is unstable)
- Choose **3D (URP)** template
- Project Name: `CrimsonComet_Prototype`
- Location: Dedicated project folder (NOT in cloud-synced directory like Dropbox)
- Click "Create Project"

**Success Check:**
- [ ] Project opens without errors
- [ ] URP is active (check Project Settings > Graphics > Render Pipeline Asset)

**Common Pitfall:** Unity 6 may have preview bugs. If project won't open or has console errors on start, downgrade to 2022.3 LTS immediately.

---

**Task 1.2: Install Essential Packages** *(45 minutes)*
- Open `Window > Package Manager`
- Switch dropdown from "Packages: In Project" to "Packages: Unity Registry"
- Install the following:
  - **Input System** (v1.7.0+)
  - **Cinemachine** (v3.0.0+) - for camera later
  - **Recorder** (optional, for capturing footage)
- When prompted "You need to restart Editor," click "Yes"

**Success Check:**
- [ ] All packages show "Installed" status
- [ ] Console shows no errors after restart
- [ ] New menu appears: `Window > Analysis > Input Debugger`

---

**Task 1.3: Project Settings Configuration** *(45 minutes)*

**Physics Settings:**
- `Edit > Project Settings > Physics`
- Set `Default Solver Iterations` to **10** (higher = more stable physics)
- Set `Default Solver Velocity Iterations` to **8**
- Gravity: Set Y to **0** (we're in space, no gravity)

**Quality Settings:**
- `Edit > Project Settings > Quality`
- Delete all presets except "Medium" and "High"
- Set "Medium" as default
- In "Medium" settings:
  - VSync Count: **Don't Sync** (we'll cap framerate later)
  - Anti Aliasing: **2x Multi Sampling**
  - Texture Quality: **Full Res**

**Time Settings:**
- `Edit > Project Settings > Time`
- Fixed Timestep: **0.01666667** (60 physics updates/second)
- Maximum Allowed Timestep: **0.1**

**Input System Settings:**
- `Edit > Project Settings > Input System Package`
- Update Mode: **Process Events In Dynamic Update**
- Compensate For Screen Orientation: **Off**

**Success Check:**
- [ ] Gravity Y = 0 in Physics settings
- [ ] Fixed Timestep = 0.01666667
- [ ] Input System is set to "Both" or "Input System Package (New)"

---

**Task 1.4: Folder Structure Setup** *(30 minutes)*

Create this exact hierarchy in your Project window:

```
Assets/
├── _Project/
│   ├── Scenes/
│   ├── Scripts/
│   │   ├── Player/
│   │   ├── Camera/
│   │   ├── Managers/
│   │   └── Utilities/
│   ├── Prefabs/
│   ├── Materials/
│   ├── Audio/
│   │   ├── SFX/
│   │   └── Music/
│   ├── VFX/
│   └── Input/
└── _Testing/
    └── PrototypeScenes/
```

**Why This Structure:**
- `_Project` prefix ensures it stays at top of list
- Separation between project assets and test assets
- Modular script organization from the start

---

#### **Afternoon Session (4 hours): Version Control & Input Setup**

**Task 1.5: Initialize Git Repository** *(45 minutes)*

**If using GitHub Desktop:**
1. File > Add Local Repository > Choose your Unity project folder
2. Create `.gitignore` file in project root (copy from: https://github.com/github/gitignore/blob/main/Unity.gitignore)
3. Make initial commit: "Initial project setup"

**If using command line:**
```bash
cd /path/to/CrimsonComet_Prototype
git init
# Copy .gitignore from Unity template
git add .
git commit -m "Initial project setup - Unity 6 URP with Input System"
```

**Success Check:**
- [ ] `.git` folder exists in project root
- [ ] `.gitignore` is present and excludes Library/, Temp/, etc.
- [ ] First commit is made

**⚠️ Critical:** Never commit the `Library/` folder. It's regenerated and causes merge conflicts.

---

**Task 1.6: Create Input Action Asset** *(90 minutes)*

1. In Project window: `Right-click Assets/_Project/Input > Create > Input Actions`
2. Name it `PlayerControls`
3. Double-click to open Input Actions editor window

**Create "Gameplay" Action Map:**

Click `+` next to "Action Maps" → Name: **Gameplay**

**Create these Actions (click + next to Actions):**

| Action Name | Action Type | Control Type |
|-------------|-------------|--------------|
| Move | Value | Vector 2 |
| Look | Value | Vector 2 |
| Ascend | Button | Button |
| Descend | Button | Button |
| PrimaryBoost | Button | Button |
| QuickBoost | Button | Button |
| Drift | Button | Button |
| FirePrimary | Button | Button |
| FireSecondary | Button | Button |
| Shield | Button | Button |
| LockOn | Button | Button |
| StanceSwitch | Button | Button |

**Bind Gamepad Controls:**

For **Move**:
- Click `<No Binding>` → Add Binding → Path: `Gamepad > Left Stick`

For **Look**:
- Add Binding → Path: `Gamepad > Right Stick`

For **Ascend**:
- Add Binding → Path: `Gamepad > Button East` (Xbox: A / PS: X)

For **Descend**:
- Add Binding → Path: `Gamepad > Button North` (Xbox: Y / PS: Triangle)

For **PrimaryBoost**:
- Add Binding → Path: `Gamepad > Right Trigger`

For **QuickBoost**:
- Add Binding → Path: `Gamepad > Button South` (Xbox: B / PS: Circle)

For **Drift**:
- Add Binding → Path: `Gamepad > Left Stick Press` (L3)

For **LockOn**:
- Add Binding → Path: `Gamepad > Right Stick Press` (R3)

*(Leave other actions unbound for now - we'll add them in Phase 2)*

**Save and Generate C# Class:**
- Check "Generate C# Class" checkbox at top
- Click "Apply"
- Close window

**Success Check:**
- [ ] `PlayerControls.cs` script appears in Input folder
- [ ] `PlayerControls.inputactions` asset exists
- [ ] No console errors

---

**Task 1.7: Create Test Scene** *(45 minutes)*

1. `File > New Scene` → Choose "Basic (URP)"
2. Save immediately: `Assets/_Project/Scenes/Phase1_Movement.unity`
3. Delete default "Main Camera" (we'll make our own)
4. Create basic environment:
   - `GameObject > 3D Object > Plane` 
   - Scale it: (10, 1, 10) in Transform
   - Name: "GroundPlane"
   - Create Material: `Assets/_Project/Materials/M_Grid`
   - Set Albedo color to dark gray (RGB: 40, 40, 40)
   - Assign to plane

5. Create directional light:
   - Ensure "Directional Light" exists in hierarchy
   - Rotation: (50, -30, 0)
   - Intensity: 1.5
   - Color: Slight warm white (RGB: 255, 248, 240)

**Success Check:**
- [ ] Scene is saved
- [ ] Ground plane is visible in Scene view
- [ ] Lighting looks clear (not too dark)

---

**End of Day 1 Commit:**
```
git add .
git commit -m "Day 1: Project setup, input actions, test scene"
```

---

### **📅 Day 2: Player Object & Basic Rigidbody Setup (8 hours)**

#### **Morning Session (4 hours): Creating the Player**

**Task 2.1: Build Player GameObject** *(45 minutes)*

1. In Hierarchy: `Right-click > 3D Object > Cube`
2. Name: `Player`
3. Transform:
   - Position: (0, 2, 0)
   - Rotation: (0, 0, 0)
   - Scale: (1, 1, 1)

**Create "Cockpit" Indicator:**
1. Right-click `Player` > `3D Object > Cube`
2. Name: `Cockpit`
3. Transform:
   - Position: (0, 0, 0.6) ← relative to parent
   - Rotation: (0, 0, 0)
   - Scale: (0.3, 0.3, 0.2)

**Add Visual Distinction:**
1. Create Material: `Assets/_Project/Materials/M_PlayerBody`
   - Albedo: Crimson Red (RGB: 220, 20, 60)
   - Metallic: 0.5
   - Smoothness: 0.7

2. Create Material: `Assets/_Project/Materials/M_Cockpit`
   - Albedo: Bright Cyan (RGB: 0, 255, 255)
   - Emission: Enable, set to same cyan, HDR intensity: 2

3. Assign `M_PlayerBody` to `Player` cube's Mesh Renderer
4. Assign `M_Cockpit` to `Cockpit` cube's Mesh Renderer

**Success Check:**
- [ ] Player is red cube with glowing cyan cockpit
- [ ] Cockpit clearly shows "forward" direction
- [ ] Hierarchy: Player (parent) → Cockpit (child)

---

**Task 2.2: Add Rigidbody Component** *(30 minutes)*

1. Select `Player` object
2. `Add Component > Rigidbody`
3. Configure settings:

```
Mass: 1
Drag: 0.2
Angular Drag: 2
Use Gravity: UNCHECKED ← Critical!
Is Kinematic: UNCHECKED
Interpolate: Interpolate
Collision Detection: Continuous
Constraints: None (all rotation/position axes unlocked)
```

**Why These Settings:**
- **Mass 1:** Standard unit, easier to reason about forces
- **Drag 0.2:** Slight air resistance feel, prevents infinite acceleration
- **Angular Drag 2:** Prevents spinning forever, but still allows rotation
- **Interpolate:** Smooths physics movement between frames
- **Continuous Collision:** Prevents fast-moving object from tunneling through walls

**Success Check:**
- [ ] Rigidbody component appears in Inspector
- [ ] "Use Gravity" is unchecked
- [ ] Press Play: Cube stays floating (doesn't fall)

---

**Task 2.3: Create PlayerController Script** *(60 minutes)*

1. `Right-click Assets/_Project/Scripts/Player > Create > C# Script`
2. Name: `PlayerController`
3. Double-click to open in IDE

**Initial Script Structure:**

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    private PlayerControls controls;

    [Header("Movement Forces")]
    [SerializeField] private float thrustForce = 50f;
    [SerializeField] private float strafeForce = 30f;
    [SerializeField] private float verticalForce = 25f;

    [Header("Boost System")]
    [SerializeField] private float maxBoost = 100f;
    [SerializeField] private float currentBoost = 100f;
    [SerializeField] private float boostRechargeRate = 30f;
    [SerializeField] private float boostRechargeDelay = 0.5f;
    
    [Header("Rotation")]
    [SerializeField] private float pitchSpeed = 100f;
    [SerializeField] private float yawSpeed = 100f;
    [SerializeField] private float rollSpeed = 80f;

    // Input values
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isAscending;
    private bool isDescending;
    private bool isPrimaryBoosting;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
        
        // Subscribe to input events
        controls.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => moveInput = Vector2.zero;
        
        controls.Gameplay.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => lookInput = Vector2.zero;
        
        controls.Gameplay.Ascend.performed += ctx => isAscending = true;
        controls.Gameplay.Ascend.canceled += ctx => isAscending = false;
        
        controls.Gameplay.Descend.performed += ctx => isDescending = true;
        controls.Gameplay.Descend.canceled += ctx => isDescending = false;
        
        controls.Gameplay.PrimaryBoost.performed += ctx => isPrimaryBoosting = true;
        controls.Gameplay.PrimaryBoost.canceled += ctx => isPrimaryBoosting = false;
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    private void Update()
    {
        HandleBoostRecharge();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        // Forward/backward thrust
        if (moveInput.y != 0)
        {
            rb.AddRelativeForce(Vector3.forward * moveInput.y * thrustForce);
        }

        // Strafe left/right
        if (moveInput.x != 0)
        {
            rb.AddRelativeForce(Vector3.right * moveInput.x * strafeForce);
        }

        // Vertical movement
        if (isAscending)
        {
            rb.AddRelativeForce(Vector3.up * verticalForce);
        }
        if (isDescending)
        {
            rb.AddRelativeForce(Vector3.down * verticalForce);
        }

        // Primary boost
        if (isPrimaryBoosting && currentBoost > 0)
        {
            rb.AddRelativeForce(Vector3.forward * thrustForce * 1.5f);
            currentBoost -= 15f * Time.fixedDeltaTime;
            currentBoost = Mathf.Max(0, currentBoost);
        }
    }

    private void HandleRotation()
    {
        // Pitch (look up/down)
        if (lookInput.y != 0)
        {
            float pitch = -lookInput.y * pitchSpeed * Time.fixedDeltaTime;
            rb.transform.Rotate(Vector3.right, pitch, Space.Self);
        }

        // Yaw (look left/right)
        if (lookInput.x != 0)
        {
            float yaw = lookInput.x * yawSpeed * Time.fixedDeltaTime;
            rb.transform.Rotate(Vector3.up, yaw, Space.Self);
        }
    }

    private void HandleBoostRecharge()
    {
        if (!isPrimaryBoosting && currentBoost < maxBoost)
        {
            currentBoost += boostRechargeRate * Time.deltaTime;
            currentBoost = Mathf.Min(currentBoost, maxBoost);
        }
    }
}
```

**Save script and return to Unity.**

---

#### **Afternoon Session (4 hours): First Flight Test**

**Task 2.4: Attach Script and Initial Test** *(30 minutes)*

1. Select `Player` object
2. Drag `PlayerController.cs` onto it (or Add Component)
3. In Inspector, verify all serialized fields appear

**Connect Gamepad:**
- Plug in Xbox or PlayStation controller
- Open `Window > Analysis > Input Debugger`
- Verify gamepad appears in device list

**Press Play and Test:**

**Test Checklist:**
- [ ] Left stick forward: Cube moves forward
- [ ] Left stick backward: Cube moves backward
- [ ] Left stick left/right: Cube strafes
- [ ] Right stick: Cube rotates (pitch and yaw)
- [ ] A/X button: Cube moves up
- [ ] Y/Triangle button: Cube moves down
- [ ] Right Trigger: Cube boosts forward faster

**Expected Issues at This Point:**
- Movement might feel "slidey" (normal, will tune)
- Rotation might feel too fast/slow (will tune)
- No visual feedback yet (coming next week)

---

**Task 2.5: Debug Visualization** *(45 minutes)*

Add this helper code to `PlayerController.cs` to visualize forces:

```csharp
private void OnDrawGizmos()
{
    if (rb == null) return;

    // Draw velocity vector
    Gizmos.color = Color.green;
    Gizmos.DrawRay(transform.position, rb.velocity);

    // Draw forward direction
    Gizmos.color = Color.blue;
    Gizmos.DrawRay(transform.position, transform.forward * 3f);
}
```

**In Scene view:**
- Green line = current velocity direction
- Blue line = forward facing direction

This will be crucial for testing drift later.

---

**Task 2.6: Basic Camera Setup** *(90 minutes)*

1. Create empty GameObject: `CameraRig`
2. Position: (0, 0, 0)
3. Create child: `Camera > Main Camera`
4. Position Camera relative to CameraRig:
   - Position: (0, 3, -8)
   - Rotation: (15, 0, 0)

**Create Simple Follow Script:**

`Assets/_Project/Scripts/Camera/SimpleCameraFollow.cs`:

```csharp
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed = 10f;
    [SerializeField] private float rotationSpeed = 5f;

    private void LateUpdate()
    {
        if (target == null) return;

        // Smoothly move to target position
        transform.position = Vector3.Lerp(
            transform.position,
            target.position,
            followSpeed * Time.deltaTime
        );

        // Smoothly rotate to match target orientation
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            target.rotation,
            rotationSpeed * Time.deltaTime
        );
    }
}
```

**Attach to CameraRig and assign Player as target.**

**Test:** Camera should now follow the player smoothly.

---

**Task 2.7: Initial Tuning Session** *(60 minutes)*

Enter Play Mode and spend time adjusting these values in the Inspector **while playing**:

**Start with these baseline values if default feels wrong:**

```
PlayerController:
  Thrust Force: 50
  Strafe Force: 30
  Vertical Force: 25
  Pitch Speed: 80
  Yaw Speed: 80

Rigidbody:
  Drag: 0.2 (lower = more floaty, higher = more resistant)
  Angular Drag: 2

SimpleCameraFollow:
  Follow Speed: 8
  Rotation Speed: 5
```

**What to Feel For:**
- **Too Floaty:** Increase Rigidbody Drag
- **Too Stiff:** Decrease Rigidbody Drag
- **Too Sensitive:** Lower force values
- **Too Sluggish:** Raise force values

**Copy final values when you exit Play Mode** (Unity resets Inspector changes in Play Mode!)

---

**End of Day 2 Commit:**
```
git add .
git commit -m "Day 2: Player controller basic movement and camera"
```

---

### **📅 Day 3: Input Refinement & Edge Cases (8 hours)**

**Task 3.1: Add Input Sensitivity Curves** *(2 hours)*

Problem: Raw stick input feels "digital" even though it's analog.

**Solution: Add dead zone and response curve**

Update `PlayerController.cs`:

```csharp
[Header("Input Settings")]
[SerializeField] private float stickDeadZone = 0.15f;
[SerializeField] private AnimationCurve movementResponseCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
[SerializeField] private AnimationCurve lookResponseCurve = AnimationCurve.Linear(0, 0, 1, 1);

private Vector2 ProcessInput(Vector2 rawInput, float deadZone, AnimationCurve curve)
{
    // Apply dead zone
    if (rawInput.magnitude < deadZone)
        return Vector2.zero;

    // Normalize and apply curve
    Vector2 direction = rawInput.normalized;
    float magnitude = rawInput.magnitude;
    magnitude = curve.Evaluate(magnitude);

    return direction * magnitude;
}

// In HandleMovement(), replace direct input usage:
private void HandleMovement()
{
    Vector2 processedMove = ProcessInput(moveInput, stickDeadZone, movementResponseCurve);
    
    if (processedMove.y != 0)
    {
        rb.AddRelativeForce(Vector3.forward * processedMove.y * thrustForce);
    }
    // ... etc
}
```

**Test:** Movement should now feel more precise at small stick deflections.

---

**Task 3.2: Add Speed Limiting** *(1.5 hours)*

Without this, the player can accelerate infinitely.

Add to `PlayerController.cs`:

```csharp
[Header("Speed Limits")]
[SerializeField] private float maxNormalSpeed = 80f;
[SerializeField] private float maxBoostSpeed = 120f;

private void LateFixedUpdate()
{
    // Cap velocity
    float maxSpeed = isPrimaryBoosting ? maxBoostSpeed : maxNormalSpeed;
    
    if (rb.velocity.magnitude > maxSpeed)
    {
        rb.velocity = rb.velocity.normalized * maxSpeed;
    }
}

// Add this at bottom of script
private void LateFixedUpdate()
{
    CapVelocity();
}
```

**Note:** `LateFixedUpdate()` doesn't exist in Unity by default. We need to call it manually:

```csharp
private void FixedUpdate()
{
    HandleMovement();
    HandleRotation();
    CapVelocity(); // Call directly here instead
}
```

---

**Task 3.3: Add Boost Gauge Events** *(1 hour)*

For future UI/VFX hookup:

```csharp
using System;

public class PlayerController : MonoBehaviour
{
    public event Action<float> OnBoostChanged; // Reports 0-1 fill percentage
    public event Action OnBoostDepleted;
    public event Action OnBoostRecharged;

    private bool wasBoostEmpty = false;

    private void HandleBoostRecharge()
    {
        float previousBoost = currentBoost;
        
        if (!isPrimaryBoosting && currentBoost < maxBoost)
        {
            currentBoost += boostRechargeRate * Time.deltaTime;
            currentBoost = Mathf.Min(currentBoost, maxBoost);
            
            if (previousBoost < maxBoost && currentBoost >= maxBoost)
            {
                OnBoostRecharged?.Invoke();
            }
        }

        if (currentBoost <= 0 && !wasBoostEmpty)
        {
            wasBoostEmpty = true;
            OnBoostDepleted?.Invoke();
        }
        else if (currentBoost > 0 && wasBoostEmpty)
        {
            wasBoostEmpty = false;
        }

        OnBoostChanged?.Invoke(currentBoost / maxBoost);
    }
}
```

---

**Task 3.4: Collision Response Testing** *(2 hours)*

Add some obstacles to test physics:

1. Create several cubes in the scene (scale them to 2, 2, 2)
2. Add `Rigidbody` to each (make them heavy: Mass = 100)
3. Fly into them

**Expected:** Player bounces off realistically.

**If objects fly away wildly:** Increase their Mass to 500-1000

**If player gets stuck:** Check Rigidbody > Collision Detection = Continuous

---

**Task 3.5: Input Action Rebinding Test** *(1.5 hours)*

Test that controls work with:
- Xbox controller
- PlayStation controller
- Keyboard + Mouse (if you added bindings)

Document which feel best. We're prioritizing gamepad, but verify KB+M isn't broken.

---

**End of Day 3 Commit:**
```
git add .
git commit -m "Day 3: Input refinement, speed limits, collision testing"
```

---

### **📅 Day 4-5: Documentation & Week 1 Review (16 hours)**

**Task 4.1: Create Tuning Document** *(2 hours)*

Create `TuningValues.md` in your project root:

```markdown
# Phase 1 Tuning Values

## Current Best Settings (Updated: [Date])

### PlayerController
- Thrust Force: 50
- Strafe Force: 30
- Vertical Force: 25
- Pitch Speed: 80
- Yaw Speed: 80
- Max Normal Speed: 80
- Max Boost Speed: 120

### Rigidbody
- Mass: 1
- Drag: 0.2
- Angular Drag: 2

### Camera
- Follow Speed: 8
- Rotation Speed: 5

## Change Log
- [Date]: Initial values set
- [Date]: Reduced thrust to 50 from 60 (felt too fast)
```

Update this every time you change values.

---

**Task 4.2: Playtesting Protocol** *(4 hours)*

Spend time just *flying around*. Don't add features. Just experience what you've built.

**Use this checklist:**

```
[ ] Can I fly in a straight line without wobbling?
[ ] Can I make sharp 90° turns?
[ ] Can I fly through a tight gap (build one with cubes)?
[ ] Can I fly backward?
[ ] Does boosting feel powerful?
[ ] Does stopping feel natural?
[ ] After 10 minutes, do I want to keep flying?
```

Write down EVERYTHING that feels off in `TuningValues.md`.

---

**Task 4.3: External Playtester (Optional)** *(2 hours)*

If possible, have someone else try the controls.

Give them ZERO instructions. Watch where they get confused.

**Document:**
- What did they try first?
- What felt good immediately?
- What caused frustration?

---

**Task 4.4: Code Cleanup & Commenting** *(3 hours)*

Go through `PlayerController.cs` and add XML documentation:

```csharp
/// <summary>
/// Applies movement forces to the player rigidbody based on input.
/// Called in FixedUpdate for consistent physics.
/// </summary>
private void HandleMovement()
{
    // ...
}
```

This will help Future You understand your code.

---

**Task 4.5: Performance Profiling** *(3 hours)*

Open Unity Profiler: `Window > Analysis > Profiler`

Press Play and record for 30 seconds.

**Check:**
- CPU time: Should be <10ms per frame (for 60fps)
- Rendering: Should be <16ms
- Physics: Should be <5ms

**If any are higher:** We have a problem early (good to catch now!)

Common issues:
- Too many `GetComponent` calls per frame (cache them in Awake)
- Inefficient vector math (use `sqrMagnitude` instead of `magnitude` when possible)

---

**Task 4.6: Week 1 Review & Decision Point** *(2 hours)*

**Critical Questions:**

1. **Does the cube flying feel fun?**
   - Yes: Proceed to Week 2
   - No: Extend Week 1 by 3 days, focus on tuning

2. **Are there any show-stopping bugs?**
   - Physics freakouts?
   - Input not responding?
   - Crashes?

3. **Is framerate stable 60fps?**
   - Check in Profiler
   - If not, investigate NOW

**Make the call:** 
- ✅ **Green light:** Week 2 starts
- ⚠️ **Yellow light:** Fix issues, then Week 2
- 🛑 **Red light:** Major rework needed

---

**End of Week 1 Commit:**
```
git add .
git commit -m "Week 1 Complete: Core 6DoF movement functional and tuned"
git tag "Phase1-Week1-Complete"
```

---

## **🗓️ Week 2: Dynamics - Implementing the "Awesome" Mechanics**

**Weekly Goal:** To implement the signature mechanics that define the "Fluid Momentum" system: the Quick Boost and the High-G Drift.

**Total Time Budget:** 40 hours  
**Priority:** Get drift feeling *perfect*. This is the core innovation.

---

### **📅 Day 6: Quick Boost Implementation (8 hours)**

#### **Morning Session: Quick Boost Mechanics**

**Task 6.1: Add Quick Boost Variables** *(1 hour)*

Update `PlayerController.cs`:

```csharp
[Header("Quick Boost")]
[SerializeField] private float quickBoostForce = 1500f; // Impulse force
[SerializeField] private float quickBoostCost = 25f;
[SerializeField] private float quickBoostCooldown = 0.3f;
private float quickBoostCooldownTimer = 0f;

private void Update()
{
    HandleBoostRecharge();
    
    // Tick down cooldown
    if (quickBoostCooldownTimer > 0)
    {
        quickBoostCooldownTimer -= Time.deltaTime;
    }
}
```

---

**Task 6.2: Wire Up Input** *(30 minutes)*

```csharp
private void OnEnable()
{
    // ... existing code ...
    
    controls.Gameplay.QuickBoost.performed += OnQuickBoostPerformed;
}

private void OnDisable()
{
    controls.Gameplay.Disable();
    controls.Gameplay.QuickBoost.performed -= OnQuickBoostPerformed;
}

private void OnQuickBoostPerformed(InputAction.CallbackContext context)
{
    // Check if we can boost
    if (currentBoost >= quickBoostCost && quickBoostCooldownTimer <= 0)
    {
        ExecuteQuickBoost();
    }
}
```

---

**Task 6.3: Implement Quick Boost** *(2 hours)*

```csharp
private void ExecuteQuickBoost()
{
    // Determine boost direction (8-way based on move input)
    Vector3 boostDirection = Vector3.zero;
    
    if (moveInput.magnitude > 0.1f)
    {
        // Boost in direction of stick input (relative to player)
        boostDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
        boostDirection.Normalize();
    }
    else
    {
        // No input = boost forward
        boostDirection = transform.forward;
    }

    // Apply impulse force
    rb.AddForce(boostDirection * quickBoostForce, ForceMode.Impulse);

    // Consume boost
    currentBoost -= quickBoostCost;
    currentBoost = Mathf.Max(0, currentBoost);

    // Start cooldown
    quickBoostCooldownTimer = quickBoostCooldown;

    // Trigger event for VFX/SFX later
    OnQuickBoostExecuted?.Invoke();
}

// Add event
public event Action OnQuickBoostExecuted;
```

---

**Task 6.4: Tuning Quick Boost** *(2.5 hours)*

This is **critical**. The Quick Boost must feel like a **sharp, decisive burst**.

**Test Protocol:**
1. Stand still, press Quick Boost
   - Should "kick" you forward noticeably
   - Should NOT feel like holding RT (that's sustained boost)

2. Try Quick Boosting in different directions
   - Forward, back, left, right, diagonals
   - All should feel equally responsive

3. Try spamming Quick Boost
   - Cooldown should prevent spam (feels more deliberate)
   - But cooldown shouldn't feel restrictive

**Tuning Values to Experiment With:**
- `quickBoostForce`: Start at 1500, adjust by ±500
- `quickBoostCooldown`: 0.3s feels good, but try 0.2s and 0.5s
- `quickBoostCost`: 25 (from GDD), but might need adjustment

**Goal Feel:** Like a dodge-roll in Dark Souls, but in 3D space.

---

#### **Afternoon Session: Quick Boost Polish**

**Task 6.5: Add Screen Shake** *(1.5 hours)*

Create `Assets/_Project/Scripts/Camera/CameraShake.cs`:

```csharp
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private System.Collections.IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
```

Attach to Camera object.

In `PlayerController`:
```csharp
[SerializeField] private CameraShake cameraShake;

private void ExecuteQuickBoost()
{
    // ... existing code ...
    
    // Camera feedback
    if (cameraShake != null)
    {
        cameraShake.Shake(0.15f, 0.1f);
    }
}
```

**Test:** Quick Boost should now have a subtle screen shake.

---

**Task 6.6: Temporary Audio Placeholder** *(30 minutes)*

1. Go to freesound.org
2. Search "whoosh"
3. Download a free sound effect
4. Import to `Assets/_Project/Audio/SFX/`
5. Add AudioSource component to Player
6. Assign clip and call `audioSource.PlayOneShot()` in `ExecuteQuickBoost()`

**Don't spend time on perfect audio yet.** We just need to hear SOMETHING.

---

**End of Day 6 Commit:**
```
git add .
git commit -m "Day 6: Quick Boost implemented with screen shake and placeholder audio"
```

---

### **📅 Day 7-8: High-G Drift (THE BIG ONE) (16 hours)**

**This is the most important mechanic in the entire game.**

---

#### **Day 7 Morning: Understanding the Mechanic (4 hours)**

**Task 7.1: Research & Reference** *(1 hour)*

Watch these for inspiration:
- Zone of the Enders 2 gameplay (YouTube)
- Any space combat game with "flight assist off" (Elite Dangerous, Star Citizen)

**Key Concept:** Drift = velocity and facing direction are *decoupled*.

---

**Task 7.2: Add Drift State Variables** *(30 minutes)*

```csharp
[Header("High-G Drift")]
[SerializeField] private bool isDrifting = false;
[SerializeField] private float minimumDriftSpeed = 20f; // Can't drift if moving too slow
[SerializeField] private Color driftTrailColor = Color.yellow;

private void OnEnable()
{
    // ... existing code ...
    
    controls.Gameplay.Drift.performed += ctx => TryEnableDrift();
    controls.Gameplay.Drift.canceled += ctx => DisableDrift();
}

private void TryEnableDrift()
{
    if (rb.velocity.magnitude >= minimumDriftSpeed)
    {
        isDrifting = true;
    }
}

private void DisableDrift()
{
    isDrifting = false;
}
```

---

**Task 7.3: Implement Drift Movement Block** *(2 hours)*

The core mechanic: **When drifting, stop applying directional forces.**

```csharp
private void HandleMovement()
{
    // Only apply movement forces if NOT drifting
    if (!isDrifting)
    {
        Vector2 processedMove = ProcessInput(moveInput, stickDeadZone, movementResponseCurve);
        
        if (processedMove.y != 0)
        {
            rb.AddRelativeForce(Vector3.forward * processedMove.y * thrustForce);
        }

        if (processedMove.x != 0)
        {
            rb.AddRelativeForce(Vector3.right * processedMove.x * strafeForce);
        }

        if (isAscending)
        {
            rb.AddRelativeForce(Vector3.up * verticalForce);
        }
        if (isDescending)
        {
            rb.AddRelativeForce(Vector3.down * verticalForce);
        }

        if (isPrimaryBoosting && currentBoost > 0)
        {
            rb.AddRelativeForce(Vector3.forward * thrustForce * 1.5f);
            currentBoost -= 15f * Time.fixedDeltaTime;
            currentBoost = Mathf.Max(0, currentBoost);
        }
    }
    else
    {
        // During drift, velocity is preserved automatically by Rigidbody
        // No forces applied = pure momentum
    }
}
```

---

**Task 7.4: Test Drift v1** *(30 minutes)*

**Test Protocol:**
1. Boost forward to build up speed
2. Hold L3 (Drift)
3. Use right stick to look around

**Expected:** You should keep flying forward while being able to aim in any direction.

**If it doesn't work:**
- Check `isDrifting` in Inspector while playing
- Verify Rigidbody > Drag is LOW (0.2 or less)
- Check that no other scripts are affecting Rigidbody

---

#### **Day 7 Afternoon: Drift Polish (4 hours)**

**Task 7.5: Add Velocity Vector Visualization** *(1.5 hours)*

Players need to see their momentum direction while drifting.

Create `Assets/_Project/Scripts/UI/VelocityIndicator.cs`:

```csharp
using UnityEngine;

public class VelocityIndicator : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private float lineLength = 5f;

    private void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.yellow;
            lineRenderer.endColor = Color.yellow;
        }
    }

    private void Update()
    {
        if (playerRb.velocity.magnitude > 1f)
        {
            lineRenderer.enabled = true;
            Vector3 velocityDirection = playerRb.velocity.normalized;
            
            lineRenderer.SetPosition(0, player.position);
            lineRenderer.SetPosition(1, player.position + velocityDirection * lineLength);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
```

Attach to an empty GameObject in the scene, assign Player references.

**Test:** You should now see a yellow line showing your momentum direction.

---

**Task 7.6: Drift Visual Polish** *(1 hour)*

Make it visually obvious when drift is active:

1. Change thruster VFX color during drift (if you have VFX, we'll add next week)
2. Add subtle camera FOV change:

```csharp
// In PlayerController
private Camera mainCamera;

private void Start()
{
    mainCamera = Camera.main;
}

private void Update()
{
    HandleBoostRecharge();
    
    if (quickBoostCooldownTimer > 0)
    {
        quickBoostCooldownTimer -= Time.deltaTime;
    }
    
    // Drift FOV effect
    float targetFOV = isDrifting ? 70f : 60f;
    mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * 3f);
}
```

**Test:** FOV should widen slightly when drifting (feels faster).

---

**Task 7.7: Drift Movement Tuning** *(1.5 hours)*

**Critical Test:** Can you perform the "anime maneuver"?

**The Test:**
1. Boost forward to max speed
2. Enable drift
3. Rotate 180° (facing backward)
4. You should still be moving forward, but facing backward

**If this works, the mechanic is successful.**

Now tune:
- `minimumDriftSpeed`: Too high = hard to activate, too low = can drift while stationary (bad)
- Rigidbody Drag: Affects how long momentum lasts
- Angular Drag during drift (might want it lower for faster spin?)

---

#### **Day 8: Drift + Quick Boost Integration (8 hours)**

**Task 8.1: Allow Quick Boost During Drift** *(2 hours)*

This is a power move: Quick Boost should work during drift.

```csharp
private void OnQuickBoostPerformed(InputAction.CallbackContext context)
{
    if (currentBoost >= quickBoostCost && quickBoostCooldownTimer <= 0)
    {
        ExecuteQuickBoost();
    }
}

private void ExecuteQuickBoost()
{
    Vector3 boostDirection = Vector3.zero;
    
    if (isDrifting)
    {
        // During drift, Quick Boost in facing direction (allows drift corrections)
        boostDirection = transform.forward;
    }
    else
    {
        // Normal behavior
        if (moveInput.magnitude > 0.1f)
        {
            boostDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
            boostDirection.Normalize();
        }
        else
        {
            boostDirection = transform.forward;
        }
    }

    rb.AddForce(boostDirection * quickBoostForce, ForceMode.Impulse);
    currentBoost -= quickBoostCost;
    currentBoost = Mathf.Max(0, currentBoost);
    quickBoostCooldownTimer = quickBoostCooldown;
    
    if (cameraShake != null)
    {
        cameraShake.Shake(0.15f, 0.1f);
    }

    OnQuickBoostExecuted?.Invoke();
}
```

**Test:** 
1. Enter drift
2. Quick Boost forward (should add velocity in facing direction)
3. This allows "drift-boosting" for advanced movement

---

**Task 8.2: Drift Cancel Mechanics** *(1.5 hours)*

Design decision: Should drift auto-cancel if speed drops too low?

**Add:**
```csharp
private void Update()
{
    // ... existing code ...
    
    // Auto-cancel drift if too slow
    if (isDrifting && rb.velocity.magnitude < minimumDriftSpeed)
    {
        DisableDrift();
    }
}
```

**Test:** Drift should end if you slow down too much (prevents "stuck" state).

---

**Task 8.3: Advanced Drift Testing** *(3 hours)*

Build a test course with this script:

```
Create obstacles in a circle
Try to:
1. Fly forward through the ring
2. Enter drift
3. Rotate to look at the next ring
4. Quick Boost toward it (changing direction while maintaining momentum)
5. Exit drift
```

This is the "skill ceiling" test. If you can pull this off, the mechanic is deep enough.

---

**Task 8.4: Document Drift Behavior** *(1 hour)*

Update `TuningValues.md`:

```markdown
## Drift Mechanic Behavior

### When Drift Is Active:
- No directional input forces applied
- Velocity preserved (Rigidbody momentum)
- Rotation still works (pitch/yaw/roll)
- Quick Boost allowed (adds impulse in facing direction)
- Auto-cancels if speed < 20 m/s

### Tuning Values:
- Minimum Drift Speed: 20 m/s
- FOV Change: 60 → 70 when drifting
- Velocity Indicator: Yellow line, 5m length
```

---

**End of Day 8 Commit:**
```
git add .
git commit -m "Day 8: High-G Drift fully implemented and integrated with Quick Boost"
```

---

# **Week 2 Completion: Days 9-10**

## **📅 Day 9: Integration Testing & Combo Discovery (8 hours)**

**Daily Goal:** Test how Quick Boost and Drift work together. Discover emergent movement techniques that will become the game's skill ceiling.

---

### **Morning Session (4 hours): Combo Testing**

**Task 9.1: Build Advanced Movement Test Course** *(90 minutes)*

Create a new scene: `Assets/_Project/Scenes/Phase1_MovementCourse.unity`

**Course Layout:**

1. **Straight Speed Section** (100m long tunnel)
   - Tests sustained boost + quick boost chaining
   - Add speed gates (empty GameObjects with triggers)
   
2. **Slalom Section** (zigzag through pillars)
   - Tests quick directional changes
   - 10 pillars, 20m apart, offset left/right
   
3. **Drift Circle** (large arena with targets on walls)
   - Tests circle-strafing while drifting
   - Create ring of cubes (30m diameter)
   
4. **Vertical Shaft** (tall cylinder going up/down)
   - Tests ascend/descend control
   - 50m tall, 15m wide
   
5. **Obstacle Maze** (tight corridors with 90° turns)
   - Tests precision + boost management
   - Multiple paths, narrow gaps

**Build this with primitives:**

```csharp
// Helper script to auto-generate course
// Assets/_Project/Scripts/Utilities/CourseBuilder.cs

using UnityEngine;

public class CourseBuilder : MonoBehaviour
{
    [ContextMenu("Build Speed Tunnel")]
    private void BuildSpeedTunnel()
    {
        for (int i = 0; i < 20; i++)
        {
            // Floor
            GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.transform.position = new Vector3(0, -5, i * 5);
            floor.transform.localScale = new Vector3(10, 1, 5);
            floor.name = "Tunnel_Floor_" + i;
            
            // Left wall
            GameObject leftWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            leftWall.transform.position = new Vector3(-5, 0, i * 5);
            leftWall.transform.localScale = new Vector3(1, 10, 5);
            leftWall.name = "Tunnel_Left_" + i;
            
            // Right wall
            GameObject rightWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            rightWall.transform.position = new Vector3(5, 0, i * 5);
            rightWall.transform.localScale = new Vector3(1, 10, 5);
            rightWall.name = "Tunnel_Right_" + i;
            
            // Ceiling
            GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ceiling.transform.position = new Vector3(0, 5, i * 5);
            ceiling.transform.localScale = new Vector3(10, 1, 5);
            ceiling.name = "Tunnel_Ceiling_" + i;
        }
    }
    
    [ContextMenu("Build Slalom Course")]
    private void BuildSlalom()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject pillar = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            float xOffset = (i % 2 == 0) ? -8f : 8f;
            pillar.transform.position = new Vector3(xOffset, 0, i * 20);
            pillar.transform.localScale = new Vector3(2, 10, 2);
            pillar.name = "Slalom_Pillar_" + i;
        }
    }
}
```

Attach this to an empty GameObject, use the context menu to build sections.

**Success Check:**
- [ ] Course has 5 distinct test sections
- [ ] Each section tests a different skill
- [ ] Player spawn point is at course start

---

**Task 9.2: Movement Technique Discovery** *(90 minutes)*

**The Goal:** Find and document advanced techniques players will discover.

**Systematic Testing Protocol:**

Create a new document: `Assets/_Project/MovementTechniques.md`

```markdown
# Discovered Movement Techniques

## 1. Boost Chaining
**How:** Quick Boost → wait 0.3s → Quick Boost again
**Effect:** Maintains higher average speed than sustained boost
**Skill:** Timing cooldown perfectly
**Boost Cost:** High (50 per 2 boosts)

## 2. Drift Turn
**How:** Boost forward → Drift → Rotate 90° → Exit drift → Boost in new direction
**Effect:** Sharp turns without losing much speed
**Skill:** Knowing when to exit drift
**Use Case:** Navigating corners

## 3. Reverse Strafe
**How:** Boost backward → Drift → Face target → Fire while retreating
**Effect:** Maintain distance from enemy while attacking
**Skill:** Aiming while drifting backward
**Use Case:** Kiting enemies (combat)

## 4. Spiral Ascent
**How:** Hold Ascend + rotate with Look stick while drifting
**Effect:** Corkscrew upward movement
**Skill:** Coordinating rotation + vertical movement
**Use Case:** Dodging, looking cool

## 5. [Your discoveries here]
```

**Your Task:** Spend 90 minutes just *flying* and trying weird input combinations.

**Document anything that:**
- Feels skillful to execute
- Looks cool
- Has a practical use case
- Emerges from system interactions (not explicitly designed)

**Examples to Try:**
- Can you Quick Boost while ascending?
- Can you drift in a circle around a point?
- Can you maintain altitude while boosting forward?
- What happens if you Quick Boost backward during drift?

---

### **Afternoon Session (4 hours): Refinement & Bug Fixing**

**Task 9.3: Edge Case Testing** *(2 hours)*

**Test these scenarios and fix any broken behavior:**

**Test Case 1: Drift at Low Speed**
```
1. Slow down to barely moving (5 m/s)
2. Try to activate drift
Expected: Should NOT activate (below minimumDriftSpeed)
Actual: [Document what happens]
```

**Test Case 2: Boost Depletion During Drift**
```
1. Enter drift with low boost
2. Quick Boost until boost = 0
3. Try to Quick Boost again
Expected: Nothing happens, audio cue for "empty"
Actual: [Document]
```

**Test Case 3: Collision During Drift**
```
1. Enter drift at high speed
2. Collide with wall head-on
Expected: Bounce off, maintain drift state
Actual: [Document - does drift cancel? Should it?]
```

**Test Case 4: Simultaneous Inputs**
```
1. Hold Primary Boost + press Quick Boost
Expected: Quick Boost should interrupt/stack with primary
Actual: [Document]
```

**Test Case 5: Camera Behavior During Rapid Rotation**
```
1. Enter drift
2. Spin 360° as fast as possible
Expected: Camera follows smoothly, no jitter
Actual: [Document - is there gimbal lock?]
```

**Fix any bugs you find immediately.**

---

**Task 9.4: Boost Management Tuning** *(1.5 hours)*

The boost gauge is a critical resource. Let's ensure the economy feels right.

**Create a test scenario:**

```csharp
// Add to PlayerController for testing
[Header("Debug")]
[SerializeField] private bool infiniteBoost = false;

private void HandleBoostRecharge()
{
    if (infiniteBoost)
    {
        currentBoost = maxBoost;
        return;
    }
    
    // ... existing recharge code
}
```

**Test with infinite boost OFF:**

**Scenario A: Sustained Combat Movement**
```
Goal: Fly through slalom course using only Quick Boost (no primary boost)
Time it: How many boosts can you do before empty?
Current: [Fill in]
Target: Should be able to do 4-5 quick boosts in a row
```

**Scenario B: Speed Run**
```
Goal: Complete speed tunnel as fast as possible
Strategy: Mix primary boost + quick boost chains
Current completion time: [Fill in]
Does boost run out before finish? [Yes/No]
```

**Tuning Levers:**

If boost feels too restrictive:
- Increase `boostRechargeRate` (30 → 40)
- Decrease `quickBoostCost` (25 → 20)
- Increase `maxBoost` (100 → 120)

If boost feels infinite (no resource management):
- Decrease `boostRechargeRate` (30 → 20)
- Increase `quickBoostCost` (25 → 30)

**Goal:** Player should feel boost pressure in a 30-second engagement, but not be constantly empty.

---

**Task 9.5: Input Responsiveness Check** *(30 minutes)*

**Critical Test: Input Latency**

1. Open `Edit > Project Settings > Input System Package`
2. Set `Update Mode` to `Process Events In Dynamic Update` (should already be set)
3. Enable `Input Debugger`: `Window > Analysis > Input Debugger`

**Test:**
1. Press Quick Boost button
2. Watch for cube movement

**Target:** Movement should occur within 2 frames (33ms at 60fps)

**If there's noticeable lag:**
- Check you're using `FixedUpdate()` for physics
- Ensure Rigidbody > Interpolate = Interpolate
- Check Vsync isn't causing input buffering

---

**End of Day 9 Commit:**
```
git add .
git commit -m "Day 9: Movement test course built, advanced techniques documented, edge cases tested"
```

---

## **📅 Day 10: Week 2 Review & Documentation (8 hours)**

**Daily Goal:** Polish everything from Week 2, create comprehensive documentation, and make the GO/NO-GO decision for Week 3.

---

### **Morning Session (4 hours): Polish Pass**

**Task 10.1: Control Feel Final Tuning** *(2 hours)*

**The "10-Minute Test":**

Set a timer for 10 minutes. Just fly around the test course.

**After 10 minutes, rate these statements (1-10):**

```
[ /10] The controls feel responsive
[ /10] Quick Boost feels powerful and satisfying
[ /10] Drift feels smooth and intuitive
[ /10] I understand when to use each mechanic
[ /10] I want to keep playing after 10 minutes
[ /10] I can imagine this being fun for hours

Total: [ /60]

Minimum acceptable score: 45/60
Target score: 50+/60
```

**If total < 45:** Something is fundamentally wrong. Identify what.

Common issues at this stage:
- Controls feel "mushy" → Increase force values
- Drift feels "pointless" → Make velocity preservation more obvious (better VFX needed)
- Quick Boost feels "weak" → Increase impulse force
- Boost management is frustrating → Adjust recharge rate

**Spend the full 2 hours on micro-adjustments.** This is the foundation of the entire game.

---

**Task 10.2: Code Cleanup** *(1 hour)*

**Refactor `PlayerController.cs` for clarity:**

The script is getting long. Organize it:

```csharp
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region Serialized Fields
    [Header("References")]
    private Rigidbody rb;
    private PlayerControls controls;
    
    [Header("Movement Forces")]
    [SerializeField] private float thrustForce = 50f;
    [SerializeField] private float strafeForce = 30f;
    [SerializeField] private float verticalForce = 25f;
    
    [Header("Boost System")]
    [SerializeField] private float maxBoost = 100f;
    [SerializeField] private float boostRechargeRate = 30f;
    [SerializeField] private float boostRechargeDelay = 0.5f;
    
    [Header("Quick Boost")]
    [SerializeField] private float quickBoostForce = 1500f;
    [SerializeField] private float quickBoostCost = 25f;
    [SerializeField] private float quickBoostCooldown = 0.3f;
    
    [Header("High-G Drift")]
    [SerializeField] private float minimumDriftSpeed = 20f;
    
    [Header("Rotation")]
    [SerializeField] private float pitchSpeed = 80f;
    [SerializeField] private float yawSpeed = 80f;
    
    [Header("Speed Limits")]
    [SerializeField] private float maxNormalSpeed = 80f;
    [SerializeField] private float maxBoostSpeed = 120f;
    
    [Header("Input Settings")]
    [SerializeField] private float stickDeadZone = 0.15f;
    
    [Header("Feedback")]
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip quickBoostSound;
    
    [Header("Debug")]
    [SerializeField] private bool infiniteBoost = false;
    #endregion
    
    #region Private Fields
    private float currentBoost = 100f;
    private float quickBoostCooldownTimer = 0f;
    private bool isDrifting = false;
    private bool wasBoostEmpty = false;
    
    // Input values
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isAscending;
    private bool isDescending;
    private bool isPrimaryBoosting;
    #endregion
    
    #region Events
    public event Action<float> OnBoostChanged;
    public event Action OnBoostDepleted;
    public event Action OnBoostRecharged;
    public event Action OnQuickBoostExecuted;
    public event Action<bool> OnDriftStateChanged;
    #endregion
    
    #region Unity Lifecycle
    private void Awake() { /* ... */ }
    private void OnEnable() { /* ... */ }
    private void OnDisable() { /* ... */ }
    private void Update() { /* ... */ }
    private void FixedUpdate() { /* ... */ }
    #endregion
    
    #region Movement
    private void HandleMovement() { /* ... */ }
    private void HandleRotation() { /* ... */ }
    private void CapVelocity() { /* ... */ }
    #endregion
    
    #region Boost System
    private void HandleBoostRecharge() { /* ... */ }
    private void OnQuickBoostPerformed(InputAction.CallbackContext context) { /* ... */ }
    private void ExecuteQuickBoost() { /* ... */ }
    #endregion
    
    #region Drift System
    private void TryEnableDrift() { /* ... */ }
    private void DisableDrift() { /* ... */ }
    #endregion
    
    #region Input Processing
    private Vector2 ProcessInput(Vector2 rawInput, float deadZone) { /* ... */ }
    #endregion
    
    #region Debug
    private void OnDrawGizmos() { /* ... */ }
    #endregion
}
```

**Use `#region` blocks to collapse sections.** This makes the 400+ line script manageable.

**Add XML documentation to public methods:**

```csharp
/// <summary>
/// Executes a Quick Boost in the current facing direction (or input direction if not drifting).
/// Consumes boost gauge and applies impulse force to rigidbody.
/// </summary>
private void ExecuteQuickBoost()
{
    // ...
}
```

---

**Task 10.3: Performance Profiling** *(1 hour)*

**Open Unity Profiler:** `Window > Analysis > Profiler`

**Record 60 seconds of gameplay:**
1. Fly through entire test course
2. Use all mechanics (boost, drift, quick boost)
3. Collide with walls
4. Stop recording

**Check these metrics:**

| Metric | Target | Current | Status |
|--------|--------|---------|--------|
| CPU Main Thread | <10ms | [ ] ms | [ ] |
| Rendering | <16ms | [ ] ms | [ ] |
| Physics | <5ms | [ ] ms | [ ] |
| Scripts | <3ms | [ ] ms | [ ] |
| GC Alloc per frame | <1KB | [ ] KB | [ ] |

**If anything is over target:**

**CPU Main Thread high:**
- Check for expensive `GetComponent` calls in Update
- Cache component references in Awake

**Rendering high:**
- Shouldn't happen with primitives, but check draw calls
- Ensure static batching is enabled

**Physics high:**
- Check Rigidbody count (should be ~1-5 at this stage)
- Verify Fixed Timestep = 0.01666667

**Scripts high:**
- Profile which script is expensive
- Look for loops in Update()

**GC Alloc high:**
- Avoid `new` in Update or FixedUpdate
- Don't use LINQ in hot paths
- Cache Vector3 calculations

**Fix performance issues now before they compound.**

---

### **Afternoon Session (4 hours): Documentation & Review**

**Task 10.4: Create Player-Facing Control Guide** *(1 hour)*

Create: `Assets/_Project/ControlsGuide.md`

```markdown
# CRIMSON COMET - Controls Guide (Phase 1 Prototype)

## Basic Movement
- **Left Stick**: Move forward/back, strafe left/right
- **A / X (PS)**: Ascend (fly up)
- **Y / Triangle (PS)**: Descend (fly down)
- **Right Stick**: Pitch and Yaw (aim)

## Boost System
- **Right Trigger**: Primary Boost (sustained thrust)
  - Consumes 15 boost/second
  - Increases max speed to 120 m/s
  
- **B / Circle (PS)**: Quick Boost (evasive dash)
  - Costs 25 boost
  - 0.3 second cooldown
  - Direction based on left stick input
  - Can be used during drift

## Advanced: High-G Drift
- **L3 (Left Stick Click)**: Hold to enable drift mode
  - **Requires**: Moving at least 20 m/s
  - **Effect**: Momentum is preserved, rotation is free
  - **Use**: Circle-strafe, retreat while firing, anime maneuvers
  - **Exit**: Release L3 OR slow below 20 m/s

## Boost Management
- **Max Capacity**: 100 units
- **Recharge Rate**: 30 units/second (when not boosting)
- **Quick Boost Cost**: 25 units (4 boosts before empty)
- **Primary Boost Cost**: 15 units/second (~6.6 seconds of boost)

## Advanced Techniques

### Boost Chaining
Rapidly tap Quick Boost to maintain high speed without using Primary Boost.
**Efficiency**: Higher burst speed, but costs more boost overall.

### Drift Turn
1. Boost to build speed
2. Activate drift
3. Rotate to new direction
4. Deactivate drift and boost in new direction
**Use**: Sharp turns without losing momentum

### Reverse Strafe
1. Boost backward
2. Activate drift
3. Rotate to face forward
**Use**: Retreat while maintaining aim (critical for combat)

## Tips
- Drift is speed-dependent: faster = longer drift duration
- Quick Boost recharges faster than Primary Boost consumes
- Collisions don't cancel drift (use this to wall-bounce)
```

**This document will be gold for new playtesters.**

---

**Task 10.5: Update Tuning Document** *(30 minutes)*

Update `TuningValues.md` with final Week 2 values:

```markdown
# Week 2 Final Tuning Values

## PlayerController - Movement
- Thrust Force: 50
- Strafe Force: 30
- Vertical Force: 25
- Max Normal Speed: 80 m/s
- Max Boost Speed: 120 m/s

## PlayerController - Boost System
- Max Boost: 100
- Boost Recharge Rate: 30/second
- Primary Boost Cost: 15/second
- Quick Boost Force: 1500 (impulse)
- Quick Boost Cost: 25
- Quick Boost Cooldown: 0.3 seconds

## PlayerController - Drift
- Minimum Drift Speed: 20 m/s
- Auto-cancel: Yes (when speed < 20 m/s)
- Quick Boost Allowed During Drift: Yes

## Rigidbody
- Mass: 1
- Drag: 0.2
- Angular Drag: 2
- Interpolate: On
- Collision Detection: Continuous

## Camera
- Follow Speed: 8
- Rotation Speed: 5
- Normal FOV: 60
- Drift FOV: 70

## Input
- Stick Dead Zone: 0.15
- Look Sensitivity: Default (1.0)

## Performance (Target)
- Frame Rate: 60 fps stable
- CPU: <10ms
- Physics: <5ms

## Change Log
- Day 10: Final Week 2 values locked
- Day 9: Reduced quick boost cooldown to 0.3s (was 0.4s)
- Day 8: Added drift auto-cancel at low speed
- Day 7: Set minimum drift speed to 20 m/s
- Day 6: Initial quick boost implementation
```

---

**Task 10.6: External Playtesting** *(1.5 hours)*

**If you have someone available:**

**Playtest Protocol:**

1. **Don't explain anything.** Hand them the controller.
2. Let them play for 5 minutes.
3. Observe:
   - What do they try first?
   - Do they discover Quick Boost?
   - Do they accidentally activate Drift?
   - What causes frustration?

4. After 5 minutes, show them the Controls Guide.
5. Let them play for 10 more minutes.
6. Ask these questions:

```
Questions for Playtester:
1. On a scale of 1-10, how fun was flying around? [ /10]
2. Which mechanic felt the best? [Answer]
3. Which mechanic felt confusing? [Answer]
4. Did you understand what Drift does? [Yes/No]
5. Did you feel "in control" or "fighting the controls"? [Answer]
6. What would make it more fun? [Answer]
7. Would you play this for an hour? [Yes/No]
```

**Document their feedback in:** `Assets/_Project/PlaytestFeedback_Week2.md`

**If playtester is NOT available:**

Record yourself playing for 10 minutes. Watch it back. Pretend you're seeing it for the first time. What looks confusing?

---

**Task 10.7: Week 2 Review & GO/NO-GO Decision** *(1 hour)*

**Fill out this checklist:**

## Week 2 Success Criteria

### Functional Requirements
- [ ] Quick Boost is implemented and functional
- [ ] High-G Drift is implemented and functional
- [ ] Boost gauge recharges correctly
- [ ] Quick Boost has cooldown system
- [ ] Drift preserves momentum while allowing free rotation
- [ ] Quick Boost works during drift
- [ ] Drift auto-cancels below minimum speed
- [ ] All inputs are responsive (<50ms latency)
- [ ] No game-breaking bugs

### Feel Requirements
- [ ] Quick Boost feels like a "kick" (satisfying impact)
- [ ] Drift clearly shows momentum direction
- [ ] Boost management feels meaningful (not infinite)
- [ ] Controls feel responsive (not floaty)
- [ ] Camera follows smoothly during maneuvers
- [ ] At least 2 advanced techniques discovered
- [ ] Movement test course is completable

### Performance Requirements
- [ ] Stable 60fps in test scene
- [ ] CPU < 10ms per frame
- [ ] Physics < 5ms per frame
- [ ] No memory leaks (GC stable)

### Documentation Requirements
- [ ] All code is commented
- [ ] TuningValues.md is updated
- [ ] ControlsGuide.md is complete
- [ ] MovementTechniques.md has 3+ techniques
- [ ] Playtest feedback is documented (if available)

### Subjective Quality Check
- [ ] **Critical**: Is flying the cube fun for 10+ minutes?
- [ ] Do Quick Boost and Drift feel meaningfully different?
- [ ] Can you imagine combat being built on this foundation?
- [ ] Are you personally excited to continue building this?

**Scoring:**
- All checkboxes checked: **GREEN LIGHT** → Proceed to Week 3
- 1-3 boxes unchecked: **YELLOW LIGHT** → Fix issues, then proceed
- 4+ boxes unchecked: **RED LIGHT** → Extend Week 2 or re-evaluate

---

**Task 10.8: Final Week 2 Commit** *(30 minutes)*

**Clean up your workspace:**

1. Delete any test objects you don't need
2. Organize Hierarchy (use empty GameObjects as folders)
3. Ensure all scripts are in correct folders
4. Delete unused materials/assets

**Final commit:**

```
git add .
git commit -m "Week 2 Complete: Quick Boost and High-G Drift fully implemented and polished"
git tag "Phase1-Week2-Complete"
git push origin main --tags
```

**Create a build:**

`File > Build Settings > PC, Mac & Linux Standalone > Build`

Save it as: `Builds/Phase1_Week2_Prototype.exe`

**Why:** If something breaks in Week 3, you can always revert to this working state.

---

## **🎯 Week 2 Deliverables Checklist**

Before moving to Week 3, verify you have:

**Code:**
- [ ] `PlayerController.cs` - fully functional with Quick Boost and Drift
- [ ] `SimpleCameraFollow.cs` - smooth camera that follows player
- [ ] `CameraShake.cs` - screen shake on Quick Boost
- [ ] `VelocityIndicator.cs` - visual momentum direction indicator
- [ ] `CourseBuilder.cs` - utility to build test levels

**Scenes:**
- [ ] `Phase1_Movement.unity` - basic test scene
- [ ] `Phase1_MovementCourse.unity` - advanced test course

**Documentation:**
- [ ] `TuningValues.md` - all parameters documented
- [ ] `ControlsGuide.md` - player-facing control reference
- [ ] `MovementTechniques.md` - discovered advanced techniques
- [ ] `PlaytestFeedback_Week2.md` - external feedback (if available)

**Assets:**
- [ ] Player prefab (cube with materials)
- [ ] Basic materials (M_PlayerBody, M_Cockpit, M_Grid)
- [ ] Placeholder audio (quick boost sound)
- [ ] Test course geometry

**Build:**
- [ ] Week 2 executable build archived

**Git:**
- [ ] All work committed
- [ ] Tagged as "Phase1-Week2-Complete"

---

## **📊 Week 2 Retrospective**

**What Went Well:**
- [Fill in after completing week]

**What Was Challenging:**
- [Fill in after completing week]

**What to Improve for Week 3:**
- [Fill in after completing week]

**Key Learnings:**
- [Fill in after completing week]

---

## **🚀 Next Steps: Week 3 Preview**

**If Week 2 was successful, Week 3 will focus on:**

1. **Camera System Upgrade** - Cinemachine integration for cinematic feel
2. **Visual Feedback ("Juice")** - Particle effects for thrusters, boosts, and drift
3. **Audio Feedback** - Proper sound design for all mechanics
4. **Environment Polish** - Make the gray box slightly prettier (still no final art)
5. **Parameter Tuning** - Fine-tune everything based on Week 2 learnings

**Week 3 Goal:** Make the prototype *feel* amazing through feedback and polish.

---

## **⚠️ Critical Decisions for Week 3**

Before starting Week 3, answer these:

**Question 1: Is the core movement fun enough to build a game on?**
- If NO: Stop and fix it now. Everything else depends on this.

**Question 2: Do you understand why Drift is special?**
- If NO: Re-watch ZOE2 gameplay, try Drift more, ask for feedback.

**Question 3: Are you still excited about this project?**
- If NO: Take a day off, then reassess.

---

**End of Week 2! 🎉**

You now have a **functional, playable prototype** of the core "Fluid Momentum" system. The hardest conceptual work is done. Week 3 will make it *feel* incredible.

**Commit and celebrate this milestone.** You've built something that doesn't exist in any other game.

---

# **CRIMSON COMET: Week 3 Development Roadmap**

## **🗓️ Week 3: Context - Camera, Feedback, and Tuning**

**Weekly Goal:** Transform the functional prototype into something that *feels* amazing. Add all the visual, audio, and haptic feedback ("juice") that makes mechanics satisfying. Upgrade the camera system for dynamic action.

**Philosophy:** "Feel" is everything. A mediocre mechanic with great feedback beats a great mechanic with no feedback. This week is about making every action *scream* at the player that something important happened.

**Core Focus Areas:**
1. **Camera System** - Dynamic, cinematic camera that enhances action
2. **Visual Effects** - Particles, trails, flashes that communicate state
3. **Audio Design** - Layered sound that provides feedback and immersion
4. **Screen Effects** - Post-processing, screen shake, motion effects
5. **Haptic Feedback** - Controller rumble patterns
6. **Polish Pass** - Micro-adjustments that add up to "game feel"

**Estimated Duration:** 5 Days (40 hours)

**Success Criteria:**
- ✅ Camera automatically frames action without player input
- ✅ Every mechanic has clear visual feedback
- ✅ Audio conveys game state (boost level, speed, etc.)
- ✅ Controls feel "punchy" and responsive
- ✅ External playtester says "This feels really polished"
- ✅ 60fps stable with all VFX active

---

## **📅 Day 11: Advanced Camera System (8 hours)**

**Daily Goal:** Replace the basic camera follow script with a professional-grade Cinemachine setup that dynamically responds to player actions.

---

### **Morning Session (4 hours): Cinemachine Setup**

**Task 11.1: Install and Configure Cinemachine** *(45 minutes)*

1. Open Package Manager: `Window > Package Manager`
2. Search for "Cinemachine"
3. Install **Cinemachine 3.0.0+** (Unity 6 compatible)
4. Wait for import to complete

**Verify Installation:**
- New menu appears: `GameObject > Cinemachine`
- Cinemachine window available: `Window > Cinemachine`

**Delete Old Camera Setup:**
- Delete your `CameraRig` GameObject
- Delete `SimpleCameraFollow.cs` script (archive in a "Deprecated" folder)

**Success Check:**
- [ ] Cinemachine package installed
- [ ] Old camera deleted
- [ ] No console errors

---

**Task 11.2: Create Base Virtual Camera** *(1 hour)*

**Understanding Cinemachine:**
- **Brain**: Lives on your Main Camera, processes Virtual Camera inputs
- **Virtual Cameras**: Invisible cameras that tell the Brain where to look
- You can have multiple VCams and blend between them

**Setup:**

1. Select your **Main Camera**
2. Add Component: `Cinemachine Brain`
3. In the Brain settings:
   - Update Method: **Late Update**
   - Blend Update Method: **Late Update**
   - Default Blend: **EaseInOut, 1.0 seconds**

4. Create Virtual Camera:
   - `GameObject > Cinemachine > Targeted Cameras > Follow Camera`
   - Name it: `CM_FollowPlayer`
   - Position: Doesn't matter (Cinemachine will control it)

5. Configure the Virtual Camera:
   - **Tracking Target**: Drag your `Player` GameObject here
   - **Look At Target**: Also drag `Player`
   - **Lens > Field of View**: 60
   - **Lens > Near/Far Clip Planes**: 0.1 / 1000

**Test:** Press Play. Camera should now follow the player.

---

**Task 11.3: Configure Follow Settings** *(1.5 hours)*

Select `CM_FollowPlayer` and adjust the **Position Composer** component:

**Camera Distance Settings:**
```
Camera Distance: 12 (how far back from player)
Camera Side: 0 (centered behind player)
Camera Height: 3 (slightly above player)
Damping:
  - X: 0.5 (left/right smoothing)
  - Y: 0.3 (up/down smoothing)
  - Z: 0.5 (forward/back smoothing)
```

**Rotation Composer Settings:**
```
Tilt: 10 degrees (look down slightly)
Screen Position:
  - X: 0.5 (center horizontally)
  - Y: 0.45 (slightly lower than center - player visible)
Dead Zone:
  - Width: 0.1
  - Height: 0.1
Soft Zone:
  - Width: 0.6
  - Height: 0.6
```

**What These Do:**
- **Dead Zone**: Player can move this much without camera rotating
- **Soft Zone**: Camera gently rotates to reframe
- **Outside Soft Zone**: Camera rotates aggressively

**Tuning Process:**

Enter Play Mode and fly around while adjusting these values:

**Test Scenarios:**

1. **Straight Line Flight:**
   - Camera should follow smoothly
   - Player should stay roughly centered
   - Horizon should be stable

2. **Sharp Turns:**
   - Camera should swing around player
   - Should NOT feel sluggish
   - Should NOT make you motion sick

3. **Vertical Movement:**
   - Camera should tilt up/down smoothly
   - Damping Y controls this

4. **Drift Circle-Strafing:**
   - Camera should keep player in frame
   - Should show where player is going

**Copy values when you exit Play Mode!**

---

**Task 11.4: Create Look-At Offset** *(45 minutes)*

Problem: Camera is looking at the player's center, but we want it to look slightly ahead in the direction of movement.

**Solution: Add a Look Target**

1. Create empty GameObject as child of Player: `LookTarget`
2. Position it at: (0, 0, 5) - 5 units ahead of player
3. In `CM_FollowPlayer`:
   - Change **Look At Target** from `Player` to `LookTarget`

**Now create a script to move the look target based on velocity:**

`Assets/_Project/Scripts/Camera/DynamicLookTarget.cs`:

```csharp
using UnityEngine;

public class DynamicLookTarget : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private float maxLookAheadDistance = 15f;
    [SerializeField] private float lookAheadSpeed = 5f;
    
    private void LateUpdate()
    {
        if (playerRb.velocity.magnitude > 1f)
        {
            // Calculate look-ahead point based on velocity
            Vector3 velocityDirection = playerRb.velocity.normalized;
            float speedFactor = Mathf.Clamp01(playerRb.velocity.magnitude / 100f);
            
            Vector3 targetPosition = player.position + (velocityDirection * maxLookAheadDistance * speedFactor);
            
            // Smoothly move look target
            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                lookAheadSpeed * Time.deltaTime
            );
        }
        else
        {
            // When stationary, look at player
            transform.position = Vector3.Lerp(
                transform.position,
                player.position + player.forward * 5f,
                lookAheadSpeed * Time.deltaTime
            );
        }
    }
}
```

Attach this to `LookTarget`, assign references.

**Test:** Camera should now look ahead in the direction you're moving (feels more dynamic).

---

### **Afternoon Session (4 hours): Camera States & Polish**

**Task 11.5: Speed-Based FOV Adjustment** *(1.5 hours)*

**Goal:** FOV increases when moving fast (feels faster), narrows when slow (more precise).

Update `DynamicLookTarget.cs` to also control FOV:

```csharp
using UnityEngine;
using Unity.Cinemachine;

public class DynamicLookTarget : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private CinemachineCamera virtualCamera;
    
    [Header("Look Ahead")]
    [SerializeField] private float maxLookAheadDistance = 15f;
    [SerializeField] private float lookAheadSpeed = 5f;
    
    [Header("Dynamic FOV")]
    [SerializeField] private float baseFOV = 60f;
    [SerializeField] private float maxSpeedFOV = 75f;
    [SerializeField] private float driftFOV = 70f;
    [SerializeField] private float fovTransitionSpeed = 3f;
    [SerializeField] private float maxSpeedThreshold = 100f;
    
    private PlayerController playerController; // Reference to check drift state
    
    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }
    
    private void LateUpdate()
    {
        UpdateLookTarget();
        UpdateFOV();
    }
    
    private void UpdateLookTarget()
    {
        if (playerRb.velocity.magnitude > 1f)
        {
            Vector3 velocityDirection = playerRb.velocity.normalized;
            float speedFactor = Mathf.Clamp01(playerRb.velocity.magnitude / 100f);
            
            Vector3 targetPosition = player.position + (velocityDirection * maxLookAheadDistance * speedFactor);
            
            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                lookAheadSpeed * Time.deltaTime
            );
        }
        else
        {
            transform.position = Vector3.Lerp(
                transform.position,
                player.position + player.forward * 5f,
                lookAheadSpeed * Time.deltaTime
            );
        }
    }
    
    private void UpdateFOV()
    {
        float targetFOV;
        
        // Check if drifting (you'll need to add a public property to PlayerController)
        if (playerController != null && playerController.IsDrifting)
        {
            targetFOV = driftFOV;
        }
        else
        {
            // Speed-based FOV
            float speedFactor = Mathf.Clamp01(playerRb.velocity.magnitude / maxSpeedThreshold);
            targetFOV = Mathf.Lerp(baseFOV, maxSpeedFOV, speedFactor);
        }
        
        // Smoothly transition
        float currentFOV = virtualCamera.Lens.FieldOfView;
        virtualCamera.Lens.FieldOfView = Mathf.Lerp(currentFOV, targetFOV, fovTransitionSpeed * Time.deltaTime);
    }
}
```

**Add to PlayerController:**
```csharp
// Make drift state readable
public bool IsDrifting => isDrifting;
```

**Test:**
- Stand still: FOV = 60
- Boost to max speed: FOV should increase to 75 (feels faster!)
- Enter drift: FOV = 70 (distinct visual state)

---

**Task 11.6: Camera Shake Integration** *(1 hour)*

**Upgrade the camera shake to work with Cinemachine:**

Delete old `CameraShake.cs` from Week 2.

**New approach using Cinemachine Impulse:**

1. Add to Player GameObject: `CinemachineImpulseSource` component
2. Configure:
   - Generate Impulse: Use Default Velocity (or custom)
   - Amplitude Gain: 1.0
   - Frequency Gain: 1.0
   - Duration: 0.2

3. Add to Main Camera: `CinemachineImpulseListener` component
4. Configure:
   - Gain: 1.0
   - Use camera space: Checked

**Update PlayerController to use new shake:**

```csharp
[Header("Feedback")]
[SerializeField] private CinemachineImpulseSource impulseSource;

private void Awake()
{
    rb = GetComponent<Rigidbody>();
    controls = new PlayerControls();
    impulseSource = GetComponent<CinemachineImpulseSource>();
}

private void ExecuteQuickBoost()
{
    // ... existing boost code ...
    
    // Camera shake
    if (impulseSource != null)
    {
        impulseSource.GenerateImpulse(Vector3.one * 0.5f);
    }
    
    OnQuickBoostExecuted?.Invoke();
}
```

**Test:** Quick Boost should cause a sharp camera shake.

---

**Task 11.7: Camera Collision Prevention** *(1 hour)*

Problem: Camera can clip through walls.

**Solution: Cinemachine Deoccluder**

1. Select `CM_FollowPlayer`
2. Add Extension: `CinemachineDeoccluder`
3. Configure:
   - Damping: 0.5
   - Damping When Occluded: 0
   - Optimal Target Distance: 12 (your camera distance)
   - Camera Radius: 0.5

**Test:**
- Fly close to a wall
- Camera should automatically move closer to player to avoid clipping

---

**Task 11.8: Camera Polish Pass** *(30 minutes)*

**Final tuning session:**

Fly through your test course and fine-tune:

**Checklist:**
- [ ] Camera never clips through geometry
- [ ] Camera smoothly follows during sharp turns
- [ ] FOV change feels good (not nauseating)
- [ ] Look-ahead makes movement feel intentional
- [ ] Camera shake on Quick Boost is noticeable but not distracting
- [ ] Drift state has distinct camera feel

**Document final values in TuningValues.md:**

```markdown
## Camera (Cinemachine) - Week 3
- Base FOV: 60
- Max Speed FOV: 75
- Drift FOV: 70
- Camera Distance: 12
- Camera Height: 3
- Damping X/Y/Z: 0.5 / 0.3 / 0.5
- Look Ahead Max Distance: 15
- Impulse Amplitude: 0.5
```

---

**End of Day 11 Commit:**
```
git add .
git commit -m "Day 11: Cinemachine camera system with dynamic FOV and look-ahead"
```

---

## **📅 Day 12: Visual Feedback (VFX) (8 hours)**

**Daily Goal:** Add particle effects for all major mechanics. Every action should have a visual signature.

---

### **Morning Session (4 hours): Thruster & Boost VFX**

**Task 12.1: Create Thruster Trail System** *(2 hours)*

**Setup thruster emitter locations:**

1. Create empty GameObjects as children of `Player`:
   - `ThrusterLeft` - Position: (-0.5, -0.3, -0.5)
   - `ThrusterRight` - Position: (0.5, -0.3, -0.5)
   - Both represent engine exhaust points

2. Add Particle System to `ThrusterLeft`:
   - `Add Component > Particle System`

**Configure Base Settings:**

```
Emission:
  Rate over Time: 50

Shape:
  Shape: Cone
  Angle: 10
  Radius: 0.1
  Emit from: Base

Velocity over Lifetime:
  Linear: (0, 0, -10) [particles shoot backward]

Color over Lifetime:
  Gradient:
    Start: Bright cyan (0, 255, 255, 255)
    Mid (50%): Light blue (100, 200, 255, 200)
    End: Transparent white (255, 255, 255, 0)

Size over Lifetime:
  Curve: Start at 0.2, end at 0.05 (shrinks)

Start Lifetime: 0.5 seconds
Start Speed: 5
Start Size: 0.3
Max Particles: 100

Renderer:
  Material: Default-Particle (white)
  Render Mode: Billboard
```

3. **Duplicate** `ThrusterLeft` → `ThrusterRight`

**Test:** Press Play. You should see gentle blue particle trails from the back of the cube.

---

**Task 12.2: Dynamic Thruster Intensity** *(1.5 hours)*

Thrusters should be brighter/faster when boosting.

Create: `Assets/_Project/Scripts/VFX/ThrusterController.cs`

```csharp
using UnityEngine;

public class ThrusterController : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftThruster;
    [SerializeField] private ParticleSystem rightThruster;
    [SerializeField] private PlayerController playerController;
    
    [Header("Emission Settings")]
    [SerializeField] private float idleEmissionRate = 50f;
    [SerializeField] private float boostEmissionRate = 200f;
    [SerializeField] private float driftEmissionRate = 100f;
    
    [Header("Color Settings")]
    [SerializeField] private Color idleColor = new Color(0, 1, 1, 1); // Cyan
    [SerializeField] private Color boostColor = new Color(0, 0.5f, 1, 1); // Bright blue
    [SerializeField] private Color driftColor = new Color(1, 0.8f, 0, 1); // Orange
    
    private ParticleSystem.EmissionModule leftEmission;
    private ParticleSystem.EmissionModule rightEmission;
    private ParticleSystem.MainModule leftMain;
    private ParticleSystem.MainModule rightMain;
    
    private void Start()
    {
        leftEmission = leftThruster.emission;
        rightEmission = rightThruster.emission;
        leftMain = leftThruster.main;
        rightMain = rightThruster.main;
    }
    
    private void Update()
    {
        UpdateThrusterState();
    }
    
    private void UpdateThrusterState()
    {
        float targetEmissionRate;
        Color targetColor;
        
        // Determine state
        if (playerController.IsDrifting)
        {
            targetEmissionRate = driftEmissionRate;
            targetColor = driftColor;
        }
        else if (playerController.IsBoosting) // You'll need to add this property
        {
            targetEmissionRate = boostEmissionRate;
            targetColor = boostColor;
        }
        else
        {
            targetEmissionRate = idleEmissionRate;
            targetColor = idleColor;
        }
        
        // Apply to both thrusters
        leftEmission.rateOverTime = targetEmissionRate;
        rightEmission.rateOverTime = targetEmissionRate;
        
        leftMain.startColor = targetColor;
        rightMain.startColor = targetColor;
    }
}
```

**Add to PlayerController:**
```csharp
public bool IsBoosting => isPrimaryBoosting;
```

**Attach script to Player, assign references.**

**Test:**
- Idle: Gentle cyan trail
- Hold boost: Intense blue trail
- Enter drift: Orange trail (visual state change!)

---

**Task 12.3: Quick Boost Flash Effect** *(30 minutes)*

**Create a one-shot burst effect for Quick Boost:**

1. Create new Particle System: `QuickBoostFlash` (child of Player)
2. Position at: (0, 0, 0) (player center)
3. Configure:

```
Duration: 0.3
Looping: OFF

Emission:
  Bursts: 1 burst of 20 particles at time 0

Shape:
  Shape: Sphere
  Radius: 1

Start Lifetime: 0.2
Start Speed: 10
Start Size: Random between 0.2 and 0.5

Color over Lifetime:
  White to transparent

Size over Lifetime:
  0.5 → 0.1 (shrinks)

Renderer:
  Material: Default-Particle
```

4. **Disable the GameObject** (we'll trigger it via script)

**Update PlayerController:**

```csharp
[SerializeField] private ParticleSystem quickBoostVFX;

private void ExecuteQuickBoost()
{
    // ... existing code ...
    
    // Visual feedback
    if (quickBoostVFX != null)
    {
        quickBoostVFX.Play();
    }
    
    // Camera shake
    if (impulseSource != null)
    {
        impulseSource.GenerateImpulse(Vector3.one * 0.5f);
    }
}
```

**Test:** Quick Boost should trigger a bright flash burst.

---

### **Afternoon Session (4 hours): Speed Lines & Environmental VFX**

**Task 12.4: Speed Lines Effect** *(2 hours)*

**Create the "going fast" visual:**

1. Create new Particle System: `SpeedLines` (child of Main Camera)
2. Position: (0, 0, 5) - in front of camera
3. Configure:

```
Duration: 5 (continuous)
Looping: ON

Emission:
  Rate over Time: 0 (we'll control this)

Shape:
  Shape: Sphere
  Radius: 8
  Emit from: Volume

Start Lifetime: 0.5
Start Speed: 0
Start Size: Random between 0.05 and 0.1

Velocity over Lifetime:
  Linear Z: -50 (particles fly toward camera)

Color over Lifetime:
  Start: Transparent (0, 0, 0, 0)
  Mid (30%): White (255, 255, 255, 100)
  End: Transparent (0, 0, 0, 0)

Size over Lifetime:
  Curve: 0.5 → 0.1 → 0.5 (diamond shape)

Renderer:
  Material: Default-Particle
  Render Mode: Stretched Billboard
    - Speed Scale: 0.5
    - Length Scale: 3 (long streaks)
```

4. Create controller script:

`Assets/_Project/Scripts/VFX/SpeedLinesController.cs`:

```csharp
using UnityEngine;

public class SpeedLinesController : MonoBehaviour
{
    [SerializeField] private ParticleSystem speedLines;
    [SerializeField] private Rigidbody playerRb;
    
    [SerializeField] private float minSpeedThreshold = 60f;
    [SerializeField] private float maxSpeedThreshold = 120f;
    [SerializeField] private float maxEmissionRate = 100f;
    
    private ParticleSystem.EmissionModule emission;
    
    private void Start()
    {
        emission = speedLines.emission;
    }
    
    private void Update()
    {
        float speed = playerRb.velocity.magnitude;
        
        if (speed < minSpeedThreshold)
        {
            emission.rateOverTime = 0;
        }
        else
        {
            float speedFactor = Mathf.InverseLerp(minSpeedThreshold, maxSpeedThreshold, speed);
            emission.rateOverTime = speedFactor * maxEmissionRate;
        }
    }
}
```

**Attach to SpeedLines GameObject, assign Player's Rigidbody.**

**Test:** Boost to high speed - white streaks should appear flying past you.

---

**Task 12.5: Drift Trail Effect** *(1.5 hours)*

**Create visible momentum indicator:**

1. Create new GameObject: `DriftTrail` (child of Player)
2. Add `Trail Renderer` component
3. Configure:

```
Time: 1.0 (trail lasts 1 second)
Width Curve: 
  Start: 0.5
  End: 0.1

Color Gradient:
  Start: Yellow (255, 200, 0, 255)
  End: Orange transparent (255, 100, 0, 0)

Materials:
  Element 0: Default-Particle

Shadow Casting: Off
Alignment: View
```

4. Position at: (0, 0, 0)

**Control script:**

`Assets/_Project/Scripts/VFX/DriftTrailController.cs`:

```csharp
using UnityEngine;

public class DriftTrailController : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private PlayerController playerController;
    
    private void Update()
    {
        // Only emit trail when drifting
        trailRenderer.emitting = playerController.IsDrifting;
    }
}
```

**Attach and assign references.**

**Test:** Enter drift - orange trail should appear behind you showing your path.

---

**Task 12.6: VFX Performance Check** *(30 minutes)*

**Open Profiler:** `Window > Analysis > Profiler`

Record 60 seconds with all VFX active.

**Check:**

| Metric | Target | Current | Status |
|--------|--------|---------|--------|
| Rendering | <16ms | [ ] | [ ] |
| Particle Count | <500 | [ ] | [ ] |
| Draw Calls | <50 | [ ] | [ ] |

**If particle count is high:**
- Reduce Max Particles in each system
- Lower emission rates
- Reduce lifetime

**If draw calls are high:**
- Ensure all particles use the same material (batching)
- Use GPU Instancing if available

**Target:** 60fps with all VFX active.

---

**End of Day 12 Commit:**
```
git add .
git commit -m "Day 12: VFX system - thrusters, speed lines, drift trails"
```

---

## **📅 Day 13: Audio System (8 hours)**

**Daily Goal:** Replace placeholder sounds with a proper layered audio system. Audio should provide constant feedback about game state.

---

### **Morning Session (4 hours): Core Audio Implementation**

**Task 13.1: Acquire Audio Assets** *(1 hour)*

**Free Sound Resources:**

1. **Freesound.org** (requires free account)
   - Search terms: "thruster", "boost", "whoosh", "engine", "mechanical"
   - Download 5-10 sounds for each category
   
2. **Sonniss Game Audio GDC Bundles** (free, huge)
   - Link: sonniss.com/gameaudiogdc
   - Download latest bundle
   - Extract sci-fi/mechanical sounds

**Sounds You Need:**

| Sound Type | Description | Reference Search |
|------------|-------------|------------------|
| Thruster Loop | Low rumble, loopable | "engine loop" |
| Boost Whoosh | Sharp burst | "boost" "dash" |
| Quick Boost | Explosive pop | "explosion small" |
| Drift Engage | Mechanical click | "mechanical" |
| Drift Loop | Wind/air sound | "wind loop" |
| Low Boost Warning | Beep/alert | "warning beep" |

**Import to Unity:**
- Create folders: `Assets/_Project/Audio/SFX/Movement/`
- Drag all sounds into Unity
- Select all → Inspector:
  - Load Type: **Streaming** (for music/long loops)
  - Load Type: **Decompress on Load** (for short SFX)
  - Compression Format: **Vorbis** (quality: 70%)

---

**Task 13.2: Audio Manager Setup** *(1.5 hours)*

**Create centralized audio system:**

`Assets/_Project/Scripts/Audio/PlayerAudioManager.cs`:

```csharp
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource thrusterSource;
    [SerializeField] private AudioSource boostSource;
    [SerializeField] private AudioSource oneShotSource;
    
    [Header("Thruster Sounds")]
    [SerializeField] private AudioClip thrusterIdle;
    [SerializeField] private float idlePitch = 1.0f;
    [SerializeField] private float boostPitch = 1.5f;
    [SerializeField] private float driftPitch = 1.2f;
    
    [Header("Boost Sounds")]
    [SerializeField] private AudioClip quickBoostSound;
    [SerializeField] private AudioClip driftEngageSound;
    [SerializeField] private AudioClip driftDisengageSound;
    
    [Header("Warning Sounds")]
    [SerializeField] private AudioClip lowBoostWarning;
    
    [Header("References")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Rigidbody playerRb;
    
    private bool wasLowBoost = false;
    
    private void Start()
    {
        SetupAudioSources();
        
        // Subscribe to player events
        if (playerController != null)
        {
            playerController.OnQuickBoostExecuted += PlayQuickBoostSound;
            playerController.OnBoostDepleted += PlayBoostDepletedWarning;
        }
    }
    
    private void OnDestroy()
    {
        if (playerController != null)
        {
            playerController.OnQuickBoostExecuted -= PlayQuickBoostSound;
            playerController.OnBoostDepleted -= PlayBoostDepletedWarning;
        }
    }
    
    private void SetupAudioSources()
    {
        // Thruster source (continuous)
        if (thrusterSource != null && thrusterIdle != null)
        {
            thrusterSource.clip = thrusterIdle;
            thrusterSource.loop = true;
            thrusterSource.volume = 0.6f;
            thrusterSource.Play();
        }
    }
    
    private void Update()
    {
        UpdateThrusterPitch();
        UpdateBoostState();
    }
    
    private void UpdateThrusterPitch()
    {
        if (thrusterSource == null) return;
        
        float targetPitch;
        float targetVolume;
        
        if (playerController.IsDrifting)
        {
            targetPitch = driftPitch;
            targetVolume = 0.5f;
        }
        else if (playerController.IsBoosting)
        {
            targetPitch = boostPitch;
            targetVolume = 0.8f;
        }
        else
        {
            // Pitch based on speed
            float speedFactor = Mathf.Clamp01(playerRb.velocity.magnitude / 100f);
            targetPitch = Mathf.Lerp(idlePitch, boostPitch, speedFactor);
            targetVolume = Mathf.Lerp(0.4f, 0.7f, speedFactor);
        }
        
        thrusterSource.pitch = Mathf.Lerp(thrusterSource.pitch, targetPitch, Time.deltaTime * 3f);
        thrusterSource.volume = Mathf.Lerp(thrusterSource.volume, targetVolume, Time.deltaTime * 5f);
    }
    
    private void UpdateBoostState()
    {
        // Play warning when boost is low
        float boostPercent = playerController.CurrentBoost / playerController.MaxBoost;
        
        if (boostPercent < 0.2f && !wasLowBoost)
        {
            wasLowBoost = true;
            if (lowBoostWarning != null)
            {
                oneShotSource.PlayOneShot(lowBoostWarning, 0.5f);
            }
        }
        else if (boostPercent > 0.3f)
        {
            wasLowBoost = false;
        }
    }
    
    private void PlayQuickBoostSound()
    {
        if (quickBoostSound != null)
        {
            oneShotSource.PlayOneShot(quickBoostSound, 0.8f);
        }
    }
    
    private void PlayBoostDepletedWarning()
    {
        if (lowBoostWarning != null)
        {
            oneShotSource.PlayOneShot(lowBoostWarning, 1.0f);
        }
    }
}
```

**Add to PlayerController (expose boost values):**

```csharp
public float CurrentBoost => currentBoost;
public float MaxBoost => maxBoost;
```

---

**Task 13.3: Setup Audio Sources on Player** *(1 hour)*

1. Add 3 `AudioSource` components to Player GameObject:

**ThrusterSource:**
```
Spatial Blend: 1.0 (3D)
Volume: 0.6
Doppler Level: 0.5
Min Distance: 5
Max Distance: 50
```

**BoostSource:**
```
Spatial Blend: 1.0 (3D)
Volume: 0.7
Doppler Level: 0.3
```

**OneShotSource:**
```
Spatial Blend: 0.5 (half 3D, half 2D - always audible)
Volume: 0.8
```

2. Add `PlayerAudioManager` component
3. Assign all references in Inspector

**Test:**
- Idle: Low rumble
- Boost: Pitch increases
- Drift: Distinct pitch change
- Quick Boost: Sharp sound plays

---

**Task 13.4: Drift Audio State** *(30 minutes)*

**Add drift engage/disengage sounds:**

Update `PlayerController.cs`:

```csharp
// Add event
public event Action OnDriftEnter;
public event Action OnDriftExit;

private void TryEnableDrift()
{
    if (rb.velocity.magnitude >= minimumDriftSpeed && !isDrifting)
    {
        isDrifting = true;
        OnDriftEnter?.Invoke();
    }
}

private void DisableDrift()
{
    if (isDrifting)
    {
        isDrifting = false;
        OnDriftExit?.Invoke();
    }
}
```

**In PlayerAudioManager:**

```csharp
private void Start()
{
    SetupAudioSources();
    
    if (playerController != null)
    {
        playerController.OnQuickBoostExecuted += PlayQuickBoostSound;
        playerController.OnBoostDepleted += PlayBoostDepletedWarning;
        playerController.OnDriftEnter += PlayDriftEngage;
        playerController.OnDriftExit += PlayDriftDisengage;
    }
}

private void PlayDriftEngage()
{
    if (driftEngageSound != null)
    {
        oneShotSource.PlayOneShot(driftEngageSound, 0.6f);
    }
}

private void PlayDriftDisengage()
{
    if (driftDisengageSound != null)
    {
        oneShotSource.PlayOneShot(driftDisengageSound, 0.4f);
    }
}
```

---

### **Afternoon Session (4 hours): Audio Polish & Mixing**

**Task 13.5: Audio Mixer Setup** *(1.5 hours)*

**Create proper audio mixing:**

1. `Assets > Create > Audio Mixer`
2. Name: `MainMixer`
3. In Mixer window (`Window > Audio > Audio Mixer`):

**Create groups:**
```
Master
├── SFX
│   ├── Movement
│   ├── Weapon (for later)
│   └── UI (for later)
└── Music (for later)
```

4. Assign audio sources:
   - ThrusterSource → Movement
   - BoostSource → Movement
   - OneShotSource → Movement

**Add effects to Movement group:**
- Add: Lowpass Simple (cutoff: 22000 Hz)
- Add: Compressor (threshold: -10 dB)

**Create exposed parameters:**
- Right-click Master volume → Expose 'Volume (of Master)' to script
- Rename to "MasterVolume"
- Do same for SFX volume → "SFXVolume"

---

**Task 13.6: Doppler Effect Tuning** *(1 hour)*

**The Doppler effect makes fast movement sound dynamic.**

**Test scenario:**
1. Boost to max speed
2. Quick Boost past an obstacle
3. Listen for pitch shift

**If Doppler is too strong** (sounds weird):
- Reduce Doppler Level to 0.2-0.3 on all sources

**If Doppler is not noticeable:**
- Increase to 0.8-1.0

**Recommended:** Keep it subtle (0.3-0.5)

---

**Task 13.7: Audio Ducking System** *(1 hour)*

**Goal:** Lower certain sounds when others are playing (priority system).

Update `PlayerAudioManager.cs`:

```csharp
[Header("Ducking")]
[SerializeField] private float duckAmount = 0.5f;
[SerializeField] private float duckSpeed = 5f;

private float thrusterVolumeMultiplier = 1f;

private void Update()
{
    UpdateThrusterPitch();
    UpdateBoostState();
    UpdateDucking();
}

private void UpdateDucking()
{
    // Duck thruster when Quick Boost is playing
    bool shouldDuck = oneShotSource.isPlaying;
    
    float targetMultiplier = shouldDuck ? duckAmount : 1f;
    thrusterVolumeMultiplier = Mathf.Lerp(
        thrusterVolumeMultiplier,
        targetMultiplier,
        duckSpeed * Time.deltaTime
    );
    
    // Apply in UpdateThrusterPitch
}

private void UpdateThrusterPitch()
{
    // ... existing code ...
    
    // Apply ducking multiplier
    thrusterSource.volume = Mathf.Lerp(
        thrusterSource.volume,
        targetVolume * thrusterVolumeMultiplier,
        Time.deltaTime * 5f
    );
}
```

**Test:** Quick Boost sound should be clear over thruster rumble.

---

**Task 13.8: Collision Audio** *(30 minutes)*

**Add impact sounds when hitting walls:**

```csharp
// In PlayerAudioManager
[Header("Collision")]
[SerializeField] private AudioClip[] impactSounds;
[SerializeField] private float minImpactVelocity = 20f;

private void OnCollisionEnter(Collision collision)
{
    if (playerRb.velocity.magnitude > minImpactVelocity)
    {
        // Play random impact sound
        if (impactSounds.Length > 0)
        {
            AudioClip randomImpact = impactSounds[Random.Range(0, impactSounds.Length)];
            oneShotSource.PlayOneShot(randomImpact, 0.7f);
        }
    }
}
```

**Find 3-5 impact sounds from your library, add to array.**

---

**End of Day 13 Commit:**
```
git add .
git commit -m "Day 13: Complete audio system with layered sounds and ducking"
```

---

## **📅 Day 14: Screen Effects & Haptics (8 hours)**

**Daily Goal:** Add post-processing effects, controller rumble, and other tactile feedback.

---

### **Morning Session (4 hours): Post-Processing**

**Task 14.1: Install Post-Processing** *(30 minutes)*

Post-processing is already included in URP.

1. Select Main Camera
2. Add Component: `Rendering > Volume`
3. Check "Is Global"
4. Create new profile: `Assets/_Project/PostProcessing/GlobalVolume`

**In Project window:**
- Right-click profile → Add Override → Select:
  - **Bloom**
  - **Chromatic Aberration**
  - **Vignette**
  - **Motion Blur**
  - **Color Adjustments**

---

**Task 14.2: Configure Base Post-Processing** *(1 hour)*

**Bloom (makes glowing things glow):**
```
Intensity: 0.3
Threshold: 1.0
Scatter: 0.7
Tint: White
High Quality Filtering: On
```

**Vignette (darkens edges):**
```
Color: Black
Intensity: 0.25
Smoothness: 0.4
```

**Color Adjustments:**
```
Post Exposure: 0.2 (slightly brighter)
Contrast: 5
Saturation: 10
```

**Chromatic Aberration: Disabled** (can cause motion sickness)

**Motion Blur:**
```
Quality: Medium
Intensity: 0.0 (we'll control this dynamically)
```

**Test:** Scene should now have subtle cinematic grade.

---

**Task 14.3: Speed-Based Motion Blur** *(1.5 hours)*

**Create dynamic motion blur controller:**

`Assets/_Project/Scripts/VFX/DynamicPostProcessing.cs`:

```csharp
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DynamicPostProcessing : MonoBehaviour
{
    [SerializeField] private Volume postProcessVolume;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private PlayerController playerController;
    
    [Header("Motion Blur Settings")]
    [SerializeField] private float minSpeedForBlur = 60f;
    [SerializeField] private float maxSpeedForBlur = 120f;
    [SerializeField] private float maxBlurIntensity = 0.3f;
    [SerializeField] private float blurTransitionSpeed = 3f;
    
    [Header("Boost Flash")]
    [SerializeField] private float boostFlashIntensity = 0.5f;
    [SerializeField] private float flashDuration = 0.1f;
    
    private MotionBlur motionBlur;
    private Bloom bloom;
    private Vignette vignette;
    
    private float currentBlurIntensity = 0f;
    private float flashTimer = 0f;
    
    private void Start()
    {
        if (postProcessVolume.profile.TryGet(out motionBlur))
        {
            motionBlur.active = true;
        }
        
        postProcessVolume.profile.TryGet(out bloom);
        postProcessVolume.profile.TryGet(out vignette);
        
        // Subscribe to boost event
        if (playerController != null)
        {
            playerController.OnQuickBoostExecuted += TriggerBoostFlash;
        }
    }
    
    private void Update()
    {
        UpdateMotionBlur();
        UpdateBoostFlash();
    }
    
    private void UpdateMotionBlur()
    {
        if (motionBlur == null) return;
        
        float speed = playerRb.velocity.magnitude;
        
        float targetBlur = 0f;
        if (speed > minSpeedForBlur)
        {
            float speedFactor = Mathf.InverseLerp(minSpeedForBlur, maxSpeedForBlur, speed);
            targetBlur = speedFactor * maxBlurIntensity;
        }
        
        currentBlurIntensity = Mathf.Lerp(
            currentBlurIntensity,
            targetBlur,
            blurTransitionSpeed * Time.deltaTime
        );
        
        motionBlur.intensity.value = currentBlurIntensity;
    }
    
    private void TriggerBoostFlash()
    {
        flashTimer = flashDuration;
    }
    
    private void UpdateBoostFlash()
    {
        if (flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;
            
            // Pulse bloom intensity
            if (bloom != null)
            {
                float flashProgress = flashTimer / flashDuration;
                bloom.intensity.value = 0.3f + (flashProgress * boostFlashIntensity);
            }
            
            // Pulse vignette
            if (vignette != null)
            {
                float flashProgress = flashTimer / flashDuration;
                vignette.intensity.value = 0.25f + (flashProgress * 0.2f);
            }
        }
        else
        {
            // Return to normal
            if (bloom != null)
                bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 0.3f, Time.deltaTime * 5f);
            
            if (vignette != null)
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0.25f, Time.deltaTime * 5f);
        }
    }
    
    private void OnDestroy()
    {
        if (playerController != null)
        {
            playerController.OnQuickBoostExecuted -= TriggerBoostFlash;
        }
    }
}
```

**Attach to Main Camera, assign references.**

**Test:**
- Boost to high speed → Motion blur increases
- Quick Boost → Screen flashes briefly

---

**Task 14.4: Color Grading States** *(1 hour)*

**Different color grades for different states:**

Update `DynamicPostProcessing.cs`:

```csharp
[Header("Drift Color Grade")]
[SerializeField] private Color driftColorFilter = new Color(1f, 0.9f, 0.7f); // Warm
private ColorAdjustments colorAdjustments;

private void Start()
{
    // ... existing code ...
    postProcessVolume.profile.TryGet(out colorAdjustments);
}

private void Update()
{
    UpdateMotionBlur();
    UpdateBoostFlash();
    UpdateColorGrade();
}

private void UpdateColorGrade()
{
    if (colorAdjustments == null) return;
    
    Color targetFilter = playerController.IsDrifting ? driftColorFilter : Color.white;
    
    colorAdjustments.colorFilter.value = Color.Lerp(
        colorAdjustments.colorFilter.value,
        targetFilter,
        Time.deltaTime * 3f
    );
}
```

**Test:** Enter drift → Screen should have a subtle warm tint.

---

### **Afternoon Session (4 hours): Haptic Feedback & Polish**

**Task 14.5: Controller Rumble System** *(2 hours)*

**Create haptic feedback manager:**

`Assets/_Project/Scripts/Input/HapticFeedbackManager.cs`:

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class HapticFeedbackManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    
    [Header("Quick Boost Rumble")]
    [SerializeField] private float quickBoostDuration = 0.2f;
    [SerializeField] private float quickBoostLowFreq = 0.3f;
    [SerializeField] private float quickBoostHighFreq = 0.8f;
    
    [Header("Constant Boost Rumble")]
    [SerializeField] private float boostLowFreq = 0.1f;
    [SerializeField] private float boostHighFreq = 0.2f;
    
    [Header("Drift Rumble")]
    [SerializeField] private float driftLowFreq = 0.15f;
    [SerializeField] private float driftHighFreq = 0.1f;
    
    [Header("Collision Rumble")]
    [SerializeField] private float collisionDuration = 0.3f;
    [SerializeField] private float collisionLowFreq = 0.8f;
    [SerializeField] private float collisionHighFreq = 0.8f;
    
    private Gamepad gamepad;
    private float rumbleTimer = 0f;
    
    private void Start()
    {
        gamepad = Gamepad.current;
        
        if (playerController != null)
        {
            playerController.OnQuickBoostExecuted += TriggerQuickBoostRumble;
            playerController.OnDriftEnter += StartDriftRumble;
            playerController.OnDriftExit += StopAllRumble;
        }
    }
    
    private void Update()
    {
        UpdateConstantRumble();
        UpdateRumbleTimer();
    }
    
    private void UpdateConstantRumble()
    {
        if (gamepad == null) return;
        
        // Constant low rumble when boosting
        if (playerController.IsBoosting && rumbleTimer <= 0)
        {
            gamepad.SetMotorSpeeds(boostLowFreq, boostHighFreq);
        }
        else if (!playerController.IsBoosting && !playerController.IsDrifting && rumbleTimer <= 0)
        {
            gamepad.SetMotorSpeeds(0, 0);
        }
    }
    
    private void UpdateRumbleTimer()
    {
        if (rumbleTimer > 0)
        {
            rumbleTimer -= Time.deltaTime;
            
            if (rumbleTimer <= 0)
            {
                StopAllRumble();
            }
        }
    }
    
    private void TriggerQuickBoostRumble()
    {
        if (gamepad == null) return;
        
        gamepad.SetMotorSpeeds(quickBoostLowFreq, quickBoostHighFreq);
        rumbleTimer = quickBoostDuration;
    }
    
    private void StartDriftRumble()
    {
        if (gamepad == null) return;
        
        gamepad.SetMotorSpeeds(driftLowFreq, driftHighFreq);
    }
    
    public void TriggerCollisionRumble(float intensity)
    {
        if (gamepad == null) return;
        
        gamepad.SetMotorSpeeds(
            collisionLowFreq * intensity,
            collisionHighFreq * intensity
        );
        rumbleTimer = collisionDuration;
    }
    
    private void StopAllRumble()
    {
        if (gamepad == null) return;
        
        gamepad.SetMotorSpeeds(0, 0);
        rumbleTimer = 0;
    }
    
    private void OnDestroy()
    {
        StopAllRumble();
        
        if (playerController != null)
        {
            playerController.OnQuickBoostExecuted -= TriggerQuickBoostRumble;
            playerController.OnDriftEnter -= StartDriftRumble;
            playerController.OnDriftExit -= StopAllRumble;
        }
    }
    
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            StopAllRumble();
        }
    }
}
```

**Attach to Player, assign references.**

**Test with controller:**
- Boost: Gentle rumble
- Quick Boost: Sharp strong rumble
- Drift: Distinct rumble pattern

---

**Task 14.6: UI Boost Gauge** *(1.5 hours)*

**Finally add the minimal HUD:**

1. Create Canvas: `GameObject > UI > Canvas`
2. Set Canvas to: Screen Space - Overlay
3. Create UI Image (child of Canvas): `BoostGaugeBackground`
   - Position: Bottom center (X: 0, Y: 80)
   - Size: 300 × 30
   - Color: Dark gray (40, 40, 40, 200)

4. Create UI Image (child of Background): `BoostGaugeFill`
   - Anchor: Stretch left
   - Position: Fill parent
   - Color: Cyan (0, 255, 255, 255)
   - Image Type: Filled
   - Fill Method: Horizontal

**Create controller script:**

`Assets/_Project/Scripts/UI/BoostGaugeUI.cs`:

```csharp
using UnityEngine;
using UnityEngine.UI;

public class BoostGaugeUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private PlayerController playerController;
    
    [Header("Colors")]
    [SerializeField] private Color fullColor = Color.cyan;
    [SerializeField] private Color lowColor = Color.yellow;
    [SerializeField] private Color emptyColor = Color.red;
    
    [SerializeField] private float lowThreshold = 0.3f;
    [SerializeField] private float emptyThreshold = 0.1f;
    
    private void Update()
    {
        float boostPercent = playerController.CurrentBoost / playerController.MaxBoost;
        
        fillImage.fillAmount = boostPercent;
        
        // Color based on level
        if (boostPercent < emptyThreshold)
        {
            fillImage.color = emptyColor;
        }
        else if (boostPercent < lowThreshold)
        {
            fillImage.color = lowColor;
        }
        else
        {
            fillImage.color = fullColor;
        }
    }
}
```

**Attach to BoostGaugeBackground, assign references.**

**Test:** Gauge should drain when boosting, change color when low.

---

**Task 14.7: Final Polish Pass** *(30 minutes)*

**Go through the entire experience and check:**

**Visual Checklist:**
- [ ] Thrusters change color based on state
- [ ] Speed lines appear at high velocity
- [ ] Drift trail is visible and distinct
- [ ] Quick Boost has visible flash
- [ ] Post-processing enhances (not distracts)
- [ ] UI is readable

**Audio Checklist:**
- [ ] Thruster sound pitch shifts with speed
- [ ] Quick Boost sound is punchy
- [ ] Drift engage/disengage is audible
- [ ] Low boost warning is clear
- [ ] No audio clipping or distortion

**Haptic Checklist:**
- [ ] Controller rumbles on Quick Boost
- [ ] Rumble stops when it should
- [ ] Different states have different rumble

---

**End of Day 14 Commit:**
```
git add .
git commit -m "Day 14: Post-processing, haptic feedback, UI boost gauge complete"
```

---

## **📅 Day 15: Week 3 Review & Final Polish (8 hours)**

**Daily Goal:** Polish everything to a shippable "gray box" standard. This should feel like a real game, even if it looks like primitives.

---

### **Morning Session (4 hours): Comprehensive Testing**

**Task 15.1: Full Playtest Session** *(2 hours)*

**Extended Playtest Protocol:**

1. Set a 30-minute timer
2. Play through all test courses continuously
3. Try every mechanic combination
4. **Record yourself** (use OBS or Windows Game Bar)

**During playtest, note:**
- Any moments where controls feel unresponsive
- Any visual effects that are distracting
- Any audio that's too loud/quiet
- Any bugs or glitches

**After 30 minutes, rate:**

```
FEEL (1-10):
Movement responsiveness: [ /10]
Quick Boost satisfaction: [ /10]
Drift intuitiveness: [ /10]
Camera quality: [ /10]
Overall "juice": [ /10]

FEEDBACK (1-10):
Visual clarity: [ /10]
Audio feedback: [ /10]
Haptic feedback: [ /10]
UI readability: [ /10]

POLISH (1-10):
No bugs encountered: [ /10]
Performance (60fps): [ /10]
Professional feel: [ /10]

Total: [ /120]

Target: 90+/120 = Ready for Week 4
```

---

**Task 15.2: Bug Fixing Session** *(2 hours)*

**Fix all bugs discovered during playtest.**

**Common issues at this stage:**
- Audio sources not stopping correctly
- VFX particles piling up (memory leak)
- Camera clipping through geometry
- Input not responding after collision
- Boost gauge not updating
- Post-processing causing performance drop

**Document fixes in:** `BugLog_Week3.md`

---

### **Afternoon Session (4 hours): External Validation & Documentation**

**Task 15.3: External Playtesting** *(2 hours if available)*

**Find someone who hasn't played it before.**

**Hand them the controller with ZERO instructions.**

**Watch for:**
- How long until they figure out basic movement? (Target: <30 seconds)
- Do they discover Quick Boost? (Target: <2 minutes)
- Do they activate Drift? (Target: <5 minutes)
- Do they understand what Drift does? (May need explanation)

**After 10 minutes of exploration, give them the Controls Guide.**

**Let them play for 20 more minutes.**

**Interview questions:**

```
1. What felt the best about the controls?
2. What was confusing?
3. Did the camera ever bother you?
4. Could you tell when you were low on boost?
5. On a scale of 1-10, how fun was just flying around?
6. Would you want to see this as a full game?
7. Any suggestions for improvement?
```

**Document in:** `PlaytestFeedback_Week3.md`

---

**Task 15.4: Create Week 3 Showcase Video** *(1 hour)*

**Record a 2-minute video showing off the prototype:**

**Shot list:**
1. (0:00-0:20) Basic movement showcase
2. (0:20-0:40) Quick Boost demonstrations
3. (0:40-1:00) Drift maneuvers
4. (1:00-1:20) Advanced technique: drift-turn combo
5. (1:20-1:40) Speed run through test course
6. (1:40-2:00) Slow-mo highlight moment

**Editing (optional):**
- Add text overlays explaining mechanics
- Background music (royalty-free)
- Upload to YouTube (Unlisted) for portfolio

**Why:** This captures your progress and can be used for marketing/devlogs later.

---

**Task 15.5: Update All Documentation** *(1 hour)*

**Update these files:**

**TuningValues.md:**
```markdown
# Week 3 Final Values

## All Week 2 values +

## Camera (Cinemachine)
- Base FOV: 60
- Speed FOV: 75
- Drift FOV: 70
- Camera Distance: 12
- Look Ahead: 15m

## VFX
- Thruster Emission (Idle): 50
- Thruster Emission (Boost): 200
- Thruster Emission (Drift): 100
- Speed Lines Max Emission: 100
- Drift Trail Duration: 1.0s

## Audio
- Thruster Pitch (Idle): 1.0
- Thruster Pitch (Boost): 1.5
- Thruster Pitch (Drift): 1.2
- Thruster Volume (Idle): 0.6
- Thruster Volume (Boost): 0.8

## Post-Processing
- Bloom Intensity: 0.3
- Vignette Intensity: 0.25
- Motion Blur Max: 0.3
- Motion Blur Min Speed: 60 m/s

## Haptics
- Quick Boost Duration: 0.2s
- Quick Boost Strength: 0.8
- Drift Rumble: 0.15
```

**ControlsGuide.md:**
- Add notes about visual/audio feedback for each mechanic

**MovementTechniques.md:**
- Add any new techniques discovered this week

**README.md** (create if doesn't exist):
```markdown
# CRIMSON COMET - Phase 1 Prototype

## Current Status: Week 3 Complete

### Implemented Features:
- ✅ 6DoF flight controls
- ✅ Quick Boost (evasive dash)
- ✅ High-G Drift (momentum preservation)
- ✅ Dynamic Cinemachine camera
- ✅ Full VFX suite (thrusters, trails, speed lines)
- ✅ Layered audio system
- ✅ Post-processing effects
- ✅ Haptic feedback
- ✅ Boost gauge UI

### Performance:
- Target: 60fps on mid-range hardware
- Current: Stable 60fps in test scenes
- Particle count: <500
- Draw calls: <50

### Next Steps:
- Week 4: Arena environment and final polish

### How to Play:
See ControlsGuide.md
```

---

## **🎯 Week 3 Deliverables Checklist**

Before Week 4, verify:

**Code:**
- [ ] `DynamicLookTarget.cs` - Camera look-ahead and FOV
- [ ] `ThrusterController.cs` - Dynamic thruster VFX
- [ ] `SpeedLinesController.cs` - Speed-based particles
- [ ] `DriftTrailController.cs` - Drift momentum trail
- [ ] `PlayerAudioManager.cs` - Layered audio system
- [ ] `DynamicPostProcessing.cs` - Screen effects
- [ ] `HapticFeedbackManager.cs` - Controller rumble
- [ ] `BoostGaugeUI.cs` - HUD element

**Assets:**
- [ ] Post-processing profile configured
- [ ] Audio mixer with groups
- [ ] 10+ sound effects imported
- [ ] All particle systems optimized
- [ ] UI Canvas with boost gauge

**Scenes:**
- [ ] All test scenes updated with Week 3 features
- [ ] Performance verified in all scenes

**Documentation:**
- [ ] TuningValues.md updated
- [ ] PlaytestFeedback_Week3.md created
- [ ] BugLog_Week3.md created
- [ ] README.md created/updated

**Media:**
- [ ] 2-minute showcase video recorded
- [ ] Screenshots of all effects

**Git:**
- [ ] All work committed
- [ ] Tagged "Phase1-Week3-Complete"

---

## **📊 Week 3 Success Criteria Review**

**Functional:**
- [ ] Cinemachine camera tracks player smoothly
- [ ] All mechanics have visual feedback (VFX)
- [ ] All mechanics have audio feedback (SFX)
- [ ] Post-processing enhances experience
- [ ] Haptic feedback works on gamepad
- [ ] UI boost gauge updates correctly

**Quality:**
- [ ] Camera never clips through geometry
- [ ] VFX don't obscure gameplay
- [ ] Audio is balanced (no clipping)
- [ ] Performance is stable 60fps
- [ ] Controls still feel responsive with all effects

**Polish:**
- [ ] Every action has satisfying feedback
- [ ] Game feels "juicy" and responsive
- [ ] Professional presentation quality
- [ ] External playtesters are impressed

**Critical Question:**
- [ ] **Does this feel like a real game, even though it looks like cubes?**

**If all boxes checked:** ✅ **GREEN LIGHT for Week 4**

---

## **🚀 Week 4 Preview**

**What's Coming:**

**Week 4 will focus on:**
1. **Environmental Polish** - Make the gray boxes more interesting (still primitives, but composed)
2. **Obstacle Course Design** - Build the ultimate test arena
3. **Performance Optimization** - Ensure rock-solid 60fps
4. **Juice Refinement** - Micro-polish on all feedback
5. **Final Playtest & Demo Build** - Package for presentation

**Goal:** Create a portfolio-quality prototype that proves the core concept is amazing.

---

**End of Week 3! 🎉**

You now have a prototype that **feels incredible**. The movement is solid, the feedback is layered, and it looks/sounds/feels like a real game.

**This is the hardest work done.** Week 4 is refinement and presentation.

---

**Final Week 3 Commit:**
```
git add .
git commit -m "Week 3 Complete: Full feedback system with camera, VFX, audio, post-processing, and haptics"
git tag "Phase1-Week3-Complete"
git push origin main --tags
```

**Create archival build:**
`File > Build Settings > Build`
Save as: `Builds/Phase1_Week3_Prototype.exe`

---

# **CRIMSON COMET: Week 4 Development Roadmap**

## **🗓️ Week 4: The Playground - Environment and Final Polish**

**Weekly Goal:** Transform the functional prototype into a polished, portfolio-quality demo. Build the ultimate test arena that showcases all mechanics. Optimize performance to rock-solid 60fps. Prepare for external presentation and Phase 2 transition.

**Philosophy:** "The last 10% takes 90% of the effort." This week is about making everything feel *perfect*. Every small detail matters. This is what separates amateur prototypes from professional demos.

**Core Focus Areas:**
1. **Environment Design** - Interesting playground without final art
2. **Performance Optimization** - Bulletproof 60fps
3. **Micro-Polish** - The 100 tiny things that add up
4. **Final Tuning** - Lock in perfect values
5. **Demo Preparation** - Package for presentation
6. **Phase 1 Review** - Retrospective and lessons learned

**Estimated Duration:** 5 Days (40 hours)

**Success Criteria:**
- ✅ Complete test arena that's fun to explore for 30+ minutes
- ✅ Stable 60fps on minimum spec hardware
- ✅ Zero known bugs or crashes
- ✅ External playtester can jump in and have fun immediately
- ✅ "I want to play more" reaction from testers
- ✅ Professional demo build ready for portfolio

---

## **📅 Day 16: The Ultimate Arena (8 hours)**

**Daily Goal:** Build a comprehensive test environment that showcases all movement mechanics through interesting level design. Still using primitives, but composed in compelling ways.

---

### **Morning Session (4 hours): Arena Layout Design**

**Task 16.1: Arena Concept & Planning** *(45 minutes)*

**Create design document for the arena:**

`Assets/_Project/ArenaDesign.md`:

```markdown
# Phase 1 Final Arena - "The Crucible"

## Design Pillars:
1. **Teach** - Naturally guides player through all mechanics
2. **Test** - Challenges mastery of each mechanic
3. **Flow** - Creates opportunities for stylish combos
4. **Explore** - Rewards curiosity with shortcuts and secrets

## Arena Zones (6 total):

### Zone 1: The Gates (Tutorial)
- **Purpose:** Gentle introduction, basic movement
- **Mechanics:** Forward flight, strafing, vertical movement
- **Layout:** Wide open area with floating rings to fly through
- **Challenge:** None (safe space to get oriented)
- **Time:** 30 seconds

### Zone 2: The Slalom (Quick Boost Practice)
- **Purpose:** Teach directional Quick Boost
- **Mechanics:** Quick Boost in all 8 directions
- **Layout:** Zigzag course with tight pillars
- **Challenge:** Can't complete at normal speed, must use Quick Boost
- **Time:** 45 seconds

### Zone 3: The Gauntlet (Speed Challenge)
- **Purpose:** Test sustained high-speed flight
- **Mechanics:** Primary Boost + boost management
- **Layout:** Long narrow tunnel with minor obstacles
- **Challenge:** Maintain max speed without hitting walls
- **Time:** 30 seconds

### Zone 4: The Drift Ring (Drift Mastery)
- **Purpose:** Teach circle-strafing and drift utility
- **Mechanics:** High-G Drift
- **Layout:** Circular arena with central target
- **Challenge:** Fly in circle while aiming at center
- **Time:** 60 seconds (practice area)

### Zone 5: The Maze (Precision Navigation)
- **Purpose:** Test all mechanics in tight quarters
- **Mechanics:** All movement systems
- **Layout:** Dense 3D maze with multiple paths
- **Challenge:** Navigate without collisions, find fastest route
- **Time:** 90 seconds

### Zone 6: The Proving Grounds (Final Test)
- **Purpose:** Free-form skill expression
- **Mechanics:** Everything combined
- **Layout:** Large open space with varied obstacles
- **Challenge:** Speedrun from start to finish
- **Time:** Variable (2-5 minutes)

## Total Arena Size: 500m x 500m x 200m (vertical)
## Estimated First Playthrough: 8-12 minutes
## Speedrun Target: 4-6 minutes
```

---

**Task 16.2: Create Modular Building System** *(1 hour 15 minutes)*

**Build reusable prefabs for quick level construction:**

Create folder: `Assets/_Project/Prefabs/Environment/`

**Prefab 1: Navigation Ring**
```
Create empty GameObject: "NavRing"
├── Ring (Torus primitive or cylinder bent - use 12 thin cubes arranged in circle)
│   └── Material: M_NavRing (Bright green, emissive)
└── Trigger (Sphere collider, Is Trigger = true)
```

Make it a prefab. This marks waypoints.

**Prefab 2: Obstacle Pillar**
```
GameObject: "Pillar_Large"
├── Base (Cube, scale: 2, 10, 2)
│   └── Material: M_Obstacle (Dark gray)
└── Cap (Cube, scale: 2.5, 1, 2.5)
    └── Material: M_Obstacle
```

Variations: Pillar_Medium, Pillar_Small

**Prefab 3: Wall Section**
```
GameObject: "Wall_10m"
└── Cube (scale: 10, 5, 1)
    └── Material: M_Wall (Medium gray)
```

Snap these together to build tunnels.

**Prefab 4: Checkpoint Gate**
```
GameObject: "Checkpoint"
├── LeftPost (Cube, scale: 1, 8, 1)
│   └── Material: M_Checkpoint (Cyan, emissive)
├── RightPost (Cube, scale: 1, 8, 1)
│   └── Material: M_Checkpoint
└── Trigger (Box Collider, Is Trigger = true, scale: 10, 8, 2)
```

**Create all prefabs and organize.**

---

**Task 16.3: Build Zone 1 - The Gates** *(1 hour)*

**Create new scene:** `Assets/_Project/Scenes/Phase1_FinalArena.unity`

**Setup:**

1. **Skybox:** 
   - `Window > Rendering > Lighting`
   - Skybox Material: Create new material with Skybox/Procedural shader
   - Set to space-like (very dark blue/black with stars)

2. **Lighting:**
   - Directional Light: Intensity 0.8, slight blue tint
   - Add second Directional Light (fill): Intensity 0.3, warm tint, opposite direction

3. **Ground Plane (optional):**
   - Large plane (100, 1, 100) far below (Y: -50)
   - Gives spatial reference

**Build The Gates:**

```
1. Create empty: "Zone1_TheGates"
2. Add spawn point: Empty GameObject "PlayerSpawn" at (0, 10, 0)
3. Create 5 NavRings in a line:
   - Ring 1: (0, 10, 20)
   - Ring 2: (10, 12, 40)
   - Ring 3: (-5, 15, 60)
   - Ring 4: (0, 10, 80)
   - Ring 5: (0, 10, 100)
   
4. Add welcome text (UI WorldSpace Canvas):
   - Position: (0, 15, 10)
   - Text: "Welcome to The Crucible - Fly through the rings"
```

**Test:** Can you fly through all 5 rings smoothly?

---

**Task 16.4: Checkpoint Detection System** *(1 hour)*

**Create checkpoint tracker:**

`Assets/_Project/Scripts/Gameplay/CheckpointTrigger.cs`:

```csharp
using UnityEngine;
using UnityEngine.Events;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private int checkpointID;
    [SerializeField] private UnityEvent onCheckpointReached;
    [SerializeField] private ParticleSystem successVFX;
    [SerializeField] private AudioClip successSound;
    
    private bool hasBeenTriggered = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenTriggered) return;
        
        if (other.CompareTag("Player"))
        {
            TriggerCheckpoint();
        }
    }
    
    private void TriggerCheckpoint()
    {
        hasBeenTriggered = true;
        
        // Visual feedback
        if (successVFX != null)
        {
            successVFX.Play();
        }
        
        // Audio feedback
        if (successSound != null)
        {
            AudioSource.PlayClipAtPoint(successSound, transform.position, 0.5f);
        }
        
        // Notify arena manager
        ArenaManager.Instance?.RegisterCheckpoint(checkpointID);
        
        onCheckpointReached?.Invoke();
        
        // Change color to show completion
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.material.color = Color.green;
        }
    }
    
    public void Reset()
    {
        hasBeenTriggered = false;
    }
}
```

**Attach to each NavRing trigger, assign incrementing IDs (0, 1, 2, 3, 4).**

---

### **Afternoon Session (4 hours): Building Remaining Zones**

**Task 16.5: Build Zone 2 - The Slalom** *(1 hour)*

```
Create empty: "Zone2_TheSlalom"
Position: (0, 10, 120) - continues from Zone 1

Layout:
- 10 Obstacle Pillars in zigzag pattern
- Left: (−15, 10, 140), (−15, 10, 160), (−15, 10, 180)...
- Right: (+15, 10, 150), (+15, 10, 170), (+15, 10, 190)...
- Spacing: 20m between pillars
- Width: 30m corridor

Goal: Must Quick Boost to make sharp turns
```

**Add UI indicator:**
```
WorldSpace Canvas at entrance:
"The Slalom - Use Quick Boost for sharp turns"
```

---

**Task 16.6: Build Zone 3 - The Gauntlet** *(45 minutes)*

```
Create empty: "Zone3_TheGauntlet"
Position: (0, 10, 250)

Layout:
- Use Wall_10m prefabs to create tunnel
- Length: 100m
- Width: 8m (tight!)
- Height: 6m
- Add 5-6 floating obstacles inside (cubes, 2x2x2)
- Random positions to require micro-adjustments

Goal: Maintain max speed without hitting walls
```

---

**Task 16.7: Build Zone 4 - The Drift Ring** *(1 hour)*

```
Create empty: "Zone4_TheDriftRing"
Position: (0, 10, 370)

Layout:
1. Create circular boundary (use 20 Wall sections arranged in circle)
   - Radius: 30m
   - Height: 10m
   
2. Central target: Large glowing sphere
   - Position: (0, 10, 370)
   - Scale: (3, 3, 3)
   - Material: Emissive red
   
3. Entry/Exit gates at opposite sides

Goal: Enter → Activate Drift → Circle-strafe around target → Exit
```

**Add special script:**

`Assets/_Project/Scripts/Gameplay/DriftZone.cs`:

```csharp
using UnityEngine;
using TMPro;

public class DriftZone : MonoBehaviour
{
    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private Transform centerTarget;
    [SerializeField] private float maxDistanceForPoints = 30f;
    
    private PlayerController player;
    private float timeInDrift = 0f;
    private bool playerInZone = false;
    
    private void Update()
    {
        if (playerInZone && player != null && player.IsDrifting)
        {
            // Award points for drifting near center
            float distance = Vector3.Distance(player.transform.position, centerTarget.position);
            
            if (distance < maxDistanceForPoints)
            {
                timeInDrift += Time.deltaTime;
                scoreText.text = $"Drift Time: {timeInDrift:F1}s";
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerController>();
            playerInZone = true;
            timeInDrift = 0f;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            
            if (timeInDrift > 3f)
            {
                scoreText.text += "\n Nice!";
            }
        }
    }
}
```

---

**Task 16.8: Build Zone 5 - The Maze** *(1 hour)*

```
Create empty: "Zone5_TheMaze"
Position: (0, 10, 450)

Layout (3D maze using Wall sections):
- 40m x 40m x 20m volume
- Multiple branching paths
- 3 difficulty routes:
  • Easy Path (wide, 10m corridors): 60 seconds
  • Medium Path (narrow, 6m corridors): 45 seconds
  • Hard Path (tight, 4m corridors): 30 seconds
  
All paths lead to same exit

Construction tip:
1. Plan on paper (top-down grid)
2. Build outer walls first
3. Add internal walls to create paths
4. Add vertical variation (ramps, different heights)
5. Mark paths with colored lights:
   - Green = Easy
   - Yellow = Medium
   - Red = Hard
```

---

**Task 16.9: Build Zone 6 - The Proving Grounds** *(15 minutes)*

```
Create empty: "Zone6_ProvingGrounds"
Position: (0, 10, 520)

Layout:
- Massive open area (100m x 100m x 50m high)
- Random obstacle placement:
  • 20 pillars (various sizes)
  • 10 floating platforms
  • 5 NavRings at extreme positions (high, low, far)
  
- Final checkpoint at far end: (0, 10, 620)

Goal: Free-form navigation, find your own path
```

**Add finish line:**

```
Create large glowing gateway:
- Two 20m tall pillars
- Glowing top beam
- Trigger zone
- Victory text: "Arena Complete!"
```

---

**End of Day 16 Commit:**
```
git add .
git commit -m "Day 16: Complete arena with 6 zones - The Crucible"
```

---

## **📅 Day 17: Arena Systems & Interactivity (8 hours)**

**Daily Goal:** Add systems that make the arena feel alive and responsive. Timers, scoring, feedback systems.

---

### **Morning Session (4 hours): Arena Manager System**

**Task 17.1: Create Arena Manager** *(2 hours)*

`Assets/_Project/Scripts/Gameplay/ArenaManager.cs`:

```csharp
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ArenaManager : MonoBehaviour
{
    public static ArenaManager Instance { get; private set; }
    
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI checkpointText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    
    [Header("Checkpoints")]
    [SerializeField] private int totalCheckpoints = 15;
    
    private float runStartTime;
    private float currentRunTime;
    private float bestTime = Mathf.Infinity;
    private HashSet<int> triggeredCheckpoints = new HashSet<int>();
    private bool runActive = false;
    private bool runCompleted = false;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        LoadBestTime();
        UpdateUI();
    }
    
    private void Update()
    {
        if (runActive && !runCompleted)
        {
            currentRunTime = Time.time - runStartTime;
            UpdateTimerDisplay();
        }
    }
    
    public void StartRun()
    {
        if (runActive) return;
        
        runActive = true;
        runCompleted = false;
        runStartTime = Time.time;
        triggeredCheckpoints.Clear();
        
        Debug.Log("Arena run started!");
    }
    
    public void RegisterCheckpoint(int checkpointID)
    {
        if (!runActive)
        {
            StartRun();
        }
        
        if (triggeredCheckpoints.Contains(checkpointID)) return;
        
        triggeredCheckpoints.Add(checkpointID);
        
        Debug.Log($"Checkpoint {checkpointID} reached! ({triggeredCheckpoints.Count}/{totalCheckpoints})");
        
        UpdateUI();
        
        // Check if run complete
        if (triggeredCheckpoints.Count >= totalCheckpoints)
        {
            CompleteRun();
        }
    }
    
    private void CompleteRun()
    {
        runCompleted = true;
        float finalTime = currentRunTime;
        
        Debug.Log($"Arena completed in {finalTime:F2} seconds!");
        
        if (finalTime < bestTime)
        {
            bestTime = finalTime;
            SaveBestTime();
            Debug.Log("New best time!");
        }
        
        UpdateUI();
    }
    
    public void ResetRun()
    {
        runActive = false;
        runCompleted = false;
        currentRunTime = 0f;
        triggeredCheckpoints.Clear();
        
        // Reset all checkpoint triggers
        CheckpointTrigger[] checkpoints = FindObjectsOfType<CheckpointTrigger>();
        foreach (CheckpointTrigger cp in checkpoints)
        {
            cp.Reset();
        }
        
        UpdateUI();
        
        Debug.Log("Arena reset!");
    }
    
    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = $"Time: {currentRunTime:F2}s";
        }
    }
    
    private void UpdateUI()
    {
        if (checkpointText != null)
        {
            checkpointText.text = $"Checkpoints: {triggeredCheckpoints.Count}/{totalCheckpoints}";
        }
        
        if (bestTimeText != null && bestTime < Mathf.Infinity)
        {
            bestTimeText.text = $"Best Time: {bestTime:F2}s";
        }
        
        UpdateTimerDisplay();
    }
    
    private void SaveBestTime()
    {
        PlayerPrefs.SetFloat("ArenaBestTime", bestTime);
        PlayerPrefs.Save();
    }
    
    private void LoadBestTime()
    {
        bestTime = PlayerPrefs.GetFloat("ArenaBestTime", Mathf.Infinity);
    }
}
```

---

**Task 17.2: Create Arena UI** *(1 hour)*

**Add to Canvas:**

```
Canvas
├── TimerPanel (Top Center)
│   └── TimerText: "Time: 0.00s"
├── CheckpointPanel (Top Left)
│   └── CheckpointText: "Checkpoints: 0/15"
├── BestTimePanel (Top Right)
│   └── BestTimeText: "Best Time: --"
└── InstructionsPanel (Center, fades after 5s)
    └── Text: "Fly through all checkpoints as fast as possible!"
```

**Style the UI:**
- Semi-transparent dark background
- Large readable font (40pt+)
- Bright text (white/cyan)
- Subtle glow effect

---

**Task 17.3: Start Line Trigger** *(30 minutes)*

**Create start line in Zone 1:**

```csharp
// Assets/_Project/Scripts/Gameplay/StartLineTrigger.cs

using UnityEngine;

public class StartLineTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem startEffectVFX;
    [SerializeField] private AudioClip startSound;
    
    private bool hasStarted = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasStarted)
        {
            hasStarted = true;
            
            ArenaManager.Instance.StartRun();
            
            if (startEffectVFX != null)
            {
                startEffectVFX.Play();
            }
            
            if (startSound != null)
            {
                AudioSource.PlayClipAtPoint(startSound, transform.position);
            }
        }
    }
    
    public void Reset()
    {
        hasStarted = false;
    }
}
```

Place trigger at entrance to Zone 1.

---

**Task 17.4: Finish Line Celebration** *(30 minutes)*

**Enhance finish line:**

```csharp
// Assets/_Project/Scripts/Gameplay/FinishLineTrigger.cs

using UnityEngine;
using System.Collections;

public class FinishLineTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem celebrationVFX;
    [SerializeField] private AudioClip victorySound;
    [SerializeField] private GameObject victoryUI;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerVictory();
        }
    }
    
    private void TriggerVictory()
    {
        // Visual celebration
        if (celebrationVFX != null)
        {
            celebrationVFX.Play();
        }
        
        // Audio
        if (victorySound != null)
        {
            AudioSource.PlayClipAtPoint(victorySound, transform.position, 1.0f);
        }
        
        // Show victory UI
        if (victoryUI != null)
        {
            victoryUI.SetActive(true);
            StartCoroutine(HideVictoryUIAfterDelay(5f));
        }
        
        // Slow motion moment
        StartCoroutine(VictorySlowMo());
    }
    
    private IEnumerator VictorySlowMo()
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(1.0f);
        Time.timeScale = 1.0f;
    }
    
    private IEnumerator HideVictoryUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (victoryUI != null)
        {
            victoryUI.SetActive(false);
        }
    }
}
```

---

### **Afternoon Session (4 hours): Environmental Details**

**Task 17.5: Add Environmental Lighting** *(1.5 hours)*

**Make zones visually distinct with lighting:**

**Zone 1 (Gates): Welcoming**
```
- Add point lights at each ring: Cyan, Intensity 3, Range 15
- Warm ambient
```

**Zone 2 (Slalom): Dynamic**
```
- Add spot lights on pillars: White, Intensity 5
- Alternating on/off pattern (use simple script)
```

**Zone 3 (Gauntlet): Intense**
```
- Red warning lights along tunnel: Intensity 2, Range 10
- Flashing hazard lights at obstacles
```

**Zone 4 (Drift Ring): Focused**
```
- Central target: Strong red light, Intensity 8
- Boundary lights: Yellow, Intensity 2
```

**Zone 5 (Maze): Mysterious**
```
- Colored path markers:
  • Green lights on easy path
  • Yellow lights on medium path
  • Red lights on hard path
- Low intensity (1.5), creates atmosphere
```

**Zone 6 (Proving Grounds): Epic**
```
- Dramatic directional lights
- Volumetric fog (if performance allows)
- Bright finish line: White, Intensity 10
```

**Script for blinking lights:**

```csharp
// Assets/_Project/Scripts/Environment/BlinkingLight.cs

using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    [SerializeField] private Light lightComponent;
    [SerializeField] private float blinkInterval = 1.0f;
    [SerializeField] private float onDuration = 0.5f;
    
    private float timer;
    private bool isOn = true;
    
    private void Update()
    {
        timer += Time.deltaTime;
        
        if (isOn && timer >= onDuration)
        {
            lightComponent.enabled = false;
            isOn = false;
        }
        else if (!isOn && timer >= blinkInterval)
        {
            lightComponent.enabled = true;
            isOn = true;
            timer = 0f;
        }
    }
}
```

---

**Task 17.6: Add Ambient Particles** *(1 hour)*

**Create atmosphere with floating particles:**

```
Create particle system: "AmbientMotes"
Position: Center of each zone

Settings:
- Emission: 10 particles/second
- Start Lifetime: 20 seconds
- Start Speed: 0.5
- Start Size: Random 0.1 to 0.3
- Shape: Box (50, 50, 50)
- Color: Faint white (alpha: 50)
- Gravity Modifier: -0.1 (float upward)

Effect: Creates sense of space and movement
```

---

**Task 17.7: Audio Zones** *(1 hour)*

**Add ambient audio to each zone:**

`Assets/_Project/Scripts/Audio/AudioZone.cs`:

```csharp
using UnityEngine;

public class AudioZone : MonoBehaviour
{
    [SerializeField] private AudioClip ambientClip;
    [SerializeField] private float volume = 0.3f;
    [SerializeField] private float fadeSpeed = 2f;
    
    private AudioSource audioSource;
    private bool playerInZone = false;
    
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = ambientClip;
        audioSource.loop = true;
        audioSource.volume = 0f;
        audioSource.spatialBlend = 0.5f; // Half 3D
        audioSource.Play();
    }
    
    private void Update()
    {
        float targetVolume = playerInZone ? volume : 0f;
        audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, fadeSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }
}
```

**Find ambient sounds:**
- Zone 1: Gentle wind
- Zone 2: Industrial hum
- Zone 3: Alarm klaxon (low volume)
- Zone 4: Mechanical whirring
- Zone 5: Echoing drips
- Zone 6: Epic orchestral swell

Attach AudioZone script to each zone's root object.

---

**Task 17.8: Secret Shortcuts** *(30 minutes)*

**Add hidden routes for expert players:**

**Shortcut 1: Slalom Bypass**
```
Add high route above the slalom
- Requires vertical boost + quick boost to reach
- Shaves 10 seconds off time
- Single hidden NavRing marks it
```

**Shortcut 2: Maze Skip**
```
Add breakable wall section
- Requires boosting into it at high speed
- Opens alternate fast route
- Respawns after 30 seconds
```

**Shortcut 3: Proving Grounds Launch Pad**
```
Add angled platform that launches player toward finish
- Must hit at correct angle and speed
- Risky (easy to miss finish line)
- Ultimate speedrun strat
```

---

**End of Day 17 Commit:**
```
git add .
git commit -m "Day 17: Arena systems - timing, scoring, audio zones, shortcuts"
```

---

## **📅 Day 18: Performance Optimization (8 hours)**

**Daily Goal:** Ensure rock-solid 60fps performance. Profile, optimize, and polish the technical foundation.

---

### **Morning Session (4 hours): Profiling & Optimization**

**Task 18.1: Comprehensive Performance Profiling** *(1.5 hours)*

**Setup profiling environment:**

1. Open Profiler: `Window > Analysis > Profiler`
2. Enable deep profiling: `Profiler > Preferences > Enable Deep Profiling`
3. Set to record: CPU, Rendering, Memory, Physics, Audio

**Profiling Test Protocol:**

```
Test 1: Idle in Zone 1
- Stand still for 30 seconds
- Record baseline performance

Test 2: Full Speed Flight
- Boost through entire arena at max speed
- Record during movement

Test 3: Intense VFX Scene
- Quick Boost repeatedly while drifting
- All VFX active simultaneously

Test 4: All Audio Playing
- Trigger all audio zones
- Record audio performance
```

**Document results:**

| Test | CPU (ms) | Rendering (ms) | Physics (ms) | Memory (MB) | FPS |
|------|----------|----------------|--------------|-------------|-----|
| Idle | [ ] | [ ] | [ ] | [ ] | [ ] |
| Flight | [ ] | [ ] | [ ] | [ ] | [ ] |
| VFX | [ ] | [ ] | [ ] | [ ] | [ ] |
| Audio | [ ] | [ ] | [ ] | [ ] | [ ] |

**Target: All < 16.6ms total (60fps)**

---

**Task 18.2: CPU Optimization** *(1.5 hours)*

**Common issues and fixes:**

**Issue 1: GetComponent in Update()**

Search entire codebase for:
```csharp
// BAD
void Update() {
    GetComponent<Rigidbody>().AddForce(...);
}

// GOOD
private Rigidbody rb;
void Awake() {
    rb = GetComponent<Rigidbody>();
}
void Update() {
    rb.AddForce(...);
}
```

Fix all instances.

**Issue 2: Find/FindObjectOfType in loops**

```csharp
// BAD
void Update() {
    FindObjectOfType<ArenaManager>().DoSomething();
}

// GOOD
private ArenaManager manager;
void Start() {
    manager = FindObjectOfType<ArenaManager>();
}
void Update() {
    manager.DoSomething();
}
```

**Issue 3: String concatenation in Update**

```csharp
// BAD
timerText.text = "Time: " + time + "s";

// GOOD
timerText.text = $"Time: {time:F2}s"; // Or even better, cache the format
```

**Issue 4: Camera.main every frame**

```csharp
// Cache Camera.main
private Camera mainCam;
void Start() {
    mainCam = Camera.main;
}
```

---

**Task 18.3: Rendering Optimization** *(1 hour)*

**Optimize draw calls:**

1. **Static Batching:**
   - Select all non-moving environment objects
   - Inspector > Check "Static"
   - Reduces draw calls by batching

2. **Texture Atlasing:**
   - Combine all environment materials into single atlas
   - Or ensure all use same material (URP batches automatically)

3. **LOD Setup (if needed):**
   - Select complex prefabs
   - Add Component > LOD Group
   - Configure 3 levels (High/Med/Low)

4. **Occlusion Culling:**
   - `Window > Rendering > Occlusion Culling`
   - Select all environment: Mark as "Occluder/Occludee"
   - Bake occlusion
   - Test: Objects behind walls shouldn't render

**Check draw calls in Stats window:**
```
Game View > Stats button
Target: < 50 draw calls
```

---

### **Afternoon Session (4 hours): Memory & Polish**

**Task 18.4: Memory Optimization** *(1 hour)*

**Check for memory leaks:**

1. Profiler > Memory module
2. Take snapshot at start
3. Play for 5 minutes
4. Take another snapshot
5. Compare: Memory should be stable, not climbing

**Common leaks:**

**Particle pooling:**
```csharp
// Ensure particles are properly recycled
ParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
```

**Audio clips:**
```csharp
// Use AudioSource.PlayOneShot instead of creating new sources
```

**Event subscriptions:**
```csharp
// Always unsubscribe in OnDestroy
private void OnDestroy() {
    playerController.OnBoostEvent -= HandleBoost;
}
```

---

**Task 18.5: Physics Optimization** *(45 minutes)*

**Optimize collision detection:**

1. **Player Rigidbody:**
   - Collision Detection: **Continuous Dynamic** (prevents tunneling)
   - Interpolate: **Interpolate**

2. **Environment Rigidbodies:**
   - Should NOT have Rigidbody if static
   - If dynamic: Collision Detection: **Discrete**

3. **Physics Layers:**
   - Create layers:
     - Player
     - Environment
     - Triggers
   - `Edit > Project Settings > Physics`
   - Layer Collision Matrix: Disable unnecessary collisions
     - Triggers don't collide with Triggers
     - Environment doesn't collide with Environment

4. **Simplify Colliders:**
   - Use Box/Sphere colliders instead of Mesh colliders where possible
   - Mesh colliders are expensive

---

**Task 18.6: VFX Optimization** *(1 hour)*

**Reduce particle overhead:**

```
For each ParticleSystem:
1. Check Max Particles: Should be reasonable
   - Thrusters: 100 max
   - Speed lines: 200 max
   - Explosions: 50 max

2. Check emission rates:
   - Are you emitting more than needed?
   - Reduce by 20% and test if noticeable

3. Disable unnecessary modules:
   - Collision (expensive): Only if needed
   - Lights: Very expensive, avoid
   - Sub Emitters: Expensive, use sparingly

4. Texture size:
   - Particle textures: 256x256 max
   - Compress to RGBA Compressed
```

**Total active particles target: < 500**

---

**Task 18.7: Build Size Optimization** *(45 minutes)*

**Reduce final build size:**

1. **Texture Compression:**
   - Select all textures
   - Inspector > Max Size: 1024 (or lower if possible)
   - Compression: High Quality

2. **Audio Compression:**
   - Music: Vorbis, Quality 70%
   - SFX: ADPCM or Vorbis, Quality 50-70%

3. **Mesh Compression:**
   - Select all models
   - Inspector > Mesh Compression: Medium

4. **Stripping:**
   - `Edit > Project Settings > Player > Other Settings`
   - Managed Stripping Level: High
   - Strip Engine Code: Enabled

---

**Task 18.8: Final Performance Test** *(30 minutes)*

**Run full arena at max settings:**

1. Build the game (Development Build)
2. Run on target hardware (GTX 1060 equivalent)
3. Complete entire arena run
4. Monitor FPS with:
   - `Stats > FPS` in Unity
   - Or add FPS counter to UI

**Acceptance Criteria:**
- [ ] Minimum FPS: 60
- [ ] Average FPS: 70+
- [ ] No frame drops below 55
- [ ] Load time: < 10 seconds
- [ ] Build size: < 500 MB

**If failing:** Go back to profiler, identify bottleneck, optimize further.

---

**End of Day 18 Commit:**
```
git add .
git commit -m "Day 18: Performance optimization - 60fps stable, optimized assets"
```

---

## **📅 Day 19: Final Polish & Bug Fixing (8 hours)**

**Daily Goal:** Micro-polish pass. Fix every small annoyance. Make it feel professional.

---

### **Morning Session (4 hours): The Polish Pass**

**Task 19.1: Control Refinement** *(1.5 hours)*

**Test every input 100 times:**

**Checklist:**
- [ ] Quick Boost always responds instantly
- [ ] Drift activates/deactivates reliably
- [ ] No "stuck" states (can always move)
- [ ] Camera never glitches
- [ ] Boost gauge updates smoothly
- [ ] All buttons work as expected

**Edge cases to test:**

```
1. Press Quick Boost while boost is at exactly 0
   - Expected: Nothing, warning sound
   - Fix if broken

2. Activate Drift while moving at exactly minimum speed
   - Expected: Activates, then deactivates when slowing
   - Fix if broken

3. Collide with wall at max speed
   - Expected: Bounce, no freeze, no explosion
   - Fix if broken

4. Spam Quick Boost button 20 times
   - Expected: Respects cooldown, no weird behavior
   - Fix if broken

5. Rotate 360° while drifting
   - Expected: Smooth, no gimbal lock
   - Fix if broken
```

**Document all bugs in:** `BugLog_Final.md`

---

**Task 19.2: Audio Polish** *(1 hour)*

**Audio mixing final pass:**

1. Open Audio Mixer
2. Play through arena
3. Adjust volumes:

```
Master: 0 dB
├── SFX: -3 dB
│   ├── Movement: -6 dB
│   │   ├── Thrusters: -8 dB (subtle, not overwhelming)
│   │   ├── Boost: -2 dB (prominent)
│   │   └── Collisions: -4 dB
│   └── UI: -10 dB (quiet, just feedback)
└── Ambient: -12 dB (background atmosphere)
```

**Add subtle reverb to zone audio:**
- Audio Mixer > Add > SFX Reverb
- Room Size: 0.5
- Decay Time: 1.5s

**Test:**
- No audio clipping (check mixer meters)
- All sounds audible but not harsh
- Good balance between layers

---

**Task 19.3: VFX Polish** *(1 hour)*

**Fine-tune every particle effect:**

**Thruster Trails:**
- Reduce lifetime by 10% (less clutter)
- Increase start speed by 20% (more dramatic)

**Speed Lines:**
- Make more transparent (alpha: 80 → 60)
- Increase length scale (3.0 → 4.0)

**Drift Trail:**
- Brighten color slightly
- Widen at start (0.5 → 0.7)

**Quick Boost Flash:**
- Add more particles (20 → 30)
- Faster expansion

**Test: Record 30 seconds of gameplay**
- Watch back
- Does every effect add to the experience?
- Remove/reduce anything distracting

---

**Task 19.4: UI/UX Polish** *(30 minutes)*

**Make UI beautiful:**

**Boost Gauge:**
```
Add gradient to fill:
- Start (left): Bright cyan
- End (right): Dark cyan
Creates depth

Add animated glow when full:
- Pulse between 1.0 and 1.2 alpha
- 1-second loop
```

**Timer Text:**
```
Add drop shadow:
- Color: Black, alpha 180
- Offset: (2, -2)
- Blur: 2

Add subtle outline:
- Color: Cyan
- Width: 1px
```

**Checkpoint Counter:**
```
Animate when checkpoint reached:
- Scale pulse: 1.0 → 1.2 → 1.0 over 0.3s
- Color flash: White → Cyan
```

---

### **Afternoon Session (4 hours): Bug Hunt & Final Test**

**Task 19.5: Systematic Bug Hunt** *(2 hours)*

**Test protocol - try to break the game:**

**Collision Tests:**
```
1. Hit every wall type at max speed
2. Get stuck in corners
3. Fly under/over arena boundaries
4. Collide with multiple objects simultaneously
```

**System Tests:**
```
1. Drain boost to zero, try to boost
2. Complete arena in reverse
3. Skip checkpoints, go to finish
4. Alt-tab during play
5. Unplug controller mid-flight
6. Pause/unpause repeatedly
```

**Performance Tests:**
```
1. Trigger 10 Quick Boosts in 3 seconds
2. Activate all VFX simultaneously
3. Fly through all audio zones rapidly
4. Complete arena 5 times in a row (memory leak check)
```

**Fix all bugs immediately.**

---

**Task 19.6: Quality of Life Features** *(1 hour)*

**Add reset button:**

```csharp
// In ArenaManager
private void Update() {
    // ... existing code ...
    
    // Press R to reset (debug)
    if (Keyboard.current.rKey.wasPressedThisFrame) {
        ResetArena();
    }
}

private void ResetArena() {
    ResetRun();
    
    // Teleport player to start
    player.position = spawnPoint.position;
    player.GetComponent<Rigidbody>().velocity = Vector3.zero;
}
```

**Add pause menu:**

```csharp
// Assets/_Project/Scripts/UI/PauseMenu.cs

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private GameObject pausePanel;
    private bool isPaused = false;
    
    private void Update() {
        if (Keyboard.current.escapeKey.wasPressedThisFrame) {
            TogglePause();
        }
    }
    
    private void TogglePause() {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
        
        // Disable player input when paused
        FindObjectOfType<PlayerController>().enabled = !isPaused;
    }
    
    public void ResumeButton() {
        TogglePause();
    }
    
    public void RestartButton() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void QuitButton() {
        Application.Quit();
    }
}
```

**Create pause UI panel with buttons.**

---

**Task 19.7: Accessibility Pass** *(30 minutes)*

**Quick accessibility additions:**

**Option 1: Camera shake toggle**
```csharp
// In settings
public static bool cameraShakeEnabled = true;

// In impulse trigger
if (cameraShakeEnabled) {
    impulseSource.GenerateImpulse(...);
}
```

**Option 2: Motion blur toggle**
```csharp
// Simple toggle in settings
motionBlur.active = motionBlurEnabled;
```

**Option 3: UI scale option**
```csharp
// Canvas scaler
canvasScaler.scaleFactor = uiScale; // 0.8, 1.0, 1.2
```

Add these to a simple settings menu.

---

**Task 19.8: Final Playtest** *(30 minutes)*

**Play the entire arena 3 times:**

**Run 1: Casual**
- Explore, take your time
- Find secrets
- Enjoy the experience

**Run 2: Speedrun**
- Go as fast as possible
- Use all shortcuts
- Optimize route

**Run 3: External Tester**
- Watch someone else play
- Don't help unless stuck
- Take notes on confusion

**After all 3 runs:**
- [ ] Is it fun all 3 times?
- [ ] Does it teach mechanics naturally?
- [ ] Are there bugs?
- [ ] Is performance stable?
- [ ] Would you show this to someone?

---

**End of Day 19 Commit:**
```
git add .
git commit -m "Day 19: Final polish pass - bug fixes, QoL features, accessibility"
```

---

## **📅 Day 20: Demo Build & Phase 1 Review (8 hours)**

**Daily Goal:** Create the final demo build. Document everything. Celebrate completion. Plan Phase 2.

---

### **Morning Session (4 hours): Final Build Creation**

**Task 20.1: Pre-Build Checklist** *(45 minutes)*

**Verify everything:**

```
Code:
- [ ] No Debug.Log() in production code
- [ ] No test/cheat shortcuts enabled
- [ ] All serialized fields assigned in Inspector
- [ ] No missing script references
- [ ] All scenes in Build Settings

Assets:
- [ ] All materials assigned
- [ ] All prefabs not broken
- [ ] No missing textures
- [ ] Audio clips all assigned

Settings:
- [ ] Quality settings appropriate
- [ ] Resolution options correct
- [ ] Input bindings finalized
- [ ] Physics settings locked

Performance:
- [ ] 60fps in all areas
- [ ] No memory leaks
- [ ] Build size acceptable
- [ ] Load times reasonable
```

---

**Task 20.2: Build Configuration** *(30 minutes)*

**Setup build settings:**

1. `File > Build Settings`

2. **Platform:** PC, Mac & Linux Standalone
   - Target Platform: Windows (or your target)
   - Architecture: x86_64

3. **Scenes in Build:**
   - Add `Phase1_FinalArena.unity`
   - Remove test scenes

4. **Player Settings:**
   ```
   Company Name: [Your Name/Studio]
   Product Name: CRIMSON COMET - Phase 1 Demo
   Version: 0.1.0
   Default Icon: (create simple icon)
   
   Resolution:
   - Default Width: 1920
   - Default Height: 1080
   - Fullscreen Mode: Fullscreen Window
   - Resizable Window: True
   
   Splash Screen:
   - Show Unity Logo: False (if using Plus/Pro)
   - Or customize with your logo
   ```

5. **Build Configuration:**
   - Development Build: **UNCHECKED** (for final)
   - Compression: LZ4HC (faster loading)

---

**Task 20.3: Create Master Build** *(1 hour)*

**Build process:**

1. Click "Build"
2. Choose location: `Builds/Phase1_Final/`
3. Name: `CrimsonComet_Phase1.exe`
4. Wait for build to complete

**Test the build:**
- Run .exe
- Play through entire arena
- Check for:
  - [ ] No crashes
  - [ ] All assets load
  - [ ] Audio works
  - [ ] Controls respond
  - [ ] Performance is good

**If any issues:** Fix, rebuild.

---

**Task 20.4: Create Itch.io Page (Optional)** *(45 minutes)*

**Upload to Itch.io for easy sharing:**

1. Create account at itch.io
2. Dashboard > Create New Project

**Page setup:**
```
Title: CRIMSON COMET - Phase 1 Prototype
URL: crimson-comet-prototype
Classification: Game
Kind of project: HTML/Downloadable
Release Status: Prototype
Pricing: Free

Description:
"A high-speed mecha flight combat prototype showcasing the 'Fluid Momentum' 
movement system. Featuring Quick Boost dodging and High-G Drift mechanics.

This is a Phase 1 gray-box prototype focusing on movement feel.

Controls: Gamepad recommended. See included guide.

Features:
- 6DoF flight controls
- Quick Boost evasive dashing
- High-G Drift (decouple momentum from aim)
- 6-zone test arena
- Speedrun timer

Development time: 1 month (160 hours)"

Tags: prototype, action, mech, flight, speedrun
```

3. Upload your build .zip
4. Set screenshots (take 5-6 action shots)
5. Publish as "Restricted" or "Public"

**Copy link for portfolio.**

---

**Task 20.5: Create Trailer/Showcase Video** *(1 hour)*

**Record final showcase:**

**Shot list (2-3 minutes):**
```
0:00-0:10 - Title card: "CRIMSON COMET - Phase 1"
0:10-0:20 - Basic movement showcase (fly through Zone 1)
0:20-0:35 - Quick Boost montage (various uses)
0:35-0:50 - High-G Drift demonstration (circle-strafe, retreat fire)
0:50-1:10 - Slalom run (show skill expression)
1:10-1:30 - Full speed tunnel run
1:30-1:50 - Drift ring mastery
1:50-2:10 - Maze speedrun
2:10-2:30 - Proving grounds finale
2:30-2:45 - UI showcase (timer, checkpoints, best time)
2:45-3:00 - End card: "Phase 2: Combat - Coming Soon"
```

**Editing:**
- Use free software (DaVinci Resolve, Shotcut)
- Add text overlays explaining mechanics
- Background music (epidemic sound free, or royalty-free)
- Export 1080p60

**Upload to YouTube:**
- Title: "CRIMSON COMET - Phase 1 Movement Prototype"
- Description: Link to Itch.io, explain project
- Tags: indie game, prototype, mecha, game development

---

### **Afternoon Session (4 hours): Documentation & Retrospective**

**Task 20.6: Create Final Documentation Pack** *(2 hours)*

**Document everything for future reference:**

**1. Phase1_FinalReport.md:**

```markdown
# CRIMSON COMET - Phase 1 Final Report

## Project Overview
- **Start Date:** [Date]
- **Completion Date:** [Date]
- **Total Development Time:** 160 hours
- **Team Size:** Solo/[Team size]
- **Engine:** Unity 6
- **Status:** Complete ✅

## Goals Achieved
- [x] Functional 6DoF flight controls
- [x] Quick Boost mechanic implemented
- [x] High-G Drift mechanic implemented
- [x] Complete test arena (6 zones)
- [x] Full VFX/SFX/Haptic feedback
- [x] Performance target met (60fps)
- [x] Demo build created

## Metrics
- **Scenes:** 1 main arena
- **Scripts:** ~25 total
- **Assets:** ~150 (primitives, materials, audio)
- **Build Size:** [X] MB
- **Lines of Code:** ~3000
- **Performance:** Stable 60fps on GTX 1060

## Key Features
1. **Movement System**
   - 6DoF flight physics
   - Boost gauge management
   - Speed-based FOV/effects
   
2. **Signature Mechanics**
   - Quick Boost (0.3s cooldown, 25 boost cost)
   - High-G Drift (min 20 m/s, preserves momentum)
   
3. **Arena Design**
   - 6 distinct zones
   - 15 checkpoints
   - Speedrun timer
   - Hidden shortcuts
   
4. **Feedback Systems**
   - Dynamic VFX (thrusters, trails, speed lines)
   - Layered audio (thrusters, boosts, ambient)
   - Post-processing (motion blur, bloom)
   - Haptic feedback (controller rumble)

## Technical Achievements
- Object pooling for VFX
- Cinemachine dynamic camera
- Audio mixer ducking
- Optimized particle systems
- Static batching for environment

## Challenges Overcome
1. **Drift mechanic clarity**
   - Solution: Velocity indicator, color-coded VFX
   
2. **Boost feel**
   - Solution: Screen shake, audio layering, haptics
   
3. **Camera motion sickness**
   - Solution: Adjustable FOV, smooth damping
   
4. **Performance with VFX**
   - Solution: Particle limits, LOD, batching

## What Went Well
- [Your notes]

## What Was Difficult
- [Your notes]

## Lessons Learned
- [Your notes]

## Next Steps: Phase 2
- Implement combat (Week 5-8)
- Add enemy AI
- Create weapon systems
- Boss encounter
```

---

**2. TechnicalReference.md:**

```markdown
# Technical Reference - Quick Lookup

## Key Scripts
- `PlayerController.cs` - Core movement logic
- `PlayerAudioManager.cs` - All audio
- `ThrusterController.cs` - VFX management
- `ArenaManager.cs` - Timing/scoring
- `DynamicLookTarget.cs` - Camera look-ahead

## Tuned Values
See `TuningValues.md` for complete list

## Architecture Patterns Used
- Event-driven (OnBoostExecuted, etc.)
- Singleton (ArenaManager.Instance)
- Component-based (modular systems)
- ScriptableObject data (future weapons)

## Performance Targets
- 60fps minimum
- <16ms frame time
- <500 active particles
- <50 draw calls
```

---

**3. ControlsGuide_Final.md** (updated):

Add tips for mastery:
```markdown
## Advanced Tips

### Boost Chain Timing
Quick Boost cooldown is 0.3s. Count "1-2-3" and boost 
on every third count for perfect chains.

### Drift Entry Speed
Drift activates at 20 m/s but feels best at 60+ m/s.
Build speed first, then drift.

### Camera Control
During drift, camera look-ahead shows momentum direction.
Use this to plan your exit vector.

### Shortcuts
- Slalom: Look for the high route
- Maze: Red path is fastest but hardest
- Proving Grounds: Launch pad is risky but worth 15s
```

---

**Task 20.7: Phase 1 Retrospective** *(1 hour)*

**Honest self-assessment:**

**Fill out:**

```markdown
# Phase 1 Retrospective

## What I'm Proud Of
1. [Achievement]
2. [Achievement]
3. [Achievement]

## What I'd Do Differently
1. [Learning]
2. [Learning]
3. [Learning]

## Biggest Challenge
[Description and how you overcame it]

## Most Fun Part
[What made you excited to work each day]

## Skill Growth
What I learned:
- Technical: [e.g., Cinemachine, particle systems]
- Design: [e.g., game feel, level design]
- Process: [e.g., scope management, iteration]

## Time Analysis
Where did the 160 hours go?
- Week 1 (Movement): 40 hours
- Week 2 (Mechanics): 40 hours
- Week 3 (Feedback): 40 hours
- Week 4 (Polish): 40 hours

Was this accurate to plan? [Yes/No + notes]

## Would I Ship This?
As a demo/prototype: [Yes/No]
As a full game: [No - needs combat, story, etc.]

## Confidence for Phase 2
How ready am I to add combat? [1-10]
What scares me: [Concerns]
What excites me: [Opportunities]
```

---

**Task 20.8: Create Phase 2 Preparation Document** *(45 minutes)*

**Plan the next phase:**

`Phase2_Prep.md`:

```markdown
# Phase 2 Preparation - Combat Systems

## Phase 2 Goals (4 weeks)
1. Implement Ranger stance (beam rifle + grenades)
2. Implement Assault stance (melee weapons)
3. Create 3 enemy types (Grunt, Sniper, Brawler)
4. Build 1 combat test level
5. Polish combat feel to Phase 1 quality

## Before Starting Phase 2
- [ ] Take 3-7 day break (avoid burnout)
- [ ] Review GDD combat section
- [ ] Research reference games (ZOE2, ACE, DMC)
- [ ] Sketch weapon ideas
- [ ] Plan enemy AI behavior trees

## Questions to Answer
1. Should combat use lock-on or free-aim?
2. How does damage feedback work in 3D space?
3. How to balance movement speed with aim precision?
4. Should enemies also drift/boost?

## Risks
1. Combat might feel worse than movement (common)
2. AI in 3D space is complex
3. Weapon balance is hard
4. Might need to adjust movement for combat

## Success Criteria
- Combat feels as good as movement
- Destroying enemies is satisfying
- Enemy variety creates interesting encounters
- Weapons have distinct use cases
```

---

**Task 20.9: Celebration & Archive** *(15 minutes)*

**You did it! Celebrate:**

1. **Final Git Commit:**
```
git add .
git commit -m "Phase 1 COMPLETE - Demo build, documentation, retrospective"
git tag "Phase1-COMPLETE" -m "Fully playable movement prototype with polished arena"
git push origin main --tags
```

2. **Archive everything:**
   - Zip entire project folder → `CrimsonComet_Phase1_Archive.zip`
   - Backup to cloud (Google Drive, Dropbox, etc.)
   - Save build separately

3. **Share your work:**
   - Post on Twitter/X with #gamedev #indiedev
   - Share on Reddit r/gamedev
   - Send to friends/family
   - Add to portfolio

4. **Take a break:**
   - **You've earned it!**
   - Step away for at least 3 days
   - Play other games
   - Rest your brain

---

## **🎯 Phase 1 Complete - Final Deliverables Checklist**

**Before you consider Phase 1 done:**

### **Functional Deliverables**
- [ ] Playable demo build (.exe)
- [ ] Complete test arena (6 zones)
- [ ] All movement mechanics working
- [ ] Stable 60fps performance
- [ ] Zero known crashes
- [ ] Timer/scoring system
- [ ] Reset/pause functionality

### **Code Deliverables**
- [ ] ~25 organized scripts
- [ ] All code commented
- [ ] No debug code in production
- [ ] Clean architecture
- [ ] Event-driven systems
- [ ] Optimized performance

### **Asset Deliverables**
- [ ] Complete material library
- [ ] VFX for all mechanics
- [ ] Full audio suite (15+ sounds)
- [ ] UI elements (HUD, menus)
- [ ] Prefab library
- [ ] Organized folder structure

### **Documentation Deliverables**
- [ ] Phase1_FinalReport.md
- [ ] TechnicalReference.md
- [ ] TuningValues.md (complete)
- [ ] ControlsGuide_Final.md
- [ ] Phase1_Retrospective.md
- [ ] Phase2_Prep.md
- [ ] BugLog (empty or resolved)

### **Portfolio Deliverables**
- [ ] 2-3 minute showcase video
- [ ] 5-10 screenshots
- [ ] Itch.io page (optional)
- [ ] GitHub repository (optional)
- [ ] Playable build ready to share

### **Quality Metrics**
- [ ] 60fps stable on target hardware
- [ ] External playtester can play without instruction
- [ ] No game-breaking bugs
- [ ] Professional presentation
- [ ] 30+ minutes of replayability
- [ ] "I want more" feedback from testers

---

## **📊 Phase 1 Final Statistics**

**Development Timeline:**
- **Planned:** 4 weeks (160 hours)
- **Actual:** [Fill in]
- **Variance:** [Fill in]

**Features Implemented:**
- **Planned:** 100%
- **Delivered:** [%]
- **Cut:** [List any cuts]

**Quality Achieved:**
- **Performance:** [60fps? Yes/No]
- **Polish Level:** [1-10]
- **Bug Count:** [X remaining]
- **Playability:** [1-10]

**Personal Growth:**
- **Technical Skills:** [What you learned]
- **Design Skills:** [What you learned]
- **Project Management:** [What you learned]

---

## **🚀 What's Next?**

### **Option 1: Continue to Phase 2 (Combat)**
- Take 3-7 day break
- Review combat GDD section
- Start Week 5 with fresh energy

### **Option 2: Iterate on Phase 1**
- Add more arena zones
- Create challenge modes
- Implement leaderboards
- Polish to "sellable demo" quality

### **Option 3: Pivot**
- Use movement system for different game
- Experiment with combat in isolation
- Start a different project

### **Recommended: Option 1**
The movement feels great. Combat will make it a game. Take a break, then continue.

---

## **💭 Final Thoughts**

**You've built something unique.** The "Fluid Momentum" system with High-G Drift doesn't exist in any other game. That's special.

**The hardest part is done.** Proving the core concept works is 80% of the battle. Combat is "just" building on this foundation.

**You can finish this.** You've shown you can plan, execute, and deliver. Phase 2 is the same process, just different systems.

**Take pride in what you've made.** This is a professional-quality prototype that showcases your skills. Put it in your portfolio with confidence.

---

## **🎊 Congratulations!**

**Phase 1 is COMPLETE!** 🎉

You've gone from concept to playable prototype in 4 weeks. That's an incredible achievement.

**You now have:**
- ✅ A unique movement system
- ✅ A polished demo
- ✅ Technical skills in Unity
- ✅ A foundation for a full game
- ✅ Something you can show the world

**Rest. Reflect. Then when you're ready...**

# **Phase 2: Combat Awaits** ⚔️

---

**Final Commit:**
```
git add .
git commit -m "🎉 PHASE 1 COMPLETE - CRIMSON COMET prototype delivered"
git tag "v0.1.0-Phase1-FINAL"
git push origin main --tags
```

**Now go celebrate. You've earned it, pilot.** 🚀✨

---

**THE END OF PHASE 1**