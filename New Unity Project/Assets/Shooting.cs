//Made by Will "Sturlock" Sturley, if you are given this script please credit me and don't give this script to anyone else
//Thank you!
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    Animator Anim;

    public float bulletForce = 20f;

    [Header("Firerate Settings")]
    public float firerate = .05f;
    [SerializeField] float timer = 0f;

    private void Start()
    {
        //Anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            Shoot();
            //Anim.SetTrigger("Fire");
        }
       
    }

    public void Shoot()
    {
        if (timer < firerate) return;

        timer = 0f;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
    }
}
