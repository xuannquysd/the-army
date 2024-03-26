using UnityEditor;
using UnityEngine;

public class GenerateLevel : EditorWindow
{

    static int row, col;
    static GameObject mapPrefab;
    static int sizeSlot = 50;
    static bool[,] slotMatrixDeleted;

    static GenerateLevel window;

    static string mapTemplatePath = "Assets/_Game/Prefabs/Gameplay/Levels/Level Template.prefab";
    static string smallWallPrefabPath = "Assets/_Game/Prefabs/Gameplay/Object/Small Wall.prefab";

    static GameObject mapTemplatePrefab;
    static GameObject smallWallPrefab;

    private void OnEnable()
    {
        slotMatrixDeleted = null;
        mapTemplatePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(mapTemplatePath, typeof(GameObject));
        smallWallPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(smallWallPrefabPath, typeof(GameObject));
    }

    [MenuItem("The Army/Level/Generate Map")]
    static void OpenWindow()
    {
        window = (GenerateLevel)GetWindow(typeof(GenerateLevel));
        window.Show();

    }

    private void OnGUI()
    {
        DrawEditorField();
        DrawMapPreview();
    }

    void DrawEditorField()
    {
        GUILayout.BeginHorizontal();

        col = EditorGUILayout.IntField("Width: ", col);
        row = EditorGUILayout.IntField("Height: ", row);
        sizeSlot = EditorGUILayout.IntField("Size view: ", sizeSlot);
        GUILayout.EndHorizontal();
    }

    void DrawMapPreview()
    {
        GameObject slot = Resources.Load<GameObject>("Circle_gray");
        Texture2D texSlot = AssetPreview.GetAssetPreview(slot);

        float offsetX = (Screen.width - sizeSlot / 2f * col) / 2f;

        GUILayout.BeginArea(new Rect(offsetX, 75, col * sizeSlot, row * sizeSlot), new GUIStyle("box"));
        GUILayout.BeginHorizontal();

        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                GUILayout.BeginArea(new Rect(sizeSlot * i, j * sizeSlot, sizeSlot, sizeSlot));

                Texture img = texSlot;

                try
                {
                    if (slotMatrixDeleted != null && slotMatrixDeleted[i, j]) img = new Texture2D(sizeSlot, sizeSlot);
                }
                catch
                {
                    slotMatrixDeleted = new bool[col, row];
                }

                if (GUILayout.Button(img, new GUILayoutOption[] { GUILayout.Width(sizeSlot), GUILayout.Height(sizeSlot) }))
                {
                    slotMatrixDeleted ??= new bool[col, row];
                    slotMatrixDeleted[i, j] = !slotMatrixDeleted[i, j];
                }
                GUILayout.EndArea();
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(offsetX, row * sizeSlot + 100, col * sizeSlot, row * sizeSlot));
        GUILayout.BeginVertical();
        if (GUILayout.Button("Generate Map"))
        {
            GenerateMap();
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    void GenerateMap()
    {
        GameObject levelSpawn = PrefabUtility.InstantiatePrefab(mapTemplatePrefab) as GameObject;
        Transform wallContainer = levelSpawn.transform.Find("Wall");
        for(int i = 0; i < col; i++)
        {
            for(int j = 0; j < row; j++)
            {
                if (slotMatrixDeleted[i, j]) continue;
                GameObject wallSpawn = PrefabUtility.InstantiatePrefab(smallWallPrefab, wallContainer) as GameObject;
                wallSpawn.transform.position = new Vector3(i, j, 0f);

                LevelManager levelManager = levelSpawn.GetComponent<LevelManager>();
                levelManager.cameraSize = col;
            }
        }
    }
}