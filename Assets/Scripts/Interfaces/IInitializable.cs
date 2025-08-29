
namespace Grail
{
    public interface IInitializable
    {
        public int SortingIndex { get; }
        
        public void Initialize();
    }
}
