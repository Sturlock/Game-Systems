// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#pragma once

#include "ItemDataAsset.h"
#include "RPGSkillItem.generated.h"

/** Native structure for skills*/
UCLASS(BlueprintType)
class ACTIONRPG_API URPGSkillItem : public UItemDataAsset
{
	GENERATED_BODY()

public:
	/** Constructor */
	URPGSkillItem(const FObjectInitializer& objectInitializer);
};