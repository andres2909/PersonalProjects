using UnityEngine;
using System.Collections;

public class BrickMovement : MonoBehaviour
{
    #region Global Variables

    public static float speed = 0.05f;

    #endregion

    #region Unity Functions

    // Update is called once per frame
    void Update()
    {
        // Bricks will only move when the ball is moving
        if (GameManager.gameOn)
        {
            // Move at normal speed while brick is inside the field
            if (transform.position.z < 11f)
            {
                transform.Translate(0, 0, -speed * Time.deltaTime, Space.World);
            }
            // If it's not inside, move faster
            else
            {
                transform.Translate(0, 0, -20 * Time.deltaTime, Space.World);
            }

            // When the brick is outside, destroy it
            if (transform.position.y >= 100)
            {
                StartCoroutine(destroyBrick());
            }
        }
    }

    #endregion

    #region Other Functions

    /// <summary>
    /// Destroys a brick after 0.1 seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator destroyBrick()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    #endregion

    



}
