/*
    The following script belongs to Crafterbot, if I see this script 
    being used in any shitty mod menus, I will take actions to have it removed.
    Additionally note I made this mod at 1 am so some of the code is going to be messy/repetive.
    I will go back and clean it up later.
*/
using BepInEx;
using System.ComponentModel;
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

/*
MIT License

Copyright (c) 2023 Crafterbot

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/