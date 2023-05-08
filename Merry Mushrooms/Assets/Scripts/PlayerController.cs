using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 move;
    private int jumpedTimes;
    private bool groundedPlayer;
    [Header("----- Player Stats -----")]
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravityValue;
    [Range(1, 3)][SerializeField] int maxJumps;
    [Range(2, 3)][SerializeField] int sprintSpeed;
    [Header("----- Player Dash Properties -----")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;

    private float origSpeed;
    private int isDashing;
    //[Range(1, 3)][SerializeField] float dashUp;
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        origSpeed = playerSpeed;
    }

    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.E) && isDashing == 0)
        {
            playerDash();
            
        }
        //playerDash();
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
