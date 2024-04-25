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
    [SerializeField] private Transform attackPoint;
    private Character currentCharacter;
    private float distanceToCharacter;
    private float closestDistance = float.MaxValue;
    public Character nearestCharacter;

    //param move
    public Vector3 directionToCharacter;
    Quaternion targetRotation;

    void Start()
    {

    }

    public virtual void OnInit()
    {
        ChangeAnim(Constain.ANIM_IDLE);
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
        GetDirectionToCharacter(nearestCharacter);
        ChangeAnim(Constain.ANIM_ATTACK);
        GameObject W = Pooling.ins.SpawnFromPool("0");
        W.transform.position = attackPoint.position;  
        W.transform.rotation = transform.rotation;
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

    public virtual void NearestEnemy()
    {
        if (characterInRange.Count > 0)
        {
            closestDistance = float.MaxValue;
            foreach (Character character in characterInRange)
            {
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
                nearestCharacter.SetAim(true);
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

    public void AddCharacterInRange(Character character)
    {
        characterInRange.Add(character);
    }

    public void RemoveCharacterInRange(Character character)
    {
        characterInRange.Remove(character);
    }

    public virtual void SetAim(bool active)
    {
        
    }

    public Rigidbody GetRigibody() => characterBody;

}
