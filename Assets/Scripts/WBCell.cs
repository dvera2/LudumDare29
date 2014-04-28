using UnityEngine;
using System.Collections;

public class WBCell : MonoBehaviour
{
	public bool killDrillers = true;

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
		GameObject collidedObject = col.collider.gameObject;
		Debug.Log(string.Format("killing {0}", collidedObject.tag));
		if(collidedObject.tag == "driller")
		{
			Destroy(gameObject);
			if(killDrillers)
			{
				collidedObject.GetComponent<AudioSource>().Play();
				Destroy(collidedObject);
			}
		}
		else if(collidedObject.tag == "eater" || collidedObject.tag == "glommer")
		{
			collidedObject.GetComponent<AudioSource>().Play();
			Destroy(collidedObject);
		}
	}
}
