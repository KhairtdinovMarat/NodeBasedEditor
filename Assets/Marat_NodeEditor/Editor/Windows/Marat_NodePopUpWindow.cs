using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Marat_NodePopUpWindow : EditorWindow
{
    #region Public variables
    static Marat_NodePopUpWindow currentPopUp;
    string wantedName = "Enter a name";
    #endregion

    #region MainMethod
    public static void InitNodePopUp()
    {
        currentPopUp = GetWindow<Marat_NodePopUpWindow>();
        currentPopUp.titleContent = new GUIContent("Node Popup");
    }

    private void OnGUI()
    {
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);

        GUILayout.BeginVertical();
        
        EditorGUILayout.LabelField("Create new graph", EditorStyles.boldLabel);
        
        wantedName = EditorGUILayout.TextField("EnterName: ", wantedName);

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create graph", GUILayout.Height(40)))
        {
            if (!string.IsNullOrEmpty(wantedName) && wantedName != "Enter a name")
            {
                Marat_NodeUtils.CreateNewGraph(wantedName);
                currentPopUp.Close();
            }
            else
            {
                EditorUtility.DisplayDialog("Node message", "Enter a valid graph name", "Ok");
            }
        }

        if(GUILayout.Button("Cancel", GUILayout.Height(40)))
        {
            currentPopUp.Close();
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        GUILayout.EndVertical();

        GUILayout.Space(20);
        GUILayout.EndHorizontal();
        GUILayout.Space(20);
    }

    #endregion

    #region Utility Methods
    #endregion
}
