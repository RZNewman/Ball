using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    PlayerMover pm;
    public float resetTime =0.5f;
    public float spinRPS = 4;
    // Start is called before the first frame update
    void Start()
    {
        pm = transform.parent.GetComponent<PlayerMover>();
    }
    Quaternion resetFrom;
    bool gotReset = false;
    bool reset = false;
    float resetTimeStart;

    // Update is called once per frame
    void Update()
    {
        if (pm.spinning)
        {
            transform.Rotate(new Vector3(360*spinRPS * Time.deltaTime, 0, 0 ));
            reset = false;
            gotReset = false;
        }
        else
        {
            if (!reset)
            {
                if (!gotReset)
                {
                    resetFrom = transform.rotation;
                    gotReset = true;
                    resetTimeStart = Time.time;
                }
                float percent = (Time.time - resetTimeStart) / resetTime;
                transform.rotation = Quaternion.Lerp(resetFrom, Quaternion.identity, percent);
                if (percent > 1)
                {
                    reset = true;
                }
            }
        }
    }
}
