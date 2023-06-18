using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage, IEffectable, IPhysics
{
    static PlayerController pc;

    private Vector3 playerVelocity;
    private Vector3 move;
    private int jumpedTimes;
    private bool groundedPlayer;

    [Header("----- Componets -----")]
    [SerializeField] CharacterController controller;
    [SerializeField] public AudioSource aud;

    [Header("----- Player Stats -----")]
    [SerializeField] float playerSpeed;
    [Range(8, 20)][SerializeField] float jumpHeight;
    [SerializeField] float gravityValue;
    [Range(1, 3)][SerializeField] int maxJumps;
    [Range(2, 3)][SerializeField] int sprintSpeed;
    [SerializeField] public int maxHP;
    [SerializeField] public int maxMP;
    [SerializeField] public int HP;
    [SerializeField] public float MP;
    [SerializeField] public int level;
    [SerializeField] public int currExp;
    [SerializeField] public int expToNextLevel;
    [SerializeField] public int skillPoints;
    [SerializeField] float pushBackResolve;
    [SerializeField] public int knockbackPower;

    [Header("----- Player Dash Properties -----")]
    [SerializeField] float dashSpeed;
    [Range(2, 10)][SerializeField] public int dashCoolDown;
    private float dashTime = 0.3f;

    [Header("----- Staff Stats -----")]
    public List<Staff_Stats> staffList = new List<Staff_Stats>();
    [SerializeField] public MeshFilter staffModel;
    [SerializeField] public MeshRenderer staffMat;
    // Bullet for Player
    [SerializeField] GameObject playerBullet;
    [SerializeField] GameObject bulletPoint;
    [Header("----- Staff Shoot Stats -----")]
    [Range(2, 300)][SerializeField] int shootDistance;
    [Range(0.1f, 3)][SerializeField] float shootRate;
    [Range(1, 20)][SerializeField] public int shootDamage;
    public int selectedStaff;

    [Header("----- Bow Stats -----")]
    public List<BowStats> BowList = new List<BowStats>();
    [SerializeField] GameObject playerArrow;
    [SerializeField] GameObject arrowPoint;

    [SerializeField] public MeshRenderer bowMat;
    [SerializeField] public MeshFilter bowModel;
    [Header("----- Bow Shoot Stats -----")]
    //[Range(2, 300)][SerializeField] int bowShootDistance;
    [Range(0.1f, 3)][SerializeField] float bowShootRate;
    [Range(1, 20)][SerializeField] public int bowShootDamage;
    [SerializeField] public int bowPullTime;
    public int selectedBow;

    [Header("----- Sword Stats -----")]
    public List<SwordStats> SwordList = new List<SwordStats>();
    public List<ShieldStat> ShieldList = new List<ShieldStat>();
    [SerializeField] public GameObject MagicEffectPoint;
    [SerializeField] GameObject slashEffect;
    [SerializeField] public MeshRenderer swordMat;
    [SerializeField] public MeshFilter swordModel;
    [SerializeField] public MeshRenderer shieldMat;
    [SerializeField] public MeshFilter shieldModel;
    [SerializeField] public ParticleSystem Ps;
    public int selectedSword;



    [Header("----- Audio -----")]
    [SerializeField] AudioClip[] audJump;
    [SerializeField] AudioClip[] audDamage;
    [SerializeField] AudioClip[] audSteps;
    [SerializeField] float audJumpVol;
    [SerializeField] float audDamageVol;
    [SerializeField] float audStepsVol;

    public weapon playerWeapon;

    private float origSpeed;
    private float origHeight;
    private bool isShooting;
    public bool isReloading;
    private bool isSprinting;
    public bool isCrouching;
    private Vector3 destination;
    private Vector3 pushBack;
    private int isDashing;
    private bool isPulling;
    public float timer;
    public bool isUnderAttack;
    public bool onFire;
    public bool onIce;
    public bool onFiref;




    bool stepIsPlaying;
    public bool StaffEquipped;
    public bool BowEquipped;
    public bool SwordEquipped;
    public bool ShieldEquipped;
    public bool holdingShield;
    public bool bowShot;
    public delegate void PlayerCrouch();
    public static event PlayerCrouch Crouch;
    public static event PlayerCrouch Uncrouch;

    private Dictionary<string, StatusEffectData> statusEffects;
    private float period = 0.0f;

    public PlayerController()
    {
        this.statusEffects = null;
    }


    public enum weapon
    {
        Sword,
        Bow,
        Staff
    }

    private void Start()
    {
        // Sets original variables to players starting stats
        if (pc == null)
        {
            pc = this;
            origSpeed = playerSpeed;
            Spawn();
            controller.height = 2.0f;
            origHeight = controller.height;
            HP = maxHP;
            holdingShield = false;
            statusEffects = new Dictionary<string, StatusEffectData>();
        }
        else
        {
            Destroy(gameObject);
        }

        //gameManager.instance.playerSpawnPos = this.gameObject;

        // Spawns Player
        DontDestroyOnLoad(gameObject);

    }

    void Update()
    {
        if (gameManager.instance.activeMenu == null)
        {

            MP = Mathf.MoveTowards(MP, maxMP, Time.deltaTime);
            gameManager.instance.playerHUD.updatePlayerMana();
            OnPlayerCrouch();
            OnPlayerUncrouch();
            Movement();
            if (staffList.Count > 0)
            {
                SwitchStaff();
            }
            if (SwordList.Count > 0)
            {
                SwitchSword();
            }
            if (BowList.Count > 0)
            {
                SwitchBow();
            }

            if (Input.GetKeyDown(KeyCode.E) && isDashing == 0 && !isCrouching)
            {

                playerDash();
                StartCoroutine(WaitForDash());
            }
            //if (Input.GetKeyDown(KeyCode.R) || staffList.Count != 0 && staffList[selectedStaff].ammoClip <= 0 && StaffEquipped)
            //{
            //    StartCoroutine(Reload());
            //}
            //if (Input.GetKeyDown(KeyCode.R) || BowList.Count != 0 && BowList[selectedBow].ammoClip <= 0 && BowEquipped)
            //{
            //    StartCoroutine(Reload());
            //}

            if (Input.GetButton("Shoot") && !isShooting && !isReloading && staffList.Count > 0 && StaffEquipped)
            {


                StartCoroutine(shoot());

            }

            if (Input.GetButtonDown("Shoot") && BowEquipped)
            {
                
               

                
                timer = 0;

            }

            if (Input.GetButton("Shoot") && BowEquipped)
            {
                if (!isPulling)
                {
                  aud.PlayOneShot(BowList[selectedBow].pullSound, BowList[selectedBow].pullVol);
                }

                isPulling = true;
                bowShot = true;
                
               
                timer = Mathf.MoveTowards(timer, 3, Time.deltaTime);
                // Debug.Log(timer);
                Debug.Log(timer);



                //timer = Time.time;
            }
            else if (Input.GetButtonUp("Shoot") && !isShooting && BowEquipped && bowShot/* || timer - Time.time == 4*/)
            {
                //Debug.Log((int)(Time.time - timer));
                //Debug.Log(Mathf.RoundToInt(Time.time - timer));
                StartCoroutine(BowShoot());
                StartCoroutine(BowCoolDown());
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

    IEnumerator BowCoolDown()
    {
        isShooting = true;
        yield return new WaitForSeconds(1);
        //Debug.Log("did thing");
        isShooting = false;
        bowShot = false;
        isPulling = false;
    }
    #region PlayerMovement
    void Movement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            if (playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
                jumpedTimes = 0;
            }
            if (!stepIsPlaying && move.normalized.magnitude > 0.5f)
            {
                StartCoroutine(playSteps());
            }
        }

        move = (transform.right * Input.GetAxis("Horizontal")) +
               (transform.forward * Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && jumpedTimes < maxJumps)
        {
            aud.PlayOneShot(audJump[Random.Range(0, audJump.Length)], audJumpVol);
            jumpedTimes++;
            playerVelocity.y = jumpHeight;
        }

        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        pushBack = Vector3.Lerp(pushBack, Vector3.zero, Time.deltaTime * pushBackResolve);
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

    void playerDash()
    {
        isDashing++;
        StartCoroutine(Dash());
    }

    IEnumerator WaitForDash()
    {
        // How long the player has to wait before dashing again
        gameManager.instance.playerHUD.dashCooldown();
        yield return new WaitForSeconds(dashCoolDown);
        isDashing = 0;
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
    IEnumerator playSteps()
    {
        stepIsPlaying = true;
        aud.PlayOneShot(audSteps[Random.Range(0, audSteps.Length)], audStepsVol);
        if (!isSprinting)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }
        stepIsPlaying = false;
    }

    #endregion

    #region Staff
    IEnumerator shoot()
    {
        if (MP >= 5 || staffList[selectedStaff].baseStaff)
        {
            if (MP <= 1.9)
            {
                yield break;
            }

                isShooting = true;



                aud.PlayOneShot(staffList[selectedStaff].shootSound, staffList[selectedStaff].shootVol);

                if (!staffList[selectedStaff].baseStaff)
                    MP -= 5;
                else
                    MP -= 2;



                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    destination = hit.point;
                }
                else
                {
                    destination = ray.GetPoint(staffList[selectedStaff].shootDistance);
                }
                // Creates bullet object and shoots it towards the center ray of the camera
                GameObject bulletToShoot = Instantiate(staffList[selectedStaff].BulletToShoot, bulletPoint.transform.position, Camera.main.transform.rotation);
                bulletToShoot.GetComponent<Rigidbody>().AddForce(bulletPoint.transform.forward * (staffList[selectedStaff].shootDistance * 100));
                //Destroy(staffList[selectedStaff].BulletToShoot, 2,);

                //Muzzle Flash
                GameObject muzzle = GameObject.FindGameObjectWithTag("MuzzleFlash");
                Instantiate(staffList[selectedStaff].muzzleEffect, muzzle.transform.position, staffList[selectedStaff].muzzleEffect.transform.rotation);
            }


            gameManager.instance.playerHUD.updatePlayerMana();
            yield return new WaitForSeconds(staffList[selectedStaff].shootRate);
            isShooting = false;
        
    }

    public void staffPickup(Staff_Stats stats)
    {
        staffList.Add(stats);

        shootDamage = stats.shootDamage;
        shootDistance = stats.shootDistance;
        shootRate = stats.shootRate;

        staffModel.mesh = stats.model.GetComponent<MeshFilter>().sharedMesh;
        staffMat.material = stats.model.GetComponent<MeshRenderer>().sharedMaterial;
        StaffEquipped = true;
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
        //gameManager.instance.UpdateAmmoCount();
    }
    #endregion

    #region Bow
    IEnumerator BowShoot()
    {

        if (MP <= 9 && !BowList[selectedBow].baseStaff)
        {
            yield break;
        }

        aud.PlayOneShot(BowList[selectedBow].shootSound, BowList[selectedBow].shootVol);
        if (BowList[selectedBow].fire || BowList[selectedBow].ice || BowList[selectedBow].earth)
        {
            MP -= 10;
        }


        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {

            destination = ray.GetPoint(BowList[selectedStaff].bowShootDistance);
        }
        // Creates bullet object and shoots it towards the center ray of the camera
        GameObject bulletToShoot = Instantiate(BowList[selectedBow].arrowToShoot, arrowPoint.transform.position, Camera.main.transform.rotation);
        bulletToShoot.GetComponent<Rigidbody>().velocity = (destination - arrowPoint.transform.position).normalized * (timer + BowList[selectedStaff].bowShootDistance);
        //Destroy(bulletToShoot, 1);

        ////Muzzle Flash
        //GameObject muzzle = GameObject.FindGameObjectWithTag("MuzzleFlash");
        ////Instantiate(BowList[selectedStaff].muzzleEffect, muzzle.transform.position, staffList[selectedStaff].muzzleEffect.transform.rotation);


        //}
        gameManager.instance.playerHUD.updatePlayerMana();
        yield return new WaitForSeconds(bowShootRate);
        //isShooting = false;

    }
    void SwitchBow()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectedBow < BowList.Count - 1 && !bowShot)
        {
            selectedBow++;
            ChangeBowStats();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedBow > 0 && !bowShot)
        {
            selectedBow--;
            ChangeBowStats();
        }
    }
    void ChangeBowStats()
    {
        shootDamage = BowList[selectedBow].bowShootDamage;
        shootDistance = BowList[selectedBow].bowShootDistance;
        bowModel.mesh = BowList[selectedBow].model.GetComponent<MeshFilter>().sharedMesh;
        bowMat.material = BowList[selectedBow].model.GetComponent<MeshRenderer>().sharedMaterial;
        //gameManager.instance.UpdateAmmoCount();
    }
    public void BowPickup(BowStats stats)
    {
        BowList.Add(stats);

        shootDamage = stats.bowShootDamage;
        shootDistance = stats.bowShootDistance;
        //shootRate = stats.bow;

        bowModel.mesh = stats.model.GetComponent<MeshFilter>().sharedMesh;
        bowMat.material = stats.model.GetComponent<MeshRenderer>().sharedMaterial;
        BowEquipped = true;
        selectedBow = BowList.Count - 1;
        //gameManager.instance.UpdateAmmoCount();
    }

    #endregion

    #region Sword
    public void swordPickup(SwordStats stat)
    {
        SwordList.Add(stat);

        shootDamage = stat.swingtDamage;

        //shootRate = stats.bow;

        swordModel.sharedMesh = stat.model.GetComponent<MeshFilter>().sharedMesh;
        swordMat.sharedMaterial = stat.model.GetComponent<MeshRenderer>().sharedMaterial;
        SwordEquipped = true;
        selectedSword = SwordList.Count - 1;
        //gameManager.instance.UpdateAmmoCount();
    }

    void SwitchSword()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectedSword < SwordList.Count - 1)
        {
            selectedSword++;
            ChangeSwordStats();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedSword > 0)
        {
            selectedSword--;
            ChangeSwordStats();
        }
    }

    void ChangeSwordStats()
    {
        //shootDamage = staffList[selectedStaff].shootDamage;
        //shootDistance = staffList[selectedStaff].shootDistance;
        //shootRate = staffList[selectedStaff].shootRate;
        //GameObject magic = GameObject.FindGameObjectWithTag("MagicEffect");
        //Instantiate(SwordList[selectedSword].magicEffect, magic.transform.position, (SwordList[selectedSword].magicEffect.transform.rotation));
        swordModel.mesh = SwordList[selectedSword].model.GetComponent<MeshFilter>().sharedMesh;
        swordMat.material = SwordList[selectedSword].model.GetComponent<MeshRenderer>().sharedMaterial;
        //gameManager.instance.UpdateAmmoCount();
    }
    #endregion

    #region PlayerFeatures
    public void takeDamage(int amount)
    {
        // Player will take damage based off the amount 
        HP -= amount;
        if (amount > 0)
            aud.PlayOneShot(audDamage[Random.Range(0, audDamage.Length)], audDamageVol);
        gameManager.instance.playerHUD.updatePlayerHealth(amount);
        if (HP <= 0)
        {
            HP = 0;
            // Player is dead and display game over screen.
            gameManager.instance.GameOver();
        }
    }

    public void Spawn()
    {
        controller.enabled = false;
        transform.position = gameManager.instance.playerSpawnPos.transform.position;
        transform.rotation = gameManager.instance.playerSpawnPos.transform.rotation;
        controller.enabled = true;
        HP = maxHP;
        gameManager.instance.playerHUD.updatePlayerHealth(0);
    }

    public void SpawnOnLoad()
    {
        controller.enabled = false;
        transform.position = gameManager.instance.playerSpawnPos.transform.position;
        transform.rotation = gameManager.instance.playerSpawnPos.transform.rotation;
        controller.enabled = true;
    }
    public void KnockBack(Vector3 dir)
    {
        pushBack += dir;
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

        if (StaffEquipped)
        {
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


        }
        if (BowEquipped)
        {
            if (BowList[selectedBow].ammoReserves < BowList[selectedBow].origAmmo)
            {
                BowList[selectedBow].ammoClip = BowList[selectedBow].ammoReserves;
                BowList[selectedBow].ammoReserves = 0;
            }
            else
            {
                BowList[selectedBow].ammoReserves -= (BowList[selectedBow].origAmmo - BowList[selectedBow].ammoClip);
                BowList[selectedBow].ammoClip = BowList[selectedBow].origAmmo;
            }

        }

        yield return new WaitForSeconds(2);
        gameManager.instance.UpdateAmmoCount();
        isReloading = false;
    }

    public void AddEXP(int amount)
    {
        currExp += amount;
        gameManager.instance.playerHUD.UpdatePlayerEXP(amount);
        if (currExp >= expToNextLevel)
        {
            level++;
            skillPoints++;
            gameManager.instance.invManager.UpdateSkillPoints();
            currExp -= expToNextLevel;
            gameManager.instance.playerHUD.UpdatePlayerLevel();
        }
    }
    #endregion
    public MeshFilter GetStaffModel()
    {
        return staffModel;
    }
    public MeshRenderer GetStaffMat()
    {
        return staffMat;
    }
    public MeshFilter GetBowModel()
    {
        return bowModel;
    }
    public MeshRenderer GetBowMat()
    {
        return bowMat;
    }
    public MeshFilter GetSwordModel()
    {
        return swordModel;
    }
    public MeshRenderer GetSwordMat()
    {
        return swordMat;
    }


    //#region Attacking Functions
    //IEnumerator MeleeSlash()
    //{
    //    isAttacking = true;
    //    animr.SetTrigger("Attacking");
    //    yield return new WaitForSeconds(AttackRate);
    //    isAttacking = false;
    //}
    //public void AttackingOn()
    //{
    //    PMeleeObj.enabled = true;
    //}
    //public void AttackingOff()
    //{
    //    PMeleeObj.enabled = false;
    //}
    //#endregion
}
