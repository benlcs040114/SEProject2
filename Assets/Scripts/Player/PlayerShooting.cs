using UnityEngine;
/// <summary>
/// The player shoots the script
/// </summary>
public class PlayerShooting : MonoBehaviour
{
    public int DamagePerShot = 20;//bullet damage
    private int damage;//damage
    public float TimeBetweenBullets = 0.15f;//shot interval
    public float Range = 100;//farthest shooting distance

    public GameObject Efect;//special effects when shooting

    float timer;//time

    Ray shootRay;//Rays
    RaycastHit shootHit;//ray return information
    int shootableMask;//Radiographic inspection level
    ParticleSystem gunParticles;//particle effects
    LineRenderer gunLine;//draw a line
    AudioSource gunAudio;//sound player
    Light gunLight;//light
    float effectsDisplayTime = 0.2f;
    PlayerHealth PH;//player script

    [SerializeField]
    private GameObject Boom;//explode game object
    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        PH = transform.parent.parent.parent.GetComponent<PlayerHealth>();
        damage = DamagePerShot + PH.Level * 5;
    }
  
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0) && timer >= TimeBetweenBullets && Time.timeScale != 0)  // mouse control
        {
            Shoot();
        }
        if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.E)) && BombUI.instance.CurrBombNum>0)//mouse control
        {
            CreateBoom();
        }
        if (timer >= TimeBetweenBullets * effectsDisplayTime)//timer
        {
            DisableEffects();
        }
        damage = DamagePerShot + PH.Level * 5;
    }
    //shooting function
    void Shoot()
    {
        timer = 0f;
        gunAudio.Play();
        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, Camera.main.transform.position);

        shootRay.origin = Camera.main.transform.position;
        shootRay.direction = Camera.main.transform.forward;

        if(Physics.Raycast(shootRay,out shootHit, Range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage, shootHit.point);
                Instantiate(Efect, shootHit.point, Quaternion.identity);
            }

            gunLine.SetPosition(1, shootHit.point);
        }
        else if(Physics.Raycast(shootRay,out shootHit, Range, LayerMask.GetMask("Floor")))
        {
            gunLine.SetPosition(1, shootHit.point);
            Instantiate(Efect, shootHit.point, Quaternion.identity);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * Range);
        }
    }
    //Example Word Bomb
    void CreateBoom()
    {
        BombUI.instance.CurrBombNum -= 1;
        //  Vector3 pos = PlayerMovement.instance.GetMousePoint;
        Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward * 2;
        pos.y = 2f;
        GameObject bomb = Instantiate(Boom, pos, Quaternion.identity) as GameObject;
        bomb.GetComponent<Bomb>().damage = DamagePerShot * 2 + PH.Level * 7;
        bomb.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward*50);
    }
    //Turn off the gun shooting effect
    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
}
