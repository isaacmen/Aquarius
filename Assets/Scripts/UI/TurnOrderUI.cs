using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderUI : MonoBehaviour {
    public Image[] images;

    private Vector3[] positions;
    private Vector3[] scales;
    private int currentTurn;

    private void Start()
    {
        currentTurn = 1;
        positions = new Vector3[images.Length];
        scales = new Vector3[images.Length];
        for (int i = 0; i < images.Length; i++)
        {
            positions[i] = images[i].rectTransform.position;
            scales[i] = images[i].rectTransform.localScale;
        }
    }

    private void Update()
    {
        //TODO: fix for actual turn change
        if (Input.GetKeyDown(KeyCode.Space))
        {
            updateTurn();
        }
    }

    public void updateTurn()
    {
        for (int i = 0; i < images.Length; i++)
        {
            int nextPositionIndex = (i + currentTurn) % images.Length;
            images[i].rectTransform.position = positions[nextPositionIndex];
            images[i].rectTransform.localScale = scales[nextPositionIndex];
        }
        currentTurn++;
    }
}
