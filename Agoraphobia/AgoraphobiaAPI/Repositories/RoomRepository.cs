﻿using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Dtos.Room;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaLibrary;
using Microsoft.EntityFrameworkCore;

namespace AgoraphobiaAPI.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDBContext _context;
    public RoomRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<Room>> GetAllAsync()
    {
        return await _context.Rooms
            .Include(x => x.Weapons)
            .ThenInclude(x => x.Weapon)
            .Include(x => x.Armors)
            .ThenInclude(x => x.Armor)
            .Include(x => x.Consumables)
            .ThenInclude(x => x.Consumable)
            .Include(x=>x.Enemy)
            .ToListAsync();
    }

    public async Task<Room?> GetByIdAsync(int id)
    {
        return await _context.Rooms
            .Include(x => x.Weapons)
            .ThenInclude(x => x.Weapon)
            .Include(x => x.Armors)
            .ThenInclude(x => x.Armor)
            .Include(x => x.Consumables)
            .ThenInclude(x => x.Consumable)
            .Include(x=>x.Enemy)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Room?> CreateAsync(Room room)
    {
        await _context.Rooms.AddAsync(room);
        await _context.SaveChangesAsync();
        return room;
    }

    public async Task<Room?> DeleteAsync(int id)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);
        if (room is null)
            return null;
        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();
        return room;
    }

    public async Task<Room?> UpdateAsync(int id, CreateRoomRequestDto roomDto)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);
        if (room is null)
            return null;

        room.Name = roomDto.Name;
        room.Description = roomDto.Description;
        room.OrientationId = roomDto.OrientationId;
        room.EnemyId = roomDto.EnemyId;

        await _context.SaveChangesAsync();
        return room;
    }
}