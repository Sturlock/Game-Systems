// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#pragma once

#include "CoreMinimal.h"
#include "Engine/DataAsset.h"
#include "Components/Image.h"
#include <Items/RPGItem.h>
#include "JsonDataAssetBase.h"
#include "weaponActor.h"
#include "WeaponData.generated.h"


/**
 * 
 */
UCLASS(BlueprintType)
class ACTIONRPG_API UWeaponData : public UJsonDataAssetBase
{
	GENERATED_BODY()
public:
	UWeaponData();

	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Weapon)
		TSubclassOf<AweaponActor> weaponActor;
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Weapon)
		FString itemName;
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Weapon)
		FString itemDescription;
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Weapon)
		FSlateBrush itemIcon;
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Weapon)
		int32 price;
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Weapon)
		int32 maxCount;
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = Weapon)
		TSubclassOf<URPGGameplayAbility> grantedAbility;
	UPROPERTY(EditAnywhere, BlueprintReadWrite , Category = Weapon)
		int32 abilityLevel;

	virtual TSharedPtr<FJsonObject> ToJson() override;
	virtual bool FromJson(FJsonObject& jsonObject) override;
};
