using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPWarning : MonoBehaviour
{
    [SerializeField]
    private Sprite yellow;
    [SerializeField]
    private Sprite red;

    [SerializeField]
    private float halfLine;
    [SerializeField]
    private float dyingLine;

    private Image image;
    private float elapsedTime = 0;
    [SerializeField]
    private float speed = 1;

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = yellow;
        image.enabled = false;
    }

    void FixedUpdate()
    {
        if (PreLoad.Scripts.HPController.HpRatio < halfLine)
        {
            if (image.enabled == false) image.enabled = true;

            if (PreLoad.Scripts.HPController.HpRatio < dyingLine && image.sprite != red)
            {
                elapsedTime = 0;
                image.sprite = red;
            }

            elapsedTime += Time.deltaTime;
            image.color = new Color(1, 1, 1, Mathf.Abs(Mathf.Sin(elapsedTime * speed)));
        }
    }
}
