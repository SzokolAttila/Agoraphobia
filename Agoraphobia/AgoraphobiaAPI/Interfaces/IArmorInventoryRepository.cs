﻿using AgoraphobiaAPI.Dtos.ArmorInventory;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Interfaces;

public interface IArmorInventoryRepository
{
    public Task<List<ArmorInventory>> GetArmorInventoriesAsync(int id);
    public Task<ArmorInventory> CreateAsync(ArmorInventory armorInventory);
    public Task<ArmorInventory?> AddOneAsync(CreateArmorInventoryRequestDto update);
}