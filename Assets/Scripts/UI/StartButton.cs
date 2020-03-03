using UnityEngine.EventSystems;
using UnityEngine;

public class StartButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        LevelManager.instance.LoadScene(1);
    }
}
