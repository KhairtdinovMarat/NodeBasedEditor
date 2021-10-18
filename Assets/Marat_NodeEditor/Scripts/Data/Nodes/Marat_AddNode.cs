using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Marat_AddNode : Marat_NodeBase
{
    #region Public variables
    public float NodeValue;
    public Marat_NodeOutput output;

    public Marat_NodeInput nodeInputA, nodeInputB;
    public Marat_NodeOutput nodeOutput;

   
    #endregion

    #region Constructors
    public Marat_AddNode()
    {
        output = new Marat_NodeOutput();
    }
    #endregion

    #region Main methods
    public override void InitNode()
    {
        base.InitNode();

        nodeType = NodeType.Add;
        nodeRect = new Rect(10f, 10f, 200f, 65f);
    }

    public override void UpdateNode(Event e, Rect viewRect)
    {
        base.UpdateNode(e, viewRect);
    }

#if UNITY_EDITOR
    public override void UpdateNodeGUI(Event e, Rect viewRect, GUISkin viewSkin)
    {
        base.UpdateNodeGUI(e, viewRect, viewSkin);

        if (parentGraph != null)
        {            
            Rect inputARect = new Rect(nodeRect.x - 15 * scale, nodeRect.y + 7 * scale, 15f * scale, 20f * scale);
            Rect inputBRect = new Rect(nodeRect.x - 15 * scale, nodeRect.y + nodeRect.height - 27 * scale, 15f * scale, 20f * scale);
            Rect outputRect = new Rect(nodeRect.x + nodeRect.width, nodeRect.y + nodeRect.height * .5f - 10f * scale, 15f * scale, 20f * scale);

            DrawNodeInput(nodeInputA, inputARect, viewSkin);            
            DrawNodeInput(nodeInputB, inputBRect, viewSkin);
            DrawNodeOutput(outputRect, viewSkin);
            
            ProcessData();
        }
    }

    public override void ProcessData()
    {
        base.ProcessData();

        if (nodeInputA.isOccupied && nodeInputB.isOccupied)
        {
            Marat_NodeBase nodeA = nodeInputA.inputNode;
            Marat_NodeBase nodeB = nodeInputB.inputNode;

            nodeValue = nodeA.nodeValue + nodeB.nodeValue;
        }
    }

    public override void DrawNodeProperties()
    {
        base.DrawNodeProperties();
        EditorGUILayout.FloatField("Sum: ", nodeValue);
    }

#endif

    #endregion

    #region Utility methods
    #endregion
}
