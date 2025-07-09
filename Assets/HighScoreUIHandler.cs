using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreUIHandler : MonoBehaviour
{
    [SerializeField]
    private Text highScoreText;

    private void Start()
    {
        var scores = GameManager.Instance.playerScores;
        if (scores == null || scores.Count == 0)
        {
            highScoreText.text = "No High Scores";
            return;
        }

        // Sort scores descending and take top 10
        var topScores = scores.OrderByDescending(s => s.Score).Take(10).ToList();

        string display = "High Scores:\n\n";
        int i = 0;
        foreach (var topScore in topScores)
        {
            display += $"{++i}:\t\t{topScore.Name}\t\t{topScore.Score}\n";
        }

        highScoreText.text = display;
    }

    public void GoBackMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
