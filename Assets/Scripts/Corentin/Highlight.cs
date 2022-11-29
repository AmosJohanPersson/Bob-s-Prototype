using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    private Color startcolor;

    private void OnMouseEnter()
    {
        startcolor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.blue;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = startcolor;
    }
}
