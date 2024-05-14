using AgoraphobiaAPI.Dtos.Player;
using AgoraphobiaLibrary;

namespace AgoraphobiaAPI.Mappers;

public static class PlayerMapper
{
    public static Player ToAccountFromCreateDto(this CreatePlayerRequestDto playerDto)
    {
        return new Player(playerDto.AccountId);
    }
}