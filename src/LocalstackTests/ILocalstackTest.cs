using System.Threading.Tasks;

namespace LocalstackS3SetupForNetCore.LocalstackTests
{
    public interface ILocalstackTest
    {
        Task Execute();
    }
}