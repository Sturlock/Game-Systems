// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#include "RPGPlayerControllerBase.h"
#include "RPGCharacterBase.h"
#include "RPGGameInstanceBase.h"
#include "ItemDataAsset.h"

// LA -
// Prevents code optimisation which is useful for stepping through as it means
// each line accurately matches where we are in execution
// Although it MUST be turned back on at the end of the file
// See: https://docs.microsoft.com/en-us/cpp/preprocessor/optimize?view=msvc-160
#pragma optimize("", off)

bool ARPGPlayerControllerBase::AddInventoryItem(UItemDataAsset* NewItem, ERPGItemType ItemType, int32 ItemCount, int32 ItemLevel, bool bAutoSlot)
{
	bool bChanged = false;
	if (!NewItem || ItemType == ERPGItemType::Undefined)
	{
		UE_LOG(LogActionRPG, Warning, TEXT("AddInventoryItem: Failed trying to add null item!"));
		return false;
	}

	if (ItemCount <= 0 || ItemLevel <= 0)
	{
		UE_LOG(LogActionRPG, Warning, TEXT("AddInventoryItem: Failed trying to add item %s with negative count or level!"), *NewItem->GetName());
		return false;
	}

	if (!GetGameInstance() || !GetGameInstance()->ItemExists(NewItem, ItemType))
	{
		UE_LOG(LogActionRPG, Warning, TEXT("AddInventoryItem: Failed trying to add item %s could not find on game instance!"), *NewItem->GetName());
		return false;
	}

	// Find current item data, which may be empty
	FRPGItemData OldData;
	GetInventoryItemData(NewItem, OldData);

	UItemDataAsset* itemData = GetGameInstance()->GetBaseItemData(NewItem, ItemType);

	// Find modified data
	FRPGItemData NewData = OldData;
	NewData.UpdateItemData(FRPGItemData(ItemCount, ItemLevel, ItemType), itemData->MaxCount, itemData->MaxLevel);

	if (OldData != NewData)
	{
		// If data changed, need to update storage and call callback
		InventoryData.Add(NewItem, NewData);
		NotifyInventoryItemChanged(true, NewItem, ItemType);
		bChanged = true;
	}

	if (bAutoSlot)
	{
		// Slot item if required
		bChanged |= FillEmptySlotWithItem(NewItem, ItemType);
	}

	if (bChanged)
	{
		return true;
	}
	return false;
}

bool ARPGPlayerControllerBase::RemoveInventoryItem(UItemDataAsset* RemovedItem, int32 RemoveCount)
{
	if (!RemovedItem)
	{
		UE_LOG(LogActionRPG, Warning, TEXT("RemoveInventoryItem: Failed trying to remove null item!"));
		return false;
	}

	// Find current item data, which may be empty
	FRPGItemData NewData;
	GetInventoryItemData(RemovedItem, NewData);

	if (!NewData.IsValid())
	{
		// Wasn't found
		return false;
	}

	// If RemoveCount <= 0, delete all
	if (RemoveCount <= 0)
	{
		NewData.ItemCount = 0;
	}
	else
	{
		NewData.ItemCount -= RemoveCount;
	}

	if (NewData.ItemCount > 0)
	{
		// Update data with new count
		InventoryData.Add(RemovedItem, NewData);
	}
	else
	{
		// Remove item entirely, make sure it is unslotted
		InventoryData.Remove(RemovedItem);

		for (TPair<FRPGItemSlot, UItemDataAsset*>& Pair : SlottedItems)
		{
			if (Pair.Value == RemovedItem)
			{
				Pair.Value = nullptr;
				NotifySlottedItemChanged(Pair.Key, Pair.Value, Pair.Key.ItemType);
			}
		}
	}

	// If we got this far, there is a change so notify and save
	NotifyInventoryItemChanged(false, RemovedItem, NewData.ItemType);
	return true;
}

void ARPGPlayerControllerBase::GetInventoryItems(TArray<UItemDataAsset*>& Items, ERPGItemType ItemType)
{
	for (const TPair<UItemDataAsset*, FRPGItemData>& Pair : InventoryData)
	{
		if (Pair.Value.ItemType == ItemType || ItemType == ERPGItemType::Undefined)
		{
			Items.Add(Pair.Key);
		}
	}
}

bool ARPGPlayerControllerBase::SetSlottedItem(FRPGItemSlot ItemSlot, UItemDataAsset* Item)
{
	// Iterate entire inventory because we need to remove from old slot
	bool bFound = false;
	for (TPair<FRPGItemSlot, UItemDataAsset*>& Pair : SlottedItems)
	{
		if (Pair.Key == ItemSlot)
		{
			// Add to new slot
			bFound = true;
			Pair.Value = Item;
			NotifySlottedItemChanged(Pair.Key, Pair.Value, Pair.Key.ItemType);
		}
		else if (Item != nullptr && Pair.Value == Item)
		{
			// If this item was found in another slot, remove it
			Pair.Value = nullptr;
			NotifySlottedItemChanged(Pair.Key, Pair.Value, Pair.Key.ItemType);
		}
	}

	if (bFound)
	{
		return true;
	}
	return false;
}

int32 ARPGPlayerControllerBase::GetInventoryItemCount(UItemDataAsset* Item) const
{
	const FRPGItemData* FoundItem = InventoryData.Find(Item);

	if (FoundItem)
	{
		return FoundItem->ItemCount;
	}
	return 0;
}

bool ARPGPlayerControllerBase::GetInventoryItemData(UItemDataAsset* Item, FRPGItemData& ItemData) const
{
	const FRPGItemData* FoundItem = InventoryData.Find(Item);

	if (FoundItem)
	{
		ItemData = *FoundItem;
		return true;
	}
	ItemData = FRPGItemData(0, 0, ERPGItemType::Undefined);
	return false;
}

UItemDataAsset* ARPGPlayerControllerBase::GetSlottedItem(FRPGItemSlot ItemSlot, UItemDataAsset*& OutItemData) const
{
	
	 UItemDataAsset* const* FoundItem = SlottedItems.Find(ItemSlot);

	if (FoundItem)
	{
		ERPGItemType itemType;
		UWorld* World = GetWorld();
		URPGGameInstanceBase* gi = World ? World->GetGameInstance<URPGGameInstanceBase>() : nullptr;
		if (!gi || !gi->FindItem(*FoundItem, itemType, OutItemData))
		{
			OutItemData = nullptr;
		}
		return *FoundItem;
	}
	OutItemData = nullptr;
	return nullptr;
}

void ARPGPlayerControllerBase::GetSlottedItems(TArray<UItemDataAsset*>& Items, ERPGItemType ItemType, bool bOutputEmptyIndexes)
{
	for (TPair<FRPGItemSlot, UItemDataAsset*>& Pair : SlottedItems)
	{
		if (Pair.Key.ItemType == ItemType || ItemType == ERPGItemType::Undefined)
		{
			Items.Add(Pair.Value);
		}
	}
}

void ARPGPlayerControllerBase::FillEmptySlots()
{
	for (const TPair<UItemDataAsset*, FRPGItemData>& Pair : InventoryData)
	{
		FillEmptySlotWithItem(Pair.Key, Pair.Value.ItemType);
	}
}

void ARPGPlayerControllerBase::InitInventory()
{
	InventoryData.Reset();
	SlottedItems.Reset();

	if (!GetGameInstance())
	{
		return;
	}

	for (const TPair<ERPGItemType, int32>& Pair : GetGameInstance()->SlotsPerItemType)
	{
		for (int32 SlotNumber = 0; SlotNumber < Pair.Value; SlotNumber++)
		{
			SlottedItems.Add(FRPGItemSlot(Pair.Key, SlotNumber));
		}
	}

	// Copy from save game into controller data
	for (const TPair<UItemDataAsset*, FRPGItemData>& ItemPair : GetGameInstance()->DefaultInventoryItems)
	{
		InventoryData.Add(ItemPair.Key, ItemPair.Value);
	}

	FillEmptySlots();
	NotifyInventoryLoaded();
}

URPGGameInstanceBase* ARPGPlayerControllerBase::GetGameInstance()
{
	if (GameInstance == nullptr)
	{
		UWorld* World = GetWorld();
		GameInstance = World ? World->GetGameInstance<URPGGameInstanceBase>() : nullptr;
	}	
	return GameInstance;
}

bool ARPGPlayerControllerBase::FillEmptySlotWithItem(UItemDataAsset* NewItem, ERPGItemType ItemType)
{
	FRPGItemSlot EmptySlot;
	for (TPair<FRPGItemSlot, UItemDataAsset*>& Pair : SlottedItems)
	{
		if (Pair.Key.ItemType == ItemType)
		{
			if (Pair.Value == NewItem)
			{
				// Item is already slotted
				return false;
			}
			else if (!Pair.Value && (!EmptySlot.IsValid() || EmptySlot.SlotNumber > Pair.Key.SlotNumber))
			{
				// We found an empty slot worth filling
				EmptySlot = Pair.Key;
			}
		}
	}

	if (EmptySlot.IsValid())
	{
		SlottedItems[EmptySlot] = NewItem;
		NotifySlottedItemChanged(EmptySlot, NewItem, ItemType);
		return true;
	}

	return false;
}

void ARPGPlayerControllerBase::NotifyInventoryItemChanged(bool bAdded, UItemDataAsset* Item, ERPGItemType ItemType)
{
	// Notify native before blueprint
	OnInventoryItemChangedNative.Broadcast(bAdded, Item, ItemType);
	OnInventoryItemChanged.Broadcast(bAdded, Item, ItemType);

	// Call BP update event
	InventoryItemChanged(bAdded, Item, ItemType);
}

void ARPGPlayerControllerBase::NotifySlottedItemChanged(FRPGItemSlot ItemSlot, UItemDataAsset* Item, ERPGItemType ItemType)
{
	// Notify native before blueprint
	OnSlottedItemChangedNative.Broadcast(ItemSlot, Item, ItemType);
	OnSlottedItemChanged.Broadcast(ItemSlot, Item, ItemType);

	// Call BP update event
	SlottedItemChanged(ItemSlot, Item, ItemType);
}

void ARPGPlayerControllerBase::NotifyInventoryLoaded()
{
	// Notify native before blueprint
	OnInventoryLoadedNative.Broadcast();
	OnInventoryLoaded.Broadcast();
}

void ARPGPlayerControllerBase::BeginPlay()
{
	InitInventory();

	Super::BeginPlay();
}

#pragma optimize("", on)