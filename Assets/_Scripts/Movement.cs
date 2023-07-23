using UnityEngine;

/// <summary>
/// This class manages player movement by turning keyboard input into on-screen movement.
/// </summary>
public class Movement : MonoBehaviour
{
    private float _moveSpeed;   // How fast the player translates.
    private float _rotateSpeed; // How fast the player rotates.
    private float _hMovement, _vMovement; // How fast the player moves vertically (U/D) and horizontally (L/R).
    private bool _isRotating; // Indicates whether the player is actively moving or not.

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

        // Determine if rotating. By checking if the Q or E buttons are down.
        _isRotating = Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E) ? true : false;
    }

    private void FixedUpdate()
    {
        // Create a temporary vector determining where player is to move to. Add it to players current position.
        Vector3 _tempVect = new Vector3(_hMovement, _vMovement, 0f).normalized * _moveSpeed * Time.deltaTime;
        transform.position += _tempVect;

        // Manage rotational movement, if any.
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
