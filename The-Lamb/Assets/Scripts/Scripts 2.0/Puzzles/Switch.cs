using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    private GameObject doorswitch;
    [SerializeField]
    private GameObject Door;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Flame" || other.gameObject.tag == "Stick" ){
            doorswitch.GetComponent<SpriteRenderer>().color = Color.yellow;
            Door.SetActive(false);
        }
    }
}
