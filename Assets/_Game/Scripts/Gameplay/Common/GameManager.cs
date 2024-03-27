using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameState _gameState = GameState.PAUSE;

    public static GameManager Instance;

    public GameState GameState { get => _gameState; set => _gameState = value; }

    private void Awake()
    {
        Instance = this;
        SessionPref.ClearSaveData();
    }
}
