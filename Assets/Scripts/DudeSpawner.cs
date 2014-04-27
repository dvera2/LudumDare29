using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DudeSpawner : MonoBehaviour {

	[System.Serializable]
	public class PrefabInfo
	{
		public string ID;
		public GameObject prefab;
		public float weight;
		public float normWeight;
	}

	public PrefabInfo[] prefabEntries;

	public Transform spawnPoint;

	public float spawnRatePerSecond = 1.0f;
	public Vector3 spawnDirection = Vector3.right;

	private float _elapsedTime = 0;
	private float _timeToSpawn = 0;


	// Use this for initialization
	protected virtual void Start () {
		_elapsedTime = 0;
		SortPrefabs();

		ScheduleNext();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		_elapsedTime += Time.deltaTime;
		if(_elapsedTime >= _timeToSpawn + Random.Range(-0.2f, 0.2f) * (1.0f / spawnRatePerSecond)) {

			var info = GetNextPrefab();
			if(info == null)
				Debug.LogError("Info is NULL!");
			else
				Debug.Log ("Spawned prefab: " + info.ID);

			var inst = (GameObject)GameObject.Instantiate(info.prefab, Vector3.zero, Quaternion.identity);
			
			inst.name = info.ID;
			inst.transform.parent = transform;
			inst.transform.position = spawnPoint.position;
			var moveable = inst.GetComponentInChildren<Moveable>();
			if(moveable) moveable.direction = spawnDirection;

			ScheduleNext();
		}
	}

	protected virtual void ScheduleNext()
	{
		_timeToSpawn = _elapsedTime + (1.0f / spawnRatePerSecond);        
	}


	protected void SortPrefabs()
	{
		List<PrefabInfo> entries = new List<PrefabInfo>(prefabEntries);
		float total = 0;
		foreach(var e in entries)
			total += e.weight;

		foreach(var e in entries)
			e.normWeight = e.weight / total;

		entries.Sort((x,y) => {
			if(x.weight < y.weight)
				return -1;
			if(x.weight > y.weight)
				return 1;
			return 0;
		});

		prefabEntries = entries.ToArray();
	}

	protected PrefabInfo GetNextPrefab()
	{
		float r = Random.Range(0,1.0f);
		float total = 0;
		for(int i = prefabEntries.Length - 1; i >= 0; i--)
		{
			total += prefabEntries[i].normWeight;
			if(r <= total)
				return prefabEntries[i];
		}
		return null;
	}

	protected virtual void OnDrawGizmos()
	{
		if(!spawnPoint)
			return;

		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(spawnPoint.position, 0.5f);
	}
}
