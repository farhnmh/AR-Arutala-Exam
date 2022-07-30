using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Globalization;

public class TimeConvertHandler : MonoBehaviour
{
    [Header("General Attribute")]
    public TMP_InputField timeInputField;
    public TextMeshProUGUI detailText;
    public string resultText;
    
    public void ConvertTime()
    {
        try
        {
            string time = timeInputField.text.Replace(" ", "");
            resultText = DateTime.ParseExact(time, "hh:mm:sstt", CultureInfo.InvariantCulture).ToString("HH:mm:ss");
            detailText.text = $"Result: {resultText}";
            timeInputField.text = string.Empty;
        }
        catch
        {
            detailText.text = "Please Enter 12-Hour Format!";    
        }
    }
}
