using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Epilogue : MonoBehaviour {

	public Text endingText;
	public Text groupText;
	public string plotWrapUp;
	public string leoEnding;
	public string libraEnding;
	public string scorpioEnding;
	public string groupEnding;

	public Image Portrait;
	public Image Sprite;
	public Image Frame;
	public GameObject GroupSprite;
	public Sprite LeoPortrait;
	public Sprite LeoSprite;
	public Sprite LibraPortrait;
	public Sprite LibraSprite;
	public Sprite ScorpioPortrait;
	public Sprite ScorpioSprite;


	// Use this for initialization
	void Start () {
		StartCoroutine (WriteSentence ());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Q)) {
			Debug.Log ("pressedt");
			Application.Quit ();
		}
	}

	IEnumerator WriteSentence ()
	{
		endingText.text = "";
		foreach (char letter in plotWrapUp.ToCharArray())
		{
			endingText.text += letter;
			yield return null;
		}

		yield return new WaitForSeconds (1);

		Portrait.sprite = LeoPortrait;
		Sprite.sprite = LeoSprite;
		endingText.text = "";
		foreach (char letter in leoEnding.ToCharArray())
		{
			endingText.text += letter;
			yield return null;
		}

		yield return new WaitForSeconds (3);

		Portrait.sprite = LibraPortrait;
		Sprite.sprite = LibraSprite;
		endingText.text = "";
		foreach (char letter in libraEnding.ToCharArray())
		{
			endingText.text += letter;
			yield return null;
		}

		yield return new WaitForSeconds (3);

		Portrait.sprite = ScorpioPortrait;
		Sprite.sprite = ScorpioSprite;
		endingText.text = "";
		foreach (char letter in scorpioEnding.ToCharArray())
		{
			endingText.text += letter;
			yield return null;
		}

		yield return new WaitForSeconds (3);

		GroupSprite.SetActive (true);
		groupText.gameObject.SetActive (true);
		Sprite.sprite = LeoSprite;

		endingText.gameObject.SetActive (false);
		Portrait.gameObject.SetActive (false);
		Frame.gameObject.SetActive (false);

		groupText.text = "";
		foreach (char letter in groupEnding.ToCharArray())
		{
			groupText.text += letter;
			yield return null;
		}

		yield return new WaitForSeconds (3);

		StopAllCoroutines();
	}

}

