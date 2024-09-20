using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;

    [Space]

    [SerializeField] RectTransform acorn, lives;
    [SerializeField] TextMeshProUGUI acornText, livesText;

    float time;
    bool triggered;
    Vector2 acornStartSize, livesStartSize;
    IEnumerator coroutine;

    private void Start()
    {
        acornStartSize = acorn.sizeDelta;
        livesStartSize = lives.sizeDelta;
    }

    public void SetAcornUI(int count, int max)
    {
        acornText.text = count.ToString() + "/" + max.ToString();

        if (count > 0)
        {
            if (triggered)
            {
                StopCoroutine(coroutine);
                coroutine = ScaleUI(acorn.sizeDelta);
                StartCoroutine(coroutine);
                return;
            }

            triggered = true;
            coroutine = ScaleUI(acornStartSize);
            StartCoroutine(coroutine);
        }
    }

    public void SetLivesUI(int count)
    {
        livesText.text = count.ToString();
    }

    IEnumerator ScaleUI(Vector2 startSize)
    {
        acorn.sizeDelta = startSize;
        time = 0f;

        while (triggered)
        {
            time += Time.deltaTime;
            
            if (time > 1f)
            {
                while (acorn.sizeDelta.magnitude > acornStartSize.magnitude)
                {
                    acorn.sizeDelta -= Vector2.one * 0.01f;
                    yield return null;
                    continue;
                }
                
                acorn.sizeDelta = acornStartSize;
                time = 0f;
                triggered = false;
                break;
            }

            acorn.sizeDelta += Vector2.one * curve.Evaluate(time);
            
            if (acorn.sizeDelta.magnitude < acornStartSize.magnitude)
            {
                acorn.sizeDelta = acornStartSize;
            }

            yield return null;
        }
    }
}
