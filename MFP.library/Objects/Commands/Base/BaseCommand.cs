using System;
using System.IO;

namespace MFP.library.Objects.Commands.Base
{
    public class BaseCommand
    {
        internal BaseCommand(BinaryReader bReader, Func<Stream> streamProvider)
        {
            StreamProvider = streamProvider;

            BReader = bReader;
        }
        
        protected readonly BinaryReader BReader;

        protected readonly Func<Stream> StreamProvider;
    }
}