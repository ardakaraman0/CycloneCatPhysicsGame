using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using System.Linq;

public class LevelBase : MonoBehaviour
{

    [SerializeField]
    int levelIndex = 0;

    [SerializeField]
    GameObject levelPrefab;
    [SerializeField]
    GameObject currentLevel;

    [SerializeField]
    float pointsCap = 1000f;
    [SerializeField]
    float playerPoints = 0;

    [SerializeField]
    int timeLeft = 15 * 60;

    [SerializeField]
    Image _progressBar;
    [SerializeField]
    TMP_Text _timerText;

    [SerializeField]
    GameObject inGameUI;
    [SerializeField]
    GameObject winUI;
    [SerializeField]
    GameObject loseUI;

    [SerializeField]
    CinemachineInputProvider _inputProvider;
    [SerializeField]
    CinemachineBrain _mainCamera;
    [SerializeField]
    CinemachineFreeLook _playerCam;
    [SerializeField]
    CinemachineVirtualCamera _shooterCam;

    [SerializeField]
    Transform breakableParent;
    [SerializeField]
    List<Transform> breakables;
    int breakableLeft;
    float initialCount;


    bool end = false;
    void Start()
    {
        //breakables = breakableParent.GetComponentsInChildren<Transform>().ToList();
        breakableLeft = breakables.Count;
        initialCount = breakableLeft;

        _progressBar.fillAmount = 0f;
        StartCoroutine(TimeLeftCounter());
    }

    void FixedUpdate()
    {
        UpdateProgressBar(breakables.Count(t => t.CompareTag("Dead")));
    }

    private void Update()
    {
        if (end)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                NextLevel();
            }
        }
    }

    WaitForSeconds oneSecTimer = new WaitForSeconds(1f);
    int minutes = 14;
    int seconds = 59;
    IEnumerator TimeLeftCounter()
    {
        _timerText.text = minutes.ToString() + ":" + seconds.ToString();
        while(timeLeft >= 0)
        {
            if (seconds <= 0)
            {
                minutes--;
                seconds = 59;
                _timerText.text = minutes.ToString() + ":" + seconds.ToString();
            } else
            {
                _timerText.text = minutes.ToString() + ":" + seconds.ToString();
            }
            yield return oneSecTimer;
            seconds--;

            if(minutes == 0)
            {
                break;
            }
        }
        inGameUI.SetActive(false);
        loseUI.SetActive(true);
        _playerCam.gameObject.SetActive(false);
        end = true;
    }


    public void UpdateProgressBar(float v)
    {
        _progressBar.fillAmount = v / initialCount;

        if((v / initialCount) >= 1f)
        {
            inGameUI.SetActive(false);
            winUI.SetActive(true);
            _playerCam.gameObject.SetActive(false);
            end = true;
        }
    }

    public void NextLevel()
    {
        Destroy(currentLevel);
        inGameUI.SetActive(true);
        loseUI.SetActive(false);
        winUI.SetActive(false);
        currentLevel = Instantiate(levelPrefab);
    }



}

