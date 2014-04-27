using UnityEngine;
using System.Collections;

public class DespawnTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "RBC")
		{
			Destroy(other.gameObject);
		}
	}
}
