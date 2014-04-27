using UnityEngine;
using System.Collections;

public class Moveable : MonoBehaviour {

	public MusicController _musicController;

	public Vector3 direction = Vector3.right;
	public float moveSpeed = 1.0f;

	public bool _usesMusicController = false;

	private Vector3 nextPosition;
	private Vector3 velocity;

	void OnDestroy()
	{
		if(_musicController)
		{
			_musicController.MusicPulseOccurred -= HandleMusicPulseOccurred;
		}
	}

	void Awake()
	{
		if(!_musicController)
			_musicController = MusicController.Instance;

		_musicController.MusicPulseOccurred += HandleMusicPulseOccurred;
	}

	void HandleMusicPulseOccurred ()
	{
		nextPosition = transform.position + direction * moveSpeed *  _musicController.TimeTilNextTick + velocity * _musicController.TimeTilNextTick;
	}

	// Update is called once per frame
	void Update () 
	{
		if(_usesMusicController)
		{
			if(_musicController.ShouldMoveThisFrame)
			{
				var pos = transform.position;
				pos = Vector3.SmoothDamp(pos, nextPosition, ref velocity, _musicController.TimeTilNextTick);
				transform.position = pos;
			}
		}
		else
		{
			transform.position += (direction * moveSpeed * Time.deltaTime);
		}
	}
}
