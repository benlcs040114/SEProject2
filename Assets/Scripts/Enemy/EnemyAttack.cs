using UnityEngine;
using System.Collections;
using UnityEngine.AI;
//enemy attack
public class EnemyAttack : MonoBehaviour
{
    public NavMeshAgent nav;
    public static float nav_speed = 3f;//enemy movement speed
    //attack interval
    public float timeBetweenAttacks = 0.5f;
    public static int attackDamage = 10;//attack damage

    Animator anim;//animation component

    GameObject player;//player
    PlayerHealth playerHealth;//player life bar
    EnemyHealth enemyHealth;//enemy life bar
    bool playerInRange;//Determine if the player is within range
    float timer;//timer
    public bool isboss;//Determine if the player is boss
    float bosstimer;//boss timer
    public GameObject[] buttle;
    void Awake()
    {
        nav=GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(player.name);
        playerHealth = player.GetComponent<PlayerHealth>();//get script
       // Debug.Log(playerHealth);
        anim = GetComponent<Animator>();//Get the animation controller
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void FixedUpdate()
    {
        Debug.Log(nav.speed+"=enemy movement speed");
        Debug.Log(attackDamage + "=enemy damage");
        nav.speed = nav_speed;

        timer += Time.deltaTime;//timing
        bosstimer += Time.deltaTime;//timing
        if(timer>= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth>0)
        {
            Attack();//attack
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");//Play player death animation
        }
        if(isboss && bosstimer > 5 && Random.Range(0, 100) > 97)
        {
            BossAttack();//big boss attack
        }
    }
    //attack function
    void Attack()
    {
        timer = 0f;
        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
    //boss attack function
    void BossAttack()
    {
        bosstimer = 0;
        Instantiate(buttle[Random.Range(0,buttle.Length)], transform.position + transform.forward * 2+new Vector3(0,0.9f,0), transform.rotation);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject == player)
        {
            playerInRange = true;
        }


    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == player)
        {
            playerInRange = false;
        }
    }
}
