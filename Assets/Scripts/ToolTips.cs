using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ToolTips : MonoBehaviour
{
    public GameObject tooltip;
    public Text tooltiptext;

    public void SetText(string text)
    {
        tooltiptext.text = text;
    }
    private void Update()
    {
        Vector2 position = Input.mousePosition;
        transform.position = position;
    }
}
