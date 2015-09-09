using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    #region Unity Functions

    // Use this for initialization
    void Start()
    {
        // Adds force a to the projectile so it moves forward
        GetComponent<Rigidbody>().AddForce(0, 0, 2000);
    }

    void OnCollisionEnter(Collision col)
    {
        // Brick collision
        if (col.gameObject.tag == "Brick")
        {
            // Move brick to be destroyed
            col.gameObject.transform.position = new Vector3(100, 100, 0);
            // Add 1 to the score
            GameManager.score++;
        }
        // Ball collision
        else if (col.gameObject.tag == "Ball")
        {
            // Lose a life
            GameManager.lives--;
            // Place the ball infront of the paddle
            GameManager.gameOn = false;
        }

        // Destroy the laser GameObject
        Destroy(gameObject);
    }

    #endregion
}
