using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

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

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player"|| other.gameObject.tag == "Weight"){
            Door.SetActive(false);
        }
    }

     private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Weight"){
            Door.SetActive(true);
        }
    }
}
