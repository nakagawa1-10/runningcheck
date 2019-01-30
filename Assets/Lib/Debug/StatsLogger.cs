using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Kosu.UnityLibrary
{
    public class StatsLogger : MonoBehaviour
    {

        [SerializeField]
        private Text _logStringPrefab;

        [SerializeField]
        private RectTransform _logStringParent;

        [SerializeField]
        private int _maxDisplayCount = 8;

        [SerializeField]
        private bool _outputLog = false;

        [SerializeField]
        private string _logDirPathInStreamingAssets;


        private void OnEnable()
        {
            Application.logMessageReceivedThreaded += LogCallbackHandler;
        }

        private void OnDisable()
        {
            Application.logMessageReceivedThreaded -= LogCallbackHandler;
        }

        private void LogCallbackHandler(string logString, string stackTrace, LogType type)
        {
            string log = "";

            switch (type)
            {
                case LogType.Log:
                    log = "<color=\"#00FF11\">";
                    break;

                case LogType.Warning:
                    log = "<color=\"#FFF000\">";
                    break;

                case LogType.Error:
                case LogType.Exception:
                    log = "<color=\"#FF0500\">";
                    break;
            }

            log += string.Format("[{0}][{1}] {2}</color>",
                                 type.ToString(),
                                 System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                                 logString);

            if (_outputLog)
            {
                var planeLog = string.Format("[{0}][{1}] {2}",
                                 type.ToString(),
                                 System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                                 logString);
                ExportLog(planeLog);
            }

            var text = Instantiate(_logStringPrefab);
            if (!text.gameObject.activeSelf) text.gameObject.SetActive(true);
            text.text = log;
            text.transform.SetParent(_logStringParent, false);

            while (_logStringParent.childCount > _maxDisplayCount)
            {
                var child = _logStringParent.GetChild(0);
                DestroyImmediate(child.gameObject);
            }
        }

        private void ExportLog(string log)
        {
            if (!Directory.Exists(Application.streamingAssetsPath + "/" + _logDirPathInStreamingAssets)) return;

            var filePath = Application.streamingAssetsPath + "/" + _logDirPathInStreamingAssets + "/" + System.DateTime.Now.ToString("yyyy_MM_dd") + ".txt";

            File.AppendAllText(filePath, log + System.Environment.NewLine);
        }
    }
}
