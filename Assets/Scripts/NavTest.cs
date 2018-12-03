using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using IngameDebugConsole;

public class NavTest : MonoBehaviour
{

	public GameObject target;
	public NavMeshAgent agent;

	//[ConsoleMethod("nav_set_target", "Set target of npc to head towards")]
	public void SetTarget(GameObject target)
	{
		this.target = target;
	}

	// Use this for initialization
	void Start()
	{
		DebugLogConsole.AddCommandInstance("nav_set_target", "Set target of npc to head towards", "SetTarget", this);
	}

	// Update is called once per frame
	void Update()
	{
		agent.destination = target.transform.position;
	}
}
