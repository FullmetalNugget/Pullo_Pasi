using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void UpdateScore(int value)
    {
        scoreText.text = " " + value;
    }
}
