using UnityEngine;
using System.Collections;

public class Blobbie : MonoBehaviour {

	public enum State
	{
		Alive,
		BeingAttacked,
		Dead
	}

	public Transform parent;
	public float maxStretchScale = 3.0f;

	private Vector3 originalScale;
	private Vector3 originalPosition;

	public bool isOccupied = false;

	public bool isBeingAttacked = false;

	public float pulseSpeed = 1.0f;

	public float beingAttackedTime = 0;
	public State state = State.Alive;



	// Use this for initialization
	void Start () {
		originalScale = transform.localScale;
		originalPosition = transform.localPosition;


	}
	
	// Update is called once per frame
	void Update () {

		var pulseDirection = (0.5f + 0.5f * Mathf.Sin (pulseSpeed * Time.time)) * originalScale;
		transform.localScale = originalScale + pulseDirection;

		
		if(state == State.BeingAttacked)
			beingAttackedTime += Time.deltaTime;

		for(int i = 0; i < transform.childCount; i++)
		{
			var t = transform.GetChild(i);
			if(t)
			{
				t.position += 0.5f * (parent.position - t.position) * Time.deltaTime;
			}
		}

		isOccupied = transform.childCount > 0;
	}

	void OnCollisionEnter(Collision collision)
	{

		bool wasOccupied = isOccupied;
		bool isGlommer = collision.collider.gameObject.layer == LayerMask.NameToLayer("Glommer");
		bool isVirii = collision.collider.gameObject.layer == LayerMask.NameToLayer("Virii");
		if(isGlommer || isVirii)
		{
			isOccupied = true;
		}

		if(wasOccupied && state == State.Alive)
		{
			state = State.BeingAttacked;
			StartCoroutine(KillOffBlobbieLater());
		}
		else if(!wasOccupied)
		{
			var virii = collision.collider.gameObject;
			var moveable = virii.GetComponent<Moveable>();
			if(moveable)
				Destroy (moveable);

			virii.transform.parent = transform;
			StartCoroutine(RemoveCollider(virii));
		}
	}

	IEnumerator KillOffBlobbieLater()
	{
		yield return new WaitForSeconds(beingAttackedTime);
		state = State.Dead;
		Destroy (gameObject, 0.5f);
	}

	IEnumerator RemoveCollider(GameObject virus)
	{
		GameObject v = virus;
		yield return new WaitForEndOfFrame();
		if(v)
		{
			v.collider.enabled = false;
			Destroy (v.gameObject, 3.0f);
		}
	}
}
