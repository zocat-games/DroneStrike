namespace Zocat
{
    public enum MapPointType
    {
        None = 0,
        Pistol = 10,
        Sniper = 20,
        MachineGun = 30,
        WarPlane = 40
    }

    public enum SlotType
    {
        None = 0,
        Pistol = 1,
        Primary = 2,
        Secondary = 3,
        Disposable0 = 4,
        Disposable1 = 5,
        Vest = 6,
        Helmet = 7
    }

    public enum CategoryType
    {
        __All = 1,
        __Item = 2,
        __Upgradable = 3,
        _Armor = 101,
        _Disposable = 102,
        _Gun = 103,
        _Harmful = 104,
        _HarmfulDisposable = 105,
        _Healer = 106,
        _HealerDisposable = 107,
        _Rifle = 108,
        Explosive = 201,
        Helmet = 202,
        Unguided = 203,
        Medical = 204,
        MachineGun = 205,
        Guided = 206,
        Vest = 207
    }

    public enum ItemType
    {
        ps_0_SmithWesson = 10,
        ps_1_Colt_M1911 = 11,
        ps_2_Deagel = 12,
        ps_3_Revolver = 13,
        mg_0_MP7 = 20,
        mg_1_P90 = 21,
        mg_2_Groza = 22,
        mg_3_SCAR = 23,
        mg_4_AK_47 = 24,
        mg_5_M249 = 25,
        sn_0_MK12 = 30,
        sn_1_M24 = 31,
        sn_2_AWM = 32,
        sn_3_Dragunov = 33,
        sn_4_DSR1 = 34,
        ex_0_Grenade_I = 40,
        ex_1_Grenade_II = 41,
        ex_2_IED_I = 42,
        hr_0_Pills = 50,
        hr_1_Needle = 51,
        hr_2_Medkit = 52,
        ht_0_M1_Helmet = 60,
        ht_1_WW2_Helmet = 61,
        ht_2_PASGT_Helmet = 62,
        vs_0_Tactical_Modular_Vest = 70,
        vs_1_Soft_Armor_Vest = 71,
        vs_2_Hard_Armor_Vest = 72
    }

    public enum AttributeType
    {
        Name = 0,
        Icon = 1,
        Index = 2,
        Level = 3,
        Unlocked = 4,
        Purchased = 5,
        Equipped = 6,
        DamageMin = 7,
        EffectiveRangeMin = 8,
        MagazineSizeMin = 9,
        ReloadSpeedMin = 10,
        /*--------------------------------------------------------------------------------------*/
        BonusHealthMin = 11,
        DefenseMin = 12,
        ExplosiveDefenseMin = 13,
        HeadShotProtectionMin = 14,
        Radius = 15,
        HealingAmount = 16,
        ApplicationDuration = 17,
        DamageBoost = 18,
        Durability = 19,
        Description = 20,
        Amount = 21,
        Price = 22,
        RapidFire = 23,
        CategoryIndex = 24,
        AudioClip
    }

    public enum ColorType
    {
        Khaki0 = 0,
        Khaki1 = 1,
        ItemButton0 = 2,
        ItemButton1 = 3,
        Durability0 = 4,
        Durability1 = 5,
        Level0 = 6,
        Level1 = 7,
        Amount0 = 8,
        Gold = 9,
        Silver = 10,
        BlackAlpha0 = 11,
        RedAlpha0 = 12
    }

    // public 
}