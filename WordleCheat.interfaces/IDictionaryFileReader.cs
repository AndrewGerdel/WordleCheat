using System;
namespace WordleCheat.interfaces
{
    public interface IDictionaryFileReader
    {
        List<string> ReadDictionaryFile(string filePath);
    }
}

