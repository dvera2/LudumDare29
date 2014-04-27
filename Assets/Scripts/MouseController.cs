using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

	public DudeSpawner spawner;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
		{
			var clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if(Physics.Raycast(clickRay, out hitInfo, 100.0f, ~LayerMask.NameToLayer("Screen")))
			{
				var clickPos = hitInfo.point;
				clickPos.z = 0;
				spawner.spawnDirection = Vector3.Normalize(clickPos - spawner.spawnPoint.transform.position);
			}
		}
	}
}
