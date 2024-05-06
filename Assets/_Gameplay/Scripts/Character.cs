using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    private string currentAnim;
    [SerializeField] private Animator anim;
    public SkinnedMeshRenderer rendererCharacter;
    [SerializeField] private Rigidbody characterBody;
    public List<Character> characterInRange = new List<Character>();
    public List<GameObject> weaponCharacter;
    [SerializeField] private Transform attackPoint;
    private Character currentCharacter;
    private float distanceToCharacter;
    private float closestDistance = float.MaxValue;
    public Character nearestCharacter;
    public bool isDead;
    public Transform weaponPoint;

    public WeaponController bullet;
    public GameObject weapon;

    //param move
    public Vector3 directionToCharacter;
    Quaternion targetRotation;
    public bool isMoving;

    public virtual void OnInit()
    {
        characterInRange.Clear();
        isDead = false;
        SetupWeapon(1);
    }

    public virtual void OnDespawn()
    {

    }  

    public virtual void Moving()
    {

    }

    public virtual void StopMoving()
    {
        
    }

    public virtual void Attack()
    {
        if (!weapon.activeSelf)
        {
            weapon.SetActive(true);
        }
        GetDirectionToCharacter(nearestCharacter);
        ChangeAnim(Constain.ANIM_ATTACK);
        StartCoroutine(ThrowBullet());
        
    }  

    IEnumerator ThrowBullet()
    {
        yield return new WaitForSeconds(.25f);
        if (!isMoving)
        {
            weapon.SetActive(false);
            SetupBullet(1);
        }
    }    

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    public virtual void SetupWeapon(int index)
    {
        weapon = Instantiate(weaponCharacter[index]);
        weapon.transform.parent = weaponPoint.transform;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }
    
    public virtual void SetupBullet(int index)
    {
        bullet = Pooling.ins.SpawnFromPool(index.ToString());
        bullet.owner = this;
        bullet.gameObject.transform.position = attackPoint.position;
        bullet.gameObject.transform.rotation = attackPoint.rotation;
    }    

    public virtual void NearestEnemy()
    {
        if(characterInRange.Count  < 1)
        {
            nearestCharacter = null;
            return;
        } 
            

        CheckCharacterDeadInRange();
        if (characterInRange.Count == 1)
        {
            nearestCharacter = characterInRange[0];
            if(this is Player)
            {
                nearestCharacter.SetAim(true);
            }    
        }    
        else if (characterInRange.Count > 1)
        {
            closestDistance = float.MaxValue;
            for (int i = 0; i < characterInRange.Count; i++)
            {
                Character character = characterInRange[i];
                distanceToCharacter = Vector3.Distance(this.transform.position, character.transform.position);
                if (distanceToCharacter < closestDistance)
                {
                    closestDistance = distanceToCharacter;
                    nearestCharacter = character;
                }
            }

            if (currentCharacter != nearestCharacter)
            {
                currentCharacter?.SetAim(false);
                currentCharacter = nearestCharacter;
                if (this is Player)
                {
                    nearestCharacter.SetAim(true);
                }
            }
        }
        else
        {
            nearestCharacter = null;
            currentCharacter = null;
        }
    }


    public void GetDirectionToCharacter(Character C)
    {
        directionToCharacter = C.transform.position - transform.position;
        directionToCharacter.y = 0f;
        targetRotation = Quaternion.LookRotation(directionToCharacter);
        transform.rotation = targetRotation;
    }

    public void AddCharacterInRange(Character C)
    {
        characterInRange.Add(C);
    }

    public void RemoveCharacterInRange(Character C)
    {
        characterInRange.Remove(C);
    }

    private void CheckCharacterDeadInRange()
    {
        for (int i = characterInRange.Count - 1; i >= 0; i--)
        {
            Character character = characterInRange[i];
            if (character.isDead)
            {
                RemoveCharacterInRange(character);
            }
        }
    }    

    public virtual void SetAim(bool active)
    {
        
    }

    public Rigidbody GetRigibody() => characterBody;   
}
