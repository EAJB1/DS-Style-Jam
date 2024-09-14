using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderTextureSize : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] RawImage rawImage;
    [SerializeField] float aspectRatio;

    void Start()
    {
        aspectRatio = rawImage.texture.texelSize.y / rawImage.texture.texelSize.x;
        rectTransform.sizeDelta = new Vector2(Screen.height * aspectRatio, Screen.height);
    }
}
