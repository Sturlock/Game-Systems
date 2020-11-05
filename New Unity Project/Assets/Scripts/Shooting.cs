//Made by Will "Sturlock" Sturley, if you are given this script please credit me and don't give this script to anyone else
//Thank you!
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Bullet Settings")]
    public Transform firePoint;
    [SerializeField] float timer = 0f;
    public ShootingsObj shootingScriptable;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            Shoot();
            
        }
       
    }

    public void Shoot()
    {
        if (timer < shootingScriptable.firerate) return;

        timer = 0f;

        GameObject bullet = Instantiate(shootingScriptable.bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * shootingScriptable.bulletForce, ForceMode.Impulse);
    }
}
