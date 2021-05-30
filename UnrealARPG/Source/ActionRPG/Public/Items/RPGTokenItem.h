// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#pragma once

#include "ItemDataAsset.h"
#include "RPGTokenItem.generated.h"

/** Native structure for tokens*/
UCLASS(BlueprintType)
class ACTIONRPG_API URPGTokenItem : public UItemDataAsset
{
	GENERATED_BODY()

public:
	/** Constructor */
	URPGTokenItem()
	{
		ItemType = ERPGItemType::Token;
		MaxCount = 0; // Infinite
	}
};