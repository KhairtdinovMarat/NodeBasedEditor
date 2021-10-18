using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class Marat_NodePropertyView : Marat_ViewBase
{
    #region Public variables
    public Marat_NodePropertyView propertyView;
    #endregion

    #region Private variables
    #endregion

    #region Protected variables
    #endregion

    #region Constructors
    public Marat_NodePropertyView() : base("Property View") { }
    #endregion

    #region MainMethods

    public override void UpdateView(Rect editorRect, Rect percentageRect, Event e, Marat_NodeGraph currentGraph)
    {
        base.UpdateView(editorRect, percentageRect, e, currentGraph);

        GUILayout.BeginArea(viewRect);
        GUILayout.BeginHorizontal();
        GUILayout.Space(5);
        GUILayout.BeginVertical();
        GUILayout.Space(30);
        if (currentGraph != null)
        {
            if (currentGraph.showNodeProperties)
            {
                currentGraph.selectedNode.DrawNodeProperties();
            }
        }
        GUILayout.Space(5);
        GUILayout.EndVertical();
        GUILayout.Space(5);
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
        ProcessEvents(e);
    }



    public override void ProcessEvents(Event e)
    {
        base.ProcessEvents(e);

        if (viewRect.Contains(e.mousePosition))
        {
            Debug.Log("Inside " + viewTitle);
        }
    }
    #endregion

    #region UtilityMethods
    #endregion 
}
