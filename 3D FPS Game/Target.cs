using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    private bool IsHit;
    public string type;
    public float startTime;
    public string action;
    private Vector3 IniPosition;
    private Quaternion IniRotation;
    // Start is called before the first frame update
    private Animator animator;
    private RuntimeAnimatorController animatorController;
    void Start()
    {
        switch(action){
            case "LeftMove":
                animatorController = Resources.Load<RuntimeAnimatorController>("Animation/LeftMoveAnimator");
                break;
            case "RightMove":
                animatorController = Resources.Load<RuntimeAnimatorController>("Animation/RightMoveAnimator");
                break;
            case "Stand":
                animatorController = Resources.Load<RuntimeAnimatorController>("Animation/StandAnimator");
                break;
            case "CircleMove":
                animatorController = Resources.Load<RuntimeAnimatorController>("Animation/CircleMoveAnimator");
                break ;
            case "Still":
                animatorController = Resources.Load<RuntimeAnimatorController>("Animation/StillAnimator");
                break;
            default:
                break;
        }

        IniPosition = transform.position;
        IniRotation = transform.rotation;
        if(type == "motivate"){
            if (gameObject.GetComponent<Animator>() == null){
                animator = gameObject.AddComponent<Animator>();
                animator.runtimeAnimatorController = animatorController;
                animator.applyRootMotion = true;                
            }
            animator.speed = 0;
            Invoke("StartAnimation", startTime);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(){
        CancelInvoke();
        gameObject.transform.position = IniPosition;
        gameObject.transform.rotation = IniRotation;
        animator.speed = 0f;
        if(type == "motivate"){
            IsHit = false;
            Invoke("Up", startTime);            
        }

    }

    // 被击倒时的动画控制
    public void HitDown()
    {
        if(type == "motivate" && !IsHit){
            animator.SetTrigger("Hit");
            IsHit = true;
        }

    }

    public void Up(){
        animator.speed = 1f;
        animator.SetTrigger("Up");
    }

    public void StartAnimation()
    {
        animator.speed = 1f;
    }
    
}
