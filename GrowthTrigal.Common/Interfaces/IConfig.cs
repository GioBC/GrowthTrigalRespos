using SQLite.Net.Interop;

namespace GrowthTrigal.Common.Interfaces
{
    public interface IConfig
    {
        string DirectoryDB { get; }

        ISQLitePlatform Platform { get; }
    }


}
