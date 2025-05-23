﻿using CarFactory.Enums;

namespace CarFactory.Utils.UserInputReader;

public class InputReader : IInputReader
{
    public int GetValidUserOption()
    {
        while ( true )
        {
            if ( int.TryParse( Console.ReadLine(), out int option ) && option > 0 )
            {
                return option;
            }

            Console.Write( Messages.InvalidOptionSelectInput );
        }
    }

    public UserCommand GetValidUserCommand()
    {
        while ( true )
        {
            Console.Write( "> " );
            string inputUserCommand = Console.ReadLine()?.Trim();

            if ( int.TryParse( inputUserCommand, out int _ ) )
            {
                Console.WriteLine( Messages.UnknownCommandSelected );
                continue;
            }

            if ( Enum.TryParse( typeof( UserCommand ), inputUserCommand, ignoreCase: true, out object? command ) )
            {
                return ( UserCommand )command;
            }

            Console.WriteLine( Messages.UnknownCommandSelected );
        }
    }
}
