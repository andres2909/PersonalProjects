using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour
{
    #region Global Variables

    // Public Variables
    public AudioSource bounceSound;        // Bounce sound effect
    public AudioSource deathSound;         // Death sound effect

    // Private Variables
    private float yDir = -1;                // Direction on Y
    private float xDir = 1;                 // Direction on X
    private float xAngle = 1;               // Angle on X
    private float yAngle = 1;               // Angle on Y
    private float speed = 12;               // Speed of the ball
    private bool destroyBrick = true;       // Makes sure ball doesn't destroy 2 cubes at the same time
    private GameObject paddle;              // Paddle GameObject

    #endregion

    #region Unity Functions

    // Use this for initialization
    void Start()
    {
        // Get the paddle as a GameObject
        paddle = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Only move ball when gameOn is true
        if (GameManager.gameOn)
        {
            // Move the ball
            transform.position += new Vector3(xAngle * speed * xDir * Time.deltaTime, 0, yAngle * speed * yDir * Time.deltaTime);

            // Right
            if (transform.position.x >= 14)
            {
                xDir = -1;
                bounceSound.Play();
            }
            // Left
            else if (transform.position.x <= -14)
            {
                xDir = 1;
                bounceSound.Play();
            }
            // Top
            else if (transform.position.z >= 13)
            {
                yDir = -1;
                bounceSound.Play();
            }
            // Bottom
            else if (transform.position.z < -50)
            {
                deathSound.Play();
                GameManager.lives--;
                GameManager.gameOn = false;
            }
        }
        // If gameOn is not true, place the ball in the paddle
        else
        {
            transform.position = paddle.transform.position - new Vector3(0, 0, -1.5f);
        }
    }

    /// <summary>
    /// Ball collision
    /// </summary>
    void OnCollisionEnter(Collision col)
    {
        // If ball collides with a brick while going up...
        if (col.gameObject.tag == "Brick" && destroyBrick)
        {
            // Change ball's direction
            yDir *= -1;

            // Play bounce sound
            bounceSound.Play();

            Vector3 dist = col.gameObject.transform.position - transform.position;

            xAngle = (dist.x / (paddle.transform.localScale.x / 2));
            if (xAngle >= 0)
            {
                xDir = -1;
                xAngle += 0.3f;
            }
            else
            {
                xDir = 1;
                xAngle -= 0.3f;
            }
            xAngle = Mathf.Abs(xAngle);

            // Set ready to false while code is executing
            destroyBrick = false;

            // If the ball has the same color as the brick...
            if (GetComponent<Renderer>().material.color == col.gameObject.GetComponent<Renderer>().material.color)
            {
                // +10 points
                GameManager.score += 10;
            }
            // If the ball and the brick have different colors...
            else
            {
                // +5 Points
                GameManager.score += 5;
            }

            // Reduce paddle's width by 0.05 (down to 50% of normal size)
            if (paddle.transform.localScale.x >= 3.5)
            {
                paddle.transform.localScale -= new Vector3(0.05f, 0, 0); // 70 Hits
                //paddle.transform.localScale -= new Vector3(0.025f, 0, 0); // 140 Hits

            }

            // Increase ball's speed by 0.1 (up to 100% of normal speed)
            if (speed <= 24)
            {
                speed += 0.1f; // 100 Hits
            }

            // Increase brick's movement by 0.03 (up to 150% of normal speed)
            if (BrickMovement.speed <= 1.5f)
            {
                BrickMovement.speed += 0.03f; // 50 Hits
            }

            // Reduce the cooldown of the laser down to 50%
            if (Paddle.laserImgCD < 230)
            {
                Paddle.laserImgCD += 0.87f;
            }
            if (Paddle.laserCD > 1)
            {
                Paddle.laserCD -= 0.01f;
            }

            // Change the color of the ball to the color of the brick
            GetComponent<Renderer>().material.color = col.gameObject.GetComponent<Renderer>().material.color;

            // Ensure that code doesn't execute twice
            StartCoroutine(BreakBrick());

            // Move the brick to be destroyed
            col.gameObject.transform.position = new Vector3(100, 100, 0);
        }
        // If the ball collides with the paddle while going down...
        else if (col.gameObject.tag == "Player")
        {
            yDir = 1;

            // Play bounce sound
            bounceSound.Play();

            Vector3 dist = col.gameObject.transform.position - transform.position;

            xAngle = (dist.x * 1.5f / paddle.transform.localScale.x);
            if (xAngle >= 0)
            {
                xDir = -1;
                xAngle += 0.3f;
            }
            else
            {
                xDir = 1;
                xAngle -= 0.3f;
            }
            xAngle = Mathf.Abs(xAngle);
        }
        else if (col.gameObject.tag == "Laser")
        {
            deathSound.Play();
        }


    }

    #endregion

    #region Other Functions

    IEnumerator BreakBrick()
    {
        yield return new WaitForSeconds(0.05f);
        destroyBrick = true;
    }

    #endregion


}
