using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{
    #region Global Variables

    // Public Static Variables
    public static float laserCD;                // Time to re-use the laser
    public static float laserImgCD;             // Time for the Image to load

    // Public Variables
    public GameObject laserPrefab;              // Laser Prefab
    public Image laserCooldownImg;              // Cooldown bar image

    // Private variables
    private float range = 11.5f;                // Range of the paddle
    private float speed = 50f;                  // Speed of the paddle
    private bool laserReady = true;             // Is laser ready to use?
    //private float laserCooldownImgWidth;        // Width of the Cooldown bar image

    private AudioSource laserSound;
    private GameObject laser;

    #endregion

    #region Unity Functions

    /// <summary>
    /// Start Function
    /// </summary>
    void Start()
    {
        // Get the laser sound effect in the paddle
        laserSound = GetComponent<AudioSource>();
        
        // Laser CD and Image CD
        laserCD = 2;
        laserImgCD = 115;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Keyboard Movement
        float h = Input.GetAxis("Horizontal") * Time.deltaTime;

        if (h != 0)
        {
            // Move the paddle
            transform.Translate(new Vector3(h * speed, 0, 0));
        }

        // Accelerometer movement
        transform.Translate(Input.acceleration.x * Time.deltaTime * speed, 0, 0);

        // Get the paddle's position to a vector variable    
        Vector3 currentPosition = transform.position;
        // Modify the variable to keep the X position within minX and maxX
        currentPosition.x = Mathf.Clamp(currentPosition.x, -range, range);
        // Set the transform position to our modified vector
        transform.position = currentPosition;

        // Increment the width of the Cooldown bar image
        if (laserCooldownImg.rectTransform.sizeDelta.x < 230)
        {
            Vector2 scale = laserCooldownImg.rectTransform.sizeDelta;
            scale.x += laserImgCD * Time.deltaTime;
            laserCooldownImg.rectTransform.sizeDelta = scale;
        }
    }

    #endregion

    #region Other Functions

    /// <summary>
    /// Shoots the laser
    /// </summary>
    public void ShootLaser()
    {
        // Check if ball is moving
        if (GameManager.gameOn)
        {
            // Check if laser is ready
            if (laserReady)
            {
                //Instantiate the laser playing the sound effect
                laser = Instantiate(laserPrefab, transform.position + new Vector3(0, 0, 2), Quaternion.Euler(90, 0, 0)) as GameObject;
                laser.tag = "Laser";
                laserSound.Play();

                //Cooldown
                laserReady = false;
                laserCooldownImg.rectTransform.sizeDelta = new Vector2(0, 32);
                StartCoroutine(LaserCooldown());
            }
        }
        // If the ball is not moving, start moving it
        else
        {
            GameManager.gameOn = true;
        }
    }

    /// <summary>
    /// Makes the Laser available
    /// </summary>
    IEnumerator LaserCooldown()
    {
        yield return new WaitForSeconds(laserCD);
        laserReady = true;
    }

    #endregion
}
