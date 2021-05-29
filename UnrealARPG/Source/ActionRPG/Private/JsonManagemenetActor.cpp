// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#include "JsonManagemenetActor.h"
#include "Serialization/JsonReader.h"
#include "Serialization/JsonSerializer.h"

// Sets default values
AJsonManagemenetActor::AJsonManagemenetActor()
{
	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;
}

// Called when the game starts or when spawned
void AJsonManagemenetActor::BeginPlay()
{
	Super::BeginPlay();

	if (jsonDataAsset != nullptr && !JsonInputString.IsEmpty())
	{
		TSharedPtr<FJsonObject> jsonObject = GetJsonFromString(JsonInputString);
		jsonDataAsset->FromJson(*jsonObject.Get());
	}
}

// Called every frame
void AJsonManagemenetActor::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
}

TSharedPtr<FJsonObject> AJsonManagemenetActor::GetJsonFromString(const FString& jsonString)
{
	TSharedPtr<FJsonObject> jsonObject = MakeShared<FJsonObject>();

	TSharedRef<TJsonReader<TCHAR>> reader = TJsonReaderFactory<TCHAR>::Create(jsonString);
	FJsonSerializer::Deserialize(reader, jsonObject);
	return jsonObject;
}

FString AJsonManagemenetActor::GetStringFromJson(TSharedRef<FJsonObject> jsonObject)
{
	FString output;
	TSharedRef<TJsonWriter<TCHAR>> writer = TJsonWriterFactory<TCHAR>::Create(&output);
	FJsonSerializer::Serialize(jsonObject, writer);
	return output;
}

void AJsonManagemenetActor::PostEditChangeProperty(FPropertyChangedEvent& PropertyChangedEvent)
{
	Super::PostEditChangeProperty(PropertyChangedEvent);

	FName propertyName = PropertyChangedEvent.Property->GetFName();

	if (propertyName == GET_MEMBER_NAME_CHECKED(AJsonManagemenetActor, bFromJson))
	{
		bFromJson = false;
		if (jsonDataAsset != nullptr && !JsonInput.IsEmpty())
		{
			TSharedPtr<FJsonObject> jsonObject = GetJsonFromString(JsonInput);
			jsonDataAsset->FromJson(*jsonObject.Get());
		}
	}
	if (propertyName == GET_MEMBER_NAME_CHECKED(AJsonManagemenetActor, bToJson))
	{
		bToJson = false;
		if (jsonDataAsset != nullptr)
		{
			JsonOutput = GetStringFromJson(jsonDataAsset->ToJson().ToSharedRef());
		}
	}
}