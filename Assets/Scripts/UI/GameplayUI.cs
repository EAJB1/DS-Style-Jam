using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] RectTransform top, bottom;

    [Space]

    [SerializeField] RectTransform acorn;
    [SerializeField] RectTransform lives;
    [SerializeField] RectTransform level;

    [Space]

    [SerializeField] TextMeshProUGUI acornText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Slider subLevelSlider;
    
    [Space]

    [SerializeField] AnimationCurve curve;
    [SerializeField] float sliderLerp;

    float time;
    bool triggeredScale, triggeredLerp;
    Vector2 acornStartSize, livesStartSize, levelStartSize;
    IEnumerator coroutine;

    private void Start()
    {
        acornStartSize = acorn.sizeDelta;
        livesStartSize = lives.sizeDelta;
        levelStartSize = level.sizeDelta;
    }

    public void SetAcornUI(int count, int max)
    {
        acornText.text = count.ToString() + "/" + max.ToString();

        if (count > 0)
        {
            if (triggeredScale)
            {
                StopCoroutine(coroutine);
                coroutine = ScaleUI(acorn.sizeDelta);
                StartCoroutine(coroutine);
                return;
            }

            triggeredScale = true;
            coroutine = ScaleUI(acornStartSize);
            StartCoroutine(coroutine);
        }
    }

    public void SetLivesUI(int count)
    {
        livesText.text = count.ToString();
    }

    public void SetLevelUI(int count)
    {
        levelText.text = count.ToString();
    }

    public void SetSubLevelUI(int current, int max)
    {
        subLevelSlider.maxValue = max;

        triggeredLerp = true;
        StartCoroutine(LerpUI(current));
    }

    IEnumerator LerpUI(int end)
    {
        while (triggeredLerp)
        {
            if (subLevelSlider.value > end)
            {
                triggeredLerp = false;
                subLevelSlider.value = end;
                break;
            }

            subLevelSlider.value += sliderLerp * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator ScaleUI(Vector2 startSize)
    {
        acorn.sizeDelta = startSize;
        time = 0f;

        while (triggeredScale)
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
                triggeredScale = false;
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
