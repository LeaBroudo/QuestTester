using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class DebugCanvasLogger : MonoBehaviour
{

    public int maxLines = 8;
    private Queue<string> queue = new Queue<string>();
    private string currentText = "";
    private Text text;
    

    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnEnable()
    {
        Application.logMessageReceivedThreaded += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceivedThreaded -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Delete oldest message
        if (queue.Count >= maxLines) queue.Dequeue();

        queue.Enqueue(logString + stackTrace);
        //queue.Enqueue(logString);

        var builder = new StringBuilder();
        foreach (string st in queue)
        {
            builder.Append(st).Append("\n\n");
        }

        currentText = builder.ToString();
        text.text = currentText;
    }
}
