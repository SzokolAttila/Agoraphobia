namespace AgoraphobiaLibrary.Exceptions.Player;

public class InventoryAlreadyFullException : Exception
{
    public InventoryAlreadyFullException()
        : base ("Your inventory is full!")
    {
        
    }
}