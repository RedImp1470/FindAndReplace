using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using Object = UnityEngine.Object;

public class FindAndReplaceHelper : EditorWindow
{
    public void ReplaceAThing(IEnumerable<GameObject> objects, Object outcome)
    {
        foreach (var obj in objects)
        {
            var rot = obj.transform.rotation;
            var pos = obj.transform.position;
            try
            {
                var newObj = Instantiate(outcome, pos, rot) as GameObject;
                Undo.RegisterCreatedObjectUndo(newObj, "Create object");
                newObj.transform.parent = obj.transform.parent;
                newObj.name = outcome.name;
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

    public void ReplaceAThing(IEnumerable<GameObject> objects, Material outcome)
    {
        foreach (var obj in objects)
        {
            Undo.RegisterCompleteObjectUndo(obj, "change material");
            obj.GetComponent<Renderer>().sharedMaterial = outcome;
        }
    }

    public List<GameObject> FindGameObjectsWith(string objName)
    {
        var allGameObjects = FindObjectsOfType<GameObject>();
        var objects = allGameObjects.Where(obj => obj.name.Contains(objName)).ToList();
        return objects;
    }

    public List<GameObject> FindGameObjectsWith(Material mat)
    {
        var allGameObjects = FindObjectsOfType<GameObject>();
        var objects = new List<GameObject>();
        foreach (var obj in allGameObjects)
        {
            if (obj.GetComponent<Renderer>() == null) continue;
            var objMat = obj.GetComponent<Renderer>().sharedMaterial;

            if (objMat.name.Equals(mat.name))
            {
                objects.Add(obj);
            }
        }
        return objects;
    }

}
