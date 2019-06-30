using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Epilogue : MonoBehaviour {

	public Text endingText;
	public Text groupText;
	public string plotWrapUp;
	public string leoEnding1;
	public string libraEnding1;
	public string scorpioEnding1;
    public string leoEnding2;
    public string libraEnding2;
    public string scorpioEnding2;
    public string groupEndingA;
    public string groupEndingB;
    public string groupEndingC;
    public string groupEndingD;

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
    public Image fadeOut;

    private string leoEnding;
    private string libraEnding;
    private string scorpioEnding;
    private string groupEnding;


	// Use this for initialization
	void Start () {
        setEndings();
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
        StartCoroutine(fade());
	}

    private void setEndings()
    {
        int ending = 0;

        if (AbilityCatalog.shieldBash || !AbilityCatalog.layOnHands)
        {
            libraEnding = libraEnding1;
            ending++;
        }
        else
            libraEnding = libraEnding2;

        if (AbilityCatalog.arpeggioPocoAPoco || !AbilityCatalog.crescendo)
        {
            leoEnding = leoEnding1;
            ending++;
        }
        else
            leoEnding = leoEnding2;

        if (AbilityCatalog.lightning || !AbilityCatalog.powerWordKill)
        {
            scorpioEnding = scorpioEnding1;
            ending++;
        }
        else
            scorpioEnding = scorpioEnding2;

        switch (ending)
        {
            case 3:
                groupEnding = groupEndingA;
                break;
            case 2:
                groupEnding = groupEndingB;
                break;
            case 1:
                groupEnding = groupEndingC;
                break;
            case 0:
                groupEnding = groupEndingD;
                break;
            default:
                groupEnding = groupEndingD;
                break;
        }
        print("Endings set");
    }

    IEnumerator fade()
    {
        while (fadeOut.canvasRenderer.GetAlpha() <= 50)
        {
            fadeOut.canvasRenderer.SetAlpha(fadeOut.canvasRenderer.GetAlpha() + 0.3f);
            yield return null;
        }

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Title Screen");
    }

}

