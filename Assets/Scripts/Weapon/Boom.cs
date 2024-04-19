
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour {

    [SerializeField]
    private float radius = 1.5f;//radius
    [SerializeField]
    private float power = 600f;//damage
    public int damage; //Damage variable

    private void Start()
    {
        StartCoroutine(lifeTime());//Turn on life timer
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);//Get all colliders within the radius
        foreach (Collider hits in colliders)//Iterate over all colliders
        {
            EnemyHealth enemy = hits.GetComponent<EnemyHealth>();//Get the script on the enemy
            if (hits.GetComponent<Rigidbody>() && enemy != null)
            {
                enemy.TakeDamage(damage,transform.position);//enemy takes damage
                hits.GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, radius);//Gives an effect when the enemy takes damage
            }
        }
    }
    

    IEnumerator lifeTime()
    {
        while (GetComponent<ParticleSystem>().isPlaying)//监听报站效果的播放
        {
            yield return null;
        }

        Destroy(this.gameObject);//The explosion effect ends and the game object is destroyed
    }
}
