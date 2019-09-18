using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

/// <summary>
/// Entry points wrapper for ALAXPay SDK
/// </summary>
public class ALAXPayWrapper : AndroidJavaProxy
{
    public static string CurrentBundleId;

    public event EventHandler ActivityReturnedResult;

    AndroidJavaClass AlaxPayClass = new AndroidJavaClass("io.alax.sdk.pay.$AlaxPay");
    AndroidJavaClass UIClass = new AndroidJavaClass("io.alax.sdk.pay.$AlaxPay$Ui");
    AndroidJavaClass APIClass = new AndroidJavaClass("io.alax.sdk.pay.$AlaxPay$Api");

    public ALAXPayWrapper(string javaInterface) : base("io.alax.sdk.pay.$AlaxPay") 
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public void Init() { }

    /// <summary>
    /// A Listener for a native android event
    /// </summary>
    /// <param name="requestCode"></param>
    /// <param name="resultCode"></param>
    /// <param name="resultIntent"></param>
    public void onActivityResult(int requestCode, int resultCode, AndroidJavaClass resultIntent) 
    {
        Debug.Log("Transfer complete!");
        EventHandler handler = ActivityReturnedResult;
        
        // IF RESULT_OK
        if (resultCode == -1)
            handler?.Invoke(this, new EventArgs());
    }

    

    /// <summary>
    /// 
    /// </summary>
    public void UIRequestTransferActivity(string publisherId, float cost, string xApiKey, string inAppBundleId = null) 
    {
        if (inAppBundleId != null)
            CurrentBundleId = inAppBundleId;

        AlaxPayClass.CallStatic("io.alax.sdk.pay.$AlaxPay$Ui$RequestTransferActivity", 
            new AndroidJavaObject("TransferInput", new object[3] { publisherId, cost, xApiKey })); 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public AndroidJavaObject UIParseActivityResult() 
    {
        AndroidJavaObject transactionObj = new AndroidJavaObject("io.alax.sdk.pay.$TransferInput");
        AlaxPayClass.CallStatic("io.alax.sdk.pay.$AlaxPay$Ui$RequestTransferActivity", new object[1] { new object() });
        return transactionObj;
    } 

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string UIParseActivityError() 
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
