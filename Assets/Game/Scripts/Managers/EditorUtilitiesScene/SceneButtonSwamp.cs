using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class SceneButtonSwamp : EditorWindow
{
    [MenuItem("MyMenu/Move To Left #1")]
    public static void MoveToLeftScene() {
        if (EditorApplication.isPlaying) return;
        if (EditorBuildSettings.scenes.Length < 1) return;
        var nextIndex = GetNextIndex(SideMove.Left,SceneManager.GetActiveScene().buildIndex);
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene(EditorBuildSettings.scenes[nextIndex].path);
    }
    [MenuItem("MyMenu/Move To Right #2")]
    public static void MoveToRightScene() {
        if (EditorApplication.isPlaying) return;
        if (EditorBuildSettings.scenes.Length < 1) return;
        var nextIndex = GetNextIndex(SideMove.Right,SceneManager.GetActiveScene().buildIndex);
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene(EditorBuildSettings.scenes[nextIndex].path);
    }

    private static int GetNextIndex(SideMove moveTo, int buildIndex) {
        switch (moveTo) {
            case SideMove.Left: {
                var index = buildIndex - 1;
                if (IsInBuildIndexesRange(index)) 
                    return index;

                return EditorBuildSettings.scenes.Length - 1;
            }
            case SideMove.Right: {
                var index = buildIndex + 1;
                if (IsInBuildIndexesRange(index)) 
                    return index;

                return 0;
            }
        }

        return 0;
    }

    private static bool IsInBuildIndexesRange(int index) {
        if (index >= 0 && index < EditorBuildSettings.scenes.Length)
            return true;

        return false;
    }
    
    public enum SideMove
    {
        Left,
        Right
    }
}
