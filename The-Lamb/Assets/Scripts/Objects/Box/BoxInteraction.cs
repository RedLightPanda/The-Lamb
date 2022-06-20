using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteraction : MonoBehaviour
{

    private Rigidbody2D Rigbox;
    public Transform Lamb;

    // Start is called before the first frame update
    void Start()
    {
        Rigbox = GetComponent<Rigidbody2D>();
        Lamb = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveBox()
    {
        Debug.Log ("Box");
        Rigbox.mass = 1;
        this.transform.SetParent(Lamb.transform);
    }

}
