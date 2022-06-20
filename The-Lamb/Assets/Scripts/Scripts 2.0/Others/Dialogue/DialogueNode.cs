using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Tree.Dialogue
{

    public class DialogueNode: ScriptableObject
    {
        [SerializeField]
        bool isPlayerTalking = false;
        [SerializeField] 
        string text;
        [SerializeField] 
        Rect rect = new Rect (0, 0, 200, 100);
        [SerializeField] 
        List<string> branches = new List<string>();

        public Rect GetRect(){
            return rect;
        }

        public string GetTxt(){
            return text;
        }

        public List<string> GetLeaves(){
            return branches;
        }

        public bool isPlayerTurn()
        {
            return isPlayerTalking;
        }



#if UNITY_EDITOR
        public void SetPos(Vector2 newPos){
            Undo.RecordObject(this, "Move Dialogue Leaf");
            rect.position = newPos;
            EditorUtility.SetDirty(this);
        }

        public void SetTxt(string newTxt){
            if (newTxt != text){
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newTxt;
                EditorUtility.SetDirty(this);
            }
        }

        public void RemoveLeaf(string childID){
            Undo.RecordObject(this, "Remove Dialogue Link");
            branches.Remove(childID);
            EditorUtility.SetDirty(this);
        }

          public void AddLeaf(string childID){
            Undo.RecordObject(this, "Add Dialogue Link");
            branches.Add(childID);
            EditorUtility.SetDirty(this);
        }

            public void SetPlayerSpeaking(bool newIsPlayerTalking)
        {
            Undo.RecordObject(this,"Change Dialogue Speaker");
            isPlayerTalking = newIsPlayerTalking;
            EditorUtility.SetDirty(this);
            
        }


#endif
    }
}

