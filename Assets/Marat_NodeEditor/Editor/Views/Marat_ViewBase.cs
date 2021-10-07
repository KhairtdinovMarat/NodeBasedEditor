using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class Marat_ViewBase
{
    #region Public variables
    public string viewTitle;
    public Rect viewRect;
    public Rect borderRect;
    #endregion

    #region Private variables
    #endregion

    #region Protected variables
    protected GUISkin viewSkin;
    protected Marat_NodeGraph currentGraph;
    #endregion

    #region Constructors
    public Marat_ViewBase(string title)
    {
        viewTitle = title;
        GetEditorSkin();
    }
    #endregion

    #region MainMethods
    public virtual void UpdateView(Rect editorRect, Rect percentageRect, Event e, Marat_NodeGraph currentGraph)
    {
        if (viewSkin == null)
        {
            GetEditorSkin();
            return;
        }

        //Set the current view graph
        this.currentGraph = currentGraph;

        if (currentGraph != null)
        {
            viewTitle = currentGraph.graphName;
        }
        else
        {
            viewTitle = "No graph";
        }

        //Update view rectangle
        viewRect = new Rect(editorRect.x*percentageRect.x,
                            editorRect.y*percentageRect.y,
                            editorRect.width*percentageRect.width,
                            editorRect.height*percentageRect.height);
    }

    public virtual void ProcessEvents(Event e) 
    {
        if (viewRect.Contains(e.mousePosition))
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    switch (e.button)
                    {
                        //left click
                        case 0:
                            //Debug.Log("Left click in" + viewTitle); ;
                            break;
                        //right clich
                        case 1:
                            Debug.Log("Right click in" + viewTitle);
                            break;
                    }
                    break;
                case EventType.MouseDrag:
                    Debug.Log("Mouse drag in" + viewTitle);
                    break;
                case EventType.MouseUp:
                    //Debug.Log("Mouse button #" + e.button + " released in " + viewTitle);
                    break;
            }
        }
    }

    #endregion

    #region UtilityMethods
    protected void GetEditorSkin() 
    {
        viewSkin = Resources.Load("GUISkins/EditorSkins/NodeEditorSkin") as GUISkin;
    }
    #endregion
}
