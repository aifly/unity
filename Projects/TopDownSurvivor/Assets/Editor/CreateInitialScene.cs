using System.IO;
using TopDownSurvivor.Gameplay;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopDownSurvivor.EditorTools
{
    public static class CreateInitialScene
    {
        private const string ScenePath = "Assets/Scenes/Main.unity";
        private const string PlayerSpritePath = "Assets/Art/Generated/player.png";

        public static void Generate()
        {
            ConfigureExternalEditor();
            EnsureFolders();
            EnsurePlayerSprite();

            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            GameObject player = CreatePlayer();
            CreateCamera(player.transform);
            CreateGrid();

            EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), ScenePath);
            EditorBuildSettings.scenes = new EditorBuildSettingsScene[]
            {
                new EditorBuildSettingsScene(ScenePath, true)
            };

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"Generated initial scene at {ScenePath}");
        }

        private static void ConfigureExternalEditor()
        {
            const string codePath = @"E:\Unity\VSCode\Code.exe";
            if (File.Exists(codePath))
            {
                EditorPrefs.SetString("kScriptsDefaultApp", codePath);
                EditorPrefs.SetString("kScriptEditorArgs", "\"$(ProjectPath)\" -g \"$(File)\":$(Line):$(Column)");
            }
        }

        private static void EnsureFolders()
        {
            Directory.CreateDirectory("Assets/Art/Generated");
            Directory.CreateDirectory("Assets/Scenes");
            Directory.CreateDirectory("Assets/Scripts");
        }

        private static void EnsurePlayerSprite()
        {
            if (!File.Exists(PlayerSpritePath))
            {
                const int size = 64;
                Texture2D texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
                Color32 transparent = new Color32(0, 0, 0, 0);
                Color32 fill = new Color32(42, 177, 133, 255);
                Color32 highlight = new Color32(238, 250, 245, 255);
                Vector2 center = new Vector2((size - 1) * 0.5f, (size - 1) * 0.5f);
                float radius = size * 0.42f;

                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        float distance = Vector2.Distance(new Vector2(x, y), center);
                        Color32 color = distance <= radius ? fill : transparent;

                        if (distance <= radius * 0.42f && y > center.y)
                        {
                            color = highlight;
                        }

                        texture.SetPixel(x, y, color);
                    }
                }

                texture.Apply();
                File.WriteAllBytes(PlayerSpritePath, texture.EncodeToPNG());
                Object.DestroyImmediate(texture);
            }

            AssetDatabase.ImportAsset(PlayerSpritePath, ImportAssetOptions.ForceUpdate);
            TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(PlayerSpritePath);
            importer.textureType = TextureImporterType.Sprite;
            importer.spritePixelsPerUnit = 64f;
            importer.mipmapEnabled = false;
            importer.filterMode = FilterMode.Point;
            importer.SaveAndReimport();
        }

        private static GameObject CreatePlayer()
        {
            GameObject player = new GameObject("Player");
            player.transform.position = Vector3.zero;

            SpriteRenderer renderer = player.AddComponent<SpriteRenderer>();
            renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(PlayerSpritePath);
            renderer.sortingOrder = 10;

            Rigidbody2D body = player.AddComponent<Rigidbody2D>();
            body.gravityScale = 0f;
            body.freezeRotation = true;
            body.interpolation = RigidbodyInterpolation2D.Interpolate;

            CircleCollider2D collider = player.AddComponent<CircleCollider2D>();
            collider.radius = 0.42f;

            player.AddComponent<PlayerController2D>();
            return player;
        }

        private static void CreateCamera(Transform player)
        {
            GameObject cameraObject = new GameObject("Main Camera");
            cameraObject.tag = "MainCamera";
            cameraObject.transform.position = new Vector3(0f, 0f, -10f);

            Camera camera = cameraObject.AddComponent<Camera>();
            camera.orthographic = true;
            camera.orthographicSize = 6f;
            camera.backgroundColor = new Color(0.08f, 0.1f, 0.12f);

            CameraFollow2D follow = cameraObject.AddComponent<CameraFollow2D>();
            follow.SetTarget(player);
        }

        private static void CreateGrid()
        {
            Material lineMaterial = new Material(Shader.Find("Sprites/Default"));
            GameObject grid = new GameObject("Grid");

            for (int i = -8; i <= 8; i++)
            {
                CreateLine(grid.transform, new Vector3(i, -5f, 0f), new Vector3(i, 5f, 0f), lineMaterial);
                CreateLine(grid.transform, new Vector3(-8f, i, 0f), new Vector3(8f, i, 0f), lineMaterial);
            }
        }

        private static void CreateLine(Transform parent, Vector3 start, Vector3 end, Material material)
        {
            GameObject lineObject = new GameObject("Grid Line");
            lineObject.transform.SetParent(parent);

            LineRenderer line = lineObject.AddComponent<LineRenderer>();
            line.material = material;
            line.positionCount = 2;
            line.SetPosition(0, start);
            line.SetPosition(1, end);
            line.startWidth = 0.025f;
            line.endWidth = 0.025f;
            line.startColor = new Color(1f, 1f, 1f, 0.13f);
            line.endColor = new Color(1f, 1f, 1f, 0.13f);
            line.useWorldSpace = true;
        }
    }
}
