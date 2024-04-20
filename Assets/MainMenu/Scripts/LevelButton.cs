using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private GameObject _levelTab;
    [SerializeField] private GameObject _mainCanvas;

    public int _id;

    public void CloseLevelTab()
    {
        _levelTab.SetActive(false);
        _mainCanvas.SetActive(true);
    }

    public void OpenLevelTab()
    {
        _levelTab.SetActive(true);
        _mainCanvas.SetActive(false);
    }
}
