# CRIMSON COMET - Phase 1 Master Tuning Document

## Document Info

- **Date Created:** [Today's date]
- **Last Updated:** [Today's date]
- **Phase:** Week 1 - Core Movement
- **Status:** Ready for Week 2 Review

---

## PLAYER CONTROLLER SETTINGS

### Movement Forces

```
Thrust Force: [VALUE]
Strafe Force: [VALUE]
Vertical Force: [VALUE]
```

**Notes on Movement:**

- Thrust feels: [responsive/sluggish/too fast/perfect]
- Strafe feels: [same options]
- Vertical feels: [same options]

### Boost System

```
Max Boost: 100
Boost Recharge Rate: [VALUE]
Boost Drain Rate: [VALUE]
```

**Boost Economy:**

- Time to fully drain: [X] seconds
- Time to fully recharge: [X] seconds
- Continuous boost duration: [X] seconds

**Notes:**

- Boost feels: [powerful/weak/balanced]
- Resource management: [too generous/too stingy/perfect]

### Rotation

```
Pitch Speed: [VALUE]
Yaw Speed: [VALUE]
```

**Notes:**

- Camera movement: [makes me sick/comfortable/needs adjustment]
- Can do precise aiming: [yes/no/sometimes]

### Speed Limits

```
Max Normal Speed: [VALUE]
Max Boost Speed: [VALUE]
```

**Speed Feel:**

- Normal speed: [too slow/perfect/too fast]
- Boost speed: [barely faster/noticeably faster/way too fast]
- Speed difference feels: [X]% faster

### Input Settings

```
Stick Dead Zone: [VALUE]
Movement Response Curve: [Linear/EaseInOut/Custom]
Look Response Curve: [Linear/EaseInOut/Custom]
```

**Input Feel:**

- Dead zone: [prevents drift perfectly/too much/too little]
- Small movements: [too sensitive/smooth/not sensitive enough]
- Full deflection: [responsive/delayed/perfect]

---

## RIGIDBODY PHYSICS

### Core Settings

```
Mass: 1
Drag: [VALUE]
Angular Drag: [VALUE]
Use Gravity: UNCHECKED
Interpolate: Interpolate
Collision Detection: Continuous
```

**Physics Feel:**

- Momentum: [floaty/weighty/perfect]
- Stopping distance: [too long/too short/good]
- Drift after input: [ice skating/sticky/balanced]

### Collision Settings

```
Player Collision Detection: Continuous
Obstacle Mass: [VALUE]
Obstacle Collision Detection: Discrete
```

**Collision Feel:**

- Bouncing: [too bouncy/dead stop/realistic]
- Getting stuck: [happens often/rare/never]

---

## CAMERA SETTINGS

### Simple Follow Camera

```
Follow Speed: [VALUE]
Rotation Speed: [VALUE]
Camera Distance: 8
Camera Height: 3
Camera Angle: 15°
```

**Camera Feel:**

- Follows: [too slow/laggy/smooth/too tight]
- Rotation: [makes me dizzy/smooth/too slow]
- Visibility: [can see everything/some blind spots/good]

---

## PERFORMANCE METRICS

### Target Hardware

- GPU: GTX 1060 or equivalent
- CPU: Intel i5-6600 / Ryzen 5 1600
- RAM: 8GB
- Target: 60fps stable

### Actual Performance (as of [date])

```
Frame Rate (empty scene): [X] fps
Frame Rate (with obstacles): [X] fps
CPU Usage: [X]%
Memory Usage: [X] MB
Draw Calls: [X]
Particles: [X] active
```

**Performance Notes:**

- Stable 60fps: [yes/no]
- Frame drops: [never/rare/common/constant]
- Loading time: [X] seconds

---

## CHANGE LOG

### [Today's Date]

- Initial tuning complete
- Values locked for Week 1 review
- [List any major changes you made today]

### [Previous dates if any]

- [Previous changes]

---

## KNOWN ISSUES

### Critical (Must Fix Before Week 2)

- [ ] [List any game-breaking bugs]

### High Priority (Should Fix Soon)

- [ ] [List important but not critical issues]

### Low Priority (Nice to Have)

- [ ] [List polish items]

### Not Issues (By Design)

- Movement feels "floaty" - this is space, working as intended
- [Other things that seem wrong but are correct]

---

## PLAYTESTER FEEDBACK

### Internal Testing (You)

**Date:** [Today]
**Duration:** [X] hours total

**What feels AMAZING:**

1. [List best moments]
2. [Best features]
3. [What makes you smile]

**What feels OFF:**

1. [List frustrations]
2. [Confusing elements]
3. [What makes you frown]

**The "10 Minute Test":**
After flying for 10 minutes straight:

- Still having fun? [yes/no]
- Want to keep going? [yes/no]
- Feel like you're improving? [yes/no]

### External Testing (Others)

**Tester 1:** [Name/relationship]
**Date:** [Date]
**Controller Experience:** [Expert/Moderate/Beginner/Never]

**Observations:**

- Time to first successful flight: [X] seconds
- Discovered boost: [yes/no] after [X] minutes
- Hit obstacles: [often/sometimes/rarely]
- Gave up: [yes after X min / no]

**Quotes:**

- "[Exact quote about what they said]"

**Rating (1-10):**

- Fun: [X]/10
- Controls: [X]/10
- Would play more: [X]/10

[Repeat for additional testers]

---

## WEEK 1 REVIEW DECISION

### Success Criteria Checklist

**Functional Requirements:**

- [ ] Player can move in all 6 directions
- [ ] Rotation works on pitch and yaw
- [ ] Boost drains and recharges correctly
- [ ] Speed limiting prevents infinite acceleration
- [ ] Collision doesn't cause stuck states
- [ ] No game-breaking bugs
- [ ] Stable 60fps in test scene

**Feel Requirements:**

- [ ] Controls feel responsive (<50ms perceived lag)
- [ ] Movement feels intentional (not floaty/random)
- [ ] Analog input feels smooth (dead zone working)
- [ ] Camera follows without nausea
- [ ] 10+ minutes of flying stays fun

**Technical Requirements:**

- [ ] Code is clean and commented
- [ ] Settings are documented
- [ ] Git commits are up to date
- [ ] No console errors or warnings

### The GO/NO-GO Question

**Can you honestly answer YES to this:**

> "I would happily spend 30+ hours flying this cube around,
> even with no enemies, no objectives, just pure flight."

**Your answer:** [YES / NO / MAYBE]

**If YES:** ✅ GREEN LIGHT - Proceed to Week 2 (Quick Boost & Drift)

**If MAYBE:** ⚠️ YELLOW LIGHT - Fix specific issues, re-test, then proceed

**If NO:** 🛑 RED LIGHT - Movement needs fundamental rework

### Issues to Fix Before Week 2

1. [List specific problems]
2. [Estimated time to fix each]
3. [Which are blockers vs. nice-to-haves]

---

## NEXT STEPS

**If GREEN LIGHT:**

- [ ] Commit all changes
- [ ] Tag as "Week1-Complete"
- [ ] Take 1-2 day break
- [ ] Begin Week 2 with fresh energy

**If YELLOW LIGHT:**

- [ ] Create focused fix list
- [ ] Allocate 4-8 hours for fixes
- [ ] Re-test after fixes
- [ ] Make final decision

**If RED LIGHT:**

- [ ] Identify core problem (physics? input? design?)
- [ ] Research solutions
- [ ] Consider extending Week 1 by 1 week
- [ ] Be willing to experiment with radical changes

---

## REFERENCE VALUES (Good Defaults if Lost)

If you ever need to reset to "known good" settings:

```
PlayerController:
  Thrust: 50, Strafe: 30, Vertical: 25
  Pitch: 100, Yaw: 100
  Max Speed: 80, Boost Speed: 120
  Dead Zone: 0.15

Rigidbody:
  Mass: 1, Drag: 0.2, Angular Drag: 2

Camera:
  Follow: 10, Rotation: 5
```

---

## INSPIRATION & REFERENCE

**When movement feels good, it should remind you of:**

- Zone of the Enders 2 - Speed and fluidity
- Ace Combat 7 - Responsive controls
- Star Fox 64 - Arcade action feel
- Elite Dangerous - 6DoF freedom

**When it doesn't feel good:**

- Too floaty = increase drag
- Too stiff = decrease drag
- Too slow = increase forces
- Too twitchy = decrease rotation speeds

---

**END OF DOCUMENT**
