using System.Threading.Tasks;
using cTime.Core.Data;

namespace cTime.Core.Service
{
    public interface ICTimeStampHelperCallback
    {
        Task OnNotLoggedIn();

        bool SupportsQuestions();

        Task OnDidNothing();

        Task<bool> OnAlreadyCheckedInWannaCheckOut();
        Task OnAlreadyCheckedIn();

        Task<bool> OnAlreadyCheckedOutWannaCheckIn();
        Task OnAlreadyCheckedOut();

        Task OnSuccess(TimeState timeState);
    }
}