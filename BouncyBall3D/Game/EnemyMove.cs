using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour 
{
    [HideInInspector]
    public float speed = 1;
    [HideInInspector]
    public float timer = 2;

	// Update is called once per frame
	void Update () 
    {
        transform.Translate(0, 0, speed * Time.deltaTime);

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            transform.Rotate(new Vector3(0, -180, 0));
            timer = 4;
        }
	}
}
