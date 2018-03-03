using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scr_GameManager : MonoBehaviour
{
    [SerializeField]
    float lvlTime = 0;
    GameObject playerGo, countertextGO ,comboTextGO ,scoreTextGO ,leftButtonGO , rightButtonGO , jumpAndPunchButtonGO;
    Text countertext ,comboText ,scoreText;
    int score = 0, comboCounter = 1 ,destroyedGameObjects = 0;
    float combotimer = 0;
    bool timeTrigger = false;
    void Awake ()
    {
        playerGo = GameObject.FindGameObjectWithTag("Player");
        leftButtonGO = GameObject.FindGameObjectWithTag("LeftButton");
        rightButtonGO = GameObject.FindGameObjectWithTag("RightButton");
        jumpAndPunchButtonGO = GameObject.FindGameObjectWithTag("JumpAndPunchButton");
        countertextGO = GameObject.FindGameObjectWithTag("Counter");
        countertext = countertextGO.GetComponent<Text>();
        comboTextGO = GameObject.FindGameObjectWithTag("Combo");
        comboText = comboTextGO.GetComponent<Text>();
        scoreTextGO = GameObject.FindGameObjectWithTag("Score");
        scoreText = scoreTextGO.GetComponent<Text>();
        scoreTextGO.GetComponent<Animator>().enabled = false;
    }



    void Update()
    {
        countertext.text = (lvlTime.ToString("0"));
        lvlTime -= Time.deltaTime;

        scoreText.text = (score.ToString("0"));
        if (lvlTime <= 0)
        {
            // Close LvL
        }

        //Combo

        if (timeTrigger)
        {
            combotimer -= Time.deltaTime;
        }

        if (destroyedGameObjects >= 1 && destroyedGameObjects <= 5)
        {
            comboText.text = ("Combo X " + destroyedGameObjects);
            comboCounter = destroyedGameObjects;
            timeTrigger = true;

            if (combotimer <= 0)
            {
                timeTrigger = false;
                destroyedGameObjects = 0;
                comboText.text = ("");
            }
        }

        if (destroyedGameObjects > 5)
        {
            timeTrigger = false;
            destroyedGameObjects = 0;
            comboText.text = ("");
        }
    }
    public void DestroyedGameObjects()
    {
        destroyedGameObjects++;
        combotimer = destroyedGameObjects + 1;
    }

    public void Score()
    {
        score += 1;
        score *= comboCounter;
        scoreTextGO.GetComponent<Animator>().enabled = true;
    }

}
