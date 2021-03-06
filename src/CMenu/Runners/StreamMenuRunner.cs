﻿using System.IO;
using System.Text;

namespace CMenu.Runners
{
    public class StreamMenuRunner : MenuRunner
    {
        public StreamMenuRunner(Stream reader, Stream writer, Encoding encoding)
            : base(new StreamReader(reader, encoding), new StreamWriter(writer, encoding))
        {
        }
    }
}