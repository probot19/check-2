using UnityEngine;
using TMPro;
using System.Collections;

public class ComboAnimator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _comboText;

    private Coroutine mRoutine;

    public void ShowCombo(int comboCount)
    {
        gameObject.SetActive(true);
        _comboText.text = $"COMBO x{comboCount}";

        if (mRoutine != null)
            StopCoroutine(mRoutine);

        mRoutine = StartCoroutine(PlayComboAnimation());
    }

    private IEnumerator PlayComboAnimation()
    {
        float durationUp = 0.15f;
        float durationDown = 0.2f;

        Vector3 normalScale = Vector3.one;
        Vector3 bigScale = Vector3.one * 1.6f;

        for (float t = 0; t < 1; t += Time.deltaTime / durationUp)
        {
            float eased = EaseOutBack(t);
            transform.localScale = Vector3.Lerp(normalScale, bigScale, eased);
            yield return null;
        }

        transform.localScale = bigScale;

        for (float t = 0; t < 1; t += Time.deltaTime / durationDown)
        {
            float eased = EaseOutCubic(t);
            transform.localScale = Vector3.Lerp(bigScale, normalScale, eased);
            yield return null;
        }

        transform.localScale = normalScale;
        gameObject.SetActive(false);
    }

    private float EaseOutBack(float t)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1f;
        return 1 + c3 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2);
    }

    private float EaseOutCubic(float t)
    {
        return 1 - Mathf.Pow(1 - t, 3);
    }
}
