using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : INavState
{
	NPCBehaviour parent;

	public void Init(NPCBehaviour parent)
	{
		parent.SetTarget(parent.transform.position);
	}

	public void Update()
	{
		
	}
}
