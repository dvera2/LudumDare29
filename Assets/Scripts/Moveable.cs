using UnityEngine;
using System.Collections;

public class Moveable : MonoBehaviour {

	public MusicController _musicController;

	public Vector3 direction = Vector3.right;
	public float moveSpeed = 1.0f;

	public bool _usesMusicController = false;

	public Vector3 target;
	private Vector3 nextPosition;
	private Vector3 velocity;

	private bool hasCollided = false;

	void OnStart()
	{
		target = direction * 100;
	}
			
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

	void Start()
	{
		nextPosition = transform.position + direction * moveSpeed * _musicController.TimeTilNextTick;
	}

	void HandleMusicPulseOccurred ()
	{
		// new pulse; disable physics
		if (hasCollided)
		{
			hasCollided = false;
			//StartCoroutine(DeferZeroVelocity());
		}
		nextPosition = transform.position + direction * moveSpeed * _musicController.TimeTilNextTick; // + velocity * _musicController.TimeTilNextTick;
	}

	IEnumerator DeferZeroVelocity()
	{
		yield return new WaitForEndOfFrame ();
//		if(hasCollided)
		{
			hasCollided = false;
			rigidbody.isKinematic = false;
			rigidbody.velocity = Vector3.zero;
			rigidbody.isKinematic = true;
		}
	}
	
	void OnCollisionEnter(Collision col)
	{
		if(gameObject.layer == LayerMask.NameToLayer("RBC"))
		   return;

		hasCollided = true;
	}

	// Update is called once per frame
	void Update () 
	{
//		var look = transform;
//		look.LookAt(target);
//		transform.rotation = Quaternion.Slerp(transform.rotation, look.rotation, Time.deltaTime);
//		transform.Rotate(new Vector3(0, 90, 0));

		if(_usesMusicController && !hasCollided)
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
