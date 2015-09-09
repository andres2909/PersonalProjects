using UnityEngine;
using System.Collections;

public class Plus10 : MonoBehaviour 
{
    //Time the +10 takes to fade away
    public float fadeTimer = 1.5f;
	
	// Update is called once per frame
	void Update () 
    {
        //3D text will move up
        transform.Translate(new Vector3(0, Time.deltaTime * 10, 0));

        //The timer will be reduced each frame
        fadeTimer -= Time.deltaTime;
        //Opacity of the text will be equals to the timer
        renderer.material.color = new Color(1, 1, 1, fadeTimer);

        //When the timer reaches 0 it will no longer count backwards
        if (fadeTimer < 0)
        {
            fadeTimer = 0;
            //Delete 30 text after opacity turns to 0
            GameObject.Destroy(gameObject);
        }
	}
}
