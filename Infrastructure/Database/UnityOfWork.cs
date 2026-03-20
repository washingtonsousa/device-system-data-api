using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    internal interface IUnityOfWork
    {
          bool Commit();
          Task<bool> CommitAsync();   
    }
    internal class UnityOfWork : IUnityOfWork
    {
        DatabaseContext Context { get; }

        public bool Commit()
        {
            try
            {
                Context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
               await Context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
