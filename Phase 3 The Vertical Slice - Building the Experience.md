## **CRCRIMSON COMET: Development Roadmap**

### **Phase 3: The "Vertical Slice" - Building the Experience**

**Primary Goal:** To produce a single, high-quality level from start to finish that is representative of the final game. This includes final player and enemy assets, a fully arted-out environment, a scripted mission structure, and polished audio-visual feedback.

**Technical Stack:** (Continuing from Phase 2)
*   **Engine:** Unity 6
*   **Rendering:** Universal Render Pipeline (URP) with custom shaders.
*   **Core Systems:** The proven mechanics from the Phase 2 prototype.

**Estimated Duration:** 6 Weeks

---

### **Week 9: The Star of the Show - The "Comet" Asset**

**Weekly Goal:** To replace the player's primitive cube with a fully modeled, textured, and animated "Crimson Comet" machine.

*   **Step 1: Low-Poly 3D Modeling**
    *   Using 3D modeling software (like Blender), create the 3D model for the player's mech.
    *   **Crucial:** Keep it low-poly. Focus on a strong, iconic silhouette rather than minute surface details. Think late PS2 / early PS3 era complexity. This is key for performance.
    *   Model the weapons (rifle, saber) as separate objects that can be attached to the model's "hands."

*   **Step 2: UV Unwrapping & Cel-Shaded Texturing**
    *   Properly UV unwrap the model.
    *   Create a simple color texture. For a cel-shaded style, this might just be flat areas of red, dark gray, and gold. You don't need complex, realistic materials. The "magic" happens in the shader.

*   **Step 3: Rigging and Basic Animation**
    *   Create a basic skeleton (a "rig") for the model.
    *   Create a few essential, simple animations:
        *   Idle (a gentle, floating state).
        *   Melee Swing (a simple, clean slash animation).
        *   Stance Switch (a quick animation for holstering/drawing weapons).
    *   Import the model and animations into Unity.

*   **Step 4: Implementing the Cel-Shader**
    *   Either find a good URP-compatible cel-shading shader from the Unity Asset Store or write a simple one using Shader Graph.
    *   The shader should create a "stepped" lighting effect (giving it that hard-edged anime shadow) and an outline effect. Apply this material to the imported model.

*   **Step 5: Asset Integration**
    *   In the Player prefab, disable the visibility of the primitive cube but keep its colliders.
    *   Make the new 3D model a child of the Player object.
    *   In `PlayerController.cs` and `WeaponManager.cs`, add code to trigger the animations based on player state (e.g., play the "Melee Swing" animation when attacking).

*   **End of Week 9 Checkpoint:** The player cube is gone! You are now flying a fully realized, cel-shaded Crimson Comet. Its basic movements and attacks are now visually represented.

---

### **Week 10: The Antagonist & The Arena**

**Weekly Goal:** To create the first real enemy and the environment for our vertical slice.

*   **Step 1: Modeling the "Grunt" Enemy**
    *   Following the same low-poly, cel-shaded workflow, create a model for a standard enemy mobile suit. Make its design simpler and more utilitarian than the player's heroic machine.
    *   Rig and animate it with basic idle and firing animations.
    *   Integrate this asset into the `EnemyAI` prefab, replacing the blue sphere.

*   **Step 2: Designing the "Graveyard of Giants" Level**
    *   Choose a theme for the level, as per the GDD: an asteroid field cluttered with the debris of a massive, destroyed capital ship.
    *   **Model the Set Pieces:** Create a handful of large, low-poly environmental assets: a few unique asteroid models, and several large chunks of a broken battleship (a bridge section, an engine nacelle, a piece of hull plating).
    *   **Level Layout ("Blockout"):** In a new scene, arrange these set pieces to create an interesting playspace. Use the principles from the gray box arena: create open areas for dogfighting, tight corridors (through a broken ship hull) for high-speed maneuvering, and large objects for cover.

*   **Step 3: Skybox and Lighting**
    *   Create or find a beautiful space skybox (a 360-degree texture of stars and nebulae). This adds immense production value for little performance cost.
    *   Set up the scene's lighting. In URP, a single Directional Light (representing a distant sun) will work well with the cel-shader to cast hard shadows.

*   **End of Week 10 Checkpoint:** You have a beautiful, atmospheric space environment populated with real enemy models. The game suddenly has a sense of place and scale.

---

### **Week 11 & 12: Mission & Polish - The Full Experience**

**Weekly Goal:** To script a complete mission flow and heavily polish all audio-visual feedback to a "final" quality standard. This is a two-week focus due to its high detail.

*   **Step 1: The Mission Manager**
    *   Create a script, `MissionManager.cs`, as a central controller for the level's events.
    *   **Scripted Events:** Create a simple, linear mission structure:
        1.  **Start:** Player spawns in. A simple UI message appears: "OBJECTIVE: Destroy all enemy scouts."
        2.  **Wave 1:** Spawn a wave of 3 "Grunt" enemies.
        3.  **Check Condition:** The `MissionManager` monitors the enemies. Once all are destroyed, trigger the next event.
        4.  **Wave 2:** A new message appears: "WARNING: Enemy reinforcements detected." Spawn a second, larger wave of 5 Grunts.
        5.  **End:** Once Wave 2 is defeated, a "MISSION COMPLETE" message is displayed.

*   **Step 2: Advanced "Juice" - VFX**
    *   Using Unity's Particle System, create high-quality, stylized effects:
        *   **Thruster Trails:** A beautiful ribbon of energy that trails behind the player and enemies.
        *   **Muzzle Flashes:** A bright flash at the barrel of the rifle when fired.
        *   **Beam Projectiles:** Replace the primitive projectile shapes with sleek, glowing particle effects.
        *   **Impacts:** A crackling spark effect that plays where a beam hits a surface or a shield.
        *   **Explosions:** A satisfying, multi-stage particle explosion for when an enemy is destroyed.

*   **Step 3: Advanced "Juice" - Audio**
    *   This is the time to replace all placeholder sounds with high-quality, unique ones.
    *   **Sound Design:** Record or acquire final sounds for weapons, thrusters, impacts, and explosions. The audio should match the weight and energy of the visuals.
    *   **Music:** Add a dynamic music track. Have a calmer "exploration" piece that transitions into a high-energy "combat" track when enemies are present.

*   **Step 4: HUD and UI Polish**
    *   Redesign the placeholder HUD from Phase 1 to match the game's stylized aesthetic.
    *   Add a lock-on reticle that changes color or shape when an enemy is in optimal range.
    *   Implement damage indicators (small arrows on the screen pointing towards the source of incoming fire).
    *   Add the mission objective text and other pop-up messages.

*   **End of Week 12 Checkpoint:** You have a complete, playable, 5-minute mission. It has a beginning, middle, and end. It features final-quality assets, polished effects, and a cohesive art and sound direction.

---

### **Week 13 & 14: Bug Fixing, Optimization, and Final Build**

**Weekly Goal:** To stabilize the vertical slice, ensure it runs smoothly, and prepare it for presentation.

*   **Step 1: Intensive Playtesting**
    *   Play the mission over and over. Get friends to play it.
    *   Log every bug, every awkward camera movement, every piece of feedback.

*   **Step 2: Debugging**
    *   Methodically go through the bug list and fix everything. Does the lock-on sometimes fail? Does an enemy get stuck in an asteroid? Fix it now.

*   **Step 3: Optimization**
    *   Use the Unity Profiler to check performance. Are there any spikes in CPU usage? Is the GPU struggling?
    *   Optimize where necessary. Maybe the explosion particle effect has too many particles. Maybe an enemy AI script is running too many checks per frame. Ensure a stable framerate is maintained throughout the mission.

*   **Step 4: Final Build & Presentation**
    *   Add a simple start menu and a credits screen.
    *   Build the project into a standalone executable (`.exe`).
    *   Play the final build from start to finish to ensure nothing broke in the build process.

*   **End of Phase 3 - Final Review:** You now have a polished, self-contained `CRIMSON COMET.exe`. This is your game in miniature. It proves that your concept is not only fun but also technically achievable and artistically compelling. This build is your cornerstone for all future development and your primary marketing tool.