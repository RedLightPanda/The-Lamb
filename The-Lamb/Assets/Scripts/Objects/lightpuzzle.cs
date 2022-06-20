using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightpuzzle : MonoBehaviour
{
    public TorchScripts touchy;

    [SerializeField]
    private GameObject Door;

    [SerializeField]
    private GameObject Light;

    [SerializeField]
    private int Unlock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Flame"){
            Light.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }


}
