using UnityEngine;
using System.Collections;

public class MM_Ball : MonoBehaviour 
{
	#region Global Variables

	#endregion
	
	#region Unity Functions

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

    void OnCollisionEnter(Collision col)
    {
        GetComponent<Rigidbody>().AddForce(0, 410, 0);
    }

	#endregion
	
	#region Other Functions
	
	#endregion
}
