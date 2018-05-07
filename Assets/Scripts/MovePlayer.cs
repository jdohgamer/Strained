using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class MovePlayer : MonoBehaviour {

    public KeyCode moveL;
    public KeyCode moveR;

    public float horizVel = 0;
    public int laneNum = 2;
    public string controlLocked = "n";


	// Use this for initialization
	void Start () {
        Renderer rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().velocity = new Vector3(horizVel, 0, 4);

        if(Input.GetKeyDown (moveL) && (laneNum > 1) && (controlLocked == "n"))
        {
            horizVel = -2;
            StartCoroutine(stopSlide());
            laneNum -= 1;
            controlLocked = "y";
        }
        if (Input.GetKeyDown(moveR) && (laneNum < 3) && (controlLocked == "n"))
        {
            horizVel = 2;
            StartCoroutine(stopSlide());
            laneNum += 1;
            controlLocked = "y";
        }
	}

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.tag == "Lethal")
        {
            Destroy(gameObject);
            GM.zVelAdj = 0;
        }
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            GM.coinTotal += 1;
        }
        if (collision.gameObject.tag == "PowerUp")
        {
            Destroy(collision.gameObject);
            
        }
	}

	IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(.5f);
        horizVel = 0;
        controlLocked = "n";
    }
}
