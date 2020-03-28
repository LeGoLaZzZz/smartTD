using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class BuildingShopButton : MonoBehaviour
{
    public BuildingType buildingType;
    public event Action<BuildingShopButton> ShopButtonClicked;
    public float currentPrice;


    [SerializeField] private Image icon;
    [SerializeField] private Text title;
    [SerializeField] private Text priceText;
    [SerializeField] private GameObject notEnabledObject;

    private Button _button;


    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        GoldManager.GetInstance().GoldAmountChanged += SetButtonEnable;
    }

    private void OnDisable()
    {
        GoldManager.GetInstance().GoldAmountChanged -= SetButtonEnable;
    }

    public void ClickOnButton()
    {
        OnShopButtonClicked(this);
    }


    public void SetButtonEnable(float gold)
    {
        _button.enabled = currentPrice <= gold;
        notEnabledObject.SetActive(!_button.enabled);
    }

    public void SetInfo(string titleText, Sprite iconSprite, BuildingType type, float price)
    {
        currentPrice = price;
        priceText.text = price.ToString();
        icon.sprite = iconSprite;
        title.text = titleText;
        buildingType = type;
    }

    protected virtual void OnShopButtonClicked(BuildingShopButton obj)
    {
        ShopButtonClicked?.Invoke(obj);
    }
}