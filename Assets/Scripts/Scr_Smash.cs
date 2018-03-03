using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Smash : MonoBehaviour
{
    GameObject gameMananger;
    int number = 0;
    void Start ()
    {
        gameMananger = GameObject.FindGameObjectWithTag("GameManager");
    }

    // If i use a Trigger collider
    private void OnTriggerStay2D(Collider2D target)
    {
        if (target.transform.tag == "BreakableObjects")
        {
            if (number == 1)
            {
                target.gameObject.GetComponent<Scr_ObjectsHealth>().DMGToObject(10);
                gameMananger.GetComponent<Scr_GameManager>().Score();
                number = 0;
            }
        }
    }

    public void SmashOrder()
    {
        number = 1;
    }
}
