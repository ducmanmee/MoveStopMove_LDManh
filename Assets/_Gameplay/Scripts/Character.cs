using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    public List<GameObject> hatCharacter;
    public List<Material> pantCharacter;

    [SerializeField] private Transform attackPoint;
    private Character currentCharacter;
    private float distanceToCharacter;
    private float closestDistance = float.MaxValue;
    public Character nearestCharacter;

    private int weaponCharacterToUse;
    private int pantCharacterToUse;
    private int hatCharacterToUse;

    public bool isDead;
    public bool delayAttack;
    public Transform weaponPoint;

    public WeaponController bullet;
    public GameObject weapon;
    public SkinnedMeshRenderer pantMaterialCharacte;



    //param move
    public Vector3 directionToCharacter;
    Quaternion targetRotation;
    public bool isMoving;
    private float delayAttackTime = .25f;

    public virtual void OnInit()
    {
        isDead = false;
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
        if (delayAttack || isDead) return;
        delayAttack = true;
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
            SetupBullet();
        }
        yield return new WaitForSeconds(delayAttackTime); 
        delayAttack = false;
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

    public void SetupWeapon()
    {      
        if(weapon != null)
        {
            Destroy(weapon);
        }    
        weapon = Instantiate(weaponCharacter[weaponCharacterToUse]);
        weapon.transform.parent = weaponPoint.transform;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }
    
    public void SetupBullet()
    {  
        bullet = Pooling.ins.SpawnFromPool(weaponCharacterToUse.ToString());
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

    public virtual void Dead()
    {
        ClearCharacterInRange();
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

    public void ClearCharacterInRange()
    {
        characterInRange.Clear();
    }    

    public int WeaponToUse
    {
        get { return weaponCharacterToUse; }
        set { weaponCharacterToUse = value; }
    }

    public int PantToUse
    {
        get { return pantCharacterToUse; }
        set { pantCharacterToUse = value; }
    }

    public int HatToUse
    {
        get { return hatCharacterToUse; }
        set { hatCharacterToUse = value; }
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    public void SetPant(Material materialPant)
    {
        pantMaterialCharacte.material = materialPant;
    }

    public void SetHat(int index)
    {
        for (int i = 0; i < hatCharacter.Count; i++)
        {
            if (i == index)
            {
                hatCharacter[i].SetActive(true);
            }
            else
            {
                hatCharacter[i].SetActive(false);
            }
        }
    }   
    

    public virtual void SetAim(bool active)
    {
        
    }

    public Rigidbody GetRigibody() => characterBody;   
}
