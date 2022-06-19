using HarmonyLib;
using ProjectM;
using ProjectM.UI;
using StunLocalization;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity;
using UnityEngine;
using UnityEngine.Events;
using Wetstone.API;
using Unity.Collections;

namespace VRisingExpanded.Hooks
{
    public static class QuickJoinServer
    {
        public static void Initialize()
        {
            if (VWorld.IsServer) return;

            Harmony.CreateAndPatchAll(typeof(QuickJoinServer));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(MenuCollection), "Awake")]
        public static void Awake(MenuCollection __instance)
        {
            // var _layout = GameObject.Find("MainMenuCanvas(Clone)/Canvas/MenuParent/MainMenu(Clone)/Content/SideBar");

            MenuButton btn = GameObject.Instantiate(__instance.MenuButtonPrefab);

            btn.Initialize(__instance._InstancedButtons[2].LocalizationKey, new UnityEngine.UI.Button.ButtonClickedEvent(), true, false);

            btn.transform.SetParent(__instance.ButtonsParent, false);
        }
    }
}