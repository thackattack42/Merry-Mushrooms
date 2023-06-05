using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageEnemy_Scpt : MonoBehaviour
{
    #region fields
    [Header("------ Stats ------")]
    // [Range(5, 100)][SerializeField] public int maxEnemyHP;
    [Range(5, 1000)][SerializeField] public int HP;
    [Range(5, 100)][SerializeField] int playerFaceSpeed;
    public int viewCone;
    public float animrTransSpeed;
    [SerializeField] int roamDist;
    [SerializeField] int roamPauseTime;

    [Header("------ Componets ------")]
    [SerializeField] Renderer model;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] Transform shootPos;
    public Transform headPos;
    [SerializeField] public Animator animr;
    [SerializeField] AudioSource aud;

    [Header("------ Weapon Stats ------")]
    [Range(5, 10)][SerializeField] int shootDist;
    [Range(0.1f, 10)][SerializeField] float ShootRate;
    [Range(30, 180)][SerializeField] float ShootAngle;
    [SerializeField] GameObject bullet;

    [Header("------ Audio ------")]
    [SerializeField] AudioClip[] audShoot;

    [Header("------ Audio Vol ------")]
    [SerializeField] float audShootVol;
    //Other Assets
    Color origColor;
    private bool isShooting;
    public Vector3 playerDir;
    public bool playerInRange;
    public float angleToPlayer;
    float speed;
    bool DestChosen;
    Vector3 startingPos;
    public float stoppingDistOrig;
    float viewDistOrig;

    #endregion
    #region Start and Update
    // Start is called before the first frame update
    public void Start()
    {


        startingPos = transform.position;
        stoppingDistOrig = agent.stoppingDistance;
        //gameManager.instance.UpdateGameGoal(1);
        origColor = model.material.color;
        viewDistOrig = GetComponent<SphereCollider>().radius;
    }

    // Update is called once per frame
    public void Update()
    {
        if (agent.isActiveAndEnabled)
        {
            speed = Mathf.Lerp(speed, agent.velocity.normalized.magnitude, Time.deltaTime * animrTransSpeed);
            animr.SetFloat("Speed", speed);

            if (!CompareTag("Boss Enemy"))
            {
                if (playerInRange && !canSeePlayer())
                {
                    StartCoroutine(Roam());
                }
                else if (agent.destination != gameManager.instance.player.transform.position)
                {
                    StartCoroutine(Roam());
                }

            }
            else
            {
                if (playerInRange && !canSeePlayer()) { }
                else if (agent.destination != gameManager.instance.player.transform.position) { }
            }
        }
    }
    #endregion
    #region Functions
    #region If Hit Color Flash
    public IEnumerator FlashHitColor() //flash when the enemy is hit
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = origColor;
    }
    #endregion
    #region Shooting Functions
    IEnumerator shoot()
    {
        isShooting = true;
        animr.SetTrigger("MageAttack");
        yield return new WaitForSeconds(ShootRate);
        isShooting = false;
    }
    public void createBullet()
    {
        Instantiate(bullet, shootPos.position, transform.rotation);
    }
    #endregion
    #region Collider Enter/Exit
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    //public void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("PlayerBulletTag"))
    //    {

    //        //GetComponent<IceEnemy_Scpt>().TakeEarthDamage(2);
    //        GetComponent<FireEnemy_Scpt>().TakeIceDamage(2);
    //       // GetComponent<EarthEnemy_Scpt>().TakeFireDamage(2);
    //    }
    //}
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.stoppingDistance = 0;
            playerInRange = false;
        }
    }
    #endregion
    #region Enemy's Vision
    public void FacePlayer()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerFaceSpeed);
    }
    public bool canSeePlayer()
    {
        playerDir = gameManager.instance.player.transform.position - headPos.position;
        angleToPlayer = Vector3.Angle(new Vector3(playerDir.x, 0, playerDir.z), transform.forward);

        Debug.DrawRay(headPos.position, playerDir);

        RaycastHit hit;
        if (Physics.Raycast(headPos.position, playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= viewCone)
            {
                agent.stoppingDistance = stoppingDistOrig;
                agent.SetDestination(gameManager.instance.player.transform.position);

                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    FacePlayer();
                }

                if (!isShooting && angleToPlayer <= ShootAngle)
                {
                    //aud.PlayOneShot(audShoot[Random.Range(0, audShoot.Length)], audShootVol);
                    StartCoroutine(shoot());
                }
                return true;
            }
        }
        agent.stoppingDistance = 0;
        return false;
    }
    #endregion
    #region Movement Actions
    public IEnumerator Roam()
    {
        if (!DestChosen && agent.remainingDistance < 0.05f)
        {
            DestChosen = true;
            agent.stoppingDistance = 0;
            yield return new WaitForSeconds(roamPauseTime);
            DestChosen = false;

            Vector3 ranPos = Random.insideUnitSphere * roamDist;
            ranPos += startingPos;

            NavMeshHit hitdest;
            NavMesh.SamplePosition(ranPos, out hitdest, roamDist, 1);

            agent.SetDestination(hitdest.position);
        }
    }
    #endregion
    #region Crouch Vision functions
    void OnEnable()
    {
        PlayerController.Crouch += ReduceVision;
        PlayerController.Uncrouch += IncreaseVision;
    }
    void OnDisable()
    {
        PlayerController.Crouch -= ReduceVision;
        PlayerController.Uncrouch -= IncreaseVision;
    }

    void ReduceVision()
    {
        if (!canSeePlayer())
            GetComponent<SphereCollider>().radius /= 2;
    }
    void IncreaseVision()
    {
        GetComponent<SphereCollider>().radius = viewDistOrig;
    }
    #endregion
    #region Physics Functions
    public void KnockBack(Vector3 dir)
    {
        agent.velocity += dir;
    }
    #endregion
    #region Enemy's Death
    public IEnumerator EnemyDespawn()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
    #endregion
    #endregion
}
