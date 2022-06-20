using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LambsAtk : MonoBehaviour
{
    [SerializeField]
    private Transform Firepoint;
    [SerializeField]
    private GameObject Fire,Rod;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        #region Shooting
        if(Input.GetButtonDown("Fire2"))
        {
            Shoot();
        }
        #endregion

         #region Swing
        if(Input.GetButtonDown("Fire1"))
        {
            Rod.SetActive(true);
            //Debug.Log("Swing my sword");
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            Rod.SetActive(false);
            //Debug.Log("Keeping it my pants");
        }
        #endregion   
    }

    #region Swingy thing
    void Swingathing()
    {
        Rod.SetActive(true);    
    }
    #endregion
    
    #region Projectile
    void Shoot()
    {
        Instantiate(Fire, Firepoint.position, Firepoint.rotation);
    }
    #endregion
}
