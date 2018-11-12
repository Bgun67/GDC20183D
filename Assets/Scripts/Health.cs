using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] private float _health = 100f;

    public float ObjHealth
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
        }
    }
}
