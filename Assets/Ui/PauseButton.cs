using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public GameObject screenBtn;
    public Sprite pause, resume;
    public static bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }

    void onClick()
    {
        if (paused)
            ResumeGame();
        else
            PauseGame();

        paused = !paused;
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        gameObject.GetComponent<Image>().sprite = resume;
        screenBtn.GetComponent<TMPro.TextMeshProUGUI>().text = "PAUSED";
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        gameObject.GetComponent<Image>().sprite = pause;
        screenBtn.GetComponent<TMPro.TextMeshProUGUI>().text = "";
    }
}
