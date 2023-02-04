using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class pickup_script : MonoBehaviour
{

    public ItemType item_i_am = ItemType.Platform_Crystal;

    private Vector3 base_position;

    // Start is called before the first frame update
    void Start()
    {
        base_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = base_position + new Vector3(0.0f, Mathf.Sin(Time.time*3.0f)*0.5f, 0.0f);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<FP_Playermovement>().GiveItem(item_i_am);
            Destroy(this.gameObject);
        }
    }

}
