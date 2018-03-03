using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Camera : MonoBehaviour
{
    [SerializeField]
    float SmoothTimey = 0 , SmoothTimex = 0;
    private Vector2 Velocity;
    private GameObject Player ,canvas;
    bool startLvl = false ,trigger = false;

    void Start()
    {
        gameObject.GetComponent<Camera>().orthographicSize = 20;
        gameObject.transform.position = new Vector3 (0,0,-10);
        Player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        canvas.SetActive(false);
    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0 || Input.GetKey(KeyCode.F) && !startLvl)
        {
            trigger = true;
        }

        if (trigger && !startLvl)
        {
            if (gameObject.GetComponent<Camera>().orthographicSize >= 10)
            {
                gameObject.GetComponent<Camera>().orthographicSize -= Time.deltaTime * 20;
                canvas.SetActive(true);
            }

            if (gameObject.GetComponent<Camera>().orthographicSize <= 10)
            {
                startLvl = true;
                gameObject.GetComponent<Camera>().orthographicSize = 10;
            }
        }

        if (startLvl)
        {
            float X = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x, ref Velocity.x, SmoothTimex);
            float Y = Mathf.SmoothDamp(transform.position.y, Player.transform.position.y, ref Velocity.y, SmoothTimey);
            transform.position = new Vector3(X, Y, -10);
        }
    }
}
