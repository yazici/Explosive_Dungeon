using UnityEngine;
using UnityEngine.Purchasing;

public class MyIAPManager : MonoBehaviour, IStoreListener {

    private IStoreController controller;
    private IExtensionProvider extensions;

    void Start () {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct("disable_ads", ProductType.NonConsumable);

        UnityPurchasing.Initialize (this, builder);
    }public void OnInitialized (IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;
    }public void OnInitializeFailed (InitializationFailureReason error)
    {
    } public PurchaseProcessingResult ProcessPurchase (PurchaseEventArgs e)
    {
        return PurchaseProcessingResult.Complete;
    }public void OnPurchaseFailed (Product i, PurchaseFailureReason p)
    {
    }
}