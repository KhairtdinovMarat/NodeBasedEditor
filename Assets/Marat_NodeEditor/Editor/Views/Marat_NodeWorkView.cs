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
    protected int deletedNodeID = 0;
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

        Marat_NodeUtils.DrawGrid(viewRect, 25, 0.075f, Color.white);
        Marat_NodeUtils.DrawGrid(viewRect, 100, 0.15f, Color.white);

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
                            bool overNode = false;                            
                            if (currentGraph!=null)
                            {
                                if (currentGraph.nodes.Count > 0)
                                {
                                    for(int i =0; i < currentGraph.nodes.Count; i++)
                                    {
                                        if (currentGraph.nodes[i].nodeRect.Contains(e.mousePosition))
                                        {
                                            overNode = true;
                                            deletedNodeID = i;
                                            Debug.Log("Over node of type:\t" + currentGraph.nodes[i].GetType().ToString());
                                        }
                                    }
                                }
                            }
                            if (!overNode)
                            {
                                ProcessContextMenu(e, 1);
                            }
                            else
                            {
                                ProcessContextMenu(e, 2);
                            }
                            break;
                    }
                    break;
                case EventType.MouseDrag:
                    MoveCamera(e);
                    break;
                case EventType.MouseUp:
                    //Debug.Log("Mouse button #" + e.button + " released in " + viewTitle);
                    break;
                case EventType.ScrollWheel:
                    Zoom(e);
                    break;
            }
        }
    }

    private void MoveCamera(Event e)
    {
        if(currentGraph!=null && currentGraph.nodes!=null && currentGraph.nodes.Count > 0 && currentGraph.selectedNode == null)
        {
            foreach (Marat_NodeBase node in currentGraph.nodes)
            {
                node.nodeRect.position += e.delta;
            }
        }
    }
    private void Zoom(Event e)
    {
        if (currentGraph != null && currentGraph.nodes != null && currentGraph.nodes.Count > 0)
        {
            foreach (Marat_NodeBase node in currentGraph.nodes)
            {
                node.nodeRect.position += (node.nodeRect.position - e.mousePosition) * e.delta.y * -.01f;
                node.Zoom(e);
            }
        }
    }

    private void ProcessContextMenu(Event e, int menuID)
    {
        GenericMenu menu = new GenericMenu();

        if (menuID == 1) 
        {
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
        }
        if (menuID == 2)
        {
            menu.AddItem(new GUIContent("Delete node"), false, ContextCallback, "5");
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
            case "5":
                Marat_NodeUtils.DeleteNode(deletedNodeID, currentGraph);
                break;
            default:
                break;
        }
    }

    #endregion


}
