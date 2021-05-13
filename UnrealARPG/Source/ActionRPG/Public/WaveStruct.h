// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#pragma once

#include "CoreMinimal.h"
#include "DataAssets/JsonDataAssetBase.h"
#include "JsonObjectConverter.h"
#include "Classes/GameplayTagContainer.h"
#include "WaveStruct.generated.h"

UENUM(BlueprintType)
enum class EWaveDifficult : uint8
{
	WD_Easy			UMETA(DisplayName = "Easy")
	WD_Normal		UMETA(DisplayName = "Normal")
	WD_Hard			UMETA(DisplayName = "Hard")
};

class ARPGCharacterBase;
USTRUCT(BlueprintType)
struct FSpawnGroup
{
	GENERATED_BODY()
public:
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Wave Data")
	TArray<TSubclassOf<ARPGCharacterBase>> Enemies;
	
	TSharedRef<FJsonObject> ToJson();

	void FromJson(TSharedRef<FJsonObject> json);
};

USTRUCT(BlueprintType)
struct FWaveData
{
	GENERATED_BODY()
public:
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = "Wave Date")
	TArray<FSpawnGroup> EnemiesGroup;

	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = "Wave Date")
	float WaveTime;

	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = "Wave Date")
	float NumOfCake;

	TSharedRef<FJsonObject> ToJson();

	void FromJson(TSharedRef<FJsonObject> json);
};

class ACTIONRPG_API WaveStruct : public UJsonDataAssetBase
{

	GENERATED_BODY()
public:
	
	UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = "Wave Date")
	TArray<FWaveData> WaveData;

	virtual TSharedPtr<FJsonObject> ToJson() override;

	virtual bool FromJson(FJsonObject& jsonObject) override;


private:

	FSpawnGroup SpawnGroup;
};
