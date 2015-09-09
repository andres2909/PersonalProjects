using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    #region Global Variables
    
    // Private variables                          
    private float range = 7.0f;                             // Movement range of the camera
    private Vector3 velocityCamSmooth = Vector3.zero;       // Smoothing of the camera
    private float camSmoothDampTime = 0.3f;                 // Damping of the camera
    private Transform player;                               // Storess the transform of the player each frame

    #endregion

    #region Unity Functions

    /// <summary>
	/// Update is called once per frame.
	/// </summary>
	void Update()
    {
        // Get the player's position each frame
        player = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate()
    {
        smoothPosition(transform.position, player.position + new Vector3(0, 35, -10));
        transform.rotation = Quaternion.Euler(50, Mathf.Clamp(-(transform.position.x * 1f), -20, 20), -(transform.position.x / 2));
    }

    #endregion

    #region Other Functions

    /// <summary>
    /// Moves the camera from point A to B with a delay
    /// </summary>
    /// <param name="fromPos">Point A</param>
    /// <param name="toPos">Poinnt B</param>
    private void smoothPosition(Vector3 fromPos, Vector3 toPos)
    {
        // Making a smooth transition between camera's current position and the position it wants to be in
        this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);

        // Get the camera's position to a vector variable
        Vector3 currentPosition = transform.position;
        // Modify the variable to keep the X position within the range
        currentPosition.x = Mathf.Clamp(currentPosition.x, -range, range);
        // Set the transform position to our modified vector
        transform.position = currentPosition;
    }

    #endregion

}
