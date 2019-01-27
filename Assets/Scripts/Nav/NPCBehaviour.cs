using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCBehaviour : MonoBehaviour {

	private Vector3 navTarget;
	private NavMeshAgent agent;
	private Vector2 facing;
	private INavState state;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}

	public void SetState(INavState state)
	{
		this.state = state;
		state.Init(this);
	}

	public void SetTarget(Vector3 target)
	{
		navTarget = target;
	}
	
	// Update is called once per frame
	void Update () {
		if (navTarget != null)
			agent.destination = navTarget;
		if (state != null)
			state.Update();
	}
}
