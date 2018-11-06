using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIonGameObject : MonoBehaviour {
    public Camera cam;
    public GameObject character;
    public Vector3 offset;

    private Vector3 position;

    private void Update()
    {
        position = character.transform.position;
        Vector3 charaScreenPosition = cam.WorldToScreenPoint(position);
        transform.position = charaScreenPosition + offset;
    }
}
