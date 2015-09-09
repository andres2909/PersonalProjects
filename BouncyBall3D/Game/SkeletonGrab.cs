using UnityEngine;
using System.Collections;

public class SkeletonGrab : MonoBehaviour 
{
    private GameObject playerBall;
    private bool grab = false;
    private bool goDown = false;

	// Use this for initialization
	void Start () 
    {
        playerBall = GameObject.Find("Player");
        //transform.parent.transform.parent.rigidbody.useGravity = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (grab == true)
        {
            playerBall.transform.position = transform.position;
            if (transform.parent.gameObject.transform.position.x > 0)
                transform.parent.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            else if (transform.parent.gameObject.transform.position.x < 0)
                transform.parent.gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
            transform.parent.gameObject.GetComponent<EnemyMove>().speed = 5;
            transform.parent.gameObject.GetComponent<EnemyMove>().timer = 5;
            transform.parent.gameObject.GetComponent<Animator>().SetBool("Run", true);
            GameObject.Find("Camera").GetComponent<GeneralControls>().cameraSpeedMultiplier = 0;
            GameObject.Find("Player").GetComponent<AudioSource>().enabled = false;
        }
        if (goDown == true)
        {
            transform.parent.transform.parent.rigidbody.useGravity = true;
        }
	}

    void OnTriggerEnter(Collider col) 
    {
        if (col.gameObject == playerBall)
        {
            playerBall.transform.position = transform.position;
            grab = true;
        }
        if (col.gameObject.name == "JumpTrigger")
        {
            goDown = true;
            transform.parent.transform.parent.rigidbody.AddForce(0, 700, 0);
            transform.parent.gameObject.GetComponent<Animator>().SetBool("Run", false);
        }
    }
}
