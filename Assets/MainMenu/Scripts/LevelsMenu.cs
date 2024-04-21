using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform content;

    private int[] _levels;

    private void Start()
    {
        SaveManager.instance.LoadPlayerData();
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        _levels = new int[sceneCount];

        int playerLevel = SaveManager.instance.playerData.playerLevel;

        for (int i = 0; i < sceneCount; i++)
        {
            if (i != 0)
            {
                _levels[i] = i;
                GameObject buttonGO = Instantiate(buttonPrefab, content);
                LevelButton levelButton = buttonGO.GetComponent<LevelButton>();
                levelButton._id = i;
                buttonGO.GetComponentInChildren<Text>().text = i.ToString();

                // Добавляем обработчик нажатия на кнопку для загрузки сцены
                int sceneIndex = i;
                buttonGO.GetComponent<Button>().onClick.AddListener(() => LoadScene(sceneIndex));

                // Делаем кнопку неактивной, если уровень кнопки больше чем уровень игрока
                buttonGO.GetComponent<Button>().interactable = i <= playerLevel;
            }
        }
    }

    private void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}