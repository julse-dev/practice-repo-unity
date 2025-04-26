using System.Text.RegularExpressions;
using System;
using UnityEngine;
using TMPro;

public class RegularExpressionController : MonoBehaviour
{
    /*
     *  This script uses regular expressions to separate currency like dollars($) or won(₩) into symbols and numbers.     * 
     */
    
    // Test Script for RegularExpression
    [SerializeField] private TMP_InputField moneyInputField;      // Input Text. ex) $10,000
    [SerializeField] private TMP_Text returnText;   // Return Space
    
    public void ExecuteRegularExpression()
    {
        double money = 0;
        string inputMoney = moneyInputField.text;

        string pattern = @"[^\D]";  // Pattern of Remove Number

        string currency = Regex.Replace(inputMoney, pattern, "");
        currency = currency.Replace(",", "").Replace(".", "");  // , . 제거

        money = double.Parse(inputMoney.Replace(currency, "").Replace(",", ""));
        money = Convert.ToInt32(Math.Ceiling(money));

        string serverIAPLog = $"Return Currency : {currency}, Money : {money}";

        returnText.text = serverIAPLog;
    }

}




