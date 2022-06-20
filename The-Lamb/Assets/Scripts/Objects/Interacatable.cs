using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interacatable : MonoBehaviour
{
    public bool PlayerinRange;
    public UnityEvent Interactaction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerinRange){
            if (Input.GetButton("Fire1")){
                Debug.Log("Ya you got it.");
                Interactaction.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")){
            PlayerinRange = true;
        }   
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")){
            PlayerinRange = false;
        }   
    }
}
