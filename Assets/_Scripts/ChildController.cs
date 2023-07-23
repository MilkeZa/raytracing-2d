using UnityEngine;

/// <summary>
/// This class handles the creation and retrieval of child gameobjects used to visualize individual rays.
/// </summary>
public class ChildController : MonoBehaviour
{
    /* OPTIMIZATION : Make the _childObjects array public with a private set as shown below:
     * public GameObject[] childObjects { get; private set; } 
     * 
     * This would avoid the need for a dedicated getter, decreasing lines of code required to do the same thing.
     */
    private GameObject[] _childObjects;

    /// <summary>
    /// Create an array of gameobjects and assign them to be a child of the
    /// parents transform.
    /// </summary>
    /// <param name="childCount">How many child objects are to be created.</param>
    public void CreateChildren(int childCount)
    {
        _childObjects = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            // Create an empty object, and give it a name.
            GameObject childObject = new GameObject();
            childObject.name = $"_lr{i}";

            // Set the childs parent and put it in the array of children.
            childObject.transform.parent = gameObject.transform;
            _childObjects[i] = childObject;
        }
    }

    /// <summary>
    /// Get the current array of child objects.
    /// </summary>
    /// <returns>An array of GameObjects representing the child objects.</returns>
    public GameObject[] GetChildObjects()
    {
        return _childObjects;
    }
}
