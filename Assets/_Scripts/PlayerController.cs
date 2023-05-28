using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Movement settings
    [Header("Movement")]
    [SerializeField][Range(1f, 10f)] private float _moveSpeed = 10f;
    [SerializeField][Range(1f, 50f)] private float _rotateSpeed = 25f;

    // Raycaster settings
    [Header("Raycast Physics")]
    [SerializeField][Range(20f, 180f)] private float _fieldOfView = 90f;
    private enum RayCount
    {
        One, Three, Five, Seven, Nine,
        Eleven, Thirteen, Fifteen, Seventeen, Nineteen
    };
    [SerializeField] private RayCount _rayCountChoice = RayCount.One;
    private int _rayCount;

    // Ray Visualizer settings
    [Header("Raycast Visuals")]
    [SerializeField] private Material _rayMaterial;
    private enum RayColor { Black, White, Grey, Red, Green, Blue, Yellow };
    [SerializeField] private RayColor _rayColorChoice;
    private Color _rayColor = Color.white;
    [SerializeField][Range(0.01f, 0.1f)] private float _rayWidth = 0.05f;

    // Script controllers
    private Movement _movementController;
    private Raycaster _raycaster;
    private RayVisualizer _rayVisualizer;
    private ChildController _childController;

    private void Awake()
    {
        // Grab all of the script controllers from the gameobject.
        _movementController = GetComponent<Movement>();
        _childController = GetComponent<ChildController>();
        _raycaster = GetComponent<Raycaster>();
        _rayVisualizer = GetComponent<RayVisualizer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set movement speeds.
        _movementController.SetSpeeds(_moveSpeed, _rotateSpeed);

        // Set ray count and pass along to raycaster.
        SetRayCount();
        _raycaster.SetVariables(_rayCount, _fieldOfView);

        // Set ray color, then create child objects and configure their line renderers.
        SetRayColor();
        SetChildObjects(_rayCount, _rayWidth, _rayColor, _rayMaterial);
    }

    // Update is called once per frame
    void Update()
    {
        CastRays();
    }

    private void SetRayCount()
    {
        switch (_rayCountChoice)
        {
            case RayCount.One: _rayCount = 1; break;
            case RayCount.Three: _rayCount = 3; break;
            case RayCount.Five: _rayCount = 5; break;
            case RayCount.Seven: _rayCount = 7; break;
            case RayCount.Nine: _rayCount = 9; break;
            case RayCount.Eleven: _rayCount = 11; break;
            case RayCount.Thirteen: _rayCount = 13; break;
            case RayCount.Fifteen: _rayCount = 15; break;
            case RayCount.Seventeen: _rayCount = 17; break;
            case RayCount.Nineteen: _rayCount = 19; break;
        }
    }

    private void SetRayColor()
    {
        switch (_rayColorChoice)
        {
            case RayColor.Black: _rayColor = Color.black; break;
            case RayColor.White: _rayColor = Color.white; break;
            case RayColor.Grey: _rayColor = Color.grey; break;
            case RayColor.Red: _rayColor = Color.red; break;
            case RayColor.Blue: _rayColor = Color.blue; break;
            case RayColor.Green: _rayColor = Color.green; break;
            case RayColor.Yellow: _rayColor = Color.yellow; break;
        }
    }

    private void SetChildObjects(int rayCount, float rayWidth, Color rayColor, Material rayMaterial)
    {
        // Create the child objects, one child per ray.
        _childController.CreateChildren(rayCount);

        // Grab the child objects and add one line renderer component per child.
        GameObject[] childObjects = _childController.GetChildObjects();
        //_rayVisualizer.SetLineRenderers(_childController.GetChildObjects());
        _rayVisualizer.SetLineRenderers(childObjects);

        // Lastly, format the line renderers.
        _rayVisualizer.FormatLineRenderers(rayWidth, rayColor, rayMaterial);
    }

    private void CastRays()
    {
        // Get hitpoints from raycaster.
        List<Vector3[]> hitPoints = _raycaster.CalculateHitPoints();

        // Pass hitpoints along to ray visualizer to draw to screen.
        _rayVisualizer.DrawRays(hitPoints);
    }
}
