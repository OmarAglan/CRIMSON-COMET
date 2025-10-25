## **CRIMSON COMET: Development Roadmap**

### **Phase 4: Full Production - Building the Game**

**Primary Goal:** To develop all planned content for the game—including levels, enemies, story elements, and player progression—and integrate them into a cohesive experience that is playable from start to finish. By the end of this phase, the game will be "Content Complete" and achieve its Alpha milestone.

**Estimated Duration:** 16 Weeks (This is the longest phase and highly variable)

---

### **Module 4.1: Systems & Tooling (Weeks 15-16)**

**Sub-Goal:** To refactor the prototype code into scalable, data-driven systems. This initial investment in tooling will dramatically speed up the creation of all future content.

*   **Step 1: Create a Weapon System**
    *   **Task:** Use Unity's **ScriptableObjects** to define weapons. Instead of hard-coding stats in `WeaponManager.cs`, create a `WeaponData` ScriptableObject that holds variables like `damage`, `fireRate`, `projectilePrefab`, `muzzleFlashVFX`, `fireSFX`, etc.
    *   **Benefit:** You can now create a new weapon (e.g., a "Bazooka") entirely in the Unity editor by creating a new asset and tweaking its values, without writing a single new line of code.

*   **Step 2: Create an Enemy AI Framework**
    *   **Task:** Refactor the `EnemyAI.cs` script into a more modular state machine. Create a base `Enemy.cs` class that handles shared logic like health and taking damage. Then, create specific AI behaviors that inherit from it (e.g., `GruntAI.cs`, `SniperAI.cs`).
    *   **Benefit:** This makes creating new enemy types significantly faster. A "Sniper" might share 80% of its code with a "Grunt" but have a different `Attacking` state behavior.

*   **Step 3: Develop a Mission/Encounter Manager**
    *   **Task:** Create a robust system for scripting missions. This could be a "Wave Spawner" that reads from a ScriptableObject defining which enemies to spawn, where, and in what order. It should also handle triggering simple events like displaying UI messages or starting music cues.
    *   **Benefit:** This decouples level design from complex coding. You can now design and tweak enemy encounters for a level without having to modify the core mission script every time.

*   **End of Module 4.1 Checkpoint:** The core systems are now robust and data-driven. You have a "content factory" ready to be used.

---

### **Module 4.2: Content Expansion (Weeks 17-24)**

**Sub-Goal:** To create the bulk of the game's assets and levels, forming the main body of the player's experience. This is the longest and most asset-heavy part of production.

*   **Step 1: Level Production (3-5 New Levels)**
    *   **Workflow:** For each new level:
        1.  **Concept & Layout:** Design the mission flow on paper. What is the objective? What is the visual theme (e.g., "Inside a derelict space colony," "A dense nebula with low visibility")?
        2.  **Gray Box Layout:** Build the entire level using the primitive shapes from Phase 1. Place enemy encounters using your new `EncounterManager`. Playtest the flow and difficulty extensively.
        3.  **Art Pass:** Once the layout is fun and finalized, replace the gray boxes with the final, polished art assets, reusing assets from the Vertical Slice where possible and creating new key set pieces as needed.

*   **Step 2: Enemy Variety (2-3 New Enemy Types)**
    *   **Task:** Using your new AI framework, design and implement new enemy archetypes that challenge the player in different ways.
        *   **Example 1: The "Sniper."** Stays at long range, has a highly telegraphed but powerful shot, forcing the player to master cover and the Quick Boost.
        *   **Example 2: The "Brawler."** Aggressively rushes the player to engage in melee, testing their close-quarters combat and evasion skills.
    *   **Workflow:** Model, rig, and animate these new enemies to the same quality as the Grunt.

*   **Step 3: The Boss Encounter**
    *   **Task:** Design and build one unique, memorable boss fight for the game's climax. This will be a heavily scripted encounter, not relying on the standard AI system.
    *   **Example:** A massive, mobile fortress with multiple destructible components (turrets, engines, shield generators) and distinct attack phases.

*   **Step 4: Player Arsenal Expansion**
    *   **Task:** Using your new Weapon System, create 2-3 new primary weapons for the player.
    *   **Example:** A slow-firing, high-impact **Bazooka**; a short-range, rapid-fire **Gatling Gun**.
    *   **Workflow:** Model the new weapons and create their unique VFX and SFX.

*   **End of Module 4.2 Checkpoint:** The game now has a full campaign's worth of levels and a varied roster of enemies to fight.

---

### **Module 4.3: Narrative and Progression (Weeks 25-28)**

**Sub-Goal:** To connect the finished content with a narrative thread and a sense of player progression.

*   **Step 1: Implement the Game Loop Shell**
    *   **Task:** Build the out-of-mission UI. This includes the Main Menu, a Mission Select screen, and a simple "Hangar" screen where the player can equip different weapons.
    *   **System:** Implement a save/load system to track player progress (completed missions, unlocked weapons).

*   **Step 2: Storytelling Delivery**
    *   **Task:** Write the story and dialogue. Create a simple, non-intrusive system to deliver it.
    *   **Implementation:**
        *   **Mission Briefings:** Text-based briefing screens before each mission to set up the objective and story context.
        *   **In-Game "Comms":** A UI element that displays a character portrait and text for an "Operator" who gives the player real-time updates and story tidbits during missions.

*   **Step 3: Player Progression**
    *   **Task:** Define the unlock path. The player starts with the standard Beam Rifle. After completing Mission 2, they unlock the Bazooka. After Mission 4, the Gatling Gun.
    *   **Benefit:** This provides a clear incentive for players to progress through the campaign and gives them new tools to experiment with.

*   **End of Module 4.3 Checkpoint:** The game is now a structured experience. The player can progress from one mission to the next, follow a story, and unlock new gear along the way.

---

### **Module 4.4: Final Polish & Balancing (Weeks 29-30)**

**Sub-Goal:** To play through the entire game from start to finish, polishing rough edges, balancing the difficulty, and ensuring a smooth experience.

*   **Step 1: Game-Wide Balance Pass**
    *   **Task:** Play the full campaign. Is there a sudden difficulty spike in Mission 4? Is the final boss too easy? Is the Bazooka completely overpowered? Tweak all the data-driven values (health, damage, enemy counts, etc.) until the difficulty curve feels intentional and fair.

*   **Step 2: Audio and Visual Polish**
    *   **Task:** Do a full pass on all levels, adding ambient audio, polishing lighting, and ensuring all visual effects are consistent and impactful. Implement the final music for each mission.

*   **Step 3: Build the Settings Menu**
    *   **Task:** Create a functional settings menu. This must include: Audio controls (Master, Music, SFX volume), Graphics settings (Quality presets, Resolution), and Control remapping. This is essential for a PC release.

*   **End of Phase 4 - Final Review (Alpha Milestone):** The game is now **Content Complete**. Every planned level, enemy, and system is in the game. The experience can be played from the opening menu to the closing credits. The game is now ready for the next major stage: **Beta Testing**, where the focus will shift entirely to bug fixing, optimization, and feedback from external testers.