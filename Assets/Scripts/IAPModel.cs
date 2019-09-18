using UnityEngine;

/// <summary>
/// Statuses for InApp Purchase
/// </summary>
public enum PurchaseStatus { NotPresent, Empty, Purchased, Restored }

/// <summary>
/// Available inapps types. Only consumable is supported at the moment
/// </summary>
public enum PurchaseType { Consumable, NonConsumable, Subscription }

/// <summary>
/// A model class holding the data and status of the InApp purchase
/// </summary>
[System.Serializable]
public class ModelIAP  
{
    /// <summary>
    /// Key or key-base used for saving the data about an InApp into PlayerPrefs
    /// </summary>
    public string Key; // { get; set; }

    /// <summary>
    /// Current status of the purchase
    /// </summary>
    public PurchaseStatus Status; // { get; set; }

    /// <summary>
    /// InApp's price
    /// </summary>
    public float Price; // { get; set; }

    /// <summary>
    /// Id of the transaction, applicable only for Purchased and Restored statuses
    /// </summary>
    public string TransactionId; // { get; set; }

    /// <summary>
    /// The moment in time when the purchase was done (if successfully), represented in a string
    /// </summary>
    public string PurchasedAt; // { get; set; }
}
