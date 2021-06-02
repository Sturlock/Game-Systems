// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#include "RPGGameInstanceBase.h"
#include "Items/RPGItem.h"
#include "Kismet/GameplayStatics.h"

// LA -
// Prevents code optimisation which is useful for stepping through as it means
// each line accurately matches where we are in execution
// Although it MUST be turned back on at the end of the file
// See: https://docs.microsoft.com/en-us/cpp/preprocessor/optimize?view=msvc-160
#pragma optimize("", off)

URPGGameInstanceBase::URPGGameInstanceBase()
{}

bool URPGGameInstanceBase::ItemExists(UItemDataAsset* ItemKey, ERPGItemType ItemType) const
{
	switch (ItemType)
	{
	case ERPGItemType::Potion:
		return Potions.Contains((URPGPotionItem*)ItemKey);
	case ERPGItemType::Skill:
		return Skills.Contains((URPGSkillItem*)ItemKey);
	case ERPGItemType::Token:
		return Tokens.Contains((URPGTokenItem*)ItemKey);
	case ERPGItemType::Weapon:
		return Weapons.Contains((UWeaponData*)ItemKey);
	}
	return false;
}

bool URPGGameInstanceBase::TryGetPotion(URPGPotionItem* PotionKey, URPGPotionItem*& outPotion) const
{
	URPGPotionItem* const* ptr = Potions.Find(PotionKey);
	if (ptr != nullptr)
	{
		outPotion = *ptr;
		return true;
	}
	outPotion = nullptr;
	return false;
}

URPGPotionItem* URPGGameInstanceBase::GetPotion(URPGPotionItem* PotionKey) const
{
	URPGPotionItem* const* ptr = Potions.Find(PotionKey);
	if (ptr != nullptr)
	{
		return *ptr;
	}
	return nullptr;
}

bool URPGGameInstanceBase::TryGetSkill(URPGSkillItem* SkillKey, URPGSkillItem*& outSkill) const
{
	URPGSkillItem* const* ptr = Skills.Find(SkillKey);
	if (ptr != nullptr)
	{
		outSkill = *ptr;
		return true;
	}
	outSkill = nullptr;
	return false;
}

URPGSkillItem* URPGGameInstanceBase::GetSkill(URPGSkillItem* Skill) const
{
	URPGSkillItem* const* ptr = Skills.Find(Skill);
	if (ptr != nullptr)
	{
		return *ptr;
	}
	return nullptr;
}

bool URPGGameInstanceBase::TryGetToken(URPGTokenItem* Token, URPGTokenItem*& outToken) const
{
	URPGTokenItem* const* ptr = Tokens.Find(Token);
	if (ptr != nullptr)
	{
		outToken = *ptr;
		return true;
	}
	outToken = nullptr;
	return false;
}

URPGTokenItem* URPGGameInstanceBase::GetToken(URPGTokenItem* Token) const
{
	URPGTokenItem* const* ptr = Tokens.Find(Token);
	if (ptr != nullptr)
	{
		return *ptr;
	}
	return nullptr;
}

bool URPGGameInstanceBase::TryGetWeapon(UWeaponData* Weapon, UWeaponData*& outWeapon) const
{
	UWeaponData* const* ptr = Weapons.Find(Weapon);
	if (ptr != nullptr)
	{
		outWeapon = *ptr;
		return true;
	}
	outWeapon = nullptr;
	return false;
}

UWeaponData* URPGGameInstanceBase::GetWeapon(UWeaponData* WeaponKey) const
{
	UWeaponData* const* ptr = Weapons.Find(WeaponKey);
	if (ptr != nullptr)
	{
		return *ptr;
	}
	return nullptr;
}

bool URPGGameInstanceBase::TryGetBaseItemData(UItemDataAsset* ItemKey, ERPGItemType ItemType, UItemDataAsset*& outItem) const
{
	switch (ItemType)
	{
	case ERPGItemType::Potion:
	{
		UItemDataAsset* ptr = (UItemDataAsset*)Potions.Find((URPGPotionItem*)ItemKey);
		if (ptr != nullptr) outItem = ptr;
		return ptr != nullptr;
	}
	case ERPGItemType::Skill:
	{
		UItemDataAsset* ptr = (UItemDataAsset*)Skills.Find((URPGSkillItem*)ItemKey);
		if (ptr != nullptr) outItem = ptr;
		return ptr != nullptr;
	}
	case ERPGItemType::Token:
	{
		UItemDataAsset* ptr = (UItemDataAsset*)Tokens.Find((URPGTokenItem*)ItemKey);
		if (ptr != nullptr) outItem = ptr;
		return ptr != nullptr;
	}
	case ERPGItemType::Weapon:
	{
		UItemDataAsset* ptr = (UItemDataAsset*)Weapons.Find((UWeaponData*)ItemKey);
		if (ptr != nullptr) outItem = ptr;
		return ptr != nullptr;
	}
	case ERPGItemType::Undefined:
		ERPGItemType itemType;
		bool found = FindItem(ItemKey, itemType, outItem);
		return found;
	}
	outItem = nullptr;
	return false;
}

UItemDataAsset* URPGGameInstanceBase::GetBaseItemData(UItemDataAsset* ItemKey, ERPGItemType ItemType) const
{
	switch (ItemType)
	{
	case ERPGItemType::Potion:
	{
		UItemDataAsset* ptr = (UItemDataAsset*)Potions.Find((URPGPotionItem*)ItemKey);
		if (ptr != nullptr) return ptr;
		else return nullptr;
	}
	case ERPGItemType::Skill:
	{
		UItemDataAsset* ptr = (UItemDataAsset*)Skills.Find((URPGSkillItem*)ItemKey);
		if (ptr != nullptr) return ptr;
		else return nullptr;
	}
	case ERPGItemType::Token:
	{
		UItemDataAsset* ptr = (UItemDataAsset*)Tokens.Find((URPGTokenItem*)ItemKey);
		if (ptr != nullptr) return ptr;
		else return nullptr;
	}
	case ERPGItemType::Weapon:
	{
		UItemDataAsset* ptr = (UItemDataAsset*)Weapons.Find((UWeaponData*)ItemKey);
		if (ptr != nullptr) return ptr;
		else return nullptr;
	}
	case ERPGItemType::Undefined:
	{
		ERPGItemType itemType;
		UItemDataAsset* itemData;
		FindItem(ItemKey, itemType, itemData);
		return itemData;
	}
	}
	return nullptr;
}

bool URPGGameInstanceBase::FindItem(UItemDataAsset* ItemKey, ERPGItemType& OutItemType, UItemDataAsset*& OutItemData) const
{
	UItemDataAsset* ptr = (UItemDataAsset*)Potions.Find((URPGPotionItem*)ItemKey);
	if (ptr != nullptr)
	{
		OutItemType = ERPGItemType::Potion;
		OutItemData = ptr;
		return true;
	}
	ptr = (UItemDataAsset*)Skills.Find((URPGSkillItem*)ItemKey);
	if (ptr != nullptr)
	{
		OutItemType = ERPGItemType::Skill;
		OutItemData = ptr;
		return true;
	}
	ptr = (UItemDataAsset*)Tokens.Find((URPGTokenItem*)ItemKey);
	if (ptr != nullptr)
	{
		OutItemType = ERPGItemType::Token;
		OutItemData = ptr;
		return true;
	}
	ptr = (UItemDataAsset*)Weapons.Find((UWeaponData*)ItemKey);
	if (ptr != nullptr)
	{
		OutItemType = ERPGItemType::Weapon;
		OutItemData = ptr;
		return true;
	}
	OutItemType = ERPGItemType::Undefined;
	OutItemData = nullptr;
	return false;
}

void URPGGameInstanceBase::GetItemsBaseInfo(ERPGItemType ItemType, TSet<UItemDataAsset*>& OutItems) const
{
	OutItems = TSet<UItemDataAsset*>();
	switch (ItemType)
	{
	case ERPGItemType::Potion:
	{
		for (URPGPotionItem* pair : Potions)
		{
			OutItems.Add(pair);
		}
		break;
	}
	case ERPGItemType::Skill:
	{
		for (URPGSkillItem* pair : Skills)
		{
			OutItems.Add(pair);
		}
		break;
	}
	case ERPGItemType::Token:
	{
		for (URPGTokenItem* pair : Tokens)
		{
			OutItems.Add(pair);
		}
		break;
	}
	case ERPGItemType::Weapon:
	{
		for (UWeaponData* pair : Weapons)
		{
			OutItems.Add(pair);
		}
		break;
	}
	case ERPGItemType::Undefined:
	{
		for (URPGPotionItem* pair : Potions)
		{
			OutItems.Add(pair);
		}
		for (URPGSkillItem* pair : Skills)
		{
			OutItems.Add(pair);
		}
		for (URPGTokenItem* pair : Tokens)
		{
			OutItems.Add(pair);
		}
		for (UWeaponData* pair : Weapons)
		{
			OutItems.Add(pair);
		}
		break;
	}
	}
}

bool URPGGameInstanceBase::IsValidItemSlot(FRPGItemSlot ItemSlot) const
{
	if (ItemSlot.IsValid())
	{
		const int32* FoundCount = SlotsPerItemType.Find(ItemSlot.ItemType);

		if (FoundCount)
		{
			return ItemSlot.SlotNumber < *FoundCount;
		}
	}
	return false;
}

void URPGGameInstanceBase::Init()
{
	Super::Init();
}

#pragma optimize("", on)