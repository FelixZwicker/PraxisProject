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
    public TextMeshProUGUI moneyDisplayShopUI;

    private List<Items> itemsCollection = new List<Items>();

    private Items firstItem; 
    private Items secondItem;
    private Items thirdItem;

    private void Start()
    {
        //initiate all possible items
        HealthItem healthItem = new HealthItem("+10 max Health", 500, itemsary[0]);
        AmmoItem ammoItem = new AmmoItem("+5 Ammunition", 300, itemsary[1]);
        FirerateItem firerateItem = new FirerateItem("Increased Firerate", 400, itemsary[2]);


        //store all items
        itemsCollection.Add(healthItem);
        itemsCollection.Add(ammoItem);
        itemsCollection.Add(firerateItem);
    }

    private void Update()
    {
        DisplayMoney();
    }

    public void LoadShop()
    {
        //get random three
        firstItem = itemsCollection[Random.Range(0, itemsCollection.Count)];
        secondItem = itemsCollection[Random.Range(0, itemsCollection.Count)];
        thirdItem = itemsCollection[Random.Range(0, itemsCollection.Count)];

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
            InstallItem(firstItem);
        }
        else if(itemName == secondItem.name)
        {
            InstallItem(secondItem);
        }
        else if(itemName == thirdItem.name)
        {
            InstallItem(thirdItem);
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
            PlayerController.money -= item.price;
            item.ItemEffect();
        }
        else
        {
            Debug.Log("not enough money");
        }

    }

    void DisplayMoney()
    {
        moneyDisplayShopUI.text = PlayerController.money.ToString() + " €";
    }
}

public class Items
{
    public string name;
    public float price;
    public Sprite picture;

    public Items(string name, float price, Sprite picture)
    {
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

}

public class HealthItem : Items
{
    public HealthItem(string name, float price, Sprite picture)
         :base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        PlayerController.maxHealth += 10;
    }
}

public class AmmoItem : Items
{
    public AmmoItem(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }

    public override void ItemEffect()
    {
        Shooting.maxAmmo += 5;
    }
}

public class FirerateItem : Items
{
    public FirerateItem(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }

    public override void ItemEffect()
    {
        Shooting.bulletForce += 0.5f;
    }
}
