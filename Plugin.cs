using HarmonyLib;
using INeedWorkshopDeps.Attributes;
using Push.Input;
using UnityEngine;

namespace Push {
    [ContentWarningPlugin("novinity.Push", "1.0.1", true)]
    [ContentWarningDependency(3382537338)]
    public class Plugin {
        public static Plugin Instance { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }
        internal static PushInput pushInput = new PushInput();

        static Plugin() {
            pushInput.Enable();

            Patch();

            Debug.Log($"{"novinity.Push"} v{"1.0.1"} has loaded!");
        }

        internal static void Patch() {
            Harmony ??= new Harmony("novinity.Push");

            Debug.Log("Patching...");

            Harmony.PatchAll();

            Debug.Log("Finished patching!");
        }

        internal static void Unpatch() {
            Debug.Log("Unpatching...");

            Harmony?.UnpatchSelf();

            Debug.Log("Finished unpatching!");
        }
    }
}
