using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public delegate void MusicEvent();
	public event MusicEvent MusicPulseOccurred;

	public int _bpm = 120;
	private int _cachedBPM;

	public float _heartbeatTime = 60.0f / 120.0f;
	public float _elapsedTime = 0;
	private float _timeOfNextEvent = 0;

	private static MusicController _instance;
	public static MusicController Instance
	{
		get {
			if(!_instance)
			{
				GameObject inst = GameObject.FindGameObjectWithTag("MusicController");
				if(!inst)
				{
					
					inst = new GameObject("MusicController");
				}

				_instance = inst.GetComponent<MusicController>();

				if(!_instance)
					_instance = inst.AddComponent<MusicController>();
			}
			return _instance;
		}
	}

	public int BPM
	{
		get
		{
			return _bpm;
		}
		set
        {
            _heartbeatTime = 60.0f / value;
        }
    }

	private bool _shouldMoveThisFrame = false;
	public bool ShouldMoveThisFrame
	{
		get
		{
			return _shouldMoveThisFrame;
		}
	}

	public float TimeTilNextTick
	{
		get { return _timeOfNextEvent - _elapsedTime; }
	}

	void Start()
	{
		_cachedBPM = _bpm;
		_heartbeatTime = 60.0f / _bpm;
		_timeOfNextEvent = _heartbeatTime;
	}
	
	// Update is called once per frame
	void Update () {
		_elapsedTime += Time.deltaTime;

		if(_cachedBPM != _bpm)
		{
			_cachedBPM = _bpm;
			_heartbeatTime = 60.0f / _bpm;
		}

		if(_elapsedTime >= _timeOfNextEvent)
		{
			_shouldMoveThisFrame = !_shouldMoveThisFrame;
			_timeOfNextEvent = _elapsedTime + _heartbeatTime;

			if(MusicPulseOccurred != null)
			{
				MusicPulseOccurred();
			}
		}
	}

	void OnDrawGizmos()
	{
		if(_shouldMoveThisFrame)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawCube(Vector3.zero, Vector3.one);
		}

	}
}
