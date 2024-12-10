

namespace Contracts
{
    public interface IRepository
    {
        List<MathLog> Memory { get; }
        int MemoryPosition { get; set; }

        void LoadMemory(string filePath);
        void SaveMemory(string filePath);
    }
}