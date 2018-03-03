using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_ObjectsHealth : MonoBehaviour
{
    [SerializeField]
    int healthPoint = 0;
    void Start()
    {

    }
    void Update ()
    {
        if (healthPoint <= 0)
        {
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<Scr_GameManager>().DestroyedGameObjects();
        }
    }

    public void DMGToObject(int DMG)
    {
        healthPoint -= DMG;
    }
}
