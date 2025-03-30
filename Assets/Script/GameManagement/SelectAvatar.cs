using System;
using UnityEngine;

public class SelectAvatar : MonoBehaviour
{
    [SerializeField] private GameObject _carContainer;
    [SerializeField] private GameObject[] _cars;
    [SerializeField] private int _carIndex;

    void Start()
    {
        Ready("Player1");
        Ready("Player2");
    }
    // Update is called once per frame
    void Update()
    {
        _carContainer.transform.Rotate(new Vector3(0f, 0.125f, 0f), Space.Self);
    }

    public void Ready(string player)
    {
        SelectAvatarManager.Instance.AssignAvatarToPlayer(player, _cars[_carIndex]);
    }

    public void PreviousCar()
    {
        _carIndex--;
        if (_carIndex < 0) _carIndex = _cars.Length - 1;
        foreach (var car in _cars)
        {
            car.SetActive(false);
        }
        _cars[_carIndex].SetActive(true);
    }

    public void NextCar()
    {
        _carIndex++;
        if (_carIndex >= _cars.Length) _carIndex = 0;
        foreach (var car in _cars)
        {
            car.SetActive(false);
        }
        _cars[_carIndex].SetActive(true);
    }
}
