using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Collections.Concurrent;

namespace OneshotSunApparatice.Patches
{
    [HarmonyPatch(typeof(LungProp))]
    internal class LungPropPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        static void ReplaceModel(LungProp __instance)
        {
            Plugin.Instance.mls.LogInfo("LungPropPatch.Start");

            // Rename anything relating to the aparatus to be The Sun
            ScanNodeProperties scanNodeProperties = __instance.gameObject.GetComponentInChildren<ScanNodeProperties>();
            scanNodeProperties.headerText = "The Sun";
            __instance.itemProperties.itemName = "The Sun";


            GameObject.Destroy(__instance.gameObject.transform.Find("Mesh").gameObject);
            GameObject gameObject = GameObject.Instantiate(Plugin.assetBundle.LoadAsset<GameObject>("assets/bulb.prefab"), __instance.gameObject.transform);

            // Hard Coded Positioning because I'm lazy
            gameObject.transform.localRotation = Quaternion.Euler(0, 180, 180);
            gameObject.transform.localScale = new Vector3(4, 4, 4);
        }
    }
}
