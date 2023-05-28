using UnityEngine;

/// <summary>
/// This class handles the creation and retrieval of child gameobjects.
/// </summary>
public class ChildController : MonoBehaviour
{
    private GameObject[] childObjects;

    /// <summary>
    /// Create an array of gameobjects and assign them to be a child of the
    /// parents transform.
    /// </summary>
    /// <param name="childCount">How many child objects are to be created.</param>
    public void CreateChildren(int childCount)
    {
        childObjects = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            // Create an empty object, and give it a name.
            GameObject childObject = new GameObject();
            childObject.name = $"_lr{i}";

            // Set the childs parent and put it in the array of children.
            childObject.transform.parent = gameObject.transform;
            childObjects[i] = childObject;
        }
    }

    public GameObject[] GetChildObjects()
    {
        return childObjects;
    }
}
