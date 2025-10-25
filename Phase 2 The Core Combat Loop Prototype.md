## **CRIMSON COMET: Development Roadmap**

### **Phase 2: The Core Combat Loop Prototype**

**Primary Goal:** To implement all core combat mechanics (ranged, melee, stance-switching) and a basic enemy AI. The result will be a complete, self-contained gameplay loop where the player can engage and defeat a thinking, hostile target.

**Technical Stack:** (Continuing from Phase 1)
*   **Engine:** Unity 6
*   **Rendering:** Universal Render Pipeline (URP)
*   **Input:** Unity's modern Input System package
*   **Language:** C#

**Estimated Duration:** 4 Weeks

---

### **Week 5: Armaments - Ranged Combat & Stance Switching**

**Weekly Goal:** To implement the "Ranger" stance, allowing the player to target and destroy objects with a primary ranged weapon.

*   **Step 1: The "Beam Rifle"**
    *   Create a C# script, `WeaponManager.cs`, and attach it to the `Player` object. This script will handle firing logic and stance states.
    *   Create a projectile prefab: use a Sphere or Capsule primitive, name it `BeamProjectile`, and add a Rigidbody (gravity off). Create a `Projectile.cs` script for it.
    *   In `WeaponManager.cs`, create a `FireRifle()` function. This will `Instantiate` the `BeamProjectile` at a designated fire point (an empty child GameObject on the player) and use `Rigidbody.AddForce` to launch it.
    *   In `Projectile.cs`, add a self-destruct timer (`Destroy(gameObject, 3f)`) and the `OnCollisionEnter()` method to detect hits.

*   **Step 2: The Target**
    *   Create an "Enemy" primitive (a blue Sphere works well). Name it `DummyTarget`.
    *   Create a script, `Health.cs`, and attach it to the `DummyTarget`. Give it a `public float currentHealth`.
    *   In `Projectile.cs`, on collision, check if the other object `GetComponent<Health>()`. If it does, call a public function `TakeDamage(float amount)` on that component, then destroy the projectile.
    *   In `Health.cs`, the `TakeDamage` function subtracts from health and destroys the GameObject if health is <= 0.

*   **Step 3: Implementing Stance Switching**
    *   In `WeaponManager.cs`, create an `enum` for `CombatStance { Ranger, Assault }` and a variable to hold the `currentStance`.
    *   In `PlayerController.cs`, use the `StanceSwitch` input action to call a public function in `WeaponManager` that toggles the `currentStance`.
    *   In the `WeaponManager`, hook up the `FirePrimary` input action. When triggered, check `if (currentStance == CombatStance.Ranger)` and call `FireRifle()`.

*   **Step 4: Target Lock-On (Basic)**
    *   Implement a simple lock-on system. When the `LockOn` button is pressed, find the closest object with a `Health` component within a certain range and screen-space.
    *   Store this in a `public Transform currentTarget`.
    *   In `CameraController.cs`, if `currentTarget` is not null, modify the camera logic to keep both the player and the target in frame.
    *   In `PlayerController.cs`, if a target is locked, you can add a slight rotational force to the Rigidbody to help the player keep facing the target.

*   **End of Week 5 Checkpoint:** You can fly your cube, lock onto blue enemy spheres, and destroy them with projectiles. The "Ranger" stance is functional.

---

### **Week 6: Close Quarters - Melee Combat**

**Weekly Goal:** To implement the "Assault" stance, giving the player a satisfying close-range melee attack.

*   **Step 1: The "Beam Saber" Hitbox**
    *   Create a thin, long Cube as a child of the `Player` object to represent the saber.
    *   Add a `Collider` component and check the **`Is Trigger`** box. This allows it to detect collisions without physically pushing objects.
    *   By default, the saber's `GameObject` should be disabled.

*   **Step 2: Scripting the Melee Attack**
    *   In `WeaponManager.cs`, create a function `PerformMeleeAttack()`.
    *   This function will use a **Coroutine** to enable the saber object, wait for a short duration (e.g., 0.5 seconds for the swing), and then disable it again.
    *   Create a new script, `MeleeWeapon.cs`, and attach it to the saber object. This script will contain the `OnTriggerEnter()` function.
    *   Inside `OnTriggerEnter()`, check for a `Health` component on the object it hits and call `TakeDamage()`. To prevent a single swing from hitting the same enemy multiple times, keep a list of objects hit during the current swing and ignore them on subsequent trigger events.

*   **Step 3: Integrating Melee with Stance & Controls**
    *   In `WeaponManager.cs`, when the `FireSecondary` input (`L1`) is received, check if the `currentStance` is `Assault`, and if so, call the `PerformMeleeAttack` coroutine.
    *   **Melee with Momentum:** In `PlayerController.cs`, when a melee attack is initiated, add a small, short burst of forward impulse: `rb.AddForce(transform.forward * meleeLungeForce, ForceMode.Impulse)`. This lunge is critical for making the attack feel weighty and helps the player connect with their target.

*   **End of Week 6 Checkpoint:** The Assault stance is fully functional. You can switch to it, and L1 will perform a lunge-and-slash attack that can destroy enemy targets. The core loop of [Move -> Ranged -> Close In -> Melee -> Move Away] is now possible.

---

### **Week 7: The Enemy - Creating a Threat**

**Weekly Goal:** To evolve the enemy from a passive target into a dynamic participant that shoots back.

*   **Step 1: Basic Enemy AI State Machine**
    *   Create a script `EnemyAI.cs` and attach it to the `DummyTarget` prefab.
    *   Create a simple state machine using an `enum`: `Idle`, `Chasing`, `Attacking`.
    *   In `Update()`, check the distance to the player (`Player` object can be found by its Tag). If the player is within a large "aggro range," switch state from `Idle` to `Chasing`.
    *   In the `Chasing` state, use `Rigidbody.AddForce` to move the enemy slowly towards the player.
    *   If the player enters a smaller "attack range," switch to the `Attacking` state.

*   **Step 2: Enemy Ranged Attack**
    *   Give the enemy a simplified version of the `WeaponManager`.
    *   In the `Attacking` state, use a timer. When the timer is up, the enemy instantiates its own projectile prefab (make it a different color, like red) and fires it at the player's current position. Reset the timer.

*   **Step 3: Player Feedback for Damage**
    *   Add a `Health.cs` component to the `Player` object.
    *   When the player's `TakeDamage()` function is called, trigger clear feedback:
        *   **Audio:** Play a "hull impact" sound.
        *   **Visual:** Briefly flash a red UI Panel over the whole screen or trigger a camera shake effect.
        *   **UI:** Update the Player HUD (from Phase 1) to include a health bar that depletes.

*   **End of Week 7 Checkpoint:** The battlefield is now hostile. Enemy spheres will pursue you and shoot back, forcing you to use the movement skills from Phase 1 to evade fire while lining up your own shots. Combat now has stakes.

---

### **Week 8: Polish and Integration**

**Weekly Goal:** To integrate all new combat systems with the core movement mechanics and add a layer of polish ("Juice") to make the loop feel truly satisfying.

*   **Step 1: Implementing Boost Canceling**
    *   This is the final, crucial step for fluid combat. The `QuickBoost` function in `PlayerController.cs` must be able to interrupt other states.
    *   For example, if you are in the middle of the melee swing coroutine in `WeaponManager.cs`, the `QuickBoost` function should be able to stop that coroutine immediately.
    *   Test this extensively. Can you fire your rifle and instantly boost-cancel the recoil animation (which you can simulate with a short timer)? Can you finish a melee attack and instantly dodge?

*   **Step 2: Adding Combat "Juice"**
    *   **Hit Stop:** When your melee attack successfully connects (`OnTriggerEnter` in `MeleeWeapon.cs`), momentarily pause the game time (`Time.timeScale = 0.05f`) for a fraction of a second, then set it back to 1. This adds a powerful, chunky feel to impacts.
    *   **Enemy Hit Flash:** In the enemy's `Health.cs`, create a simple coroutine that briefly swaps its material to a bright white "flash" material when it takes damage.
    *   **Improved VFX/SFX:** Create slightly more distinct particle effects for player vs. enemy projectiles. Add a simple particle burst for when an enemy is destroyed. Find placeholder sounds for melee "swoosh," melee "impact," and an "explosion."

*   **Step 3: Full Loop Playtesting and Tuning**
    *   Populate your Gray Box Arena with several enemy spheres.
    *   Playtest the full loop extensively. Is the combat too easy? Too hard? Is one stance clearly better than the other?
    *   Spend the rest of the week **tuning public variables**: player health, enemy health, weapon damage, projectile speed, AI speed, boost costs, etc. This balancing act is what transforms a set of mechanics into a well-designed game.

*   **End of Phase 2 - Final Review:** You now have a complete vertical slice of the core gameplay. All primary mechanics are implemented in a functional, testable state. The key question is now: **"Is the loop of fighting these blue spheres fun and engaging for 10 minutes?"** If yes, you are ready to move on to creating actual content and art in Phase 3.