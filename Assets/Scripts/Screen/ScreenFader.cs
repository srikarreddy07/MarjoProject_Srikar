using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float clearAlpha = 0f;
    [SerializeField] float solidAlpha = 1f;
    [SerializeField] float fadeDuration = 1f;

    public float FadeDuration { get => fadeDuration; }

    [Header("UI To Fade")]
    [SerializeField] MaskableGraphic[] graphicsToMask;

    private void SetAlpha(float alpha)
    {
        foreach (MaskableGraphic graphic in graphicsToMask)
        {
            if (graphic != null)
                graphic.canvasRenderer.SetAlpha(alpha);
        }
    }

    private void Fade (float alpha, float duration)
    {
        foreach(MaskableGraphic graphic in graphicsToMask)
        {
            if (graphic != null)
                graphic.CrossFadeAlpha(alpha, duration, true);
        }
    }

    public void Fadeoff()
    {
        SetAlpha(solidAlpha);
        Fade(clearAlpha, fadeDuration);
    }

    public void FadeOn()
    {
        SetAlpha(clearAlpha);
        Fade(solidAlpha, fadeDuration);
    }
}
