using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INavState {
	void Init(NPCBehaviour parent);
	void Update();
}
