# **CRIMSON COMET: Phase 2 Development Roadmap (Weeks 5-6)**

## **Phase 2 Overview: The Core Combat Loop Prototype**

**Primary Goal:** Transform the movement prototype into a complete combat experience. Implement ranged weapons, melee attacks, stance switching, and basic enemy AI to create a self-contained gameplay loop.

**Philosophy:** Combat must feel as good as movement. Every shot, every swing, every kill must be satisfying. We're building on the foundation of Phase 1, not replacing it.

**Technical Stack:** (Continuing from Phase 1)
- **Engine:** Unity 6
- **Rendering:** Universal Render Pipeline (URP)
- **Input:** Unity's modern Input System package
- **Language:** C#

**Estimated Duration:** 4 Weeks (160 hours total)

**Phase 2 Success Criteria:**
- ✅ Both combat stances (Ranger/Assault) feel distinct and valuable
- ✅ Weapons have satisfying feedback (VFX, SFX, impact)
- ✅ Enemies pose a credible threat
- ✅ Combat integrates seamlessly with Phase 1 movement
- ✅ The core loop (engage → attack → evade → re-engage) is fun
- ✅ 60fps maintained with combat active

---

## **🗓️ Week 5: Armaments - Ranged Combat & Stance Switching**

**Weekly Goal:** Implement the "Ranger" stance with functional beam rifle, projectile system, targeting, and stance switching. By week's end, you should be able to lock onto and destroy targets with ranged weapons.

**Total Time Budget:** 40 hours  
**Daily Breakdown:** 8 hours/day × 5 days

---

### **📅 Day 21: Weapon System Architecture (8 hours)**

**Daily Goal:** Create the foundational weapon management system that will handle all combat for both stances.

---

#### **Morning Session (4 hours): Weapon Manager Setup**

**Task 21.1: Create Weapon Data Architecture** *(1.5 hours)*

**Create ScriptableObject system for weapons:**

`Assets/_Project/Scripts/Combat/WeaponData.cs`:

```csharp
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuPath = "CrimsonComet/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Basic Info")]
    public string weaponName;
    public WeaponType weaponType;
    
    [Header("Combat Stats")]
    public float damage = 15f;
    public float fireRate = 300f; // Rounds per minute
    public float projectileSpeed = 200f;
    public float range = 500f;
    
    [Header("Resource Cost")]
    public float boostCostPerShot = 0f; // Optional boost consumption
    public bool requiresAmmo = false;
    public int maxAmmo = 30;
    
    [Header("Prefabs & Effects")]
    public GameObject projectilePrefab;
    public GameObject muzzleFlashVFX;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    
    [Header("Recoil")]
    public float recoilForce = 2f; // Pushback on player
    public float screenShakeIntensity = 0.1f;
    
    // Calculated property
    public float TimeBetweenShots => 60f / fireRate;
}

public enum WeaponType
{
    Projectile,
    Hitscan,
    Melee
}
```

**Create first weapon data asset:**
1. Right-click in Project: `Create > CrimsonComet > Weapon Data`
2. Name: `WD_BeamRifle`
3. Configure:
```
Weapon Name: "Beam Rifle"
Weapon Type: Projectile
Damage: 15
Fire Rate: 300 (5 shots/second)
Projectile Speed: 200
Range: 500
```

---

**Task 21.2: Create Weapon Manager Script** *(2 hours)*

`Assets/_Project/Scripts/Combat/WeaponManager.cs`:

```csharp
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class WeaponManager : MonoBehaviour
{
    [Header("Combat Stance")]
    [SerializeField] private CombatStance currentStance = CombatStance.Ranger;
    
    [Header("Ranger Weapons")]
    [SerializeField] private WeaponData rangerPrimary;
    [SerializeField] private WeaponData rangerSecondary;
    
    [Header("Assault Weapons")]
    [SerializeField] private WeaponData assaultPrimary;
    [SerializeField] private WeaponData assaultSecondary;
    
    [Header("Fire Points")]
    [SerializeField] private Transform rangerFirePoint;
    [SerializeField] private Transform assaultFirePoint;
    
    [Header("References")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerControls controls;
    
    // Internal state
    private float primaryFireTimer = 0f;
    private float secondaryFireTimer = 0f;
    private bool primaryFireHeld = false;
    private bool secondaryFireHeld = false;
    
    // Events
    public event Action<CombatStance> OnStanceChanged;
    public event Action<WeaponData> OnWeaponFired;
    
    // Properties
    public CombatStance CurrentStance => currentStance;
    public WeaponData ActivePrimaryWeapon => currentStance == CombatStance.Ranger ? rangerPrimary : assaultPrimary;
    public WeaponData ActiveSecondaryWeapon => currentStance == CombatStance.Ranger ? rangerSecondary : assaultSecondary;
    
    private void Awake()
    {
        controls = new PlayerControls();
    }
    
    private void OnEnable()
    {
        controls.Gameplay.Enable();
        
        // Subscribe to input
        controls.Gameplay.FirePrimary.performed += ctx => primaryFireHeld = true;
        controls.Gameplay.FirePrimary.canceled += ctx => primaryFireHeld = false;
        
        controls.Gameplay.FireSecondary.performed += ctx => secondaryFireHeld = true;
        controls.Gameplay.FireSecondary.canceled += ctx => secondaryFireHeld = false;
        
        controls.Gameplay.StanceSwitch.performed += ctx => SwitchStance();
    }
    
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    
    private void Update()
    {
        UpdateFireTimers();
        HandleWeaponInput();
    }
    
    private void UpdateFireTimers()
    {
        if (primaryFireTimer > 0)
            primaryFireTimer -= Time.deltaTime;
        
        if (secondaryFireTimer > 0)
            secondaryFireTimer -= Time.deltaTime;
    }
    
    private void HandleWeaponInput()
    {
        // Primary weapon
        if (primaryFireHeld && primaryFireTimer <= 0)
        {
            FirePrimaryWeapon();
        }
        
        // Secondary weapon (tap only for now)
        if (secondaryFireHeld && secondaryFireTimer <= 0)
        {
            FireSecondaryWeapon();
            secondaryFireHeld = false; // Prevent hold
        }
    }
    
    private void FirePrimaryWeapon()
    {
        WeaponData weapon = ActivePrimaryWeapon;
        if (weapon == null) return;
        
        // Fire the weapon
        if (currentStance == CombatStance.Ranger)
        {
            FireProjectile(weapon, rangerFirePoint);
        }
        else if (currentStance == CombatStance.Assault)
        {
            // Melee will be implemented next week
            Debug.Log("Melee attack (not yet implemented)");
        }
        
        // Set cooldown
        primaryFireTimer = weapon.TimeBetweenShots;
        
        // Trigger event
        OnWeaponFired?.Invoke(weapon);
    }
    
    private void FireSecondaryWeapon()
    {
        WeaponData weapon = ActiveSecondaryWeapon;
        if (weapon == null) return;
        
        Debug.Log($"Fire secondary: {weapon.weaponName}");
        
        secondaryFireTimer = weapon.TimeBetweenShots;
    }
    
    private void FireProjectile(WeaponData weapon, Transform firePoint)
    {
        if (weapon.projectilePrefab == null)
        {
            Debug.LogWarning($"No projectile prefab for {weapon.weaponName}");
            return;
        }
        
        // Instantiate projectile
        GameObject projectile = Instantiate(
            weapon.projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );
        
        // Initialize projectile
        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.Initialize(weapon.damage, weapon.projectileSpeed, weapon.range);
        }
        
        // Apply recoil to player
        if (playerController != null)
        {
            playerController.ApplyRecoil(weapon.recoilForce);
        }
        
        // VFX/SFX will be added later
    }
    
    public void SwitchStance()
    {
        currentStance = (currentStance == CombatStance.Ranger) 
            ? CombatStance.Assault 
            : CombatStance.Ranger;
        
        Debug.Log($"Switched to {currentStance} stance");
        
        OnStanceChanged?.Invoke(currentStance);
    }
}

public enum CombatStance
{
    Ranger,
    Assault
}
```

---

**Task 21.3: Add Recoil to PlayerController** *(30 minutes)*

Update `PlayerController.cs`:

```csharp
// Add this method to PlayerController
public void ApplyRecoil(float force)
{
    if (rb == null) return;
    
    // Push player backward slightly
    rb.AddRelativeForce(-transform.forward * force, ForceMode.Impulse);
}
```

This creates feedback when firing weapons.

---

#### **Afternoon Session (4 hours): Fire Points & Testing**

**Task 21.4: Create Fire Point GameObjects** *(45 minutes)*

**Setup weapon mount points:**

1. Select `Player` GameObject
2. Create empty child: `WeaponMounts`
3. Create children of WeaponMounts:

**RangerFirePoint:**
```
Position: (0.3, 0, 0.6) - slightly right and forward
Rotation: (0, 0, 0)
```

**AssaultFirePoint:**
```
Position: (0, 0, 0.8) - center forward
Rotation: (0, 0, 0)
```

4. Add visual indicators (small colored cubes):
   - RangerFirePoint → Blue cube (0.1, 0.1, 0.1)
   - AssaultFirePoint → Red cube (0.1, 0.1, 0.1)

**These show where projectiles spawn.**

---

**Task 21.5: Attach WeaponManager to Player** *(15 minutes)*

1. Add `WeaponManager` component to `Player`
2. Assign references in Inspector:
   - Ranger Primary: Drag `WD_BeamRifle`
   - Ranger Fire Point: Drag `RangerFirePoint`
   - Player Controller: Drag `PlayerController` component
   - Controls: Will auto-generate

---

**Task 21.6: Input Testing** *(1 hour)*

**Test stance switching:**

Add debug visualization to `WeaponManager.cs`:

```csharp
private void OnGUI()
{
    GUI.Label(new Rect(10, 100, 200, 30), $"Stance: {currentStance}");
    GUI.Label(new Rect(10, 130, 300, 30), $"Primary: {ActivePrimaryWeapon?.weaponName ?? "None"}");
}
```

**Press Play and test:**
- [ ] Triangle/Y switches stance
- [ ] UI shows "Ranger" or "Assault"
- [ ] Primary weapon name updates

---

**Task 21.7: Create Projectile System** *(2 hours)*

`Assets/_Project/Scripts/Combat/Projectile.cs`:

```csharp
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private float damage;
    private float speed;
    private float maxRange;
    
    private Vector3 spawnPosition;
    private Rigidbody rb;
    private bool initialized = false;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
    
    public void Initialize(float damageAmount, float projectileSpeed, float range)
    {
        damage = damageAmount;
        speed = projectileSpeed;
        maxRange = range;
        
        spawnPosition = transform.position;
        initialized = true;
        
        // Launch projectile
        rb.velocity = transform.forward * speed;
        
        // Auto-destroy after max range
        float lifetime = maxRange / speed;
        Destroy(gameObject, lifetime);
    }
    
    private void Update()
    {
        if (!initialized) return;
        
        // Check if traveled max range
        float distanceTraveled = Vector3.Distance(spawnPosition, transform.position);
        if (distanceTraveled >= maxRange)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // Check for Health component
        Health target = collision.gameObject.GetComponent<Health>();
        
        if (target != null)
        {
            target.TakeDamage(damage);
        }
        
        // Destroy projectile on any collision
        Destroy(gameObject);
    }
}
```

**Create projectile prefab:**

1. Create Sphere primitive: `BeamProjectile`
2. Scale: (0.2, 0.2, 0.2)
3. Add Rigidbody:
   - Use Gravity: OFF
   - Collision Detection: Continuous
4. Add `Projectile` script
5. Create material: `M_BeamProjectile`
   - Color: Bright cyan
   - Emission: Enabled, cyan, HDR intensity: 2
6. Make prefab: Drag to `Assets/_Project/Prefabs/Combat/`
7. Delete from scene

**Assign to WD_BeamRifle:**
- Projectile Prefab: `BeamProjectile`

---

**End of Day 21 Commit:**
```
git add .
git commit -m "Day 21: Weapon system architecture - WeaponManager, projectiles, stance switching"
```

---

### **📅 Day 22: Health System & Destructible Targets (8 hours)**

**Daily Goal:** Create a robust health/damage system and set up test targets to shoot at.

---

#### **Morning Session (4 hours): Health Component**

**Task 22.1: Create Health System** *(2 hours)*

`Assets/_Project/Scripts/Combat/Health.cs`:

```csharp
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    
    [Header("Damage Response")]
    [SerializeField] private bool destroyOnDeath = true;
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private float deathDelay = 0f;
    
    [Header("Damage Feedback")]
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float flashDuration = 0.1f;
    
    private Material originalMaterial;
    private Renderer meshRenderer;
    private bool isDead = false;
    
    // Events
    public event Action<float, float> OnHealthChanged; // current, max
    public event Action<float> OnDamageTaken;
    public event Action OnDeath;
    
    // Properties
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public float HealthPercent => currentHealth / maxHealth;
    public bool IsDead => isDead;
    
    private void Awake()
    {
        currentHealth = maxHealth;
        meshRenderer = GetComponent<Renderer>();
        
        if (meshRenderer != null)
        {
            originalMaterial = meshRenderer.material;
        }
    }
    
    public void TakeDamage(float amount)
    {
        if (isDead) return;
        
        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);
        
        // Trigger events
        OnDamageTaken?.Invoke(amount);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        
        // Visual feedback
        if (meshRenderer != null && flashMaterial != null)
        {
            StartCoroutine(DamageFlash());
        }
        
        // Check for death
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void Heal(float amount)
    {
        if (isDead) return;
        
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    
    private void Die()
    {
        if (isDead) return;
        
        isDead = true;
        
        OnDeath?.Invoke();
        
        // Spawn death VFX
        if (deathVFX != null)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
        }
        
        // Play death sound
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }
        
        // Destroy or deactivate
        if (destroyOnDeath)
        {
            Destroy(gameObject, deathDelay);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    
    private System.Collections.IEnumerator DamageFlash()
    {
        if (meshRenderer != null && flashMaterial != null)
        {
            meshRenderer.material = flashMaterial;
            yield return new WaitForSeconds(flashDuration);
            meshRenderer.material = originalMaterial;
        }
    }
    
    // Debug
    private void OnGUI()
    {
        if (isDead) return;
        
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
        
        if (screenPos.z > 0)
        {
            GUI.Label(new Rect(screenPos.x - 50, Screen.height - screenPos.y, 100, 20), 
                      $"HP: {currentHealth:F0}/{maxHealth:F0}");
        }
    }
}
```

---

**Task 22.2: Create Damage Flash Material** *(30 minutes)*

1. Duplicate `M_PlayerBody` material
2. Rename: `M_DamageFlash`
3. Settings:
   - Albedo: Pure white (255, 255, 255)
   - Emission: Enabled, white, HDR: 3
   - Metallic: 0
   - Smoothness: 1

This material will briefly replace the original when hit.

---

**Task 22.3: Create Test Targets** *(1.5 hours)*

**Create "Dummy Target" prefab:**

1. Create Sphere: `DummyTarget`
2. Scale: (2, 2, 2) - larger than player
3. Add `Health` component:
   - Max Health: 100
   - Destroy On Death: True
4. Create material: `M_DummyTarget`
   - Color: Blue (0, 100, 255)
   - Metallic: 0.3
   - Smoothness: 0.6
5. Assign flash material:
   - Flash Material: `M_DamageFlash`
   - Flash Duration: 0.1
6. Make prefab

**Create target spawn script:**

`Assets/_Project/Scripts/Gameplay/TargetSpawner.cs`:

```csharp
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private int targetCount = 5;
    [SerializeField] private float spawnRadius = 50f;
    [SerializeField] private bool spawnOnStart = true;
    
    private void Start()
    {
        if (spawnOnStart)
        {
            SpawnTargets();
        }
    }
    
    [ContextMenu("Spawn Targets")]
    public void SpawnTargets()
    {
        for (int i = 0; i < targetCount; i++)
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
            Instantiate(targetPrefab, randomPos, Quaternion.identity);
        }
    }
    
    [ContextMenu("Clear Targets")]
    public void ClearTargets()
    {
        Health[] targets = FindObjectsOfType<Health>();
        foreach (Health target in targets)
        {
            DestroyImmediate(target.gameObject);
        }
    }
}
```

---

#### **Afternoon Session (4 hours): Testing & Iteration**

**Task 22.4: Setup Combat Test Scene** *(1 hour)*

1. Duplicate `Phase1_FinalArena.unity`
2. Rename: `Phase2_CombatTest.unity`
3. Save in `Scenes/` folder

**Add targets:**
1. Create empty: `TargetManager`
2. Add `TargetSpawner` script
3. Assign `DummyTarget` prefab
4. Set count: 10
5. Spawn radius: 100

**Press Play → Use context menu to spawn targets**

---

**Task 22.5: First Combat Test** *(1 hour)*

**Test protocol:**

```
1. Enter Play Mode
2. Spawn targets (right-click TargetSpawner → Spawn Targets)
3. Fly toward a target
4. Hold R1 to fire

Expected:
- Blue projectiles shoot from RangerFirePoint
- Projectiles travel forward
- On hitting target: white flash
- After ~7 hits (15 damage × 7 = 105): target explodes

Actual:
[Document what happens]
```

**Common issues:**

**Issue: Projectiles don't spawn**
- Check WeaponData has projectile prefab assigned
- Check RangerFirePoint is assigned in WeaponManager
- Check console for errors

**Issue: Projectiles spawn but don't move**
- Check Projectile.Initialize() is being called
- Check Rigidbody on projectile has no constraints

**Issue: Projectiles pass through targets**
- Check targets have Collider component
- Check projectiles have Rigidbody + Collider
- Check collision layers (should both be on Default)

---

**Task 22.6: Damage Number Feedback (Optional)** *(1.5 hours)*

**Visual damage numbers:**

`Assets/_Project/Scripts/Combat/DamageNumber.cs`:

```csharp
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private float lifetime = 1f;
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);
    
    private float timer = 0f;
    private Color startColor;
    
    public void Initialize(float damageAmount, Vector3 position)
    {
        transform.position = position;
        textMesh.text = damageAmount.ToString("F0");
        startColor = textMesh.color;
        
        Destroy(gameObject, lifetime);
    }
    
    private void Update()
    {
        timer += Time.deltaTime;
        
        // Float upward
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        
        // Fade out
        float alpha = fadeCurve.Evaluate(timer / lifetime);
        textMesh.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
    }
}
```

**Create prefab:**
1. Create empty: `DamageNumber`
2. Add 3D TextMeshPro (TextMeshPro → 3D Text)
3. Settings:
   - Font Size: 4
   - Color: Yellow
   - Alignment: Center/Middle
4. Add `DamageNumber` script
5. Assign textMesh reference
6. Make prefab

**Integrate with Health:**

```csharp
// In Health.cs
[SerializeField] private GameObject damageNumberPrefab;

public void TakeDamage(float amount)
{
    // ... existing code ...
    
    // Spawn damage number
    if (damageNumberPrefab != null)
    {
        GameObject dmgNum = Instantiate(damageNumberPrefab, transform.position + Vector3.up, Quaternion.identity);
        dmgNum.GetComponent<DamageNumber>()?.Initialize(amount, transform.position + Vector3.up);
    }
}
```

---

**Task 22.7: Tuning First Pass** *(30 minutes)*

**Adjust these values for good feel:**

**BeamRifle (WD_BeamRifle):**
```
Damage: 15 (feels right for 7 shots to kill)
Fire Rate: 300 RPM (5 shots/second)
Projectile Speed: 200 m/s (fast but dodgeable)
Recoil Force: 1.5 (subtle pushback)
```

**DummyTarget:**
```
Max Health: 100 (baseline)
```

**Projectile:**
```
Scale: (0.2, 0.2, 0.2) - visible but not huge
```

**Test:** Can you destroy 5 targets in under 30 seconds?

---

**End of Day 22 Commit:**
```
git add .
git commit -m "Day 22: Health system, destructible targets, damage feedback"
```

---

### **📅 Day 23: Target Lock-On System (8 hours)**

**Daily Goal:** Implement a robust lock-on targeting system that helps players track enemies during high-speed combat.

---

#### **Morning Session (4 hours): Lock-On Core System**

**Task 23.1: Create Targetable Interface** *(30 minutes)*

Good practice: Use interface for anything that can be targeted.

`Assets/_Project/Scripts/Combat/ITargetable.cs`:

```csharp
using UnityEngine;

public interface ITargetable
{
    Transform Transform { get; }
    Vector3 TargetPosition { get; }
    bool IsValidTarget { get; }
    float GetPriority(); // For selecting between multiple targets
}
```

**Implement in Health:**

```csharp
public class Health : MonoBehaviour, ITargetable
{
    // ... existing code ...
    
    // ITargetable implementation
    public Transform Transform => transform;
    public Vector3 TargetPosition => transform.position;
    public bool IsValidTarget => !isDead;
    
    public float GetPriority()
    {
        // Closer to death = higher priority
        return 1f - HealthPercent;
    }
}
```

---

**Task 23.2: Create Targeting System** *(2.5 hours)*

`Assets/_Project/Scripts/Combat/TargetingSystem.cs`:

```csharp
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

public class TargetingSystem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float maxLockOnRange = 500f;
    [SerializeField] private float maxLockOnAngle = 60f; // Cone in front of player
    [SerializeField] private float lockOffAngle = 150f; // Wider cone to maintain lock
    [SerializeField] private LayerMask targetLayers = ~0;
    
    [Header("References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private PlayerControls controls;
    
    [Header("UI")]
    [SerializeField] private GameObject lockOnReticle;
    [SerializeField] private float reticleOffset = 2f; // Above target
    
    private ITargetable currentTarget;
    private List<ITargetable> availableTargets = new List<ITargetable>();
    private bool isLockedOn = false;
    
    // Events
    public event System.Action<ITargetable> OnTargetLocked;
    public event System.Action OnTargetLost;
    
    // Properties
    public ITargetable CurrentTarget => currentTarget;
    public bool IsLockedOn => isLockedOn && currentTarget != null && currentTarget.IsValidTarget;
    
    private void Awake()
    {
        controls = new PlayerControls();
        
        if (playerCamera == null)
            playerCamera = Camera.main;
    }
    
    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.LockOn.performed += OnLockOnPressed;
    }
    
    private void OnDisable()
    {
        controls.Gameplay.Disable();
        controls.Gameplay.LockOn.performed -= OnLockOnPressed;
    }
    
    private void Update()
    {
        UpdateAvailableTargets();
        UpdateCurrentLock();
        UpdateLockOnUI();
    }
    
    private void UpdateAvailableTargets()
    {
        availableTargets.Clear();
        
        // Find all objects with Health component
        Health[] allTargets = FindObjectsOfType<Health>();
        
        foreach (Health health in allTargets)
        {
            if (health.gameObject == playerTransform.gameObject) continue; // Don't target self
            
            ITargetable targetable = health as ITargetable;
            if (targetable != null && targetable.IsValidTarget)
            {
                availableTargets.Add(targetable);
            }
        }
    }
    
    private void OnLockOnPressed(InputAction.CallbackContext context)
    {
        if (isLockedOn)
        {
            // Toggle off
            ClearTarget();
        }
        else
        {
            // Try to acquire target
            AcquireTarget();
        }
    }
    
    private void AcquireTarget()
    {
        ITargetable bestTarget = null;
        float bestScore = float.MaxValue;
        
        foreach (ITargetable target in availableTargets)
        {
            if (!IsTargetInRange(target)) continue;
            if (!IsTargetInView(target, maxLockOnAngle)) continue;
            
            // Score based on distance and angle
            float distance = Vector3.Distance(playerTransform.position, target.TargetPosition);
            float angle = Vector3.Angle(playerTransform.forward, target.TargetPosition - playerTransform.position);
            
            // Combined score (lower is better)
            float score = (distance / maxLockOnRange) + (angle / maxLockOnAngle);
            
            if (score < bestScore)
            {
                bestScore = score;
                bestTarget = target;
            }
        }
        
        if (bestTarget != null)
        {
            SetTarget(bestTarget);
        }
    }
    
    private void SetTarget(ITargetable target)
    {
        currentTarget = target;
        isLockedOn = true;
        
        if (lockOnReticle != null)
        {
            lockOnReticle.SetActive(true);
        }
        
        OnTargetLocked?.Invoke(target);
        
        Debug.Log($"Locked onto: {target.Transform.name}");
    }
    
    private void ClearTarget()
    {
        currentTarget = null;
        isLockedOn = false;
        
        if (lockOnReticle != null)
        {
            lockOnReticle.SetActive(false);
        }
        
        OnTargetLost?.Invoke();
        
        Debug.Log("Lock-on cleared");
    }
    
    private void UpdateCurrentLock()
    {
        if (!isLockedOn) return;
        
        // Check if target is still valid
        if (currentTarget == null || !currentTarget.IsValidTarget)
        {
            ClearTarget();
            return;
        }
        
        // Check if target is out of range
        if (!IsTargetInRange(currentTarget))
        {
            ClearTarget();
            return;
        }
        
        // Check if target is out of view (wider angle for maintaining lock)
        if (!IsTargetInView(currentTarget, lockOffAngle))
        {
            ClearTarget();
            return;
        }
    }
    
    private bool IsTargetInRange(ITargetable target)
    {
        float distance = Vector3.Distance(playerTransform.position, target.TargetPosition);
        return distance <= maxLockOnRange;
    }
    
    private bool IsTargetInView(ITargetable target, float maxAngle)
    {
        Vector3 directionToTarget = target.TargetPosition - playerTransform.position;
        float angle = Vector3.Angle(playerTransform.forward, directionToTarget);
        return angle <= maxAngle;
    }
    
    private void UpdateLockOnUI()
    {
        if (lockOnReticle == null) return;
        
        if (isLockedOn && currentTarget != null)
        {
            // Position reticle above target
            Vector3 targetPos = currentTarget.TargetPosition + Vector3.up * reticleOffset;
            lockOnReticle.transform.position = targetPos;
            
            // Make reticle face camera
            lockOnReticle.transform.LookAt(playerCamera.transform);
            lockOnReticle.transform.Rotate(0, 180, 0);
        }
    }
    
    // Helper: Get aiming direction (for weapon firing)
    public Vector3 GetAimDirection(Transform firePoint)
    {
        if (isLockedOn && currentTarget != null)
        {
            // Aim at target
            return (currentTarget.TargetPosition - firePoint.position).normalized;
        }
        else
        {
            // Aim forward
            return firePoint.forward;
        }
    }
}
```

---

**Task 23.3: Create Lock-On Reticle** *(1 hour)*

**Visual indicator for locked target:**

1. Create Quad primitive: `LockOnReticle`
2. Rotation: (0, 0, 0)
3. Scale: (2, 2, 1)
4. Remove MeshCollider component

**Create reticle texture:**
- Open image editor (Photoshop, GIMP, Paint.NET)
- Create 256x256 image
- Draw hollow red square/diamond/crosshair
- Save as PNG with transparency
- Import to Unity: `Assets/_Project/Art/UI/LockOnReticle.png`

**Create material:**
1. Create material: `M_LockOnReticle`
2. Shader: Unlit/Transparent
3. Texture: LockOnReticle.png
4. Color: Red (255, 0, 0)
5. Rendering Mode: Transparent

**Assign to quad, make prefab, delete from scene**

---

#### **Afternoon Session (4 hours): Lock-On Integration**

**Task 23.4: Integrate with Player** *(45 minutes)*

1. Add `TargetingSystem` component to Player
2. Assign references:
   - Player Transform: Player
   - Player Camera: Main Camera
   - Lock On Reticle: LockOnReticle prefab
3. Settings:
   - Max Lock On Range: 300
   - Max Lock On Angle: 60
   - Lock Off Angle: 120

---

**Task 23.5: Integrate with Weapons** *(1.5 hours)*

**Update WeaponManager to use targeting:**

```csharp
public class WeaponManager : MonoBehaviour
{
    // ... existing code ...
    
    [Header("Targeting")]
    [SerializeField] private TargetingSystem targetingSystem;
    
    private void FireProjectile(WeaponData weapon, Transform firePoint)
    {
        if (weapon.projectilePrefab == null)
        {
            Debug.LogWarning($"No projectile prefab for {weapon.weaponName}");
            return;
        }
        
        // Get aim direction from targeting system
        Vector3 aimDirection = firePoint.forward;
        
        if (targetingSystem != null)
        {
            aimDirection = targetingSystem.GetAimDirection(firePoint);
        }
        
        // Calculate rotation to face aim direction
        Quaternion aimRotation = Quaternion.LookRotation(aimDirection);
        
        // Instantiate projectile
        GameObject projectile = Instantiate(
            weapon.projectilePrefab,
            firePoint.position,
            aimRotation // Use calculated rotation
        );
        
        // Initialize projectile
        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.Initialize(weapon.damage, weapon.projectileSpeed, weapon.range);
        }
        
        // Apply recoil to player
        if (playerController != null)
        {
            playerController.ApplyRecoil(weapon.recoilForce);
        }
    }
}
```

**Assign TargetingSystem reference in Inspector**

---

**Task 23.6: Camera Lock-On Assistance** *(1.5 hours)*

**Make camera frame both player and target:**

Update `DynamicLookTarget.cs`:

```csharp
using UnityEngine;
using Unity.Cinemachine;

public class DynamicLookTarget : MonoBehaviour
{
    // ... existing code ...
    
    [Header("Lock-On")]
    [SerializeField] private TargetingSystem targetingSystem;
    [SerializeField] private float lockOnWeight = 0.5f; // How much to bias toward target
    
    private void UpdateLookTarget()
    {
        Vector3 targetPosition;
        
        // Check if locked on
        if (targetingSystem != null && targetingSystem.IsLockedOn && targetingSystem.CurrentTarget != null)
        {
            // Blend between velocity direction and target direction
            Vector3 velocityLookPos = player.position + playerRb.velocity.normalized * maxLookAheadDistance;
            Vector3 targetLookPos = targetingSystem.CurrentTarget.TargetPosition;
            
            targetPosition = Vector3.Lerp(velocityLookPos, targetLookPos, lockOnWeight);
        }
        else
        {
            // Normal velocity-based look-ahead
            if (playerRb.velocity.magnitude > 1f)
            {
                Vector3 velocityDirection = playerRb.velocity.normalized;
                float speedFactor = Mathf.Clamp01(playerRb.velocity.magnitude / 100f);
                
                targetPosition = player.position + (velocityDirection * maxLookAheadDistance * speedFactor);
            }
            else
            {
                targetPosition = player.position + player.forward * 5f;
            }
        }
        
        // Smoothly move look target
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            lookAheadSpeed * Time.deltaTime
        );
    }
}
```

**Assign TargetingSystem reference**

---

**Task 23.7: Lock-On Testing & Tuning** *(30 minutes)*

**Test protocol:**

```
1. Spawn 5 targets in front of player
2. Press R3 (lock-on button)
3. Red reticle should appear on nearest target
4. Camera should adjust to frame both player and target
5. Fire weapon - projectiles should curve toward target
6. Press R3 again - lock should release
```

**Tuning values:**

```
TargetingSystem:
- Max Lock On Range: 300 (can lock from far)
- Max Lock On Angle: 60 (must be in front)
- Lock Off Angle: 120 (maintains lock behind you)

DynamicLookTarget:
- Lock On Weight: 0.5 (50% camera bias toward target)
  - Too high (0.8+): Camera ignores movement
  - Too low (0.2-): Lock-on barely visible
```

---

**Task 23.8: Lock-On Audio & VFX** *(30 minutes)*

**Add feedback when locking on:**

Update `TargetingSystem.cs`:

```csharp
[Header("Feedback")]
[SerializeField] private AudioClip lockOnSound;
[SerializeField] private AudioClip lockOffSound;
[SerializeField] private AudioSource audioSource;

private void SetTarget(ITargetable target)
{
    currentTarget = target;
    isLockedOn = true;
    
    if (lockOnReticle != null)
    {
        lockOnReticle.SetActive(true);
    }
    
    // Audio feedback
    if (lockOnSound != null && audioSource != null)
    {
        audioSource.PlayOneShot(lockOnSound);
    }
    
    OnTargetLocked?.Invoke(target);
}

private void ClearTarget()
{
    // ... existing code ...
    
    // Audio feedback
    if (lockOffSound != null && audioSource != null)
    {
        audioSource.PlayOneShot(lockOffSound);
    }
    
    // ... rest of code
}
```

**Find sounds:**
- Lock-on: Short beep (freesound.org: "radar lock")
- Lock-off: Softer beep or click

---

**End of Day 23 Commit:**
```
git add .
git commit -m "Day 23: Lock-on targeting system with camera integration"
```

---

### **📅 Day 24-25: Weapon VFX/SFX & Polish (16 hours)**

**Goal:** Make weapons feel amazing through comprehensive feedback.

---

#### **Day 24 Morning: Muzzle Flash & Projectile Trails** *(4 hours)*

**Task 24.1: Create Muzzle Flash VFX** *(2 hours)*

1. Create Particle System: `MuzzleFlash_BeamRifle`
2. Position at RangerFirePoint
3. Settings:

```
Duration: 0.1
Looping: OFF

Emission:
  Bursts: 1 burst of 5 particles at time 0

Shape:
  Shape: Cone
  Angle: 30
  Radius: 0.3

Start Lifetime: 0.1
Start Speed: 3
Start Size: Random 0.3 to 0.6

Color over Lifetime:
  Start: Bright cyan (0, 255, 255, 255)
  End: Transparent cyan

Size over Lifetime:
  Start: 1.0
  End: 0.1

Renderer:
  Material: Default-Particle (additive)
```

4. Disable GameObject
5. Make prefab

**Integrate:**

```csharp
// In WeaponManager.FireProjectile()
// Spawn muzzle flash
if (weapon.muzzleFlashVFX != null)
{
    GameObject flash = Instantiate(weapon.muzzleFlashVFX, firePoint.position, firePoint.rotation);
    Destroy(flash, 1f);
}
```

Assign muzzle flash prefab to WD_BeamRifle

---

**Task 24.2: Projectile Trail** *(1 hour)*

Add Trail Renderer to BeamProjectile prefab:

```
Time: 0.3
Width: 0.1 → 0.0
Color: Cyan gradient (bright → transparent)
Material: Default-Particle
```

---

**Task 24.3: Impact VFX** *(1 hour)*

Create hit spark effect:

1. Particle System: `ImpactSparks`
2. Settings:

```
Duration: 0.2
Looping: OFF
Emission: Burst 10 particles
Shape: Sphere, radius 0.5
Start Speed: 5-10
Color: Yellow-orange gradient
Size: 0.1-0.2
Gravity Modifier: 0.5 (falls slightly)
```

**Integrate in Projectile:**

```csharp
[SerializeField] private GameObject impactVFX;

private void OnCollisionEnter(Collision collision)
{
    // Spawn impact effect
    if (impactVFX != null)
    {
        GameObject impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity);
        Destroy(impact, 2f);
    }
    
    // ... existing damage code ...
}
```

---

#### **Day 24 Afternoon: Weapon Audio** *(4 hours)*

**Task 24.4: Layered Weapon Audio** *(2 hours)*

**Create weapon audio stack:**

Find these sounds (freesound.org):
- Fire sound (main): "laser shot"
- Fire tail: "energy discharge"
- Mechanical click: "gun cock"

**Update WeaponManager:**

```csharp
[SerializeField] private AudioSource weaponAudioSource;

private void FireProjectile(WeaponData weapon, Transform firePoint)
{
    // ... existing code ...
    
    // Audio
    if (weapon.fireSound != null && weaponAudioSource != null)
    {
        weaponAudioSource.pitch = Random.Range(0.95f, 1.05f); // Variation
        weaponAudioSource.PlayOneShot(weapon.fireSound);
    }
}
```

---

**Task 24.5: Enemy Death Sound** *(1 hour)*

Find explosion sound, assign to Health component's deathSound field.

---

**Task 24.6: Audio Mixing** *(1 hour)*

Open Audio Mixer, create groups:

```
Master
├── SFX
│   ├── Movement (from Phase 1)
│   ├── Weapons
│   │   ├── PlayerWeapons
│   │   └── EnemyWeapons
│   └── Impacts
└── Music
```

Assign all weapon sounds to appropriate groups.

**Levels:**
- PlayerWeapons: -3 dB
- Impacts: -6 dB

---

#### **Day 25: Screen Effects & Polish** *(8 hours)*

**Task 25.1: Tracer Rounds** *(2 hours)*

Make every 5th shot a brighter "tracer":

```csharp
private int shotCount = 0;

private void FireProjectile(WeaponData weapon, Transform firePoint)
{
    // ... existing code ...
    
    shotCount++;
    bool isTracer = (shotCount % 5 == 0);
    
    // Make tracer brighter
    if (isTracer)
    {
        Renderer rend = projectile.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.SetColor("_EmissionColor", Color.cyan * 5f);
        }
    }
}
```

---

**Task 25.2: Hit Markers** *(2 hours)*

**Create UI hit confirmation:**

`Assets/_Project/Scripts/UI/HitMarker.cs`:

```csharp
using UnityEngine;
using UnityEngine.UI;

public class HitMarker : MonoBehaviour
{
    [SerializeField] private Image hitMarkerImage;
    [SerializeField] private float displayDuration = 0.2f;
    [SerializeField] private AnimationCurve fadeCurve;
    
    private float timer = 0f;
    private bool showing = false;
    
    private void Start()
    {
        hitMarkerImage.enabled = false;
    }
    
    public void Show()
    {
        hitMarkerImage.enabled = true;
        timer = displayDuration;
        showing = true;
    }
    
    private void Update()
    {
        if (showing)
        {
            timer -= Time.deltaTime;
            
            float alpha = fadeCurve.Evaluate(1f - (timer / displayDuration));
            Color c = hitMarkerImage.color;
            c.a = alpha;
            hitMarkerImage.color = c;
            
            if (timer <= 0)
            {
                hitMarkerImage.enabled = false;
                showing = false;
            }
        }
    }
}
```

Create crosshair sprite with X or + shape, add to canvas center.

**Connect to damage:**

```csharp
// In WeaponManager or PlayerController
private HitMarker hitMarker;

private void OnEnable()
{
    // Subscribe to all Health components taking damage
    // When player's projectile hits, call hitMarker.Show()
}
```

---

**Task 25.3: Ammo Counter UI** *(1 hour)*

Add ammo display to HUD:

```
Current Weapon: BEAM RIFLE
Ammo: ∞ (or 25/100 if limited)
Heat: [=======   ] (if overheating system)
```

---

**Task 25.4: Stance Indicator** *(1 hour)*

Visual indicator of current stance:

```
Large icon:
- Ranger: Rifle symbol (blue)
- Assault: Sword symbol (red)

Position: Bottom right corner
Animates when switching (scale pulse)
```

---

**Task 25.5: Final Combat Feel Pass** *(2 hours)*

**Tuning checklist:**

```
Beam Rifle:
- [ ] Feels responsive (fires immediately on input)
- [ ] Projectiles visible and trackable
- [ ] Hit feedback is clear
- [ ] Killing target feels satisfying
- [ ] Audio is punchy, not annoying
- [ ] Muzzle flash is visible but not blinding

Lock-On:
- [ ] Easy to acquire target
- [ ] Reticle is clear
- [ ] Camera helps but doesn't fight player
- [ ] Audio confirms lock
- [ ] Can reliably hit locked target

Performance:
- [ ] 60fps with 10 targets on screen
- [ ] No lag when firing rapidly
- [ ] Particles don't cause stutter
```

---

**End of Week 5 Commit:**
```
git add .
git commit -m "Week 5 Complete: Ranger stance, beam rifle, lock-on, full weapon feedback"
git tag "Phase2-Week5-Complete"
```

---

## **🎯 Week 5 Deliverables Checklist**

**Before Week 6:**

### **Functional**
- [ ] WeaponManager with stance switching
- [ ] Beam Rifle fires projectiles
- [ ] Projectiles damage Health components
- [ ] Targets die and explode
- [ ] Lock-on acquires and tracks targets
- [ ] Camera assists during lock-on

### **Feedback**
- [ ] Muzzle flash VFX
- [ ] Projectile trails
- [ ] Impact sparks
- [ ] Damage flash on hit
- [ ] Death explosion
- [ ] All weapon audio (fire, impact, death)
- [ ] Lock-on audio (acquire, release)

### **UI**
- [ ] Lock-on reticle
- [ ] Hit markers
- [ ] Stance indicator
- [ ] Health display on targets

### **Performance**
- [ ] 60fps with 10 active targets
- [ ] No memory leaks from spawning/destroying

---

**Week 5 is COMPLETE! Ranger stance is fully functional and satisfying.**

**Next: Week 6 will add melee combat (Assault stance).**
# **CRIMSON COMET: Phase 2 Development Roadmap (Weeks 6-7)**

## **🗓️ Week 6: Close Quarters - Melee Combat**

**Weekly Goal:** Implement the "Assault" stance with satisfying close-range melee combat. Create a three-hit combo system with lunging movement and proper hitbox detection. By week's end, slashing enemies should feel as good as shooting them.

**Total Time Budget:** 40 hours  
**Daily Breakdown:** 8 hours/day × 5 days

**Week 6 Success Criteria:**
- ✅ Melee weapons have physical presence (visible blade)
- ✅ Three-hit combo flows naturally
- ✅ Melee attacks provide forward momentum (lunge)
- ✅ Hitboxes detect enemies accurately
- ✅ Hit-stop creates impactful feeling
- ✅ Can destroy targets with melee as effectively as ranged

---

### **📅 Day 26: Melee Weapon Foundation (8 hours)**

**Daily Goal:** Create the physical melee weapon objects and basic swing mechanics.

---

#### **Morning Session (4 hours): Beam Saber Visual & Hitbox**

**Task 26.1: Create Beam Saber Model** *(1.5 hours)*

**Build the weapon from primitives:**

1. Create empty GameObject: `BeamSaber` (child of Player)
2. Position: (0.5, 0, 0.8) - right side, forward
3. Rotation: (0, 0, 0)

**Saber components:**

```
BeamSaber (root)
├── Handle
│   └── Cylinder (0.1 width, 0.5 height)
│   └── Material: M_SaberHandle (dark gray, metallic)
├── Blade
│   └── Cube (0.15 width, 0.15 depth, 2.5 height)
│   └── Material: M_SaberBlade (magenta, emissive)
└── HitBox
    └── Box Collider (Is Trigger: TRUE)
    └── Size: (0.3, 0.3, 2.8)
```

**Create materials:**

**M_SaberHandle:**
```
Albedo: Dark gray (60, 60, 60)
Metallic: 0.8
Smoothness: 0.9
```

**M_SaberBlade:**
```
Albedo: Magenta (255, 0, 200)
Emission: Enabled
  - Color: Magenta
  - HDR Intensity: 3
Metallic: 0.5
Smoothness: 1.0
```

**Setup collider:**
- HitBox object has Box Collider
- Is Trigger: **CHECKED** ✅
- Center: (0, 0, 1.25) - extends forward from handle
- Size: (0.3, 0.3, 2.8) - slightly larger than blade

**By default, disable entire BeamSaber GameObject**

---

**Task 26.2: Create Melee Weapon Script** *(2 hours)*

`Assets/_Project/Scripts/Combat/MeleeWeapon.cs`:

```csharp
using UnityEngine;
using System.Collections.Generic;

public class MeleeWeapon : MonoBehaviour
{
    [Header("Weapon Data")]
    [SerializeField] private WeaponData weaponData;
    
    [Header("Hitbox")]
    [SerializeField] private Collider hitboxCollider;
    
    [Header("VFX")]
    [SerializeField] private ParticleSystem slashTrailVFX;
    [SerializeField] private GameObject impactVFX;
    
    [Header("Audio")]
    [SerializeField] private AudioClip swingSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioSource audioSource;
    
    [Header("Settings")]
    [SerializeField] private float hitStopDuration = 0.05f;
    [SerializeField] private float screenShakeOnHit = 0.3f;
    
    // Hit tracking (prevent same enemy being hit multiple times per swing)
    private HashSet<GameObject> hitEnemiesThisSwing = new HashSet<GameObject>();
    private bool isActive = false;
    
    // References
    private PlayerController playerController;
    
    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        
        if (hitboxCollider != null)
        {
            hitboxCollider.enabled = false; // Start disabled
        }
    }
    
    public void ActivateWeapon()
    {
        isActive = true;
        hitEnemiesThisSwing.Clear();
        
        if (hitboxCollider != null)
        {
            hitboxCollider.enabled = true;
        }
        
        // Play swing sound
        if (swingSound != null && audioSource != null)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(swingSound);
        }
        
        // Play slash trail VFX
        if (slashTrailVFX != null)
        {
            slashTrailVFX.Play();
        }
    }
    
    public void DeactivateWeapon()
    {
        isActive = false;
        
        if (hitboxCollider != null)
        {
            hitboxCollider.enabled = false;
        }
        
        hitEnemiesThisSwing.Clear();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        
        // Don't hit player
        if (other.transform.root == transform.root) return;
        
        // Don't hit same enemy twice in one swing
        if (hitEnemiesThisSwing.Contains(other.gameObject)) return;
        
        // Check for Health component
        Health targetHealth = other.GetComponent<Health>();
        
        if (targetHealth != null)
        {
            // Register hit
            hitEnemiesThisSwing.Add(other.gameObject);
            
            // Deal damage
            float damage = weaponData != null ? weaponData.damage : 40f;
            targetHealth.TakeDamage(damage);
            
            // Spawn impact VFX
            if (impactVFX != null)
            {
                Vector3 impactPoint = other.ClosestPoint(transform.position);
                GameObject impact = Instantiate(impactVFX, impactPoint, Quaternion.identity);
                Destroy(impact, 2f);
            }
            
            // Play hit sound
            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound, 0.8f);
            }
            
            // Hit stop effect
            if (hitStopDuration > 0)
            {
                StartCoroutine(HitStop());
            }
            
            // Screen shake
            if (playerController != null)
            {
                playerController.TriggerScreenShake(screenShakeOnHit);
            }
        }
    }
    
    private System.Collections.IEnumerator HitStop()
    {
        float originalTimeScale = Time.timeScale;
        Time.timeScale = 0.05f; // Dramatic slowdown
        
        yield return new WaitForSecondsRealtime(hitStopDuration);
        
        Time.timeScale = originalTimeScale;
    }
}
```

**Attach to BeamSaber, assign hitbox collider reference**

---

**Task 26.3: Add Screen Shake to PlayerController** *(30 minutes)*

Update `PlayerController.cs`:

```csharp
using Unity.Cinemachine;

[Header("Camera Shake")]
[SerializeField] private CinemachineImpulseSource impulseSource;

public void TriggerScreenShake(float intensity)
{
    if (impulseSource != null)
    {
        impulseSource.GenerateImpulse(Vector3.one * intensity);
    }
}
```

Ensure Player has CinemachineImpulseSource component (should from Phase 1).

---

#### **Afternoon Session (4 hours): Melee Attack System**

**Task 26.4: Create Melee Combo System** *(2.5 hours)*

`Assets/_Project/Scripts/Combat/MeleeComboSystem.cs`:

```csharp
using UnityEngine;
using System.Collections;

[System.Serializable]
public class ComboAttack
{
    public string attackName;
    public float damage;
    public float swingDuration = 0.4f;
    public float recoveryDuration = 0.2f;
    public float lungForce = 15f;
    public AnimationCurve lungeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
}

public class MeleeComboSystem : MonoBehaviour
{
    [Header("Combo Chain")]
    [SerializeField] private ComboAttack[] comboChain = new ComboAttack[3];
    
    [Header("References")]
    [SerializeField] private MeleeWeapon meleeWeapon;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Transform playerTransform;
    
    [Header("Settings")]
    [SerializeField] private float comboWindowDuration = 0.8f;
    [SerializeField] private float comboCooldown = 0.5f;
    
    // State
    private int currentComboIndex = 0;
    private bool isAttacking = false;
    private bool canCombo = false;
    private float comboWindowTimer = 0f;
    private Coroutine attackCoroutine;
    
    // Properties
    public bool IsAttacking => isAttacking;
    public bool CanAttack => !isAttacking || canCombo;
    
    private void Update()
    {
        // Update combo window timer
        if (comboWindowTimer > 0)
        {
            comboWindowTimer -= Time.deltaTime;
            
            if (comboWindowTimer <= 0)
            {
                ResetCombo();
            }
        }
    }
    
    public void PerformAttack()
    {
        if (!CanAttack) return;
        
        // If in combo window, advance to next attack
        if (canCombo && currentComboIndex < comboChain.Length - 1)
        {
            currentComboIndex++;
        }
        
        // Start attack
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        
        attackCoroutine = StartCoroutine(ExecuteAttack(comboChain[currentComboIndex]));
    }
    
    private IEnumerator ExecuteAttack(ComboAttack attack)
    {
        isAttacking = true;
        canCombo = false;
        
        // Activate weapon hitbox
        if (meleeWeapon != null)
        {
            meleeWeapon.ActivateWeapon();
        }
        
        // Apply lunge
        StartCoroutine(ApplyLunge(attack));
        
        // Visual: Show weapon (should already be visible in Assault stance)
        
        // Wait for swing duration
        yield return new WaitForSeconds(attack.swingDuration);
        
        // Deactivate hitbox
        if (meleeWeapon != null)
        {
            meleeWeapon.DeactivateWeapon();
        }
        
        // Enter recovery
        canCombo = true; // Can input next attack
        comboWindowTimer = comboWindowDuration;
        
        yield return new WaitForSeconds(attack.recoveryDuration);
        
        isAttacking = false;
        
        // If no combo input, reset after window
    }
    
    private IEnumerator ApplyLunge(ComboAttack attack)
    {
        float elapsedTime = 0f;
        float lungeDuration = attack.swingDuration;
        
        while (elapsedTime < lungeDuration)
        {
            float curveValue = attack.lungeCurve.Evaluate(elapsedTime / lungeDuration);
            Vector3 lungeForce = playerTransform.forward * attack.lungForce * curveValue;
            
            playerRb.AddForce(lungeForce, ForceMode.Acceleration);
            
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    
    public void ResetCombo()
    {
        currentComboIndex = 0;
        canCombo = false;
        comboWindowTimer = 0f;
    }
    
    public bool CanBoostCancel()
    {
        // Allow boost canceling during recovery frames
        return isAttacking && canCombo;
    }
}
```

**Attach to Player GameObject**

---

**Task 26.5: Configure Combo Chain** *(1 hour)*

**In Inspector, set up 3-hit combo:**

**Attack 1: Horizontal Slash**
```
Attack Name: "Horizontal Slash"
Damage: 40
Swing Duration: 0.4
Recovery Duration: 0.2
Lung Force: 15
Lunge Curve: EaseOut (starts fast, slows down)
```

**Attack 2: Vertical Slash**
```
Attack Name: "Vertical Slash"
Damage: 40
Swing Duration: 0.4
Recovery Duration: 0.2
Lung Force: 18
Lunge Curve: Linear
```

**Attack 3: Spinning Finisher**
```
Attack Name: "Spinning Slash"
Damage: 80
Swing Duration: 0.6
Recovery Duration: 0.4
Lung Force: 25
Lunge Curve: EaseInOut (dramatic)
```

**Assign references:**
- Melee Weapon: BeamSaber
- Player Rb: Player's Rigidbody
- Player Transform: Player's Transform

---

**Task 26.6: Integrate with WeaponManager** *(30 minutes)*

Update `WeaponManager.cs`:

```csharp
[Header("Assault Melee")]
[SerializeField] private MeleeComboSystem meleeComboSystem;
[SerializeField] private GameObject beamSaber;

private void HandleWeaponInput()
{
    // Primary weapon
    if (primaryFireHeld && primaryFireTimer <= 0)
    {
        FirePrimaryWeapon();
    }
    
    // Secondary weapon
    if (currentStance == CombatStance.Ranger)
    {
        if (secondaryFireHeld && secondaryFireTimer <= 0)
        {
            FireSecondaryWeapon();
            secondaryFireHeld = false;
        }
    }
    else if (currentStance == CombatStance.Assault)
    {
        // Melee uses primary input (L1)
        if (primaryFireHeld && meleeComboSystem != null && meleeComboSystem.CanAttack)
        {
            meleeComboSystem.PerformAttack();
            primaryFireHeld = false; // Tap only, not hold
        }
    }
}

public void SwitchStance()
{
    currentStance = (currentStance == CombatStance.Ranger) 
        ? CombatStance.Assault 
        : CombatStance.Ranger;
    
    // Show/hide appropriate weapons
    UpdateWeaponVisibility();
    
    Debug.Log($"Switched to {currentStance} stance");
    
    OnStanceChanged?.Invoke(currentStance);
}

private void UpdateWeaponVisibility()
{
    if (beamSaber != null)
    {
        beamSaber.SetActive(currentStance == CombatStance.Assault);
    }
    
    // Ranger weapons are just fire points, always invisible
}
```

---

**End of Day 26 Commit:**
```
git add .
git commit -m "Day 26: Melee weapon foundation - beam saber, hitbox, combo system"
```

---

### **📅 Day 27: Melee VFX & Animation** *(8 hours)*

**Daily Goal:** Add visual flair to melee attacks - slash trails, impact effects, and weapon motion.

---

#### **Morning Session (4 hours): Slash Trail VFX**

**Task 27.1: Create Slash Trail Particle System** *(2 hours)*

**Build streak effect:**

1. Create Particle System as child of BeamSaber: `SlashTrail`
2. Position: (0, 0, 1.5) - middle of blade
3. Settings:

```
Duration: 0.5
Looping: OFF
Play On Awake: OFF

Emission:
  Rate over Time: 100
  Bursts: None

Shape:
  Shape: Cone
  Angle: 5
  Radius: 0.1
  Length: 2.5

Start Lifetime: 0.2
Start Speed: 0.5
Start Size: 0.4
Start Color: Magenta (255, 0, 200)

Color over Lifetime:
  Gradient: Bright magenta → transparent

Size over Lifetime:
  Curve: 1.0 → 0.2 (shrinks)

Velocity over Lifetime:
  Linear: (0, 0, -2) - trails behind swing

Renderer:
  Material: Default-Particle (additive blend)
  Render Mode: Stretched Billboard
    - Speed Scale: 0.3
    - Length Scale: 2.0
```

**Assign in MeleeWeapon component: Slash Trail VFX**

---

**Task 27.2: Melee Impact VFX** *(1.5 hours)*

**Create spark burst:**

1. Create Particle System: `MeleeImpact`
2. Settings:

```
Duration: 0.3
Looping: OFF

Emission:
  Bursts: 1 burst of 20 particles at time 0

Shape:
  Shape: Sphere
  Radius: 0.5

Start Lifetime: 0.3-0.5
Start Speed: 8-15
Start Size: 0.2-0.4
Start Color: White to yellow gradient

Color over Lifetime:
  White → Yellow → Orange → Transparent

Size over Lifetime:
  1.0 → 0.1

Gravity Modifier: 0.3 (slight fall)

Renderer:
  Render Mode: Billboard
  Material: Default-Particle
```

3. Make prefab
4. Assign to MeleeWeapon: Impact VFX

---

**Task 27.3: Screen Distortion on Heavy Hit** *(30 minutes)*

**Add post-processing pulse:**

Update `DynamicPostProcessing.cs`:

```csharp
using UnityEngine.Rendering.Universal;

[Header("Melee Hit Effect")]
[SerializeField] private float meleeHitChromaticAberration = 0.5f;
[SerializeField] private float meleeHitDuration = 0.1f;

private ChromaticAberration chromaticAberration;
private float hitEffectTimer = 0f;

private void Start()
{
    // ... existing code ...
    postProcessVolume.profile.TryGet(out chromaticAberration);
}

public void TriggerMeleeHitEffect()
{
    hitEffectTimer = meleeHitDuration;
}

private void Update()
{
    // ... existing code ...
    UpdateMeleeHitEffect();
}

private void UpdateMeleeHitEffect()
{
    if (chromaticAberration == null) return;
    
    if (hitEffectTimer > 0)
    {
        hitEffectTimer -= Time.deltaTime;
        float intensity = (hitEffectTimer / meleeHitDuration) * meleeHitChromaticAberration;
        chromaticAberration.intensity.value = intensity;
    }
    else
    {
        chromaticAberration.intensity.value = Mathf.Lerp(
            chromaticAberration.intensity.value, 
            0f, 
            Time.deltaTime * 5f
        );
    }
}
```

**Enable Chromatic Aberration in post-processing volume**

**Call from MeleeWeapon:**
```csharp
// In OnTriggerEnter after hit
FindObjectOfType<DynamicPostProcessing>()?.TriggerMeleeHitEffect();
```

---

#### **Afternoon Session (4 hours): Weapon Motion & Audio**

**Task 27.4: Procedural Weapon Swing** *(2 hours)*

**Animate blade during attack:**

`Assets/_Project/Scripts/Combat/MeleeWeaponAnimator.cs`:

```csharp
using UnityEngine;
using System.Collections;

public class MeleeWeaponAnimator : MonoBehaviour
{
    [Header("Swing Animation")]
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private Vector3 idlePosition = new Vector3(0.5f, 0, 0.8f);
    [SerializeField] private Vector3 idleRotation = Vector3.zero;
    
    [Header("Attack 1: Horizontal")]
    [SerializeField] private Vector3 horizontal_StartRot = new Vector3(0, 0, -90);
    [SerializeField] private Vector3 horizontal_EndRot = new Vector3(0, 0, 90);
    
    [Header("Attack 2: Vertical")]
    [SerializeField] private Vector3 vertical_StartRot = new Vector3(-90, 0, 0);
    [SerializeField] private Vector3 vertical_EndRot = new Vector3(90, 0, 0);
    
    [Header("Attack 3: Spin")]
    [SerializeField] private Vector3 spin_Rotation = new Vector3(0, 720, 0); // Two full spins
    
    private Vector3 currentTargetPos;
    private Vector3 currentTargetRot;
    
    private void Start()
    {
        if (weaponTransform == null)
            weaponTransform = transform;
        
        ResetToIdle();
    }
    
    public void PlaySwing(int comboIndex, float duration)
    {
        StopAllCoroutines();
        
        switch (comboIndex)
        {
            case 0:
                StartCoroutine(SwingAnimation(horizontal_StartRot, horizontal_EndRot, duration));
                break;
            case 1:
                StartCoroutine(SwingAnimation(vertical_StartRot, vertical_EndRot, duration));
                break;
            case 2:
                StartCoroutine(SpinAnimation(spin_Rotation, duration));
                break;
        }
    }
    
    public void ResetToIdle()
    {
        StopAllCoroutines();
        weaponTransform.localPosition = idlePosition;
        weaponTransform.localEulerAngles = idleRotation;
    }
    
    private IEnumerator SwingAnimation(Vector3 startRot, Vector3 endRot, float duration)
    {
        float elapsed = 0f;
        
        // Wind-up (20% of duration)
        float windUpTime = duration * 0.2f;
        while (elapsed < windUpTime)
        {
            float t = elapsed / windUpTime;
            weaponTransform.localEulerAngles = Vector3.Lerp(idleRotation, startRot, t);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        // Swing (80% of duration)
        elapsed = 0f;
        float swingTime = duration * 0.8f;
        while (elapsed < swingTime)
        {
            float t = elapsed / swingTime;
            weaponTransform.localEulerAngles = Vector3.Lerp(startRot, endRot, t);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        // Return to idle
        yield return StartCoroutine(ReturnToIdle(0.2f));
    }
    
    private IEnumerator SpinAnimation(Vector3 spinAmount, float duration)
    {
        float elapsed = 0f;
        Vector3 startRot = weaponTransform.localEulerAngles;
        
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            weaponTransform.localEulerAngles = startRot + (spinAmount * t);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        yield return StartCoroutine(ReturnToIdle(0.3f));
    }
    
    private IEnumerator ReturnToIdle(float duration)
    {
        float elapsed = 0f;
        Vector3 startRot = weaponTransform.localEulerAngles;
        
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            weaponTransform.localEulerAngles = Vector3.Lerp(startRot, idleRotation, t);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
```

**Attach to BeamSaber, assign weapon transform**

**Integrate with combo system:**

```csharp
// In MeleeComboSystem
[SerializeField] private MeleeWeaponAnimator weaponAnimator;

private IEnumerator ExecuteAttack(ComboAttack attack)
{
    isAttacking = true;
    canCombo = false;
    
    // Trigger weapon animation
    if (weaponAnimator != null)
    {
        weaponAnimator.PlaySwing(currentComboIndex, attack.swingDuration);
    }
    
    // ... rest of code
}
```

---

**Task 27.5: Melee Audio Design** *(1.5 hours)*

**Find/create these sounds:**

**SwingSounds (3 variations):**
- "sword whoosh 1"
- "sword whoosh 2"
- "sword whoosh 3"

**HitSounds (2 variations):**
- "sword impact metal 1"
- "sword impact metal 2"

**Create audio variety script:**

```csharp
// In MeleeWeapon
[Header("Audio Arrays")]
[SerializeField] private AudioClip[] swingSounds;
[SerializeField] private AudioClip[] hitSounds;

public void ActivateWeapon()
{
    // ... existing code ...
    
    // Play random swing sound
    if (swingSounds.Length > 0 && audioSource != null)
    {
        AudioClip randomSwing = swingSounds[Random.Range(0, swingSounds.Length)];
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(randomSwing);
    }
}

private void OnTriggerEnter(Collider other)
{
    // ... existing damage code ...
    
    // Play random hit sound
    if (hitSounds.Length > 0 && audioSource != null)
    {
        AudioClip randomHit = hitSounds[Random.Range(0, hitSounds.Length)];
        audioSource.PlayOneShot(randomHit, 0.8f);
    }
}
```

---

**Task 27.6: Melee Feedback Polish** *(30 minutes)*

**Tuning pass:**

```
Hit Stop Duration:
- Light attacks (1-2): 0.03s
- Heavy attack (3): 0.08s

Screen Shake:
- Light: 0.2 intensity
- Heavy: 0.5 intensity

Lunge Force:
- Attack 1: 15 (moderate)
- Attack 2: 18 (faster)
- Attack 3: 25 (dramatic)

Audio Volume:
- Swing: 0.6
- Hit: 0.8
- Finisher hit: 1.0
```

---

**End of Day 27 Commit:**
```
git add .
git commit -m "Day 27: Melee VFX and audio - slash trails, impacts, weapon animation"
```

---

### **📅 Day 28: Boost-Canceling & Combat Flow** *(8 hours)*

**Daily Goal:** Implement the critical boost-cancel mechanic that allows fluid transitions between offense and evasion.

---

#### **Morning Session (4 hours): Boost Cancel System**

**Task 28.1: Enable Boost Canceling** *(2 hours)*

**Update PlayerController to allow cancel:**

```csharp
// In PlayerController
[Header("Boost Canceling")]
[SerializeField] private WeaponManager weaponManager;
[SerializeField] private MeleeComboSystem meleeCombo;

private void OnQuickBoostPerformed(InputAction.CallbackContext context)
{
    // Check if we can boost
    if (currentBoost >= quickBoostCost && quickBoostCooldownTimer <= 0)
    {
        // Check if canceling an action
        bool canceledAction = TryBoostCancel();
        
        // Execute boost
        ExecuteQuickBoost();
    }
}

private bool TryBoostCancel()
{
    bool didCancel = false;
    
    // Cancel melee combo
    if (meleeCombo != null && meleeCombo.IsAttacking)
    {
        if (meleeCombo.CanBoostCancel())
        {
            meleeCombo.ResetCombo();
            didCancel = true;
        }
    }
    
    // Cancel weapon fire recovery (already works, but formalize)
    // This is the "recovery frame" concept from fighting games
    
    return didCancel;
}
```

---

**Task 28.2: Visual Cancel Feedback** *(1 hour)*

**Make cancels feel impactful:**

```csharp
// In PlayerController
private bool TryBoostCancel()
{
    bool didCancel = false;
    
    if (meleeCombo != null && meleeCombo.IsAttacking)
    {
        if (meleeCombo.CanBoostCancel())
        {
            meleeCombo.ResetCombo();
            didCancel = true;
            
            // Visual feedback for successful cancel
            TriggerCancelEffect();
        }
    }
    
    return didCancel;
}

private void TriggerCancelEffect()
{
    // Small screen flash
    if (impulseSource != null)
    {
        impulseSource.GenerateImpulse(Vector3.one * 0.15f);
    }
    
    // Spawn cancel VFX (optional)
    // Play "whoosh" sound
}
```

---

**Task 28.3: Advanced Cancel Techniques** *(1 hour)*

**Document cancel mechanics:**

Create: `Assets/_Project/CombatMechanics.md`

```markdown
# Combat Mechanics Guide

## Boost Canceling

### What It Is
Pressing Quick Boost (Circle/B) during certain actions will 
immediately cancel them and execute the boost. This allows 
you to:
- Escape danger mid-combo
- Reposition during attacks
- Chain offense into defense seamlessly

### When You Can Cancel

**Melee Attacks:**
- After hit 1 connects: Can cancel into boost
- After hit 2 connects: Can cancel into boost
- During hit 3 windup: CANNOT cancel (committed)
- During hit 3 recovery: Can cancel into boost

**Ranged Attacks:**
- Immediately after firing: Can cancel recoil

**Movement:**
- Can always boost (no cancel needed)

### Advanced Techniques

**Melee Hit-Confirm Cancel:**
1. Hit enemy with attack 1
2. Immediately boost-cancel
3. Circle around enemy
4. Re-engage with full combo

**Shotgun Melee:**
1. Close distance with boost
2. Switch to Ranger
3. Fire point-blank
4. Boost-cancel backward
5. Repeat

**Momentum Juggling:**
1. Drift at high speed
2. Quick boost forward (stacks speed)
3. Melee attack 3 (lunge adds more speed)
4. Boost-cancel to safety
5. You're now very far from start point
```

---

#### **Afternoon Session (4 hours): Combat Flow Testing**

**Task 28.4: Create Combat Training Course** *(1.5 hours)*

**Build practice arena:**

1. Create new scene or section: `CombatTraining`
2. Spawn 15 DummyTargets in formations:
   - 5 in a line (ranged practice)
   - 5 in a circle (melee practice)
   - 5 scattered (mixed combat)

**Add respawn script:**

`Assets/_Project/Scripts/Gameplay/TargetRespawner.cs`:

```csharp
using UnityEngine;
using System.Collections;

public class TargetRespawner : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float respawnDelay = 3f;
    
    private GameObject currentTarget;
    
    private void Start()
    {
        SpawnTarget();
    }
    
    private void SpawnTarget()
    {
        currentTarget = Instantiate(targetPrefab, spawnPoint.position, spawnPoint.rotation);
        
        // Subscribe to death event
        Health health = currentTarget.GetComponent<Health>();
        if (health != null)
        {
            health.OnDeath += OnTargetDied;
        }
    }
    
    private void OnTargetDied()
    {
        StartCoroutine(RespawnAfterDelay());
    }
    
    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnTarget();
    }
}
```

**This creates infinite practice targets**

---

**Task 28.5: Combat Challenge Scenarios** *(1.5 hours)*

**Create skill tests:**

**Challenge 1: Ranged Accuracy**
```
Setup: 10 stationary targets at 100m
Goal: Destroy all with <20 shots (>50% accuracy)
Time Limit: 30 seconds
```

**Challenge 2: Melee Speed**
```
Setup: 10 targets in circle (20m radius)
Goal: Destroy all with melee only
Time Limit: 20 seconds
Skill: Boost between targets, cancel combos
```

**Challenge 3: Mixed Combat**
```
Setup: 5 ranged targets, 5 close targets
Goal: Destroy all, any method
Time Limit: 30 seconds
Skill: Stance switching, positioning
```

**Challenge 4: Cancel Mastery**
```
Setup: 1 target, you have low boost
Goal: Hit with melee 10 times without taking damage
Skill: Must cancel to dodge, manage boost
```

Build these as separate GameObject groups, toggle on/off.

---

**Task 28.6: Combat Feel Tuning** *(1 hour)*

**Full combat loop test:**

**Protocol:**
1. Spawn 10 mixed targets
2. Engage with only ranged → destroy 5
3. Engage with only melee → destroy 5
4. Time yourself

**Evaluation:**

```
Ranged Combat:
- [ ] Lock-on helps but isn't required
- [ ] Projectiles feel responsive
- [ ] Killing enemy is satisfying
- [ ] Can defeat 5 targets in <20 seconds

Melee Combat:
- [ ] Lunge closes distance effectively
- [ ] 3-hit combo flows naturally
- [ ] Hit-stop feels impactful
- [ ] Boost-cancel is intuitive
- [ ] Can defeat 5 targets in <15 seconds

Stance Switching:
- [ ] Can switch mid-combat smoothly
- [ ] Clear visual distinction between stances
- [ ] Each stance has clear use case
```

**Adjust values until all boxes check**

---

**End of Day 28 Commit:**
```
git add .
git commit -m "Day 28: Boost-cancel system, combat flow, training scenarios"
```

---

### **📅 Day 29-30: Polish & Integration** *(16 hours)*

**Goal:** Final week polish for melee systems.

---

#### **Day 29 Morning: Secondary Weapons** *(4 hours)*

**Task 29.1: Implement Thrown Beam Axe** *(2 hours)*

**Create weapon data:**

Create ScriptableObject: `WD_BeamAxe`
```
Weapon Name: "Beam Axe"
Weapon Type: Projectile
Damage: 60
Fire Rate: 60 RPM (1 shot/second)
Projectile Speed: 80
Range: 100
```

**Create axe projectile:**

1. Create primitive: Cube + Cylinder for axe head shape
2. Scale: (0.5, 1.5, 0.3)
3. Add material: M_BeamAxe (orange emissive)
4. Add Rigidbody
5. Add `Projectile.cs`
6. **Add rotation script:**

```csharp
// Assets/_Project/Scripts/Combat/SpinningProjectile.cs
using UnityEngine;

public class SpinningProjectile : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 720f;
    [SerializeField] private Vector3 spinAxis = Vector3.right;
    
    private void Update()
    {
        transform.Rotate(spinAxis, spinSpeed * Time.deltaTime, Space.Self);
    }
}
```

7. Make prefab: `BeamAxe_Projectile`

**Add homing (optional):**

```csharp
// In Projectile.cs
[SerializeField] private bool hasHoming = false;
[SerializeField] private float homingStrength = 15f;
private Transform homingTarget;

public void SetHomingTarget(Transform target)
{
    homingTarget = target;
}

private void FixedUpdate()
{
    if (hasHoming && homingTarget != null)
    {
        Vector3 targetDirection = (homingTarget.position - transform.position).normalized;
        Vector3 newDirection = Vector3.RotateTowards(
            rb.velocity.normalized,
            targetDirection,
            homingStrength * Mathf.Deg2Rad * Time.fixedDeltaTime,
            0f
        );
        
        rb.velocity = newDirection * speed;
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }
}
```

---

**Task 29.2: Integrate Axe with Assault Stance** *(1 hour)*

```csharp
// In WeaponManager
private void FireSecondaryWeapon()
{
    WeaponData weapon = ActiveSecondaryWeapon;
    if (weapon == null) return;
    
    if (currentStance == CombatStance.Assault)
    {
        // Fire beam axe
        Transform firePoint = assaultFirePoint;
        FireProjectile(weapon, firePoint);
        
        // If targeting system has lock, give axe homing
        if (targetingSystem != null && targetingSystem.IsLockedOn)
        {
            GameObject lastProjectile = /* store reference to last spawned projectile */;
            Projectile proj = lastProjectile.GetComponent<Projectile>();
            proj?.SetHomingTarget(targetingSystem.CurrentTarget.Transform);
        }
    }
    
    secondaryFireTimer = weapon.TimeBetweenShots;
}
```

---

**Task 29.3: Grenade Launcher (Ranger Secondary)** *(1 hour)*

**Create grenade projectile:**

1. Sphere primitive: `GrenadeProjectile`
2. Add Rigidbody with gravity
3. Arc trajectory script:

```csharp
// Assets/_Project/Scripts/Combat/GrenadeProjectile.cs
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    [SerializeField] private float damage = 50f;
    [SerializeField] private float explosionRadius = 15f;
    [SerializeField] private float fuseTime = 3f;
    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private LayerMask damageableLayers;
    
    private Rigidbody rb;
    private float timer;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = fuseTime;
    }
    
    public void Launch(Vector3 velocity)
    {
        rb.velocity = velocity;
    }
    
    private void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            Explode();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // Bounce a bit, then explode on next hit
        if (rb.velocity.magnitude < 5f)
        {
            Explode();
        }
    }
    
    private void Explode()
    {
        // Spawn VFX
        if (explosionVFX != null)
        {
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
        }
        
        // Area damage
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, damageableLayers);
        
        foreach (Collider hit in hits)
        {
            Health health = hit.GetComponent<Health>();
            if (health != null)
            {
                // Falloff damage based on distance
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                float damageMultiplier = 1f - (distance / explosionRadius);
                float finalDamage = damage * damageMultiplier;
                
                health.TakeDamage(finalDamage);
            }
        }
        
        Destroy(gameObject);
    }
}
```

**Launch with arc:**

```csharp
// In WeaponManager, special handling for grenades
if (weapon.weaponType == WeaponType.Grenade)
{
    Vector3 launchVelocity = firePoint.forward * weapon.projectileSpeed;
    launchVelocity += Vector3.up * 20f; // Arc upward
    
    GameObject grenade = Instantiate(weapon.projectilePrefab, firePoint.position, Quaternion.identity);
    grenade.GetComponent<GrenadeProjectile>()?.Launch(launchVelocity);
}
```

---

#### **Day 29 Afternoon: UI & Feedback** *(4 hours)*

**Task 29.4: Combo Counter UI** *(2 hours)*

**Create combo display:**

`Assets/_Project/Scripts/UI/ComboCounter.cs`:

```csharp
using UnityEngine;
using TMPro;

public class ComboCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private CanvasGroup canvasGroup;
    
    [SerializeField] private float comboTimeout = 2f;
    [SerializeField] private float fadeSpeed = 3f;
    
    private int comboCount = 0;
    private float comboTimer = 0f;
    
    private void Start()
    {
        canvasGroup.alpha = 0f;
    }
    
    private void Update()
    {
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
            
            // Fade in
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1f, fadeSpeed * Time.deltaTime);
            
            if (comboTimer <= 0)
            {
                ResetCombo();
            }
        }
        else
        {
            // Fade out
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0f, fadeSpeed * Time.deltaTime);
        }
    }
    
    public void AddHit()
    {
        comboCount++;
        comboTimer = comboTimeout;
        
        UpdateDisplay();
    }
    
    private void UpdateDisplay()
    {
        comboText.text = $"{comboCount} HITS";
        
        int multiplier = Mathf.Min(comboCount / 5, 10); // Max 10x
        multiplierText.text = multiplier > 1 ? $"x{multiplier}" : "";
        
        // Scale pulse
        transform.localScale = Vector3.one * 1.2f;
        LeanTween.scale(gameObject, Vector3.one, 0.1f).setEaseOutBack();
    }
    
    private void ResetCombo()
    {
        comboCount = 0;
        comboTimer = 0f;
    }
}
```

**Add to Canvas, subscribe to damage events**

---

**Task 29.5: Weapon Swap Animation** *(1 hour)*

**Visual weapon switch:**

```csharp
// In WeaponManager
[SerializeField] private float stanceSwapDuration = 0.3f;

public void SwitchStance()
{
    currentStance = (currentStance == CombatStance.Ranger) 
        ? CombatStance.Assault 
        : CombatStance.Ranger;
    
    StartCoroutine(AnimateStanceSwap());
    
    OnStanceChanged?.Invoke(currentStance);
}

private IEnumerator AnimateStanceSwap()
{
    // Brief slow-mo
    Time.timeScale = 0.5f;
    yield return new WaitForSecondsRealtime(0.1f);
    Time.timeScale = 1f;
    
    // Update visibility
    UpdateWeaponVisibility();
    
    // Play swap sound
    // Show UI indicator pulse
}
```

---

**Task 29.6: Crosshair Customization** *(1 hour)*

**Different crosshairs per stance:**

```csharp
// Assets/_Project/Scripts/UI/DynamicCrosshair.cs
using UnityEngine;
using UnityEngine.UI;

public class DynamicCrosshair : MonoBehaviour
{
    [SerializeField] private Image crosshairImage;
    [SerializeField] private Sprite rangerCrosshair;
    [SerializeField] private Sprite assaultCrosshair;
    [SerializeField] private WeaponManager weaponManager;
    
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color enemyColor = Color.red;
    
    private void Start()
    {
        weaponManager.OnStanceChanged += UpdateCrosshair;
        UpdateCrosshair(weaponManager.CurrentStance);
    }
    
    private void UpdateCrosshair(CombatStance stance)
    {
        crosshairImage.sprite = (stance == CombatStance.Ranger) 
            ? rangerCrosshair 
            : assaultCrosshair;
    }
    
    private void Update()
    {
        // Change color when over enemy
        // Raycast from center screen
        // If hit enemy, turn red
    }
}
```

---

#### **Day 30: Final Polish** *(8 hours)*

**Task 30.1: Combat Tutorial Messages** *(2 hours)*

**In-game tips:**

```csharp
// Assets/_Project/Scripts/UI/TutorialMessages.cs
using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialMessages : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private CanvasGroup messageGroup;
    
    private Queue<string> messageQueue = new Queue<string>();
    
    private void Start()
    {
        // Queue tutorial messages
        QueueMessage("Press R1 to fire your weapon", 3f);
        QueueMessage("Press Triangle to switch stances", 3f);
        QueueMessage("In Assault stance, press L1 to melee", 3f);
        QueueMessage("Press Circle during attacks to boost-cancel", 4f);
    }
    
    public void QueueMessage(string message, float duration)
    {
        StartCoroutine(ShowMessage(message, duration));
    }
    
    private IEnumerator ShowMessage(string message, float duration)
    {
        messageText.text = message;
        
        // Fade in
        float elapsed = 0f;
        while (elapsed < 0.5f)
        {
            messageGroup.alpha = Mathf.Lerp(0, 1, elapsed / 0.5f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        // Hold
        yield return new WaitForSeconds(duration);
        
        // Fade out
        elapsed = 0f;
        while (elapsed < 0.5f)
        {
            messageGroup.alpha = Mathf.Lerp(1, 0, elapsed / 0.5f);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
```

---

**Task 30.2: Performance Pass** *(2 hours)*

**Optimize combat systems:**

```
1. Object Pooling for projectiles (if not already)
2. Limit active VFX particles
3. Reduce trail renderer points
4. Optimize hitbox checks (use layers)
5. Profile melee combo (should be <1ms)
```

---

**Task 30.3: Bug Fixing** *(2 hours)*

**Common combat bugs:**

```
Issue: Melee hitbox stays active
Fix: Ensure DeactivateWeapon() is always called

Issue: Can't switch stance during combat
Fix: Allow stance switch, cancel current action

Issue: Boost-cancel doesn't work sometimes
Fix: Check cooldown timer isn't blocking

Issue: Damage applies multiple times
Fix: HashSet tracking in MeleeWeapon working?

Issue: Lock-on switches targets randomly
Fix: Increase lock-off angle, or add sticky timer
```

---

**Task 30.4: Final Tuning** *(2 hours)*

**Balance pass:**

```markdown
# Week 6 Final Values

## Beam Saber
- Attack 1: 40 damage, 0.4s swing, 15 lunge
- Attack 2: 40 damage, 0.4s swing, 18 lunge
- Attack 3: 80 damage, 0.6s swing, 25 lunge
- Total combo: 160 damage, 1.4 seconds
- DPS: ~114 (higher than rifle's ~75)

## Beam Axe
- Damage: 60
- Speed: 80 m/s
- Cooldown: 1 second
- Homing: 15° correction

## Combat Feel
- Melee should kill target in 2 full combos (160×2=320 vs 300 HP)
- Rifle should kill in ~20 shots (15×20=300)
- Axe should kill in 5 hits (60×5=300)

## Target Health (adjusted)
- Dummy Target: 300 HP (was 100)
- This makes combat last longer, more satisfying
```

---

**End of Week 6 Commit:**
```
git add .
git commit -m "Week 6 Complete: Assault stance fully implemented with melee combat, secondary weapons, polish"
git tag "Phase2-Week6-Complete"
```

---

## **🎯 Week 6 Deliverables Checklist**

**Before Week 7:**

### **Functional**
- [ ] Beam Saber with 3-hit combo
- [ ] Melee hitbox detection
- [ ] Lunge movement on attacks
- [ ] Hit-stop on impacts
- [ ] Boost-cancel system working
- [ ] Beam Axe thrown weapon
- [ ] Grenade launcher
- [ ] Stance switching UI

### **Feedback**
- [ ] Slash trail VFX
- [ ] Melee impact sparks
- [ ] Screen shake on heavy hits
- [ ] Weapon swing sounds (3 variations)
- [ ] Impact sounds (2 variations)
- [ ] Procedural weapon animation
- [ ] Combo counter UI

### **Polish**
- [ ] Crosshair changes per stance
- [ ] Tutorial messages
- [ ] Combat training area
- [ ] All weapons balanced

### **Performance**
- [ ] 60fps with melee combat active
- [ ] No hitbox leaks or double-hits

---

**Week 6 is COMPLETE! Combat now has depth with ranged AND melee.**

---

## **🗓️ Week 7: The Enemy - Creating a Threat**

**Weekly Goal:** Transform passive dummy targets into active, threatening enemies with AI behavior. Create three distinct enemy types that force players to use different tactics.

**Total Time Budget:** 40 hours  
**Daily Breakdown:** 8 hours/day × 5 days

**Week 7 Success Criteria:**
- ✅ Enemies actively pursue and attack player
- ✅ Three enemy types with distinct behaviors
- ✅ Player can take damage and die
- ✅ Combat scenarios require tactical thinking
- ✅ AI performs well (no stuttering or errors)
- ✅ Enemy attacks are telegraphed and fair

---

### **📅 Day 31: AI State Machine Foundation** *(8 hours)*

**Daily Goal:** Build a robust, reusable AI system that all enemies will use.

---

#### **Morning Session (4 hours): State Machine Architecture**

**Task 31.1: Create Base Enemy AI** *(2.5 hours)*

`Assets/_Project/Scripts/AI/EnemyAI.cs`:

```csharp
using UnityEngine;
using System.Collections;

public enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Retreat,
    Dead
}

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Health))]
public class EnemyAI : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] protected EnemyState currentState = EnemyState.Idle;
    [SerializeField] protected float detectionRange = 200f;
    [SerializeField] protected float attackRange = 100f;
    [SerializeField] protected float retreatRange = 30f;
    [SerializeField] protected float loseTargetRange = 300f;
    
    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 20f;
    [SerializeField] protected float chaseSpeed = 30f;
    [SerializeField] protected float rotationSpeed = 3f;
    
    [Header("Attack Settings")]
    [SerializeField] protected float attackCooldown = 2f;
    protected float attackTimer = 0f;
    
    [Header("References")]
    protected Transform player;
    protected Rigidbody rb;
    protected Health health;
    
    // State tracking
    protected Vector3 lastKnownPlayerPosition;
    protected float distanceToPlayer;
    
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
        rb.useGravity = false; // Space combat
    }
    
    protected virtual void Start()
    {
        // Find player
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        
        // Subscribe to health events
        if (health != null)
        {
            health.OnDeath += OnDeath;
        }
    }
    
    protected virtual void Update()
    {
        if (currentState == EnemyState.Dead) return;
        
        if (player != null)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.position);
            lastKnownPlayerPosition = player.position;
        }
        
        UpdateState();
        ExecuteState();
        
        // Update timers
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
    }
    
    protected virtual void UpdateState()
    {
        if (player == null) return;
        
        switch (currentState)
        {
            case EnemyState.Idle:
                if (distanceToPlayer <= detectionRange)
                {
                    ChangeState(EnemyState.Chase);
                }
                break;
            
            case EnemyState.Chase:
                if (distanceToPlayer <= attackRange)
                {
                    ChangeState(EnemyState.Attack);
                }
                else if (distanceToPlayer > loseTargetRange)
                {
                    ChangeState(EnemyState.Idle);
                }
                break;
            
            case EnemyState.Attack:
                if (distanceToPlayer > attackRange * 1.5f)
                {
                    ChangeState(EnemyState.Chase);
                }
                else if (distanceToPlayer < retreatRange && health.HealthPercent < 0.3f)
                {
                    ChangeState(EnemyState.Retreat);
                }
                break;
            
            case EnemyState.Retreat:
                if (distanceToPlayer > attackRange)
                {
                    ChangeState(EnemyState.Chase);
                }
                break;
        }
    }
    
    protected virtual void ExecuteState()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                State_Idle();
                break;
            case EnemyState.Chase:
                State_Chase();
                break;
            case EnemyState.Attack:
                State_Attack();
                break;
            case EnemyState.Retreat:
                State_Retreat();
                break;
        }
    }
    
    protected virtual void State_Idle()
    {
        // Do nothing, or patrol
    }
    
    protected virtual void State_Chase()
    {
        MoveTowards(lastKnownPlayerPosition, chaseSpeed);
        RotateTowards(lastKnownPlayerPosition);
    }
    
    protected virtual void State_Attack()
    {
        // Face player
        RotateTowards(lastKnownPlayerPosition);
        
        // Maintain distance
        if (distanceToPlayer < attackRange * 0.8f)
        {
            MoveAwayFrom(lastKnownPlayerPosition, moveSpeed * 0.5f);
        }
        
        // Attack logic (override in subclasses)
        if (attackTimer <= 0)
        {
            PerformAttack();
        }
    }
    
    protected virtual void State_Retreat()
    {
        MoveAwayFrom(lastKnownPlayerPosition, moveSpeed);
        RotateTowards(lastKnownPlayerPosition); // Keep facing while retreating
    }
    
    protected virtual void PerformAttack()
    {
        // Override in subclasses
        attackTimer = attackCooldown;
    }
    
    protected void MoveTowards(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        rb.AddForce(direction * speed, ForceMode.Acceleration);
        
        // Limit speed
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }
    
    protected void MoveAwayFrom(Vector3 target, float speed)
    {
        Vector3 direction = (transform.position - target).normalized;
        rb.AddForce(direction * speed, ForceMode.Acceleration);
    }
    
    protected void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
    
    protected virtual void ChangeState(EnemyState newState)
    {
        currentState = newState;
        OnStateEnter(newState);
    }
    
    protected virtual void OnStateEnter(EnemyState state)
    {
        // Override for state-specific setup
    }
    
    protected virtual void OnDeath()
    {
        currentState = EnemyState.Dead;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }
    
    protected virtual void OnDestroy()
    {
        if (health != null)
        {
            health.OnDeath -= OnDeath;
        }
    }
    
    // Debug
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
```

This is the base class. All enemy types will inherit from this.

---

**Task 31.2: Create Enemy Type: Grunt** *(1.5 hours)*

**Basic ranged attacker:**

`Assets/_Project/Scripts/AI/EnemyGrunt.cs`:

```csharp
using UnityEngine;

public class EnemyGrunt : EnemyAI
{
    [Header("Grunt Weapon")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 150f;
    [SerializeField] private float projectileDamage = 10f;
    
    [Header("Audio")]
    [SerializeField] private AudioClip fireSound;
    private AudioSource audioSource;
    
    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    protected override void PerformAttack()
    {
        if (player == null || projectilePrefab == null || firePoint == null)
        {
            return;
        }
        
        // Fire projectile at player
        Vector3 aimDirection = (player.position - firePoint.position).normalized;
        
        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.LookRotation(aimDirection)
        );
        
        // Initialize projectile
        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.Initialize(projectileDamage, projectileSpeed, 500f);
        }
        
        // Audio
        if (fireSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
        
        // Set cooldown
        attackTimer = attackCooldown;
    }
}
```

---

#### **Afternoon Session (4 hours): Enemy Prefab Creation**

**Task 31.3: Build Grunt Enemy Prefab** *(2 hours)*

**Visual design:**

1. Create Sphere: `Enemy_Grunt`
2. Scale: (1.5, 1.5, 1.5)
3. Create material: `M_EnemyGrunt`
   - Color: Blue (0, 100, 255)
   - Metallic: 0.5
   - Smoothness: 0.6

4. Add "cockpit" indicator (small cube):
   - Position: (0, 0, 0.8) - front
   - Scale: (0.3, 0.3, 0.3)
   - Color: Dark blue

5. Add components:
   - Rigidbody (Use Gravity: OFF)
   - Health (Max HP: 100)
   - EnemyGrunt script
   - AudioSource
   - Tag: "Enemy"

6. Create fire point:
   - Empty GameObject child: `FirePoint`
   - Position: (0, 0, 1)

**Create enemy projectile:**

1. Duplicate player's BeamProjectile
2. Rename: `EnemyProjectile`
3. Change material color to red
4. Make prefab

**Assign references in EnemyGrunt:**
- Projectile Prefab: EnemyProjectile
- Fire Point: FirePoint transform
- Attack Cooldown: 2 seconds
- Attack Range: 100
- Detection Range: 200

**Make Enemy_Grunt a prefab**

---

**Task 31.4: Player Damage System** *(1.5 hours)*

**Enable player to take damage:**

Player already has Health component from earlier. Now add feedback:

`Assets/_Project/Scripts/Player/PlayerDamageHandler.cs`:

```csharp
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDamageHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health health;
    [SerializeField] private Image damageFlashImage;
    [SerializeField] private Image healthBarFill;
    
    [Header("Feedback")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private float flashDuration = 0.2f;
    [SerializeField] private Color flashColor = new Color(1, 0, 0, 0.3f);
    
    private AudioSource audioSource;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        if (health != null)
        {
            health.OnDamageTaken += HandleDamage;
            health.OnHealthChanged += UpdateHealthBar;
            health.OnDeath += HandleDeath;
        }
        
        if (damageFlashImage != null)
        {
            damageFlashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0);
        }
        
        UpdateHealthBar(health.CurrentHealth, health.MaxHealth);
    }
    
    private void HandleDamage(float amount)
    {
        // Visual flash
        if (damageFlashImage != null)
        {
            StartCoroutine(DamageFlash());
        }
        
        // Audio
        if (hurtSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }
        
        // Screen shake
        PlayerController pc = GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.TriggerScreenShake(0.3f);
        }
    }
    
    private IEnumerator DamageFlash()
    {
        float elapsed = 0f;
        
        while (elapsed < flashDuration)
        {
            float alpha = Mathf.Lerp(flashColor.a, 0, elapsed / flashDuration);
            damageFlashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        damageFlashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0);
    }
    
    private void UpdateHealthBar(float current, float max)
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = current / max;
            
            // Color based on health
            if (current / max > 0.5f)
                healthBarFill.color = Color.green;
            else if (current / max > 0.25f)
                healthBarFill.color = Color.yellow;
            else
                healthBarFill.color = Color.red;
        }
    }
    
    private void HandleDeath()
    {
        Debug.Log("Player died!");
        
        // Show death screen
        // Restart level
        // Or respawn
        
        Time.timeScale = 0.3f; // Slow-mo death
        Invoke("RestartLevel", 2f);
    }
    
    private void RestartLevel()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
    
    private void OnDestroy()
    {
        if (health != null)
        {
            health.OnDamageTaken -= HandleDamage;
            health.OnHealthChanged -= UpdateHealthBar;
            health.OnDeath -= HandleDeath;
        }
    }
}
```

**Add to Player GameObject, assign UI references**

---

**Task 31.5: Create Player Health UI** *(30 minutes)*

**Add to Canvas:**

```
Canvas
└── PlayerHUD
    ├── HealthBar (Bottom Left)
    │   ├── Background (Dark gray)
    │   └── Fill (Green → Red based on health)
    └── DamageFlash (Full Screen)
        └── Image (Red, alpha 0 by default)
```

**Health Bar:**
- Anchor: Bottom Left
- Position: (20, 80)
- Size: (300, 30)
- Fill Image Type: Filled (Horizontal)

**Damage Flash:**
- Anchor: Stretch (full screen)
- Color: Red (255, 0, 0, 0)

**Assign to PlayerDamageHandler**

---

**End of Day 31 Commit:**
```
git add .
git commit -m "Day 31: Enemy AI foundation - state machine, grunt enemy, player damage system"
```

# **CRIMSON COMET: Phase 2 Development Roadmap (Week 7 Continued + Week 8)**

## **🗓️ Week 7 Continued: Enemy AI Variations**

---

### **📅 Day 32: Enemy Type - Sniper (8 hours)**

**Daily Goal:** Create a long-range enemy that forces players to use evasion and cover.

---

#### **Morning Session (4 hours): Sniper AI & Behavior**

**Task 32.1: Create Sniper AI Script** *(2 hours)*

`Assets/_Project/Scripts/AI/EnemySniper.cs`:

```csharp
using UnityEngine;
using System.Collections;

public class EnemySniper : EnemyAI
{
    [Header("Sniper Weapon")]
    [SerializeField] private GameObject chargedProjectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 300f;
    [SerializeField] private float projectileDamage = 120f; // High damage!
    
    [Header("Charge System")]
    [SerializeField] private float chargeTime = 2f;
    [SerializeField] private LineRenderer laserSight;
    [SerializeField] private Color chargeColor = Color.red;
    
    [Header("Positioning")]
    [SerializeField] private float preferredRange = 250f;
    [SerializeField] private float maxRange = 400f;
    
    private bool isCharging = false;
    private float chargeTimer = 0f;
    
    protected override void Awake()
    {
        base.Awake();
        
        // Override base ranges for sniper
        detectionRange = 400f;
        attackRange = 250f;
        retreatRange = 100f; // Get away if player gets close
        
        // Snipers are slower
        moveSpeed = 15f;
        chaseSpeed = 20f;
    }
    
    protected override void Start()
    {
        base.Start();
        
        // Setup laser sight
        if (laserSight == null)
        {
            GameObject laserObj = new GameObject("LaserSight");
            laserObj.transform.SetParent(transform);
            laserSight = laserObj.AddComponent<LineRenderer>();
            
            laserSight.startWidth = 0.05f;
            laserSight.endWidth = 0.05f;
            laserSight.material = new Material(Shader.Find("Sprites/Default"));
            laserSight.startColor = Color.clear;
            laserSight.endColor = Color.clear;
            laserSight.positionCount = 2;
        }
    }
    
    protected override void State_Attack()
    {
        // Maintain optimal range
        if (distanceToPlayer < preferredRange)
        {
            State_Retreat(); // Back up
            return;
        }
        else if (distanceToPlayer > maxRange)
        {
            State_Chase(); // Get closer
            return;
        }
        
        // Face player
        RotateTowards(lastKnownPlayerPosition);
        
        // Charge and fire
        if (!isCharging && attackTimer <= 0)
        {
            StartCoroutine(ChargeAndFire());
        }
    }
    
    private IEnumerator ChargeAndFire()
    {
        isCharging = true;
        chargeTimer = 0f;
        
        // Show laser sight
        if (laserSight != null)
        {
            laserSight.enabled = true;
        }
        
        // Charge up
        while (chargeTimer < chargeTime)
        {
            chargeTimer += Time.deltaTime;
            
            // Update laser sight
            if (laserSight != null && firePoint != null && player != null)
            {
                laserSight.SetPosition(0, firePoint.position);
                laserSight.SetPosition(1, player.position);
                
                // Fade in laser
                float chargePercent = chargeTimer / chargeTime;
                Color laserColor = Color.Lerp(Color.clear, chargeColor, chargePercent);
                laserSight.startColor = laserColor;
                laserSight.endColor = laserColor;
            }
            
            // Keep rotating to track player
            if (player != null)
            {
                RotateTowards(player.position);
            }
            
            yield return null;
        }
        
        // FIRE!
        FireSniperShot();
        
        // Hide laser
        if (laserSight != null)
        {
            laserSight.enabled = false;
        }
        
        isCharging = false;
        attackTimer = attackCooldown;
    }
    
    private void FireSniperShot()
    {
        if (player == null || chargedProjectilePrefab == null || firePoint == null)
        {
            return;
        }
        
        // Aim at player's CURRENT position (not predicted - player can dodge)
        Vector3 aimDirection = (player.position - firePoint.position).normalized;
        
        GameObject projectile = Instantiate(
            chargedProjectilePrefab,
            firePoint.position,
            Quaternion.LookRotation(aimDirection)
        );
        
        // Initialize projectile
        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.Initialize(projectileDamage, projectileSpeed, 1000f);
        }
        
        // Recoil effect
        rb.AddForce(-transform.forward * 50f, ForceMode.Impulse);
    }
    
    protected override void OnStateEnter(EnemyState state)
    {
        base.OnStateEnter(state);
        
        // If interrupted while charging, stop
        if (state != EnemyState.Attack && isCharging)
        {
            StopAllCoroutines();
            isCharging = false;
            
            if (laserSight != null)
            {
                laserSight.enabled = false;
            }
        }
    }
}
```

---

**Task 32.2: Create Sniper Visual Design** *(1.5 hours)*

**Build sniper enemy:**

1. Create Cylinder (tall and thin): `Enemy_Sniper`
2. Scale: (1, 2, 1) - taller than grunt
3. Material: `M_EnemySniper`
   - Color: Dark blue (0, 50, 150)
   - Metallic: 0.7

4. Add "scope" indicator:
   - Small sphere at top
   - Material: Glowing cyan (emissive)

5. Add components:
   - Rigidbody (Use Gravity: OFF)
   - Health (Max HP: 80) - Low health, glass cannon
   - EnemySniper script
   - AudioSource

6. Create charge projectile:
   - Duplicate EnemyProjectile
   - Rename: `SniperProjectile`
   - Scale: (0.3, 0.3, 0.3) - Larger
   - Material: Bright red, high emission
   - Trail renderer: Red trail

**Assign references:**
- Charged Projectile Prefab: SniperProjectile
- Fire Point: Create child empty at (0, 0, 2)
- Charge Time: 2 seconds
- Attack Cooldown: 5 seconds (slow but deadly)

**Make prefab**

---

**Task 32.3: Sniper Audio & VFX** *(30 minutes)*

**Find sounds:**
- Charge-up sound (high-pitched whine, 2 seconds)
- Sniper fire sound (loud crack/boom)

**Add charge VFX:**
- Particle system at fire point
- Emits during charge
- Red glowing orb that grows

---

#### **Afternoon Session (4 hours): Enemy Type - Brawler**

**Task 32.4: Create Brawler AI Script** *(2.5 hours)*

**Aggressive melee enemy:**

`Assets/_Project/Scripts/AI/EnemyBrawler.cs`:

```csharp
using UnityEngine;
using System.Collections;

public class EnemyBrawler : EnemyAI
{
    [Header("Melee Attack")]
    [SerializeField] private float meleeDamage = 60f;
    [SerializeField] private float meleeRange = 15f;
    [SerializeField] private float tackleDistance = 30f;
    [SerializeField] private float tackleForce = 50f;
    
    [Header("Melee Hitbox")]
    [SerializeField] private Collider meleeHitbox;
    
    [Header("Behavior")]
    [SerializeField] private float aggressionRange = 150f;
    [SerializeField] private bool isTackling = false;
    
    private enum BrawlerAttackType
    {
        Melee,
        Tackle
    }
    
    protected override void Awake()
    {
        base.Awake();
        
        // Brawlers are fast and aggressive
        moveSpeed = 25f;
        chaseSpeed = 60f; // Very fast!
        attackRange = 20f;
        detectionRange = 200f;
        retreatRange = 0f; // Never retreats
        
        attackCooldown = 1.5f;
        
        if (meleeHitbox != null)
        {
            meleeHitbox.enabled = false;
        }
    }
    
    protected override void State_Chase()
    {
        // Charge straight at player
        MoveTowards(lastKnownPlayerPosition, chaseSpeed);
        RotateTowards(lastKnownPlayerPosition);
        
        // Visual: Leave speed trail
    }
    
    protected override void State_Attack()
    {
        if (isTackling) return; // Don't interrupt tackle
        
        RotateTowards(lastKnownPlayerPosition);
        
        if (attackTimer <= 0)
        {
            // Choose attack based on distance
            if (distanceToPlayer <= meleeRange)
            {
                StartCoroutine(PerformMeleeCombo());
            }
            else if (distanceToPlayer <= tackleDistance)
            {
                StartCoroutine(PerformTackle());
            }
        }
    }
    
    private IEnumerator PerformMeleeCombo()
    {
        attackTimer = attackCooldown;
        
        // Two-hit melee combo
        for (int i = 0; i < 2; i++)
        {
            // Enable hitbox
            if (meleeHitbox != null)
            {
                meleeHitbox.enabled = true;
            }
            
            // Small lunge forward
            rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
            
            // Swing duration
            yield return new WaitForSeconds(0.3f);
            
            // Disable hitbox
            if (meleeHitbox != null)
            {
                meleeHitbox.enabled = false;
            }
            
            // Recovery
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    private IEnumerator PerformTackle()
    {
        attackTimer = attackCooldown * 1.5f; // Longer cooldown for tackle
        isTackling = true;
        
        // Telegraph (0.5s windup)
        Vector3 tackleDirection = (player.position - transform.position).normalized;
        
        yield return new WaitForSeconds(0.5f);
        
        // CHARGE!
        rb.velocity = Vector3.zero; // Reset velocity
        rb.AddForce(tackleDirection * tackleForce, ForceMode.Impulse);
        
        // Enable hitbox during charge
        if (meleeHitbox != null)
        {
            meleeHitbox.enabled = true;
        }
        
        // Tackle lasts 1 second
        yield return new WaitForSeconds(1f);
        
        // Disable hitbox
        if (meleeHitbox != null)
        {
            meleeHitbox.enabled = false;
        }
        
        isTackling = false;
        
        // Recovery state (vulnerable)
        yield return new WaitForSeconds(1f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (meleeHitbox != null && !meleeHitbox.enabled) return;
        
        // Check if hit player
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                float damage = isTackling ? meleeDamage * 1.5f : meleeDamage;
                playerHealth.TakeDamage(damage);
                
                // Knockback player
                Rigidbody playerRb = other.GetComponent<Rigidbody>();
                if (playerRb != null && isTackling)
                {
                    Vector3 knockbackDir = (other.transform.position - transform.position).normalized;
                    playerRb.AddForce(knockbackDir * 30f, ForceMode.Impulse);
                }
            }
        }
    }
}
```

---

**Task 32.5: Build Brawler Prefab** *(1 hour)*

1. Create Cube: `Enemy_Brawler`
2. Scale: (2, 2, 2) - Bulky
3. Material: `M_EnemyBrawler`
   - Color: Orange (255, 150, 0)
   - Metallic: 0.3

4. Add spikes (visual threat):
   - 4 small cubes protruding from sides
   - Material: Dark orange

5. Add melee hitbox:
   - Box Collider (Is Trigger: TRUE)
   - Size: (3, 3, 3)
   - Extends in front when attacking

6. Components:
   - Rigidbody (Use Gravity: OFF, Mass: 2)
   - Health (Max HP: 150) - Tanky
   - EnemyBrawler script
   - Assign meleeHitbox reference

**Make prefab**

---

**Task 32.6: Enemy Variety Testing** *(30 minutes)*

**Create mixed combat scenario:**

1. Spawn 2 Grunts
2. Spawn 1 Sniper (far away)
3. Spawn 1 Brawler (close)

**Test:**
- Do you need to prioritize targets?
- Is sniper laser visible and dodge-able?
- Is brawler threatening but beatable?

**Adjust AI parameters until combat feels balanced**

---

**End of Day 32 Commit:**
```
git add .
git commit -m "Day 32: Enemy AI variations - Sniper and Brawler types"
```

---

### **📅 Day 33: Enemy Spawning & Encounters (8 hours)**

**Daily Goal:** Create systems for spawning enemies in waves and designing combat encounters.

---

#### **Morning Session (4 hours): Wave Spawner System**

**Task 33.1: Create Wave Spawner** *(2 hours)*

`Assets/_Project/Scripts/Gameplay/WaveSpawner.cs`:

```csharp
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemyWave
{
    public string waveName;
    public GameObject[] enemyPrefabs;
    public int[] enemyCounts;
    public float spawnDelay = 1f;
    public float timeBetweenSpawns = 2f;
}

public class WaveSpawner : MonoBehaviour
{
    [Header("Waves")]
    [SerializeField] private EnemyWave[] waves;
    [SerializeField] private float timeBetweenWaves = 5f;
    
    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnRadius = 10f;
    
    [Header("Settings")]
    [SerializeField] private bool autoStart = false;
    [SerializeField] private bool loopWaves = false;
    
    private int currentWave = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();
    private bool isSpawning = false;
    
    // Events
    public event System.Action<int> OnWaveStart;
    public event System.Action<int> OnWaveComplete;
    public event System.Action OnAllWavesComplete;
    
    private void Start()
    {
        if (autoStart)
        {
            StartWaves();
        }
    }
    
    [ContextMenu("Start Waves")]
    public void StartWaves()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnWaves());
        }
    }
    
    private IEnumerator SpawnWaves()
    {
        isSpawning = true;
        
        for (int i = 0; i < waves.Length; i++)
        {
            currentWave = i;
            
            OnWaveStart?.Invoke(currentWave);
            
            yield return StartCoroutine(SpawnWave(waves[i]));
            
            // Wait for all enemies to die
            yield return StartCoroutine(WaitForWaveClear());
            
            OnWaveComplete?.Invoke(currentWave);
            
            // Delay before next wave
            if (i < waves.Length - 1)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        
        // All waves complete
        OnAllWavesComplete?.Invoke();
        
        if (loopWaves)
        {
            currentWave = 0;
            StartCoroutine(SpawnWaves());
        }
        else
        {
            isSpawning = false;
        }
    }
    
    private IEnumerator SpawnWave(EnemyWave wave)
    {
        yield return new WaitForSeconds(wave.spawnDelay);
        
        // Spawn each enemy type
        for (int i = 0; i < wave.enemyPrefabs.Length; i++)
        {
            if (i >= wave.enemyCounts.Length) break;
            
            GameObject prefab = wave.enemyPrefabs[i];
            int count = wave.enemyCounts[i];
            
            for (int j = 0; j < count; j++)
            {
                SpawnEnemy(prefab);
                yield return new WaitForSeconds(wave.timeBetweenSpawns);
            }
        }
    }
    
    private void SpawnEnemy(GameObject enemyPrefab)
    {
        if (enemyPrefab == null) return;
        
        // Choose random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        // Random position within radius
        Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
        Vector3 spawnPos = spawnPoint.position + randomOffset;
        
        // Spawn enemy
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        
        // Track enemy
        activeEnemies.Add(enemy);
        
        // Subscribe to death
        Health health = enemy.GetComponent<Health>();
        if (health != null)
        {
            health.OnDeath += () => OnEnemyDied(enemy);
        }
    }
    
    private void OnEnemyDied(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }
    
    private IEnumerator WaitForWaveClear()
    {
        while (activeEnemies.Count > 0)
        {
            // Remove null references (destroyed enemies)
            activeEnemies.RemoveAll(e => e == null);
            
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    private void OnDrawGizmos()
    {
        if (spawnPoints == null) return;
        
        Gizmos.color = Color.cyan;
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint != null)
            {
                Gizmos.DrawWireSphere(spawnPoint.position, spawnRadius);
            }
        }
    }
}
```

---

**Task 33.2: Setup Combat Arena with Waves** *(1.5 hours)*

**In Combat Test scene:**

1. Create empty GameObject: `WaveManager`
2. Add `WaveSpawner` script
3. Create spawn points:
   - 4 empty GameObjects around arena perimeter
   - Name: `SpawnPoint_North`, `_South`, `_East`, `_West`
   - Position 100m from center in each direction

**Configure waves:**

**Wave 1: Introduction**
```
Wave Name: "Scout Party"
Enemy Prefabs: [Grunt]
Enemy Counts: [3]
Spawn Delay: 2s
Time Between Spawns: 2s
```

**Wave 2: Mixed**
```
Wave Name: "Assault Team"
Enemy Prefabs: [Grunt, Brawler]
Enemy Counts: [2, 1]
Time Between Spawns: 3s
```

**Wave 3: Sniper Support**
```
Wave Name: "Long Range"
Enemy Prefabs: [Grunt, Sniper]
Enemy Counts: [3, 1]
```

**Wave 4: Boss Wave**
```
Wave Name: "Heavy Assault"
Enemy Prefabs: [Grunt, Brawler, Sniper]
Enemy Counts: [2, 2, 1]
```

**Assign spawn points array, enable auto-start**

---

**Task 33.3: Wave UI Display** *(30 minutes)*

`Assets/_Project/Scripts/UI/WaveUI.cs`:

```csharp
using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI statusText;
    
    private void Start()
    {
        if (waveSpawner != null)
        {
            waveSpawner.OnWaveStart += OnWaveStart;
            waveSpawner.OnWaveComplete += OnWaveComplete;
            waveSpawner.OnAllWavesComplete += OnAllComplete;
        }
        
        statusText.text = "Prepare for combat...";
    }
    
    private void OnWaveStart(int waveIndex)
    {
        waveText.text = $"WAVE {waveIndex + 1}";
        statusText.text = "Incoming hostiles!";
    }
    
    private void OnWaveComplete(int waveIndex)
    {
        statusText.text = "Wave cleared!";
    }
    
    private void OnAllComplete()
    {
        statusText.text = "All waves defeated!";
        waveText.text = "VICTORY";
    }
    
    private void OnDestroy()
    {
        if (waveSpawner != null)
        {
            waveSpawner.OnWaveStart -= OnWaveStart;
            waveSpawner.OnWaveComplete -= OnWaveComplete;
            waveSpawner.OnAllWavesComplete -= OnAllComplete;
        }
    }
}
```

Add to Canvas with TextMeshPro elements.

---

#### **Afternoon Session (4 hours): Combat Scenarios**

**Task 33.4: Design Combat Scenarios** *(2 hours)*

**Scenario 1: Sniper Gauntlet**
```
Setup: 3 snipers at different ranges
Goal: Destroy all without taking damage
Skill: Dodging laser sights, using cover
```

**Scenario 2: Brawler Rush**
```
Setup: 5 brawlers spawn close
Goal: Survive and destroy all
Skill: Kiting, boost management, melee vs melee
```

**Scenario 3: Mixed Squad**
```
Setup: 2 grunts (ranged cover), 1 brawler (pressure), 1 sniper (backline)
Goal: Destroy in optimal order
Skill: Target prioritization
```

Build these as separate GameObject groups with WaveSpawner.

---

**Task 33.5: Enemy Difficulty Scaling** *(1.5 hours)*

**Create difficulty modifier:**

`Assets/_Project/Scripts/Gameplay/DifficultyScaler.cs`:

```csharp
using UnityEngine;

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}

public class DifficultyScaler : MonoBehaviour
{
    [SerializeField] private Difficulty currentDifficulty = Difficulty.Normal;
    
    private static DifficultyScaler instance;
    public static Difficulty CurrentDifficulty => instance != null ? instance.currentDifficulty : Difficulty.Normal;
    
    private void Awake()
    {
        instance = this;
    }
    
    public static float GetHealthMultiplier()
    {
        switch (CurrentDifficulty)
        {
            case Difficulty.Easy: return 0.7f;
            case Difficulty.Normal: return 1.0f;
            case Difficulty.Hard: return 1.5f;
            default: return 1.0f;
        }
    }
    
    public static float GetDamageMultiplier()
    {
        switch (CurrentDifficulty)
        {
            case Difficulty.Easy: return 0.7f;
            case Difficulty.Normal: return 1.0f;
            case Difficulty.Hard: return 1.3f;
            default: return 1.0f;
        }
    }
    
    public static float GetSpeedMultiplier()
    {
        switch (CurrentDifficulty)
        {
            case Difficulty.Easy: return 0.8f;
            case Difficulty.Normal: return 1.0f;
            case Difficulty.Hard: return 1.2f;
            default: return 1.0f;
        }
    }
}
```

**Apply in enemy scripts:**

```csharp
// In EnemyAI.Awake()
protected virtual void Awake()
{
    rb = GetComponent<Rigidbody>();
    health = GetComponent<Health>();
    
    // Apply difficulty scaling
    if (health != null)
    {
        health.maxHealth *= DifficultyScaler.GetHealthMultiplier();
    }
    
    moveSpeed *= DifficultyScaler.GetSpeedMultiplier();
    chaseSpeed *= DifficultyScaler.GetSpeedMultiplier();
}
```

---

**Task 33.6: Combat Testing Session** *(30 minutes)*

**Full combat loop test:**

1. Start wave system
2. Fight through all 4 waves
3. Document:
   - Time to complete
   - Deaths
   - Which enemies are hardest
   - Which weapons are most effective

**Balance adjustments based on data**

---

**End of Day 33 Commit:**
```
git add .
git commit -m "Day 33: Wave spawning system, combat scenarios, difficulty scaling"
```

---

### **📅 Day 34-35: Enemy Polish & AI Refinement (16 hours)**

**Goal:** Make enemies feel intelligent and threatening through polish and refinement.

---

#### **Day 34 Morning: AI Improvements** *(4 hours)*

**Task 34.1: Add Dodge Behavior** *(2 hours)*

**Enemies should evade player attacks:**

```csharp
// In EnemyAI base class
[Header("Evasion")]
[SerializeField] protected float dodgeChance = 0.3f;
[SerializeField] protected float dodgeCooldown = 3f;
protected float dodgeTimer = 0f;

protected virtual void Update()
{
    // ... existing code ...
    
    if (dodgeTimer > 0)
        dodgeTimer -= Time.deltaTime;
    
    CheckForIncomingDanger();
}

protected virtual void CheckForIncomingDanger()
{
    if (dodgeTimer > 0) return;
    
    // Raycast in front to detect incoming projectiles
    RaycastHit hit;
    if (Physics.Raycast(transform.position, transform.forward, out hit, 50f))
    {
        if (hit.collider.CompareTag("PlayerProjectile"))
        {
            if (Random.value < dodgeChance)
            {
                PerformDodge();
            }
        }
    }
}

protected virtual void PerformDodge()
{
    // Quick boost to the side
    Vector3 dodgeDirection = Random.value > 0.5f ? transform.right : -transform.right;
    rb.AddForce(dodgeDirection * 30f, ForceMode.Impulse);
    
    dodgeTimer = dodgeCooldown;
}
```

Tag player projectiles as "PlayerProjectile".

---

**Task 34.2: Formation Flying for Grunts** *(1.5 hours)*

**Grunts work together:**

```csharp
// In EnemyGrunt
[Header("Formation")]
[SerializeField] private float formationRadius = 20f;
[SerializeField] private bool useFormation = true;

private Vector3 formationOffset;

protected override void Start()
{
    base.Start();
    
    if (useFormation)
    {
        // Random position in formation
        formationOffset = Random.insideUnitSphere * formationRadius;
        formationOffset.y = 0; // Keep on plane
    }
}

protected override void State_Attack()
{
    if (useFormation && player != null)
    {
        // Orbit around player at formation distance
        Vector3 targetPosition = player.position + formationOffset;
        
        if (Vector3.Distance(transform.position, targetPosition) > 5f)
        {
            MoveTowards(targetPosition, moveSpeed);
        }
    }
    
    base.State_Attack();
}
```

---

**Task 34.3: Brawler Pack Tactics** *(30 minutes)*

**Multiple brawlers coordinate attacks:**

```csharp
// Static list to track all active brawlers
private static List<EnemyBrawler> allBrawlers = new List<EnemyBrawler>();

protected override void Start()
{
    base.Start();
    allBrawlers.Add(this);
}

protected override void OnDestroy()
{
    base.OnDestroy();
    allBrawlers.Remove(this);
}

private bool IsOtherBrawlerAttacking()
{
    foreach (EnemyBrawler brawler in allBrawlers)
    {
        if (brawler != this && brawler.isTackling)
        {
            return true;
        }
    }
    return false;
}

protected override void State_Attack()
{
    // Wait if another brawler is tackling (take turns)
    if (IsOtherBrawlerAttacking())
    {
        State_Chase(); // Circle instead
        return;
    }
    
    base.State_Attack();
}
```

---

#### **Day 34 Afternoon: Enemy Feedback** *(4 hours)*

**Task 34.4: Enemy Death VFX** *(1.5 hours)*

**Create satisfying explosions:**

1. Particle System: `EnemyDeathExplosion`
2. Settings:

```
Duration: 1.0
Looping: OFF
Emission: Burst 50 particles
Shape: Sphere, radius 1
Start Speed: 10-20
Start Size: 0.5-1.5
Color: Enemy color → bright white → orange → transparent
Gravity: 0.5
```

**Add debris:**
- Sub Emitter spawns small cube pieces
- They fly outward and tumble

**Integrate:**

```csharp
// In Health.cs
[SerializeField] private GameObject deathVFX;

private void Die()
{
    // ... existing code ...
    
    if (deathVFX != null)
    {
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
        
        // Color explosion based on enemy type
        ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            var main = ps.main;
            Renderer rend = GetComponent<Renderer>();
            if (rend != null)
            {
                main.startColor = rend.material.color;
            }
        }
        
        Destroy(explosion, 3f);
    }
}
```

---

**Task 34.5: Enemy Audio Variety** *(1 hour)*

**Add enemy vocalizations:**

Find/create:
- Aggro sound (when spotting player)
- Attack sound (when firing/melee)
- Pain sound (when taking damage)
- Death sound (explosion)

**Implement:**

```csharp
// In EnemyAI
[Header("Audio")]
[SerializeField] protected AudioClip aggroSound;
[SerializeField] protected AudioClip[] attackSounds;
[SerializeField] protected AudioClip painSound;
protected AudioSource audioSource;

protected virtual void Start()
{
    // ... existing code ...
    
    audioSource = GetComponent<AudioSource>();
    if (audioSource == null)
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    
    // Subscribe to health damage
    if (health != null)
    {
        health.OnDamageTaken += OnDamageTaken;
    }
}

protected virtual void ChangeState(EnemyState newState)
{
    EnemyState previousState = currentState;
    currentState = newState;
    
    // Play aggro sound when entering chase from idle
    if (previousState == EnemyState.Idle && newState == EnemyState.Chase)
    {
        if (aggroSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(aggroSound);
        }
    }
    
    OnStateEnter(newState);
}

protected virtual void OnDamageTaken(float amount)
{
    if (painSound != null && audioSource != null)
    {
        audioSource.PlayOneShot(painSound, 0.6f);
    }
}
```

---

**Task 34.6: Enemy Health Bars** *(1 hour)*

**World-space health bars:**

`Assets/_Project/Scripts/UI/EnemyHealthBar.cs`:

```csharp
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image fillImage;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float displayRange = 100f;
    
    private Camera mainCamera;
    private Transform player;
    
    private void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        
        if (health != null)
        {
            health.OnHealthChanged += UpdateHealthBar;
        }
        
        // Initial update
        UpdateHealthBar(health.CurrentHealth, health.MaxHealth);
    }
    
    private void Update()
    {
        // Face camera
        if (canvas != null && mainCamera != null)
        {
            canvas.transform.LookAt(mainCamera.transform);
            canvas.transform.Rotate(0, 180, 0);
        }
        
        // Show/hide based on distance
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            canvas.gameObject.SetActive(distance <= displayRange);
        }
    }
    
    private void UpdateHealthBar(float current, float max)
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = current / max;
            
            // Color based on health
            if (current / max > 0.6f)
                fillImage.color = Color.green;
            else if (current / max > 0.3f)
                fillImage.color = Color.yellow;
            else
                fillImage.color = Color.red;
        }
    }
}
```

**Add to each enemy prefab:**
- World Space Canvas (above enemy)
- Background panel + Fill image
- EnemyHealthBar script

---

**Task 34.7: Enemy Spawn VFX** *(30 minutes)*

**Teleport-in effect:**

```csharp
// In WaveSpawner.SpawnEnemy()
private void SpawnEnemy(GameObject enemyPrefab)
{
    // ... spawn position code ...
    
    // Spawn teleport VFX first
    GameObject spawnVFX = Instantiate(teleportVFX, spawnPos, Quaternion.identity);
    Destroy(spawnVFX, 2f);
    
    // Delay enemy spawn slightly
    StartCoroutine(DelayedSpawn(enemyPrefab, spawnPos, 0.5f));
}

private IEnumerator DelayedSpawn(GameObject prefab, Vector3 position, float delay)
{
    yield return new WaitForSeconds(delay);
    
    GameObject enemy = Instantiate(prefab, position, Quaternion.identity);
    // ... rest of spawn code
}
```

---

#### **Day 35: AI Final Polish** *(8 hours)*

**Task 35.1: AI Debugging Tools** *(2 hours)*

**Visual debug for AI states:**

```csharp
// In EnemyAI
private void OnDrawGizmos()
{
    // ... existing gizmos ...
    
    // Draw state indicator
    Color stateColor = currentState switch
    {
        EnemyState.Idle => Color.gray,
        EnemyState.Chase => Color.yellow,
        EnemyState.Attack => Color.red,
        EnemyState.Retreat => Color.blue,
        EnemyState.Dead => Color.black,
        _ => Color.white
    };
    
    Gizmos.color = stateColor;
    Gizmos.DrawWireSphere(transform.position, 2f);
    
    // Draw line to player
    if (player != null && currentState != EnemyState.Idle)
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, player.position);
    }
}

#if UNITY_EDITOR
private void OnGUI()
{
    Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 5);
    if (screenPos.z > 0)
    {
        GUI.Label(new Rect(screenPos.x - 50, Screen.height - screenPos.y, 100, 20), 
                  $"{currentState}\nHP: {health.CurrentHealth:F0}");
    }
}
#endif
```

---

**Task 35.2: Performance Optimization** *(2 hours)*

**Optimize AI calculations:**

```csharp
// In EnemyAI
[Header("Optimization")]
[SerializeField] private float updateInterval = 0.1f; // Don't update every frame
private float updateTimer = 0f;

protected virtual void Update()
{
    if (currentState == EnemyState.Dead) return;
    
    updateTimer += Time.deltaTime;
    
    if (updateTimer >= updateInterval)
    {
        updateTimer = 0f;
        
        if (player != null)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.position);
            lastKnownPlayerPosition = player.position;
        }
        
        UpdateState();
    }
    
    ExecuteState(); // Execute every frame for smooth movement
    
    // Update timers every frame
    if (attackTimer > 0)
        attackTimer -= Time.deltaTime;
}
```

**Layer-based collision optimization:**
- Create layer "Enemy"
- Set all enemies to this layer
- `Edit > Project Settings > Physics`
- Disable Enemy-Enemy collision in matrix

---

**Task 35.3: Balance Tuning Pass** *(3 hours)*

**Final enemy values:**

Create: `EnemyBalanceSheet.md`

```markdown
# Enemy Balance Values (Final)

## Grunt
- Health: 100 HP
- Speed: 20 m/s (chase: 30 m/s)
- Damage: 10 per shot
- Fire Rate: 0.5s cooldown
- Detection Range: 200m
- Attack Range: 100m
- Threat Level: Low (1/5)
- Time to Kill (Player DPS): ~6 seconds

## Sniper
- Health: 80 HP (glass cannon)
- Speed: 15 m/s (chase: 20 m/s)
- Damage: 120 per shot (60% player health!)
- Fire Rate: 5s cooldown (2s charge)
- Detection Range: 400m
- Attack Range: 250m
- Threat Level: High (4/5)
- Time to Kill: ~5 seconds
- Counter: Dodge laser sight, close distance

## Brawler
- Health: 150 HP (tank)
- Speed: 25 m/s (chase: 60 m/s - FAST)
- Damage: 60 melee, 90 tackle
- Attack Rate: 1.5s cooldown
- Detection Range: 200m
- Attack Range: 20m
- Threat Level: Medium-High (3.5/5)
- Time to Kill: ~10 seconds
- Counter: Kite, boost away, use range

## Player Reference
- Health: 200 HP
- Beam Rifle DPS: ~75 (15 damage × 5 shots/s)
- Beam Saber DPS: ~114 (160 combo / 1.4s)

## Engagement Guidelines
- 1 Grunt: Easy
- 3 Grunts: Moderate
- 1 Sniper: Moderate (if aware)
- 1 Brawler: Moderate
- 2 Brawlers: Hard
- Mixed (2 Grunt + 1 Sniper + 1 Brawler): Hard
- 5+ enemies: Very Hard
```

**Test each combination, adjust values**

---

**Task 35.4: Final Bug Fixing** *(1 hour)*

**Common AI bugs:**

```
Issue: Enemies get stuck on geometry
Fix: Increase Rigidbody angular drag, add timeout to unstuck

Issue: Sniper laser goes through walls
Fix: Add raycast check before showing laser

Issue: Brawler tackle never stops
Fix: Add max tackle duration, force stop

Issue: Enemies spawn inside each other
Fix: Increase spawn radius, add spawn collision check

Issue: Dead enemies still in active list
Fix: Ensure proper cleanup in OnDestroy

Issue: Formation causes jitter
Fix: Increase formation radius, add smoothing
```

---

**End of Week 7 Commit:**
```
git add .
git commit -m "Week 7 Complete: Full enemy AI with 3 types, wave system, polish"
git tag "Phase2-Week7-Complete"
```

---

## **🗓️ Week 8: Polish and Integration**

**Weekly Goal:** Integrate all combat systems with Phase 1 movement. Add final polish ("juice") to make combat feel amazing. Extensive playtesting and balance tuning.

**Total Time Budget:** 40 hours  
**Daily Breakdown:** 8 hours/day × 5 days

---

### **📅 Day 36: Boost-Cancel Refinement (8 hours)**

**Daily Goal:** Perfect the boost-cancel system for seamless combat flow.

---

#### **Morning Session (4 hours): Advanced Canceling**

**Task 36.1: Comprehensive Cancel System** *(2.5 hours)*

**Expand PlayerController cancel logic:**

```csharp
// In PlayerController.cs
[Header("Cancel System")]
[SerializeField] private WeaponManager weaponManager;
[SerializeField] private MeleeComboSystem meleeCombo;
[SerializeField] private float cancelBoostMultiplier = 1.2f; // Bonus boost from cancel

public bool IsPerformingAction()
{
    bool weaponFiring = weaponManager != null && weaponManager.IsFiring;
    bool meleeActive = meleeCombo != null && meleeCombo.IsAttacking;
    
    return weaponFiring || meleeActive;
}

private bool TryBoostCancel()
{
    bool didCancel = false;
    
    // Cancel melee combo
    if (meleeCombo != null && meleeCombo.IsAttacking)
    {
        if (meleeCombo.CanBoostCancel())
        {
            meleeCombo.ForceCancel();
            didCancel = true;
        }
    }
    
    // Cancel weapon recoil/reload
    if (weaponManager != null && weaponManager.CanCancel())
    {
        weaponManager.ForceCancel();
        didCancel = true;
    }
    
    if (didCancel)
    {
        // Visual/audio feedback
        TriggerCancelEffect();
        
        // Optional: Reward skilled cancel with bonus speed
        return true;
    }
    
    return false;
}

private void ExecuteQuickBoost()
{
    // Check if we're canceling an action
    bool wasCanceling = TryBoostCancel();
    
    Vector3 boostDirection = Vector3.zero;
    
    if (isDrifting)
    {
        boostDirection = transform.forward;
    }
    else
    {
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
    
    // Apply force (with cancel bonus if applicable)
    float finalForce = quickBoostForce;
    if (wasCanceling)
    {
        finalForce *= cancelBoostMultiplier;
    }
    
    rb.AddForce(boostDirection * finalForce, ForceMode.Impulse);
    
    // ... rest of boost code
}

private void TriggerCancelEffect()
{
    // Bright flash
    if (impulseSource != null)
    {
        impulseSource.GenerateImpulse(Vector3.one * 0.2f);
    }
    
    // VFX burst at player position
    // Play "cancel" sound effect
}
```

---

**Task 36.2: WeaponManager Cancel Support** *(1 hour)*

```csharp
// In WeaponManager.cs
private bool isFiring = false;
private bool canCancelFire = false;

public bool IsFiring => isFiring;
public bool CanCancel() => canCancelFire;

public void ForceCancel()
{
    isFiring = false;
    canCancelFire = false;
    
    // Stop any fire coroutines
    StopAllCoroutines();
    
    // Reset weapon state
    primaryFireTimer = 0f;
}

private void FirePrimaryWeapon()
{
    isFiring = true;
    canCancelFire = false;
    
    // ... existing fire code ...
    
    // After a brief delay, allow cancel
    StartCoroutine(EnableCancelAfterDelay(0.1f));
}

private IEnumerator EnableCancelAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    canCancelFire = true;
    
    yield return new WaitForSeconds(0.2f);
    isFiring = false;
}
```

---

**Task 36.3: MeleeComboSystem Cancel Support** *(30 minutes)*

```csharp
// In MeleeComboSystem.cs
public void ForceCancel()
{
    if (attackCoroutine != null)
    {
        StopCoroutine(attackCoroutine);
    }
    
    isAttacking = false;
    canCombo = false;
    
    // Deactivate weapon immediately
    if (meleeWeapon != null)
    {
        meleeWeapon.DeactivateWeapon();
    }
    
    ResetCombo();
}
```

---

#### **Afternoon Session (4 hours): Cancel Techniques**

**Task 36.4: Document Advanced Techniques** *(1 hour)*

Update `CombatMechanics.md`:

```markdown
## Advanced Boost-Cancel Techniques

### 1. Melee Cancel Chain
**Execution:**
1. Start melee combo (L1)
2. After hit 1 connects, boost-cancel (Circle)
3. Reposition behind enemy
4. Start new combo

**Effect:** Confuses enemy AI, maintains pressure
**Difficulty:** Medium

### 2. Shot-Cancel Strafe
**Execution:**
1. Fire rifle (hold R1)
2. Immediately boost-cancel sideways
3. Fire again from new position

**Effect:** Constant movement while shooting, hard to hit
**Difficulty:** Medium

### 3. Drive-By Slash
**Execution:**
1. Boost at high speed toward enemy
2. Switch to Assault mid-boost
3. Melee as you pass
4. Boost-cancel to escape

**Effect:** High-damage hit-and-run
**Difficulty:** Hard

### 4. Drift-Cancel Momentum
**Execution:**
1. Enter drift at max speed
2. Charge Quick Boost (wait 0.3s)
3. Boost-cancel drift
4. Velocity stacks dramatically

**Effect:** Extreme speed burst
**Difficulty:** Expert
**Risk:** Hard to control

### 5. Animation Feint
**Execution:**
1. Start melee attack 3 (big windup)
2. Enemy reacts/dodges
3. Boost-cancel before commit
4. Punish enemy recovery

**Effect:** Bait enemy into wasting dodge
**Difficulty:** Expert (requires reads)
```

---

**Task 36.5: Cancel Training Course** *(2 hours)*

**Build skill challenges:**

**Challenge 1: Cancel Chain**
```
Setup: 5 targets in a row
Goal: Destroy all with melee, cancel after each hit
Success: Complete in <10 seconds
```

**Challenge 2: No-Hit Run**
```
Setup: 3 Grunts firing constantly
Goal: Destroy all without taking damage
Requirement: Must use shot-cancel strafing
```

**Challenge 3: Speed Demon**
```
Setup: 10 checkpoints around arena
Goal: Hit all in <20 seconds
Requirement: Must use drift-cancel momentum
```

Build these in Combat Training scene.

---

**Task 36.6: Cancel Feedback Polish** *(1 hour)*

**Make cancels FEEL amazing:**

```csharp
// Enhanced cancel effect
private void TriggerCancelEffect()
{
    // 1. Screen shake
    if (impulseSource != null)
    {
        impulseSource.GenerateImpulse(Vector3.one * 0.25f);
    }
    
    // 2. Particle burst
    if (cancelVFX != null)
    {
        GameObject vfx = Instantiate(cancelVFX, transform.position, Quaternion.identity);
        Destroy(vfx, 1f);
    }
    
    // 3. Sound effect (sharp "click")
    if (cancelSound != null && audioSource != null)
    {
        audioSource.pitch = Random.Range(1.1f, 1.3f); // Slightly high pitch
        audioSource.PlayOneShot(cancelSound, 0.7f);
    }
    
    // 4. Brief time dilation
    StartCoroutine(CancelTimeDilation());
}

private IEnumerator CancelTimeDilation()
{
    Time.timeScale = 0.8f;
    yield return new WaitForSecondsRealtime(0.05f);
    Time.timeScale = 1f;
}
```

**Create cancel VFX:**
- Ring burst expanding outward
- Cyan/white color
- Quick (0.2s lifetime)

---

**End of Day 36 Commit:**
```
git add .
git commit -m "Day 36: Boost-cancel system perfected with advanced techniques"
```

---

### **📅 Day 37-38: Combat "Juice" Implementation (16 hours)**

**Goal:** Add all the micro-polish that makes combat feel incredible.

---

#### **Day 37 Morning: Hit-Stop & Impact** *(4 hours)*

**Task 37.1: Enhanced Hit-Stop System** *(2 hours)*

**Create dedicated hit-stop manager:**

`Assets/_Project/Scripts/Combat/HitStopManager.cs`:

```csharp
using UnityEngine;
using System.Collections;

public class HitStopManager : MonoBehaviour
{
    private static HitStopManager instance;
    public static HitStopManager Instance => instance;
    
    [Header("Settings")]
    [SerializeField] private AnimationCurve hitStopCurve = AnimationCurve.EaseInOut(0, 0.05f, 1, 1f);
    
    private Coroutine currentHitStop;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public static void TriggerHitStop(float duration, float timeScale = 0.05f)
    {
        if (Instance != null)
        {
            if (Instance.currentHitStop != null)
            {
                Instance.StopCoroutine(Instance.currentHitStop);
            }
            
            Instance.currentHitStop = Instance.StartCoroutine(Instance.HitStopCoroutine(duration, timeScale));
        }
    }
    
    private IEnumerator HitStopCoroutine(float duration, float minTimeScale)
    {
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            // Curve from slow to normal
            float t = elapsed / duration;
            Time.timeScale = Mathf.Lerp(minTimeScale, 1f, hitStopCurve.Evaluate(t));
            
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        
        Time.timeScale = 1f;
    }
}
```

**Add to scene (attach to GameManager or similar)**

---

**Task 37.2: Hit-Stop Integration** *(1.5 hours)*

**Apply to melee:**

```csharp
// In MeleeWeapon.OnTriggerEnter()
private void OnTriggerEnter(Collider other)
{
    // ... existing damage code ...
    
    if (targetHealth != null)
    {
        // Different hit-stop for different combo hits
        float hitStopDuration = 0.05f;
        float hitStopIntensity = 0.1f;
        
        // Finisher hit has more impact
        if (/* is final combo hit */)
        {
            hitStopDuration = 0.1f;
            hitStopIntensity = 0.05f; // Slower
        }
        
        HitStopManager.TriggerHitStop(hitStopDuration, hitStopIntensity);
    }
}
```

**Apply to projectiles:**

```csharp
// In Projectile.OnCollisionEnter()
private void OnCollisionEnter(Collision collision)
{
    Health target = collision.gameObject.GetComponent<Health>();
    
    if (target != null)
    {
        target.TakeDamage(damage);
        
        // Smaller hit-stop for projectiles
        HitStopManager.TriggerHitStop(0.02f, 0.5f);
    }
    
    Destroy(gameObject);
}
```

---

**Task 37.3: Impact Visual Effects** *(30 minutes)*

**Enhance impact particles:**

```csharp
// In MeleeWeapon
private void OnTriggerEnter(Collider other)
{
    // ... existing code ...
    
    // Spawn impact effect
    if (impactVFX != null)
    {
        Vector3 impactPoint = other.ClosestPoint(transform.position);
        GameObject impact = Instantiate(impactVFX, impactPoint, Quaternion.identity);
        
        // Scale effect based on damage
        float damageScale = Mathf.Clamp(damage / 100f, 0.5f, 2f);
        impact.transform.localScale = Vector3.one * damageScale;
        
        Destroy(impact, 2f);
    }
}
```

---

#### **Day 37 Afternoon: Screen Effects** *(4 hours)*

**Task 37.4: Damage Directional Indicator** *(2 hours)*

**Show where damage came from:**

`Assets/_Project/Scripts/UI/DamageDirectionIndicator.cs`:

```csharp
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DamageDirectionIndicator : MonoBehaviour
{
    [System.Serializable]
    public class DamageIndicator
    {
        public Image image;
        public float lifetime;
        public Vector3 damageSource;
    }
    
    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField] private Transform indicatorParent;
    [SerializeField] private float indicatorLifetime = 2f;
    [SerializeField] private float fadeSpeed = 3f;
    
    private List<DamageIndicator> activeIndicators = new List<DamageIndicator>();
    private Transform player;
    private Health playerHealth;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        playerHealth = player?.GetComponent<Health>();
        
        if (playerHealth != null)
        {
            playerHealth.OnDamageTaken += ShowDamageIndicator;
        }
    }
    
    private void Update()
    {
        UpdateIndicators();
    }
    
    private void ShowDamageIndicator(float amount)
    {
        // Find source of damage (raycast backwards from player)
        // For now, use a simple approach
        
        Vector3 damageSource = FindNearestEnemy();
        
        if (damageSource != Vector3.zero)
        {
            CreateIndicator(damageSource);
        }
    }
    
    private Vector3 FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDist = float.MaxValue;
        Vector3 closestPos = Vector3.zero;
        
        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(player.position, enemy.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestPos = enemy.transform.position;
            }
        }
        
        return closestPos;
    }
    
    private void CreateIndicator(Vector3 damageSource)
    {
        GameObject indicatorObj = Instantiate(indicatorPrefab, indicatorParent);
        Image image = indicatorObj.GetComponent<Image>();
        
        DamageIndicator indicator = new DamageIndicator
        {
            image = image,
            lifetime = indicatorLifetime,
            damageSource = damageSource
        };
        
        activeIndicators.Add(indicator);
    }
    
    private void UpdateIndicators()
    {
        for (int i = activeIndicators.Count - 1; i >= 0; i--)
        {
            DamageIndicator indicator = activeIndicators[i];
            
            // Update lifetime
            indicator.lifetime -= Time.deltaTime;
            
            if (indicator.lifetime <= 0)
            {
                Destroy(indicator.image.gameObject);
                activeIndicators.RemoveAt(i);
                continue;
            }
            
            // Update position (point toward damage source)
            Vector3 directionToSource = (indicator.damageSource - player.position).normalized;
            directionToSource.y = 0; // Keep on horizontal plane
            
            float angle = Mathf.Atan2(directionToSource.x, directionToSource.z) * Mathf.Rad2Deg;
            indicator.image.transform.rotation = Quaternion.Euler(0, 0, -angle);
            
            // Fade out
            Color color = indicator.image.color;
            color.a = Mathf.Lerp(color.a, 0, fadeSpeed * Time.deltaTime);
            indicator.image.color = color;
        }
    }
}
```

**Create indicator UI:**
- Red arrow pointing toward damage source
- Positioned at screen edge
- Fades over time

---

**Task 37.5: Kill Streak System** *(1.5 hours)*

**Track and reward consecutive kills:**

`Assets/_Project/Scripts/Gameplay/KillStreakTracker.cs`:

```csharp
using UnityEngine;
using TMPro;
using System.Collections;

public class KillStreakTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI streakText;
    [SerializeField] private float streakResetTime = 5f;
    
    private int currentStreak = 0;
    private float streakTimer = 0f;
    
    private void Update()
    {
        if (streakTimer > 0)
        {
            streakTimer -= Time.deltaTime;
            
            if (streakTimer <= 0)
            {
                ResetStreak();
            }
        }
    }
    
    public void RegisterKill()
    {
        currentStreak++;
        streakTimer = streakResetTime;
        
        UpdateDisplay();
        CheckForMilestone();
    }
    
    private void UpdateDisplay()
    {
        if (streakText != null)
        {
            if (currentStreak > 1)
            {
                streakText.text = $"{currentStreak} KILL STREAK!";
                streakText.gameObject.SetActive(true);
                
                // Pulse animation
                LeanTween.cancel(streakText.gameObject);
                LeanTween.scale(streakText.gameObject, Vector3.one * 1.3f, 0.1f)
                    .setEaseOutBack()
                    .setOnComplete(() => {
                        LeanTween.scale(streakText.gameObject, Vector3.one, 0.1f);
                    });
            }
            else
            {
                streakText.gameObject.SetActive(false);
            }
        }
    }
    
    private void CheckForMilestone()
    {
        // Rewards/effects at certain streaks
        if (currentStreak == 5)
        {
            Debug.Log("5 KILL STREAK!");
            // Play special sound, VFX
        }
        else if (currentStreak == 10)
        {
            Debug.Log("10 KILL STREAK! UNSTOPPABLE!");
            // Major effect
        }
    }
    
    private void ResetStreak()
    {
        currentStreak = 0;
        UpdateDisplay();
    }
}
```

**Subscribe to enemy deaths:**

```csharp
// In WaveSpawner or GameManager
private KillStreakTracker streakTracker;

private void SpawnEnemy(GameObject prefab)
{
    // ... spawn code ...
    
    Health health = enemy.GetComponent<Health>();
    if (health != null)
    {
        health.OnDeath += () => {
            OnEnemyDied(enemy);
            
            if (streakTracker != null)
            {
                streakTracker.RegisterKill();
            }
        };
    }
}
```

---

**Task 37.6: Slow-Mo Finishers** *(30 minutes)*

**Final enemy death in slow motion:**

```csharp
// In WaveSpawner.WaitForWaveClear()
private IEnumerator WaitForWaveClear()
{
    while (activeEnemies.Count > 1) // More than 1
    {
        activeEnemies.RemoveAll(e => e == null);
        yield return new WaitForSeconds(0.5f);
    }
    
    // Last enemy - trigger slow-mo on death
    if (activeEnemies.Count == 1 && activeEnemies[0] != null)
    {
        Health lastEnemy = activeEnemies[0].GetComponent<Health>();
        if (lastEnemy != null)
        {
            lastEnemy.OnDeath += TriggerFinisherSlowMo;
        }
    }
    
    // Wait for all to die
    while (activeEnemies.Count > 0)
    {
        activeEnemies.RemoveAll(e => e == null);
        yield return new WaitForSeconds(0.5f);
    }
}

private void TriggerFinisherSlowMo()
{
    StartCoroutine(SlowMoFinisher());
}

private IEnumerator SlowMoFinisher()
{
    Time.timeScale = 0.3f;
    yield return new WaitForSecondsRealtime(1f);
    Time.timeScale = 1f;
}
```

---

#### **Day 38: Audio & Haptic Polish** *(8 hours)*

**Task 38.1: Dynamic Combat Music** *(3 hours)*

**Music layers based on combat intensity:**

`Assets/_Project/Scripts/Audio/DynamicMusicSystem.cs`:

```csharp
using UnityEngine;

public class DynamicMusicSystem : MonoBehaviour
{
    [Header("Music Layers")]
    [SerializeField] private AudioSource baseLayer;
    [SerializeField] private AudioSource combatLayer;
    [SerializeField] private AudioSource intenseLayer;
    
    [Header("Settings")]
    [SerializeField] private float transitionSpeed = 1f;
    [SerializeField] private int enemyThresholdCombat = 1;
    [SerializeField] private int enemyThresholdIntense = 3;
    
    private enum MusicState
    {
        Idle,
        Combat,
        Intense
    }
    
    private MusicState currentState = MusicState.Idle;
    private int currentEnemyCount = 0;
    
    private void Start()
    {
        // Start all layers playing, but mute combat/intense
        baseLayer.volume = 1f;
        combatLayer.volume = 0f;
        intenseLayer.volume = 0f;
        
        // Sync all layers
        baseLayer.Play();
        combatLayer.Play();
        intenseLayer.Play();
    }
    
    private void Update()
    {
        // Count active enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        currentEnemyCount = enemies.Length;
        
        // Determine state
        MusicState targetState;
        if (currentEnemyCount >= enemyThresholdIntense)
        {
            targetState = MusicState.Intense;
        }
        else if (currentEnemyCount >= enemyThresholdCombat)
        {
            targetState = MusicState.Combat;
        }
        else
        {
            targetState = MusicState.Idle;
        }
        
        if (targetState != currentState)
        {
            TransitionToState(targetState);
        }
        
        UpdateLayerVolumes();
    }
    
    private void TransitionToState(MusicState newState)
    {
        currentState = newState;
    }
    
    private void UpdateLayerVolumes()
    {
        float baseTarget = 0f;
        float combatTarget = 0f;
        float intenseTarget = 0f;
        
        switch (currentState)
        {
            case MusicState.Idle:
                baseTarget = 1f;
                break;
            case MusicState.Combat:
                baseTarget = 0.6f;
                combatTarget = 1f;
                break;
            case MusicState.Intense:
                baseTarget = 0.4f;
                combatTarget = 0.8f;
                intenseTarget = 1f;
                break;
        }
        
        // Smooth transition
        baseLayer.volume = Mathf.Lerp(baseLayer.volume, baseTarget, transitionSpeed * Time.deltaTime);
        combatLayer.volume = Mathf.Lerp(combatLayer.volume, combatTarget, transitionSpeed * Time.deltaTime);
        intenseLayer.volume = Mathf.Lerp(intenseLayer.volume, intenseTarget, transitionSpeed * Time.deltaTime);
    }
}
```

**Setup:**
- Find/create 3 music tracks that layer together
- Base: Ambient synth pads
- Combat: Drums + bass
- Intense: Full orchestral/electronic

---

**Task 38.2: Combat Sound Mix** *(2 hours)*

**Properly mix all combat audio:**

Open Audio Mixer, refine:

```
Master (0 dB)
├── Music (-3 dB)
│   ├── Base Layer (-6 dB)
│   ├── Combat Layer (0 dB)
│   └── Intense Layer (0 dB)
├── SFX (0 dB)
│   ├── PlayerWeapons (-2 dB)
│   │   ├── Ranged (-3 dB)
│   │   └── Melee (-1 dB)
│   ├── EnemyWeapons (-4 dB)
│   ├── Impacts (-2 dB)
│   ├── Explosions (+2 dB) ← Louder for emphasis
│   └── Movement (-8 dB) ← Quieter, just ambience
└── UI (-10 dB)
```

**Add ducking:**
- When explosion plays, duck music by -6dB
- When player takes damage, briefly duck all SFX

---

**Task 38.3: Enhanced Haptics** *(2 hours)*

**Detailed rumble for all actions:**

```csharp
// In HapticFeedbackManager
public enum HapticType
{
    Light,
    Medium,
    Heavy,
    Pulse,
    Impact
}

public void TriggerHaptic(HapticType type)
{
    if (gamepad == null) return;
    
    switch (type)
    {
        case HapticType.Light:
            gamepad.SetMotorSpeeds(0.1f, 0.2f);
            StartCoroutine(StopAfter(0.1f));
            break;
        
        case HapticType.Medium:
            gamepad.SetMotorSpeeds(0.3f, 0.5f);
            StartCoroutine(StopAfter(0.2f));
            break;
        
        case HapticType.Heavy:
            gamepad.SetMotorSpeeds(0.6f, 0.8f);
            StartCoroutine(StopAfter(0.3f));
            break;
        
        case HapticType.Pulse:
            StartCoroutine(PulseRoutine());
            break;
        
        case HapticType.Impact:
            gamepad.SetMotorSpeeds(0.8f, 1.0f);
            StartCoroutine(StopAfter(0.15f));
            break;
    }
}

private IEnumerator PulseRoutine()
{
    for (int i = 0; i < 3; i++)
    {
        gamepad.SetMotorSpeeds(0.3f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        gamepad.SetMotorSpeeds(0f, 0f);
        yield return new WaitForSeconds(0.1f);
    }
}

private IEnumerator StopAfter(float delay)
{
    yield return new WaitForSeconds(delay);
    gamepad.SetMotorSpeeds(0f, 0f);
}
```

**Apply:**
- Light: Projectile fire
- Medium: Melee hit
- Heavy: Melee finisher, player damage
- Pulse: Low health warning
- Impact: Explosion, death

---

**Task 38.4: Audio Polish Pass** *(1 hour)*

**Add missing sounds:**
- Enemy aggro vocalizations
- Weapon switch sound
- Low ammo/boost warning
- Victory fanfare
- Defeat sound

**Ensure no audio clipping or distortion**

---

**End of Day 38 Commit:**
```
git add .
git commit -m "Day 37-38: Combat juice - hit-stop, screen effects, audio/music, haptics"
```

---

### **📅 Day 39-40: Full Loop Testing & Balance (16 hours)**

**Goal:** Extensive playtesting, balance tuning, bug fixing.

---

#### **Day 39: Balance Tuning** *(8 hours)*

**Task 39.1: Weapon Balance** *(3 hours)*

**Test each weapon against enemy types:**

Create spreadsheet:

```
| Weapon | vs Grunt | vs Sniper | vs Brawler | TTK Average |
|--------|----------|-----------|------------|-------------|
| Beam Rifle | 6.7s | 5.3s | 10s | 7.3s |
| Beam Saber | 4s | 3.5s | 6.7s | 4.7s |
| Beam Axe | 8.3s | 6.7s | 12.5s | 9.2s |
| Grenade | 10s | 8s | 15s | 11s |

Target TTK: 5-8 seconds for average engagement
```

**Adjust weapon values:**
- If rifle feels weak vs Brawler, increase damage vs heavy armor
- If saber is too strong, reduce combo damage
- etc.

---

**Task 39.2: Enemy Balance** *(2 hours)*

**Test each enemy type:**

```
Solo Tests:
- 1v1 each enemy type
- Document: How hard? Fun factor? Frustration points?

Group Tests:
- 3 Grunts
- 2 Brawlers
- 1 of each type
- 5 mixed enemies

Adjust:
- Enemy HP
- Enemy damage
- Enemy aggression ranges
- Enemy attack cooldowns
```

---

**Task 39.3: Boost Economy** *(1.5 hours)*

**Test boost management:**

```
Scenario: 30-second continuous combat (respawning enemies)

Questions:
- Do you run out of boost?
- Can you maintain mobility?
- Is boost management interesting or annoying?

Current values:
- Max Boost: 100
- Recharge Rate: 30/s
- Quick Boost Cost: 25
- Primary Boost Cost: 15/s

If boost feels restrictive:
- Increase max capacity to 120
- Increase recharge to 40/s

If boost feels infinite:
- Decrease max to 80
- Increase costs
```

---

**Task 39.4: Difficulty Curve** *(1.5 hours)*

**Test wave progression:**

```
Wave 1: Should be easy tutorial (1-2 deaths max)
Wave 2: Moderate challenge (2-3 deaths acceptable)
Wave 3: Hard (3-5 deaths)
Wave 4: Very hard (5+ deaths, requires mastery)

If progression feels off:
- Adjust enemy counts
- Adjust spawn timing
- Add/remove enemy types per wave
```

---

#### **Day 40: Final Testing & Polish** *(8 hours)*

**Task 40.1: Extended Playtest Session** *(4 hours)*

**Play for 2 hours straight:**

1. Complete all waves
2. Try all weapons
3. Practice advanced techniques
4. Test edge cases

**Document everything:**
- Bugs found
- Balance issues
- Frustration points
- Fun moments
- Confusing mechanics

---

**Task 40.2: External Playtesting** *(2 hours if available)*

**Give build to someone else:**

**Questions to ask:**
1. Which weapons felt best?
2. Which enemies were most challenging?
3. Did you understand boost-canceling?
4. Did combat feel fair or cheap?
5. Rate overall fun: 1-10
6. Would you play more?

**Implement critical feedback immediately**

---

**Task 40.3: Bug Fixing Marathon** *(1.5 hours)*

**Fix all known bugs:**

Priority order:
1. Game-breaking (crashes, softlocks)
2. Gameplay-breaking (exploits, broken mechanics)
3. Major annoyances (UI bugs, audio issues)
4. Minor polish (typos, small visual glitches)

---

**Task 40.4: Final Balance Pass** *(30 minutes)*

**Lock in final values:**

Update balance document with FINAL numbers.

```markdown
# Phase 2 Final Balance Values

## Player
- Health: 200 HP
- Beam Rifle: 15 damage, 300 RPM
- Beam Saber: 40/40/80 combo (160 total)
- Boost: 100 max, 30/s recharge

## Enemies
- Grunt: 100 HP, 10 damage
- Sniper: 80 HP, 120 damage
- Brawler: 150 HP, 60 damage

## Difficulty
- Easy: 0.7x enemy HP/damage
- Normal: 1.0x
- Hard: 1.5x HP, 1.3x damage

[Locked - do not change without full retest]
```

---

**End of Phase 2 Final Commit:**
```
git add .
git commit -m "Phase 2 COMPLETE - Full combat loop with polish and balance"
git tag "Phase2-COMPLETE"
git tag "v0.2.0-CombatPrototype"
```

---

## **🎯 Phase 2 Complete - Final Deliverables Checklist**

### **Functional Combat**
- [ ] Ranger stance (beam rifle + grenades)
- [ ] Assault stance (beam saber + axe)
- [ ] Stance switching mid-combat
- [ ] Lock-on targeting system
- [ ] All weapons functional and balanced

### **Enemy AI**
- [ ] 3 enemy types (Grunt, Sniper, Brawler)
- [ ] State machine AI with 5 states
- [ ] Enemies pursue, attack, retreat
- [ ] Wave spawning system
- [ ] Difficulty scaling

### **Combat Systems**
- [ ] Boost-cancel mechanic working
- [ ] Hit-stop on impacts
- [ ] Player damage and death
- [ ] Health/boost UI
- [ ] Kill streak tracking

### **Feedback & Polish**
- [ ] All weapons have VFX/SFX
- [ ] Enemy death explosions
- [ ] Hit markers and damage numbers
- [ ] Dynamic music system
- [ ] Haptic feedback for all actions
- [ ] Screen effects (flash, shake, slow-mo)

### **Balance**
- [ ] All weapons viable
- [ ] All enemies threatening but fair
- [ ] Difficulty modes functional
- [ ] Combat scenarios tested
- [ ] Balance values documented

### **Performance**
- [ ] 60fps with 10+ enemies
- [ ] No memory leaks
- [ ] AI optimized
- [ ] Zero game-breaking bugs

---

## **📊 Phase 2 Final Review**

**The Critical Question:**

### **"Is the loop of fighting enemies for 10 minutes fun and engaging?"**

**If YES:**
- ✅ Phase 2 is a success
- ✅ Core combat loop is proven
- ✅ Ready for Phase 3 (content creation)

**If NO:**
- ⚠️ Identify what's not fun
- ⚠️ Fix core issues before moving on
- ⚠️ Combat must be solid before adding content

---

## **🚀 What's Next: Phase 3 Preview**

**With Phase 2 complete, you now have:**
- Fluid, responsive movement (Phase 1)
- Deep, satisfying combat (Phase 2)
- A complete gameplay loop

**Phase 3 will add:**
1. Actual mission structure
2. Environmental variety (not just gray boxes)
3. Story integration
4. Boss encounters
5. Final art pass
6. Campaign progression

**But first: Take a 3-7 day break!**

You've built something incredible. The hardest design work is done. Phase 3 is about content, not systems.

---

**🎊 CONGRATULATIONS! PHASE 2 COMPLETE! 🎊**

You now have a **fully functional combat prototype** that integrates seamlessly with the Phase 1 movement system. 

**You've created:**
- A unique movement system (High-G Drift)
- Deep combat with stance switching
- Intelligent enemy AI
- Professional-level game feel

**This is portfolio-ready material.** 

**Rest, reflect, then when ready... Phase 3 awaits.** 🎮✨

---

**Total Phase 2 Development Time:** 160 hours (4 weeks)  
**Total Project Time:** 320 hours (8 weeks)  
**Progress:** ~60% complete (movement + combat done, content remains)

---

**Final Phase 2 Commit:**
```
git add .
git commit -m "🎉 PHASE 2 COMPLETE - Combat prototype fully functional and polished"
git tag "v0.2.0-FINAL"
git push origin main --tags
```

**Now go celebrate. You've earned it, pilot.** 🚀🎉

---

**Ready for Phase 3 when you are!** 🎮