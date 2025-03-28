using BabyHaven.Repositories.DBContext;
using BabyHaven.Repositories.Models;
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
        private PromotionRepository promotionRepository;
        private PackagePromotionRepository packagePromotionRepository;
        private TransactionRepository transactionRepository;
        private DiseaseRepository diseaseRepository;
        private RoleRepository roleRepository;
        private UserAccountRepository userAccountRepository;
        private MemberRepository memberRepository;
        private MemberMembershipRepository memberMembershipRepository;
        private SpecializationRepository specializationRepository;
        private ChildrenRepository childrenRepository;
        private GrowthRecordRepository growthRecordRepository;
        private DoctorRepository doctorRepository;
        private DoctorSpecializationRepository doctorSpecializationRepository;
        private AlertRepository alertRepository;
        private MilestoneRepository milestoneRepository;
        private ChildMilestoneRepository childMilestoneRepository;
        private BlogCategoryRepository blogCategoryRepository;
        private BlogRepository blogRepository;
        private ConsultationRequestRepository consultationRequestRepository;
        private ConsultationResponseRepository consultationResponseRepository;
        private RatingFeedbackRepository ratingFeedbackRepository;
        private BmiPercentileRepository bmiPercentileRepository;

        public UnitOfWork() 
        {
            context ??= new SWP391_ChildGrowthTrackingSystemContext();
        }

        public BmiPercentileRepository BmiPercentileRepository
        {
            get
            {
                return bmiPercentileRepository ??= new BmiPercentileRepository(context);
            }
        }

        public ChildMilestoneRepository ChildMilestoneRepository
        {
            get
            {
                return childMilestoneRepository ??= new ChildMilestoneRepository(context);
            }
        }

        public MilestoneRepository MilestoneRepository
        {
            get
            {
                return milestoneRepository ??= new MilestoneRepository(context);
            }
        }

        public AlertRepository AlertRepository
        {
            get
            {
                return alertRepository ??= new AlertRepository(context);
            }
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

        public PromotionRepository PromotionRepository
        {
            get
            {
                return promotionRepository ??= new PromotionRepository(context);
            }
        }

        public PackagePromotionRepository PackagePromotionRepository
        {
            get
            {
                return packagePromotionRepository ??= new PackagePromotionRepository(context);
            }
        }

        public TransactionRepository TransactionRepository
        {
            get
            {
                return transactionRepository ??= new TransactionRepository(context);
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
      
        public UserAccountRepository UserAccountRepository
        { 
            get
            {
                return userAccountRepository ??= new UserAccountRepository(context);
            } 
        }

        public MemberRepository MemberRepository
        {
            get
            {
                return memberRepository ??= new MemberRepository(context);
            }
        }

        public MemberMembershipRepository MemberMembershipRepository
        {
            get
            {
                return memberMembershipRepository ??= new MemberMembershipRepository(context);
            }
        }

        public ChildrenRepository ChildrenRepository
        {
            get
            {
                return childrenRepository ??= new ChildrenRepository(context);
            }
        }

        public GrowthRecordRepository GrowthRecordRepository
        {
            get
            {
                return growthRecordRepository ??= new GrowthRecordRepository(context);
            }
        }
      
        public SpecializationRepository SpecializationRepository
        { 
            get
            {
                return specializationRepository ??= new SpecializationRepository(context);
            }
        }

        public DoctorRepository DoctorRepository
        {
            get
            {
                return doctorRepository ??= new DoctorRepository(context);
            }
        }

        public DoctorSpecializationRepository DoctorSpecializationRepository
        {
            get
            {
                return doctorSpecializationRepository ??= new DoctorSpecializationRepository(context);
            }
        }
        public BlogCategoryRepository BlogCategoryRepository
        {
            get
            {
                return blogCategoryRepository ??= new BlogCategoryRepository(context);
            }
        }
        public BlogRepository BlogRepository
        {
            get
            {
                return blogRepository ??= new BlogRepository(context);
            }
        }

        public ConsultationRequestRepository ConsultationRequestRepository
        {
            get
            {
                return consultationRequestRepository ??= new ConsultationRequestRepository(context);
            }
        }

        public ConsultationResponseRepository ConsultationResponseRepository
        {
            get
            {
                return consultationResponseRepository ??= new ConsultationResponseRepository(context); 
            }
        }

        public RatingFeedbackRepository RatingFeedbackRepository
        {
            get 
            {
                return ratingFeedbackRepository ??= new RatingFeedbackRepository(context);
            }
        }
    }
}