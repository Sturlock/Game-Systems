// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.
#include "RPGTokenItem.h"

URPGTokenItem::URPGTokenItem(const FObjectInitializer& objectInitializer) :Super(objectInitializer)
{
	ItemType = ERPGItemType::Token;
	MaxCount = 0; // Infinite
}
