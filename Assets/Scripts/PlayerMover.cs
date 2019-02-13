using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    BoxCollider col;
    PlayerCollision playerC;

    public float maxMove;
    public float moveFactor;

    public float spinHeightPercent = 0.07f;
    public float spinTime = 0.3f;
    public float spinProportion = 4;

    float spinTimeCurrent = 0;

    [HideInInspector]
    public bool spinning = false;

    float spinSense;
    private void Start()
    {
        spinSense = Screen.height * spinHeightPercent;
        col = GetComponent<BoxCollider>();
        playerC = GetComponent<PlayerCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
                float diffX = lp.x - fp.x;
                float diffY = lp.y - fp.y;





                if (Mathf.Abs(diffY) > spinSense)
                {
                    spin();
                }
                else
                {
                    slide(diffX * moveFactor);
                }

                fp = lp;
            
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;
            }
        }

        if (spinning)
        {
            spinTimeCurrent -= Time.deltaTime;
            if (spinTimeCurrent <= 0)
            {
                spinning = false;
                Vector3 newSize = col.size;
                newSize.z = 1;
                col.size = newSize;
            }
        }
        

       
    }
    void slide(float distance)
    {
        Vector3 pos = transform.position + new Vector3(distance, 0, 0);
        if (Mathf.Abs(pos.x) > maxMove)
        {
            pos.x = maxMove * (pos.x > 0 ? 1 : -1);
        }
        transform.position = pos;
    }
    void spin()
    {
        spinning = true;
        spinTimeCurrent = spinTime;
        Vector3 newSize = col.size;
        newSize.z = spinProportion;
        col.size = newSize;
    }
}
