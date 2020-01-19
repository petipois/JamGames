using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public int gameLevel = 1;
    public Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        GetLevel();
    }
    public void LevelLoader(int level)
    {
        level = gameLevel;
        SceneManager.LoadScene(level);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    int GetLevel()
    {
        if (PlayerPrefs.GetInt("LEVEL_REACHED")>0)
        {
            return PlayerPrefs.GetInt("LEVEL_REACHED");
        }
        else
        {
            return 1;   
        }

    }
    private void Update()
    {
        levelText.text = "Best Level: "+GetLevel();
    }
}
