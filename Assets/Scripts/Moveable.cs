using UnityEngine;
using System.Collections;

public class Moveable : MonoBehaviour {

	public Vector3 direction = Vector3.right;
	public float moveSpeed = 1.0f;
	
	// Update is called once per frame
	void Update () {
		transform.position += (direction * moveSpeed * Time.deltaTime);
	}
}
