// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#pragma once

#include "CoreMinimal.h"
#include "RPGCharacterBase.h"
#include "WaveStruct.generated.h"


USTRUCT(BlueprintType)
struct FSpawnGroup
{
	GENERATED_BODY()
public:
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Test")
		TArray<TSubclassOf<ARPGCharacterBase>> Enemys;

};
/**
 * 
 */
class ACTIONRPG_API WaveStruct: public ARPGCharacterBase
{
public:
	WaveStruct();
	

	/*UFUNCTION(BlueprintCallable, Category = Enemy)
	 bool IsFuck();*/

private:

	FSpawnGroup SpawnGroup;
};


