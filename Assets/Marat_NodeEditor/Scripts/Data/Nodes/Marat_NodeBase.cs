using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;
#endif
using System;
using System.Collections;

[Serializable]
public class Marat_NodeBase : ScriptableObject
{
    #region Public variables
    public string nodeName;
    public Rect nodeRect;
    public Marat_NodeGraph parentGraph;
    public NodeType nodeType;
    public bool isSelected = false;
    #endregion

    #region Protected variables
    protected GUISkin nodeSkin;
    #endregion

    #region Subclasses
    [Serializable]
    public class Marat_NodeInput
    {
        public bool isOccupied = false;
        public Marat_NodeBase inputNode;
        public Marat_NodeInput()
        {
        }
    }

    [Serializable]
    public class Marat_NodeOutput
    {
        public bool isOccupied = false;
        public Marat_NodeBase node;
        public Marat_NodeOutput()
        {
        }
    }
    #endregion

    #region Main methods
    public virtual void InitNode()
    {
    }

    

    public virtual void UpdateNode(Event e, Rect viewRect)
    {
        ProcessEvents(e, viewRect);
    }
#if UNITY_EDITOR


    public virtual void UpdateNodeGUI(Event e, Rect viewRect, GUISkin viewSkin)
    {
        ProcessEvents(e, viewRect);

        if (!isSelected)
        {
            GUI.Box(nodeRect, nodeName, viewSkin.GetStyle("NodeDefault"));
        }        
        else
        {
            GUI.Box(nodeRect, nodeName, viewSkin.GetStyle("NodeSelected"));
        }


        EditorUtility.SetDirty(this);
    }
#endif
#endregion

    #region Utility methods
    void ProcessEvents(Event e, Rect viewRect)
    {
        if (isSelected)
        {
            if (viewRect.Contains(e.mousePosition))
            {
                if (e.type == EventType.MouseDrag)
                {
                    nodeRect.position += e.delta;
                }
            }
        }
    }

    public void DrawNodeInput(Marat_NodeInput nodeInput, Rect rect, GUISkin viewSkin)
    {
        if (GUI.Button(rect, "", viewSkin.GetStyle("NodeInputDefault")))
        {
            var wantsConnection = parentGraph.wantsConnection;
            if (wantsConnection)
            {
                nodeInput.inputNode = parentGraph.connectionNode;
                nodeInput.isOccupied = nodeInput.inputNode != null ? true : false;                

                parentGraph.wantsConnection = false;
                parentGraph.connectionNode = null;
            }
        }
        if (nodeInput.isOccupied)
        {
            DrawLine(nodeInput.inputNode, rect);
        }
    }

    public void DrawNodeOutput(Rect rect, GUISkin viewSkin)
    {
        if (GUI.Button(rect, "", viewSkin.GetStyle("NodeOutputDefault")))
        {
            parentGraph.wantsConnection = true;
            parentGraph.connectionNode = this;
        }
    }

    public void DrawLine(Marat_NodeBase inputNode, Rect rect)
    {
        Vector3 start_vector = new Vector3(inputNode.nodeRect.x + inputNode.nodeRect.width + 15f, inputNode.nodeRect.y + inputNode.nodeRect.height / 2, 0f);
        Vector3 finish_vector = new Vector3(rect.x, rect.y + (rect.height / 2f), 0);
        parentGraph.DrawLine(start_vector, finish_vector);        
    }
    #endregion
}
