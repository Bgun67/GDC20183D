using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GDC
{
    [RequireComponent(typeof(Rigidbody))]
    public class Grenade : MonoBehaviour
    {

        float _detTime = 3f;
        float _explosionDmg = 100f;
        float _explosionRange = 5f;
        int _projectiles = 20;
        float _projectileDmg = 40f;

        private void Start()
        {

        }

        private void Update()
        {
            _detTime -= Time.deltaTime;
            //add explosion
            if (_detTime <= 0)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRange);

                foreach (Collider col in hitColliders)
                {
                    Health a = col.GetComponent<Health>();
                    if (a)
                    {
                        float distMod = Mathf.Clamp(1 / (transform.position - col.ClosestPointOnBounds(transform.position)).magnitude, 0.01f, 5f);
                        a.ObjHealth -= _explosionDmg * distMod;
                        Debug.Log(_explosionDmg * distMod);
                    }
                }
                for(int x = 0; x < _projectiles; x++)
                {
                    RaycastHit rayHit;
                    Vector3 dir = Random.rotation * Vector3.forward*10;
                    Debug.DrawRay(transform.position, dir, Color.red);

                    if (Physics.Raycast(transform.position, dir, out rayHit, 10f))
                    {
                        Health a = rayHit.collider.GetComponent<Health>();
                        if (a)
                        {
                            a.ObjHealth -= _projectileDmg;
                        }
                    }
                }

                Destroy(this.gameObject);
            }
        }



    }
}

