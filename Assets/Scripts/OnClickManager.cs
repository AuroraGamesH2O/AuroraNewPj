using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickManager : MonoBehaviour {
    public static OnClickManager instance { get; private set; }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
        
    }

    public void Manage(Scr_PlayerMovement instance)
    {
      
    }
}
