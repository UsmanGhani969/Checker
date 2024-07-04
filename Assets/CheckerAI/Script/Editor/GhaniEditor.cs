using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.IO;
using UnityEngine.SceneManagement;

public abstract class GhaniEditor:MonoBehaviour
{

    #region PlayerPrefs
    [MenuItem("Ghani/Player Prefs/Clear All Player Prefs")]
    public static void DeleteData() => PlayerPrefs.DeleteAll();
    [MenuItem("Ghani/Player Prefs/10000 Free Coins")]
    public static void AddCoins() => PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1000);
	#endregion
	#region Anchoring
	[MenuItem("Ghani/Ancoring _1")]
	public static void AnchorsToCorners()
	{
		foreach (Transform transform in Selection.transforms)
		{
			RectTransform t = transform as RectTransform;
			RectTransform pt = Selection.activeTransform.parent as RectTransform;

			if (t == null || pt == null) return;

			Vector2 newAnchorsMin = new Vector2(t.anchorMin.x + t.offsetMin.x / pt.rect.width,
												t.anchorMin.y + t.offsetMin.y / pt.rect.height);
			Vector2 newAnchorsMax = new Vector2(t.anchorMax.x + t.offsetMax.x / pt.rect.width,
												t.anchorMax.y + t.offsetMax.y / pt.rect.height);

			t.anchorMin = newAnchorsMin;
			t.anchorMax = newAnchorsMax;
			t.offsetMin = t.offsetMax = new Vector2(0, 0);
		}
	}
    #endregion
    #region SwitchTargetPlatForm
    [MenuItem("Ghani/Switch Target/PC")]
	public static void PCBuild()
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);
	}
	[MenuItem("Ghani/Switch Target/Webgl")]
	public static void Webgl()
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.WebGL, BuildTarget.WebGL);
	}
	[MenuItem("Ghani/Switch Target/Android")]
	public static void Android()
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
	}
	#endregion
	#region Window
	[MenuItem("Ghani/Services _2")]
	public static void OpenServices()
	{
		EditorApplication.ExecuteMenuItem("Window/General/Services");
	}
	[MenuItem("Ghani/Lighting _3")]
	public static void OpenLighting()
	{
		EditorApplication.ExecuteMenuItem("Window/Rendering/Lighting");
	}
	[MenuItem("Ghani/Build Window _4")]
	public static void BuildWindow()
	{
		EditorApplication.ExecuteMenuItem("File/Build Settings...");
	}
	
	[MenuItem("Ghani/Project Setting _5")]
	public static void ProjectSetting()
	{
		EditorApplication.ExecuteMenuItem("Edit/Project Settings...");
	}

	[MenuItem("Ghani/Occlusion Culling _6")]
	public static void OpenOcculusion()
	{
		EditorApplication.ExecuteMenuItem("Window/Rendering/Occlusion Culling");
	}
    #endregion
    #region Importing
	[MenuItem("Ghani/Import Package _7")]
	public static void IMport()
    {
		EditorApplication.ExecuteMenuItem("Assets/Import Package/Custom Package...");
	}
	[MenuItem("Ghani/Import Assets _8")]
	public static void IMportAssets()
	{
		EditorApplication.ExecuteMenuItem("Assets/Import New Asset...");
	}
	#endregion
	#region Counting
	[MenuItem("Ghani/Hierarcy Objects _9")]
	public static void CountObjects()
	{
		int count = FindObjectsOfType<Transform>().Length;
		EditorUtility.DisplayDialog("Count Objects","There are " + count + " objects in the scene!","OK");
	}
    #endregion
    #region Info
    [MenuItem("Ghani/Info")]
    public static void Info()
    {
		EditorUtility.DisplayDialog("Info About this Editor.","Player Prefs Must be \"Coins\"" , "Ok");
    }
	#endregion
	#region OpenScene
	[MenuItem("Ghani/OpenScene/Scene 0")]
	public static void OpenScene0()
    {
		EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
		EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(0));
	}
	[MenuItem("Ghani/OpenScene/Scene 1")]
	public static void OpenScene1()
	{
		EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
		EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(1));
	}
	[MenuItem("Ghani/OpenScene/Scene 2")]
	public static void OpenScene2()
	{
		EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
		EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(2));
	}
	#endregion
	#region CreatingFolders

	[MenuItem("Ghani/Create Project Folders/Game/2DGame", false, 1)]
	public static void Setup2DDirectories()
	{
		CreateDirectory(Application.productName, "Art", "Script", "Scenes", "Models", "Prefab");
		AssetDatabase.Refresh();
	}
	private static void CreateDirectory(string rootFolder, params string[] directories)
	{
		var pathname = Path.Combine(Application.dataPath, rootFolder);
		foreach (var newDir in directories)
		{
			Directory.CreateDirectory(Path.Combine(pathname, newDir));
		}
	}

	[MenuItem("Ghani/Create Project Folders/Game/3DGame", false, 1)]
	public static void Setup3DDirectories()
	{
		CreateDirectory(Application.productName, "Art", "Script", "Scenes", "Models", "Prefab", "Material",
			"Animation");
		AssetDatabase.Refresh();
	}
	#endregion

}
