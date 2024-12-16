using Photon.Voice;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseMode : MonoBehaviour
{
    public enum ConnectionType
    {
        None,
        Host,
        Join
    }

    [SerializeField]
    private ConnectionType connectionType;

    private void GetMode()
    {
        switch (connectionType)
        {
            case ConnectionType.None:
                //Handle 'None' case
                break;

             
            case ConnectionType.Host:
                HostMultiplayer();
                //Handle Host case
                break;
            case ConnectionType.Join:
                JoinMultiplayer();
                //Handle Join case
                break;

        }
    }

    private void Start()
    {
        PlayerPrefs.SetInt("GameMode", 0); //Host=1 Join=2
        GetMode();
        //HostMultiplayer();
        //JoinMultiplayer();
    }

 

    public void HostMultiplayer()
    {
        PlayerPrefs.SetInt("GameMode", 1);
        SceneManager.LoadScene("GameArea");
    }

    public void JoinMultiplayer()
    {
        PlayerPrefs.SetInt("GameMode", 2);
        SceneManager.LoadScene("GameArea");

    }
}
