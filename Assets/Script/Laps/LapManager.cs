using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapManager : MonoBehaviour
{
    public int _lapNumber;
    public int _maxLapNumber;
    public List<CheckPoint> _checkpoints;
    [SerializeField] private int _numberOfCheckpoints;
    
    [SerializeField] private TMP_Text _lapText;

    private void Start()
    {
        _numberOfCheckpoints = FindObjectsByType<CheckPoint>(FindObjectsSortMode.None).Length;
        _checkpoints = new List<CheckPoint>();
        _lapText.text = "Laps : " + _lapNumber.ToString() + " / " + _maxLapNumber;
    }

    public void AddCheckPoint(CheckPoint checkPointToAdd)
    {
        if(checkPointToAdd.isFinishLine)
        {
            FinishLap();
        }

        if(_checkpoints.Contains(checkPointToAdd) == false)
        {
            _checkpoints.Add(checkPointToAdd);
        }
    }

    private void FinishLap()
    {
        if (_checkpoints.Count > _numberOfCheckpoints/2)
        {
            _lapNumber++;
            _lapText.text = "Laps : " + _lapNumber.ToString() + " / " + _maxLapNumber;
            _checkpoints.Clear();
        }
    }
}