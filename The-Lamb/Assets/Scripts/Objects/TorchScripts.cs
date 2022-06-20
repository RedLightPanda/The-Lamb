using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keys", menuName = "Puzzles/Lights", order = 1)]
public class TorchScripts : ScriptableObject
{
      [SerializeField]
    private bool Lit;

    [SerializeField]
    private GameObject Candel,Door;
    
    [SerializeField]
    public int Fire;

    [SerializeField]
    private int Unlock;

    // Start is called before the first frame update
    void Start()
    {
        Lit=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void TrueofFalse(){
        if(Lit==false){
            Fire++;
            Lit=true;
        }
        else if (Lit==true)
        {
            return;
        }
    }

    public void Unlocked(){
        if(Fire == Unlock){
            Door.SetActive(false);
        }
    }
}
