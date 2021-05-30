// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#pragma once

#include "CoreMinimal.h"
#include "ItemDataAsset.h"
#include "JsonDataAssetBase.h"
#include "weaponActor.h"
#include "WeaponData.generated.h"

/**
 *
 */


UCLASS(BlueprintType)
class ACTIONRPG_API UWeaponData : public UItemDataAsset
{
	GENERATED_BODY()
public:
	/** Constructor */
	UWeaponData()
	{
		ItemType = ERPGItemType::Weapon;
	}

	/** Weapon actor to spawn */
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Weapon)
		TSubclassOf<AActor> weaponActor;
};
