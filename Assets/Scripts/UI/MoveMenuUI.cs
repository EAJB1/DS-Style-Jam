using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMenuUI : MonoBehaviour
{
    [SerializeField] Animator menu1, menu2;
    [SerializeField] bool isMenu1, isMenu2;
    [SerializeField] float speed;

    AnimationEvent evt;

    private void Start()
    {
        menu1.speed = speed;
        menu2.speed = speed;
    }

    public void MovePost()
    {
        evt = new AnimationEvent();

        if (isMenu1)
        {
            evt.objectReferenceParameter = menu1;
            menu1.SetTrigger("Down");
        }
        else if (isMenu2)
        {
            evt.objectReferenceParameter = menu2;
            menu2.SetTrigger("Down");
        }
        
        evt.time = 1f;
        evt.functionName = "Move";
        
        AnimationClip clip;
        clip = menu1.runtimeAnimatorController.animationClips[0];
        clip.AddEvent(evt);
    }

    public void Move()
    {
        if (isMenu1)
        {
            menu2.SetTrigger("Up");
        }
        else if (isMenu2)
        {
            menu1.SetTrigger("Up");
        }
    }
}
