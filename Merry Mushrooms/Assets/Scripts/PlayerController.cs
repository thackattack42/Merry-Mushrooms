using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
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
    //[SerializeField] MeshFilter staffTexture;
    [SerializeField] MeshRenderer staffMat;

    private float dashTime = 0.3f;
    private float origSpeed;
    private int isDashing;
    private bool isShooting;
    private bool isReloading;
    private bool isSprinting;
    public bool isCrouching;
    private int ammoAmount;
    private int origAmmoClip;
    private float origHeight;
    private int reloadOnce = 0;
    int selectedStaff;

    private void Start()
    {
        // Sets original variables to players starting stats
        origSpeed = playerSpeed;
        origAmmoClip = gameManager.instance.ammoClip;
        controller.height = 2.0f;
        origHeight = controller.height;
        // Spawns Player
        Spawn();
    }

    void Update()
    {
        if (gameManager.instance.activeMenu == null)
        {

            Movement();
            SwitchStaff();
            if (Input.GetKeyDown(KeyCode.E) && isDashing == 0 && !isCrouching)
            {
                playerDash();
                StartCoroutine(WaitForDash());
            }
            if (Input.GetKeyDown(KeyCode.R) && reloadOnce == 0 && gameManager.instance.ammoClip != origAmmoClip)
            {
                //isShooting = false;
                reloadOnce = 1;
                isReloading = true;
                gameManager.instance.UpdateAmmoCount();
                StartCoroutine(WaitForReload());

                //isShooting = false;
            }

            if (Input.GetButton("Shoot") && !isShooting && !isReloading && staffList.Count > 0)
            {
                StartCoroutine(shoot());
            }
        }

        Sprint();
        Crouch();
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
        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}

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
        if (gameManager.instance.ammoClip > 0)
        {
            isShooting = true;

            gameManager.instance.ammoClip--;
            

            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDistance))
            {
                IDamage damageable = hit.collider.GetComponent<IDamage>();

                if (damageable != null)
                {
                    damageable.takeDamage(shootDamage);
                }
            }
            gameManager.instance.UpdateAmmoCount();
            yield return new WaitForSeconds(shootRate);
            isShooting = false;
        }
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
        gameManager.instance.playerHUD.dashCooldown(dashCoolDown);
        yield return new WaitForSeconds(dashCoolDown);
        isDashing = 0;
    }

    IEnumerator WaitForReload()
    {
        isShooting = true;
        yield return new WaitForSeconds(2);
        isShooting = false;
        isReloading = false;
        reloadOnce = 0;
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
        //staffTexture.mesh = stats.model.GetComponent<Texture>();
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
    }
    public void Crouch()
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
}
