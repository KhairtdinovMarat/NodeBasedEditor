using System.Collections;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Marat_NodeGraph: ScriptableObject
{
    #region Public methods
    public string graphName = "New Graph";
    public List<Marat_NodeBase> nodes;
    public Marat_NodeBase selectedNode;

    public bool wantsConnection;
    public Marat_NodeBase connectionNode;
    public Rect nodeConnectionPointRect;
    public bool connectionEstablished = false;
    public bool showNodeProperties;
    #endregion

    #region MainMethods
    void OnEnable()
    {
        if (nodes == null)
        {
            nodes = new List<Marat_NodeBase>();
        }        
    }

    public void InitGraph()
    {
        if (nodes.Count > 0)
        {
            for(int i =0; i<nodes.Count; i++)
            {
                nodes[i].InitNode();
            }
        }
    }

    public void UpdateGraph(Event e, Rect viewRect)
    {
        if (nodes.Count > 0)
        {
            ProcessEvents(e, viewRect);
        }
    }

    bool drawing = true;

#if UNITY_EDITOR
    public void UpdateGraphGUI(Event e, Rect viewRect, GUISkin viewSkin)
    {
        if (nodes.Count > 0)
        {
            ProcessEvents(e, viewRect);
            foreach (Marat_NodeBase node in nodes)
            {
                node.UpdateNodeGUI(e, viewRect, viewSkin);
            }
        }

        if (wantsConnection)
        {            
            if (connectionNode != null)
            {
                Vector3 startingVector = new Vector3(connectionNode.nodeRect.x + connectionNode.nodeRect.width + 12f,
                                     connectionNode.nodeRect.y + (connectionNode.nodeRect.height * .5f), 0f);
                Vector3 finishVector = new Vector3(e.mousePosition.x, e.mousePosition.y, 0f);
                
                if(e.button == 1)
                {
                    wantsConnection = false;
                    connectionNode = null;
                }
                if (drawing)
                {
                    DrawLine(startingVector, finishVector);
                }
            }
        }

        if (e.type == EventType.Layout) 
        {
            if (selectedNode != null)
            {
                showNodeProperties = true;
            } 
        }
        EditorUtility.SetDirty(this);
    }

    public void DrawLine(Vector3 startingVector, Vector3 endingVector)  
    {        
        Handles.BeginGUI();
        Handles.DrawBezier(startingVector, endingVector, startingVector + Vector3.right * 50f, endingVector + Vector3.left * 50f, Color.white, null, 5f);
        Handles.EndGUI();        
    }
#endif

    #endregion

    #region Utility methods
    void ProcessEvents(Event e, Rect viewRect)
    {
        if (viewRect.Contains(e.mousePosition))
        {
            if (e.button == 0)
            {
                if(e.type == EventType.MouseDown)
                {
                    DeselectAllNodes();
                    showNodeProperties = false;
                    bool setNode = false;
                    selectedNode = null;
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        if (nodes[i].nodeRect.Contains(e.mousePosition))
                        {
                            nodes[i].isSelected = true; 
                            selectedNode = nodes[i];
                            setNode = true;
                        }
                    }
                    if (!setNode)
                    {
                        DeselectAllNodes();
                    }
                }
            }
        }
    }

    void DeselectAllNodes()
    {
        for(int i = 0; i < nodes.Count; i++) 
        {
            nodes[i].isSelected = false;
        }        
    }
    #endregion
}
