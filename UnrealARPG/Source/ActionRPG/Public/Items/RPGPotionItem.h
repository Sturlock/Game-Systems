// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#pragma once

#include "ItemDataAsset.h"
#include "RPGPotionItem.generated.h"

/** Native structure for potions*/
UCLASS(BlueprintType)
class ACTIONRPG_API URPGPotionItem : public UItemDataAsset
{
	GENERATED_BODY()

public:
	/** Constructor */
	URPGPotionItem()
	{
		ItemType = ERPGItemType::Potion;
	}
};