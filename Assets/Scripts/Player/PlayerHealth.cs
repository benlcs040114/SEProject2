using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;//The player's starting health
    public int currentHealth;//The player's current health
    public Image healthSlider;//player health bar
    public Image damageImage;//Health picture
    public AudioClip deathClip;//sound of death
    public AudioClip winClip;//sound of death
    public float flashSpeed = 4f;//blood deduction speed
    public Color flashColor = new Color(1f, 0, 0, 0.1f);//blood bar color

    Animator anim;//component Animaitor
    AudioSource playerAudio;//sound player
    PlayerMovement playerMovement;//mobile script
    PlayerShooting playerShooting;//shooting script
    bool isDead;//is dead boolean
    bool damaged;//Death or not damage variable value

    public AudioSource BGM;//background music player

    Text Bufftext;//text component
    bool isplusbuff;//Is there a buff
    float buffdisplaytime = 1f;//buff time

    Renderer Damage_color;//damage color component
    Color p_color;//color variable value
   
    private float NextLvExp;//
    public float currExp;
    public int Level;
    public Text[] lvexp;
    public Image expslider;

    List<int> scores;

    public GameObject DieCanvas;//canvas at death
    public GameObject WinCanvas;//canvas at win

    float time;



    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
        Bufftext = GetComponentInChildren<Text>();
        Bufftext.enabled = false;
        foreach(var color in transform.GetComponentsInChildren<Renderer>())
        {
            if (color.transform.name == "FirstPersonController")
            {
                Damage_color = color;
            }
        }
       // p_color = Damage_color.material.color;

        NextLvExp = (((int)Mathf.Pow((Level - 1), 3)) + 15) / 5 * ((Level - 1) * 2 + 20) + (10 - ((((int)Mathf.Pow((Level - 1), 3)) + 15) / 5 * ((Level - 1) * 2 + 20) % 10)) + (Level - 1) * 30;
        foreach(var t in lvexp)
        {
            if (t.name == "Lv") t.text = "Lv." + Level;
            if (t.name == "exp") t.text = currExp + "/" + NextLvExp;
        }
        expslider.fillAmount = currExp / NextLvExp;
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
    }
    bool temp;
    void Update()
    {
        if (time >= 60f&& currentHealth > 0 && !temp)
        {
            temp = true;
            StartCoroutine(Win());
        }

        if (damaged)
        {
            damageImage.color = flashColor;
            p_color = new Color(1, 0.1f, 0.1f);
           
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

            p_color = Color.Lerp(p_color, new Color(1, 1, 1), 0.05f);
        }
        damaged = false;
        
        if (isplusbuff)
        {
            Vector3 camera = Camera.main.transform.position;
            Bufftext.transform.GetComponentInParent<Canvas>().transform.LookAt(camera);
            buffdisplaytime -= Time.deltaTime;
            if (buffdisplaytime <= 0)
            {
                isplusbuff = false;
                Bufftext.enabled = false;
                buffdisplaytime = 1f;
            }
        }

        Damage_color.material.color = p_color;
        foreach (var t in lvexp)
        {
            if (t.name == "Lv") t.text = "Lv." + Level;
            if (t.name == "exp") t.text = currExp + "/" + NextLvExp;
        }
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.fillAmount = (float)currentHealth / (float)startingHealth;
        playerAudio.Play();

        if(currentHealth<=0 && !isDead)
        {
            StartCoroutine(Death());
        }
    }
    /// <summary>
    /// player dies
    /// </summary>
    IEnumerator Death()
    {
        isDead = true;

        playerShooting.DisableEffects();

        anim.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();
        
       // playerMovement.enabled = false;
        playerShooting.enabled = false;
        BGM.Stop();
        yield return new WaitForSeconds(3);
        DieCanvas.SetActive(true);
    }

    /// <summary>
    /// player wins
    /// </summary>
    IEnumerator Win()
    {
        playerShooting.DisableEffects();

        //anim.SetTrigger("Die");
        playerAudio.clip = winClip;
        playerAudio.Play();

        // playerMovement.enabled = false;
        playerShooting.enabled = false;
        BGM.Stop();
        yield return new WaitForSeconds(0f);
        WinCanvas.SetActive(true);
    }


    /// <summary>
    /// read scene
    /// </summary>
    public void RestartLevel(InputField obj)
    {
        //Open the ranking document, if no document was created
        FileStream ifr = new FileStream(Application.dataPath + "/ScoreList.txt", FileMode.OpenOrCreate);
        ifr.Close();//close document

        string[] txts = File.ReadAllLines(Application.dataPath + "/ScoreList.txt");
        List<string> list = new List<string>();
        System.Array.ForEach(txts, t => list.Add(t));//Add each line of the document to a list
        string playertxt = obj.text +","+Level + "," + ScoreManager.score;
        list.Add(playertxt);//Add this player information to the document
        //Reorder by score
        list.Sort((x, y) => int.Parse(y.Split(',')[2]) - int.Parse(x.Split(',')[2]));

        File.WriteAllLines(Application.dataPath + "/ScoreList.txt", list.ToArray());

        SceneManager.LoadScene(0);
    }

    void OnTriggerEnter(Collider col)//trigger
    { 
        if (col.CompareTag("Item"))
        {
            currentHealth = currentHealth + 10 > startingHealth ? currentHealth = startingHealth : currentHealth + 10;
            Destroy(col.gameObject);
            healthSlider.fillAmount = (float)currentHealth / (float)startingHealth;
            Bufftext.enabled = true;
            Bufftext.text = "+10";
            isplusbuff = true;
        }
    }

    public void ExpUpdate(float exp)//Statistical current value
    {
        currExp = currExp + exp >= NextLvExp ? LevelUp(currExp+exp) : currExp + exp;
        expslider.fillAmount = currExp / NextLvExp;

    }
    float LevelUp(float exp)//level upgrade function
    {
        Level += 1;
        startingHealth += Level * 5;
        currExp =   exp - NextLvExp;
        NextLvExp = (((int)Mathf.Pow((Level - 1), 3)) + 15) / 5 * ((Level - 1) * 2 + 20) + (10 - ((((int)Mathf.Pow((Level - 1), 3)) + 15) / 5 * ((Level - 1) * 2 + 20) % 10)) + (Level - 1) * 30;
        if (currExp >= NextLvExp) LevelUp(currExp);
        return currExp;
    }
}
