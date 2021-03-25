// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#pragma once

#include "CoreMinimal.h"
#include "Engine/DataAsset.h"
#include "Dom/JsonObject.h"
#include "JsonDataAssetBase.generated.h"

/**
 * 
 */
UCLASS()
class ACTIONRPG_API UJsonDataAssetBase : public UDataAsset
{
	GENERATED_BODY()
public:
	UJsonDataAssetBase();

	UPROPERTY(BlueprintReadOnly,EditAnywhere,Category = JSON)
	FString JsonObjectKey;

	virtual TSharedPtr<FJsonObject> ToJson() { return nullptr; }
	virtual bool FromJson(FJsonObject& jsonObject) { return false; }
};
