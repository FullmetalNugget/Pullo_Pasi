using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [Tooltip("Drag every item GameObject in here. Exit opens once they're all destroyed.")]
    [SerializeField] private GameObject[] items;

    [Tooltip("Name of the scene to load when the exit opens.")]
    [SerializeField] private string menuSceneName = "Menu";

    private bool opened = false;

    void Update()
    {
        if (opened) return;

        if (AllItemsCollected())
        {
            opened = true;
            SceneManager.LoadScene(menuSceneName);
        }
    }

    private bool AllItemsCollected()
    {
        foreach (GameObject item in items)
        {
            if (item != null) return false; // still exists, not collected yet
        }
        return true;
    }
}