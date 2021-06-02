// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.
#include "RPGPotionItem.h"

URPGPotionItem::URPGPotionItem(const FObjectInitializer& objectInitializer):Super(objectInitializer)
{
	ItemType = ERPGItemType::Potion;
}

TSharedPtr<FJsonObject> URPGPotionItem::ToJson()
{
	TSharedPtr<FJsonObject> jsonObject = MakeShared<FJsonObject>();
	jsonObject->SetStringField("item_name", ItemName);
	jsonObject->SetStringField("item_description", ItemDescription);
	jsonObject->SetNumberField("price", Price);
	jsonObject->SetNumberField("max_count", MaxCount);
	jsonObject->SetNumberField("max_level", MaxLevel);
	jsonObject->SetNumberField("ability_level", AbilityLevel);
	return jsonObject;
}

bool URPGPotionItem::FromJson(FJsonObject& jsonObject)
{
	ItemName = jsonObject.GetStringField("item_name");
	ItemDescription = jsonObject.GetStringField("item_description");
	Price = jsonObject.GetNumberField("price");
	MaxCount = jsonObject.GetNumberField("max_count");
	MaxLevel = jsonObject.GetNumberField("max_level");
	AbilityLevel = jsonObject.GetNumberField("ability_level");

	return false;
}