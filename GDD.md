# **CRIMSON COMET**
## **Complete Game Design Document**

---

**Version:** 2.0 (Extended Edition)  
**Date:** October 25, 2025  
**Document Status:** Living Document - Pre-Production  
**Classification:** Internal Development Use  
**Lead Designer:** [Your Name]  
**Project Code:** CC-001

---

## 📋 **Document Control**

| Version | Date | Author | Changes |
|---------|------|--------|---------|
| 1.0 | Oct 25, 2025 | Gemini AI & User | Initial draft |
| 2.0 | Oct 25, 2025 | Claude AI | Extended edition with full scope definition |

### **Document Purpose**
This GDD serves as the single source of truth for CRIMSON COMET's design, scope, and technical requirements. All team members must refer to this document when making design decisions. Any deviations must be documented and approved.

---

## 📑 **Table of Contents**

1. [Executive Summary](#1-executive-summary)
2. [Vision Statement](#2-vision-statement)
3. [Core Concept](#3-core-concept)
4. [Gameplay Mechanics](#4-gameplay-mechanics)
5. [Combat System](#5-combat-system)
6. [Control Scheme](#6-control-scheme)
7. [Progression & Unlocks](#7-progression--unlocks)
8. [Campaign Structure](#8-campaign-structure)
9. [Enemy Design](#9-enemy-design)
10. [Art & Audio Direction](#10-art--audio-direction)
11. [Technical Overview](#11-technical-overview)
12. [User Interface & UX](#12-user-interface--ux)
13. [Accessibility & Settings](#13-accessibility--settings)
14. [Story & Setting](#14-story--setting)
15. [Monetization & Business Model](#15-monetization--business-model)
16. [Budget & Resources](#16-budget--resources)
17. [Scope Management](#17-scope-management)
18. [Success Metrics](#18-success-metrics)
19. [Risk Assessment](#19-risk-assessment)
20. [Post-Launch Content](#20-post-launch-content)

---

## **1. Executive Summary**

### **1.1 The Pitch**
"**Armored Core meets Zone of the Enders**—a lightning-fast mecha action game where momentum mastery and precision violence create the ultimate ace pilot fantasy."

### **1.2 Target Market**
- **Primary:** PC gamers aged 18-35 who enjoy action games with high skill ceilings
- **Secondary:** Mecha anime fans (Gundam, Evangelion, Macross enthusiasts)
- **Genre Crossover:** Players of Devil May Cry, Bayonetta, Ace Combat, Armored Core

### **1.3 Platform & Release**
- **Platform:** PC (Steam) - Exclusive at launch
- **Price Point:** $14.99 USD (regional pricing adjusted)
- **Target Release:** Q4 2026 (14-month development timeline)
- **Minimum Viable Product:** 6 missions, 3 enemy types, 3 weapons

### **1.4 Team Size Assumption**
- **Solo Developer:** Timeline × 1.5 multiplier
- **2-Person Team:** Timeline as written
- **3+ Team:** Timeline × 0.75 multiplier (can add stretch goals)

### **1.5 Core Pillars**
1. **Gameplay is King** - Controls must feel perfect before adding content
2. **Style is Queen** - Striking visuals that run on modest hardware
3. **Skill Expression** - Easy to learn, lifetime to master
4. **Respect Player Time** - No grinding, no filler, pure action

---

## **2. Vision Statement**

### **2.1 The Fantasy**
CRIMSON COMET delivers the fantasy of being **an untouchable ace pilot** in a legendary machine. Players should feel like the protagonist of a mecha anime—performing impossible maneuvers, threading through asteroid fields at blistering speed, and turning entire battlefields into personal playgrounds of stylish destruction.

### **2.2 Core Experience**
Every moment of gameplay should deliver:
- **Constant Flow:** Movement transitions seamlessly between states
- **Precision Violence:** Every shot, every swing, every dodge feels intentional
- **Visual Spectacle:** Players create their own highlight reel moments naturally
- **Mastery Satisfaction:** Skill improvements are tangible and rewarding

### **2.3 Emotional Goals**
By the end of a play session, players should feel:
- ✅ Empowered (I'm in complete control)
- ✅ Skilled (I'm getting better)
- ✅ Stylish (That looked incredible)
- ✅ Satisfied (I conquered a challenge)

### **2.4 What We Are NOT**
- ❌ A simulation (we prioritize feel over realism)
- ❌ A grind fest (no padding, no busywork)
- ❌ A narrative-driven game (story supports gameplay, not vice versa)
- ❌ A live service (premium one-time purchase, complete experience)

---

## **3. Core Concept**

### **3.1 Genre Classification**
- **Primary:** Third-Person Mecha Action
- **Secondary:** Space Combat Sim (Arcade-style)
- **Sub-Genre:** Character Action Game (DMC-style combo system applied to flight combat)

### **3.2 Unique Selling Propositions**

#### **USP #1: High-G Drift System**
The signature mechanic that separates CRIMSON COMET from all other flight combat games. By decoupling orientation from momentum, players can:
- Strafe around enemies while maintaining forward velocity
- Fire at pursuers while flying away
- Navigate tight corridors without losing speed
- Execute anime-style "impossible" maneuvers

**Reference Games:** None do this exactly—we're pioneering this space.

#### **USP #2: Fluid Momentum Combat**
Boost-canceling allows offensive and defensive actions to flow into each other without interruption. This creates a skill ceiling comparable to fighting game combos but applied to 3D space combat.

**Reference Implementation:** Similar to Armored Core's quick-boost canceling but more forgiving and integrated into every action.

#### **USP #3: Performance-Optimized Anime Aesthetic**
Beautiful cel-shaded visuals that deliver stunning looks without requiring high-end hardware. We're targeting stable 60fps on GTX 1060—a card from 2016.

**Market Gap:** Most anime-style games are either low-budget or AAA. We're high-quality indie.

### **3.3 Competitive Analysis**

| Game | What They Do Well | What We Do Better |
|------|------------------|-------------------|
| **Zone of the Enders 2** | Speed, spectacle | Modern controls, PC optimization |
| **Armored Core 6** | Deep customization | Accessibility, instant action |
| **Ace Combat 7** | Epic scale | Close-quarters intensity |
| **Daemon X Machina** | Mecha power fantasy | Control precision, art direction |
| **Star Wars Squadrons** | Cockpit immersion | Third-person spectacle, melee |

### **3.4 Target Player Types**

#### **The Speedrunner** (Primary)
- Wants: Frame-perfect execution, shortcuts, time optimization
- We Deliver: Boost-cancel chains, mission rankings, tight controls

#### **The Stylist** (Primary)
- Wants: Looking cool, highlight moments, creative expression
- We Deliver: Drift maneuvers, dynamic camera, satisfying VFX

#### **The Story Consumer** (Secondary)
- Wants: Narrative context, cool characters, world immersion
- We Deliver: Mission briefings, environmental storytelling, anime tropes

#### **The Completionist** (Tertiary)
- Wants: Unlockables, achievements, 100% completion
- We Deliver: S-rank challenges, weapon unlocks, color schemes

---

## **4. Gameplay Mechanics**

### **4.1 Core Movement System: "Fluid Momentum"**

#### **4.1.1 Six Degrees of Freedom (6DoF)**
The player has complete freedom of movement in zero-gravity space:
- **Translation:** Forward/Back, Left/Right, Up/Down
- **Rotation:** Pitch, Yaw, Roll (full 360° on all axes)
- **Physics Model:** Newtonian momentum with arcade adjustments for playability

#### **4.1.2 Primary Boost (R2/Right Trigger)**
**Function:** Sustained forward thrust  
**Input:** Hold R2  
**Mechanics:**
- Accelerates player to maximum velocity (configurable: 80-120 m/s)
- Consumes Boost Gauge at 15 units/second
- Produces visible thruster trail VFX
- Audio: Low rumbling hum that increases in pitch with speed

**Design Intent:** This is the player's "sprint" button. It should feel powerful but not unlimited.

#### **4.1.3 Boost Gauge System**

**Core Resource Management:**
- **Maximum Capacity:** 100 units
- **Regeneration Rate:** 30 units/second (when not boosting)
- **Regeneration Delay:** Begins 0.5 seconds after releasing boost
- **Visual Feedback:** HUD bar (bright blue = full, orange = low, red flashing = empty)

**Consumption Rates:**
- Primary Boost: -15 units/second
- Quick Boost: -25 units per activation
- Melee Lunge: -15 units per attack
- Shield Block: -10 units/second while active

**Design Philosophy:** Boost management is a core skill. Skilled players can maintain near-constant mobility; new players must learn resource rhythm.

#### **4.1.4 Quick Boost / Evasive Maneuver (Circle/B Button)**

**Function:** Instantaneous directional burst  
**Input:** Tap Circle + Direction on Left Stick  
**Mechanics:**
- **Force Applied:** 40-60 m/s impulse (tunable in playtesting)
- **Direction:** 8-way directional (combines stick input with current facing)
- **No Direction Input:** Boosts directly forward
- **Cooldown:** 0.3 seconds (prevents spam)
- **Boost Cost:** 25 units per activation

**Feedback Stack:**
- **Visual:** Screen shake (2 frames), motion blur spike, bright thruster flash
- **Audio:** Sharp "WHOOSH-HISS" sound (distinct from primary boost)
- **Haptic:** Strong controller rumble pulse (0.2 seconds)

**Advanced Technique - Boost Chaining:**
When Quick Boost is used in the direction of existing momentum, velocity stacks (max: 150 m/s). This allows skilled players to build extreme speed.

#### **4.1.5 High-G Drift (Hold L3/Left Stick Click)**

**THE SIGNATURE MECHANIC**

**Function:** Decouples orientation from velocity vector  
**Input:** Hold L3 while moving  
**Mechanics:**
- **Physics State Change:** All directional input forces are disabled
- **Momentum Preservation:** Rigidbody velocity remains constant
- **Free Rotation:** Player can aim in any direction using Right Stick
- **Speed Requirement:** Must be moving >20 m/s to activate
- **Visual Feedback:** 
  - Velocity vector indicator appears on HUD
  - Thruster trail color shifts to yellow-orange
  - Subtle camera tilt (5° roll) in direction of momentum

**Use Cases:**
1. **Circle-Strafing:** Maintain speed while rotating around a target
2. **Retreating Fire:** Fly backward while facing forward to shoot
3. **Complex Navigation:** Thread through obstacles without changing flight path
4. **Anime Flourishes:** Barrel rolls and spins during combat for style

**Design Balance:**
- **Benefit:** Incredible maneuverability, maintains momentum
- **Cost:** Cannot change direction while drifting (commitment required)
- **Skill Expression:** Knowing *when* to drift vs. when to boost normally

#### **4.1.6 Boost Canceling (Advanced Mechanic)**

**Function:** Quick Boost interrupts animation recovery frames  
**Cancellable Actions:**
- Weapon firing recoil (all weapons)
- Melee attack recovery
- Shield guard stun/pushback
- Landing impact (if ground collisions exist)
- Stance switch animation

**Implementation:**
```
OnQuickBoostInput() {
    if (isInRecoveryState) {
        CancelCurrentAnimation();
        ExecuteQuickBoost();
    }
}
```

**Design Intent:** This creates DMC-style cancel combos but in 3D flight. Mastering this is the difference between intermediate and expert play.

**Learning Curve:**
- Week 1: Players use Quick Boost for dodging only
- Week 2: Players discover they can cancel melee recovery
- Week 3: Players chain attacks → cancel → reposition → repeat
- Mastery: Players maintain 100% uptime on offensive pressure

---

## **5. Combat System**

### **5.1 Combat Philosophy**
Combat in CRIMSON COMET is a **dynamic dance** of closing distance, delivering violence, and repositioning. The player must constantly evaluate:
1. **Spacing:** Am I in optimal range for my current weapon?
2. **Resources:** Do I have boost to dodge the incoming attack?
3. **Positioning:** Am I about to fly into an asteroid?
4. **Momentum:** Can I chain this attack into the next action?

### **5.2 Stance System**

#### **Core Concept:**
Rather than cycling through a weapon wheel, the player has **two combat modes** they can instantly switch between. Each stance changes what the R1 and L1 buttons do.

**Design Benefit:** 
- No menu diving in combat
- Encourages switching based on situation
- Creates distinct "phases" within encounters

#### **Stance 1: Ranger (Long-Range)**

**Visual Identifier:** Rifle held in two hands, shield on back  
**Optimal Range:** 100-300 meters  
**Gameplay Role:** Sustained DPS, enemy suppression, shield-breaking

**Button Mapping:**
- **R1:** Fire Primary Weapon (Beam Rifle)
- **L1:** Fire Secondary Weapon (Rifle Grenade / Head Vulcans)
- **L2:** Shield Guard (raise shield to block)

**Ranger Weapon Specs:**

**Beam Rifle (Primary):**
- Damage: 15 per shot
- Fire Rate: 300 RPM (5 shots/second)
- Projectile Speed: 200 m/s
- Magazine: Infinite (heat-based, 40 shots before 2s cooldown)
- Recoil: Mild (player velocity reduced by 2 m/s per shot)
- VFX: Clean cyan beam with bright muzzle flash
- SFX: "VWEEM-VWEEM" (high-pitched laser)

**Rifle Grenade (Secondary):**
- Damage: 50 (direct hit) + 30 (splash radius 15m)
- Fire Rate: 60 RPM (1 shot/second)
- Projectile: Arcing ballistic with 3-second fuse
- Ammo: 8 rounds (regenerates 1 per 5 seconds)
- Use Case: Area denial, grouped enemies, shield destruction
- VFX: Orange projectile trail, large explosion with screen shake
- SFX: "THOOMP" (launch) → "KA-BOOM" (detonation)

#### **Stance 2: Assault (Close-Quarters)**

**Visual Identifier:** Rifle stowed on back, melee weapon drawn  
**Optimal Range:** 0-50 meters  
**Gameplay Role:** Burst damage, high-risk/high-reward engagements

**Button Mapping:**
- **R1:** Throw Beam Axe (ranged melee)
- **L1:** Beam Saber Combo (tap up to 3 times)
- **L2:** Shield Guard (same as Ranger)

**Assault Weapon Specs:**

**Beam Saber (Primary Melee):**
- **Combo Chain:**
  - Hit 1: 40 damage, horizontal slash (0.4s)
  - Hit 2: 40 damage, vertical slash (0.4s)
  - Hit 3: 80 damage, spinning slash with forward dash (0.6s)
- **Total Combo:** 160 damage over 1.4 seconds
- **Hitbox:** 8-meter cone in front of player
- **Lunge Distance:** 15 meters per swing (helps close distance)
- **Boost Cost:** 10 units per swing
- **Cancel Window:** Can boost-cancel after hit 1 or 2 (not during hit 3)
- VFX: Glowing magenta blade with trail effect, spark bursts on hit
- SFX: "VWOOM" (ignition) → "SLASH-SLASH-SLASH" (swings) → "SHING" (impact)

**Thrown Beam Axe (Secondary):**
- Damage: 60 damage
- Speed: 80 m/s (slower than rifle projectiles)
- Range: 100 meters before returning
- Cooldown: 3 seconds (axe must return before re-throw)
- Tracking: Slight homing (15° correction per second)
- Use Case: Ranged option in Assault stance, finisher for fleeing enemies
- VFX: Spinning axe with energy trail, boomerang return
- SFX: "WHHHHIRRR" (throw) → "CHUNK" (impact)

### **5.3 Target Lock-On System**

**Activation:** Press R3 (Right Stick Click)  
**Behavior:**
- Scans 120° cone in front of player
- Locks onto nearest enemy with Health component
- Max lock range: 500 meters
- Lock persists until: enemy dies, player presses R3 again, or enemy leaves 150° cone for 3 seconds

**Lock-On Benefits:**
- Camera auto-adjusts to keep both player and target visible
- Slight aim assist (projectiles gain 5° homing toward locked target)
- Target health bar appears above enemy
- Distance to target displayed on HUD

**Lock-On Drawbacks:**
- Reduced camera freedom (can't fully free-look)
- Can only lock one target at a time
- Relies on player keeping target in front

**Design Intent:** Lock-on is helpful but not required. Skilled players may prefer free aim for multi-target scenarios.

### **5.4 Defensive Options**

#### **Shield Guard (L2/Left Trigger - All Stances)**

**Function:** Raises physical shield mounted on forearm  
**Mechanics:**
- **Damage Reduction:** 70% from frontal 90° cone
- **Boost Drain:** 10 units/second while raised
- **Movement Penalty:** Speed reduced to 40% while active
- **Shield HP:** 200 (regenerates 50/second after 3s of no damage)
- **Break State:** If shield HP reaches 0, 5-second stun (cannot boost)

**Visual Feedback:**
- Shield glows bright blue when raised
- Impact sparks on shield when hit
- Shield cracks/shatters when broken (regenerates visually over time)

**Strategic Use:**
- Emergency defense when boost is empty
- Holding position against enemy fire
- Protecting yourself during recovery frames

**Design Balance:** Shield is strong but immobile. Active evasion (Quick Boost) is always superior if you have the boost gauge and skill.

#### **Active Evasion (Quick Boost)**

**The Primary Defense:**  
Dodging via Quick Boost is always preferable to blocking because:
1. No movement penalty
2. Can reposition to better attack angle
3. Maintains offensive pressure
4. More stylish (higher skill expression)

**Design Goal:** New players shield-tank. Skilled players never stop moving.

### **5.5 Damage Types & Enemy Armor**

**Simplified System (For Clarity):**

| Damage Type | Strong Against | Weak Against |
|-------------|---------------|--------------|
| **Beam (Rifle, Saber)** | Shields, Light Armor | Heavy Armor |
| **Explosive (Grenade)** | Heavy Armor, Groups | Single Fast Targets |
| **Kinetic (Axe, Melee)** | All Armor Types | Shielded Enemies |

**Design Intent:** No type is useless, but situational optimization encourages stance-switching mid-combat.

### **5.6 Combat Feedback Loop**

**"Juice" Implementation (Critical for Feel):**

Every successful hit must trigger:
1. **Visual Feedback:**
   - Enemy white-flash for 2 frames
   - Damage numbers spawn above enemy (optional, toggle in settings)
   - Screen shake (intensity scales with damage: 0.1-0.5 units)
   
2. **Audio Feedback:**
   - Distinct "hit confirm" sound (different per weapon type)
   - Enemy pain/damage vocalization (synthesized, not human-sounding)
   
3. **Haptic Feedback:**
   - Controller rumble pulse (0.1s) on hit
   - Stronger rumble (0.3s) on killing blow

4. **Timing Feedback (Advanced):**
   - **Hit-stop:** On melee critical hits, freeze game for 0.05 seconds (creates weight)
   - **Slow-mo finish:** When final enemy in wave dies, 0.5s of 30% slow motion

**Design Philosophy:** The player must *feel* every successful action in their hands, eyes, and ears simultaneously.

---

## **6. Control Scheme**

### **6.1 Primary Input: Gamepad**

#### **Default Mapping (PlayStation Layout)**

| Action | Button | Hold/Tap | Notes |
|--------|--------|----------|-------|
| **Movement** | Left Stick | Hold | 8-directional relative to camera |
| **Camera/Aim** | Right Stick | Hold | Adjustable sensitivity |
| **Primary Boost** | R2 | Hold | Sustained thrust |
| **Quick Boost** | ◯ (Circle) | Tap | Evasive dash |
| **Shield Guard** | L2 | Hold | Raises shield |
| **Fire Primary** | R1 | Hold/Tap | Auto/semi-auto depends on weapon |
| **Fire Secondary** | L1 | Tap | Or melee combo in Assault |
| **Ascend** | × (Cross) | Hold | Vertical thrust up |
| **Descend** | ▢ (Square) | Hold | Vertical thrust down |
| **High-G Drift** | L3 | Hold | Signature move |
| **Target Lock** | R3 | Tap | Toggle lock-on |
| **Stance Switch** | △ (Triangle) | Tap | Ranger ↔ Assault |
| **Cycle Sub-Weapon** | D-Pad | Tap | If multiple secondaries unlocked |
| **Pause Menu** | Options | Tap | |

#### **Default Mapping (Xbox Layout)**

| Action | Button | Hold/Tap |
|--------|--------|----------|
| **Movement** | Left Stick | Hold |
| **Camera/Aim** | Right Stick | Hold |
| **Primary Boost** | RT | Hold |
| **Quick Boost** | B | Tap |
| **Shield Guard** | LT | Hold |
| **Fire Primary** | RB | Hold/Tap |
| **Fire Secondary** | LB | Tap |
| **Ascend** | A | Hold |
| **Descend** | X | Hold |
| **High-G Drift** | LSB (L3) | Hold |
| **Target Lock** | RSB (R3) | Tap |
| **Stance Switch** | Y | Tap |
| **Cycle Sub-Weapon** | D-Pad | Tap |
| **Pause Menu** | Menu | Tap |

### **6.2 Secondary Input: Keyboard + Mouse**

#### **Default KB+M Mapping**

| Action | Key/Button | Notes |
|--------|-----------|-------|
| **Forward/Back** | W/S | WASD movement |
| **Strafe Left/Right** | A/D | |
| **Ascend/Descend** | Space/Ctrl | Or scroll wheel |
| **Camera** | Mouse Movement | Free-look |
| **Primary Boost** | Shift | Hold |
| **Quick Boost** | Middle Mouse | Or Q |
| **Fire Primary** | Left Mouse | |
| **Fire Secondary** | Right Mouse | |
| **Shield Guard** | F | Hold or toggle |
| **High-G Drift** | Alt | Hold |
| **Target Lock** | Tab | Toggle |
| **Stance Switch** | E | Tap |
| **Cycle Sub-Weapon** | Mouse Wheel | Up/Down |
| **Pause Menu** | Esc | |

**KB+M Advantages:**
- Precise aiming for ranged combat
- Faster target switching (flick to new enemy)

**KB+M Challenges:**
- Drift + Free aim requires careful hand positioning
- Less intuitive analog movement (WASD is binary)

**Development Note:** KB+M is secondary priority. Must be playable but can be tuned post-launch.

### **6.3 Control Customization**

**Remapping Options (Must Include):**
- ✅ Every action can be rebound
- ✅ Multiple bindings per action (e.g., Space OR Middle Mouse for Quick Boost)
- ✅ Separate sensitivity for X and Y camera axes
- ✅ Sensitivity curves: Linear, Exponential, Logarithmic
- ✅ Deadzone adjustment (0-30% for stick drift compensation)
- ✅ Toggle vs. Hold options for: Boost, Drift, Shield, Lock-On

**Accessibility Presets:**
- **Preset A: "Southpaw"** (left/right stick functions swapped)
- **Preset B: "Bumper Jumper"** (face buttons moved to bumpers)
- **Preset C: "Single-Hand"** (all essential actions on one side)

### **6.4 Control Tutorialization**

**First Mission - Forced Tutorial Prompts:**
- **0:00-0:30** - Movement only (no combat)
- **0:30-1:00** - Primary Boost introduced
- **1:00-1:30** - Quick Boost introduced via obstacle
- **1:30-2:00** - High-G Drift introduced (fly through rings while aiming at target)
- **2:00+** - Combat tutorial begins

**Philosophy:** Show, don't tell. Force muscle memory through design, not text boxes.

---

## **7. Progression & Unlocks**

### **7.1 Progression Philosophy**
- ❌ No grinding, no RNG, no gating
- ✅ All content unlocked through natural story progression
- ✅ Optional challenges for cosmetics only
- ✅ Players can replay any mission to practice

### **7.2 Weapon Unlock Path**

**Starting Loadout:**
- **Ranger:** Beam Rifle + Rifle Grenade
- **Assault:** Beam Saber + Thrown Axe

**Mission 3 Unlock: Heavy Bazooka**
- **Stance:** Ranger (replaces Rifle Grenade in secondary slot)
- **Stats:** 150 damage, 30 RPM, 3-round magazine, 5m splash
- **Trade-off:** Massive damage but slow + limited ammo
- **Use Case:** Taking down heavy enemies or bosses

**Mission 5 Unlock: Gatling Gun**
- **Stance:** Ranger (replaces Beam Rifle in primary slot)
- **Stats:** 8 damage, 900 RPM, 200-round mag before overheat
- **Trade-off:** Lower per-shot damage but overwhelming DPS
- **Use Case:** Shredding light enemy swarms

**Mission 7 Unlock: Plasma Lance**
- **Stance:** Assault (replaces Beam Saber in primary slot)
- **Stats:** 200 damage, single thrust attack, 2s cooldown
- **Trade-off:** One-hit kill potential but slow and requires precision
- **Use Case:** Boss weak points, high-skill high-reward playstyle

### **7.3 Performance Part Upgrades**

**System:** Parts are found as optional pickups in side paths during missions

**Available Parts (9 Total):**

| Part Name | Effect | Location |
|-----------|--------|----------|
| **Boost Capacitor +** | +20% max boost gauge | Mission 2 side path |
| **Quick Regen Unit** | Boost recharges 50% faster | Mission 3 side path |
| **Reinforced Frame** | +30% max health | Mission 4 side path |
| **Weapon Amplifier** | +15% all weapon damage | Mission 5 side path |
| **Shield Extender** | +40% shield HP | Mission 6 side path |
| **Thruster Overdrive** | +10% max speed | Mission 7 side path |
| **Lightweight Armor** | -20% boost consumption | Mission 8 side path |
| **Auto-Repair System** | Regenerate 5 HP/second | Post-game unlock |
| **Burst Limiter** | Remove Quick Boost cooldown | Post-game unlock |

**Equip System:**
- Can equip **3 parts simultaneously**
- Must choose strategic loadout (glass cannon vs. tank vs. balanced)
- Can change parts between missions in Hangar

**Design Balance:** Parts provide ~30% power increase when optimized. Skill still matters more than build.

### **7.4 Cosmetic Unlocks**

**Color Schemes (12 Total):**
- **Default:** Crimson Red + Gold accents
- **Stealth Black:** Unlocked by completing any mission undetected
- **Arctic White:** Unlocked by S-ranking Mission 3
- **Royal Blue:** Unlocked by S-ranking Mission 5
- **Neon Pink:** Unlocked by S-ranking Mission 7
- **Veteran Green:** Unlocked by completing Campaign on Hard
- **Gold Plated:** Unlocked by S-ranking all missions
- **Prototype Gray:** Unlocked by finding all parts
- **Solar Orange:** Hidden pickup in Mission 4
- **Void Purple:** Hidden pickup in Mission 6
- **Hazard Yellow:** Complete game without using shield
- **Rival's Crimson:** Post-game challenge unlock

**Decals (8 Sets):**
- Unit emblems, kill marks, warning stripes
- Unlocked via achievements (30 kills with X weapon, etc.)

**No Pay-to-Win:** All cosmetics are earned, never purchased.

---

## **8. Campaign Structure**

### **8.1 Scope Definition**

**Minimum Viable Product (MVP):**
- **6 Story Missions** (5-8 hours first playthrough)
- **3 Enemy Types** (Grunt, Sniper, Brawler)
- **3 Weapon Sets** (starting + 1 Ranger + 1 Assault unlock)
- **1 Boss Fight** (final mission)

**Target Scope (If Development Goes Well):**
- **8 Story Missions** (6-10 hours first playthrough)
- **5 Enemy Types** (+ Heavy, + Support)
- **5 Weapon Sets** (starting + 2 Ranger + 2 Assault)
- **2 Boss Fights** (mid-game mini-boss + final boss)

**Stretch Goals (If Ahead of Schedule):**
- **10 Story Missions**
- Optional side missions (replayable encounters)
- Challenge modes (Time Attack, Survival)

### **8.2 Mission Structure Template**

**Every Mission Follows This Arc:**

1. **Briefing (30-60 seconds)**
   - Mission objective displayed
   - Story context from Operator
   - Player spawns in loadout screen

2. **Opening (1-2 minutes)**
   - Player enters level
   - Environmental storytelling
   - Light enemy contact

3. **Escalation (3-5 minutes)**
   - Primary objective pursuit
   - Multiple combat encounters
   - Environmental hazards

4. **Climax (2-4 minutes)**
   - Final wave or mini-boss
   - Maximum intensity

5. **Debrief (20 seconds)**
   - Mission clear screen
   - Rank displayed (S/A/B/C/D)
   - Unlocks shown (if any)

**Design Goal:** 6-8 minutes per mission average (speedrun: 3-4 min, casual: 8-12 min)

### **8.3 Mission Ranking System**

**Rank Calculation:**

| Rank | Requirements |
|------|-------------|
| **S** | Time <6 min, Accuracy >80%, No Deaths, Style >1000 |
| **A** | Time <8 min, Accuracy >70%, Max 1 Death |
| **B** | Time <10 min, Accuracy >60% |
| **C** | Time <15 min OR Mission Complete |
| **D** | Mission Complete (any condition) |

**Scoring Components:**

**Time Bonus:**
- Under 5 min: +500 points
- Under 7 min: +300 points
- Under 10 min: +100 points

**Combat Metrics:**
- **Accuracy:** (Shots Hit / Shots Fired) × 100
- **Kills:** 50 points per enemy
- **Style Points:** 
  - Kill with melee: +20
  - Kill while drifting: +30
  - Multi-kill (2+ within 2 seconds): +50
  - No damage taken in combat: +100

**Damage Taken Penalty:**
- 0 damage: +500 bonus
- <30% max HP damage: +200 bonus
- Death: -300 per death

**Design Intent:** Ranking system encourages multiple replays and mastery. S-ranks are challenging but achievable.

### **8.4 Detailed Mission Outline**

#### **Mission 1: "Graveyard Awakening"**
- **Objective:** Destroy scout patrol (6 Grunt enemies)
- **Location:** Asteroid field with ship debris
- **Unlocks:** Nothing (tutorial mission)
- **New Mechanics Introduced:** All basic movement + Ranger stance
- **Target Time:** 6 minutes
- **Narrative:** Player awakens in derelict ship, receives call from Operator, first combat sortie

#### **Mission 2: "Supply Run"**
- **Objective:** Escort cargo ship through enemy ambush
- **Location:** Dense asteroid field with narrow passages
- **Unlocks:** Boost Capacitor + part (side path)
- **New Mechanics Introduced:** Assault stance, escort objectives
- **Target Time:** 7 minutes
- **Narrative:** Faction needs supplies, player protects civilian transport

#### **Mission 3: "Silent Approach"**
- **Objective:** Infiltrate enemy comm station, destroy antenna
- **Location:** Interior of capital ship wreckage
- **Unlocks:** Heavy Bazooka + Quick Regen Unit (side path)
- **New Mechanics Introduced:** Stealth option (can avoid some enemies), weak points
- **Target Time:** 8 minutes
- **Narrative:** Disable enemy early-warning system
- **S-Rank Challenge:** Complete without being detected

#### **Mission 4: "Debris Field Defense"**
- **Objective:** Hold position against 3 waves of enemies
- **Location:** Broken space station orbiting planet
- **Unlocks:** Reinforced Frame part (side path) + Solar Orange color (hidden)
- **New Mechanics Introduced:** Wave survival, Sniper enemy type
- **Target Time:** 10 minutes
- **Narrative:** Protect evacuation of civilian refugees

#### **Mission 5: "Capital Assault"**
- **Objective:** Destroy enemy carrier's engines and bridge
- **Location:** Exterior and interior of massive enemy ship
- **Unlocks:** Gatling Gun + Weapon Amplifier (side path)
- **New Mechanics Introduced:** Large-scale destruction, Brawler enemy type
- **Target Time:** 9 minutes
- **Narrative:** Faction launches offensive, player is the spearhead
- **Mini-Boss:** Ace enemy pilot (1v1 duel)

#### **Mission 6: "The Gauntlet"**
- **Objective:** Escape through enemy blockade
- **Location:** Narrow canyon network with heavy enemy presence
- **Unlocks:** Shield Extender part (side path) + Void Purple color (hidden)
- **New Mechanics Introduced:** Time pressure (escape timer), mines
- **Target Time:** 7 minutes
- **Narrative:** Mission 5 was a trap, player must escape

#### **Mission 7: "Turning Point"** (MVP Final Mission)
- **Objective:** Destroy enemy command fortress
- **Location:** Enemy stronghold in planetary rings
- **Unlocks:** Plasma Lance + Thruster Overdrive (side path)
- **New Mechanics Introduced:** Multi-phase boss fight
- **Target Time:** 12 minutes
- **Narrative:** Faction launches final assault, player faces rival ace pilot
- **Boss:** "Obsidian Hawk" - rival ace with similar abilities

#### **Mission 8: "Crimson Dawn"** (Stretch Goal)
- **Objective:** Destroy enemy flagship's core
- **Location:** Interior of mobile fortress
- **Unlocks:** Post-game parts (Auto-Repair, Burst Limiter)
- **Target Time:** 15 minutes
- **Narrative:** True final battle, faction's fate decided
- **Boss:** "The Admiral's Machine" - massive mobile armor with multiple phases

### **8.5 Difficulty Modes**

**Normal (Default):**
- Enemy HP: 100%
- Enemy damage: 100%
- Enemy count: Standard
- Player HP: 100%

**Easy (For Story Focus):**
- Enemy HP: 70%
- Enemy damage: 70%
- Enemy count: -20%
- Player HP: 130%
- Boost regeneration: +30%

**Hard (For Skilled Players):**
- Enemy HP: 130%
- Enemy damage: 150%
- Enemy count: +30%
- Player HP: 100%
- Boost regeneration: -20%
- Enemy accuracy: +15%

**Insane (Post-Game Unlock):**
- Enemy HP: 200%
- Enemy damage: 200%
- Enemy count: +50%
- Player HP: 50%
- One-hit kill on most attacks
- For masochists only

---

## **9. Enemy Design**

### **9.1 Enemy Design Philosophy**
Each enemy type must:
1. **Teach a skill** (encourage player to use a specific mechanic)
2. **Have clear telegraphs** (attacks are readable, never cheap)
3. **Be visually distinct** (silhouette recognition at 200m)
4. **Scale in groups** (1 enemy is manageable, 5 require tactics)

### **9.2 Enemy Type: Grunt (Basic)**

**Role:** Cannon fodder, teaches fundamentals  
**First Appearance:** Mission 1

**Stats:**
- HP: 100
- Speed: 40 m/s
- Armor Type: Light

**AI Behavior:**
- **Idle State:** Patrol waypoints
- **Alert State:** Rotate toward player if within 300m
- **Chase State:** Pursue at 40 m/s if player within 200m
- **Attack State:** Stop at 100m range, fire rifle

**Attack Pattern:**
- **Rifle Burst:** 3-shot burst, 20 damage per shot, fires every 2 seconds
- **Projectile Speed:** 150 m/s
- **Accuracy:** 60% at optimal range

**Weak Points:**
- Head (2x damage multiplier)
- Back (1.5x damage, must flank)

**Loot on Death:**
- Nothing (killed for score only)

**Design Intent:** Practice target. Teaches player to aim, dodge, and use basic combat loop.

### **9.3 Enemy Type: Sniper (Precision)**

**Role:** Ranged threat, teaches evasion and cover use  
**First Appearance:** Mission 4

**Stats:**
- HP: 80 (low health, glass cannon)
- Speed: 30 m/s (slow, stationary when firing)
- Armor Type: Light

**AI Behavior:**
- **Positioning:** Stays at 200-400m range, behind cover
- **Attack Prep:** 2-second charge-up (bright red laser sight)
- **Attack:** Single devastating shot

**Attack Pattern:**
- **Charged Shot:** 120 damage (60% of player HP)
- **Telegraph:** Loud charging sound + red laser line
- **Projectile Speed:** 300 m/s (very fast)
- **Cooldown:** 5 seconds between shots
- **Accuracy:** 90% if player not moving, 40% if Quick Boosting

**Weak Points:**
- Exposed while charging (perfect opening for counter-attack)

**Design Intent:** Teaches player to identify threats, use Quick Boost at right moment, and prioritize targets.

### **9.4 Enemy Type: Brawler (Aggressive)**

**Role:** Close-range threat, teaches melee combat and spacing  
**First Appearance:** Mission 5

**Stats:**
- HP: 150 (tanky)
- Speed: 60 m/s (faster than player without boost)
- Armor Type: Medium (resistant to beam weapons)

**AI Behavior:**
- **Aggro:** Immediately rushes player when in 200m range
- **Close Combat:** Uses melee strikes when within 20m
- **Relentless:** Does not retreat, fights until death

**Attack Pattern:**
- **Melee Combo:** Two horizontal swings (40 damage each)
- **Tackle:** Dashes forward 30m, 60 damage on contact
- **Recovery:** 1-second vulnerability after missing tackle

**Weak Points:**
- Rear thruster (exposed after tackle whiff)

**Counter-Strategy:** 
- Bait tackle
- Quick Boost dodge
- Counter-attack during recovery

**Design Intent:** Teaches player to stay calm under pressure, use Assault stance effectively, and master timing.

### **9.5 Enemy Type: Heavy (Tank) [Stretch Goal]**

**Role:** Bullet sponge, teaches sustained damage and positioning  
**First Appearance:** Mission 6 (if included)

**Stats:**
- HP: 300
- Speed: 25 m/s (very slow)
- Armor Type: Heavy (weak to explosives)

**AI Behavior:**
- **Area Denial:** Deploys stationary turret mode
- **Suppression:** Continuous fire to lock down zones
- **Shield:** Deploys frontal barrier (must flank)

**Attack Pattern:**
- **Turret Mode:** 360° rotating cannon, 15 damage per shot, 600 RPM
- **Missile Barrage:** Fires 8 homing missiles, 30 damage each, once per 10 seconds

**Weak Points:**
- Back radiator (must flank for 3x damage)
- Shoot down missiles mid-flight (grants style points)

**Design Intent:** Teaches player to use terrain, approach from multiple angles, and manage long engagements.

### **9.6 Enemy Type: Support (Healer) [Stretch Goal]**

**Role:** Force multiplier, teaches target prioritization  
**First Appearance:** Mission 7 (if included)

**Stats:**
- HP: 60 (very fragile)
- Speed: 50 m/s (evasive)
- Armor Type: None (no damage reduction)

**AI Behavior:**
- **Stay Back:** Maintains 150m from player
- **Healing:** Restores 50 HP to damaged allies every 5 seconds
- **Evasion:** Constantly strafes and boosts when targeted

**Attack Pattern:**
- **Self-Defense:** Weak pistol (5 damage, last resort only)
- **Smoke Screen:** Deploys visual obscurement when HP <30

**Tactical Impact:**
- Makes other enemies significantly harder to kill
- Forces player to break through frontline to eliminate healer first

**Design Intent:** Teaches triage decision-making and prioritization under fire.

### **9.7 Boss Design: "Obsidian Hawk" (Rival Ace)**

**First Appearance:** Mission 7 (final boss for MVP)

**Stats:**
- HP: 1500 (multi-phase, 500 HP per phase)
- Speed: 80 m/s (faster than player)
- Armor: Adaptive (changes per phase)

**Phase 1: Ranged Duel**
- **Behavior:** Maintains 150m range, uses sniper rifle
- **Attacks:**
  - Charged Sniper Shot (150 damage)
  - Rapid-Fire Mode (20 damage/shot, 5-shot burst)
- **Phase Transition:** <500 HP, retreats behind debris

**Phase 2: Close Combat**
- **Behavior:** Aggressive melee assault
- **Attacks:**
  - Beam Lance Thrust (100 damage)
  - Spinning Slash (80 damage, AOE)
  - Dash Attack (120 damage + knockback)
- **Unique Mechanic:** Uses player's own moves (Quick Boost, Drift)
- **Phase Transition:** <500 HP remaining

**Phase 3: Berserk**
- **Behavior:** Mixes ranged and melee unpredictably
- **Attacks:** All previous attacks + new ability
  - **"Crimson Mirror":** Copies player's last attack
- **Increased Speed:** 100 m/s
- **Visual Change:** Mech glows red, thruster trails intensify

**Unique Dialogue:** Boss taunts player throughout fight via comms

**Arena:** Circular debris field with cover (destructible over fight duration)

**Design Intent:** This is a skill check. If player mastered all mechanics, they can win. Boss mirrors player abilities to create a true rival duel.

---

## **10. Art & Audio Direction**

### **10.1 Visual Style Pillars**

#### **Aesthetic Goals:**
1. **Readable:** Player always knows what's happening
2. **Stylish:** Looks like high-budget anime
3. **Performant:** Runs on modest hardware
4. **Iconic:** Distinctive visual identity

#### **Core Art Style: "Clean Anime Realism"**
- **Not:** Pixel art, voxel art, realistic PBR
- **Is:** Cel-shaded, hand-drawn textures, high-contrast lighting
- **References:**
  - *Zone of the Enders 2* (mechanical design)
  - *Gundam Extreme Vs.* (VFX and impact)
  - *DAEMON X MACHINA* (customization and colors)
  - *13 Sentinels* (UI aesthetic)

### **10.2 Character Design: The Crimson Comet**

**Silhouette Requirements:**
- Must be recognizable at 500m distance
- Must read as "heroic protagonist" at a glance
- Must have clear "front" orientation

**Design Elements:**
- **Height:** 18 meters (for sense of scale)
- **Primary Color:** Crimson Red (Pantone 200 C)
- **Accent Color:** Gold (Pantone 871 C)
- **Shape Language:** 
  - Angular/sharp for aggression
  - Broad shoulders for power
  - Streamlined limbs for speed

**Distinctive Features:**
- V-fin antenna (anime mecha tradition)
- Asymmetric shoulder armor (left = shield mount, right = weapon rack)
- Glowing cyan mono-eye visor
- Long flowing thruster "skirt" panels (visual flair during movement)

**Poly Count Target:** 8,000-12,000 triangles (low-poly modern)

### **10.3 Enemy Design Language**

**Visual Differentiation:**
- **Grunt:** Boxy, utilitarian, dull gray
- **Sniper:** Thin, elongated, dark blue with scope lens
- **Brawler:** Bulky, heavy armor, orange accents
- **Heavy:** Massive, turret-like, olive drab
- **Support:** Small, circular, white with red cross

**Enemy Color Coding:**
- **Standard Enemy:** Gray/Blue palette
- **Elite Variant:** Red highlights
- **Boss:** Unique color (black with purple)

**Design Rule:** Never make enemies more visually interesting than the player's mech.

### **10.4 Environment Art**

**Level Themes:**

**Theme 1: Asteroid Field**
- **Palette:** Grays, browns, deep space blacks
- **Assets:** 
  - 5 unique asteroid models (various sizes)
  - Procedural scatter for filler rocks
- **Lighting:** Single distant sun (harsh directional light)

**Theme 2: Ship Graveyard**
- **Palette:** Rusted metals, faded paint, exposed wiring
- **Assets:**
  - Broken battleship sections (bridge, hull, engines)
  - Floating debris (panels, girders, containers)
- **Lighting:** Ambient space light + flickering emergency lights

**Theme 3: Planetary Rings**
- **Palette:** Ice blues, cosmic purples, starlight whites
- **Assets:**
  - Ice chunks with translucent shader
  - Gas clouds (volumetric fog)
- **Lighting:** Planetary glow from below + rim lighting

**Optimization Technique:**
- Heavy use of texture atlases
- Static batching for non-moving objects
- LOD system (3 levels: High/Med/Low)
- Aggressive occlusion culling

### **10.5 Visual Effects (VFX)**

**VFX Philosophy:** "Readable Spectacle"
- Effects must be beautiful but never obscure gameplay information
- Every effect has a clear start, peak, and end
- Color-coding for clarity (blue = friendly, red = enemy)

**Critical VFX Assets:**

**Thruster Effects:**
- **Primary Boost:** Long blue ribbon trail (3-second fade)
- **Quick Boost:** Bright white flash + short trail burst
- **Drift Mode:** Yellow-orange spiral trail

**Weapon VFX:**
- **Beam Rifle:** Cyan laser beam (ray cast visualization)
- **Rifle Grenade:** Orange projectile trail + sphere explosion
- **Beam Saber:** Magenta blade glow + slash arc particle
- **Axe Throw:** Spinning energy trail (helix pattern)

**Impact VFX:**
- **Bullet Hits:** Small spark burst
- **Explosion:** Large sphere burst with outward particle ring
- **Shield Block:** Hexagonal barrier ripple effect
- **Melee Impact:** Screen shake + large spark shower

**Environmental VFX:**
- **Debris Destruction:** Chunks fly outward with smoke trail
- **Engine Damage:** Fire particles + black smoke
- **Death Explosion:** Multi-stage (flash → debris → shockwave)

**VFX Budget:** Max 2,000 particles on screen (auto-culls oldest)

### **10.6 Cel-Shading Implementation**

**Shader Requirements:**
- **Toon Ramp:** 3-step shading (bright, mid, shadow)
- **Rim Lighting:** Bright outline when backlit
- **Outlines:** 2-pixel black lines on all models
- **Emission:** Support for glowing elements (eyes, thrusters, weapons)

**Technical Implementation (URP Shader Graph):**
```
1. Calculate Lambert lighting
2. Posterize to 3 discrete values
3. Add Fresnel rim light
4. Edge detection for outlines (inverted hull method)
5. Add emissive areas (unaffected by lighting)
```

**Performance Target:** <2ms per frame for all shading

### **10.7 Audio Design**

#### **Sound Effects Pillars:**
1. **Impactful:** Every action has weight
2. **Informative:** Audio conveys gameplay information
3. **Stylized:** Anime-inspired, not realistic

**Critical SFX Assets:**

**Movement Sounds:**
- **Thruster Idle:** Low rumble (looping)
- **Primary Boost:** Deep roar increasing in pitch with speed
- **Quick Boost:** Sharp "WHOOSH-HISS" (0.5s)
- **Drift Engage:** Mechanical "click-whirr" + wind sound shift

**Weapon Sounds:**
- **Beam Rifle:** "VWEEM" (sci-fi laser)
- **Rifle Grenade:** "THOOMP" (launch) + "BOOM" (explosion)
- **Beam Saber:** "VWOOM" (ignition) + "SLASH" (swing) + "SHING" (impact)
- **Axe Throw:** "WHHIRRR" (spinning through air) + "CHUNK" (impact)

**Combat Feedback:**
- **Hit Confirm:** Distinct "thunk" on successful hit
- **Shield Impact:** Metallic "CLANG" + energy crackle
- **Low Health Warning:** Pulsing alarm beep (gradual tempo increase)
- **Boost Empty:** Warning buzz
- **Enemy Death:** Satisfying explosion with bass drop

**UI Sounds:**
- **Menu Navigate:** Soft beep
- **Menu Select:** Firm "click"
- **Mission Start:** Dramatic "whoosh"
- **Rank Display:** Ascending tone (higher for better rank)

**Voice/Comms:**
- **Operator:** Female voice, calm and professional
- **Enemy Chatter:** Synthesized/robotic (minimal dialogue)
- **Boss:** Rival pilot, confident and taunting
- **Player Character:** Silent (player IS the character)

#### **Music System**

**Dynamic Music Implementation:**
- **Exploration State:** Ambient synth pads, slow tempo (80 BPM)
- **Combat State:** Electronic drums kick in, tempo increases (140 BPM)
- **Boss State:** Full orchestral + electronic hybrid, intense (160 BPM)
- **Victory State:** Triumphant resolution (30-second sting)

**Track Structure (Layered System):**
```
Layer 1: Bass + Drums (always playing, low volume in exploration)
Layer 2: Melody (fades in during combat)
Layer 3: Harmony/Strings (adds during intense moments)
Layer 4: Brass/Lead (boss fights only)
```

**Transitions:** Smooth crossfades (2-second blend between states)

**Inspiration References:**
- *Ace Combat* series (epic orchestral moments)
- *Armored Core* series (industrial electronic)
- *Furi* (synthwave intensity)
- *Nier: Automata* (emotional weight)

**Music Budget:** 
- 8-10 unique tracks (2-3 minutes each)
- Total: 20-25 minutes of original music
- Consider commissioning or royalty-free libraries

---

## **11. Technical Overview**

### **11.1 Engine & Tools**

**Game Engine:** Unity 6 (2023 LTS if stability issues)  
**Render Pipeline:** Universal Render Pipeline (URP)  
**Scripting Language:** C#  
**Version Control:** Git (GitHub or GitLab)  
**Project Management:** Trello, Notion, or HacknPlan

**External Tools:**
- **3D Modeling:** Blender 3.6+
- **Texturing:** Substance Painter or Photoshop
- **Audio:** Audacity (editing), FMOD/Wwise (implementation)
- **VFX:** Unity VFX Graph or Particle System

### **11.2 Performance Targets**

#### **Minimum Specifications:**
- **CPU:** Intel i5-6600 / AMD Ryzen 5 1600
- **GPU:** NVIDIA GTX 1060 6GB / AMD RX 580 8GB
- **RAM:** 8GB
- **Storage:** 2GB available space
- **Target:** 1080p @ 60fps (stable)

#### **Recommended Specifications:**
- **CPU:** Intel i5-9600K / AMD Ryzen 5 3600
- **GPU:** NVIDIA RTX 3060 / AMD RX 6700
- **RAM:** 16GB
- **Storage:** 2GB SSD
- **Target:** 1440p @ 90fps+

#### **Performance Non-Negotiables:**
- ✅ Must maintain 60fps minimum at all times
- ✅ Frame pacing must be consistent (no stutters)
- ✅ Loading times <10 seconds between missions
- ❌ Will sacrifice visual fidelity over framerate stability

### **11.3 Technical Architecture**

#### **Core Systems:**

**1. Player Controller System**
```
PlayerController.cs (main controller)
├── MovementModule.cs (physics & thrust)
├── BoostModule.cs (boost gauge management)
├── DriftModule.cs (momentum preservation)
└── InputHandler.cs (input processing)
```

**2. Weapon System**
```
WeaponManager.cs (stance & firing)
├── WeaponData.cs (ScriptableObject definitions)
├── ProjectilePool.cs (object pooling)
├── MeleeHitbox.cs (melee detection)
└── TargetingSystem.cs (lock-on logic)
```

**3. Enemy AI System**
```
EnemyAI.cs (base class)
├── StateIdle.cs
├── StateChase.cs
├── StateAttack.cs
└── StateRetreat.cs (if applicable)

EnemyTypes:
├── GruntAI.cs (inherits EnemyAI)
├── SniperAI.cs
└── BrawlerAI.cs
```

**4. Mission Management**
```
MissionManager.cs (level controller)
├── WaveSpawner.cs (enemy instantiation)
├── ObjectiveTracker.cs (mission goals)
├── RankingSystem.cs (score calculation)
└── MissionData.cs (ScriptableObject definitions)
```

**5. Save System**
```
SaveManager.cs
├── PlayerProgress.cs (unlocks, completion)
├── SettingsData.cs (audio, graphics, controls)
└── Serialization via JSON
```

### **11.4 Data-Driven Design**

**ScriptableObjects (Critical for Iteration Speed):**

**WeaponData.SO:**
```csharp
[CreateAssetMenu]
public class WeaponData : ScriptableObject {
    public string weaponName;
    public int damage;
    public float fireRate;
    public GameObject projectilePrefab;
    public AudioClip fireSound;
    public ParticleSystem muzzleFlash;
    public float boostCost;
    // etc.
}
```

**EnemyData.SO:**
```csharp
[CreateAssetMenu]
public class EnemyData : ScriptableObject {
    public string enemyName;
    public int maxHealth;
    public float moveSpeed;
    public float attackRange;
    public float attackDamage;
    public GameObject modelPrefab;
    public AIBehaviorType behaviorType;
    // etc.
}
```

**MissionData.SO:**
```csharp
[CreateAssetMenu]
public class MissionData : ScriptableObject {
    public string missionName;
    public string briefingText;
    public SceneReference levelScene;
    public List<WaveData> enemyWaves;
    public List<UnlockData> rewards;
    public float timeForSRank;
    // etc.
}
```

**Benefit:** Designers can create new weapons, enemies, and missions entirely in the Unity editor without touching code.

### **11.5 Object Pooling**

**Why:** Instantiating/destroying objects every frame kills performance

**Pooled Objects:**
- Projectiles (beam shots, grenades, explosions)
- VFX particles (impacts, trails, flashes)
- Audio sources (for overlapping sounds)
- Damage numbers (if enabled)

**Pool Sizes:**
- Projectiles: 100
- VFX: 50
- Audio: 20

### **11.6 Save System**

**Save File Contents:**
```json
{
  "playerProgress": {
    "completedMissions": [1, 2, 3],
    "unlockedWeapons": ["beamRifle", "bazooka"],
    "unlockedParts": ["boostCapacitor"],
    "unlockedColors": ["crimson", "stealthBlack"],
    "missionRanks": {"mission1": "S", "mission2": "A"}
  },
  "settings": {
    "masterVolume": 0.8,
    "musicVolume": 0.6,
    "sfxVolume": 1.0,
    "graphicsQuality": "High",
    "controlScheme": "default"
  },
  "statistics": {
    "totalPlayTime": 7200,
    "totalKills": 453,
    "favoriteWeapon": "beamSaber"
  }
}
```

**Save Triggers:**
- Auto-save after mission complete
- Manual save in hangar
- Settings changes save immediately

**Backup System:**
- Keep last 3 save files
- Cloud sync via Steam Cloud (if using Steamworks)

### **11.7 Optimization Strategies**

**CPU Optimization:**
- Use object pooling for frequently spawned objects
- Cache GetComponent calls in Awake()
- Use FixedUpdate() only for physics, Update() for logic
- Spatial partitioning for enemy AI (only update enemies near player)
- Coroutines for delayed actions (not Update() checks)

**GPU Optimization:**
- Static batching for environment objects
- GPU instancing for repeated meshes (asteroids)
- LOD system (3 levels minimum)
- Occlusion culling (pre-bake)
- Texture atlases to reduce draw calls
- Limit particle count (2000 max on screen)

**Memory Optimization:**
- Compress textures (BC7 for color, BC4 for grayscale)
- Audio compression (Vorbis for music, ADPCM for SFX)
- Unload unused assets between missions
- Addressables for level streaming

### **11.8 Build Pipeline**

**Development Builds:**
- Debug symbols enabled
- Profiler active
- Cheat console available

**Release Builds:**
- Strip debug symbols
- Enable IL2CPP for better performance
- Compress assets (LZ4 for faster loading)
- Code obfuscation (optional, for anti-cheat)

**Platform-Specific:**
- **Windows:** DirectX 11 (primary), DirectX 12 (optional)
- **Steam Deck:** Special control preset, 720p/40fps target

---

## **12. User Interface & UX**

### **12.1 HUD Design Philosophy**

**Principles:**
- **Minimal:** Only show critical information
- **Contextual:** Elements appear only when needed
- **Non-Intrusive:** Never blocks gameplay view
- **Readable:** Clear at a glance during intense action

### **12.2 In-Game HUD Elements**

#### **Permanent Elements:**

**1. Boost Gauge (Bottom Center)**
- Horizontal bar, 300px wide
- Color: Blue (full) → Orange (low) → Red (empty)
- Position: 10% from bottom, centered
- Animated: Smooth fill/drain

**2. Health Bar (Bottom Left)**
- Horizontal bar, 200px wide
- Color: Green (high) → Yellow (medium) → Red (low)
- Shows numerical value (e.g., "150/200")
- Flashes red when taking damage

**3. Weapon Indicator (Bottom Right)**
- Shows current stance (Ranger/Assault)
- Shows equipped weapons (icons)
- Ammo count (for limited-ammo weapons)
- Cooldown overlays (for grenades, etc.)

**4. Reticle (Screen Center)**
- Dynamic crosshair
- Changes color when over enemy (white → red)
- Expands when firing (shows spread/recoil)
- Contracts when locked-on

#### **Contextual Elements:**

**5. Lock-On Indicator**
- Appears only when target locked
- Shows enemy name and health bar
- Distance to target
- Lead indicator (shows where to aim for moving target)

**6. Damage Numbers (Optional)**
- Float upward from hit location
- Color-coded: White (normal), Yellow (weak point), Red (critical)
- Fades out after 1 second
- Can be disabled in settings

**7. Warning Indicators**
- Red arrow at screen edge pointing toward incoming fire
- "CAUTION" text when low health
- "BOOST DEPLETED" warning flash

**8. Objective Marker**
- Waypoint showing mission objective location
- Distance displayed
- Auto-hides when within 50m

### **12.3 Menu Systems**

#### **Main Menu**
```
┌─────────────────────────────┐
│   CRIMSON COMET            │
│                             │
│   > Start Campaign          │
│     Continue                │
│     Mission Select          │
│     Hangar                  │
│     Settings                │
│     Credits                 │
│     Quit                    │
│                             │
│   [Background: Animated     │
│    3D mech rotating]        │
└─────────────────────────────┘
```

#### **Hangar (Loadout Screen)**
```
┌─────────────────────────────┐
│  HANGAR                     │
│                             │
│  [3D Model Preview]         │
│                             │
│  Ranger Stance:             │
│  ├─ Primary: [Beam Rifle ▼]│
│  └─ Secondary: [Grenade  ▼]│
│                             │
│  Assault Stance:            │
│  ├─ Primary: [Beam Saber ▼]│
│  └─ Secondary: [Axe      ▼]│
│                             │
│  Parts: [3 Slots]           │
│  Color: [Crimson Red     ▼]│
│                             │
│  [Launch Mission]           │
└─────────────────────────────┘
```

#### **Mission Briefing**
```
┌─────────────────────────────┐
│  MISSION 03: SILENT APPROACH│
│                             │
│  [Static image of level]    │
│                             │
│  OBJECTIVE:                 │
│  Infiltrate enemy comm      │
│  station and destroy the    │
│  primary antenna array.     │
│                             │
│  OPERATOR:                  │
│  "This is a stealth mission.│
│   Avoid detection if you    │
│   can. Good luck, Comet."   │
│                             │
│  Difficulty: [Normal    ▼]  │
│                             │
│  [Begin Mission]            │
└─────────────────────────────┘
```

#### **Pause Menu (In-Mission)**
```
┌─────────────────────────────┐
│   PAUSED                    │
│                             │
│   > Resume                  │
│     Restart Mission         │
│     Settings                │
│     Abandon Mission         │
│                             │
└─────────────────────────────┘
```

#### **Mission Complete Screen**
```
┌─────────────────────────────┐
│   MISSION COMPLETE!         │
│                             │
│   Rank: [S]                 │
│                             │
│   Time: 05:42 / 06:00       │
│   Accuracy: 87%             │
│   Enemies Defeated: 24      │
│   Deaths: 0                 │
│                             │
│   Total Score: 8,450        │
│                             │
│   UNLOCKED:                 │
│   [Icon] Heavy Bazooka      │
│                             │
│   [Continue]                │
└─────────────────────────────┘
```

### **12.4 Settings Menu Structure**

#### **Graphics Settings**
- Quality Preset: Low / Medium / High / Ultra / Custom
- Resolution: [Dropdown of supported resolutions]
- Fullscreen Mode: Fullscreen / Borderless / Windowed
- VSync: On / Off
- Frame Rate Limit: 30 / 60 / 90 / 120 / Unlimited
- Texture Quality: Low / Medium / High
- Shadow Quality: Low / Medium / High / Off
- Particle Density: 50% / 75% / 100%
- Anti-Aliasing: Off / FXAA / TAA
- Motion Blur: On / Off
- Depth of Field: On / Off

#### **Audio Settings**
- Master Volume: 0-100%
- Music Volume: 0-100%
- SFX Volume: 0-100%
- Voice Volume: 0-100%
- Audio Output Device: [Dropdown]

#### **Gameplay Settings**
- Difficulty: Easy / Normal / Hard
- Damage Numbers: On / Off
- Screen Shake: 0-100%
- Camera Shake: 0-100%
- Auto-Save: On / Off
- Objective Markers: On / Off

#### **Controls Settings**
- Control Scheme: Gamepad / Keyboard+Mouse
- Remap All Buttons: [Individual button mapping]
- Look Sensitivity X: 1-10
- Look Sensitivity Y: 1-10
- Invert Y-Axis: On / Off
- Toggle vs Hold:
  - Boost: Toggle / Hold
  - Drift: Toggle / Hold
  - Shield: Toggle / Hold
- Deadzone: 0-30%
- Vibration: On / Off

#### **Accessibility Settings**
- Colorblind Mode: None / Deuteranopia / Protanopia / Tritanopia
- HUD Scale: 80% / 100% / 120% / 140%
- Subtitle Size: Small / Medium / Large
- High Contrast Mode: On / Off
- Reduced Motion: On / Off (reduces camera shake, motion blur)

---

## **13. Accessibility & Settings**

### **13.1 Accessibility Philosophy**

**Goal:** Make CRIMSON COMET playable for as many people as possible without compromising core design.

**Principles:**
1. Options, not mandates (players choose what helps them)
2. No "easy mode" stigma (accessibility ≠ difficulty)
3. Clear communication (explain what each option does)

### **13.2 Visual Accessibility**

**Colorblind Support:**
- **Deuteranopia Mode:** Adjust red/green to orange/blue
- **Protanopia Mode:** Similar to Deuteranopia
- **Tritanopia Mode:** Adjust blue/yellow to pink/teal
- **Implementation:** Shader-based color grading filter

**High Contrast Mode:**
- Increases outline thickness on enemies (2px → 4px)
- Brightens HUD elements
- Adds colored backgrounds to text

**Motion Sensitivity Options:**
- Reduce FOV (90° → 75°) to minimize peripheral motion
- Disable motion blur entirely
- Reduce screen shake intensity
- Slow camera acceleration

**HUD Customization:**
- Scale all UI elements (80%-140%)
- Change HUD opacity (60%-100%)
- Reposition HUD elements (preset layouts)

### **13.3 Auditory Accessibility**

**Visual Audio Cues:**
- Optional indicator shows direction of sounds
- Important audio events have screen-space icons
- Subtitles for all dialogue (with speaker labels)

**Audio Balance:**
- Independent volume sliders for 4 categories
- Boost important gameplay sounds over ambient

### **13.4 Motor Accessibility**

**Control Simplification:**
- **Auto-Fire Mode:** Hold R1 instead of tapping
- **Aim Assist:** Sticky reticle (slight slowdown over enemies)
- **Toggle Options:** Convert all "hold" inputs to "toggle"

**Alternative Control Schemes:**
- **Single-Hand Preset:** All essential actions on one side of controller
- **Foot Pedal Support:** Map boost/fire to external USB pedals (via Steam Input)

**Reduced Input Complexity:**
- **Simple Mode:** Removes stance switching (auto-switches based on range)
- **Auto-Dodge:** Quick Boost automatically activates when projectile is about to hit (3-second cooldown)

### **13.5 Cognitive Accessibility**

**Difficulty Adjustments:**
- Enemy HP/damage scaling (covered in Section 8.5)
- Infinite boost mode (for practice/exploration)
- Invincibility mode (for story focus)

**Information Clarity:**
- Tutorial replays available in pause menu
- Mission objectives always visible on HUD
- Clear, concise UI text (no jargon)

**Reduced Time Pressure:**
- Option to disable mission timers
- Slow-motion toggle (50% speed, practice mode only)

---

## **14. Story & Setting**

### **14.1 Narrative Philosophy**

**Story Role:** The narrative is a **support structure** for gameplay, not the focus.

**Design Goals:**
- Provide context for why we're fighting
- Create memorable moments between missions
- Allow player to project themselves onto protagonist
- Never interrupt flow with long cutscenes

**Inspiration:**
- *Ace Combat* (personal stakes in big war)
- *Zone of the Enders* (mysterious protagonist, legendary machine)
- *Armored Core* (mercenary perspective, moral ambiguity)

### **14.2 Setting: "The Graveyard of Giants"**

**Location:** The Outer Reaches, a solar system at the edge of colonized space

**Historical Context:**
50 years ago, this system was a strategic chokepoint in a massive interstellar war between two superpowers: The Terran Coalition and The Frontier Alliance. The war culminated in the "Battle of the Ashfield," a week-long orbital siege that saw thousands of capital ships and mobile armors destroyed.

The war ended in mutual exhaustion. Both sides withdrew, leaving behind a massive debris field of wrecked ships, shattered stations, and dead machines. This became known as "The Graveyard of Giants."

**Present Day:**
Small independent factions, refugees, and scavengers now inhabit the ruins. One such faction is the **Crimson Dawn Militia**, a ragtag group of former soldiers and civilians trying to survive and maintain independence.

A new threat has emerged: The **Obsidian Fleet**, remnants of the Frontier Alliance who reject the peace treaty and seek to restart the war by controlling the resource-rich Ashfield.

### **14.3 Characters**

#### **The Player: "Comet" (Callsign Only)**

**Identity:** Never shown, never named beyond callsign  
**Voice:** Silent (player IS the character)  
**Background:** Unknown (intentionally ambiguous)

**Reputation:**
- Known as the pilot of the legendary "Crimson Comet"
- The Crimson Comet is a prototype machine from the old war, recently reactivated
- Rumors suggest the machine chooses its pilot (anime trope)

**Personality:**
- Defined by player actions (aggressive vs. tactical playstyle)
- Loyal to Crimson Dawn Militia
- Professional, skilled, respected

#### **Operator: "Aria" (Voice Only)**

**Role:** Mission control, provides briefings and real-time tactical updates  
**Personality:** Calm, professional, dry humor  
**Relationship:** Trusts Comet implicitly, serves as emotional anchor  
**Voice Direction:** "Competent AI assistant meets protective older sister"

**Sample Lines:**
- "Comet, this is Aria. I'm reading multiple bogeys on approach. You know what to do."
- "Nice work out there. The refugees made it out safely thanks to you."
- "Careful, Comet. That's not a standard unit—it's an ace."

#### **Commander Valis (Crimson Dawn Leader)**

**Role:** Appears in briefings, gives strategic context  
**Personality:** Weary veteran, pragmatic, protective of his people  
**Conflict:** Knows sending Comet into danger repeatedly, but has no choice

**Sample Lines:**
- "We're outgunned, outmanned, and out of options. But we have you, Comet."
- "This mission is critical. If we lose that supply run, we lose everything."

#### **Antagonist: "Raven" (Obsidian Fleet Ace)**

**Role:** Rival pilot, mirrors player's skills  
**Machine:** "Obsidian Hawk" - black and purple mech  
**Personality:** Confident, theatrical, obsessed with finding worthy opponents  
**Motivation:** Believes the war's end was a mistake, seeks to prove Frontier Alliance superiority

**Sample Lines:**
- "So, you're the famous Crimson Comet. Let's see if the legend lives up to the hype."
- "Impressive! But this is where your luck runs out!"
- (After defeat) "This...isn't...over..."

### **14.4 Story Structure**

#### **Three-Act Structure:**

**Act 1: Awakening (Missions 1-3)**
- Establish setting and stakes
- Introduce Crimson Dawn's dire situation
- First encounter with Obsidian Fleet
- Climax: Successfully disrupt enemy communications

**Act 2: Escalation (Missions 4-5)**
- Obsidian Fleet retaliates with full force
- Crimson Dawn on defensive
- Heavy losses among allies
- Encounter with Raven (first duel, interrupted)
- Climax: Assault on enemy carrier (trap)

**Act 3: Resolution (Missions 6-7/8)**
- Comet must escape and regroup
- Final assault on Obsidian stronghold
- Confrontation with Raven (full battle)
- Ending: Open-ended (Obsidian Fleet defeated, but war continues elsewhere)

### **14.5 Environmental Storytelling**

**Show, Don't Tell:**
- Broken refugee ships fleeing battle
- Old war wrecks with faded insignias
- Distress beacons on empty stations
- Crew logs found in debris (optional collectibles)

**Level Design Narrative:**
- Early missions: Sparse debris, isolated skirmishes
- Mid missions: Dense battlefields, active war zones
- Late missions: Enemy strongholds, imposing architecture

### **14.6 Dialogue Delivery**

**No Cutscenes (Except Mission 1 Intro & Final Ending):**
- All story delivered through:
  - Mission briefing text screens
  - In-mission radio chatter (plays while flying)
  - Environmental context

**Voice Acting:**
- **Aria:** ~200 lines (mission control)
- **Valis:** ~50 lines (briefings)
- **Raven:** ~30 lines (boss fight taunts)
- **Enemy Chatter:** ~50 lines (generic callouts)

**Total VO Budget:** ~330 lines, estimate $2000-3000 for professional VO

### **14.7 Optional Lore (For Interested Players)**

**Codex Entries (Text-Based, Accessed from Menu):**
- History of the Great War
- Technical specifications of machines
- Biographies of key figures
- Faction backgrounds

**Not Required for Enjoyment:** Gameplay comes first, lore is a bonus.

---

## **15. Monetization & Business Model**

### **15.1 Pricing Strategy**

**Base Price:** $14.99 USD

**Justification:**
- Indie game price point
- 6-8 hour campaign (competitive for price)
- High replayability (mission rankings)
- No microtransactions or DLC at launch

**Regional Pricing:**
- Use Steam's recommended regional pricing
- Ensure affordability in emerging markets

### **15.2 No Microtransactions**

**Philosophy:** Premium, complete experience

**What's Included in Base Purchase:**
- Full campaign
- All weapons and parts (unlocked through play)
- All cosmetics (earned through achievements)
- Future bug fixes and patches (free)

**No:**
- ❌ Loot boxes
- ❌ Battle passes
- ❌ Cosmetic shops
- ❌ Pay-to-win mechanics
- ❌ "Deluxe" editions

### **15.3 Post-Launch DLC (Optional, If Successful)**

**Potential DLC Content (6-12 Months Post-Launch):**

**DLC 1: "Frontier Stories" - $4.99**
- 3 new story missions
- 1 new playable mech (different handling characteristics)
- 2 new weapons
- New boss encounter

**DLC 2: "Arena Mode" - $2.99**
- Endless survival mode
- Leaderboards
- New arena maps
- Cosmetic rewards

**Free Updates (Goodwill Gestures):**
- Quality of life improvements
- New color schemes
- Time attack mode
- Photo mode

**DLC Philosophy:**
- Only if base game is profitable
- Must provide substantial value
- Never split community (no multiplayer paywall)

### **15.4 Revenue Projections**

**Conservative Estimates:**

**Scenario 1: Modest Success**
- 500 units sold @ $14.99 = $7,495
- Steam cut (30%) = -$2,248
- Net revenue = $5,247

**Scenario 2: Moderate Success**
- 2,000 units sold @ $14.99 = $29,980
- Steam cut (30%) = -$8,994
- Net revenue = $20,986

**Scenario 3: Breakout Success**
- 10,000 units sold @ $14.99 = $149,900
- Steam cut (30%) = -$44,970
- Net revenue = $104,930

**Break-Even Target:**
- Assuming $2,000 development costs (software, assets, marketing)
- Need 200 sales to break even
- Realistic target: 500-2,000 sales in first year

### **15.5 Marketing Budget**

**Total Budget:** $500 (shoestring indie)

**Allocation:**
- **Trailer Production:** $200 (editing software, music license)
- **Press Kit Materials:** $50 (website hosting)
- **Influencer Keys:** $0 (free game codes, 20-30 keys)
- **Paid Ads:** $100 (Reddit, Twitter promoted posts)
- **Steam Visibility:** $100 (participating in Steam events)
- **Contingency:** $50

**Free Marketing Strategies:**
- Regular dev log posts (Reddit, Twitter, TikTok)
- GIF showcases of cool gameplay moments
- Participate in #screenshotsaturday
- Submit to indie game festivals (free or low-cost)
- Build wishlist count before launch (aim for 1,000+)

---

## **16. Budget & Resources**

### **16.1 Development Costs (Solo/Small Team)**

**Software Licenses:**
- Unity (Personal): **$0** (free for revenue <$100k)
- Blender: **$0** (open source)
- GIMP/Krita: **$0** (open source)
- Audacity: **$0** (open source)
- **Total: $0**

**Asset Purchases:**
- Sound Effects Pack: **$100**
- Music Commission (or royalty-free): **$200**
- Font Licenses: **$20**
- **Total: $320**

**Voice Acting:**
- Professional VO: **$2,000** (if budget allows)
- OR Community actors: **$0-200** (rev-share or small fee)

**Marketing:**
- As detailed in Section 15.5: **$500**

**Platform Fees:**
- Steam Direct Fee: **$100** (one-time, recoupable)

**Miscellaneous:**
- External playtesting incentives: **$100**
- Contingency: **$200**

**Total Estimated Budget:**
- **Minimum:** $820 (using free VO)
- **Target:** $3,220 (with pro VO)

### **16.2 Time Investment (Solo Developer)**

**Total Development Time:** 14 months (~1,700 hours)

**Hourly Breakdown:**
- Phase 0 (Pre-Production): 80 hours
- Phase 1 (Gray Box): 160 hours
- Phase 2 (Combat): 160 hours
- Phase 3 (Vertical Slice): 240 hours
- Phase 4 (Full Production): 640 hours
- Phase 5 (Beta): 320 hours
- Phase 6 (Launch): 100 hours

**If Working Part-Time (20 hours/week):**
- 85 weeks = ~20 months

**If Working Full-Time (40 hours/week):**
- 42.5 weeks = ~10 months

### **16.3 Team Roles (If Expanding)**

**Solo Developer:**
- Must handle all roles (programmer, artist, designer, audio)
- Timeline × 1.5 multiplier
- Focus on strengths, outsource weaknesses

**2-Person Team (Ideal):**
- **Role A:** Programmer + Game Designer
  - All systems implementation
  - Combat tuning
  - Mission scripting
- **Role B:** 3D Artist + Level Designer
  - All modeling and texturing
  - Environment creation
  - VFX creation
- **Both:** Share audio duties or outsource

**3-Person Team:**
- **Programmer:** Pure systems work
- **Artist:** Pure visual work
- **Designer:** Level design, balance, audio

**Outsourcing Opportunities:**
- Music composition (commission 1-2 tracks for $200-500)
- Voice acting (online casting for $50-200 per role)
- Marketing trailer editing (Fiverr for $100-200)

### **16.4 Revenue Sharing (If Team)**

**Equity Split Options:**

**Equal Partnership (2-person):**
- 50/50 split after recouping shared expenses
- Clear contract upfront

**Role-Based (3-person):**
- Lead Developer: 40%
- Artist: 30%
- Designer: 30%

**Hybrid (Contractor + Core):**
- Core team: 80% split among members
- Contractors: Flat fee upfront (no revenue share)

**Legal Protection:**
- Written agreement before starting
- Define IP ownership clearly
- Exit clauses if someone leaves mid-project

---

## **17. Scope Management**

### **17.1 The "Minimum Viable Product" (MVP)**

**Non-Negotiable Core (Must Ship With):**
- ✅ Fluid Momentum movement system (fully polished)
- ✅ 6 completable missions
- ✅ 3 enemy types (Grunt, Sniper, Brawler)
- ✅ 1 boss fight
- ✅ 3 weapon sets (starting + 2 unlocks)
- ✅ Basic progression (weapon unlocks)
- ✅ Mission ranking system
- ✅ Stable 60fps on minimum spec

**Can Be Cut If Behind Schedule:**
- ❌ Missions 7-8 (reduce to 6-mission campaign)
- ❌ Heavy and Support enemy types
- ❌ Performance parts system (keep cosmetics only)
- ❌ Arena/Challenge modes
- ❌ Full voice acting (use text only)
- ❌ Some weapon varieties

### **17.2 Scope Cut Decision Points**

**End of Phase 2 Review:**
- **Question:** Is combat fun after 20 hours of playtesting?
- **If NO:** Extend Phase 2 by 2 weeks, cut 2 missions from Phase 4
- **If YES:** Proceed as planned

**End of Phase 3 Review:**
- **Question:** Does the vertical slice feel AAA-quality?
- **If NO:** 
  - Cut visual complexity (simpler shaders, fewer particles)
  - Cut 2 missions from campaign
  - Remove performance parts system
- **If YES:** Proceed as planned

**Week 20 of Phase 4 Review:**
- **Question:** Are we on track to finish all 8 missions?
- **If NO:**
  - Reduce to 6 missions (MVP)
  - Remove Heavy/Support enemies
  - Simplify final boss (single phase instead of three)
- **If YES:** Proceed as planned

### **17.3 Feature Priority Matrix**

| Feature | Priority | Effort | Can Cut? |
|---------|----------|--------|----------|
| Fluid Momentum System | CRITICAL | HIGH | NO |
| Basic Combat (Ranger) | CRITICAL | MEDIUM | NO |
| 6 Missions | CRITICAL | HIGH | NO |
| Cel-Shading | HIGH | MEDIUM | NO |
| Assault Stance | HIGH | MEDIUM | Merge with Ranger |
| Mission Rankings | HIGH | LOW | NO |
| Lock-On System | MEDIUM | MEDIUM | Simplify |
| Performance Parts | MEDIUM | MEDIUM | YES |
| 8 Missions | MEDIUM | HIGH | YES (cut to 6) |
| 5 Enemy Types | MEDIUM | HIGH | YES (cut to 3) |
| Voice Acting | LOW | LOW | YES (text only) |
| Arena Mode | LOW | HIGH | YES |
| Photo Mode | LOW | LOW | YES |

### **17.4 The "Cut List" (Pre-Prepared)**

**If 1 Week Behind Schedule:**
- Remove Photo Mode
- Remove Arena Mode
- Simplify tutorial (text prompts instead of interactive)

**If 2 Weeks Behind Schedule:**
- Cut Mission 8
- Remove Heavy and Support enemies
- Remove performance parts system (cosmetics only)

**If 4 Weeks Behind Schedule:**
- Cut Missions 7-8 (6-mission MVP)
- Remove voice acting (text only)
- Merge Ranger and Assault into single stance
- Boss fight reduced to single-phase

**If 6+ Weeks Behind Schedule:**
- **STOP.** Re-evaluate if project is viable.
- Consider Early Access release (3 missions + promise of more)
- Or pivot to "Prototype Demo" (free release to build audience)

### **17.5 Scope Creep Prevention**

**Rules to Avoid Scope Creep:**
1. **No New Features After Phase 3:** Only polish existing systems
2. **"Rule of 3":** Every new idea must replace 3 existing tasks
3. **Weekly Reviews:** Compare progress to timeline, adjust immediately
4. **Feature Freeze:** Phase 5 is bug-fixing ONLY, zero new features

**Common Traps to Avoid:**
- ❌ "Just one more weapon type..."
- ❌ "This enemy type would be cool..."
- ❌ "We should add a story cutscene here..."
- ❌ "What if we added a crafting system..."

**The Mantra:** "Ship a small, polished game > abandon a large, broken one"

---

## **18. Success Metrics**

### **18.1 Development Milestones**

**Phase 1 Success Criteria:**
- [ ] Flying the cube is fun for 10+ minutes without enemies
- [ ] All movement mechanics feel responsive
- [ ] Boost gauge management feels meaningful
- [ ] Can navigate test arena without frustration

**Phase 2 Success Criteria:**
- [ ] Combat loop (engage → attack → evade) feels satisfying
- [ ] Both stances have clear use cases
- [ ] Enemy AI provides challenge without being unfair
- [ ] Can defeat 5 enemies in a row without dying

**Phase 3 Success Criteria:**
- [ ] Vertical slice mission is completable start-to-finish
- [ ] Visual style is cohesive and appealing
- [ ] Performance is stable (60fps minimum)
- [ ] External playtesters say "I want to play more"

**Phase 4 Success Criteria:**
- [ ] All planned missions are playable
- [ ] Progression curve feels satisfying
- [ ] No game-breaking bugs
- [ ] Can complete campaign in one sitting without crashes

**Phase 5 Success Criteria:**
- [ ] All blocker bugs fixed
- [ ] Beta testers can complete campaign
- [ ] Performance is optimized
- [ ] Game is "shippable"

**Phase 6 Success Criteria:**
- [ ] Game is live on Steam
- [ ] No critical bugs in first week
- [ ] Positive player reception (>70% positive reviews)
- [ ] Break-even sales within first month

### **18.2 Quality Benchmarks**

**Technical Quality:**
- Frame rate: 60fps minimum, 90fps average on recommended spec
- Load times: <10 seconds between missions
- Crash rate: <0.1% (less than 1 crash per 1000 play sessions)
- Input latency: <50ms (controller to screen response)

**Gameplay Quality:**
- Mission completion rate: >80% of players finish Mission 1
- Mission retention: >50% of players reach Mission 4
- Average play session: 30-60 minutes
- Replay rate: >30% of players replay missions for better ranks

**Player Satisfaction:**
- Steam reviews: Target >70% positive (Mixed to Mostly Positive)
- Average playtime: 5-8 hours (indicates campaign completion)
- Wishlist conversion: >20% (1 in 5 wishlists become purchases)
- Refund rate: <10% (under Steam's average)

### **18.3 Sales Targets**

**First Week:**
- **Conservative:** 50 sales ($750 revenue)
- **Realistic:** 200 sales ($3,000 revenue)
- **Optimistic:** 500 sales ($7,500 revenue)

**First Month:**
- **Conservative:** 200 sales ($3,000 revenue)
- **Realistic:** 500 sales ($7,500 revenue)
- **Optimistic:** 1,500 sales ($22,500 revenue)

**First Year:**
- **Conservative:** 500 sales ($7,500 revenue)
- **Realistic:** 2,000 sales ($30,000 revenue)
- **Optimistic:** 5,000 sales ($75,000 revenue)

**Success Threshold:**
- Break-even at 200 sales
- "Worth the time" at 1,000+ sales
- "Sustainable indie career" at 5,000+ sales

### **18.4 Community Metrics**

**Pre-Launch:**
- Steam wishlist goal: 1,000+ (indicates healthy interest)
- Discord members: 100+ (small but engaged community)
- Twitter/social followers: 500+ (awareness building)

**Post-Launch:**
- Active players (daily): 50-100 in first month
- Review count: 20+ reviews in first week (builds social proof)
- Content creation: 5-10 YouTube videos or streams
- Discussion threads: Active posts on Steam forums/Reddit

---

## **19. Risk Assessment**

### **19.1 Technical Risks**

**Risk #1: Physics System Instability**
- **Probability:** MEDIUM
- **Impact:** CRITICAL (breaks core gameplay)
- **Mitigation:** 
  - Test rigidbody physics extensively in Phase 0
  - Keep Unity 2022.3 LTS as fallback from Unity 6
  - Prototype alternative movement (transform-based) as backup
- **Contingency:** Pivot to hybrid system (physics for collisions only)

**Risk #2: Performance Issues**
- **Probability:** MEDIUM
- **Impact:** HIGH (kills sales if unoptimized)
- **Mitigation:**
  - Profile regularly throughout development
  - Design for low-end hardware from start
  - Build for target spec hardware frequently
- **Contingency:** Cut visual fidelity, reduce particle count, simplify shaders

**Risk #3: Unity 6 Bugs**
- **Probability:** MEDIUM (new engine version)
- **Impact:** HIGH (could block progress)
- **Mitigation:**
  - Use LTS version instead of latest
  - Follow Unity forums for known issues
  - Keep backups before engine updates
- **Contingency:** Downgrade to Unity 2022.3 LTS

**Risk #4: Control Feel Issues**
- **Probability:** LOW (can be iterated)
- **Impact:** CRITICAL (if controls feel bad, game fails)
- **Mitigation:**
  - Extensive playtesting in Phase 1
  - Get external feedback early
  - Be willing to completely redo if needed
- **Contingency:** Extend Phase 1 by 2-4 weeks for polish

### **19.2 Scope Risks**

**Risk #5: Timeline Overrun**
- **Probability:** HIGH (extremely common in gamedev)
- **Impact:** MEDIUM (delays release, costs money)
- **Mitigation:**
  - Pre-planned cut list (Section 17.4)
  - Weekly progress reviews
  - Realistic time estimates with padding
- **Contingency:** Aggressive scope cuts at predetermined checkpoints

**Risk #6: Feature Creep**
- **Probability:** MEDIUM
- **Impact:** MEDIUM (derails timeline)
- **Mitigation:**
  - Strict feature freeze after Phase 3
  - Document all "sequel ideas" separately
  - "Rule of 3" for new features
- **Contingency:** Ruthlessly cut new features, stick to GDD

**Risk #7: Solo Developer Burnout**
- **Probability:** HIGH (14 months is long)
- **Impact:** CRITICAL (project abandonment)
- **Mitigation:**
  - Schedule regular breaks
  - Work sustainable hours (not crunch)
  - Celebrate milestone completions
  - Maintain support network
- **Contingency:** Take 1-2 week break, return refreshed (extend timeline)

### **19.3 Market Risks**

**Risk #8: Low Sales**
- **Probability:** MEDIUM-HIGH (saturated indie market)
- **Impact:** HIGH (financial loss, demotivation)
- **Mitigation:**
  - Build wishlist count pre-launch (1,000+ goal)
  - Market consistently during development
  - Price competitively ($14.99)
  - Ensure quality exceeds price point
- **Contingency:** Run sales/bundles, consider DLC, move to next project

**Risk #9: Negative Reviews**
- **Probability:** LOW-MEDIUM
- **Impact:** HIGH (kills visibility algorithm)
- **Mitigation:**
  - Extensive beta testing
  - Fix all critical bugs before launch
  - Manage expectations in marketing
  - Respond professionally to feedback
- **Contingency:** Rapid patch deployment, engage with community, improve over time

**Risk #10: Competing Releases**
- **Probability:** MEDIUM
- **Impact:** MEDIUM (splits audience attention)
- **Mitigation:**
  - Monitor release calendars
  - Avoid launching same week as AAA mecha games
  - Carve unique niche (our USPs)
- **Contingency:** Delay launch by 1-2 weeks to avoid competition

### **19.4 Legal Risks**

**Risk #11: IP Infringement**
- **Probability:** LOW (if careful)
- **Impact:** CRITICAL (lawsuit, takedown)
- **Mitigation:**
  - Original designs only (no Gundam copies)
  - Clear inspiration, not imitation
  - Use royalty-free or licensed assets only
  - Avoid trademarked terms
- **Contingency:** Redesign flagged assets immediately

**Risk #12: Asset Licensing Issues**
- **Probability:** LOW
- **Impact:** MEDIUM (must replace assets)
- **Mitigation:**
  - Document all asset sources
  - Use only commercial-use licenses
  - Keep receipts for purchased assets
- **Contingency:** Replace problematic assets with originals

### **19.5 Risk Response Plan**

**If Multiple Risks Trigger:**
1. **Pause and Assess:** Stop adding new features
2. **Triage:** Fix critical issues first
3. **Communicate:** Update community on delays honestly
4. **Cut Scope:** Use pre-prepared cut list
5. **Re-Baseline:** Adjust timeline and expectations

**Project Kill Criteria (When to Stop):**
- Core movement system doesn't feel fun after 8+ weeks
- Technical issues require engine rewrite
- Budget exhausted with <50% completion
- Personal circumstances require indefinite hiatus

**Better to stop early than release a bad game.**

---

## **20. Post-Launch Content**

### **20.1 Immediate Post-Launch (Weeks 1-4)**

**Priority: Stabilization**
- **Week 1:** Critical bug fixes only
  - Game-breaking crashes
  - Progression blockers
  - Major exploits
- **Week 2:** High-priority bugs
  - Performance issues
  - Input problems
  - Balance tweaks
- **Week 3-4:** Quality of life improvements
  - UI adjustments based on feedback
  - Minor balance changes
  - Added control options

**Patches Deployed:**
- Day 1 Patch (if needed)
- Week 1 Hotfix
- Week 2 Stability Update
- Month 1 Quality of Life Patch

### **20.2 Free Updates (Months 2-6)**

**Update 1: "Quality of Life" (Month 2)**
- Photo mode
- Mission practice mode (replay any mission without affecting save)
- Additional color schemes (3-5 new)
- Improved tutorials
- Bug fixes

**Update 2: "Challenge Mode" (Month 4)**
- Time Attack mode (leaderboards via Steam)
- New Game+ (start with all unlocks, harder enemies)
- Survival mode (endless waves)
- New achievements

**Update 3: "Community Feedback" (Month 6)**
- Most-requested features from community
- Final balance pass
- Performance optimizations
- Quality of life features

### **20.3 Paid DLC (If Game is Successful)**

**DLC Plan Triggers:**
- >2,000 sales in first 3 months
- >75% positive Steam reviews
- Active community requesting more content
- Developer has bandwidth

**DLC 1: "Echoes of War" ($4.99) - Month 9**
- 3 new story missions
- New playable mech: "Azure Striker"
  - Different handling (faster, less armor)
  - Unique weapon loadout
- 2 new enemy types
- New boss: "Ghost Squadron" (3v1 fight)
- 2 new weapon types
- Development time: 8-10 weeks

**DLC 2: "Arena Champions" ($2.99) - Month 12**
- Standalone arena mode
- 5 unique arena maps
- Boss rush mode
- Cosmetic rewards (skins, decals)
- Online leaderboards
- Development time: 4-6 weeks

**Bundle Deal:** Game + Both DLCs for $19.99 (save $2.97)

### **20.4 Community Engagement**

**Communication Channels:**
- **Steam Forums:** Official support and announcements
- **Discord Server:** Community hub, direct communication
- **Twitter/X:** Development updates, GIFs, marketing
- **Reddit:** r/CRIMSON_COMET (if community grows), r/IndieGaming
- **YouTube:** Patch notes videos, dev diaries

**Community Events:**
- Monthly speedrun competitions (prize: Steam keys for friends)
- Screenshot contests (best action shots)
- Fan art showcases
- Community-voted balance changes

**Developer Presence:**
- Respond to bug reports within 24-48 hours
- Weekly "What are you working on?" updates
- Monthly dev streams (if comfortable)
- Annual "State of the Comet" post

### **20.5 Long-Term Vision (Year 2+)**

**If Game Achieves Breakout Success (5,000+ sales):**

**Option A: Sequel**
- CRIMSON COMET II with expanded scope
- More machines, bigger campaign, multiplayer (?)
- Leverage existing tech and assets
- 18-24 month development

**Option B: Major Expansion**
- $9.99 standalone expansion
- New campaign (8-10 missions)
- Multiple playable mechs
- New mechanics (e.g., squad commands)
- 12-16 month development

**Option C: Spiritual Successor**
- New IP using CRIMSON COMET's engine/systems
- Different setting (fantasy mechs? underwater mechs?)
- Iterate on what worked, fix what didn't

**If Game is Modest Success (500-2,000 sales):**
- Complete planned free updates
- Maybe one small paid DLC
- Move to next project with lessons learned
- Keep CRIMSON COMET maintained (critical bugs only)

**If Game Underperforms (<500 sales):**
- Complete critical bug fixes
- Release final "definitive edition" patch
- Open-source codebase (optional, for learning community)
- Move to next project immediately

### **20.6 Sunset Plan**

**When to Stop Supporting:**
- Active player base drops to <10 daily players
- New bug reports stop coming in
- Developer moves to new project full-time
- Typically 12-24 months post-launch

**Final Actions:**
- Release "Complete Edition" patch with all updates
- Discount game permanently (50% off)
- Archive community channels
- Write post-mortem blog post
- Thank the community

---

## **APPENDICES**

### **Appendix A: Glossary**

**6DoF:** Six Degrees of Freedom - Full 3D movement (translate + rotate on all axes)
**Boost-Cancel:** Using Quick Boost to interrupt animation recovery frames
**Cel-Shading:** Non-photorealistic rendering style that mimics hand-drawn animation
**DPS:** Damage Per Second
**High-G Drift:** The signature mechanic of decoupling orientation from velocity
**Hit-Stop:** Brief game pause on impact to add weight to attacks
**LOD:** Level of Detail - Lower poly models at distance for performance
**MVP:** Minimum Viable Product - Smallest shippable version
**Scope Creep:** Uncontrolled expansion of project features
**URP:** Universal Render Pipeline - Unity's optimized rendering system
**VFX:** Visual Effects (particles, explosions, etc.)

### **Appendix B: Reference Games**

**Core Inspirations:**
1. **Zone of the Enders: The 2nd Runner**
   - Speed, spectacle, melee emphasis
2. **Armored Core Series**
   - Customization depth, boost-canceling
3. **Ace Combat Series**
   - Mission structure, dynamic music
4. **Devil May Cry Series**
   - Combo system, style ranking
5. **Daemon X Machina**
   - Art style, mech power fantasy

**Study for Specific Elements:**
- **Star Fox 64:** Tutorial design, branching paths
- **Vanquish:** Boost-sliding movement
- **Nier: Automata:** Cel-shading implementation
- **Katana Zero:** Hit-stop implementation
- **Furi:** Boss design philosophy

### **Appendix C: Development Tools List**

**Essential:**
- Unity 6 (or 2022.3 LTS)
- Visual Studio Code or Rider (C# IDE)
- Blender 3.6+
- Git + GitHub/GitLab
- GIMP or Krita (texture editing)

**Recommended:**
- Audacity (audio editing)
- OBS Studio (recording gameplay for marketing)
- ShareX (screenshot tool)
- Notion or Trello (project management)

**Optional:**
- Substance Painter (advanced texturing)
- FMOD/Wwise (advanced audio)
- Spine or Live2D (if adding 2D UI elements)

### **Appendix D: Learning Resources**

**Unity Learning:**
- Unity Learn (official tutorials)
- Brackeys (YouTube channel)
- Code Monkey (YouTube channel)

**Game Design:**
- Game Maker's Toolkit (YouTube)
- Extra Credits (YouTube)
- "The Art of Game Design" by Jesse Schell (book)

**3D Modeling:**
- Blender Guru (YouTube)
- Grant Abbitt (YouTube)

**Marketing:**
- "How to Market a Game" by Chris Zukowski (book)
- Game Dev Underground (YouTube)

### **Appendix E: Contact & Credits**

**Project Lead:** [Your Name]
**Email:** [Your Email]
**Social Media:** 
- Twitter: [@YourHandle]
- Discord: [Your Server Invite]

**Special Thanks:**
- Gemini AI (initial GDD draft)
- Claude AI (extended edition & analysis)
- [Beta Testers]
- [Voice Actors]
- [Music Composer]
- [Community Contributors]

### **Appendix F: Version History**

- **v1.0 (Oct 25, 2025):** Initial GDD by Gemini AI
- **v2.0 (Oct 25, 2025):** Extended edition by Claude AI
  - Added 9 major new sections
  - Expanded technical details
  - Added risk assessment and scope management
  - Detailed post-launch plans
  - Added budget and resource planning

---

## **FINAL NOTES**

### **For the Developer:**

This GDD is a **living document**. As you prototype and test, you will discover things that work differently than planned. That's normal and expected. Update this document to reflect reality, not theory.

**Remember:**
1. **Gameplay first, always.** If it's not fun, nothing else matters.
2. **Scope cuts are not failures.** They're smart project management.
3. **Finish and ship.** A completed 6-mission game is infinitely better than an abandoned 10-mission project.
4. **Take care of yourself.** Burnout kills more indie projects than lack of funding.

### **The Most Important Rule:**

**When in doubt, ask yourself: "Does this make the game more fun?"**

If the answer is no, cut it. If the answer is yes, prioritize it. Everything else is secondary.

---

**Good luck, pilot. The Crimson Comet awaits.**

*END OF DOCUMENT*

---

**Document Length:** ~25,000 words  
**Last Updated:** October 25, 2025  
**Next Review:** After Phase 1 Completion