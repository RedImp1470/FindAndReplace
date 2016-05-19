using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class FindAndReplace : EditorWindow
{
    private List<GameObject> _originalObjects;
    private bool _chosen;
    private FindAndReplaceHelper _findAndReplaceHelper;
    public Object Source;
    private Object _tempSource;
    public Object Outcome;
    private ThingToReplace _tempThingToReplace;

    public enum ThingToReplace
    {
        Object,
        Material
    }

    private ThingToReplace _thing;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Find and Replace")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        FindAndReplace window = (FindAndReplace)GetWindow(typeof(FindAndReplace));
        window.Show();
    }

    void OnGUI()
    {
        _findAndReplaceHelper = CreateInstance<FindAndReplaceHelper>();
        _tempThingToReplace = _thing;
        _thing = (ThingToReplace)EditorGUILayout.EnumPopup("Replace: ", _thing);

        WantToReplace(_thing);
    }


    private void WantToReplace(ThingToReplace thing)
    {
        EditorGUILayout.LabelField("Select what you want to replace:");

        _tempSource = Source;
        
        SelectThingToReplace(thing);

        if (_tempSource != Source)
        {
            _chosen = false;
        }

        if (GUILayout.Button("Search!"))
        {
            if (Source == null)
            {
                ShowNotification(new GUIContent("Nothing selected for searching"));
            }
            else
            {
                Search(thing);

                _chosen = true;

                if (_originalObjects.Count < 1)
                {
                    ShowNotification(new GUIContent("Nothing of this type in scene"));
                    _chosen = false;
                }
            }
        }

        EditorGUILayout.BeginToggleGroup(" ", _chosen);
        EditorGUILayout.LabelField("Select what you want to replace with:");

        SelectThingToReplaceWith(thing);

        if (GUILayout.Button("Replace!"))
        {
            if (Outcome == null || _originalObjects.Count < 1)
            {
                ShowNotification(new GUIContent("No object selected for replacing"));
            }
            else
            {
                Replace(thing);
                Source = null;
                Outcome = null;
                _chosen = false;
            }
        }
        EditorGUILayout.EndToggleGroup();
    }

    private void SelectThingToReplace(ThingToReplace thing)
    {
        if (_tempThingToReplace != thing)
            Source = null;
        switch (thing)
        {
            case ThingToReplace.Object:
                Source = EditorGUILayout.ObjectField(Source, typeof(GameObject), true);
                break;

            case ThingToReplace.Material:
                Source = EditorGUILayout.ObjectField(Source, typeof(Material), true);
                break;
        }
        
    }

    private void SelectThingToReplaceWith(ThingToReplace thing)
    {
        if (_tempThingToReplace != thing)
            Outcome = null;
        switch (thing)
        {
            case ThingToReplace.Object:
                Outcome = EditorGUILayout.ObjectField(Outcome, typeof(GameObject), false);
                break;
            case ThingToReplace.Material:
                Outcome = EditorGUILayout.ObjectField(Outcome, typeof(Material), false);
                break;
        }
    }

    private void Search(ThingToReplace thing)
    {
        switch (thing)
        {
            case ThingToReplace.Object:
                var srcName = Source.name;
                _originalObjects = _findAndReplaceHelper.FindGameObjectsWith(srcName);
                break;
            case ThingToReplace.Material:
                _originalObjects = _findAndReplaceHelper.FindGameObjectsWith(Source as Material);
                break;
        }
    }

    private void Replace(ThingToReplace thing)
    {
        switch (thing)
        {
            case ThingToReplace.Object:
                _findAndReplaceHelper.ReplaceAThing(_originalObjects, Outcome);
                break;
            case ThingToReplace.Material:
                _findAndReplaceHelper.ReplaceAThing(_originalObjects, Outcome as Material);
                break;
        }
    }
}


