using UnityEngine;
using System.Collections;

public class RBCell : MonoBehaviour {

	private Moveable moveable;

	void Awake()
	{
		moveable = GetComponent<Moveable> ();
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.gameObject.layer == LayerMask.NameToLayer ("Glommer"))
		{
			Destroy (GetComponent<Moveable> ());
		}

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

		if(!moveable)
			return;

		var box = GetComponent<BoxCollider>();
		var tip = transform.position;
		tip.y += box.bounds.extents.y;

		RaycastHit[] hits;
		hits = Physics.RaycastAll (new Ray (tip, moveable.direction), 1.0f);

		bool shouldStop = false;
		if(hits != null)
		{
			foreach(var hit in hits)
			{
				if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Glommer"))
					shouldStop = true;
			}
		}

		if(shouldStop)
			Destroy (moveable);
	}
}
