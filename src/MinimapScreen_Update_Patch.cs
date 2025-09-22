using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MiniMapMoveCamera
{
    [HarmonyPatch(typeof(MinimapScreen), nameof(MinimapScreen.LateUpdate))]
    public static class MinimapScreen_Update_Patch
    {
        private static Stopwatch DoubleClickTimer = new Stopwatch();
        public static void Prefix(MinimapScreen __instance)
        {
            
            if (Input.GetKeyDown(Plugin.Config.MoveCameraKey))
            {

                if (DoubleClickTimer.IsRunning)
                {
                    if (DoubleClickTimer.ElapsedMilliseconds < Plugin.Config.DoubleClickTime)
                    {
                        DoubleClickTimer.Stop();
                        __instance.Hide();
                        return;
                    }
                }

                DoubleClickTimer.Restart();
                State state = UserModSystem._modContext.State;
                GameCamera camera = state.Get<GameCamera>();
                camera.SetCameraMode(CameraMode.BorderMove);

                Vector2 normCursorPos = __instance._minimapCursorPosHandler.NormCursorPos;
                int num3 = Mathf.RoundToInt(normCursorPos.x * (float)__instance._mapGrid.MaxWidth);
                int num4 = Mathf.RoundToInt(normCursorPos.y * (float)__instance._mapGrid.MaxHeight);

                CellPosition cellPosition = new CellPosition(num3, num4);
                var x = state.Get<MapRenderer>();
                Vector3 location = x.ConvertPosToWorld(new CellPosition(num3, num4), Vector2.zero);
                camera.MoveCameraToPosition(location, .25f);
            }
        }
    }
}
