using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace OneshotSunApparatice
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
        public static Plugin Instance;
        internal ManualLogSource mls = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.PLUGIN_NAME);
        internal static AssetBundle assetBundle;

        // Stolen Code from SharedUtils, sorry...
        public static string PathForResourceInAssembly(string resourceName, Assembly assembly = null)
        {
            return Path.Combine(Path.GetDirectoryName((assembly ?? Assembly.GetCallingAssembly()).Location), resourceName);
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                harmony.PatchAll();
            }

            string FolderLocation = Instance.Info.Location;
            assetBundle = AssetBundle.LoadFromFile(PathForResourceInAssembly("bulb", null));
            if (assetBundle == null)
            {
                mls.LogInfo("Failed to load AssetBundle!");
                return;
            }
            else
            {
                mls.LogInfo("AssetBundle loaded!");
            }

            harmony.PatchAll(typeof(Plugin));
            harmony.PatchAll(typeof(Patches.LungPropPatch));

            // Plugin startup logic
            mls.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
