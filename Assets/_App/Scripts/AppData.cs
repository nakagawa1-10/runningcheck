using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NomuraKogei.Tsunagaling.RunningChecker
{
    public class AppSetting
    {
        public static readonly string PATH = Application.streamingAssetsPath + "/Json/app_setting.json";

        public int TargetFps;

        public string ProcessPath;

        public bool EnableLogBackup;

        public string UnityLogFilePath;

        public string LogBackupDirPath;

        public AppSetting()
        {
            TargetFps = 30;
            ProcessPath = string.Empty;
            EnableLogBackup = true;
            UnityLogFilePath = string.Empty;
            LogBackupDirPath = string.Empty;
        }
    }
}
