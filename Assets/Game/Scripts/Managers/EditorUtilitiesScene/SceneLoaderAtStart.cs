using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class SceneLoaderAtStart : EditorWindow
{
    [Obsolete("Obsolete")]
    static SceneLoaderAtStart()
    {
        EditorApplication.playmodeStateChanged += ModeChanged;
    }
    
    private static async void ModeChanged() {
        if (!EditorApplication.isPlayingOrWillChangePlaymode &&
            EditorApplication.isPlaying) 
        {
            await Task.Delay(500);
            ReturnToLastScene();
        }
    }

    [MenuItem("Play/Execute starting scene #z")]
    public static void RunMainScene()
    {
        EditorPrefs.SetString("lastScene", SceneManager.GetActiveScene().path);
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene(EditorBuildSettings.scenes[0].path);
        EditorApplication.isPlaying = true;
    }
   
    [MenuItem("Play/Exit Scene #x")]
    public static void ExitScene()
    {
        EditorApplication.ExitPlaymode();
    }
    
    [MenuItem("Play/Reload editing scene _%g")]
    public static void ReturnToLastScene()
    {
        EditorSceneManager.OpenScene(EditorPrefs.GetString("lastScene"));
    }
}
