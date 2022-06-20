using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlat : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float wait;

    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyBDown();
    }

    void KeyBDown(){

        if(Input.GetKeyUp(KeyCode.S)){
            wait = 0.5f;
        }

        if(Input.GetKey(KeyCode.S)){
            if(wait <=0){
                effector.rotationalOffset = 180f;
                wait = 0.5f;
            }else
            {
                wait -= Time.deltaTime;
            }
        }
    }
}
