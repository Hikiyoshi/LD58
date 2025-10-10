using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action OnGameover;
    public event Action<int> OnChangeSteps;

    [SerializeField] private int steps;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    public void Gameover()
    {
        Debug.Log("Gameover");
        OnGameover?.Invoke();
    }

    public void DescreaseSteps()
    {
        steps -= 1;

        if (steps <= 0)
        {
            Gameover();
            return;
        }

        OnChangeSteps?.Invoke(steps);
    }
}
