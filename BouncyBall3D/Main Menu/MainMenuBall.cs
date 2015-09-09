using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuBall : MonoBehaviour 
{
    //Public Variables
    public int height = 700;
    public GameObject Skin;

    //Private Variables
    private List<Material> matList;

	// Use this for initialization
	void Start () 
    {
        //Variable initialization
        matList = new List<Material>();

        //Get the Materials from the Skins folder in the Resources
        foreach (Material mat in Resources.LoadAll<Material>("Skins"))
        {
            //Add them to the Materials List
            matList.Add(mat);
        }

        if (Skin != null)
        {
            Skin.renderer.material = matList[PlayerPrefs.GetInt("CurrentSkin")];
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnCollisionEnter() 
    {
        rigidbody.AddForce(new Vector3(0,height,0));
    }
}
