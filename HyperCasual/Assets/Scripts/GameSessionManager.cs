using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSessionManager : MonoBehaviour
{
    public static GameSessionManager instance;
    public string menuLevel = "Menu";
    public Text timeText, levelText, survivalText,messageText;
    public GameObject player, pauseMenu, overMenu;
    public Animator anim;
    int currentLevel ,damageLevel = 1, nextLoadLevel;
    public float gameTime = 15, secondsToNextLevel = 2f;
    bool isGameOver = false;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        anim = player.GetComponentInChildren<Animator>();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        nextLoadLevel = currentLevel + 1;
    }
    void TurnOffMenus(GameObject panel)
    {

        panel.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        GameStarters();
    }
    void GameStarters()
    {
        TurnOffMenus(overMenu);
        TurnOffMenus(pauseMenu);
    }
    void SaveLevel()
    {
        PlayerPrefs.SetInt("LEVEL_REACHED", currentLevel);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        SceneManager.LoadScene(menuLevel);
    }
    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
            return;
        if (gameTime > 0)
        {
            survivalText.text = Mathf.RoundToInt(gameTime).ToString();
            gameTime -= Time.deltaTime;

        } else
        {
            SaveLevel();
            StartCoroutine(NextLevel());
        }

        levelText.text = "Lvl " + currentLevel;

    }
    public void HitPlayer()
    {
        anim.SetInteger("damage", damageLevel);
        damageLevel++;
        if (damageLevel > 5)
        {

            anim.SetBool("destroyed", true);

            GameOver();
        }
    }
  
    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(secondsToNextLevel);
        if(nextLoadLevel >= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(menuLevel);

        }
        else
        {
            SceneManager.LoadScene(nextLoadLevel);
        }


    }
    public void GameOver()
    {
        isGameOver = true;
        //show game over screen
        Time.timeScale = 0;
        SaveLevel();
        overMenu.SetActive(true);
    }
}
