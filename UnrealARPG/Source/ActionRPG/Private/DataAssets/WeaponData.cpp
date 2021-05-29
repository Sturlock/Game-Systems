// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#include "WeaponData.h"



TSharedPtr<FJsonObject> UWeaponData::ToJson()
{
	TSharedPtr<FJsonObject> jsonObject = MakeShared<FJsonObject>();
	jsonObject->SetStringField("item_name", WeaponDataAsset.ItemName.ToString());
	jsonObject->SetStringField("item_description", WeaponDataAsset.ItemDescription.ToString());
	jsonObject->SetNumberField("price", WeaponDataAsset.Price);
	jsonObject->SetNumberField("max_count", WeaponDataAsset.MaxCount);
	jsonObject->SetNumberField("ability_level", WeaponDataAsset.AbilityLevel);
	return jsonObject;
}
bool UWeaponData::FromJson(FJsonObject& jsonObject)
{
	FString name;
	FString description;
	WeaponDataAsset.ItemName.FromString(jsonObject.GetStringField("item_name"));
	WeaponDataAsset.ItemDescription.FromString(jsonObject.GetStringField("item_description"));
	WeaponDataAsset.Price = jsonObject.GetNumberField("price");
	WeaponDataAsset.MaxCount = jsonObject.GetNumberField("max_count");
	WeaponDataAsset.AbilityLevel = jsonObject.GetNumberField("ability_level");

	

	return true;
}