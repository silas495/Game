using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {


	public Vector3 xPosition;
	public Vector3 velocity;
	public Vector3 acceleration;
	public World world;


	void Start () {
		world = FindObjectOfType<World>();
		xPosition = transform.position;
	}
	
	void Update () {
		acceleration = Vector3.ClampMagnitude( combine(), 2);
		velocity = Vector3.ClampMagnitude(velocity + acceleration * Time.deltaTime, 4);
		xPosition += velocity * Time.deltaTime;
	}

	private void LateUpdate()
	{
		transform.position = xPosition;
		transform.rotation = Quaternion.FromToRotation(Vector3.forward, velocity);
	}

	Vector3 cohesion(){
		Vector3 r = Vector3.zero;
		var neighs = world.getNeigh(this, 13);
		if (neighs.Count == 0) return r;

		foreach (var agent in neighs) r += agent.xPosition;
		r /= neighs.Count;
		r -= this.xPosition;

		return Vector3.Normalize(r);
	}

	Vector3 separation()
	{
		Vector3 r = Vector3.zero;
		var neighs = world.getNeigh(this, 3);
		if (neighs.Count == 0) return r;

		foreach (var agent in neighs)
		{
			Vector3 twm = this.xPosition - agent.xPosition;
			if (Vector3.Magnitude(twm) != 0)
			{
				r += Vector3.Normalize(twm) / Vector3.Magnitude(twm) / Vector3.Magnitude(twm);
			}
		}

		return Vector3.Normalize(r);
	}

	Vector3 alignment()
	{
		Vector3 r = Vector3.zero;
		var neighs = world.getNeigh(this, 13);
		if (neighs.Count == 0) return r;

		foreach (var agent in neighs) r += agent.velocity;

		return Vector3.Normalize(r);
	}

	Vector3 combine()
	{
		return cohesion() + separation() * 2 + alignment();
	}
}
