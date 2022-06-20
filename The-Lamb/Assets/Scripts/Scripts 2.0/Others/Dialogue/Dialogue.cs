using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Tree.Dialogue
{
[CreateAssetMenu(fileName = "New Dialogue Tree", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver 
    { 

        [SerializeField]
        List<DialogueNode> Leaves = new List<DialogueNode>();
        [SerializeField]
        Vector2 newNodeOffset = new Vector2 (250, 0);

        Dictionary<string, DialogueNode> leafLookup = new Dictionary<string, DialogueNode>();


        private void OnValidate(){
            leafLookup.Clear();
            foreach (DialogueNode node in GetAllBranches()){
                leafLookup[node.name]=node;
            }
        }
        public IEnumerable<DialogueNode> GetAllBranches(){
            return Leaves;
        }

         public DialogueNode GetRootLeaf()
        {
        return Leaves[0];
        }

        public IEnumerable<DialogueNode> GetAllLeaves(DialogueNode Leaf)
        {
            foreach (string childID in Leaf.GetLeaves())
            {
                if(leafLookup.ContainsKey(childID))
                {
                    yield return leafLookup[childID];
                }     
            }
        }

           public IEnumerable<DialogueNode> GetAllPlayerLeaves(DialogueNode currentNode)
        {
            foreach (DialogueNode node in GetAllLeaves(currentNode))
            {
                if(node.isPlayerTurn())
                {
                    yield return node;
                }
            }
        }

        public IEnumerable<DialogueNode> GetAllNPCLeaves(DialogueNode currentNode)
        {
            foreach (DialogueNode node in GetAllLeaves(currentNode))
            {
                if(!node.isPlayerTurn())
                {
                    yield return node;
                }
            }
        }

#if UNITY_EDITOR
        public void CreateBranch(DialogueNode trunk)
        {
            DialogueNode newLeaf = SproutLeaf(trunk);
            Undo.RegisterCreatedObjectUndo(newLeaf, "Created Dialogue Node");
            Undo.RecordObject(this, "Added Dialogue");
            AddBuds(newLeaf);
           
        }

        public void DeadLeaf(DialogueNode nodeToDelete)
        {
            Undo.RecordObject(this,"Removed Dialogue Node");
            Leaves.Remove(nodeToDelete);
            OnValidate();
            RakeDeadLeaves(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);          
        }

                private DialogueNode SproutLeaf(DialogueNode trunk)
        {
            DialogueNode newLeaf = CreateInstance<DialogueNode>();
            newLeaf.name = Guid.NewGuid().ToString();
            if (trunk != null)
            {
                trunk.AddLeaf(newLeaf.name);    
                newLeaf.SetPlayerSpeaking(!trunk.isPlayerTurn());
                newLeaf.SetPos(trunk.GetRect().position + newNodeOffset); 
            }

            return newLeaf;
        }
        
        private void AddBuds (DialogueNode newLeaf){
            Leaves.Add(newLeaf);
            OnValidate();
        }

        private void RakeDeadLeaves(DialogueNode nodeToDelete)
        {
            foreach(DialogueNode node in GetAllBranches())
            {
                node.RemoveLeaf(nodeToDelete.name);
            }
        }
#endif

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
              if(Leaves.Count == 0)
            {               
               DialogueNode newLeaf = SproutLeaf(null);
               AddBuds(newLeaf);
            }
            
            if (AssetDatabase.GetAssetPath(this) != "")
            {
                foreach(DialogueNode node in GetAllBranches())
                {
                    if (AssetDatabase.GetAssetPath(node) == ""){
                        AssetDatabase.AddObjectToAsset(node, this);
                    }
                }
            }
#endif
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}

