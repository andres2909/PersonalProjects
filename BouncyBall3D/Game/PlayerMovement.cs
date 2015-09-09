using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
    //Public Variables
    public float moveSpeed = 100.0f; //Movement Speed
    public int jumpHeight = 400; //Jump Height
    public Transform cameraPosition; //Camera's position in the world
    public AudioSource jumpSound; //Audio Source for the jumping sound


    //Private Variables
    private bool grounded = false; //Grounded variable checks if the player is touching the ground
    private int moveSpeeedMultiplier = 1;

	// Update is called once per frame
	void Update () 
    {
        //MoveX variable stores the input in the X axis multiplying it by the movement speed and Time.deltaTime
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime * moveSpeeedMultiplier;
        float moveXaccel = Input.acceleration.x * moveSpeed * 5 * Time.deltaTime * moveSpeeedMultiplier;

        //Moves the character in the X axis
        transform.Translate(moveX, 0, 0);
        transform.Translate(moveXaccel, 0, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        //Rotation will always be 0
        transform.rotation = Quaternion.Euler(0, 0, 0);

        transform.Rotate(0,0,5);

        //If the height of the player is less or equals to the Camera's height - 15 units...
        if (transform.position.y <= cameraPosition.position.y - 15)
        {
            //Multiply the movement speed by 0 so the player cant move
            moveSpeed *= 0;
            //Multiply the player's jump height by 0 so he cant jump
            jumpHeight *= 0;
            //Stop the jump sound from playing
            jumpSound.Stop();
        }

        //Whenever the Y velocity has a positive value...
        if (rigidbody.velocity.y > 0)
        {
            //Disable the player's collider
            //collider.enabled = false;
            grounded = false;
        }
        else
        {
            grounded = Physics.Raycast(transform.position, -Vector3.up, 0.5f);
            collider.enabled = true;
        }

        //If the player reaches the map's limits (-20, 20)...
        if (transform.position.x >= 20)
        {
            //Multiply the X position value by -1 so the player appears in the opposite edge
            transform.position = new Vector3(-19, transform.position.y, 0);
        }
        else if (transform.position.x <= -20)
        {
            //Multiply the X position value by -1 so the player appears in the opposite edge
            transform.position = new Vector3(19, transform.position.y, 0);
        }

        if (GeneralControls.gameState == GameState.paused)
        {
            moveSpeeedMultiplier = 0;
        }
        else if (GeneralControls.gameState == GameState.playing)
        {
            moveSpeeedMultiplier = 1;
        }
	}

    void FixedUpdate()
    {   
        //Player jumps automatically only if he is grounded (if the grounded variable is set to true)
        if (grounded == true)
        {
            Jump();
        }
    }

    //Player's Jump
    void Jump() 
    {
        //Velocity is set to 0
        rigidbody.velocity = new Vector3(0, 0, 0);
        //Add a force in the Y axis
        rigidbody.AddForce(new Vector3(0, jumpHeight, 0));
        //Play the Jump Sound
        jumpSound.Play();
    }
}
