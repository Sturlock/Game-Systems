// Fill out your copyright notice in the Description page of Project Settings.

#include "ExamplePawnDataAsset.h"
#include "UObject/ConstructorHelpers.h"


UExamplePawnDataAsset::UExamplePawnDataAsset() 
{
	//Sound Effect
	static ConstructorHelpers::FObjectFinder<USoundBase> FireAudio(TEXT("/Game/TwinStick/Audio/TwinStickFire.TwinStickFire"));
	FireSound = FireAudio.Object;
	//Movement
	MoveSpeed = 1000.0f;
	//Weapon
	GunOffset = FVector(90.0f, 0.0f, 0.0f);
	FireRate = 0.1f;

}