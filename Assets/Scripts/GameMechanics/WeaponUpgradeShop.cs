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

    public GameObject MachineGunShop;
    public GameObject LaserGunShop;
    public GameObject RocketLauncherShop;
    public GameObject RandomShop;
    public GameObject WeaponUpgradeShopMoneyDisplay;
    public TextMeshProUGUI WeaponUpgradeShopMoneyText;

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
        machineGunDamageUpgradeItemOne = new MachineGunDamageUpgradeItemOne("+1 Damage", 10, null);
        machineGunDamageUpgradeItemTwo = new MachineGunDamageUpgradeItemTwo("+5 Damage", 10, null);
        machineGunDamageUpgradeItemThree = new MachineGunDamageUpgradeItemThree("+10 Damage", 10, null);
        machineGunReloadUpgradeItemOne = new MachineGunReloadUpgradeItemOne("-1s reload Time", 10, null);
        machineGunReloadUpgradeItemTwo = new MachineGunReloadUpgradeItemTwo("-2.5s reload Time", 10, null);
        machineGunReloadUpgradeItemThree = new MachineGunReloadUpgradeItemThree("-5s reload Time", 10, null);
        machineGunFireUpgradeItem = new MachineGunFireUpgradeItem("permanend Fire", 10, null);

        laserGunDamageUpgradeItemOne = new LaserGunDamageUpgradeItemOne("+0.2 Damage", 10, null);
        laserGunDamageUpgradeItemTwo = new LaserGunDamageUpgradeItemTwo("+0.2 Damage", 10, null);
        laserGunDamageUpgradeItemThree = new LaserGunDamageUpgradeItemThree("+0.2 Damage", 10, null);
        laserGunCooldownUpgradeItemOne = new LaserGunCooldownUpgradeItemOne("-0.4 Cooldown reduction", 10, null);
        laserGunCooldownUpgradeItemTwo = new LaserGunCooldownUpgradeItemTwo("-0.5 Cooldown reduction", 10, null);
        laserGunCooldownUpgradeItemThree = new LaserGunCooldownUpgradeItemThree("-0.5 Cooldown reduction", 10, null);
        laserGunMaxDurationItem = new LaserGunMaxDurationItem("+12 Laser Duration", 10, null);

        rocketLauncherDamageItemOne = new RocketLauncherDamageItemOne("+1 Damage", 10, null);
        rocketLauncherDamageItemTwo = new RocketLauncherDamageItemTwo("+3 Damage", 10, null);
        rocketLauncherDamageItemThree = new RocketLauncherDamageItemThree("+6 Damage", 10, null);
        rocketLauncherEnviromentDamageItemOne = new RocketLauncherEnviromentDamageItemOne("+1 Damage", 10, null);
        rocketLauncherEnviromentDamageItemTwo = new RocketLauncherEnviromentDamageItemTwo("+1.5 Damage", 10, null);
        rocketLauncherEnviromentDamageItemThree = new RocketLauncherEnviromentDamageItemThree("+2.5 Damage", 10, null);
        rocketLauncherRadiusItem = new RocketLauncherRadiusItem("+2.5 Radius", 10, null);
    }

    private void Update()
    {
        WeaponUpgradeShopMoneyText.text = playerControllerScript.money.ToString();
    }

    public void SelectWeaponShop()
    {
        if(CollectWeapon.MachineGunEquipped)
        {
            MachineGunShop.SetActive(true);
        }
        else if(CollectWeapon.LaserGunEquipped)
        {
            LaserGunShop.SetActive(true);
        }
        else if(CollectWeapon.RocketLauncherEquipped)
        {
            RocketLauncherShop.SetActive(true);
        }
        RandomShop.SetActive(false);
        WeaponUpgradeShopMoneyDisplay.SetActive(true);
    }

    public void DeselectWeaponShop()
    {
        MachineGunShop.SetActive(false);
        LaserGunShop.SetActive(false);
        RocketLauncherShop.SetActive(false);
        RandomShop.SetActive(true);
        WeaponUpgradeShopMoneyDisplay.SetActive(false);
    }

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
        if(playerControllerScript.money > item.price)
        {
            playerControllerScript.money -= item.price;
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
        InstallUpgradeItem(machineGunDamageUpgradeItemTwo);
    }

    public void MachineGunDamageUpgradeThree()
    {
        InstallUpgradeItem(machineGunDamageUpgradeItemThree);
    }

    public void MachineGunReloadSpeedUpgradeOne()
    {
        InstallUpgradeItem(machineGunReloadUpgradeItemOne);
    }

    public void MachineGunReloadSpeedUpgradeTwo()
    {
        InstallUpgradeItem(machineGunReloadUpgradeItemTwo);
    }

    public void MachineGunReloadSpeedUpgradeThree()
    {
        InstallUpgradeItem(machineGunReloadUpgradeItemThree);
    }

    public void MachineGunPermenandFireUpgrade()
    {
        InstallUpgradeItem(machineGunFireUpgradeItem);
    }

    //------------ Laser Gun Upgrades ------------//
    public void LaserGunDamageUpgradeOne()
    {
        InstallUpgradeItem(laserGunDamageUpgradeItemOne);
    }

    public void LaserGunDamageUpgradeTwo()
    {
        InstallUpgradeItem(laserGunDamageUpgradeItemTwo);
    }

    public void LaserGunDamageUpgradeThree()
    {
        InstallUpgradeItem(laserGunDamageUpgradeItemThree);
    }

    public void LaserGunCooldownUpgradeOne()
    {
        InstallUpgradeItem(laserGunCooldownUpgradeItemOne);
    }

    public void LaserGunCooldownUpgradeTwo()
    {
        InstallUpgradeItem(laserGunCooldownUpgradeItemTwo);
    }

    public void LaserGunCooldownUpgradeThree()
    {
        InstallUpgradeItem(laserGunCooldownUpgradeItemThree);
    }

    public void LaserGunMaxDurationUpgrade()
    {
        InstallUpgradeItem(laserGunMaxDurationItem);
    }

    //------------ Rocket Launcher Upgrades ------------//
    public void RocketLauncherDamageUpgradeOne()
    {
        InstallUpgradeItem(rocketLauncherDamageItemOne);
    }

    public void RocketLauncherDamageUpgradeTwo()
    {
        InstallUpgradeItem(rocketLauncherDamageItemTwo);
    }

    public void RocketLauncherDamageUpgradeThree()
    {
        InstallUpgradeItem(rocketLauncherDamageItemThree);
    }

    public void RocketLauncherEnviromentDamageUpgradeOne()
    {
        InstallUpgradeItem(rocketLauncherEnviromentDamageItemOne);
    }

    public void RocketLauncherEnviromentDamageUpgradeTwo()
    {
        InstallUpgradeItem(rocketLauncherEnviromentDamageItemTwo);
    }

    public void RocketLauncherEnviromentDamageUpgradeThree()
    {
        InstallUpgradeItem(rocketLauncherEnviromentDamageItemThree);
    }

    public void RocketLauncherDamageRadiusUpgrade()
    {
        InstallUpgradeItem(rocketLauncherRadiusItem);
    }
}

public class MachineGunDamageUpgradeItemOne : Items
{
    public MachineGunDamageUpgradeItemOne(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        GameObject machineGunBulletPrefab = Resources.Load("Prefab/Player/Weapon/MashineGunBullet") as GameObject;
        MashineGunBullet machmachineGunBulletScript = machineGunBulletPrefab.GetComponent<MashineGunBullet>();
        machmachineGunBulletScript.damage += 1;
    }
}

public class MachineGunDamageUpgradeItemTwo : Items
{
    public MachineGunDamageUpgradeItemTwo(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        GameObject machineGunBulletPrefab = Resources.Load("Prefab/Player/Weapon/MashineGunBullet") as GameObject;
        MashineGunBullet machmachineGunBulletScript = machineGunBulletPrefab.GetComponent<MashineGunBullet>();
        machmachineGunBulletScript.damage += 5;
    }
}

public class MachineGunDamageUpgradeItemThree : Items
{
    public MachineGunDamageUpgradeItemThree(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        GameObject machineGunBulletPrefab = Resources.Load("Prefab/Player/Weapon/MashineGunBullet") as GameObject;
        MashineGunBullet machmachineGunBulletScript = machineGunBulletPrefab.GetComponent<MashineGunBullet>();
        machmachineGunBulletScript.damage += 10;
    }
}

public class MachineGunReloadUpgradeItemOne : Items
{
    public MachineGunReloadUpgradeItemOne(string name, float price, Sprite picture)
         : base(name, price, picture)
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
    public MachineGunReloadUpgradeItemTwo(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        Shooting shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        shootingScript.machineGunReloadSpeed -= 2.5f;
        shootingScript.stopReloading = true;
    }
}

public class MachineGunReloadUpgradeItemThree : Items
{
    public MachineGunReloadUpgradeItemThree(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        Shooting shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        shootingScript.machineGunReloadSpeed -= 5f;
        shootingScript.stopReloading = true;
    }
}

public class MachineGunFireUpgradeItem : Items
{
    public MachineGunFireUpgradeItem(string name, float price, Sprite picture)
         : base(name, price, picture)
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
    public LaserGunDamageUpgradeItemOne(string name, float price, Sprite picture)
         : base(name, price, picture)
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
    public LaserGunDamageUpgradeItemTwo(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        Laser laserScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Laser>();
        laserScript.laserDamage += 0.2f;
    }
}

public class LaserGunDamageUpgradeItemThree : Items
{
    public LaserGunDamageUpgradeItemThree(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        Laser laserScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Laser>();
        laserScript.laserDamage += 0.2f;
    }
}

public class LaserGunCooldownUpgradeItemOne : Items
{
    public LaserGunCooldownUpgradeItemOne(string name, float price, Sprite picture)
         : base(name, price, picture)
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
    public LaserGunCooldownUpgradeItemTwo(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        Laser laserScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Laser>();
        laserScript.coolDownSpeed += 0.5f;
    }
}

public class LaserGunCooldownUpgradeItemThree : Items
{
    public LaserGunCooldownUpgradeItemThree(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        Laser laserScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Laser>();
        laserScript.coolDownSpeed += 0.5f;
    }
}

public class LaserGunMaxDurationItem : Items
{
    public LaserGunMaxDurationItem(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        Laser laserScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Laser>();
        laserScript.laserMaxTimer += 12;
        laserScript.laserCurrentTimer = laserScript.laserMaxTimer;

        Slider laserTimerSlider = laserScript.laserTimerUI.GetComponent<Slider>();
        laserTimerSlider.maxValue = laserScript.laserMaxTimer;
    }
}

public class RocketLauncherDamageItemOne : Items
{
    public RocketLauncherDamageItemOne(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        GameObject explosionBulletPrefab = Resources.Load("Prefab/Player/Weapon/ExplosionBullet") as GameObject;
        ExplosionBullet explosionBulletScript = explosionBulletPrefab.GetComponent<ExplosionBullet>();
        explosionBulletScript.damage += 1;
    }
}

public class RocketLauncherDamageItemTwo : Items
{
    public RocketLauncherDamageItemTwo(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        GameObject explosionBulletPrefab = Resources.Load("Prefab/Player/Weapon/ExplosionBullet") as GameObject;
        ExplosionBullet explosionBulletScript = explosionBulletPrefab.GetComponent<ExplosionBullet>();
        explosionBulletScript.damage += 3;
    }
}

public class RocketLauncherDamageItemThree : Items
{
    public RocketLauncherDamageItemThree(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        GameObject explosionBulletPrefab = Resources.Load("Prefab/Player/Weapon/ExplosionBullet") as GameObject;
        ExplosionBullet explosionBulletScript = explosionBulletPrefab.GetComponent<ExplosionBullet>();
        explosionBulletScript.damage += 6;
    }
}

public class RocketLauncherEnviromentDamageItemOne : Items
{
    public RocketLauncherEnviromentDamageItemOne(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        GameObject explosionBulletPrefab = Resources.Load("Prefab/Player/Weapon/ExplosionBullet") as GameObject;
        ExplosionBullet explosionBulletScript = explosionBulletPrefab.GetComponent<ExplosionBullet>();
        explosionBulletScript.enviromentalDamage += 1;
    }
}

public class RocketLauncherEnviromentDamageItemTwo : Items
{
    public RocketLauncherEnviromentDamageItemTwo(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        GameObject explosionBulletPrefab = Resources.Load("Prefab/Player/Weapon/ExplosionBullet") as GameObject;
        ExplosionBullet explosionBulletScript = explosionBulletPrefab.GetComponent<ExplosionBullet>();
        explosionBulletScript.enviromentalDamage += 1.5f;
    }
}

public class RocketLauncherEnviromentDamageItemThree : Items
{
    public RocketLauncherEnviromentDamageItemThree(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        GameObject explosionBulletPrefab = Resources.Load("Prefab/Player/Weapon/ExplosionBullet") as GameObject;
        ExplosionBullet explosionBulletScript = explosionBulletPrefab.GetComponent<ExplosionBullet>();
        explosionBulletScript.enviromentalDamage += 2.5f;
    }
}

public class RocketLauncherRadiusItem : Items
{
    public RocketLauncherRadiusItem(string name, float price, Sprite picture)
         : base(name, price, picture)
    {
    }
    public override void ItemEffect()
    {
        GameObject explosionBulletPrefab = Resources.Load("Prefab/Player/Weapon/ExplosionBullet") as GameObject;
        ExplosionBullet explosionBulletScript = explosionBulletPrefab.GetComponent<ExplosionBullet>();
        explosionBulletScript.radius += 2.5f;
    }
}