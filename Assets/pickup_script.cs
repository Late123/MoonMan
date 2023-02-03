using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class pickup_script : MonoBehaviour
{

    public ItemType item_i_am = ItemType.Platform_Crystal;

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
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<FP_Playermovement>().GiveItem(item_i_am);
        }
    }

}
