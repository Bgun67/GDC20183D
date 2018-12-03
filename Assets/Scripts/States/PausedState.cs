using GDC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedState : State
{
	public PausedState(Player player) : base(player)
	{

	}
	public override void Update()
	{
		base.Update();
		Cursor.lockState = CursorLockMode.None;
	}
}
