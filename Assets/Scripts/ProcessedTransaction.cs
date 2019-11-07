using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;


[System.Serializable]
class ProcessedTransaction {

    /// <summary>
    /// 
    /// </summary>
    public List<string> Signatures;

    /// <summary>
    /// 
    /// </summary>
    public List<AndroidJavaObject> Extensions;

    /// <summary>
    /// 
    /// </summary>
    public List<AndroidJavaObject> Opertations;

    /// <summary>
    /// 
    /// </summary>
    public string Expiration;

    /// <summary>
    /// 
    /// </summary>
    public int RefBlockNum;

    /// <summary>
    /// 
    /// </summary>
    public long RefBlockPrefix;

    /// <summary>
    /// 
    /// </summary>
    public List<AndroidJavaObject> OperationResults;

    /// <summary>
    /// Mapping from Android Java Object into the current object instance fields
    /// </summary>
    /// <param name="jObject"></param>
    public void MapFromJavaObject(AndroidJavaObject jObject) {
        try
        {
            Signatures = jObject.Get<List<string>>("signatures");
            Debug.Log(Signatures.ToString());
        }

        catch (Exception ex)
        {
            Debug.Log("ERROR: " + ex.Message);
        }
    }

}