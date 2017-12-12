using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace _4CounterService
{
    public class CounterService:MarshalByRefObject
    {
        private int _count;
        public int GetCount()
        {
            _count++;
            return this._count;
        }
        public CounterService()
        {
            Console.WriteLine("Counter Object has been activated!");
        }
        ~CounterService()
        {
            Console.WriteLine("Counter Object has been destroied!");
        }
        public override object InitializeLifetimeService()
        {
            ILease lease = (ILease)base.InitializeLifetimeService();
            if (lease.CurrentState == LeaseState.Initial)
            {
                lease.InitialLeaseTime = TimeSpan.FromSeconds(1);
                lease.RenewOnCallTime = TimeSpan.FromSeconds(1);
                lease.SponsorshipTimeout = TimeSpan.FromSeconds(1);
            }
            return lease;
        }


    }
}
