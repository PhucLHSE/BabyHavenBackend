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
        private FeatureRepository featureRepository;
        private MembershipPackageRepository membershipPackageRepository;
        private PackageFeatureRepository packageFeatureRepository;
        private DiseaseRepository diseaseRepository;
        private RoleRepository roleRepository;

        public UnitOfWork() 
        {
            context ??= new SWP391_ChildGrowthTrackingSystemContext();
        }

        public FeatureRepository FeatureRepository
        {
            get
            {
                return featureRepository ??= new FeatureRepository(context);
            }
        }

        public MembershipPackageRepository MembershipPackageRepository
        {
            get
            {
                return membershipPackageRepository ??= new MembershipPackageRepository(context);
            }
        }

        public PackageFeatureRepository PackageFeatureRepository
        {
            get
            {
                return packageFeatureRepository ??= new PackageFeatureRepository(context);
            }
        }

        public DiseaseRepository DiseaseRepository
        {
            get
            {
                return diseaseRepository ??= new DiseaseRepository(context);
            }
        }
        public RoleRepository RoleRepository
        {
            get
            {
                return roleRepository ??= new RoleRepository(context);
            }
        }
    }
}