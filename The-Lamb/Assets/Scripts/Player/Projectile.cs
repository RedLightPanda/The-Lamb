using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform Firepoint;
    public GameObject Fire;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(Fire, Firepoint.position, Firepoint.rotation);
    }
}
