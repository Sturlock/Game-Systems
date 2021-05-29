// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#pragma once

#include "CoreMinimal.h"
#include "Engine/DataAsset.h"
#include "Components/Image.h"
#include "RPGAttributeSet.h"
#include "Items/RPGItem.h"
#include "JsonDataAssetBase.h"
#include "weaponActor.h"
#include "WeaponData.generated.h"

/**
 *
 */

USTRUCT(BlueprintType)
struct FWeaponAssetData : public FRPGItemStruct
{
	GENERATED_BODY()
public:
	FWeaponAssetData()
		: FRPGItemStruct()
	{
		ItemType = ERPGItemType::Weapon;

		UPROPERTY(BlueprintReadOnly)
	}


	TSharedRef<FJsonObject> ToJson();

	void FromJson(TSharedRef<FJsonObject> json);

};

UCLASS(BlueprintType)
class ACTIONRPG_API UWeaponData : public UJsonDataAssetBase
{
	GENERATED_BODY()
public:
	/** Constructor */
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = "WeaponDataAsset")
	FWeaponAssetData WeaponDataAsset;

	virtual TSharedPtr<FJsonObject> ToJson() override;
	virtual bool FromJson(FJsonObject& jsonObject) override;
};
