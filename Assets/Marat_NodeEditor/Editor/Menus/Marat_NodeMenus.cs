using System.Collections;
using UnityEngine;
using UnityEditor;

public class Marat_NodeMenus : MonoBehaviour
{
    [MenuItem("NodeEditor/LaunchEditor")]
    public static void InitNodeEditor()
    {
        Marat_NodeEditorWindow.InitEditorWindow();
    }
}
