using System;
using UnityEngine;

public class SelectAvatarManager : MonoBehaviour
{
    public static SelectAvatarManager Instance;
    
    public string avatarPlayer1;
    public string avatarPlayer2;
    
    [Header("Prefabs"),Space(5)]
    public GameObject octanePrefab;
    public GameObject fennecPrefab;
    public GameObject dominusPrefab;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AssignAvatarToPlayer(String playerTarget, GameObject avatarTarget)
    {
        if (playerTarget == "Player1")
        {
            if (avatarTarget.name == "Octane") avatarPlayer1 = "Octane";
            else if (avatarTarget.name == "Fennec") avatarPlayer1 = "Fennec";
            else if (avatarTarget.name == "Dominus") avatarPlayer1 = "Dominus";
        }
        
        else if (playerTarget == "Player2")
        {
            if (avatarTarget.name == "Octane") avatarPlayer2 = "Octane";
            else if (avatarTarget.name == "Fennec") avatarPlayer2 = "Fennec";
            else if (avatarTarget.name == "Dominus") avatarPlayer2 = "Dominus";
        }
        
        DontDestroyOnLoad(gameObject);
    }
}
