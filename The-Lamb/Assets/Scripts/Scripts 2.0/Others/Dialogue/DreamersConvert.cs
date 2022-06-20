using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tree.Dialogue
{

    public class DreamersConvert : MonoBehaviour
    {
        [SerializeField] Dialogue script = null;
        [SerializeField] bool isPlayer = false;


         void Update()
        {
            PlayerTalking();
        }
        
#region Triggers
        void OnTriggerEnter2D(Collider2D other) 
        {
          if(other.tag == "Player") 
          {
            isPlayer = true;
            Debug.Log("Hello Player");
          }  
        }

        void OnTriggerExit2D(Collider2D other) 
        {
            if(other.tag == "Player")
            {
                isPlayer = false;
                Debug.Log("Goodbye Player");
            }
            
        }
#endregion

        void PlayerTalking()
        {
            if(script == null)
            {
                return;
            }

            if (Input.GetButtonDown("Interactive") && isPlayer)
            {
                Debug.Log("We got it Tim.");
                GetComponent<PlayersConversant>().StartDialoge(script);
            }
            return;
        }
    }
}
