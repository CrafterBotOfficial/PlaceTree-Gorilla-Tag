/*
    The following script belongs to Crafterbot, if I see this script 
    being used in any shitty mod menus, I will take actions to have it removed.
    Additionally note I made this mod at 1 am so some of the code is going to be messy/repetive.
    I will go back and clean it up later.
*/
using BepInEx;
using HarmonyLib;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using Utilla;

namespace TreePlaceGorillaTag
{
    [BepInPlugin(ModInfo.ModGUILD, ModInfo.ModName, ModInfo.ModVersion)]
    [BepInDependency("org.legoandmars.gorillatag.utilla")]
    [Description("HauntedModMenu")]
    [ModdedGamemode]
    public class Main : BaseUnityPlugin
    {
        public static Main Instance { get; private set; }

        public bool ModAllowed;
        private void Awake()
        {
            Instance = this;
            
            Utilla.Events.GameInitialized += Events_GameInitialized;
        }

        private void Events_GameInitialized(object sender, System.EventArgs e)
        {
            DontDestroyOnLoad(new GameObject().AddComponent<Input>());
            DontDestroyOnLoad(new GameObject().AddComponent<TreePlace.TreeManager>());
        }

        public bool _modEnabled;
        public bool _roomValid;
        private void OnEnable() => _modEnabled = true;
        private void OnDisable()
        {
            _modEnabled = false;
            TreePlace.TreeManager.ClearAllTree();
        }
        [ModdedGamemodeJoin]
        private void OnModdedGamemodeJoin(string gamemode) => _roomValid = true;
        [ModdedGamemodeLeave]
        private void OnModdedGamemodeLeave(string gamemode)
        {
            _roomValid = false;
            TreePlace.TreeManager.ClearAllTree();
        }
    }

    internal class ModInfo
    {
        public const string ModGUILD = "crafterbot.placetree.gorillatag";
        public const string ModName = "Place Tree";
        public const string ModVersion = "1.0.0";
    }
}