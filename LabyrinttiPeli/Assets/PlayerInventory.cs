using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int score = 0;
    public ScoreUI scoreUI;

    public void Add(int amount)
    {
        score += amount;
        scoreUI.UpdateScore(score);
        Debug.Log("Pisteet: " + score);
    }
}
