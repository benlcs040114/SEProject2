using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Modification : MonoBehaviour
{
    public Text txt;
    string nandu1, nandu2, nandu3;
    private void Awake()
    {
        nandu1 = "easy";
        nandu2 = "middle";
        nandu3 = "difficult";
    }
    private void Start()
    {
        Time.timeScale = 0;
    }
    public void Easy()
    {
        txt.text = nandu1;
        EnemyAttack.nav_speed = 0.5f;
        EnemyAttack.attackDamage = 1;
        Time.timeScale = 1;
    }
    public void Middle()
    {
        txt.text = nandu2;
        EnemyAttack.nav_speed = 3f;
        EnemyAttack.attackDamage = 10;
        Time.timeScale = 1;
    }
    public void Difficult()
    {
        txt.text = nandu3;
        EnemyAttack.nav_speed = 6f;
        EnemyAttack.attackDamage = 20;
        Time.timeScale = 1;
    }
}
