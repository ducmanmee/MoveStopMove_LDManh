using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float speed = 1f;
    public float speedRotate = 500f;
    public Transform mesh;
    Quaternion target;

    void Start()
    {
        
    }

    public void OnDespawn()
    {
        Pooling.ins.EnQueueObj("0", this.gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        mesh.Rotate(Vector3.forward, speedRotate * Time.deltaTime);
        if(Vector3.Distance(transform.position, Player.Instance.transform.position) > 5.8f)
        {
            OnDespawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constain.TAG_ENEMY) || other.CompareTag(Constain.TAG_PLAYER))
        {
            OnDespawn();
        }
    }
}
