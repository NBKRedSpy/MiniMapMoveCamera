using MiniMapMoveCamera.Utility.Mcm;
using MiniMapMoveCamera.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using UnityEngine;


namespace MiniMapMoveCamera;

public class ModConfig : PersistentConfig<ModConfig>, IMcmConfigTarget
{

    /// <summary>
    /// The button to click to move the view to the mini map's position.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public KeyCode MoveCameraKey { get; set; } = KeyCode.Mouse2;

    /// <summary>
    /// The maximum time, in milliseconds, that can elapse between two consecutive clicks to be considered
    /// a double-click.
    /// </summary>
    public int DoubleClickTime { get; set; } = 250;

    public ModConfig() 
    {
    }


    public ModConfig(string configPath, Utility.Logger logger) : base(configPath, logger)
    {
    }

}
