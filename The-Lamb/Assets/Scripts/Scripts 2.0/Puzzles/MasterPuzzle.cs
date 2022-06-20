using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterPuzzle : MonoBehaviour
{
    [SerializeField]private PuzzleSO puzzlekey;
    [SerializeField]private GameObject Door;
    [SerializeField]private GameObject[] PuzzlePeace;
    public bool peaceActive;
    [SerializeField]private int peaceAct;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void Unlocked(){
        if(peaceAct == puzzlekey.Unlocked){
            Door.SetActive(false);
        }
        else if(peaceAct >= puzzlekey.Unlocked)
        {
            Door.SetActive(true);
        }
    }
}
