using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcess : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    float duratuion = 1f;
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!spriteRenderer)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
#endif

    private void Start()
    {
        GameManager.Instance.playGrayscale += StartGrayscaleRoutine;
    }
    private void OnDisable()
    {
        GameManager.Instance.playGrayscale -= StartGrayscaleRoutine;
    }
    private IEnumerator GrayscaleRoutine(float duration)
    {
        float time = 0;
        while (duration > time)
        {
            float durationFrame = Time.deltaTime;
            float ratio = time / duration;
            float grayAmount = ratio;
            SetGrayscale(grayAmount);
            time += durationFrame;
            yield return null;
        }
        SetGrayscale(1);
    }

    public void StartGrayscaleRoutine()
    {
        StartCoroutine(GrayscaleRoutine(duratuion));
    }

    void SetGrayscale(float amount = 1)
    {
        spriteRenderer.material.SetFloat("_GrayscaleAmount", amount);
    }
}
