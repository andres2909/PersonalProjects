using UnityEngine;
using System.Collections;

public class CreateBrick : MonoBehaviour
{
    #region Global Variables

    // Publoc Variables
    public GameObject brickPrefab;      // Brick prefab
    public Material[] materials;        // Array of materials containing all the colors

    #endregion

    #region Unity Functions
    
    void OnTriggerExit(Collider col)
    {
        // Check if the object exiting the trigger is a brick
        if (col.gameObject.tag == "Brick")
        {
            // Instantiate a new brick and assign a random material
            GameObject newBrick = Instantiate(brickPrefab, transform.position, Quaternion.identity) as GameObject;
            newBrick.tag = "Brick";
            newBrick.GetComponent<Renderer>().material = materials[Random.Range(0, materials.Length)];
        }
    }

    #endregion
    
}
