using UnityEngine;
using System.Collections;

public class Warpable : MonoBehaviour {

	public MusicController _musicController;
	public bool _usesMusicController = false;

	public float scaleMultiplier = 1.5f;

	private Vector3  originalScale;

	private Vector3 nextScale = Vector3.one;
	private Vector3 velocity;

	private bool flipTheScale = false;
	
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
		originalScale = transform.localScale;
		nextScale = originalScale;
	}
	
	void HandleMusicPulseOccurred ()
	{
		nextScale = _musicController.ShouldMoveThisFrame ? 1.0f * originalScale : scaleMultiplier * originalScale;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_usesMusicController)
		{
			var scale = transform.localScale;
			scale = Vector3.SmoothDamp(scale, nextScale, ref velocity, _musicController.TimeTilNextTick);
       		 transform.localScale = scale;
        }
    }
}
