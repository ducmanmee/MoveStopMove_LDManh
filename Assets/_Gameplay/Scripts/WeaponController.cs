using UnityEngine;
using DG.Tweening;

public class WeaponController : MonoBehaviour
{
    public float speed = 1f;
    public Transform mesh;
    Quaternion target;
    private float distanceToDes = 0f;
    public Character owner;

    void Start()
    {
        
    }

    public void OnDespawn()
    {
        distanceToDes = 0f;
        UnBullet(1);
    }

    private void Update()
    {
        if(owner.IsDead) OnDespawn();
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        distanceToDes += speed * Time.deltaTime;
        if (distanceToDes > 6f)
        {
            OnDespawn();
        }
    }

    private void UnBullet(int index)
    {
        Pooling.ins.EnQueueObj(index.ToString(), this);
        owner.weapon.SetActive(true);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constain.TAG_ENEMY) || other.CompareTag(Constain.TAG_PLAYER))
        {
            if(other.gameObject != owner.gameObject)
            {
                Character C = Cache.GetCharacter(other);
                C.IsDead = true;
                C.Dead();
                if(owner is Player)
                {
                    owner.CountScaleCharacter();
                }    
                OnDespawn();   
            }    
        }
    }
}
