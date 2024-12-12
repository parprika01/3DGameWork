using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public Animator anim{ get; private set; }
    public AudioSource audioSource;
    private bool animEnded = false;
    // private SceneController sceneController;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)){
            anim.SetBool("Fire",true);
        } else {
            anim.SetBool("Fire",false);
        }

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        // 判断当前动画是否已播放完毕
        if (!animEnded && stateInfo.IsName("Shoot") && stateInfo.normalizedTime >= 1.0f)
        {
            animEnded = true; 
            // 射箭
            if(!gameManager.currentGame.IsArrowRunOut()){
                gameManager.currentGame.UpdateArrowNum();
                GameObject arrow = gameManager.currentGame.arrowFactory.GetArrow(transform.position, transform.rotation, gameManager.currentGame.ArrowType);
                audioSource.Play();
                arrow.GetComponent<ArrowController>().Shoot();                
            } 
        }

        if (stateInfo.normalizedTime < 1.0f && animEnded)
        {
            animEnded = false; // 重置为未完成，等待下一次发射
        }
    }
}
