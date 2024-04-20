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

    // Start is called before the first frame update
    void Start()
    {

    }

    public virtual void OnInit()
    {
        
    }

    public virtual void OnDespawn()
    {

    }

    public virtual void Movement()
    {

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

    public Rigidbody GetRigibody() => characterBody;
}
