using BepInEx;
using CutEnding.Patches;
using UnityEngine.SceneManagement;

namespace CutEnding;

[BepInAutoPlugin(id: ID)]
public partial class SSCutEndingPlugin : BaseUnityPlugin
{
    public const string ID = "github.vitaxses.sscutending";

    private void Awake()
    {
        SceneManager.sceneLoaded += SceneChangedPatch.OnSceneChanged;
        Logger.LogInfo($"Plugin {Name} ({Id}) has loaded!");
    }
}