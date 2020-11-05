//Made by Will "Sturlock" Sturley, if you are given this script please credit me and don't give this script to anyone else
//Thank you!
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletsObj bulletScriptable;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DestWall")
        {

            bulletScriptable.target = collision.transform.GetComponent<Target>();

            bulletScriptable.target.TakeDamage(bulletScriptable.damage);
            Destroy(gameObject);
        }
        else { Destroy(gameObject); }


    }
}
