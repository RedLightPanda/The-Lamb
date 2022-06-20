using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tree.Dialogue
{
    public class PlayersConversant : MonoBehaviour
    {
        Dialogue currentScript;
        DialogueNode currentNode = null;
        bool isChoosing = false;

        public event Action onConverstaitonUpdate;

        public void StartDialoge (Dialogue newLines)
        {
            currentScript = newLines;
            currentNode = currentScript.GetRootLeaf();
            onConverstaitonUpdate();
        }

        public void Quit()
        {
            currentScript = null;
            currentNode = null;
            isChoosing = false;
            onConverstaitonUpdate();
        }

        public bool IsActive()
        {
            return currentScript != null;
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }

        public string GetText()
        {
            if(currentNode == null)
            {
                return"";
            }

            return currentNode.GetTxt();
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return currentScript.GetAllLeaves(currentNode);
        }

        public void SelectChoice(DialogueNode chosenLine)
        {
            currentNode = chosenLine;
            isChoosing = false;
            Next();
        }
            
        public void Next()
        {
            int PlayersLines = currentScript.GetAllPlayerLeaves(currentNode).Count();
            if(PlayersLines > 0)
            {
                isChoosing = true;
                onConverstaitonUpdate();
                return;
            }

            DialogueNode[] leaves = currentScript.GetAllNPCLeaves(currentNode).ToArray();
            int randomDex = UnityEngine.Random.Range(0,leaves.Count());
            currentNode = leaves[randomDex];
            onConverstaitonUpdate();
        }

        public bool HasNext()
        {
            return currentScript.GetAllLeaves(currentNode).Count() > 0;
        }
    }
}