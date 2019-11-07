using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

/// <summary>
/// Entry points wrapper for ALAXPay SDK
/// </summary>
public class ALAXPayWrapper 
{
    private static string _blockNum;
    private static string _trxInBlock;
    private static bool _isParsed;
    private static AndroidJavaObject _transactionConfirmation;

    public static string CurrentBundleId;

    public event EventHandler ActivityReturnedResult;
    public event EventHandler VerificationReturnedResult;

    static AndroidJavaClass AlaxPayClass = new AndroidJavaClass("io.alax.sdk.pay.CallHelper");
    static AndroidJavaClass AlaxParseActivity = new AndroidJavaClass("io.alax.sdk.pay.ALAXParseResponseActivity");

    public class ParsedCallback : AndroidJavaProxy {
        public ParsedCallback() : base("io.alax.sdk.pay.ALAXParseListener") { }

        public void onParsed(string blockNum, string trxInBlock, bool isParsed, AndroidJavaObject transactionConfirmation)  
        {
            _blockNum = blockNum;
            _trxInBlock = trxInBlock;
            _isParsed = isParsed;
            _transactionConfirmation = transactionConfirmation;
        }
    }


    public ALAXPayWrapper() 
    {
        Init();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Init() 
    {
        AlaxPayClass.CallStatic("init");
        Debug.Log("ALAX init: DONE!");
    }

    /// <summary>
    /// A Listener for a native android event
    /// </summary>
    /// <param name="requestCode"></param>
    /// <param name="resultCode"></param>
    /// <param name="resultIntent"></param>
    /*public void onActivityResult(int requestCode, int resultCode, AndroidJavaClass resultIntent) 
    {
        Debug.Log("Transfer complete!");
        EventHandler handler = ActivityReturnedResult;
        
        // IF RESULT_OK
        if (resultCode == -1)
            handler?.Invoke(this, new EventArgs());
    }*/


    /// <summary>
    /// 
    /// </summary>
    public void UIRequestTransferActivity(string publisherId, float cost, string xApiKey, string inAppBundleId = null) 
    {
        if (inAppBundleId != null)
            CurrentBundleId = inAppBundleId;
        
        Debug.Log("Pre-requesting transfer activity...");

        string assetType = "AIAT";

        AndroidJavaObject transferInputObject = AlaxPayClass.CallStatic<AndroidJavaObject>("buildTransferInput", publisherId, cost, assetType, xApiKey);

        Debug.Log("Requesting transfer activity.");

        //AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //AndroidJavaObject currentActivity = new AndroidJava// unityClass.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject parserActivity = new AndroidJavaObject("io.alax.sdk.pay.ALAXParseResponseActivity");
        parserActivity.Call("setListener", new ParsedCallback());
        AlaxPayClass.CallStatic("requestTransferActivity", transferInputObject, parserActivity); 
       
        Debug.Log("After requesting transfer activity.");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public AndroidJavaObject UIParseActivityResult() 
    {
        AndroidJavaObject transactionObj = new AndroidJavaObject("io.alax.sdk.pay.model.TransferInput");
        Debug.Log("Requesting pasrse activity");
        AlaxPayClass.CallStatic("io.alax.sdk.pay.AlaxPay$Ui.RequestTransferActivity", new object[1] { transactionObj });
        Debug.Log("After requesting parse transfer activity");
        return transactionObj;
    } 

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public  string UIParseActivityError() 
    {
        return "";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public AndroidJavaObject APIVerifyTransfer() 
    {

        return null;
    }

}
