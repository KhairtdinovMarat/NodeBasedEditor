using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class Marat_FloatNode : Marat_NodeBase
{
    #region Public variables
    public float NodeValue;
    public Marat_NodeOutput output;
    #endregion

    #region Constructors
    public Marat_FloatNode()
    {
        output = new Marat_NodeOutput();
    }
    #endregion

    #region Main methods
    public override void InitNode()
    {
        base.InitNode();

        nodeType = NodeType.Float;
        nodeRect = new Rect(10f, 10f, 150f, 65f);
    }

    public override void UpdateNode(Event e, Rect viewRect)
    {
        base.UpdateNode(e, viewRect);
    }

#if UNITY_EDITOR
    public override void UpdateNodeGUI(Event e, Rect viewRect, GUISkin viewSkin)
    {
        base.UpdateNodeGUI(e, viewRect, viewSkin);
        var outputRect = new Rect(nodeRect.x + nodeRect.width - 2f, nodeRect.y + nodeRect.height * .5f - 10f, 20f, 20f);

        DrawNodeOutput(outputRect, viewSkin);
    }
#endif

    #endregion

    #region Utility methods

    #endregion
}
