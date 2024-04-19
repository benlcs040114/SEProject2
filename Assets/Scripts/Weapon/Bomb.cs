using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public int damage;//damage caused
    public GameObject boom;//instance effects
    public float boomtime;//Effect start time
	// Use this for initialization
	void Start () {
        StartCoroutine(CreateBoom());//Enable the coroutine for generating explosion effects
	}
	
    IEnumerator CreateBoom()
    {
        yield return new WaitForSeconds(boomtime);//waiting time

        GameObject boomEF = Instantiate(boom, transform.position, boom.transform.rotation) as GameObject;//instantiate game object
        boomEF.GetComponent<Boom>().damage = damage;//assign script scalar
        Destroy(this.gameObject);//destroy game object
    }
	// Update is called once per frame
	void Update () {
		
	}
}
