using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LayerMask", menuName = "Player/Layer Masks", order = 2)]
public class LayerList : ScriptableObject
{
    [Header("Layer Mask")]
    [SerializeField]
    public LayerMask Ground;
    public LayerMask Pit;
    public LayerMask Spike;
    public LayerMask interact;

}
