using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Info Stats/Player", order = 1)]
public class LambStats : ScriptableObject
{
    [Header ("Lamb Stats")]
    
    [SerializeField]
    public float speed;
    
    
    [SerializeField]
    public float jumpSpeed;

    public float Ladderstop;
    
    [Header ("Health")]
    [SerializeField]
    public int Light = 5;

    [Header ("Layer Mask")]
    [SerializeField]
    public LayerMask ground;
    public LayerMask interact;  
    public LayerMask pit;  
    public LayerMask spike;
    public LayerMask climb;
}
