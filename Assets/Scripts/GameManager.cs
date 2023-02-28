using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const uint AppID = 1890830;

    public static uint AppId
    {
        get => AppID;
    }
    
    private static GameManager _instance;

    public static GameManager Instance
    {
        get => _instance;
        set
        {
            if (_instance == null)
            {
                _instance = value;
            }
            else
            {
                Destroy(value.gameObject);
            }
        }
    }
    
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        #if !UNITY_EDITOR
        if (Steamworks.SteamClient.RestartAppIfNecessary(AppId))
        {
            Application.Quit();
        }
        #endif
        
        try
        {
            Steamworks.SteamClient.Init(AppId, true);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            Application.Quit();
        }
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnDestroy()
    {
        Steamworks.SteamClient.Shutdown();
        Application.Quit();
    }
}
