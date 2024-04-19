using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamenandu : MonoBehaviour
{
    public List<Toggle> toggle_group;

    private void Update()
    {
        if (toggle_group[0].isOn==true)
        {
            EnemyAttack.nav_speed = 0.5f;
            EnemyAttack.attackDamage = 1;
        }

        if (toggle_group[1].isOn == true)
        {
            EnemyAttack.nav_speed = 3f;
            EnemyAttack.attackDamage = 10;
        }

        if (toggle_group[2].isOn == true)
        {
            EnemyAttack.nav_speed = 6f;
            EnemyAttack.attackDamage = 20;
        }
    }
}
