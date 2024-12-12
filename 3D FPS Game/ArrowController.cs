using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
public delegate void CollisionHandler(Collision other);
public class ArrowController : MonoBehaviour
{
    private float speed;
    private bool hasCollided = false;
    private GameManager gameManager;
    private AudioSource audioSource;
    public string Type;
    public TrailRenderer trailRenderer;
    // Start is called before the first frame update

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(){
        // 获取变量
        Crossbow crossbow= FindObjectOfType<Crossbow>();
        speed = 50f;
        // 计算方向
        Quaternion bowRotation = crossbow.transform.rotation;
        Vector3 direction = bowRotation * Vector3.forward;

        // 设置速度
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter(Collision other) {
        // 发生碰撞
        if (!hasCollided){
            // 取消速度
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            // 回收bow
            StartCoroutine(RecycleArrow());

            hasCollided = true;
            if(other.gameObject.tag == "Ring" || other.gameObject.name == "animal"){
                //OnCollision?.Invoke(other);
                audioSource.Play();
                Vector3 hitPoint = other.contacts[0].point;
                if (Type == "Arrow"){
                    // NormalArrow
                    Vector3 up = other.gameObject.transform.up;
                    Target target = FindTarget(other.gameObject.transform);
                    gameManager.currentGame.uiController.ShowHitUI(other.gameObject.name, hitPoint, up, target.transform.parent.rotation);
                    int Score = gameManager.currentGame.scoreController.UpdateScore(other.gameObject); 
                    gameManager.currentGame.uiController.SetScore(Score);
                    target.HitDown();              
                } else {
                    // FireArrow
                    // 爆炸
                    transform.Find("SmallExplosionEffect").gameObject.SetActive(true);
                    Collider[] colliders = Physics.OverlapSphere(hitPoint, 3f);
                    HashSet<Target> targets = new HashSet<Target>();
                    foreach(Collider collider in colliders){
                        if(collider.gameObject.tag == "Ring" || collider.gameObject.name == "animal")
                            targets.Add(FindTarget(collider.gameObject.transform));
                    }
                    
                    foreach(Target target in targets){
                        Vector3 up = target.gameObject.transform.forward + transform.gameObject.transform.rotation.eulerAngles;
                        // UI
                        gameManager.currentGame.uiController.ShowHitUI("Boom", target.gameObject.transform.position, up, target.gameObject.transform.parent.rotation);

                        // Score
                        int Score = gameManager.currentGame.scoreController.UpdateScore(null);
                        gameManager.currentGame.uiController.SetScore(Score);

                        // Target
                        target.HitDown();
                    }
                                    
                }  
            } 
        }
    }

    private void OnCollisionExit(Collision other) {
        hasCollided = false;
    }

    private IEnumerator RecycleArrow() {
        yield return new WaitForSeconds(1f);
        gameManager.currentGame.arrowFactory.FreeArrow(gameObject);
    }

    private Target FindTarget(Transform ring){
        while(ring.gameObject.GetComponent<Target>() == null){
            ring = ring.parent;
        }
        return ring.gameObject.GetComponent<Target>();
    }

}
