using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class FindAndReplace : EditorWindow
{
    public Object Source;
    private Object _tempSource;
    public Object Outcome;
    private List<GameObject> _originalObjects;
    private bool _chosen;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Replace object")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        FindAndReplace window = (FindAndReplace)EditorWindow.GetWindow(typeof(FindAndReplace));
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Select ONE of the objects you want to replace:");

        _tempSource = Source;
        Source = EditorGUILayout.ObjectField(Source, typeof(Object), true);

        if (_tempSource != Source)
        {
            _chosen = false;
        }

        if (GUILayout.Button("Search!"))
        {
            if (Source == null)
            {
                ShowNotification(new GUIContent("No object selected for searching"));
            }
            else
            {
                var srcName = Source.name;
                _originalObjects = FindGameObjectsWithName(srcName);
                _chosen = true;

                if (_originalObjects.Count < 1)
                {
                    ShowNotification(new GUIContent("No object of this type in scene"));
                    _chosen = false;
                }
            }
        }

        EditorGUILayout.BeginToggleGroup(" ", _chosen);
        EditorGUILayout.LabelField("Select the prefab you want to replace with:");
        Outcome = EditorGUILayout.ObjectField(Outcome, typeof(Object), false);
        if (GUILayout.Button("Replace!"))
        {
            if (Outcome == null || _originalObjects.Count < 1)
            {
                ShowNotification(new GUIContent("No object selected for replacing"));
            }
            else
            {
                ReplaceObjects(_originalObjects, Outcome);
                Source = null;
                Outcome = null;
                _chosen = false;
            }
        }
        EditorGUILayout.EndToggleGroup();
    }

    private static List<GameObject> FindGameObjectsWithName(string name)
    {
        var allGameObjects = FindObjectsOfType<GameObject>();
        var objects = allGameObjects.Where(obj => obj.name.Contains(name)).ToList();
        return objects;
    }

    private void ReplaceObjects(IEnumerable<GameObject> objects, Object outcome)
    {
        foreach (var obj in objects)
        {
            var rot = obj.transform.rotation;
            var pos = obj.transform.position;
            try
            {
                GameObject newObj = Instantiate(outcome, pos, rot) as GameObject;
                Undo.RegisterCreatedObjectUndo(newObj, "Create object");
                newObj.transform.parent = obj.transform.parent;
                Undo.DestroyObjectImmediate(obj);

            }
            catch (ArgumentException)
            {

                ShowNotification(new GUIContent("Chosen object is not a prefab!"));

            }
            catch (NullReferenceException)
            {

                ShowNotification(new GUIContent("Chosen object is not a prefab!"));

            }
        }
    }

}


