using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    LevelMover lm;
    PlayerMover pm;
    public float kickForce = 2;

    private void Start()
    {
        lm = transform.parent.GetComponent<LevelMover>();
        pm = GetComponent<PlayerMover>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("kick");
        Vector3 dir = transform.forward * 3;
        float diffX = other.transform.position.x - transform.position.x;
        Vector3 diffV = new Vector3(diffX, 0, 0);
        dir += diffV;
        dir.Normalize();

        float str = lm.speed * kickForce;
        if (pm.spinning)
        {
            float diffZ = other.transform.position.z - transform.position.z;
            if (diffZ <0)
            {
                diffZ = 0;
            }
            str *= pm.spinProportion - diffZ;
        }

        other.GetComponent<Rigidbody>().AddForce(dir *str,ForceMode.Impulse);
        lm.kickback(pm.spinning);
    }

}
