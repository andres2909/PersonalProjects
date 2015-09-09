using UnityEngine;
using System.Collections;

public class ColorAnimation : MonoBehaviour 
{
    #region Global Variables

    // Private Variables
    private Renderer brick;     // Controls the material's color of the brick

    private float red;          // Ammount of red, blue and green in the color
    private float blue;         
    private float green;

    private float speed = 1.25f;        // Controls the speed to which the colors change
	#endregion
	
	#region Unity Functions

	// Use this for initialization
	void Start () 
	{
        // Get the Renderer component
        brick = GetComponent<Renderer>();

        // Initial color will be Light Blue
        red = 0;
        blue = 1;
        green = 1;
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Change the color of the brick
        brick.material.color = new Color(red, green, blue);
        
        // Light Blue to Green (0 1 1 - 0 1 0)
        if (red == 0 && green > 0 && blue > 0)
        {
            blue -= Time.deltaTime / speed;
            blue = Mathf.Clamp01(blue);
        }

        // Green to Yellow (0 1 0 - 1 1 0)
        else if (red < 1 && green > 0 && blue == 0)
        {
            red += Time.deltaTime / speed;
            red = Mathf.Clamp01(red);
        }

        // Yellow to Orange to Red (1 1 0 - 1 0.5 0 - 1 0 0)
        else if (red == 1 && green > 0 && blue == 0)
        {
            green -= Time.deltaTime / (speed * 2);
            green = Mathf.Clamp01(green);
        }

        // Red to LightBlue (1 0 0 - 0 1 1)
        else if (red > 0 && green >= 0 && blue >= 0)
        {
            red -= Time.deltaTime / speed;
            red = Mathf.Clamp01(red);
            green += Time.deltaTime / speed;
            green = Mathf.Clamp01(green);
            blue += Time.deltaTime / speed;
            blue = Mathf.Clamp01(blue);
        }
    }

    #endregion
}
