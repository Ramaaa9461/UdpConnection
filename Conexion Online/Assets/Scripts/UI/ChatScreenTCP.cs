using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatScreenTCP : MonoBehaviour
{
    public Text messages;
    public InputField inputMessage;


    private void Start()
    {
        inputMessage.onValueChanged.AddListener(inputFieldChange);
    }

    void inputFieldChange(string text)
    {
        messages.text += text;
        messages.text += '\n';
    }

    

}
