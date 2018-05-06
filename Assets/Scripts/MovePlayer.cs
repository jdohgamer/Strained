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


    public string[] keywords = new string[] { "fly", "pony", "left", "right" };
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    protected PhraseRecognizer recognizer;
    protected string word = "right";


	// Use this for initialization
	void Start () {
        if (keywords != null)
        {
            recognizer = new KeywordRecognizer(keywords, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
        }
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

        //switch (word)
        //{
        //    case "left":
        //        horizVel = -2;
        //        StartCoroutine(stopSlide());
        //        laneNum -= 1;
        //        controlLocked = "y";
        //        break;
        //    case "right":
        //        horizVel = -2;
        //        StartCoroutine(stopSlide());
        //        laneNum += 1;
        //        controlLocked = "y";
        //        break;
        //}
	}

    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(.5f);
        horizVel = 0;
        controlLocked = "n";
    }

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        //results.text = "You said: <b>" + word + "</b>";
    }
}
