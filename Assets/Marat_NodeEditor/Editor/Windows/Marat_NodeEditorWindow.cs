using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public  class Marat_NodeEditorWindow : EditorWindow
{
    #region Variables
    public static Marat_NodeEditorWindow curWindow;

    public Marat_NodePropertyView propertyView;
    public Marat_NodeWorkView workView;

    public Marat_NodeGraph currentGraph = null;

    public float viewPercentage = .75f;
    #endregion

    #region MainMethodsRegion
    public static void InitEditorWindow()
    {
        curWindow = GetWindow<Marat_NodeEditorWindow>();
        curWindow.titleContent = new GUIContent("Node Editor");
        curWindow.maxSize = new Vector2(1920, 1080);
        curWindow.minSize = new Vector2(720, 405);

        CreateViews();
    }

    private void OnEnable()
    {
        //Debug.Log("Enabled window");
    }

    private void OnDestroy()
    {
        //Debug.Log("Disabled window");
    }

    private void Update()
    {        
        //Debug.Log("Updating window");
    }

    private void OnGUI()
    {
        if (propertyView == null || workView == null)
        {
            CreateViews();
            return;
        }

        Event e = Event.current;
        ProcessEvents(e);

        //Update views
        workView.UpdateView(position, new Rect(0f, 0f, viewPercentage, 1f), e, currentGraph);
        propertyView.UpdateView(new Rect(position.width, position.y, position.width, position.height),
                                new Rect(viewPercentage, 0f, 1f - viewPercentage, 1f), e, currentGraph);

        Repaint();
    }

    private void ProcessEvents(Event e)
    {
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.LeftArrow)
        {
            viewPercentage -= .01f;
        }
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.RightArrow)
        {
            viewPercentage += .01f;
        }
    }


    #endregion

    #region Utility Methods

    static void CreateViews()
    {
        if (curWindow!=null)
        {            
            curWindow.propertyView = new Marat_NodePropertyView();
            curWindow.workView = new Marat_NodeWorkView();
        }
        else
        {
            curWindow = GetWindow<Marat_NodeEditorWindow>();
        }
    }
    #endregion
}
