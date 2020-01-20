using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunningChecker
{
    public class AppSetting
    {
        public static readonly string PATH = Application.streamingAssetsPath + "/Json/app_setting.json";

        public int TargetFps;

        public string ProcessPath;

        public string ProcessArgs;

        public bool EnableLogBackup;

        public string UnityLogFilePath;

        public string LogBackupDirPath;

        public AppSetting()
        {
            TargetFps = 30;
            ProcessPath = string.Empty;
            ProcessArgs = string.Empty;
            EnableLogBackup = true;
            UnityLogFilePath = string.Empty;
            LogBackupDirPath = string.Empty;
        }
    }
}
