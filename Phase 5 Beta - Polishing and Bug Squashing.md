## **CRIMSON COMET: Development Roadmap**

### **Phase 5: Beta - Polishing and Bug Squashing**

**Primary Goal:** To transition the game from "Content Complete" (Alpha) to "Release Candidate" (Gold). This involves extensive testing, bug fixing, performance optimization, and incorporating feedback to ensure the game is stable and enjoyable for a public audience.

**Estimated Duration:** 8 Weeks

---

### **Module 5.1: Internal Testing & Triage (Weeks 31-32)**

**Sub-Goal:** To conduct a thorough internal playthrough of the entire game to identify as many bugs and balancing issues as possible before involving external testers.

*   **Step 1: The "Zero-Bug" Playthrough**
    *   **Task:** You and your team (if any) will play through the entire game from start to finish, multiple times.
    *   **Methodology:** Use a bug-tracking tool (this can be a simple spreadsheet or a dedicated tool like Trello, Jira, or HacknPlan). For every single issue, no matter how small, create a ticket. Categorize bugs by severity:
        *   **Blocker:** Prevents the game from being completed (e.g., a crash, a mission trigger that doesn't fire).
        *   **Major:** Severely impacts gameplay but doesn't stop progression (e.g., a weapon that does no damage, a boss that gets stuck).
        *   **Minor:** A small, noticeable issue (e.g., a UI element is misaligned, a sound effect doesn't play).
        *   **Trivial:** Cosmetic issues (e.g., a typo in the mission briefing).

*   **Step 2: Prioritization and Triage**
    *   **Task:** Review the generated bug list. The primary goal is to fix **100% of all Blocker bugs**. After that, focus on Major bugs.
    *   **Benefit:** This structured approach ensures that the most critical issues are addressed first. It prevents time from being wasted on trivial visual glitches when a game-breaking crash still exists.

*   **End of Module 5.1 Checkpoint:** A comprehensive list of known bugs has been created and prioritized. All known "Blocker" bugs have been fixed. The game is now stable enough to be put in the hands of people who have never seen it before.

---

### **Module 5.2: Closed Beta - The First Wave of Feedback (Weeks 33-35)**

**Sub-Goal:** To gather feedback on gameplay, difficulty, and bugs from a small, trusted group of external testers.

*   **Step 1: Recruit Testers and Distribute Builds**
    *   **Task:** Select a small group (5-10 people) of trusted friends or dedicated community members.
    *   **Distribution:** Use a platform like Steam's "Playtest" feature, itch.io, or a simple password-protected download to distribute the game build.

*   **Step 2: Establish Feedback Channels**
    *   **Task:** Create a clear and easy way for testers to report feedback. A private Discord channel is excellent for this, as is a simple Google Form.
    *   **Guidance:** Give testers clear instructions. Ask them to report bugs with steps to reproduce them. Also, ask for qualitative feedback: Which mission was the most frustrating? Which weapon felt the best to use? Was the story clear?

*   **Step 3: Analyze and Act on Feedback**
    *   **Task:** Collate all the feedback and bug reports. Look for patterns. If five different testers report that Mission 3's difficulty is a massive, unfair spike, that is a high-priority balancing issue.
    *   **Workflow:** Continue the "Triage" process. New bugs are added to the tracker. Balancing feedback is used to further tune the data in your ScriptableObjects (enemy health, weapon damage, etc.).
    *   **Iteration:** Periodically release new, updated builds to your testers throughout this period to confirm that your fixes have worked.

*   **End of Module 5.2 Checkpoint:** The game has been vetted by fresh eyes. Major balancing issues have been identified and addressed, and a significant number of new bugs have been found and fixed. The game feels more fair and is significantly more stable.

---

### **Module 5.3: Performance Optimization (Week 36)**

**Sub-Goal:** To ensure the game runs at a smooth, stable framerate on a variety of hardware configurations, especially on your target minimum spec PC.

*   **Step 1: Profiling and Benchmarking**
    *   **Task:** Use the **Unity Profiler** extensively. Play through the most graphically intense parts of your game (e.g., the final boss fight with lots of VFX) and watch the Profiler for spikes.
    *   **Identify Bottlenecks:** Is the CPU struggling (often due to AI or physics)? Or is the GPU the bottleneck (due to shaders, lighting, or too many particles)?

*   **Step 2: Optimization Tasks**
    *   **CPU Optimization:**
        *   Review and optimize any AI or game logic that runs every frame in `Update()`.
        *   Cache component lookups (`GetComponent<T>()`) in `Awake()` or `Start()` instead of calling them repeatedly.
    *   **GPU Optimization:**
        *   **Draw Calls:** Use techniques like Static Batching for non-moving environment objects.
        *   **Overdraw:** Analyze your particle effects. Are you rendering too many large, transparent particles on top of each other? Reduce particle counts.
        *   **Shaders:** Simplify any overly complex shaders.
        *   **LOD (Level of Detail):** Implement LODs for your more complex models, where lower-poly versions are used when the object is far from the camera.

*   **End of Module 5.3 Checkpoint:** The game's performance is noticeably improved and more consistent. You have a clear understanding of your game's minimum hardware requirements.

---

### **Module 5.4: Release Candidate & Final Polish (Weeks 37-38)**

**Sub-Goal:** To create the "Gold Master" build—the version of the game that you intend to ship. The focus is on final polish and addressing any last-minute issues.

*   **Step 1: The "Go/No-Go" Playthrough**
    *   **Task:** Play through the entire, fully optimized game one last time. This is the final check for any lingering Blocker or Major bugs that may have been introduced during optimization.

*   **Step 2: Final Polish Pass**
    *   **Task:** Address any remaining high-priority Minor or Trivial bugs that are feasible to fix without introducing new risks.
    *   **Example:** Fix typos, adjust the volume of one loud sound effect, tweak the color of a UI element for better readability.

*   **Step 3: Build the Release Version**
    *   **Task:** Create the final, shippable build of the game.
    *   **Preparation:** Ensure all debug tools are disabled. Finalize the game icon. Prepare the store page assets (screenshots, trailer, description) for platforms like Steam or itch.io.

*   **End of Phase 5 - Final Review (Release Candidate Milestone):** The game is now **"Ready to Ship."** It is stable, performant, and polished. All known major issues have been resolved. You have a final executable file that is ready to be uploaded to digital storefronts. The next and final phase is the Launch and Post-Launch support.