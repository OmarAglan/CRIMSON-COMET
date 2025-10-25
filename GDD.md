# **Game Design Document: CRIMSON COMET**

*   **Version:** 1.0
*   **Date:** October 25, 2025
*   **Author:** Gemini AI & User Collaboration
*   **Status:** First Draft

---

### **1.0 Vision Statement**

**CRIMSON COMET** is a single-player, third-person space combat action game focused on delivering the ultimate fantasy of being an untouchable ace pilot. Through a revolutionary "Fluid Momentum" control system, players will pilot a legendary crimson machine, weaving through asteroid fields and capital ship graveyards in a ballet of blistering speed and precision violence.

Our design pillars are:
*   **Gameplay is King:** The feel of the controls and the depth of the high-speed maneuvering system are paramount.
*   **Style is Queen:** We will achieve a stunning visual experience through a distinct, cel-shaded anime art style, ensuring spectacular visuals that perform well on a wide range of PCs.
*   **Skill Expression:** The game will be easy to learn, but the mechanics of drifting, boosting, and canceling will provide a near-infinite skill ceiling for players to master.

---

### **2.0 Core Concept**

*   **Genre:** Third-Person Mecha Action, Space Combat Sim
*   **Target Audience:** Fans of mecha anime (Gundam, Macross), and fast-paced action games (Zone of the Enders, Ace Combat, Armored Core).
*   **Platform:** PC (Steam)
*   **Unique Selling Propositions (USPs):**
    *   **High-G Drift System:** A unique control mechanic that separates movement direction from orientation, allowing for iconic, anime-style combat maneuvers.
    *   **Fluid Momentum:** A highly responsive control scheme built on boost-canceling that allows players to seamlessly chain actions together into a constant flow of motion.
    *   **Optimized Stylized Visuals:** A beautiful cel-shaded art style that delivers a high-impact visual experience without requiring high-end hardware.

---

### **3.0 Gameplay Mechanics**

#### **3.1 Player Controller & Movement (The "Fluid Momentum" System)**

The player has true Six Degrees of Freedom (6DoF) in a zero-gravity environment. The core of the game is the player's mastery over their machine's momentum.

*   **Primary Boost (R2/Right Trigger):** A sustained thrust that propels the player forward. This is the main mode of traversal and consumes the Boost Gauge at a steady rate.
*   **Boost Gauge:** A regenerating resource used for all high-performance maneuvers. Managing this gauge is central to survival and success.

##### **3.1.1 Quick Boost / Evasive Maneuver (Circle/B Button)**
*   **Function:** An instantaneous, high-consumption burst of speed in the direction of the player's input (Left Stick).
*   **Purpose:** The primary tool for dodging projectiles, repositioning quickly, and for advanced movement via Boost Canceling.
*   **Feedback:** Accompanied by a sharp audio cue, screen lurch, and visible vernier thruster VFX.

##### **3.1.2 High-G Drift (Hold L3/Left Stick Click)**
*   **Function:** When held while boosting, the player's machine maintains its current velocity vector (inertia). However, the player gains free control to rotate and aim the machine in any direction using the Right Stick.
*   **Purpose:** This is the game's signature mechanic. It allows players to perform advanced maneuvers like circle-strafing, firing at pursuers while flying away, and navigating complex environments without losing speed. This is the key to high-level skill expression.

##### **3.1.3 Boost Canceling**
*   **Function:** Activating a Quick Boost will immediately cancel the end-lag animation of nearly any other action (e.g., a melee combo, weapon firing recoil, a guard stun).
*   **Purpose:** Creates an incredibly high skill ceiling, allowing advanced players to maintain constant mobility and fluidly chain offense and defense together.

#### **3.2 Combat System**

Combat is a fast-paced exchange of ranged and melee attacks, heavily reliant on the movement system for positioning and evasion.

##### **3.2.1 Stance Switching (Triangle/Y Button)**
The player can instantly switch between two primary combat stances, altering their weapon functions on the R1 and L1 buttons.

*   **Stance 1: Ranger (Long-Range)**
    *   **R1/Right Bumper:** Fire primary ranged weapon (e.g., Beam Rifle).
    *   **L1/Left Bumper:** Secondary weapon function (e.g., Rifle Grenade Launcher, Head Vulcans for intercepting missiles).
    *   **Focus:** Mid-to-long range combat, precision shots, and suppressing fire.

*   **Stance 2: Assault (Close-Quarters)**
    *   **R1/Right Bumper:** Secondary melee function (e.g., Thrown Beam Axe).
    *   **L1/Left Bumper:** Primary melee combo (e.g., Beam Saber/Axe slashes). Tapping multiple times results in a combo.
    *   **Focus:** Aggressive, high-damage, close-range combat. Melee attacks have a built-in forward lunge to help close the distance.

##### **3.2.2 Defensive Options**
*   **Shield Guard (Hold L2/Left Trigger):** The player raises a shield that can block or significantly reduce incoming damage from the front, at the cost of reduced movement speed.
*   **Evasion:** The Quick Boost is the primary defensive tool for avoiding damage entirely.

#### **3.3 Core Gameplay Loop**

The moment-to-moment gameplay follows a dynamic loop:

1.  **Assess:** The player identifies targets and threats in the 3D space.
2.  **Maneuver:** The player uses the Fluid Momentum system (Boost, Drift, Quick Boost) to close the distance, evade fire, and achieve a superior tactical position.
3.  **Attack:** The player switches to the appropriate stance and engages the enemy with ranged or melee attacks.
4.  **React:** The player uses Boost Canceling and defensive options to react to enemy counter-attacks, then re-enters the Maneuver phase.

---

### **4.0 Control Scheme**

The game is designed primarily for a standard dual-analog gamepad.

| Action | Gamepad Button |
| :--- | :--- |
| **Movement** | Left Analog Stick |
| **Aiming / Camera** | Right Analog Stick |
| **Primary Boost** | R2 / Right Trigger (Hold) |
| **Guard (Shield)** | L2 / Left Trigger (Hold) |
| **Primary Weapon Fire** | R1 / Right Bumper |
| **Melee / Secondary** | L1 / Left Bumper |
| **Quick Boost / Dodge** | Circle / B Button |
| **Ascend** | X / A Button |
| **Descend** | Square / X Button |
| **High-G Drift** | L3 / Left Stick Click (Hold) |
| **Target Lock-On** | R3 / Right Stick Click |
| **Switch Stance** | Triangle / Y Button |
| **Cycle Sub-Weapon** | D-Pad |

*Note: Keyboard + Mouse controls will be supported, with key remapping available.*

---

### **5.0 Art & Audio Direction**

#### **5.1 Visual Style**
*   **Aesthetic:** Stylized, clean "anime" realism.
*   **Rendering:** Cel-shading with crisp outlines, simple material textures, and high-contrast lighting. The focus is on strong silhouettes and readability.
*   **Visual Effects (VFX):** Highly vibrant and impactful. Beam effects will be bright, clean lines; thruster trails will be long, glowing ribbons of energy; explosions will be stylized bursts of light and particles.
*   **Inspirations:** *Zone of the Enders: The 2nd Runner*, *Daemon X Machina*, *Gundam Extreme Vs. series*.

#### **5.2 Audio Design**
*   **Sound Effects:** Must be impactful and provide clear feedback. Key sounds include the deep hum of the main thrusters, the sharp "hiss-whoosh" of the Quick Boost, the piercing "vweem" of a beam rifle, and the iconic "vwoom" of a beam saber igniting.
*   **Music:** A high-energy score blending electronic and orchestral elements. Music will be dynamic, swelling during intense combat and becoming more ambient during exploration.
*   **Inspirations:** *Ace Combat series*, *Armored Core series*.

---

### **6.0 Technical Overview**

*   **Game Engine:** Unity 2022.3 LTS (or newer).
*   **Render Pipeline:** Universal Render Pipeline (URP) to ensure performance scalability.
*   **Input System:** Unity's modern "Input System" package for robust gamepad and remapping support.
*   **Target Hardware:** The game will be designed with performance on low-to-mid range PCs as a primary constraint. A stable framerate is non-negotiable and takes priority over visual complexity.

---

### **7.0 Story & Setting**

*   **Premise:** In the twilight of a devastating interstellar war, the player is an ace pilot for a small, remnant faction. Known only by their callsign, they pilot the "Crimson Comet," a machine with legendary performance that is their faction's last, best hope for survival. The story follows their critical missions against a vastly superior enemy force.
*   **Setting:** The "Graveyard of Giants," a solar system littered with the debris of a past war. Levels will include dense asteroid fields, the shattered husks of colossal warships and space colonies, and the orbital rings of abandoned planets.

---

### **8.0 Monetization**

*   **Model:** Premium, one-time purchase.
*   **DLC:** Potential for future expansion packs (new missions, new machine parts) if the game is successful. No microtransactions.