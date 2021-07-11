using DG.Tweening;
using UnityEngine;
public abstract class OpenOrClosePanelWithFade : MonoBehaviour
{
    [SerializeField] protected CanvasGroup _canvasGroup;
    public virtual void Open()
    {
        _canvasGroup.DOFade(1, 0.3f)
            .OnComplete(() => 
            {
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            });
    }

    public virtual void Close()
    {
        _canvasGroup.DOFade(0, 0.3f)
            .OnStart(() => 
            {
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
            });
    }
}
