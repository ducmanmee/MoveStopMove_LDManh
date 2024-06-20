using UnityEngine;
using DG.Tweening;

public class WeaponController : MonoBehaviour
{
    public static WeaponController ins;
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
        //if(owner.IsDead) OnDespawn();
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
                WeaponToKillCharacter(C);
            }    
        }
    }
    public void WeaponToKillCharacter(Character C)
    {
        if(C.IsDead) return;
        owner.UpKill();
        C.IsDead = true;
        C.Dead();
        GameManager.ins.CheckWin();
        if (C is Player)
        {
            Player.ins.NameOfKiller = owner.NameOfCharacter;
            Player.ins.RankPlayer = GameManager.ins.GetCharacterAlive();
        }
        if (owner is Player)
        {
            owner.CountScaleCharacter();
        }
        OnDespawn();
    }   

}
