using BabyHaven.Repositories.DBContext;
using BabyHaven.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Repositories
{
    public class UnitOfWork
    {
        private SWP391_ChildGrowthTrackingSystemContext context;
        private MembershipPackageRepository membershipPackageRepository;
        private DiseaseRepository diseaseRepository;
        public UnitOfWork() 
        {
            context ??= new SWP391_ChildGrowthTrackingSystemContext();
        }

        public MembershipPackageRepository MembershipPackageRepository
        {
            get
            {
                return membershipPackageRepository ??= new MembershipPackageRepository(context);
            }
        }

        public DiseaseRepository DiseaseRepository
        {
            get
            {
                return diseaseRepository ??= new DiseaseRepository(context);
            }
        }
    }
}