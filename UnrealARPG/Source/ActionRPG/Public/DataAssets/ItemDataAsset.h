// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#pragma once
#include "ActionRPG.h"
#include "CoreMinimal.h"
#include "DataAssets/JsonDataAssetBase.h"
#include "Styling/SlateBrush.h"
#include "RPGTypes.h"
#include "ItemDataAsset.generated.h"

class URPGGameplayAbility;

/**
 * 
 */
UCLASS(BlueprintType)
class ACTIONRPG_API UItemDataAsset : public UJsonDataAssetBase
{
	GENERATED_BODY()

public:
	UItemDataAsset(const FObjectInitializer& objectInitializer);

	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Item)
		ERPGItemType ItemType;

	/** User-visible short name */
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Item)
		FString ItemName;

	/** User-visible long description */
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Item)
		FString ItemDescription;

	/** Icon to display */
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Item)
		FSlateBrush ItemIcon;

	/** Price in game */
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Item)
		int32 Price;

	/** Maximum number of instances that can be in inventory at once, <= 0 means infinite */
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Max)
		int32 MaxCount;

	/** Maximum level this item can be, <= 0 means infinite */
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Max)
		int32 MaxLevel;

	/** Ability to grant if this item is slotted */
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Abilities)
		TSubclassOf<URPGGameplayAbility> GrantedAbility;

	/** Ability level this item grants. <= 0 means the character level */
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = Abilities)
		int32 AbilityLevel;

	UFUNCTION(BlueprintCallable)
		bool IsConsumable();

	virtual TSharedPtr<FJsonObject> ToJson() override;
	virtual bool FromJson(FJsonObject& jsonObject) override;
};
