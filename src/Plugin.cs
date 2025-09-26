using HarmonyLib;
using MiniMapMoveCamera;
using MGSC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MiniMapMoveCamera_Bootstrap;

namespace MiniMapMoveCamera
{
    public class Plugin : BootstrapMod
    {

        public static ConfigDirectories ConfigDirectories = new ConfigDirectories();

        public static ModConfig Config { get; private set; }

        public static Utility.Logger Logger = new();

        private static McmConfiguration McmConfiguration;

        public Plugin(HookEvents hookEvents, bool isBeta) : base(hookEvents, isBeta)
        {
            Directory.CreateDirectory(ConfigDirectories.ModPersistenceFolder);

            Config = ModConfig.LoadConfig(ConfigDirectories.ConfigPath, Logger);

            McmConfiguration = new McmConfiguration(Config, Logger);
            McmConfiguration.TryConfigure();


            new Harmony("NBKRedSpy_" + ConfigDirectories.ModAssemblyName).PatchAll();
        }
     
    }
}
