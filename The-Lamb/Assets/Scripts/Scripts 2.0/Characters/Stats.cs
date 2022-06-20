using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Player/Stats", order = 1)]
public class Stats : ScriptableObject 
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

}
