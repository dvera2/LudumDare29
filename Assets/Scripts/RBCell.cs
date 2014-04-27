using UnityEngine;
using System.Collections;

public class RBCell : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
		
		Debug.Log ("collision happened: " + col.collider.name);
        if(col.collider.tag == "wall")
		{
			Debug.Log ("collision happened: " + col.collider.name);
			Destroy(gameObject);
		}
	}

	void Update()
	{
		if(transform.position.y > 7.0f || transform.position.y < -7.0f)
		{
			Destroy (gameObject);
		}
	}
}
