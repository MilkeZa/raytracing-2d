using UnityEngine;

/// <summary>
/// This class handles player movement.
/// </summary>
public class Movement : MonoBehaviour
{
    private float _moveSpeed, _rotateSpeed;
    private float _hMovement, _vMovement;
    private Vector3 _tempVect;
    private bool _isRotating;

    /// <summary>
    /// Set player movement speeds.
    /// </summary>
    /// <param name="moveSpeed">How fast the player moves left/right/up/down.</param>
    /// <param name="rotateSpeed">How fast the player rotates clockwise/counter-clockwise.</param>
    public void SetSpeeds(float moveSpeed, float rotateSpeed)
    {
        _moveSpeed = moveSpeed;
        _rotateSpeed = rotateSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Grab WASD input.
        _hMovement = Input.GetAxisRaw("Horizontal");
        _vMovement = Input.GetAxisRaw("Vertical");

        // Determine if rotating.
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            // Q or/and E are pressed, player is rotating.
            _isRotating = true;
        }
        else
        {
            // Player is not rotating.
            _isRotating = false;
        }
    }

    private void FixedUpdate()
    {
        // Create a temporary vector determining where player is to move to.
        _tempVect = new Vector3(_hMovement, _vMovement, 0f);
        _tempVect = _tempVect.normalized * _moveSpeed * Time.deltaTime;
        transform.position += _tempVect;

        // Manage rotational movement.
        if (_isRotating)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                // Rotate counter-clockwise.
                transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
            }
            
            if (Input.GetKey(KeyCode.E))
            {
                // Rotate clockwise.
                transform.Rotate(-Vector3.forward * _rotateSpeed * Time.deltaTime);
            }
        }
    }
}
