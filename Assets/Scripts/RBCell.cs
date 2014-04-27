using UnityEngine;
using System.Collections.Generic;

public class RBCell : MonoBehaviour {

	private Moveable moveable;
	private List<GameObject> childGlommers;

	void Awake()
	{
		moveable = GetComponent<Moveable> ();

	}

	void OnDestroy()
	{
		if(childGlommers != null)
		{
			foreach(var glommer in childGlommers)
			{
				Destroy(glommer);
			}
		}
	}

	void OnCollisionEnter(Collision col)
	{
		Debug.Log ("collision happened: " + col.collider.name);

		var obj = col.collider.gameObject;
		if(obj.layer == LayerMask.NameToLayer("Glommer"))
		{
			// re-parent the glommer
			var objMoveable = obj.GetComponent<Moveable> ();
			if (childGlommers == null) {
					childGlommers = new List<GameObject> ();
			}
			childGlommers.Add (obj);
			obj.transform.parent = rigidbody.transform;
			obj.rigidbody.isKinematic = true;
			obj.layer = LayerMask.NameToLayer("RBC");

			// TODO: set vel based on collision forces
//			var objPos = objMoveable.transform.position;

			// let physics take over for me
			rigidbody.isKinematic = false;

			if(moveable)
			{
				moveable._usesMusicController = false;
			}
			Destroy (objMoveable);
		}
		else if(obj.layer == LayerMask.NameToLayer("WBC"))
		{
			if(childGlommers != null && childGlommers.Count > 0)
			{
				Destroy(gameObject);
			}
		}
	}

	void Update()
	{
		if(transform.position.y > 7.0f || transform.position.y < -7.0f)
		{
			Destroy (gameObject);
			return;
		}

		if(!moveable)
			return;

//		var box = GetComponent<BoxCollider>();
//		var tip = transform.position;
//		tip.y += box.bounds.extents.y;
//
//		RaycastHit[] hits;
//		hits = Physics.RaycastAll (new Ray (tip, moveable.direction), 0.1f);
//
//		bool shouldStop = false;
//		if(hits != null)
//		{
//			foreach(var hit in hits)
//			{
//				if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Glommer"))
//					shouldStop = true;
//			}
//		}
//
//		if(shouldStop)
//			Destroy (moveable);
	}
}
