//Made by Will "Sturlock" Sturley, if you are given this script please credit me and don't give this script to anyone else
//Thank you!
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    Target target;
    public void OnCollisionEnter(Collision collision)
    {
        target = collision.transform.GetComponent<Target>();
               
        target.TakeDamage(damage);
        Destroy(gameObject);
        
        
        
                  
    }
}
