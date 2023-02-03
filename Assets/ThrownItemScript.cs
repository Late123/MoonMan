using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownItemScript : MonoBehaviour
{
    public ItemType item_i_am;


    public GameObject floating_platform_prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        switch(item_i_am)
        {
            case ItemType.Platform_Crystal:
                if (other.gameObject.tag == "Water")
                {
                    GameObject platform = Instantiate(floating_platform_prefab, transform.position, new Quaternion(0.0f, 0.70710682f, 0.707106709f, 0.0f));
                    platform.transform.SetParent(other.transform, true);
                    Destroy(this.gameObject);
                }
                break;
        }
        
    }

}
