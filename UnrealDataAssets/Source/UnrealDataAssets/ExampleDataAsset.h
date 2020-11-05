// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Engine/DataAsset.h"
#include "ExampleDataAsset.generated.h"

/**
 * 
 */
UCLASS()
class UNREALDATAASSETS_API UExampleDataAsset : public UDataAsset
{
	GENERATED_BODY()

public:
	
	UExampleDataAsset();
	
	UPROPERTY(Category = Misc, EditAnywhere, BlueprintReadWrite)
	float thing;
	UPROPERTY(Category = Misc, EditAnywhere, BlueprintReadWrite)
	int otherThing;
};
