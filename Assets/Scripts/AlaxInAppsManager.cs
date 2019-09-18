using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script managing all the inapp purcahses for the entire app.
/// Should be placed onto the empty GameObject in the Unity Editor.
/// </summary>
[System.Serializable]
public class AlaxInAppsManager : MonoBehaviour
{
    private const string _STATUS = "_status";
    private const string _TRANSACTION = "_trnsctn";
    private const string _DATE = "_datetime";
    private const string _PRICE = "_price";

    private ALAXPayWrapper _alaxGateway;
    private ModelIAP _currentInApp;

    /// <summary>
    /// A mandatory, if empty that means that the app has no inapps
    /// </summary>
    [SerializeField]
    public List<ModelIAP> InApps;

    /// <summary>
    /// A madatory field with the PublisherId
    /// </summary>
    public String PublisherId;

    /// <summary>
    /// The API Key of the publisher
    /// </summary>
    public String XApiKey;
    
    /// <summary>
    /// Singleton implementation.
    /// Can be used as a Unity Script as well as a Singletone.
    /// </summary>
    private static AlaxInAppsManager _instance;
    public static AlaxInAppsManager Instance 
    {
        get 
        {
            if (_instance == null)
                _instance = new AlaxInAppsManager();
            return _instance;
        }
    }


    /// <summary>
    /// The class constructor. 
    /// Required by singleton (to be used outside Unity, i.e. Xamarin.Android)
    /// </summary>
    AlaxInAppsManager() 
    {
        if (InApps == null)
            InApps = new List<ModelIAP>();
    }

    /// <summary>
    /// Returns a status for an inapp with the given key.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public ModelIAP GetInAppForKey(string key) 
    {
        foreach (var inapp in InApps)
        {
            if (key == inapp.Key)
                return inapp;
        }


        return null;
    }

    /// <summary>
    /// Returns a status for an inapp with the given key.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public PurchaseStatus GetStatusForKey(string key) 
    {
        foreach (var inapp in InApps)
        {
            if (key == inapp.Key)
                return inapp.Status;
        }
        return PurchaseStatus.NotPresent;
    }

    /// <summary>
    /// Returns a boolean saying whether or not the purchase has been made
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsPurchased(string key) 
    {
        var status = GetStatusForKey(key);

        if (status == PurchaseStatus.Purchased || status == PurchaseStatus.Restored)
            return true;

        return false;
    }

    /// <summary>
    /// Saves the current state of all the inapps into the PlayerPrefs
    /// </summary>
    public void SaveAll() 
    {
        string keyBase;
        string statusKey;
        string transactionKey;
        string dateKey;
        string priceKey;

        foreach (var inapp in InApps)
        {
            PrepareKeys(inapp.Key, out statusKey, out transactionKey, out dateKey, out priceKey);

            PlayerPrefs.SetInt(statusKey, (int)inapp.Status);
            PlayerPrefs.SetString(transactionKey, inapp.TransactionId);
            PlayerPrefs.SetString(dateKey, inapp.PurchasedAt);
            PlayerPrefs.SetString(priceKey, inapp.Price.ToString());
        }

        PlayerPrefs.Save();
    }

    private static void PrepareKeys(string keyBase, 
        out string statusKey, 
        out string transactionKey, 
        out string dateKey, 
        out string priceKey) 
    {
        statusKey = keyBase + _STATUS;
        transactionKey = keyBase + _TRANSACTION;
        dateKey = keyBase + _DATE;
        priceKey = keyBase + _PRICE;
    }

    /// <summary>
    /// Loads the inapps data from PlayerPrefs if available
    /// </summary>
    /// <param name="overwrite"></param>
    /// <returns>The amount of the InApps loaded from PlayerPrefs</returns>
    public int LoadInAppsFromPrefs(bool overwrite = false) 
    {
        string statusKey;
        string transactionKey;
        string dateKey;
        string priceKey;

        int loadedItemsCounter = 0;

        foreach (var inapp in InApps)
        {
            PrepareKeys(inapp.Key, out statusKey, out transactionKey, out dateKey, out priceKey);
            inapp.Status = (PurchaseStatus)PlayerPrefs.GetInt(statusKey);
            inapp.TransactionId = PlayerPrefs.GetString(transactionKey);
            inapp.PurchasedAt = PlayerPrefs.GetString(dateKey);
            float.TryParse(PlayerPrefs.GetString(priceKey), out inapp.Price);
            if (inapp.Status != PurchaseStatus.NotPresent && inapp.Status != PurchaseStatus.Empty)
                loadedItemsCounter++;
        }

        return loadedItemsCounter;
    }

    /// <summary>
    /// Starts the Purchase intent
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    /// 
    public bool StartPurchaseScenario(string key) {
        var inapp = GetInAppForKey(key);

        Debug.Log("Trying to purchase item with key: " + key);

        if (inapp.Status == PurchaseStatus.Purchased || inapp.Status == PurchaseStatus.Restored)
            return false;

        _currentInApp = inapp;

        _alaxGateway = new ALAXPayWrapper("io.alax.sdk.pay.$AlaxPay");
        _alaxGateway.ActivityReturnedResult += AlpGateway_ActivityReturnedResult;
        _alaxGateway.UIRequestTransferActivity(PublisherId, inapp.Price, XApiKey);

        return false;
    }

    //public void method for Unity inspector
    public void _StartPurchaseScenario(string key) { 
        StartPurchaseScenario(key);

        ButtonSpriteManager.lastTappedButton.ChangeButtonState(1); //Change button state to "Processing"
    }

    private void AlpGateway_ActivityReturnedResult(object sender, EventArgs e) 
    {
        string statusKey;
        string transactionKey;
        string dateKey;
        string priceKey;

        PrepareKeys(_currentInApp.Key, out statusKey, out transactionKey, out dateKey, out priceKey);

        PlayerPrefs.SetInt(statusKey, (int)_currentInApp.Status);
        PlayerPrefs.SetString(transactionKey, _currentInApp.TransactionId);
        PlayerPrefs.SetString(dateKey, _currentInApp.PurchasedAt);
        PlayerPrefs.SetString(priceKey, _currentInApp.Price.ToString());
        PlayerPrefs.Save();

        ButtonSpriteManager.lastTappedButton.ChangeButtonState(2); //Change button state to "Purchased"
    }

    public void Start() 
    {
       
    }
}