using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float speed = 1f;
    public float speedRotate = 500f;
    public Transform mesh;
    Quaternion target;
    private float distanceTraveled = 0f;

    void Start()
    {
        
    }

    public void OnDespawn()
    {
        distanceTraveled = 0f;
        Pooling.ins.EnQueueObj("0", this.gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        distanceTraveled += speed * Time.deltaTime;
        //mesh.Rotate(Vector3.forward, speedRotate * Time.deltaTime);
        if (distanceTraveled > 6f)
        {
            OnDespawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constain.TAG_ENEMY) || other.CompareTag(Constain.TAG_PLAYER))
        {
            Character C = other.GetComponent<Character>();
            C.isDead = true;
            OnDespawn();   
        }
    }
}
