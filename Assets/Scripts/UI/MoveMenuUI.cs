using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MoveMenuUI : MonoBehaviour
{
    [SerializeField] Animator home, options;
    [SerializeField] bool isHome, isOptions;
    [SerializeField] float speed;

    AnimationEvent evt;

    private void Start()
    {
        home.speed = speed;
        options.speed = speed;
    }

    public void MovePost()
    {
        evt = new AnimationEvent();

        if (isHome)
        {
            evt.objectReferenceParameter = home;
            home.SetTrigger("Down");
        }
        else if (isOptions)
        {
            evt.objectReferenceParameter = options;
            options.SetTrigger("Down");
        }
        
        evt.time = 1f;
        evt.functionName = "Move";
        
        AnimationClip clip;
        clip = home.runtimeAnimatorController.animationClips[0];
        clip.AddEvent(evt);
    }

    public void Move()
    {
        if (isHome)
        {
            options.SetTrigger("Up");
        }
        else if (isOptions)
        {
            home.SetTrigger("Up");
        }
    }
}
