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
		return Potions.Contains(ItemKey);
	case ERPGItemType::Skill:
		return Skills.Contains(ItemKey);
	case ERPGItemType::Token:
		return Tokens.Contains(ItemKey);
	case ERPGItemType::Weapon:
		return Weapons.Contains(ItemKey);
	}
	return false;
}

bool URPGGameInstanceBase::TryGetPotion(UItemDataAsset* Potion, URPGPotionItem& outPotion) const
{
	const URPGPotionItem* ptr = Potions.Find(Potion);
	if (ptr != nullptr)
	{
		outPotion = *ptr;
		return true;
	}
	outPotion = URPGPotionItem();
	return false;
}

URPGPotionItem URPGGameInstanceBase::GetPotion(UItemDataAsset* Potion) const
{
	const URPGPotionItem* ptr = Potions.Find(Potion);
	if (ptr != nullptr)
	{
		return *ptr;
	}
	return URPGPotionItem();
}

bool URPGGameInstanceBase::TryGetSkill(UItemDataAsset* Skill, URPGSkillItem& outSkill) const
{
	const URPGSkillItem* ptr = Skills.Find(Skill);
	if (ptr != nullptr)
	{
		outSkill = *ptr;
		return true;
	}
	outSkill = URPGSkillItem();
	return false;
}

URPGSkillItem URPGGameInstanceBase::GetSkill(UItemDataAsset* Skill) const
{
	const URPGSkillItem* ptr = Skills.Find(Skill);
	if (ptr != nullptr)
	{
		return *ptr;
	}
	return URPGSkillItem();
}

bool URPGGameInstanceBase::TryGetToken(UItemDataAsset* Token, URPGTokenItem& outToken) const
{
	const URPGTokenItem* ptr = Tokens.Find(Token);
	if (ptr != nullptr)
	{
		outToken = *ptr;
		return true;
	}
	outToken = URPGTokenItem();
	return false;
}

URPGTokenItem URPGGameInstanceBase::GetToken(UItemDataAsset* Token) const
{
	const URPGTokenItem* ptr = Tokens.Find(Token);
	if (ptr != nullptr)
	{
		return *ptr;
	}
	return URPGTokenItem();
}

bool URPGGameInstanceBase::TryGetWeapon(UItemDataAsset* Weapon, UWeaponData& outWeapon) const
{
	const UWeaponData* ptr = Weapons.Find(Weapon);
	if (ptr != nullptr)
	{
		outWeapon = *ptr;
		return true;
	}
	outWeapon = UWeaponData();
	return false;
}

UWeaponData URPGGameInstanceBase::GetWeapon(UItemDataAsset* WeaponKey) const
{
	const UWeaponData* ptr = Weapons.Find(WeaponKey);
	if (ptr != nullptr)
	{
		return *ptr;
	}
	return UWeaponData();
}

bool URPGGameInstanceBase::TryGetBaseItemData(UItemDataAsset* ItemKey, ERPGItemType ItemType, UItemDataAsset& outItem) const
{
	switch (ItemType)
	{
	case ERPGItemType::Potion:
	{
		UItemDataAsset* ptr = (UItemDataAsset*)Potions.Find(ItemKey);
		if (ptr != nullptr) outItem = *ptr;
		return ptr != nullptr;
	}		
	case ERPGItemType::Skill:
	{
		UItemDataAsset* ptr = (UItemDataAsset*)Skills.Find(ItemKey);
		if (ptr != nullptr) outItem = *ptr;
		return ptr != nullptr;
	}
	case ERPGItemType::Token:
	{
		UItemDataAsset* ptr = (UItemDataAsset*)Tokens.Find(ItemKey);
		if (ptr != nullptr) outItem = *ptr;
		return ptr != nullptr;
	}
	case ERPGItemType::Weapon:
	{
		UItemDataAsset* ptr = (UItemDataAsset*)Weapons.Find(ItemKey);
		if (ptr != nullptr) outItem = *ptr;
		return ptr != nullptr;
	}
	case ERPGItemType::Undefined:
		ERPGItemType itemType;
		bool found = FindItem(ItemKey, itemType, outItem);
		return found;
	}
	outItem = UItemDataAsset();
	return false;
}

UItemDataAsset URPGGameInstanceBase::GetBaseItemData(UItemDataAsset* ItemKey, ERPGItemType ItemType) const
{
	switch (ItemType)
	{
	case ERPGItemType::Potion:
	{
		UItemDataAsset* ptr = (UItemDataAsset*)Potions.Find(ItemKey);
		if (ptr != nullptr) return *ptr;
		else return UItemDataAsset();
	}
	case ERPGItemType::Skill:
	{
		UItemDataAsset* ptr = (UItemDataAsset*)Skills.Find(ItemKey);
		if (ptr != nullptr) return *ptr;
		else return UItemDataAsset();
	}
	case ERPGItemType::Token:
	{
		UItemDataAsset* ptr = (UItemDataAsset*)Tokens.Find(ItemKey);
		if (ptr != nullptr) return *ptr;
		else return UItemDataAsset();
	}
	case ERPGItemType::Weapon:
	{
		UItemDataAsset* ptr = (UItemDataAsset*)Weapons.Find(ItemKey);
		if (ptr != nullptr) return *ptr;
		else return UItemDataAsset();
	}
	case ERPGItemType::Undefined:
	{
		ERPGItemType itemType;
		UItemDataAsset itemData;
		FindItem(ItemKey, itemType, itemData);
		return itemData;
	}
	}
	return UItemDataAsset();
}

bool URPGGameInstanceBase::FindItem(UItemDataAsset* ItemKey, ERPGItemType& OutItemType, UItemDataAsset& OutItemData) const
{
	UItemDataAsset* ptr = (UItemDataAsset*)Potions.Find(ItemKey);
	if (ptr != nullptr)
	{
		OutItemType = ERPGItemType::Potion;
		OutItemData = *ptr;
		return true;
	}
	ptr = (UItemDataAsset*)Skills.Find(ItemKey);
	if (ptr != nullptr)
	{
		OutItemType = ERPGItemType::Skill;
		OutItemData = *ptr;
		return true;
	}
	ptr = (UItemDataAsset*)Tokens.Find(ItemKey);
	if (ptr != nullptr)
	{
		OutItemType = ERPGItemType::Token;
		OutItemData = *ptr;
		return true;
	}
	ptr = (UItemDataAsset*)Weapons.Find(ItemKey);
	if (ptr != nullptr)
	{
		OutItemType = ERPGItemType::Weapon;
		OutItemData = *ptr;
		return true;
	}
	OutItemType = ERPGItemType::Undefined;
	OutItemData = UItemDataAsset();
	return false;
}

void URPGGameInstanceBase::GetItemsBaseInfo(ERPGItemType ItemType, TMap<FString*, UItemDataAsset>& OutItems) const
{
	OutItems = TMap<FString*, UItemDataAsset>();
	switch (ItemType)
	{
	case ERPGItemType::Potion:
	{
		for (TPair<FString*, URPGPotionItem> pair : Potions)
		{
			OutItems.Add(pair.Key, pair.Value);
		}
		break;
	}
	case ERPGItemType::Skill:
	{
		for (TPair<UItemDataAsset*, URPGSkillItem> pair : Skills)
		{
			OutItems.Add(pair.Key, (UItemDataAsset)pair.Value);
		}
		break;
	}
	case ERPGItemType::Token:
	{
		for (TPair<UItemDataAsset*, URPGTokenItem> pair : Tokens)
		{
			OutItems.Add(pair.Key, (UItemDataAsset)pair.Value);
		}
		break;
	}
	case ERPGItemType::Weapon:
	{
		for (TPair<UItemDataAsset*, UWeaponData> pair : Weapons)
		{
			OutItems.Add(pair.Key, (UItemDataAsset)pair.Value);
		}
		break;
	}
	case ERPGItemType::Undefined:
	{
		for (TPair<FString, URPGPotionItem> pair : Potions)
		{
			OutItems.Add(pair.Key, pair.Value);
		}
		for (TPair<FString, FRPGSkillItemStruct> pair : Skills)
		{
			OutItems.Add(pair.Key, (UItemDataAsset)pair.Value);
		}
		for (TPair<FString, FRPGTokenItemStruct> pair : Tokens)
		{
			OutItems.Add(pair.Key, (UItemDataAsset)pair.Value);
		}
		for (TPair<FString, FRPGWeaponItemStruct> pair : Weapons)
		{
			OutItems.Add(pair.Key, (UItemDataAsset)pair.Value);
		}
		break;
	}
	}
}

bool URPGGameInstanceBase::IsValidItemSlot(UItemDataAsset ItemSlot) const
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