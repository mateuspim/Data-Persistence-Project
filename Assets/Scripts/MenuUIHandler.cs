using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;



#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField]
    private Text MainMenuText;

    public void Start()
    {
        UpdateText();
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
        SceneManager.LoadScene(1);
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
