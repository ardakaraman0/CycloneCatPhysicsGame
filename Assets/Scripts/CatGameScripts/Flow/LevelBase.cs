using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class LevelBase : MonoBehaviour
{

    [SerializeField]
    int levelIndex = 0;

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
    CinemachineBrain _mainCamera;
    [SerializeField]
    CinemachineFreeLook _playerCam;
    [SerializeField]
    CinemachineVirtualCamera _shooterCam;


    void Start()
    {
        //StartCoroutine(TimeLeftCounter());


    }

    void Update()
    {

    }

    WaitForSeconds oneSecTimer = new WaitForSeconds(1f);
    int minutes;
    int seconds;
    IEnumerator TimeLeftCounter()
    {
        _timerText.text = minutes.ToString() + ":" + seconds.ToString();
        while(timeLeft >= 0)
        {
            _timerText.text = minutes.ToString() + ":" + seconds.ToString();
            yield return oneSecTimer;
            timeLeft--;
            minutes = timeLeft % 60;
            seconds = timeLeft - minutes * 60;
        }
    }


    public void UpdateProgressBar(float value)
    {
        playerPoints += value;
        _progressBar.fillAmount = playerPoints / pointsCap;
    }


    public void ChangeCameras()
    {
        
    }

}

