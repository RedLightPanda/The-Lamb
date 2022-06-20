using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;

namespace Tree.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
       
        Dialogue selectedDialogue = null;
        [NonSerialized]
        GUIStyle NodeMakeup;
         [NonSerialized]
        GUIStyle PlayerLeaf;
        [NonSerialized]
        DialogueNode draggingLeaf = null;
        [NonSerialized]
        Vector2 dragOffset;
        [NonSerialized]
        DialogueNode creatingLeaf = null;
        [NonSerialized]
        DialogueNode killLeaf = null;
        [NonSerialized]
        DialogueNode interlockBranches = null;
        [NonSerialized]
        bool isDragging = false;
        [NonSerialized]
        Vector2 DragTreeOffset;

        Vector2 scrollPos;

        const float canvasSize = 4000;
        const float backgroundSize = 50;  

        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false ,"Dialogue Editor");
        }

        [OnOpenAsset(1)]
        public static bool OpenDialogueAsset(int instanceID, int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
            if(dialogue != null){

                ShowEditorWindow(); 
                return true;
            }
            
            return false;
        }

        private void OnEnable(){
            Selection.selectionChanged += OnSelectionChanged;

            NodeMakeup = new GUIStyle();
            NodeMakeup.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            NodeMakeup.normal.textColor = Color.white;
            NodeMakeup.padding = new RectOffset(20,20,20,20);
            NodeMakeup.border = new RectOffset(12,12,12,12);

            PlayerLeaf = new GUIStyle();
            PlayerLeaf.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            PlayerLeaf.normal.textColor = Color.white;
            PlayerLeaf.padding = new RectOffset(20,20,20,20);
            PlayerLeaf.border = new RectOffset(12,12,12,12);
        }

        private void OnSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;
            if (newDialogue != null)
            {
                selectedDialogue = newDialogue;
                Repaint();
            }
        }

        private void OnGUI()
        {
            if(selectedDialogue == null)
            {
                EditorGUILayout.LabelField("Hello Motto");
            }
            else
            {
                ProcessEvent();

                scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
                
                Rect canvas = GUILayoutUtility.GetRect(canvasSize,canvasSize);
                Texture2D backgroundTxt = Resources.Load("background") as Texture2D;
                Rect texCoords = new Rect(0,0, canvasSize/ backgroundSize , canvasSize/ backgroundSize);
                GUI.DrawTextureWithTexCoords(canvas,backgroundTxt, texCoords);

                foreach(var Leaf in selectedDialogue.GetAllBranches())
                {
                    DrawBranches(Leaf);
                }
                 foreach(var Leaf in selectedDialogue.GetAllBranches())
                {
                    DrawLeaf(Leaf);
                }

                EditorGUILayout.EndScrollView();

                if (creatingLeaf != null)
                {
                    
                    selectedDialogue.CreateBranch(creatingLeaf);
                    creatingLeaf = null;
                }
                if (killLeaf != null){

                    selectedDialogue.DeadLeaf(killLeaf);
                    killLeaf = null;
                }
            }
            
        }


        private void ProcessEvent()
        {
            if (Event.current.type == EventType.MouseDown && draggingLeaf == null)
            {
                draggingLeaf = GetLeafPoint(Event.current.mousePosition + scrollPos);
                if(draggingLeaf != null)
                {
                    dragOffset = draggingLeaf.GetRect().position - Event.current.mousePosition;
                    Selection.activeObject = draggingLeaf;
                }
                else
                {
                    isDragging = true;
                    DragTreeOffset = Event.current.mousePosition + scrollPos;
                    Selection.activeObject = selectedDialogue;
                }
                
            }
            else if(Event.current.type == EventType.MouseDrag && draggingLeaf != null)
            {
                
                draggingLeaf.SetPos(Event.current.mousePosition + dragOffset);

                // Update ScrollPostion
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseDrag && isDragging)
            {
                scrollPos = DragTreeOffset - Event.current.mousePosition;

                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && draggingLeaf != null)
            {
                draggingLeaf = null;
            }
            else if (Event.current.type == EventType.MouseUp && isDragging){
               isDragging = false; 
            }
        }

        private void DrawLeaf(DialogueNode Leaf)
        {
            GUIStyle style = NodeMakeup;
            if(Leaf.isPlayerTurn())
             {
                style = PlayerLeaf;
             }

            GUILayout.BeginArea(Leaf.GetRect(), style);
            Leaf.SetTxt(EditorGUILayout.TextField(Leaf.GetTxt()));

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                creatingLeaf = Leaf;
            }
            Drawlink(Leaf);
            if (GUILayout.Button("-"))
            {
                killLeaf = Leaf;
            }
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

       private void Drawlink(DialogueNode Leaf)
        {
            if (interlockBranches == null)
            {
                if (GUILayout.Button("Root"))
                {
                    interlockBranches = Leaf;
                }
            }
            else if (interlockBranches == Leaf)
                {
                    if(GUILayout.Button("Cancel"))
                    {
                     interlockBranches = null;
                    }
                }
            else if (interlockBranches.GetLeaves().Contains(Leaf.name))
            {
                if (GUILayout.Button("Unravel"))
                {
                    
                    interlockBranches.RemoveLeaf(Leaf.name);
                    interlockBranches = null;
                }
            }
            else
            {
                if (GUILayout.Button("Weave"))
                {
                    
                    interlockBranches.AddLeaf(Leaf.name);
                    interlockBranches = null;
                }
            }
        }

        private void DrawBranches(DialogueNode node)
        {
            Vector3 startPos = new Vector2( node.GetRect().xMax, node.GetRect().center.y);
            foreach(DialogueNode Bud in selectedDialogue.GetAllLeaves(node))
            {
                Vector3 endPos = new Vector2 (Bud.GetRect().xMin, Bud.GetRect().center.y);
                Vector3 branchOffset = endPos - startPos;
                branchOffset.y = 0;
                branchOffset.x *= 0.8f;
                Handles.DrawBezier(startPos, endPos, startPos+branchOffset,endPos-branchOffset, Color.white, null, 4f);
            }
        }

        private DialogueNode GetLeafPoint(Vector2 point)
        {
            DialogueNode foundLeaf = null;
            foreach (DialogueNode node in selectedDialogue.GetAllBranches()){
                if (node.GetRect().Contains(point)){
                    foundLeaf = node;
                }
            }
            return foundLeaf;
        }
    }
}
