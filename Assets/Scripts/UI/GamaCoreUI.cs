using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamaCoreUI : MonoBehaviour
{
    public Transform target;
    public TextMeshProUGUI yText;

    private float targetY;

    void Update()
    {
        if (target != null)
        {
            targetY = target.position.y;
            yText.text = targetY.ToString("F2");
        }
    }
}