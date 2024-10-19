using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Push.Input;

namespace Push {
    [ContentWarningPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_VERSION, true)]
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.visualerror.inputapi", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin {
        public static Plugin Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }
        internal PushInput pushInput = new PushInput();

        internal static ModConfig BoundConfig { get; private set; } = null!;

        private void Awake() {
            Logger = base.Logger;
            Instance = this;

            BoundConfig = new ModConfig(base.Config);
            pushInput.Enable();

            Patch();

            Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
        }

        internal static void Patch() {
            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

            Logger.LogDebug("Patching...");

            Harmony.PatchAll();

            Logger.LogDebug("Finished patching!");
        }

        internal static void Unpatch() {
            Logger.LogDebug("Unpatching...");

            Harmony?.UnpatchSelf();

            Logger.LogDebug("Finished unpatching!");
        }
    }
}
