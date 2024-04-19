using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BombUI : MonoBehaviour {
    public static BombUI instance;//singleton

    public float skillTime = 3;//Skill Cooldown
    public Text skillNum;//text
    public Image skillBar;//skill picture
    public int maxNum = 3;//greatest amount

    private int currNum = 0;
    public int CurrBombNum { get { return currNum; } set { currNum = value; } }//Current number of grenades
    private void Start()
    {
        instance = this;//Assign a value to a singleton variable
        StartCoroutine(NumUpdate());//Start the skill cooling time timing coroutine
    }

    IEnumerator NumUpdate()
    {
        float currTime = skillTime;//Current timing variable assignment
        while (true)
        {
            if (currNum < maxNum)
            {
                currTime -= Time.deltaTime;//countdown
                skillBar.fillAmount = currTime / skillTime;//ui changes
                if (currTime <= 0)
                {
                    currTime = skillTime;
                    currNum += 1;//count plus 1
                }
                skillNum.text = "Bombx <color=#67FEFF>" + currNum + "</color>";//update ui text
            }
            yield return new WaitForFixedUpdate();//The coroutine waits for a frame
        }
    }
}
