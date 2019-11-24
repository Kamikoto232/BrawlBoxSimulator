using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Doozy.Engine.UI;
using Doozy.Engine.Nody;
using Doozy.Engine.Progress;
using System;

public class BoxItemPresenter : MonoBehaviour
{
    [ContextMenuItem("ShowBrawlerPower", "ShowBrawlerPower")]

    private static BoxItemPresenter instance;
    public GraphController graphController;
    public UIButton NextItemButton;
    public UIView BoxView, BrawlerView, BrawlerPowerView, CoinsView, GemsView, TicketsView, ObtainedView, MainMenuView;
    public UIView BoxBGView, BrawlerBGView, BrawlerPowerBGView, CoinsBGView, GemsBGView, TicketsBGView, ObtainedBGView;
    public Progressor BrawlerPowerProgr, CoinsProgr, GemsProgr, TicketsProgr, RemaingProgr;
    public Progressor ObtCoinsProgr, ObtGemsProgr, ObtTicketsProgr;
    public Image BoxImage;
    public Action OnNext;

    private GameObject[] obtBrawlerPowerObjects;


    private void Awake()
    {
        instance = this;
        //graphController = FindObjectOfType<GraphController>();
    }

    private void Start()
    {
        HideAllObtainedItems();
        ItemDistributor.OnGetItem += ShowItem;
    }

    public static void ShowObtained()
    {
        instance.HideAllItemViews();
        instance.graphController.GoToNodeByName("Obtained");
    }

    public static void HideObtained()
    {
        instance.graphController.GoToNodeByName("Main Menu");
        instance.HideAllObtainedItems();
    }

    private void HideAllObtainedItems()
    {
        //graphController.GoToNodeByName("Main Menu");
        //ObtBrawlerPowerProgr.gameObject.SetActive(false);
        ObtCoinsProgr.gameObject.SetActive(false);
        ObtGemsProgr.gameObject.SetActive(false);
        ObtTicketsProgr.gameObject.SetActive(false);
    }

    public static void ShowItem(ItemData itemData)
    {

        instance.HideAllItemViews();

        switch (itemData.ItemType)
        {
            case ItemData.Type.Brawler:
                instance.ShowBrawler();
                break;

            case ItemData.Type.BrawlerPower:
                instance.ShowBrawlerPower(itemData.Count);
                break;

            case ItemData.Type.Coins:
                instance.ShowCoins(itemData.Count);
                break;

            case ItemData.Type.Gems:
                instance.ShowGems(itemData.Count);
                break;

            case ItemData.Type.Tickets:
                instance.ShowTickets(itemData.Count);
                break;
        }
    }

    private void ShowBrawler()
    {
        BrawlerView.Show();
    }

    public static void ShowBox(Box box)
    {
        instance.graphController.GoToNodeByName("OpenBox");

        instance.BoxImage.sprite = box.BoxSprite;
    }

    private void ShowBrawlerPower(int Count)
    {
        graphController.GoToNodeByName("Brawler Power");
        BrawlerPowerProgr.SetValue(Count);
        //ObtBrawlerPowerProgr.SetValue(Count);
        //ObtBrawlerPowerProgr.gameObject.SetActive(true);
    }

    private void ShowCoins(int Count)
    {
        graphController.GoToNodeByName("Coins");
        CoinsProgr.SetValue(Count);
        ObtCoinsProgr.gameObject.SetActive(true);
        ObtCoinsProgr.SetValue(Count);
    }

    private void ShowGems(int Count)
    {
        graphController.GoToNodeByName("Gems");
        GemsProgr.SetValue(Count);
        ObtGemsProgr.gameObject.SetActive(true);
        ObtGemsProgr.SetValue(Count);
    }

    private void ShowTickets(int Count)
    {
        graphController.GoToNodeByName("Tickets");
        TicketsProgr.SetValue(Count);
        ObtTicketsProgr.gameObject.SetActive(true);
        ObtTicketsProgr.SetValue(Count);
    }

    private void HideAllItemViews()
    {
        BoxView.Hide();
        //BrawlerView.Hide();
        BrawlerPowerView.Hide();
        CoinsView.Hide();
        GemsView.Hide();
        TicketsView.Hide();
    }

    public static void OnClickNextSubscribe(Action Act)
    {
        instance.OnNext = Act;
    }

    public static void SetRemaingValue(int Value)
    {
        instance.RemaingProgr.SetValue(Value);
    }

    public void ClickNext() //UnityEventCall
    {
        OnNext.Invoke();
    } 

    private IEnumerator SetActiveWait(GameObject gObj, Action callback)
    {
        while(gObj.activeSelf == false)
        {
            yield return null;
        }

        callback.Invoke();
    }
}
