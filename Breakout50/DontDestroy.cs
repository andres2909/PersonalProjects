using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour 
{	
	#region Unity Functions

	// Use this for initialization
	void Awake () 
	{
        DontDestroyOnLoad(gameObject);
	}
	#endregion
}
