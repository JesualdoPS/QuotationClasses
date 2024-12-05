namespace Calc.Persistance
{
    public interface IRepository
    {
        List<MathLog> Memory { get; set; }
        int MemoryPosition { get; set; }

        void LoadMemory(string filePath);
        void SaveMemory(string filePath);
    }
}