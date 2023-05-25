using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class WeaponUpgradeShop : MonoBehaviour
{
    public PlayerController playerControllerScript;
    public ShopController shopControllerScript;

    public GameObject machineGunShop;
    public GameObject laserGunShop;
    public GameObject rocketLauncherShop;
    public GameObject randomShop;
    public GameObject weaponUpgradeShopMoneyDisplay;
    public TextMeshProUGUI weaponUpgradeShopMoneyText;

    private MachineGunDamageUpgradeItemOne machineGunDamageUpgradeItemOne;
    private MachineGunDamageUpgradeItemTwo machineGunDamageUpgradeItemTwo;
    private MachineGunDamageUpgradeItemThree machineGunDamageUpgradeItemThree;
    private MachineGunReloadUpgradeItemOne machineGunReloadUpgradeItemOne;
    private MachineGunReloadUpgradeItemTwo machineGunReloadUpgradeItemTwo;
    private MachineGunReloadUpgradeItemThree machineGunReloadUpgradeItemThree;
    private MachineGunFireUpgradeItem machineGunFireUpgradeItem;

    private LaserGunDamageUpgradeItemOne laserGunDamageUpgradeItemOne;
    private LaserGunDamageUpgradeItemTwo laserGunDamageUpgradeItemTwo;
    private LaserGunDamageUpgradeItemThree laserGunDamageUpgradeItemThree;
    private LaserGunCooldownUpgradeItemOne laserGunCooldownUpgradeItemOne;
    private LaserGunCooldownUpgradeItemTwo laserGunCooldownUpgradeItemTwo;
    private LaserGunCooldownUpgradeItemThree laserGunCooldownUpgradeItemThree;
    private LaserGunMaxDurationItem laserGunMaxDurationItem;

    private RocketLauncherDamageItemOne rocketLauncherDamageItemOne;
    private RocketLauncherDamageItemTwo rocketLauncherDamageItemTwo;
    private RocketLauncherDamageItemThree rocketLauncherDamageItemThree;
    private RocketLauncherEnviromentDamageItemOne rocketLauncherEnviromentDamageItemOne;
    private RocketLauncherEnviromentDamageItemTwo rocketLauncherEnviromentDamageItemTwo;
    private RocketLauncherEnviromentDamageItemThree rocketLauncherEnviromentDamageItemThree;
    private RocketLauncherRadiusItem rocketLauncherRadiusItem;

    private void Start()
    {

        //initiat all weapon shop upgrade items
        machineGunDamageUpgradeItemOne = new MachineGunDamageUpgradeItemOne("Increased Damage", 360, null, false);
        machineGunDamageUpgradeItemTwo = new MachineGunDamageUpgradeItemTwo("Increased Damage", 920, null, false);
        machineGunDamageUpgradeItemThree = new MachineGunDamageUpgradeItemThree("Increased Damage", 2100, null, false);
        machineGunReloadUpgradeItemOne = new MachineGunReloadUpgradeItemOne("Decreased Reload Time", 250, null, false);
        machineGunReloadUpgradeItemTwo = new MachineGunReloadUpgradeItemTwo("Decreased Reload Time", 650, null, false);
        machineGunReloadUpgradeItemThree = new MachineGunReloadUpgradeItemThree("Decreased Reload Time", 1450, null, false);
        machineGunFireUpgradeItem = new MachineGunFireUpgradeItem("permanend Fire", 3700, null, false);

        laserGunDamageUpgradeItemOne = new LaserGunDamageUpgradeItemOne("Increased Damage", 320, null, false);
        laserGunDamageUpgradeItemTwo = new LaserGunDamageUpgradeItemTwo("Increased Damage", 1000, null, false);
        laserGunDamageUpgradeItemThree = new LaserGunDamageUpgradeItemThree("Increased Damage", 2250, null, false);
        laserGunCooldownUpgradeItemOne = new LaserGunCooldownUpgradeItemOne("Cooldown reduction", 250, null, false);
        laserGunCooldownUpgradeItemTwo = new LaserGunCooldownUpgradeItemTwo("Cooldown reduction", 650, null, false);
        laserGunCooldownUpgradeItemThree = new LaserGunCooldownUpgradeItemThree("Cooldown reduction", 1450, null, false);
        laserGunMaxDurationItem = new LaserGunMaxDurationItem("Inceased Duration", 2700, null, false);

        rocketLauncherDamageItemOne = new RocketLauncherDamageItemOne("Increased Damage", 350, null, false);
        rocketLauncherDamageItemTwo = new RocketLauncherDamageItemTwo("Increased Damage", 900, null, false);
        rocketLauncherDamageItemThree = new RocketLauncherDamageItemThree("Increased Damage", 1900, null, false);
        rocketLauncherEnviromentDamageItemOne = new RocketLauncherEnviromentDamageItemOne("Increased Enviroment Damage", 380, null, false);
        rocketLauncherEnviromentDamageItemTwo = new RocketLauncherEnviromentDamageItemTwo("Increased Enviroment Damage", 1200, null, false);
        rocketLauncherEnviromentDamageItemThree = new RocketLauncherEnviromentDamageItemThree("Increased Enviroment Damage", 2300, null, false);
        rocketLauncherRadiusItem = new RocketLauncherRadiusItem("Increased Damage Radius", 3000, null, false);
    }

    private void Update()
    {
        weaponUpgradeShopMoneyText.text = playerControllerScript.money.ToString();
    }

    //checks wich weapon the player has equipped and opens the right shop
    public void SelectWeaponShop()
    {
        if(CollectWeapon.machineGunEquipped)
        {
            machineGunShop.SetActive(true);
        }
        else if(CollectWeapon.laserGunEquipped)
        {
            laserGunShop.SetActive(true);
        }
        else if(CollectWeapon.rocketLauncherEquipped)
        {
            rocketLauncherShop.SetActive(true);
        }
        randomShop.SetActive(false);
        weaponUpgradeShopMoneyDisplay.SetActive(true);
    }

    public void DeselectWeaponShop()
    {
        machineGunShop.SetActive(false);
        laserGunShop.SetActive(false);
        rocketLauncherShop.SetActive(false);
        randomShop.SetActive(true);
        weaponUpgradeShopMoneyDisplay.SetActive(false);
    }

    //upgrades for weapons can only be bought ones
    //after the butten is unuseble
    public void MakeButtonUnusebleAfterUse()
    {
        GameObject item = EventSystem.current.currentSelectedGameObject;
        Button itemButton = item.GetComponent<Button>();
        Image itemImage = item.GetComponent<Image>();
        itemButton.enabled = false;
        Color itemImageAlpha = itemImage.color;
        itemImageAlpha.a = 0.2f;
        itemImage.color = itemImageAlpha;
    }

    void InstallUpgradeItem(Items item)
    {
        if(playerControllerScript.money >= item.price)
        {
            playerControllerScript.money -= item.price;
            item.bought = true;
            item.ItemEffect();
            shopControllerScript.PopUpMessage(item, 3);
            MakeButtonUnusebleAfterUse();
        }
        else
        {
            shopControllerScript.PopUpMessage(item, 2);
        }
    }

    //------------ Machine Gun Upgrades ------------//
    public void MachineGunDamageUpgradeOne()
    {
        InstallUpgradeItem(machineGunDamageUpgradeItemOne);
    }

    public void MachineGunDamageUpgradeTwo()
    {
        if(machineGunDamageUpgradeItemOne.bought)
        {
            InstallUpgradeItem(machineGunDamageUpgradeItemTwo);
        }
    }

    public void MachineGunDamageUpgradeThree()
    {
        if(machineGunDamageUpgradeItemTwo.bought)
        {
            InstallUpgradeItem(machineGunDamageUpgradeItemThree);
        }
    }

    public void MachineGunReloadSpeedUpgradeOne()
    {
        InstallUpgradeItem(machineGunReloadUpgradeItemOne);
    }

    public void MachineGunReloadSpeedUpgradeTwo()
    {
        if(machineGunReloadUpgradeItemOne.bought)
        {
            InstallUpgradeItem(machineGunReloadUpgradeItemTwo);
        }
    }

    public void MachineGunReloadSpeedUpgradeThree()
    {
        if(machineGunReloadUpgradeItemTwo.bought)
        {
            InstallUpgradeItem(machineGunReloadUpgradeItemThree);
        }
    }

    public void MachineGunPermenandFireUpgrade()
    {
        if(machineGunReloadUpgradeItemThree.bought && machineGunDamageUpgradeItemThree.bought)
        {
            InstallUpgradeItem(machineGunFireUpgradeItem);
        }
    }

    //------------ Laser Gun Upgrades ------------//
    public void LaserGunDamageUpgradeOne()
    {
        InstallUpgradeItem(laserGunDamageUpgradeItemOne);
    }

    public void LaserGunDamageUpgradeTwo()
    {
        if(laserGunDamageUpgradeItemOne.bought)
        {
            InstallUpgradeItem(laserGunDamageUpgradeItemTwo);
        }
    }

    public void LaserGunDamageUpgradeThree()
    {
        if (laserGunDamageUpgradeItemTwo.bought)
        {
            InstallUpgradeItem(laserGunDamageUpgradeItemThree);
        }
    }

    public void LaserGunCooldownUpgradeOne()
    {
        InstallUpgradeItem(laserGunCooldownUpgradeItemOne);
    }

    public void LaserGunCooldownUpgradeTwo()
    {
        if(laserGunCooldownUpgradeItemOne.bought)
        {
            InstallUpgradeItem(laserGunCooldownUpgradeItemTwo);
        }
    }

    public void LaserGunCooldownUpgradeThree()
    {
        if(laserGunCooldownUpgradeItemTwo.bought)
        {
            InstallUpgradeItem(laserGunCooldownUpgradeItemThree);
        }
    }

    public void LaserGunMaxDurationUpgrade()
    {
        if(laserGunCooldownUpgradeItemThree.bought && laserGunDamageUpgradeItemThree.bought)
        {
            InstallUpgradeItem(laserGunMaxDurationItem);
        }
    }

    //------------ Rocket Launcher Upgrades ------------//
    public void RocketLauncherDamageUpgradeOne()
    {
        InstallUpgradeItem(rocketLauncherDamageItemOne);
    }

    public void RocketLauncherDamageUpgradeTwo()
    {
        if(rocketLauncherDamageItemOne.bought)
        {
            InstallUpgradeItem(rocketLauncherDamageItemTwo);
        }
    }

    public void RocketLauncherDamageUpgradeThree()
    {
        if(rocketLauncherDamageItemTwo.bought)
        {
            InstallUpgradeItem(rocketLauncherDamageItemThree);
        }
    }

    public void RocketLauncherEnviromentDamageUpgradeOne()
    {
        InstallUpgradeItem(rocketLauncherEnviromentDamageItemOne);
    }

    public void RocketLauncherEnviromentDamageUpgradeTwo()
    {
        if(rocketLauncherEnviromentDamageItemOne.bought)
        {
            InstallUpgradeItem(rocketLauncherEnviromentDamageItemTwo);
        }
    }

    public void RocketLauncherEnviromentDamageUpgradeThree()
    {
        if(rocketLauncherEnviromentDamageItemTwo.bought)
        {
            InstallUpgradeItem(rocketLauncherEnviromentDamageItemThree);
        }
    }

    public void RocketLauncherDamageRadiusUpgrade()
    {
        if(rocketLauncherEnviromentDamageItemThree.bought && rocketLauncherDamageItemThree.bought)
        {
            InstallUpgradeItem(rocketLauncherRadiusItem);
        }
    }
}

public class MachineGunDamageUpgradeItemOne : Items
{
    public MachineGunDamageUpgradeItemOne(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        GameObject machineGunBulletPrefab = Resources.Load("Prefab/Player/Weapon/MashineGunBullet") as GameObject;
        MashineGunBullet machmachineGunBulletScript = machineGunBulletPrefab.GetComponent<MashineGunBullet>();
        machmachineGunBulletScript.damage += 2;
    }
}

public class MachineGunDamageUpgradeItemTwo : Items
{
    public MachineGunDamageUpgradeItemTwo(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        GameObject machineGunBulletPrefab = Resources.Load("Prefab/Player/Weapon/MashineGunBullet") as GameObject;
        MashineGunBullet machmachineGunBulletScript = machineGunBulletPrefab.GetComponent<MashineGunBullet>();
        machmachineGunBulletScript.damage += 4;
    }
}

public class MachineGunDamageUpgradeItemThree : Items
{
    public MachineGunDamageUpgradeItemThree(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        GameObject machineGunBulletPrefab = Resources.Load("Prefab/Player/Weapon/MashineGunBullet") as GameObject;
        MashineGunBullet machmachineGunBulletScript = machineGunBulletPrefab.GetComponent<MashineGunBullet>();
        machmachineGunBulletScript.damage += 9;
    }
}

public class MachineGunReloadUpgradeItemOne : Items
{
    public MachineGunReloadUpgradeItemOne(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        Shooting shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        shootingScript.machineGunReloadSpeed -= 1f;
        shootingScript.stopReloading = true;
    }
}


public class MachineGunReloadUpgradeItemTwo : Items
{
    public MachineGunReloadUpgradeItemTwo(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        Shooting shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        shootingScript.machineGunReloadSpeed -= 1f;
        shootingScript.stopReloading = true;
    }
}

public class MachineGunReloadUpgradeItemThree : Items
{
    public MachineGunReloadUpgradeItemThree(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        Shooting shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        shootingScript.machineGunReloadSpeed -= 1.5f;
        shootingScript.stopReloading = true;
    }
}

public class MachineGunFireUpgradeItem : Items
{
    public MachineGunFireUpgradeItem(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        Shooting shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        shootingScript.permanendFire = true;
    }
}

public class LaserGunDamageUpgradeItemOne : Items
{
    public LaserGunDamageUpgradeItemOne(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        Laser laserScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Laser>();
        laserScript.laserDamage += 0.2f;
    }
}

public class LaserGunDamageUpgradeItemTwo : Items
{
    public LaserGunDamageUpgradeItemTwo(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        Laser laserScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Laser>();
        laserScript.laserDamage += 0.5f;
    }
}

public class LaserGunDamageUpgradeItemThree : Items
{
    public LaserGunDamageUpgradeItemThree(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        Laser laserScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Laser>();
        laserScript.laserDamage += 1.2f;
    }
}

public class LaserGunCooldownUpgradeItemOne : Items
{
    public LaserGunCooldownUpgradeItemOne(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        Laser laserScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Laser>();
        laserScript.coolDownSpeed += 0.4f;
    }
}

public class LaserGunCooldownUpgradeItemTwo : Items
{
    public LaserGunCooldownUpgradeItemTwo(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        Laser laserScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Laser>();
        laserScript.coolDownSpeed += 0.7f;
    }
}

public class LaserGunCooldownUpgradeItemThree : Items
{
    public LaserGunCooldownUpgradeItemThree(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        Laser laserScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Laser>();
        laserScript.coolDownSpeed += 1.4f;
    }
}

public class LaserGunMaxDurationItem : Items
{
    public LaserGunMaxDurationItem(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        Laser laserScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Laser>();
        laserScript.laserMaxTimer += 15;
        laserScript.laserCurrentTimer = laserScript.laserMaxTimer;

        Slider laserTimerSlider = laserScript.laserTimerUI.GetComponent<Slider>();
        laserTimerSlider.maxValue = laserScript.laserMaxTimer;
    }
}

public class RocketLauncherDamageItemOne : Items
{
    public RocketLauncherDamageItemOne(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        GameObject explosionBulletPrefab = Resources.Load("Prefab/Player/Weapon/ExplosionBullet") as GameObject;
        ExplosionBullet explosionBulletScript = explosionBulletPrefab.GetComponent<ExplosionBullet>();
        explosionBulletScript.damage += 2.5f;
    }
}

public class RocketLauncherDamageItemTwo : Items
{
    public RocketLauncherDamageItemTwo(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        GameObject explosionBulletPrefab = Resources.Load("Prefab/Player/Weapon/ExplosionBullet") as GameObject;
        ExplosionBullet explosionBulletScript = explosionBulletPrefab.GetComponent<ExplosionBullet>();
        explosionBulletScript.damage += 4.5f;
    }
}

public class RocketLauncherDamageItemThree : Items
{
    public RocketLauncherDamageItemThree(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        GameObject explosionBulletPrefab = Resources.Load("Prefab/Player/Weapon/ExplosionBullet") as GameObject;
        ExplosionBullet explosionBulletScript = explosionBulletPrefab.GetComponent<ExplosionBullet>();
        explosionBulletScript.damage += 9;
    }
}

public class RocketLauncherEnviromentDamageItemOne : Items
{
    public RocketLauncherEnviromentDamageItemOne(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        GameObject explosionBulletPrefab = Resources.Load("Prefab/Player/Weapon/ExplosionBullet") as GameObject;
        ExplosionBullet explosionBulletScript = explosionBulletPrefab.GetComponent<ExplosionBullet>();
        explosionBulletScript.enviromentalDamage += 0.5f;
    }
}

public class RocketLauncherEnviromentDamageItemTwo : Items
{
    public RocketLauncherEnviromentDamageItemTwo(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        GameObject explosionBulletPrefab = Resources.Load("Prefab/Player/Weapon/ExplosionBullet") as GameObject;
        ExplosionBullet explosionBulletScript = explosionBulletPrefab.GetComponent<ExplosionBullet>();
        explosionBulletScript.enviromentalDamage += 3.5f;
    }
}

public class RocketLauncherEnviromentDamageItemThree : Items
{
    public RocketLauncherEnviromentDamageItemThree(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        GameObject explosionBulletPrefab = Resources.Load("Prefab/Player/Weapon/ExplosionBullet") as GameObject;
        ExplosionBullet explosionBulletScript = explosionBulletPrefab.GetComponent<ExplosionBullet>();
        explosionBulletScript.enviromentalDamage += 4;
    }
}

public class RocketLauncherRadiusItem : Items
{
    public RocketLauncherRadiusItem(string name, float price, Sprite picture, bool bought)
         : base(name, price, picture, bought)
    {
    }
    public override void ItemEffect()
    {
        GameObject explosionBulletPrefab = Resources.Load("Prefab/Player/Weapon/ExplosionBullet") as GameObject;
        ExplosionBullet explosionBulletScript = explosionBulletPrefab.GetComponent<ExplosionBullet>();
        explosionBulletScript.radius += 2.5f;
    }
}