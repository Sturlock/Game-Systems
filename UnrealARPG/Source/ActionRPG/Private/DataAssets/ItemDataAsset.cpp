// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.


#include "ItemDataAsset.h"



bool UItemDataAsset::IsConsumable()
{
	if (MaxCount <= 0)
	{
		return true;
	}
	return false;
}

TSharedPtr<FJsonObject> UItemDataAsset::ToJson()
{
	TSharedPtr<FJsonObject> jsonObject = MakeShared<FJsonObject>();
	jsonObject->SetStringField("item_name", ItemName);
	jsonObject->SetStringField("item_description", ItemDescription);
	jsonObject->SetNumberField("price", Price);
	jsonObject->SetNumberField("max_count", MaxCount);
	jsonObject->SetNumberField("ability_level", AbilityLevel);
	return jsonObject;
}
bool UItemDataAsset::FromJson(FJsonObject& jsonObject)
{
	ItemName = jsonObject.GetStringField("item_name");
	ItemDescription = jsonObject.GetStringField("item_description");
	Price = jsonObject.GetNumberField("price");
	MaxCount = jsonObject.GetNumberField("max_count");
	AbilityLevel = jsonObject.GetNumberField("ability_level");



	return true;
}