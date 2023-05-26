using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public PlayerController playerControllerScript;
    public GrenadeHandler grenadeHandlerSkript;
    public Sprite[] itemsary;
    public GameObject firstItemWindow;
    public GameObject secondItemWindow;
    public GameObject thirdItemWindow;
    public Button extraLifeButton;
    public GameObject extraLifeIndicator;
    public GameObject aBombUI;
    public GameObject popUpMessageUI;
    public TextMeshProUGUI moneyDisplayShopUI;
    public TextMeshProUGUI secondMoneyDisplayShopUI;
    public TextMeshProUGUI aBombPrice;
    public TextMeshProUGUI stunGrenadePrice;

    public bool healEquipped = false;

    private List<Items> itemsCollection = new List<Items>();

    private Items firstItem; 
    private Items secondItem;
    private Items thirdItem;
    private ExtraLifeItem extraLifeItem;
    private StunGrenadeItem stunGrenadeItem;
    private ABombItem aBombItem;

    private bool boughtItemOne;
    private bool boughtItemTwo;
    private bool boughtItemThree;

    private void Start()
    {
        //initiate all possible items
        HealthItem healthItem = new HealthItem("+10 max Health", 350, itemsary[0]);
        HealBoxItem healBoxItem = new HealBoxItem("One time full Heal", 50, itemsary[0]);
        MachineGunAmmoItem machineGunAmmoItem = new MachineGunAmmoItem("+5 Machine Gun Ammo", 300, itemsary[2]);
        RocketLauncherAmmoItem rocketLauncherAmmoItem = new RocketLauncherAmmoItem("+2 Rocket Launcher Ammo", 250, itemsary[1]);
        MoveSpeedItem moveSpeedItem = new MoveSpeedItem("Movement Speed", 100, itemsary[3]);
        extraLifeItem = new ExtraLifeItem("Extra Life", 3500, itemsary[0]);
        stunGrenadeItem = new StunGrenadeItem("Stun Grenade", 50, itemsary[0]);
        aBombItem = new ABombItem("ABomb", 600, itemsary[0]);


        //store all items
        itemsCollection.Add(healthItem);
        itemsCollection.Add(healBoxItem);
        itemsCollection.Add(machineGunAmmoItem);
        itemsCollection.Add(rocketLauncherAmmoItem);
        itemsCollection.Add(moveSpeedItem);
    }

    private void Update()
    {
        DisplayMoney();
    }

    public void LoadShop()
    {
        //------------ random item shop ------------//

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
        if(playerControllerScript.money >= item.price && (item.name != "One time full Heal" || !healEquipped))
        {
            PopUpMessage(item, 3);
            playerControllerScript.money -= item.price;
            item.ItemEffect();
            IncreasePrice(item);
            if(item.name == "Extra Life")
            {
                extraLifeIndicator.SetActive(true);
                extraLifeButton.enabled = false;
            }
        }
        else if(healEquipped && item.name != "One time full Heal")
        {
            PopUpMessage(item, 1);
        }
        else
        {
            PopUpMessage(item, 2);
        }

    }

    void IncreasePrice(Items item)
    {
        if(item.name == "One time full Heal")
        {
            item.price += 150;
            healEquipped = true;
        }
        else if(item.name == "Stun Grenade")
        {
            item.price += 30;
            stunGrenadePrice.text = item.price.ToString();
        }
        else if(item.name == "ABomb")
        {
            item.price += 250;
            aBombPrice.text = item.price.ToString();
        }
        else
        {
            item.price += 450;
        }
    }

    public void PopUpMessage(Items item, int messageID)
    {
        popUpMessageUI.SetActive(true);
        GameObject PopUpMessageText = popUpMessageUI.transform.GetChild(0).gameObject;
        if(messageID == 1)
        {
            PopUpMessageText.GetComponent<TextMeshProUGUI>().text = "You allready bought " + item.name + "!";
        }
        else if(messageID == 2)
        {
            PopUpMessageText.GetComponent<TextMeshProUGUI>().text = "You dont have enough money to buy " + item.name + "!";
        }
        else if(messageID == 3)
        {
            PopUpMessageText.GetComponent<TextMeshProUGUI>().text = item.name + " has been purchased!";
        }
    }

    public void AddExtraLife()
    {
        InstallItem(extraLifeItem);
    }

    public void AddStunGrenade()
    {
        if(grenadeHandlerSkript.currentGrenades < grenadeHandlerSkript.maxGrenades)
        {
            InstallItem(stunGrenadeItem);
        }
    }

    public void AddABomb()
    {
        if (!grenadeHandlerSkript.aBombisCurrentlyEquipped)
        {
            InstallItem(aBombItem);
            Color aBombAlpha = aBombUI.GetComponent<Image>().color;
            aBombAlpha.a = 1;
            aBombUI.GetComponent<Image>().color = aBombAlpha;
        }
        else
        {
            PopUpMessage(aBombItem, 1);
        }
    }

    void DisplayMoney()
    {
        moneyDisplayShopUI.text = playerControllerScript.money.ToString();
        secondMoneyDisplayShopUI.text = playerControllerScript.money.ToString();
    }
}

//class used to creat all possible items with name price and sprite
public class Items
{
    public string name;
    public float price;
    public Sprite picture;
    public bool bought;

    public Items(string name, float price, Sprite picture, bool bought)
    {
        this.name = name;
        this.price = price;
        this.picture = picture;
        this.bought = bought;
    }

    public virtual void ItemEffect()
    {
    }
}

public class HealthItem : Items
{
    public HealthItem(string name, float price, Sprite picture, bool bought = false)
         :base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        PlayerController playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerControllerScript.maxHealth += 10;
        playerControllerScript.currentHealth = playerControllerScript.maxHealth;
    }
}

public class HealBoxItem : Items
{
    public HealBoxItem(string name, float price, Sprite picture, bool bought = false)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        PlayerController playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerControllerScript.canUseHeal = true;
    }
}

public class MachineGunAmmoItem : Items
{
    public MachineGunAmmoItem(string name, float price, Sprite picture, bool bought = false)
         : base(name, price, picture, bought)
    {
    }

    public override void ItemEffect()
    {
        Shooting ShootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        ShootingScript.maxMachineGunAmmo += 5;
    }
}

public class RocketLauncherAmmoItem : Items
{
    public RocketLauncherAmmoItem(string name, float price, Sprite picture, bool bought = false)
        : base(name, price, picture, bought)
    {
    }

    public override void ItemEffect()
    {
        Shooting ShootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        ShootingScript.maxRocketLauncherAmmo += 2;
    }
}

public class ExtraLifeItem: Items
{
    public ExtraLifeItem(string name, float price, Sprite picture, bool bought = false)
         : base(name, price, picture, bought)
    {
    }

    public override void ItemEffect()
    {
        PlayerController playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerControllerScript.extraLife = true;
    }
}

public class StunGrenadeItem : Items
{
    public StunGrenadeItem(string name, float price, Sprite picture, bool bought = false)
         : base(name, price, picture, bought)
    {
    }

    public override void ItemEffect()
    {
        GrenadeHandler grenadeHandlerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<GrenadeHandler>();
        grenadeHandlerScript.currentGrenades += 1;
    }
}

public class ABombItem : Items
{
    public ABombItem(string name, float price, Sprite picture, bool bought = false)
         : base(name, price, picture, bought)
    {
    }

    public override void ItemEffect()
    {
        GrenadeHandler grenadeHandlerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<GrenadeHandler>();
        grenadeHandlerScript.aBombisCurrentlyEquipped = true;
    }
}

public class MoveSpeedItem : Items
{
    public MoveSpeedItem(string name, float price, Sprite picture, bool bought = false)
         : base(name, price, picture, bought)
    {
    }

    public override void ItemEffect()
    {
        PlayerController playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerControllerScript.moveSpeed += 0.2f;
        playerControllerScript.actualMoveSpeed += 0.2f;
    }
}