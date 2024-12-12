using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFactory : MonoBehaviour
{
    private List<GameObject> used = new List<GameObject>();
    private List<GameObject> free = new List<GameObject>();
    private GameObject arrow;

    public GameObject GetArrow(Vector3 position, Quaternion rotation, string type){
        foreach(GameObject arrow in free){
            if(arrow.tag == type){
                this.arrow = arrow;
                free.Remove(this.arrow);
                used.Add(this.arrow);
                this.arrow.transform.position = position;
                this.arrow.transform.rotation = rotation;
                this.arrow.SetActive(true);
                this.arrow.GetComponent<Rigidbody>().isKinematic = false;
                if(type == "FireArrow"){
                    GameObject boom = arrow.transform.Find("SmallExplosionEffect").gameObject;
                    boom.SetActive(false);
                }
                return arrow;           
            }
        }
        GameObject prefab;
        if(type == "Arrow")
            prefab = Resources.Load<GameObject>("RyuGiKen/Crossbow/Prefabs/Arrow");
        else
            prefab = Resources.Load<GameObject>("Prefabs/FireArrow");
        arrow = Instantiate(prefab, position, rotation);
        used.Add(arrow);
        arrow.GetComponent<Rigidbody>().isKinematic = false;
        if(type == "FireArrow"){
            GameObject boom = arrow.transform.Find("SmallExplosionEffect").gameObject;
            boom.SetActive(false);
        }
        arrow.GetComponent<ArrowController>().trailRenderer.gameObject.SetActive(true);
        return arrow;
    }

    public void FreeArrow(GameObject arrow){
        used.Remove(arrow);
        arrow.SetActive(false);
        arrow.GetComponent<ArrowController>().trailRenderer.Clear();
        free.Add(arrow);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
