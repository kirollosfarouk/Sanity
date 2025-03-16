using Eflatun.SceneReference;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneConfig : ScriptableObject
{
    public static string Path = "SceneConfig";
    public static string EditorPathPrefix = "Assets/Resources/";
    public static string EditorPathSuffix = ".asset";

    public SceneReference MenuScene;
    public SceneReference GameScene;
    public SceneReference EmptyScene;
    public SceneReference FightScene;

    public static SceneConfig Instance
    {
        get
        {
#if UNITY_EDITOR
            if (!AssetDatabase.AssetPathExists(EditorPathPrefix + Path + EditorPathSuffix))
            {
                AssetDatabase.CreateAsset(CreateInstance<SceneConfig>(), EditorPathPrefix + Path + EditorPathSuffix);
            }
#endif
            return Resources.Load<SceneConfig>(Path);
        }
    }
}