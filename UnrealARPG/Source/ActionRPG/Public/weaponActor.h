// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "Components/CapsuleComponent.h"
#include "Classes/GameplayTagContainer.h"
#include "weaponActor.generated.h"

class UCapsuleComponent;
class USkeletalMeshComponent;

UCLASS()
class ACTIONRPG_API AweaponActor : public AActor
{
	GENERATED_BODY()

public:
	// Sets default values for this actor's properties
	AweaponActor();

	UPROPERTY(BlueprintReadOnly)
		float attackTimeDelay;
	UPROPERTY(BlueprintReadOnly)
		bool bIsAttacking;
	UPROPERTY(BlueprintReadOnly)
		bool bEnableAttackDelay;
	UPROPERTY(BlueprintReadOnly)
		int32 attackDelayCount;
	UPROPERTY(BlueprintReadOnly)
		FGameplayTag attackEventTag;

	UFUNCTION(BlueprintCallable)
		void BeginWeaponAttack(FGameplayTag Event_Tag, float Attack_Time_Delay, int32 Attack_Delay_Count);
	UFUNCTION(BlueprintCallable)
		void EndWeaponAttack();

protected:
	// Called when the game starts or when spawned
	virtual void BeginPlay() override;
	UFUNCTION()
		void OnOverlapBegin(UPrimitiveComponent* OverlappedComp, AActor* OtherActor, UPrimitiveComponent* OtherComp,
			int32 OtherBodyIndex, bool bFromSweep, const FHitResult& SweepResult);

public:
	// Called every frame
	virtual void Tick(float DeltaTime) override;

	UPROPERTY(Category = Weapon, VisibleAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
		UCapsuleComponent* CapsuleComponent;
	UPROPERTY(Category = Weapon, VisibleAnywhere, BlueprintReadWrite, meta = (AllowPrivateAccess = "true"))
		USkeletalMeshComponent* Mesh;
};
