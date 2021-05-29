// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#include "weaponActor.h"
// Sets default values
AweaponActor::AweaponActor()
{
	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

	attackEventTag;
	attackDelayCount;
	attackTimeDelay;
	bIsAttacking;
	bEnableAttackDelay;

	Mesh = CreateDefaultSubobject<USkeletalMeshComponent>(TEXT("WeaponMesh"));
	Mesh->SetupAttachment(RootComponent);
	Mesh->OnComponentBeginOverlap.AddDynamic(this, &AweaponActor::OnOverlapBegin);

	CapsuleComponent = CreateDefaultSubobject<UCapsuleComponent>(TEXT("CapsualComponent"));
	CapsuleComponent->SetupAttachment(RootComponent);
}

void AweaponActor::BeginPlay()
{
	Super::BeginPlay();
}

void AweaponActor::OnOverlapBegin(UPrimitiveComponent* OverlappedComp, AActor* OtherActor, UPrimitiveComponent* OtherComp, int32 OtherBodyIndex, bool bFromSweep, const FHitResult& SweepResult)
{
	if (OtherActor->GetClass() != this->GetClass() && bIsAttacking)
	{
	}
}

void AweaponActor::BeginWeaponAttack(FGameplayTag Event_Tag, float Attack_Time_Delay, int32 Attack_Delay_Count)
{
	attackEventTag = Event_Tag;
	attackDelayCount = Attack_Delay_Count;
	attackTimeDelay = Attack_Time_Delay;
	bIsAttacking = true;

	CapsuleComponent->SetCollisionEnabled(ECollisionEnabled::QueryOnly);
}

void AweaponActor::EndWeaponAttack()
{
	bIsAttacking = true;

	CapsuleComponent->SetCollisionEnabled(ECollisionEnabled::NoCollision);
}

// Called when the game starts or when spawned

// Called every frame
void AweaponActor::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
}