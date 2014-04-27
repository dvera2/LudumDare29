using UnityEngine;
using System.Collections;

public class WBCell : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update()
	{
		
		if(transform.position.y > 7.0f || transform.position.y < -7.0f)
		{
			Destroy (gameObject);
        }
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.collider.tag == "virus")
		{
            Destroy(col.collider.gameObject);
        }
    }
}
