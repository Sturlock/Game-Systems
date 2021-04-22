// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.


#include "WeaponData.h"

UWeaponData::UWeaponData()
{
	itemName = "Example Item";
	itemDescription = "Its an Item";
	price = 1;
	maxCount = 1;
	abilityLevel = 1;
}

TSharedPtr<FJsonObject> UWeaponData::ToJson()
{
	TSharedPtr<FJsonObject> jsonObject = MakeShared<FJsonObject>();
	jsonObject->SetStringField("item_name", itemName);
	jsonObject->SetStringField("item_description", itemDescription);
	jsonObject->SetNumberField("price", price);
	jsonObject->SetNumberField("max_count", maxCount);
	jsonObject->SetNumberField("ability_level", abilityLevel);
	return jsonObject;
}
bool UWeaponData::FromJson(FJsonObject& jsonObject)
{
	itemName = jsonObject.GetStringField("item_name");
	itemDescription = jsonObject.GetStringField("item_description");
	price = jsonObject.GetNumberField("price");
	maxCount = jsonObject.GetNumberField("max_count");
	abilityLevel = jsonObject.GetNumberField("ability_level");
	return true;
}