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

        GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("ViewBackGround"));

        GUILayout.BeginArea(viewRect);
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
