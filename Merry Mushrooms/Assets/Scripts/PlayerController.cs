using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Experimental.GraphView;
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

    [Header("----- Player Dash Properties -----")]
    [SerializeField] float dashSpeed;
    [Range(2, 10)][SerializeField] int dashCoolDown;

    [Header("----- Weapon Stats -----")]
    [Range(2, 300)][SerializeField] int shootDistance;
    [Range(0.1f, 3)][SerializeField] float shootRate;
    [Range(1, 20)][SerializeField] int shootDamage;

    private float dashTime = 0.3f;
    private float origSpeed;
    private int isDashing;
    private int HP;
    private bool isShooting;
    private int ammoAmount;
    float dashCooldownTimer;

    private void Start()
    {
        // Sets original variables to players starting stats
        origSpeed = playerSpeed;
        HP = maxHP;
        // Spawns Player
        Spawn();
    }

    void Update()
    {
        if (gameManager.instance.activeMenu == null)
        {

            Movement();
            if (Input.GetKeyDown(KeyCode.E) && isDashing == 0)
            {
                playerDash();
                StartCoroutine(WaitForDash());
            }

            if (Input.GetButton("Shoot") && !isShooting)
            {
                StartCoroutine(shoot());
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                //isShooting = false;
                gameManager.instance.UpdateAmmoCount();
                StartCoroutine(WaitForReload());
                //isShooting = false;
            }

        }

        Sprint();
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
            gameManager.instance.dashCooldownCounter.text = dashCooldownTimer.ToString("0.0") + "s";
            gameManager.instance.dashCooldownSlider.fillAmount = (dashCooldownTimer / dashCoolDown * 100) * 0.01f;
        }
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
        if (Input.GetButtonDown("Sprint"))
        {
            playerSpeed *= sprintSpeed;

        }
        else if (Input.GetButtonUp("Sprint"))
        {
            playerSpeed /= sprintSpeed;
        }
    }
    void Spawn()
    {
        controller.enabled = false;
        transform.position = gameManager.instance.playerSpawnPos.transform.position;
        controller.enabled = true;
        maxHP = HP;
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
        playerSpeed = origSpeed;
    }

    IEnumerator WaitForDash()
    {
        // How long the player has to wait before dashing again
        dashCooldownTimer = 5f;
        yield return new WaitForSeconds(dashCoolDown);
        isDashing = 0;
        if (dashCooldownTimer <= 0)
        {
            gameManager.instance.dashCooldownCounter.text = "";
            gameManager.instance.dashCooldownSlider.fillAmount = 0f;
        }

    }

    IEnumerator WaitForReload()
    {
        isShooting = true;
        yield return new WaitForSeconds(2);
        isShooting = false;
    }

    public void takeDamage(int amount)
    {
        // Will take damage based off the amount 
        HP -= amount;
        gameManager.instance.healthPoints.text = HP.ToString();
        gameManager.instance.HPSlider.fillAmount = (HP / maxHP * 100) * 0.01f; ;

        if (HP <= 0)
        {
            // Player is dead and display game over screen.
            gameManager.instance.GameOver();
        }
    }
}
