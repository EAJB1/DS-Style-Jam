using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMenuUI : MonoBehaviour
{
    [SerializeField] Animator current, next;
    [SerializeField] float speed;

    AnimationEvent evt;

    private void Start()
    {
        current = GetComponent<Animator>();
        current.speed = speed;
    }

    public void MovePost(Animator anim)
    {
        next = anim;
        next.speed = speed;
        
        evt = new AnimationEvent();

        evt.objectReferenceParameter = current;
        current.SetTrigger("Down");
        
        evt.time = 1f;
        evt.functionName = "Move";
        
        AnimationClip clip;
        clip = current.runtimeAnimatorController.animationClips[0];
        clip.AddEvent(evt);
    }

    public void Move()
    {
        next.SetTrigger("Up");
    }
}
