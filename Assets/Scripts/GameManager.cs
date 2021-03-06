using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [HideInInspector]
    public static bool GameOver;
	public GameObject gameOverUI;
    private bool movingFast = false;
    public static bool showHealthBar = false;
    private AudioSource audioSourceComponent;

    public Camera mainCamera;
    public Sprite mutedVolumeSprite;
    public Sprite volumeOnSprite;
    public Button volumeButton;


    private void Awake()
    {
        
        audioSourceComponent = mainCamera.GetComponent<AudioSource>();
        if (PlayerPrefs.GetFloat("volume", .1f) == 0f)
        {
            ToggleAudio();
        }
    }


    void Start()
    {
        //LoadData();
        GameOver = false;
        Time.timeScale = 1;
        movingFast = false;
    }
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            EndGame();
        }
        if (GameOver) {
			return;
		}
        if (Input.GetKeyDown(KeyCode.H))
        {
            showHealthBar = !showHealthBar;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleAudio();
        }
        
		if (PlayerStats.Lives <= 0) {
			EndGame ();
		}
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            SetGameSpeed();
        }

	}
	void EndGame()
	{
        Time.timeScale = 0;
        SaveData();
        GameOver = true;
		gameOverUI.SetActive(true);
	}
    public void SetGameSpeed()
    {
        if (movingFast)
        {
            Time.timeScale = 1;
            movingFast = !movingFast;
        }
        else
        {
            Time.timeScale = 4;
            movingFast = !movingFast;
        }
    }

    public void ToggleAudio()
    {
        if(audioSourceComponent.volume == 0f)
        {
            PlayerPrefs.SetFloat("volume", .1f);
            audioSourceComponent.volume = .1f;
            volumeButton.GetComponent<Image>().sprite = volumeOnSprite;
        }
        else
        {
            PlayerPrefs.SetFloat("volume", 0f);
            audioSourceComponent.volume = 0f;
            volumeButton.GetComponent<Image>().sprite = mutedVolumeSprite;
        }

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SaveData()
    {
        SaveSystem.SavePlayerStats(this.GetComponentInParent<PlayerStats>());
    }

    public void LoadData()
    {
        SaveData data = SaveSystem.LoadPlayer();
        PlayerStats.highestEnemies = data.enemiesKilled;
        PlayerStats.highestRounds = data.rounds;
        if (data != null)
        {
            Debug.Log("Max Enemies Killed " + data.enemiesKilled);
            Debug.Log("Max Rounds Survived " + data.rounds);
        }
    }
}
