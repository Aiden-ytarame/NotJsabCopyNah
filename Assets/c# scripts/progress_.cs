using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progress_ : MonoBehaviour
{
    public float value;
    public float musicoffset = 0;
    float alpha = 1;
    private void Update()
    {
        if (gameObject.transform.localPosition.x < 2350)

        {
            gameObject.transform.localPosition = new Vector3((GameObject.Find("levelPlayer").GetComponent<AudioSource>().time - musicoffset) * value, 0);
        }
        else
        {
            if (transform.parent.GetComponentInParent<Image>().color.a > 0)
            {
                foreach (Image ii in transform.parent.parent.parent.GetComponentsInChildren<Image>())
                    ii.color = new Color(ii.color.r, ii.color.g, ii.color.b, alpha);
                    gameObject.GetComponent<RawImage>().color = transform.parent.GetComponentInParent<Image>().color;

                alpha -= 0.7f * Time.deltaTime;
            }
        }
    }
}
