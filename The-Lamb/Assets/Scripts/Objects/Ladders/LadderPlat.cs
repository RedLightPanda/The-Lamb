using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderPlat : MonoBehaviour
{
   
    private PlatformEffector2D effector;

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
           effector.rotationalOffset = 0f;
        }

        if(Input.GetKeyDown(KeyCode.S)){
            effector.rotationalOffset = 180;
        }
   }
}
