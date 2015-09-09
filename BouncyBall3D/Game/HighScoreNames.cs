using UnityEngine;
using System.Collections;

public class HighScoreNames : MonoBehaviour 
{
    public string stringToEdit = "Hello World";

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //foreach (char c in Input.inputString)
        //{
        //    if (c == "\b"[0])
        //        if (guiText.text.Length != 0)
        //            guiText.text = guiText.text.Substring(0, guiText.text.Length - 1);

        //        else
        //            if (c == "\n"[0] || c == "\r"[0])
        //                print("User entered his name: " + guiText.text);
        //            else
        //                guiText.text += c;
        //}

	}

    //void OnGUI() 
    //{
    //    // Make a background box
    //    GUI.Box(new Rect(10, 10, 100, 90), "Loader Menu");

    //    // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
    //    //if (GUI.Button(new Rect(20, 40, 80, 20), "Level 1"))
    //    //{
    //    //    Debug.Log("Button 1");
    //    //}

    //    //GUI.TextField(new Rect(20, 40, 80, 20), text);
    //    stringToEdit = GUI.TextField(new Rect(20, 40, 80, 20), stringToEdit, 25);

    //    // Make the second button.
    //    if (GUI.Button(new Rect(20, 70, 80, 20), "Level 2"))
    //    {
    //        Debug.Log(stringToEdit);
    //    }
    //}
}
