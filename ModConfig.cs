using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Push {
    class ModConfig {
        public readonly ConfigEntry<float> pushForce;
        public readonly ConfigEntry<bool> canPushMonsters, canPushPlayers;

        public ModConfig(ConfigFile cfg) {
            // We want to disable saving our config file every time we bind a
            // setting as it's inefficient and slow
            cfg.SaveOnConfigSet = false;

            pushForce = cfg.Bind(
                "General",
                "PushForce",
                5f,
                "How hard you push things"
            );

            canPushMonsters = cfg.Bind(
                "General",
                "CanPushMonsters",
                false,
                "Can you push monsters?"
            );
            canPushPlayers = cfg.Bind(
                "General",
                "CanPushPlayers",
                true,
                "Can you push players?"
            );

            // Get rid of old settings from the config file that are not used anymore
            ClearOrphanedEntries(cfg);
            // We need to manually save since we disabled `SaveOnConfigSet` earlier
            cfg.Save();
            // And finally, we re-enable `SaveOnConfigSet` so changes to our config
            // entries are written to the config file automatically from now on
            cfg.SaveOnConfigSet = true;
        }

        static void ClearOrphanedEntries(ConfigFile cfg) {
            // Find the private property `OrphanedEntries` from the type `ConfigFile`
            PropertyInfo orphanedEntriesProp = AccessTools.Property(typeof(ConfigFile), "OrphanedEntries");
            // And get the value of that property from our ConfigFile instance
            var orphanedEntries = (Dictionary<ConfigDefinition, string>)orphanedEntriesProp.GetValue(cfg);
            // And finally, clear the `OrphanedEntries` dictionary
            orphanedEntries.Clear();
        }
    }
}
