using UnityEngine;

public class SceneDebugger : MonoBehaviour {
    private static string _log = "";
    private string _output;
    private string _stack;

    private void Awake() {
        DontDestroyOnLoad(this);
    }

    void OnEnable() {
        Application.logMessageReceived += Log;
    }

    void OnDisable() {
        Application.logMessageReceived -= Log;
    }

    private void Log(string logString, string stackTrace, LogType type) {
        _output = logString;
        _stack = stackTrace;
        if (_stack.Length > 0) {
            _log = _log+ "\n" + _output + "\nSTACK:\t" + _stack ;
        }
        else {
            _log = _log+ "\n" + _output;
        }

        if (_log.Length > 5000) {
            _log = _log.Substring(0, 4000);
        }
    }

    private void OnGUI() {
        if (!Application.isEditor) {
            _log = GUI.TextArea(new Rect(10, 10, Screen.width - 10, 500), _log);
        }
    }
}