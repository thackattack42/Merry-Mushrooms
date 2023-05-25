using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage, IEffectable
{

    private Vector3 playerVelocity;
    private Vector3 move;
    private int jumpedTimes;
    private bool groundedPlayer;
    [SerializeField] CharacterController controller;
    [Header("----- Player Stats -----")]
    [SerializeField] float playerSpeed;
    [Range(8, 20)][SerializeField] float jumpHeight;
    [SerializeField] float gravityValue;
    [Range(1, 3)][SerializeField] int maxJumps;
    [Range(2, 3)][SerializeField] int sprintSpeed;
    [SerializeField] public int maxHP;
    [SerializeField] public int HP;

    [Header("----- Player Dash Properties -----")]
    [SerializeField] float dashSpeed;
    [Range(2, 10)][SerializeField] public int dashCoolDown;

    [Header("----- Weapon Stats -----")]
    public List<Staff_Stats> staffList = new List<Staff_Stats>();
    [Range(2, 300)][SerializeField] int shootDistance;
    [Range(0.1f, 3)][SerializeField] float shootRate;
    [Range(1, 20)][SerializeField] int shootDamage;
    [SerializeField] MeshFilter staffModel;
    [SerializeField] MeshRenderer staffMat;


    private float dashTime = 0.3f;
    private float origSpeed;
    private int isDashing;
    private bool isShooting;
    public bool isReloading;
    private bool isSprinting;
    public bool isCrouching;
    private float origHeight;
    public int selectedStaff;

    public delegate void PlayerCrouch();
    public static event PlayerCrouch Crouch;
    public static event PlayerCrouch Uncrouch;

    private Dictionary<string, StatusEffectData> statusEffects;
    private float period = 0.0f;

    public PlayerController()
    {
        this.statusEffects = null;
    }

    private void Start()
    {
        // Sets original variables to players starting stats
        origSpeed = playerSpeed;
        controller.height = 2.0f;
        origHeight = controller.height;

        statusEffects = new Dictionary<string, StatusEffectData>();

        // Spawns Player
        Spawn();
    }

    void Update()
    {
        if (gameManager.instance.activeMenu == null)
        {
            OnPlayerCrouch();
            OnPlayerUncrouch();
            Movement();
            SwitchStaff();
            if (Input.GetKeyDown(KeyCode.E) && isDashing == 0 && !isCrouching)
            {
                playerDash();
                StartCoroutine(WaitForDash());
            }
            if (Input.GetKeyDown(KeyCode.R) || staffList.Count != 0 && staffList[selectedStaff].ammoClip <= 0)
            {
                StartCoroutine(Reload());
            }

            if (Input.GetButton("Shoot") && !isShooting && !isReloading && staffList.Count > 0)
            {
                StartCoroutine(shoot());
            }

            // Call anything in here we want to update per second (not per frame)
            if (period > 1.0f)
            {
                period = 0.0f;
                UpdateStatusEffects();
            }
            period += UnityEngine.Time.deltaTime;
        }

        Sprint();
        CrouchPlayer();
    }

    void Movement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            jumpedTimes = 0;
        }

        move = (transform.right * Input.GetAxis("Horizontal")) +
               (transform.forward * Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && jumpedTimes < maxJumps)
        {
            jumpedTimes++;
            playerVelocity.y = jumpHeight;
        }

        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void Sprint()
    {
        if (Input.GetButtonDown("Sprint") && !isCrouching)
        {
            playerSpeed *= sprintSpeed;
            isSprinting = true;

        }
        else if (Input.GetButtonUp("Sprint") && !isCrouching)
        {
            playerSpeed = origSpeed;
            isSprinting = false;
        }
    }
    public void Spawn()
    {
        controller.enabled = false;
        transform.position = gameManager.instance.playerSpawnPos.transform.position;
        controller.enabled = true;
        takeDamage(-maxHP);
    }
    IEnumerator shoot()
    {
        if (staffList[selectedStaff].ammoClip > 0)
        {
            isShooting = true;

            staffList[selectedStaff].ammoClip--;

            RaycastHit hit;
            GameObject muzzle = GameObject.FindGameObjectWithTag("MuzzleFlash");
            Instantiate(staffList[selectedStaff].muzzleEffect, muzzle.transform.position, staffList[selectedStaff].muzzleEffect.transform.rotation);

            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDistance))
            {
                Instantiate(staffList[selectedStaff].hitEffect, hit.point, staffList[selectedStaff].hitEffect.transform.rotation);

                if (staffList[selectedStaff].fire)
                {
                    IFireDamage fireDamage = hit.collider.GetComponent<IFireDamage>();

                    if (fireDamage != null)
                        fireDamage.TakeFireDamage(shootDamage);
                }

                if (staffList[selectedStaff].earth)
                {
                    IEarthDamage earthDamage = hit.collider.GetComponent<IEarthDamage>();

                    if (earthDamage != null)
                        earthDamage.TakeEarthDamage(shootDamage);
                }

                if (staffList[selectedStaff].ice)
                {
                    IIceDamage iceDamage = hit.collider.GetComponent<IIceDamage>();

                    if (iceDamage != null)
                        iceDamage.TakeIceDamage(shootDamage);
                }

                IDamage damageable = hit.collider.GetComponent<IDamage>();

                if (damageable != null)
                    damageable.takeDamage(shootDamage);
            }
        }
        gameManager.instance.UpdateAmmoCount();
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

    void playerDash()
    {
        isDashing++;
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        // Will make player dash
        playerSpeed *= dashSpeed;
        Debug.Log("Player Dashed");
        // How long the player will dash for
        yield return new WaitForSeconds(dashTime);
        if (isSprinting)
        {
            playerSpeed /= dashSpeed;
        }
        else
            playerSpeed = origSpeed;

    }

    IEnumerator WaitForDash()
    {
        // How long the player has to wait before dashing again
        gameManager.instance.playerHUD.dashCooldown();
        yield return new WaitForSeconds(dashCoolDown);
        isDashing = 0;
    }

    public void takeDamage(int amount)
    {
        // Will take damage based off the amount 
        HP -= amount;
        gameManager.instance.playerHUD.updatePlayerHealth(HP);
        if (HP <= 0)
        {
            HP = 0;
            // Player is dead and display game over screen.
            gameManager.instance.GameOver();
        }
    }

    public void staffPickup(Staff_Stats stats)
    {
        staffList.Add(stats);

        shootDamage = stats.shootDamage;
        shootDistance = stats.shootDistance;
        shootRate = stats.shootRate;

        staffModel.mesh = stats.model.GetComponent<MeshFilter>().sharedMesh;
        staffMat.material = stats.model.GetComponent<MeshRenderer>().sharedMaterial;

        selectedStaff = staffList.Count - 1;
        gameManager.instance.UpdateAmmoCount();
    }

    void SwitchStaff()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectedStaff < staffList.Count - 1)
        {
            selectedStaff++;
            ChangeStaffStats();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedStaff > 0)
        {
            selectedStaff--;
            ChangeStaffStats();   
        }
    }
    void ChangeStaffStats()
    {
        shootDamage = staffList[selectedStaff].shootDamage;
        shootDistance = staffList[selectedStaff].shootDistance;
        shootRate = staffList[selectedStaff].shootRate;
        staffModel.mesh = staffList[selectedStaff].model.GetComponent<MeshFilter>().sharedMesh;
        staffMat.material = staffList[selectedStaff].model.GetComponent<MeshRenderer>().sharedMaterial;
        gameManager.instance.UpdateAmmoCount();
    }
    public void CrouchPlayer()
    {
        if (Input.GetButtonDown("Crouch") && !isSprinting && isDashing == 0)
        {
            playerSpeed /= 2;
            isCrouching = true;
            controller.height /= 2;
        }
        else if (Input.GetButtonUp("Crouch") && !isSprinting && isDashing == 0)
        {
            playerSpeed = origSpeed;
            isCrouching = false;
            controller.height = origHeight;
        }
    }

    void OnPlayerCrouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            if (Crouch != null)
                Crouch();
        }
    }

    void OnPlayerUncrouch()
    {
        if (Input.GetButtonUp("Crouch"))
        {
            if (Uncrouch != null)
                Uncrouch();
        }
    }

    public void ApplyEffect(StatusEffectData data)
    {
        // Check if status effect is new and apply modifier
        if (!statusEffects.ContainsKey(data.name))
        {
            switch (data.type)
            {
                case StatusEffectType.Fire:
                    HP -= data.modifier;
                    break;

                // TODO: Handle additional status effects

                default:
                    // Do nothing
                    break;
            }

            // TODO: This code is duplicated in multiple places, move to a generic function?
            gameManager.instance.playerHUD.updatePlayerHealth(HP);
            if (HP <= 0)
            {
                HP = 0;
                // Player is dead and display game over screen.
                gameManager.instance.GameOver();
            }
        }

        // Add/renew status effect
        statusEffects.Add(data.name, data);
    }

    // This is expected to be called every second (not frame)
    public void UpdateStatusEffects()
    {
        List<string> keysToRemove = new List<string>();

        foreach (var iter in statusEffects)
        {
            StatusEffectData data = iter.Value;

            // Apply status effect
            if (data.duration > 0)
            {
                switch (data.type)
                {
                    case StatusEffectType.Fire:
                        HP -= data.modifierPerSecond;
                        break;

                    // TODO: Handle additional status effects

                    default:
                        // Do nothing
                        break;
                }

                // Tick down duration by 1 second
                data.duration -= 1;

                gameManager.instance.playerHUD.updatePlayerHealth(HP);
                if (HP <= 0)
                {
                    HP = 0;
                    // Player is dead and display game over screen.
                    gameManager.instance.GameOver();
                }
            }
            else
            {
                // Add status effect key for removal
                keysToRemove.Add(data.name);
            }
        }

        // Remove status effects
        if (keysToRemove.Count > 0)
        {
            foreach (var key in keysToRemove)
            {
                statusEffects.Remove(key);
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        if (staffList[selectedStaff].ammoReserves < staffList[selectedStaff].origAmmo)
        {
            staffList[selectedStaff].ammoClip = staffList[selectedStaff].ammoReserves;
            staffList[selectedStaff].ammoReserves = 0;
        }
        else
        {
            staffList[selectedStaff].ammoReserves -= (staffList[selectedStaff].origAmmo - staffList[selectedStaff].ammoClip);
            staffList[selectedStaff].ammoClip = staffList[selectedStaff].origAmmo;
        }

        yield return new WaitForSeconds(2);
        gameManager.instance.UpdateAmmoCount();
        isReloading = false;
    }
}
