using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public Sprite[] itemsary;
    public GameObject firstItemWindow;
    public GameObject secondItemWindow;
    public GameObject thirdItemWindow;
    public Button extraLifeButton;
    public GameObject PopUpMessageUI;
    public TextMeshProUGUI moneyDisplayShopUI;
    public TextMeshProUGUI secondMoneyDisplayShopUI;
    public TextMeshProUGUI[] ItemCounterUI;

    private List<Items> itemsCollection = new List<Items>();

    private Items firstItem; 
    private Items secondItem;
    private Items thirdItem;
    private ExtraLifeItem extraLifeItem;

    private int[] ItemCounter = new int[5];

    private bool boughtItemOne;
    private bool boughtItemTwo;
    private bool boughtItemThree;

    private void Start()
    {
        //initiate all possible items
        HealthItem healthItem = new HealthItem("+10 max Health", 500, itemsary[0], 0);
        AmmoItem ammoItem = new AmmoItem("+5 Ammunition", 300, itemsary[1], 1);
        DamageItem firerateItem = new DamageItem("+1 Damage", 400, itemsary[2], 2);
        ReloadSpeedItem reloadSpeedItem = new ReloadSpeedItem("Decreased Reload Time", 600, itemsary[3], 3);
        MovementSpeedItem movementSpeedItem = new MovementSpeedItem("Inceased Movement Speed", 350, itemsary[4], 4);
        extraLifeItem = new ExtraLifeItem("1 extra life!", 10, itemsary[0], 5);


        //store all items
        itemsCollection.Add(healthItem);
        itemsCollection.Add(ammoItem);
        itemsCollection.Add(firerateItem);
        itemsCollection.Add(movementSpeedItem);
        itemsCollection.Add(reloadSpeedItem);

        for(int x = 0; x < 5; x++)
        {
            ItemCounter[x] = 0;
        }
    }

    private void Update()
    {
        DisplayMoney();
    }

    public void LoadShop()
    {
        boughtItemOne = false;
        boughtItemTwo = false;
        boughtItemThree = false;
        
        //get random three
        //but not three times the same
        firstItem = itemsCollection[Random.Range(0, itemsCollection.Count)];

        do
        {
            secondItem = itemsCollection[Random.Range(0, itemsCollection.Count)];
        } while (firstItem.name == secondItem.name);

        do
        {
            thirdItem = itemsCollection[Random.Range(0, itemsCollection.Count)];
        } while (firstItem.name == thirdItem.name || secondItem.name == thirdItem.name);

        //set gameobject name to item name - used for button identification
        firstItemWindow.name = firstItem.name;
        secondItemWindow.name = secondItem.name;
        thirdItemWindow.name = thirdItem.name;

        //adjust ui
        firstItemWindow.GetComponent<Image>().sprite = firstItem.picture;
        secondItemWindow.GetComponent<Image>().sprite = secondItem.picture;
        thirdItemWindow.GetComponent<Image>().sprite = thirdItem.picture;

        GameObject firstItemChild = firstItemWindow.transform.GetChild(0).gameObject;
        firstItemChild.GetComponent<TextMeshProUGUI>().text = firstItem.name;

        firstItemChild = firstItemWindow.transform.GetChild(1).gameObject;
        firstItemChild.GetComponent<TextMeshProUGUI>().text = firstItem.price.ToString();

        GameObject secondItemChild = secondItemWindow.transform.GetChild(0).gameObject;
        secondItemChild.GetComponent<TextMeshProUGUI>().text = secondItem.name;

        secondItemChild = secondItemWindow.transform.GetChild(1).gameObject;
        secondItemChild.GetComponent<TextMeshProUGUI>().text = secondItem.price.ToString();

        GameObject thirdItemChild = thirdItemWindow.transform.GetChild(0).gameObject;
        thirdItemChild.GetComponent<TextMeshProUGUI>().text = thirdItem.name;

        thirdItemChild = thirdItemWindow.transform.GetChild(1).gameObject;
        thirdItemChild.GetComponent<TextMeshProUGUI>().text = thirdItem.price.ToString();
    }

    public void GetSelectedItem()
    {
        string itemName = EventSystem.current.currentSelectedGameObject.name;

        if(itemName == firstItem.name)
        {
            if(boughtItemOne == false)
            {
                InstallItem(firstItem);
                boughtItemOne = true;
            }
            else
            {
                Debug.Log(firstItem.name + "allready bought");
                PopUpMessage(firstItem, 1);
            }
        }
        else if(itemName == secondItem.name)
        {
            if (boughtItemTwo == false)
            {
                InstallItem(secondItem);
                boughtItemTwo = true;
            }
            else
            {
                Debug.Log(secondItem.name + "allready bought");
                PopUpMessage(secondItem, 1);
            }
        }
        else if(itemName == thirdItem.name)
        {
            if(boughtItemThree == false)
            {
                InstallItem(thirdItem);
                boughtItemThree = true;
            }
            else
            {
                Debug.Log(thirdItem.name + "allready bought");
                PopUpMessage(thirdItem, 1);
            }
        }
        else
        {
            Debug.Log("Item couldnt be found");
        }
    }

    void InstallItem(Items item)
    {
        //Check money
        if(PlayerController.money >= item.price)
        {
            Debug.Log(item.name + " purchased");
            PopUpMessage(item, 3);
            PlayerController.money -= item.price;
            item.ItemEffect();
            item.price += 150;
            if(item.id < ItemCounterUI.Length)
            {
                ItemCounter[item.id] += 1;
                ItemCounterUI[item.id].text = ItemCounter[item.id].ToString();
            }
        }
        else
        {
            Debug.Log("not enough money");
            PopUpMessage(item, 2);
        }

    }

    void PopUpMessage(Items item, int messageID)
    {
        PopUpMessageUI.SetActive(true);
        GameObject PopUpMessageText = PopUpMessageUI.transform.GetChild(0).gameObject;
        if(messageID == 1)
        {
            PopUpMessageText.GetComponent<TextMeshProUGUI>().text = "You allready bought " + item.name + "!";
        }
        else if(messageID == 2)
        {
            PopUpMessageText.GetComponent<TextMeshProUGUI>().text = "You dont have enough money to by " + item.name + "!";
        }
        else if(messageID == 3)
        {
            PopUpMessageText.GetComponent<TextMeshProUGUI>().text = item.name + " has been purchased!";
        }
    }

    public void AddExtraLife()
    {
        InstallItem(extraLifeItem);
        extraLifeButton.enabled = false;
    }

    void DisplayMoney()
    {
        moneyDisplayShopUI.text = PlayerController.money.ToString();
        secondMoneyDisplayShopUI.text = PlayerController.money.ToString();
    }
}

public class Items
{
    public int id;
    public string name;
    public float price;
    public Sprite picture;

    public Items(string name, float price, Sprite picture, int id)
    {
        this.id = id;
        this.name = name;
        this.price = price;
        this.picture = picture;
    }

    public virtual void ItemEffect()
    {
    }

    public float GetPrice()
    {
        return price;
    }

    public void SetPrice(float price)
    {
        this.price = price;
    }

    public string GetName()
    {
        return name;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public Sprite GetPicture()
    {
        return picture;
    }

    public void SetPicture(Sprite picture)
    {
        this.picture = picture;
    }

    public int GetId()
    {
        return id;
    }

    public void SetId(int id)
    {
        this.id = id;
    }

}

public class HealthItem : Items
{
    public HealthItem(string name, float price, Sprite picture, int id)
         :base(name, price, picture, id)
    {
    }
    public override void ItemEffect()
    {
        PlayerController.maxHealth += 10;
    }
}

public class AmmoItem : Items
{
    public AmmoItem(string name, float price, Sprite picture, int id)
         : base(name, price, picture, id)
    {
    }

    public override void ItemEffect()
    {
        Shooting.maxAmmo += 5;
    }
}

public class DamageItem : Items
{
    public DamageItem(string name, float price, Sprite picture, int id)
         : base(name, price, picture, id)
    {
    }

    public override void ItemEffect()
    {
        Enemy_Health.damageTaken += 1;
    }
}

public class MovementSpeedItem : Items
{
    public MovementSpeedItem(string name, float price, Sprite picture, int id)
         : base(name, price, picture, id)
    {
    }

    public override void ItemEffect()
    {
        PlayerController.actualMoveSpeed += 0.1f;
    }
}

public class ReloadSpeedItem : Items
{
    public ReloadSpeedItem(string name, float price, Sprite picture, int id)
         : base(name, price, picture, id)
    {
    }

    public override void ItemEffect()
    {
        Shooting.reloadSpeed -= 0.2f;
    }
}

public class ExtraLifeItem: Items
{
    public ExtraLifeItem(string name, float price, Sprite picture, int id)
        : base(name, price, picture, id)
    {
    }

    public override void ItemEffect()
    {
        PlayerController.extraLife = true;
    }
}
