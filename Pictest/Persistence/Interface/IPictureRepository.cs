using System.Collections.Generic;
using System.Threading.Tasks;
using Pictest.Persistence.Storage;
using Pictest.Service.Request;

namespace Pictest.Persistence.Interface
{
    public interface IPictureRepository
    {
        Task<PictureStorage> ReadAsync(string id);
        Task<List<PictureStorage>> ReadAllAsync(string cursor);
        Task<List<PictureStorage>> ReadAllByContestAsync(string cursor, string contest);
        Task<string> CreateAsync(PictureStorage pictureStorage);
        Task UpdateAsync(string pictureId, PictureStorage pictureStorage);
        Task<string> ReadPictureUserWithMostVotes(string id);
    }
}