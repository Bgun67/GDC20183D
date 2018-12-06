using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using IngameDebugConsole;

public class NavTest : MonoBehaviour
{

	public Vector3 target;
	public NavMeshAgent agent;

	private HelperCover cover;

	private void GoToCover(HelperCover cover)
	{
		this.cover = cover;
		target = cover.transform.position;
	}

	private void PeekFromCover()
	{
		if(cover != null)
			target = cover.transform.position + cover.peek;
	}

	//[ConsoleMethod("nav_set_target", "Set target of npc to head towards")]
	public void SetTarget(GameObject target)
	{
		this.target = target.transform.position;
	}

	public void GetToCover()
	{
		HelperCover nearestCover = null;
		foreach (HelperCover cover in FindObjectsOfType<HelperCover>())//Find nearest cover
		{
			if (nearestCover == null)
				nearestCover = cover;
			else
			{
				if (Vector3.Distance(transform.position, cover.transform.position) < Vector3.Distance(transform.position, nearestCover.transform.position))
				{
					nearestCover = cover;
				}
			}
		}
		GoToCover(nearestCover);
	}

	// Use this for initialization
	void Start()
	{
		DebugLogConsole.AddCommandInstance("nav_set_target", "Set target of npc to head towards", "SetTarget", this);
		DebugLogConsole.AddCommandInstance("nav_cover", "Send npcs to nearest cover", "GetToCover", this);
		DebugLogConsole.AddCommandInstance("nav_peek", "Tell npcs to peek from cover", "PeekFromCover", this);
	}

	// Update is called once per frame
	void Update()
	{
		if (target != null)
			agent.destination = target;
	}
}
