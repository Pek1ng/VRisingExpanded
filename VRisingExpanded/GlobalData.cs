using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using Unity.Entities;
using Wetstone.API;

namespace VRisingExpanded
{
    public static class GlobalData
    {
        /// <summary>
        /// 日志记载
        /// </summary>
        public static ManualLogSource Logger;

        public static World CurrentWorld
        {
            get
            {
                if (VWorld.IsServer)
                {
                    return VWorld.Server;
                }

                return VWorld.Client;
            }
        }

        static GlobalData()
        {
        }
    }
}