using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _endCanvas;

    private int _currentScene;

    private void Start()
    {
        _currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            if (_endCanvas != null)
            {
                _endCanvas.SetActive(true);
                gameObject.SetActive(false);
            }
            if (_currentScene == SaveManager.instance.playerData.playerLevel)
            {
                SaveManager.instance.playerData.playerLevel++;
                SaveManager.instance.SavePlayerData();
            }
        }
    }

    public void GameStart()
    {
        SaveManager.instance.LoadPlayerData();
        int playerLevel = SaveManager.instance.playerData.playerLevel;
        Debug.Log(playerLevel);

        SceneManager.LoadScene(playerLevel);
    }

    public void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void ReloadCurrentLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
