using Domain.UnityOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{

    public class UnityOfWork : IUnityOfWork
    {
        public UnityOfWork(DatabaseContext context)
        {
            Context = context;
        }

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
