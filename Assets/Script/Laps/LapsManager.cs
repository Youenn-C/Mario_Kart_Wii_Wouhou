using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LapsManager : MonoBehaviour
{
    public static LapsManager Instance;

    [Header("References"), Space(5)]
    [SerializeField] private LapsCheckers[] checkers;
    [SerializeField] private int checkersCountRemaining;
    [SerializeField] private int _maxLapsCount;
    [SerializeField] private int _currentLaps = 1;
    
    [Header("UI Elements"), Space(5)]
    [SerializeField] private TMP_Text lapsText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        checkersCountRemaining = checkers.Length;
        lapsText.text = _currentLaps + "/" + _maxLapsCount;
    }

    public void Check()
    {
        if (checkersCountRemaining == 0)
        {
            if (_currentLaps < _maxLapsCount) IncreaseLapsCount();
        }
    }

    private void IncreaseLapsCount()
    {
        _currentLaps++;
        lapsText.text = _currentLaps + "/" + _maxLapsCount;

        foreach (var checker in checkers)
        {
            checker.Reset();
            checkersCountRemaining = checkers.Length;
        }
    }

    public void DecreaseCheckersCountRemaining()
    {
        if (checkersCountRemaining > 0)
        {
            checkersCountRemaining--;
        }
    }
}
