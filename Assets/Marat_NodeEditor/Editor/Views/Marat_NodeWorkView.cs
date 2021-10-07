using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Marat_NodeWorkView : Marat_ViewBase
{
    #region Public variables
    public Marat_NodeWorkView workView;
    
    #endregion

    #region Private variables
    #endregion

    #region Protected variables
    #endregion

    #region Private variables
    private Vector2 mousePos;
    #endregion

    #region Constructors
    public Marat_NodeWorkView() : base("Work View") { }
    #endregion

    #region MainMethods

    public override void UpdateView(Rect editorRect, Rect percentageRect, Event e, Marat_NodeGraph currentGraph)
    {
        base.UpdateView(editorRect, percentageRect, e, currentGraph);

        GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("ViewBackGround"));

        GUILayout.BeginArea(viewRect);
        if (currentGraph != null)
        {
            currentGraph.UpdateGraphGUI(e, viewRect, viewSkin);
        }
        GUILayout.EndArea();

        ProcessEvents(e);
    }



    #endregion

    #region UtilityMethods

    public override void ProcessEvents(Event e)
    {
        base.ProcessEvents(e);
        if (viewRect.Contains(e.mousePosition))
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    switch (e.button)
                    {
                        //left click
                        case 0:

                            break;
                        //right clich
                        case 1:
                            mousePos = e.mousePosition;
                            ProcessContextMenu(e);
                            break;
                    }
                    break;
                case EventType.MouseDrag:
                    //Debug.Log("Mouse drag in" + viewTitle);
                    break;
                case EventType.MouseUp:
                    //Debug.Log("Mouse button #" + e.button + " released in " + viewTitle);
                    break;
            }
        }
    }

    private void ProcessContextMenu(Event e)
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("Create Graph"), false, ContextCallback, "0");
        menu.AddItem(new GUIContent("Load graph"), false, ContextCallback, "1");
        
        if (currentGraph != null)
        {
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Unload graph"), false, ContextCallback, "2");

            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Add node/Float"), false, ContextCallback, "3");
            menu.AddItem(new GUIContent("Add node/Add"), false, ContextCallback, "4");
        }

        menu.ShowAsContext();
        e.Use();
    }

    void ContextCallback(object obj)
    {
        switch(obj.ToString())
        {
            case "0":
                Marat_NodePopUpWindow.InitNodePopUp();
                break;
            case "1":
                Marat_NodeUtils.LoadGraph();
                break;
            case "2":
                Marat_NodeUtils.UnloadGraph();
                break;
            case "3":
                Marat_NodeUtils.CreateNode(currentGraph, NodeType.Float, mousePos);
                break;
            case "4":
                Marat_NodeUtils.CreateNode(currentGraph, NodeType.Add, mousePos);
                break;
            default:
                break;
        }
    }

    #endregion


}
