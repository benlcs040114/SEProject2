using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
//Control enemy blood volume script
public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;//Start HP
    public int currentHealth;//current blood volume
    public float sinkSpeed = 2.5f;//Moving speed
    public int scoreValue = 10;//player's blood
    public float Exp;

    public AudioClip deathClip;//death sound
    Animator anim;//animation component
    AudioSource enemyAudio;//music player
    bool isDead;//judge dead boolean
    bool playerDead;//dead boolean
    ParticleSystem hitparticles;//particle effects
    CapsuleCollider capsulecollider;//collider
    bool isSinking;//whether to sink

    public GameObject HpDisplay;//game object
    public Image HpBar;

    public GameObject kusuri;

    PlayerHealth player;
    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
       
        hitparticles = GetComponentInChildren<ParticleSystem>();
        capsulecollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
        Transform camera = Camera.main.transform;

        HpDisplay.transform.LookAt(camera);
        HpBar.fillAmount = (float)currentHealth / (float)startingHealth;
    }
    public void TakeDamage(int amount,Vector3 hitPoint)//result in damage
    {
        if (isDead)//determine whether death
            return;
        enemyAudio.Play();//Play sound
      
        currentHealth -= amount;//health reduction

        hitparticles.transform.position = hitPoint;//this centralized location
        hitparticles.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();//enemy death call
        } 
    }

    void Death()
    {
        isDead = true;
        System.Array.ForEach(GetComponents<Collider>(),col=>col.enabled = false);
        anim.SetTrigger("Dead");
        enemyAudio.clip = deathClip;
        enemyAudio.Play();

      if(Random.Range(0,100) < 8)
        {
            Instantiate(kusuri, transform.position+new Vector3(0,0.5f,0), Quaternion.identity);
        }
    }
    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;//get script
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;//sink
        ScoreManager.score += scoreValue;//score increase
        player.ExpUpdate(Exp);
        Destroy(gameObject, 2f);//delete game object
    }

   
}
