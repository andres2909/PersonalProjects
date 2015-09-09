using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class BallSkin : MonoBehaviour
{
    private List<Material> matList; //List of all the materials

    void Start() 
    {
        matList = new List<Material>();

        //Get the Materials from the Skins folder in the Resources
        foreach (Material mat in Resources.LoadAll<Material>("Skins"))
        {
            //Add them to the Materials List
            matList.Add(mat);
        }

        renderer.material = matList[PlayerPrefs.GetInt("CurrentSkin")];
    }

	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * -600, 0) * Time.deltaTime);
        transform.Rotate(new Vector3(0, Input.acceleration.x * -600, 0) * Time.deltaTime);
	}
}
