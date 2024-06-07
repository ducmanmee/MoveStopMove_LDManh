using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    private string currentAnim;
    public Animator animCharacter;
    public List<Animator> animCharacterList;
    public List<GameObject> skinCharacterList;
    public List<Transform> weaponPointList;

    public Transform startTransformPlayer;
    [SerializeField] private Rigidbody characterBody;
    public List<Character> characterInRange = new List<Character>();

    public List<GameObject> weaponCharacter;
    public List<GameObject> hatCharacter;
    public List<Material> pantCharacter;
    public List<GameObject> khienCharacter;

    public Transform attackPoint;
    private Character currentCharacter;
    private float distanceToCharacter;
    private float closestDistance = float.MaxValue;
    public Character nearestCharacter;

    private int weaponCharacterToUse;
    private int pantCharacterToUse;
    private int hatCharacterToUse;
    private int setSkinCharacterToUse;
    private int setKhienCharacterToUse;

    public bool isDead;
    public bool delayAttack;
    public Transform weaponPoint;
    public Transform startAttackPoint;

    public WeaponController bullet;
    public GameObject weapon;
    public SkinnedMeshRenderer pantMaterialCharacter;

    private int countScale = 0;

    //param move
    public Vector3 directionToCharacter;
    Quaternion targetRotation;
    public bool isMoving;
    private float delayAttackTime = .25f;

    //Booster
    public bool weaponBoosted = false; 
    public float boostedWeaponScale = 2f; 
    public float normalWeaponScale = 1f;
    public GameObject boosterVFX;

    public virtual void OnInit()
    {
        isDead = false;
        startAttackPoint = attackPoint;
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
            animCharacter.ResetTrigger(currentAnim);
            currentAnim = animName;
            animCharacter.SetTrigger(currentAnim);
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
        weapon.transform.localScale = skinCharacterList[SetSkinToUse].transform.localScale;
    }

    public void SetupBullet()
    {  
        bullet = Pooling.ins.SpawnFromPool(weaponCharacterToUse.ToString());
        bullet.owner = this;
        bullet.gameObject.transform.position = attackPoint.position;
        bullet.gameObject.transform.rotation = attackPoint.rotation;
        Booster();
        
    }  
    
    public void Booster()
    {
        if(weaponBoosted)
        {
            bullet.gameObject.transform.localScale = skinCharacterList[SetSkinToUse].transform.localScale * boostedWeaponScale;
            weaponBoosted = false;
            BoosterVFX(false);
        }
        else
        {
            bullet.gameObject.transform.localScale = skinCharacterList[SetSkinToUse].transform.localScale * normalWeaponScale;
        }
    }  
    
    public void BoosterVFX(bool active)
    {
        boosterVFX.SetActive(active);
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

    public int SetSkinToUse
    {
        get { return setSkinCharacterToUse; }
        set { setSkinCharacterToUse = value; }
    }

    public int KhienToUse
    {
        get { return setKhienCharacterToUse; }
        set { setKhienCharacterToUse = value; }
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    public void SetPant(int index)
    {
        pantMaterialCharacter.material = pantCharacter[index];
    }

    public void SetHat(int index)
    {
        for (int i = 0; i < hatCharacter.Count; i++)
        {
            if(index == 0)
            {
                hatCharacter[i].SetActive(false);
                continue;
            }
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

    public void SetKhien(int index)
    {
        for (int i = 0; i < khienCharacter.Count; i++)
        {
            if (i == index)
            {
                khienCharacter[i].SetActive(true);
            }
            else
            {
                khienCharacter[i].SetActive(false);
            }
        }
    }   
    
    public void ResetKhien()
    {
        for(int i = 0; i < khienCharacter.Count; i++)
        {
            if (khienCharacter[i].activeSelf)
            {
                khienCharacter[i].SetActive(false); 
            }    
        }    
    }    

    public void SetSkin(int index)
    {
        for (int i = 0; i < skinCharacterList.Count; i++)
        {
            if (i == index)
            {
                skinCharacterList[i].SetActive(true);
                weaponPoint = weaponPointList[i];
                animCharacter = animCharacterList[i];
                animCharacter.ResetTrigger(currentAnim);
                currentAnim = Constain.ANIM_DANCE;
                animCharacter.SetTrigger(currentAnim);
            }
            else
            {
                skinCharacterList[i].SetActive(false);
            }
        }
    }    

    public int CountScale
    {
        get { return countScale; }
        set { countScale = value; }
    }

    public void CountScaleCharacter()
    {
        countScale++;
        if(countScale == 3)
        {
            skinCharacterList[SetSkinToUse].transform.localScale = new Vector3(skinCharacterList[SetSkinToUse].transform.localScale.x + .1f, skinCharacterList[SetSkinToUse].transform.localScale.y + .1f, skinCharacterList[SetSkinToUse].transform.localScale.z +.1f);
            countScale = 0;
            attackPoint = startAttackPoint;
        }    
    }

    public virtual void SetAim(bool active)
    {
        
    }

    public Rigidbody GetRigibody() => characterBody;   
}
