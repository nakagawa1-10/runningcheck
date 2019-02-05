using Kosu.UnityLibrary;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace NomuraKogei.Tsunagaling.RunningChecker
{
    public class AppController : MonoBehaviour
    {
        private AppSetting _setting;

        System.Diagnostics.Process _process = null;

        System.EventHandler _event;

        private bool _hasExitProcess = false;


        //------------------------------------------------------------------
        #region MonoBehavior functions
        private void Awake()
        {
            _setting = DataUtility.LoadDataFromJson<AppSetting>(AppSetting.PATH);

            // Fps設定
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = _setting.TargetFps;

            _event = new System.EventHandler(OnExitProcess);
        }
        
        private void Start()
        {
            StartProcess();
        }
        
        private void Update()
        {
            // TODO:S : UniRxで書き直し
            // Process終了値チェック
            if (_hasExitProcess)
            {
                OnDetectProcessExit();
            }

            // TODO:S : UniRxで書き直し
            // コマンド
            // 終了
            if (Input.GetKeyUp(KeyCode.Escape))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }

        private void OnApplicationQuit()
        {
            _process.Exited -= _event;
            _process.EnableRaisingEvents = false;
            _process.Kill();
            _process.Close();
            _process.Dispose();
            _process = null;
        }
        #endregion // MonoBehavior functions


        private void OnDetectProcessExit()
        {
            Debug.Log("[AppController] プロセス終了");
            ClearProcess();
            StartProcess();
        }

        private void OnExitProcess(object sender, System.EventArgs e)
        {
            _hasExitProcess = true;
        }

        private void StartProcess()
        {
            BackupLog();
            _process = new System.Diagnostics.Process();
            _process.StartInfo.FileName = _setting.ProcessPath;
            _process.EnableRaisingEvents = true;
            _process.Exited += _event;
            _process.Start();
            _hasExitProcess = false;
            Debug.Log("[AppController] プロセス開始");
        }

        private void ClearProcess()
        {
            _process.EnableRaisingEvents = false;
            _process.Exited -= _event;
            _process.Close();
            _process.Dispose();
            _process = null;
        }

        private void BackupLog()
        {
            if (!_setting.EnableLogBackup)
            {
                Debug.Log("[AppController] Backup function is disabled.");
                return;
            }

            if (!File.Exists(_setting.UnityLogFilePath))
            {
                Debug.LogWarning("[AppController] Log file can't be found. (Path : " + _setting.UnityLogFilePath + ")");
                return;
            }

            if (!Directory.Exists(_setting.LogBackupDirPath))
            {
                Directory.CreateDirectory(_setting.LogBackupDirPath);
            }

            var logFileName = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt";
            File.Copy(_setting.UnityLogFilePath, _setting.LogBackupDirPath + logFileName);
        }
    }
}
