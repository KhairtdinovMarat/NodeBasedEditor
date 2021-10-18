using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class Marat_NodeUtils
{
    private static string graphPath;

    public static void CreateNewGraph(string wantedName)
    {
        graphPath = Application.dataPath + "/Marat_NodeEditor/DataBase/";
#if UNITY_EDITOR
        graphPath = graphPath.Substring(graphPath.IndexOf("Assets/"));
#endif
        Debug.Log("App data path:\t"+graphPath);
        Marat_NodeGraph currentGraph = ScriptableObject.CreateInstance<Marat_NodeGraph>();
        if (currentGraph != null)
        {
            currentGraph.graphName = wantedName;
            currentGraph.InitGraph();
            
            AssetDatabase.CreateAsset(currentGraph, graphPath +wantedName + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Marat_NodeEditorWindow currentWindow = EditorWindow.GetWindow<Marat_NodeEditorWindow>();
            if (currentWindow.workView != null)
            {
                currentWindow.currentGraph = currentGraph;
            }
        }
        else
        {
            EditorUtility.DisplayDialog("Node message", "Unable to create node graph, please refer to the administrator", "Ok");
        }
    }

    public static void LoadGraph()
    {
        Marat_NodeGraph currentGraph;
        graphPath = EditorUtility.OpenFilePanel("Load graph", graphPath, "");
        graphPath = graphPath.Substring(graphPath.IndexOf("Assets/"));

        currentGraph = AssetDatabase.LoadAssetAtPath<Marat_NodeGraph>(graphPath);

        if (currentGraph != null)
        {
            Marat_NodeEditorWindow currentWindow = EditorWindow.GetWindow<Marat_NodeEditorWindow>();
            if (currentWindow.workView != null)
            {
                currentWindow.currentGraph = currentGraph;
            }
        }
        else
        {
            EditorUtility.DisplayDialog("Node message", "Unable to load the current graph.", "Ok");
        }
    }

    public static void UnloadGraph()
    {
        Marat_NodeEditorWindow currentWindow = EditorWindow.GetWindow<Marat_NodeEditorWindow>();
        if (currentWindow.workView != null)
        {
            currentWindow.currentGraph = null;
        }
    }

    public static void CreateNode(Marat_NodeGraph currentGraph, NodeType type, Vector2 mousePosition)
    {
        if (currentGraph!=null)
        {
            Marat_NodeBase currentNode = null;
            switch (type)
            {
                case NodeType.Add:
                    currentNode = ScriptableObject.CreateInstance<Marat_AddNode>();
                    currentNode.nodeName = "Add";
                    break;
                case NodeType.Float:
                    currentNode = ScriptableObject.CreateInstance<Marat_FloatNode>();
                    currentNode.nodeName = "Float";
                    break;
                default:
                    break;
            }
            if (currentNode!=null)
            {
                currentNode.InitNode();
                currentNode.nodeRect.position = mousePosition;
                currentNode.parentGraph = currentGraph;
                currentGraph.nodes.Add(currentNode);

                AssetDatabase.AddObjectToAsset(currentNode, currentGraph);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }

    public static void DrawGrid(Rect viewRect, float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(viewRect.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(viewRect.height/gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);
        for (int x = 0; x < widthDivs; x++)
        {
            Handles.DrawLine(new Vector3(gridSpacing*x, 0f,0f), new Vector3(gridSpacing*x, viewRect.height, 0f));
        }
        for (int y = 0; y < heightDivs; y++)
        {
            Handles.DrawLine(new Vector3(0f, gridSpacing*y, 0f), new Vector3(viewRect.width, gridSpacing * y, 0f));
        }
        //Handles.color = Color.white;
        Handles.EndGUI();
    }

    public static void DeleteNode(int nodeID, Marat_NodeGraph currentGraph)
    {
        if (currentGraph != null)
        {
            if (currentGraph.nodes.Count >= nodeID)
            {
                Marat_NodeBase deletedNode = currentGraph.nodes[nodeID];
                if (deletedNode != null)
                {
                    currentGraph.nodes.RemoveAt(nodeID);
                    ScriptableObject.DestroyImmediate(deletedNode, true);
                    AssetDatabase.Refresh();
                }
            }
        }
    }
}