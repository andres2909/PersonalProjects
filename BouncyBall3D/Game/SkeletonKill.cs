using UnityEngine;
using System.Collections;

public class SkeletonKill : MonoBehaviour
{
    public GameObject deadSkeleton;
    public AudioSource deathSound;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider col) 
    {
        Vector3 deathPos = new Vector3(transform.parent.transform.parent.position.x, transform.parent.transform.parent.position.y, 0);

        if (col.gameObject.name == "Player" && GameObject.Find("Player").rigidbody.velocity.y < 0)
        {
            GameObject.Instantiate(deadSkeleton, deathPos, transform.parent.transform.parent.transform.rotation);
            GameObject.Destroy(transform.parent.transform.parent.gameObject);            
        }
    }
}
