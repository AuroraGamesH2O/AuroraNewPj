using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Scr_PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 0, jumpPower = 0 , fallMultiplier = 0;
    [SerializeField]
    Sprite jumpSprite = null, punchSprite = null;
    Sprite idle;
    Rigidbody2D myRig;
    SpriteRenderer mySpriterenderer;
    GameObject crakedGroundGO, punchGameOBJ;
    Quaternion playerRot;
    bool playerLeftRot = false, playerRightRot = false, grounded = false, crackedGround = false, moveRight = false, moveLeft = false , timeTrigger = false;
    string jumpBottonState = null;
    float time = 0;

    void Start()
    {
        playerRot = gameObject.transform.rotation;
        punchGameOBJ = GameObject.FindGameObjectWithTag("Punch");
        mySpriterenderer = gameObject.GetComponent<SpriteRenderer>();
        idle = mySpriterenderer.sprite;
        myRig = gameObject.GetComponent<Rigidbody2D>();
        myRig.velocity = new Vector2(0, 0);
    }

    void Update()
    {

        #region         //Player Normal Move

        myRig.velocity = new Vector2(0,myRig.velocity.y);

        if (Input.GetKey(KeyCode.RightArrow) || moveRight)
        {
            playerRightRot = true;
            playerLeftRot = false;
            myRig.velocity = new Vector2(speed, myRig.velocity.y);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || moveLeft)
        {
            playerLeftRot = true;
            playerRightRot = false;
            myRig.velocity = new Vector2(-speed, myRig.velocity.y);
        }

        if (playerRightRot)
        {
            playerRot.y = Mathf.LerpAngle(playerRot.y,180,Time.deltaTime /5);
            transform.rotation = playerRot;
        }

        if (playerLeftRot)
        {
            playerRot.y = Mathf.LerpAngle(playerRot.y, 0, Time.deltaTime *22);
            transform.rotation = playerRot;
        }
        #endregion


        #region //PlayerJump&Punch

        if (timeTrigger)
        {
            time += Time.deltaTime * 10;
            Debug.Log(time);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || jumpBottonState == "Down")
        {
            timeTrigger = true;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || jumpBottonState == "Up")
        {
            timeTrigger = false;

            if (time <= 1.80f)
            {
                Debug.Log("Punch");
                mySpriterenderer.sprite = punchSprite;
                punchGameOBJ.GetComponent<Scr_Smash>().SmashOrder();
                time = 0f;
                jumpBottonState = null;
            }
        }

        if (time > 1.90f)
        {
            if (grounded)
            {
                Debug.Log("Jump");
                myRig.velocity = Vector2.up * jumpPower;
            }

            if (crackedGround)
            {
                Destroy(crakedGroundGO);
            }

            time = 0;
            jumpBottonState = null;
        }
        #endregion 


        else if (grounded || crackedGround)
        {
            mySpriterenderer.sprite = idle;
        }

        else if (!grounded && !crackedGround)
        {
            if (myRig.velocity.y < 0)
            {
                myRig.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            mySpriterenderer.sprite = jumpSprite;
        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.transform.tag == "Ground" || target.transform.tag == "BreakableObjects")
        {
            grounded = true;
            mySpriterenderer.sprite = idle;
        }

        if (target.transform.tag == "CrackedGround")
        {
            crackedGround = true;
            crakedGroundGO = target.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D target)
    {
        if (target.transform.tag == "Ground" || target.transform.tag == "BreakableObjects")
        {
            grounded = false;
        }

        if (target.transform.tag == "CrackedGround")
        {
            crackedGround = false;
            crakedGroundGO = null;
        }
    }

    #region //Buttons
    public void MoveButtonRightDown()
    {
        moveRight = true;
    }
    public void MoveButtonRightUp()
    {
        moveRight = false;
    }
    public void MoveButtonLeftDown()
    {
        moveLeft = true;
    }
    public void MoveButtonLeftUp()
    {
        moveLeft = false;
    }
    public void punchAndJumpDown()
    {
        jumpBottonState = "Down";
    }
    public void punchAndJumpUP()
    {
        jumpBottonState = "Up";
    }
    #endregion
}
