using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject testPrefab;

    public int cameraSize;
    public List<WallObject> wallObjects;
    public List<EnemyObject> enemyObjects;

    List<AllyObject> allyObjects;
    Coroutine progressGameplay;

    public static LevelManager Instance { get; private set; }

    [ContextMenu("Init Data")]
    public void InitData()
    {
        wallObjects = new();
        enemyObjects = new();

        Transform wallContainer = transform.GetChild(0);
        int totalWall = wallContainer.childCount;
        for (int i = 0; i < totalWall; i++) wallObjects.Add(wallContainer.GetChild(i).GetComponent<WallObject>());

        Transform enemyContainer = transform.GetChild(1);
        int totalEnemy = enemyContainer.childCount;
        for (int i = 0; i < totalEnemy; i++) enemyObjects.Add(enemyContainer.GetChild(i).GetComponent<EnemyObject>());
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Camera.main.orthographicSize = cameraSize * 2f;
        SpawnSaveAlly();
        GameManager.Instance.GameState = GameState.PLAYING;

        progressGameplay = StartCoroutine(ProgressGameplay());
    }

    void SpawnSaveAlly()
    {
        allyObjects = SessionPref.GetSaveAlly();

        foreach (var a in allyObjects) a.InitTargetObject(GetAllTransformLevelObject());

        InitEnemyOnChangeAlly();
    }

    private List<Transform> GetAllTransformLevelObject()
    {
        List<BaseObject> baseObjects = new();
        baseObjects.AddRange(wallObjects);
        baseObjects.AddRange(enemyObjects);

        List<Transform> allTransform = new();
        foreach (var t in baseObjects) if (t != null) allTransform.Add(t.transform);
        return allTransform;
    }

    private List<Transform> GetAllAllyTranform()
    {
        List<Transform> allTransform = new();
        foreach (var t in allyObjects) if (t != null) allTransform.Add(t.transform);
        return allTransform;
    }

    public void OnChangeQuantityAlly(AllyObject allySpawn)
    {
        InitAlly(allySpawn);
        InitEnemyOnChangeAlly();
    }

    void InitAlly(AllyObject allySpawn)
    {
        allyObjects ??= new();
        allyObjects.Add(allySpawn);

        allySpawn.InitTargetObject(GetAllTransformLevelObject());
    }

    void InitEnemyOnChangeAlly()
    {
        foreach (var enemy in enemyObjects) if (enemy != null) enemy.InitTargetObject(GetAllAllyTranform());
    }

    public void OnAllyDeath(AllyObject allyDeath)
    {
        allyObjects.Remove(allyDeath);
        InitEnemyOnChangeAlly();
        if (IsLose()) Lose();
    }

    public void OnEnemyDeath(EnemyObject enemyObject)
    {
        enemyObjects.Remove(enemyObject);
        foreach (var a in allyObjects) a.InitTargetObject(GetAllTransformLevelObject());
        if (IsWin()) Win();
    }

    public void OnWallDestroyed(WallObject wallObject)
    {
        wallObjects.Remove(wallObject);
        foreach (var a in allyObjects) a.InitTargetObject(GetAllTransformLevelObject());
        if (IsWin()) Win();
    }

    bool IsWin()
    {
        return wallObjects.Count == 0 && enemyObjects.Count == 0;
    }

    bool IsLose()
    {
        return allyObjects.Count == 0;
    }

    void Win()
    {
        StopCoroutine(progressGameplay);

        GameManager.Instance.GameState = GameState.PAUSE;
        SessionPref.SaveAlly(allyObjects);

        Invoke(nameof(SpawnNextLevel), 2f);
    }

    void SpawnNextLevel()
    {
        int totalSaveAlly = allyObjects.Count;

        float pointX = totalSaveAlly / 2f * -1f;
        foreach (var a in allyObjects)
        {
            Vector3 position = new(pointX, 2f, 0f);
            a.StopAttack();
            a.transform.SetPositionAndRotation(position, Quaternion.identity);
            pointX++;
        }

        Destroy(gameObject);
        Instantiate(testPrefab);
    }

    void Lose()
    {
        StopCoroutine(progressGameplay);

        GameManager.Instance.GameState = GameState.PAUSE;
        SessionPref.ClearSaveData();
        GameplayCanvasController.Instance.OnLose();
    }

    IEnumerator ProgressGameplay()
    {
        while (true)
        {
            if (allyObjects != null) foreach (var a in allyObjects) if (a != null) a.Progress();
            if (enemyObjects != null) foreach (var e in enemyObjects) if (e != null) e.Progress();
            yield return null;
        }
    }
}
