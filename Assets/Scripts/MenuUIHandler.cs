using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField]
    private Text MainMenuText;
    [SerializeField]
    private InputField playerNameInputField;

    public void Start()
    {
        UpdateText();
        playerNameInputField.onValueChanged.AddListener(UpdateCurrPlayer);
    }

    private void UpdateCurrPlayer(string value)
    {
        playerNameInputField.image.color = Color.white;
        GameManager.Instance.currPlayerName = value;
    }
    public void UpdateText()
    {
        if (GameManager.Instance.PlayerHighScoreName != null)
        {
            MainMenuText.text = "Best Score: " +
            GameManager.Instance.PlayerHighScoreName +
            " : " +
            GameManager.Instance.PlayerHighScore;
        }
    }

    public void StartGame()
    {
        if (!string.IsNullOrEmpty(GameManager.Instance.currPlayerName))
        {
            playerNameInputField.image.color = Color.white;
            SceneManager.LoadScene(1);
        }
        else
        {
            playerNameInputField.image.color = Color.red;
        }
    }

    public void Exit()
    {
        //GameManager.Instance.SavePlayerData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
