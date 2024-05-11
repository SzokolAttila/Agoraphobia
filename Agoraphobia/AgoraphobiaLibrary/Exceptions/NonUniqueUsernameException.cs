﻿namespace AgoraphobiaLibrary.Exceptions;

public class NonUniqueUsernameException : Exception
{
    public NonUniqueUsernameException()
        : base("The username you have chosen is already in use!")
    {
        
    }
}