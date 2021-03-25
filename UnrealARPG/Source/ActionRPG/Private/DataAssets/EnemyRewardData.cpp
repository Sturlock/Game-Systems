// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.


#include "EnemyRewardData.h"

UEnemyRewardData::UEnemyRewardData()
{
	TimeBonusPerKill = 5.f;
	MaxSoulsDropped = 1;
	MinSoulsDropped = 1;
	PotionDropChance = 0.1f;
}
TSharedPtr<FJsonObject> UEnemyRewardData::ToJson()
{
	TSharedPtr<FJsonObject> jsonObject = MakeShared<FJsonObject>();
	jsonObject->SetNumberField("time_bonus_per_kill", TimeBonusPerKill);
	jsonObject->SetNumberField("min_souls_dropped", MinSoulsDropped);
	jsonObject->SetNumberField("max_souls_dropped", MaxSoulsDropped);
	jsonObject->SetNumberField("potions_drop_chance", PotionDropChance);
	return jsonObject;
}
bool UEnemyRewardData::FromJson(FJsonObject& jsonObject)
{
	TimeBonusPerKill = jsonObject.GetNumberField("time_bonus_per_kill");
	MinSoulsDropped = jsonObject.GetNumberField("min_souls_dropped");
	MaxSoulsDropped = jsonObject.GetNumberField("max_souls_dropped");
	PotionDropChance = jsonObject.GetNumberField("potions_drop_chance");
	return true;
}