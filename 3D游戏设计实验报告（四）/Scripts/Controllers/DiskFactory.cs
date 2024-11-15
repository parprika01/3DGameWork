using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DiskFactory : MonoBehaviour
{
    public GameObject diskPrefab;
    private List<DiskData> used = new List<DiskData>();
    private List<DiskData> free = new List<DiskData>();
    // private FirstController sceneController;
    private DataManager dataManager;

    public GameObject GetDisk (int round) {
        int choice;
        int scope1 = 1, scope2 = 5, scope3 = 8, scope4 = 10;                                       
        string tag;
        diskPrefab = null;
        choice = Random.Range(0, scope4);

        if (choice <= scope1) {
            tag = "Apple";
        }
        else if (choice <= scope2 && choice > scope1) {
            tag = "Orange";
        }
        else if (choice <= scope3 && choice > scope2){
            tag = "Tomato";
        } else {
            tag = "Ribs";
        }

        for(int i=0;i<free.Count;i++) {
            if(free[i].gameObject.CompareTag(tag)) {
                diskPrefab = free[i].gameObject;
                free[i].gameObject.SetActive(true);
                free.Remove(free[i]);
                break;
            }
        }
        if (diskPrefab == null) {
            GameObject prefab =  Resources.Load("Prefabs/" + tag, typeof(GameObject)) as GameObject;
            diskPrefab = Instantiate(prefab,new Vector3(0, 0, 0), prefab.transform.rotation);
        }
        float ran_x = Random.Range(-1f, 1f) < 0 ? -1 : 1;
        used.Add(diskPrefab.GetComponent<DiskData>());
        
        // 随机生成位置
        float Xedge = 5;
        float Yedge = 3.5f;
        
        choice = Random.Range(1, 4);
        if (choice == 1) {
            diskPrefab.transform.position = new Vector3(-Xedge, Random.Range(0, Yedge / 2f), -1);
            diskPrefab.GetComponent<DiskData>().angle = Random.Range(-30, 60);
        } else if (choice == 2) {
            diskPrefab.transform.position = new Vector3(Random.Range(-Xedge / 3f, Xedge / 3f), Yedge, -1);
            diskPrefab.GetComponent<DiskData>().angle = Random.Range(-135, -45);
        } else {
            diskPrefab.transform.position = new Vector3(Xedge, Random.Range(0, Yedge / 2f), -1);
            diskPrefab.GetComponent<DiskData>().angle = Random.Range(-225, -135);
        }
        return diskPrefab;
    }

    private void IniDisk (GameObject disk) {
        if (disk.GetComponent<Rigidbody>() != null) {
            Destroy(disk.GetComponent<Rigidbody>());
        }
    }

    public void FreeDisk (GameObject disk) {
        for (int i = 0; i < used.Count; i++) {
            if (used[i].gameObject == disk) {
                IniDisk(used[i].gameObject);
                used[i].gameObject.SetActive(false);
        
                free.Add(used[i]);
                used.Remove(used[i]);
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        FirstController sceneController = (FirstController)SSDirector.getInstance ().currentSceneController;
        sceneController.diskFactory = this;
        dataManager = sceneController.dataManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
