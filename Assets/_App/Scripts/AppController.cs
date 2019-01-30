﻿using Kosu.UnityLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NomuraKogei.Tsunagaling.RunningChecker
{
    public class AppController : MonoBehaviour
    {
        private AppSetting _setting;

        System.Diagnostics.Process _process;

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
            _process = new System.Diagnostics.Process();
            _process.StartInfo.FileName = _setting.ProcessPath;
            _process.Exited += _event;
            _process.EnableRaisingEvents = true;
            _process.Start();
            _hasExitProcess = false;
            Debug.Log("[AppController] プロセス開始");
        }

        private void ClearProcess()
        {
            _process.Exited -= _event;
            _process.EnableRaisingEvents = false;
            _process.Close();
            _process.Dispose();
            _process = null;
        }
    }
}