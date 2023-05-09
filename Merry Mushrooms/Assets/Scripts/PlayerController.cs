using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    [Header("----- Player Dash Properties -----")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;

    [Header("----- Weapon Stats -----")]
    [Range(2, 300)][SerializeField] int shootDistance;
    [Range(0.1f, 3)][SerializeField] float shootRate;
    [Range(1, 20)][SerializeField] int shootDamage;

    private float origSpeed;
    private int isDashing;
    private bool isShooting;
    //[Range(1, 3)][SerializeField] float dashUp;
    private void Start()
    {

        //controller = gameObject.AddComponent<CharacterController>();
        origSpeed = playerSpeed;
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
            }

            if (Input.GetButton("Shoot") && !isShooting)
            {
                StartCoroutine(shoot());
            }
        }
        Sprint();
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
        //HP
    }
    IEnumerator shoot()
    {
        isShooting = true;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDistance))
        {
            IDamage damageable = hit.collider.GetComponent<IDamage>();

            if (damageable != null)
            {
                damageable.takeDamage(shootDamage);
            }
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
        playerSpeed *= dashSpeed;
        Debug.Log("Player Dashed");
        yield return new WaitForSeconds(dashTime);
        isDashing = 0;
        playerSpeed = origSpeed;
    }

}
