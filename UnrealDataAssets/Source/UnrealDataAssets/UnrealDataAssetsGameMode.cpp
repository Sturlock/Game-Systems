// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#include "UnrealDataAssetsGameMode.h"
#include "UnrealDataAssetsPawn.h"

AUnrealDataAssetsGameMode::AUnrealDataAssetsGameMode()
{
	// set default pawn class to our character class
	DefaultPawnClass = AUnrealDataAssetsPawn::StaticClass();
}

