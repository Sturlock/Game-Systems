// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#pragma once

#include "ActionRPG.h"

#include "ItemDataAsset.h"
#include "DataAssets/WeaponData.h"

#include "Items/RPGPotionItem.h"
#include "Items/RPGSkillItem.h"
#include "Items/RPGTokenItem.h"
#include "Items/RPGWeaponItem.h"



#include "Engine/GameInstance.h"
#include "RPGGameInstanceBase.generated.h"


class UItemDataAsset;
class URPGSaveGame;

/**
 * Base class for GameInstance, should be blueprinted
 * Most games will need to make a game-specific subclass of GameInstance
 * Once you make a blueprint subclass of your native subclass you will want to set it to be the default in project settings
 */
UCLASS()
class ACTIONRPG_API URPGGameInstanceBase : public UGameInstance
{
	GENERATED_BODY()
public:
	// Constructor
	URPGGameInstanceBase();		

	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Inventory)
	TMap<UItemDataAsset*, FRPGPotionItemStruct> Potions;

	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Inventory)
	TMap<UItemDataAsset*, FRPGSkillItemStruct> Skills;

	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Inventory)
	TMap<UItemDataAsset*, FRPGTokenItemStruct> Tokens;

	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Inventory)
	TMap<UItemDataAsset*, UWeaponData> Weapons;

	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Inventory)
	TMap<UItemDataAsset*, FRPGItemData> DefaultInventoryItems;

	/** Number of slots for each type of item */
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Inventory, DisplayName = "Item Slots Per Type")
	TMap<ERPGItemType, int32> SlotsPerItemType;

	UFUNCTION(BlueprintCallable, Category = Inventory)
	bool ItemExists(UItemDataAsset* ItemKey, ERPGItemType ItemType) const;

	UFUNCTION(BlueprintCallable, Category = Inventory)
	bool TryGetPotion(UItemDataAsset* PotionKey, FRPGPotionItemStruct& outPotion) const;

	UFUNCTION(BlueprintCallable, Category = Inventory)
	FRPGPotionItemStruct GetPotion(UItemDataAsset* PotionKey) const;

	UFUNCTION(BlueprintCallable, Category = Inventory)
	bool TryGetSkill(UItemDataAsset* SkillKey, FRPGSkillItemStruct& outSkill) const;

	UFUNCTION(BlueprintCallable, Category = Inventory)
	FRPGSkillItemStruct GetSkill(UItemDataAsset* SkillKey) const;

	UFUNCTION(BlueprintCallable, Category = Inventory)
	bool TryGetToken(UItemDataAsset* TokenKey, FRPGTokenItemStruct& outToken) const;

	UFUNCTION(BlueprintCallable, Category = Inventory)
	FRPGTokenItemStruct GetToken(UItemDataAsset* TokenKey) const;

	UFUNCTION(BlueprintCallable, Category = Inventory)
	bool TryGetWeapon(UItemDataAsset* WeaponKey, UWeaponData& outWeapon) const;

	UFUNCTION(BlueprintCallable, Category = Inventory)
	FRPGWeaponItemStruct GetWeapon(UItemDataAsset* WeaponKey) const;

	UFUNCTION(BlueprintCallable, Category = Inventory)
	bool TryGetBaseItemData(UItemDataAsset* ItemKey, ERPGItemType ItemType, UItemDataAsset& outItem) const;

	UFUNCTION(BlueprintCallable, Category = Inventory)
	UItemDataAsset GetBaseItemData(UItemDataAsset* ItemKey, ERPGItemType ItemType) const;

	UFUNCTION(BlueprintCallable, Category = Inventory)
	bool FindItem(UItemDataAsset* ItemKey, ERPGItemType& OutItemType, UItemDataAsset& OutItemData) const;

	UFUNCTION(BlueprintCallable, Category = Inventory)
	void GetItemsBaseInfo(UItemDataAsset ItemType, TMap<FString, UItemDataAsset>& OutItems) const;

	/** Returns true if this is a valid inventory slot */
	UFUNCTION(BlueprintCallable, Category = Inventory)
	bool IsValidItemSlot(UItemDataAsset ItemSlot) const;

	virtual void Init() override;
};