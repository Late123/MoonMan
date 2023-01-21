using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal_zone_script : MonoBehaviour
{
	public GameObject win_text;
	private bool has_won = false;
    // Start is called before the first frame update
    void Start()
    {
        has_won = false;
		win_text.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
		//UnityEngine.Debug.Log("You might win!!!");
		if (has_won) return;

        if (other.gameObject.tag == "Player") {
			has_won = true;
			win_text.SetActive(true);
			UnityEngine.Debug.Log("You win!!!");
		}
    }
}
