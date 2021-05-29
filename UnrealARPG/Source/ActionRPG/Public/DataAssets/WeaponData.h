// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#pragma once

#include "CoreMinimal.h"
#include "Engine/DataAsset.h"
#include "Components/Image.h"
#include "RPGAttributeSet.h"
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

	UWeaponData()
	{
		ItemType = ERPGItemType::Weapon;
	}
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Weapon)
		TSubclassOf<AActor> WeaponActor;
};
