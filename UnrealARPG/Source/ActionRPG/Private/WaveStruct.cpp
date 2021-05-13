// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.


#include "WaveStruct.h"
#include "Serialization/JsonReader.h"
#include "Serialization/JsonSerializer.h"
#include "Serialization/JsonWriter.h"

#pragma optimize("", off)

TSharedPtr<FJsonObject> FSpawnGroup::ToJson()
{
	TSharedRef<FJsonObject> outJson = MakeShared <FJsonObject>();
	FJsonObjectConverter::UStructToJsonObject(FSpawnGroup::StaticStruct(), this, outJson, 0, 0);
	return outJson;
}

void FSpawnGroup::FromJson(TSharedRef<FJsonObject> json)
{
	FJsonObjectConverter::JsonObjectToUStruct(json, FSpawnGroup::StaticStruct(), this, 0, 0);
}

TSharedRef<FJsonObject> FWaveData::ToJson()
{
	TSharedRef<FJsonObject> outJson = MakeShared<FJsonObject>();
	outJson->SetNumberField("WaveTime", WaveTime);
	outJson->SetNumberField("NumOfCake", NumOfCake);

	TArray<TSharedPtr<FJsonValue>> objects = TArray<MakeShared<FJsonValue>>();

	for (int i = 0; i <EnemiesGroup.Num(); i++)
	{
		TSharedPtr<FJsonObject> jsonObject = EnemiesGroup[i].ToJson();
		TSharedPtr<FJsonValueObject> jsonValueObject = MakeShared<FJsonValueObject>(FJsonValueObject(jsonObject));
		objects.Add(jsonValueObject);
	}

	outJson->SetArrayField("EnemiesGroup", objects);

	return outJson;
}

void FWaveData::FromJson(TSharedRef<FJsonObject> json)
{
	WaveTime = json->GetNumberField("WaveTime");
	NumOfCake = json->GetNumberField("NumOfCake");

	EnemiesGroup.Empty();
	const TArray<TSharedPtr<FJsonValue>> valArray = json->GetArrayField("EnemiesGroup");
	for (int i = 0; i < valArray.Num(); i++)
	{
		FSpawnGroup group = FSpawnGroup();
		group.FromJson(valArray[i]->AsObject().ToSharedRef());
		EnemiesGroup.Add(group);
	}
}
TSharedPtr<FJsonObject> WaveStruct::ToJson()
{
	TSharedRef<FJsonObject> outJson = MakeShared<FJsonObject>();
	TArray<TSharedPtr<FJsonValue>> object = TArray<TSharedPtr<FJsonValue>>();

	for (int i = 0; i < WaveData.Num(); i++)
	{
		TSharedPtr<FJsonObject> jsonObject = WaveData[i].ToJson();
		TSharedPtr<FJsonValueObject> jsonValueObject = MakeShared<FJsonValueObject>(FJsonValueObject(jsonObject));
		object.Add(jsonValueObject);
	}

	outJson->SetArrayField("WaveData", object);
	return outJson;
}

bool WaveStruct::FromJson(FJsonObject& jsonObject)
{
	WaveData.Empty();
	const TArray<TSharedPtr<FJsonValue>> valueArray = jsonObject.GetArrayField("WaveData");
	for (int i = 0; i < valueArray.Num(); i++
	{
		FWaveData wave = FWaveData();
			wave.FromJson(valueArray[i]->AsObject().ToSharedRef());
			WaveData.Add(wave);
	}
	return true;
}
#pragma optimize("", on)
