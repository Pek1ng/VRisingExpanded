using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using System.Reflection;
using Wetstone.API;

namespace VRisingExpanded
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("xyz.molenzwiebel.wetstone")]
    public class Plugin : BasePlugin
    {
        private Harmony _harmony;

        public override void Load()
        {
            GlobalData.Logger = Log;

            _harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());

            Wetstone.Hooks.Chat.OnChatMessage += Chat.ChatMessageHandler.HandleChatMessgae;

            GlobalData.Logger.LogInfo($"插件 {PluginInfo.PLUGIN_GUID} 加载成功!");
        }
    }
}