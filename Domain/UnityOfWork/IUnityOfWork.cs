using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnityOfWork
{
    public interface IUnityOfWork
    {
        bool Commit();
        Task<bool> CommitAsync();
    }
}
