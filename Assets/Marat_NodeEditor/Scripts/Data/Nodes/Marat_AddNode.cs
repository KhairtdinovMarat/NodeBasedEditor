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
            Rect inputARect = new Rect(nodeRect.x - 15, nodeRect.y + 7, 15f, 20f);
            Rect inputBRect = new Rect(nodeRect.x - 15, nodeRect.y + nodeRect.height - 27, 15f, 20f);
            Rect outputRect = new Rect(nodeRect.x + nodeRect.width, nodeRect.y + nodeRect.height * .5f - 10f, 15f, 20f);

            DrawNodeInput(nodeInputA, inputARect, viewSkin);            
            DrawNodeInput(nodeInputB, inputBRect, viewSkin);
            DrawNodeOutput(outputRect, viewSkin);
            
            //Debug.Log("Node input A input node:\t"+nodeInputA.inputNode.GetType().ToString());
        }
    }

#endif

    #endregion

    #region Utility methods
    #endregion
}
