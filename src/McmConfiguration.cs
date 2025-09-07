using MiniMapMoveCamera.Utility;
using MiniMapMoveCamera.Utility.Mcm;
using MiniMapMoveCamera;
using ModConfigMenu;
using ModConfigMenu.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace MiniMapMoveCamera
{
    internal class McmConfiguration : McmConfigurationBase
    {

        public McmConfiguration(ModConfig config, Utility.Logger logger) : base (config, logger) { }

        public override void Configure()
        {
            ModConfigMenuAPI.RegisterModConfig("Minimap Move Camera", new List<ConfigValue>()
            {
                CreateConfigProperty(nameof(ModConfig.DoubleClickTime), "The maximum time, in milliseconds, that can elapse between two consecutive " +
                "clicks to be considered a double-click.", 0f, 2000f),
                CreateReadOnly(nameof(ModConfig.MoveCameraKey)),

            }, OnSave);
        }
    }
}
