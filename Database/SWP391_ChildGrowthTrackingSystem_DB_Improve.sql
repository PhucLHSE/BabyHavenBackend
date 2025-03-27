IF EXISTS (SELECT * FROM sys.databases WHERE name = 'SWP391_ChildGrowthTrackingSystemDB')
BEGIN
    DROP DATABASE SWP391_ChildGrowthTrackingSystemDB;
END

CREATE DATABASE SWP391_ChildGrowthTrackingSystemDB;

USE SWP391_ChildGrowthTrackingSystemDB;

-- Table Roles
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName VARCHAR(255) NOT NULL,                        -- Tên của vai trò (ví dụ: Admin, Member, Doctor)
    Description NVARCHAR(2000),                            -- Mô tả chi tiết về vai trò
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',         -- Trạng thái (Inactive, Active, Pending, etc.)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Thời gian tạo vai trò
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP  -- Thời gian cập nhật vai trò
);

-- Table UserAccounts
CREATE TABLE UserAccounts (
    UserID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),    -- ID người dùng
    Username VARCHAR(255) UNIQUE NOT NULL,                  -- Tên người dùng duy nhất
    Email VARCHAR(255) UNIQUE NOT NULL,                     -- Email duy nhất
    PhoneNumber VARCHAR(20) UNIQUE NOT NULL,                -- Số điện thoại duy nhất
    Name NVARCHAR(255) NOT NULL,                            -- Tên người dùng
    Gender NVARCHAR(20) NOT NULL,                           -- Giới tính (Female, Male, Other)
    DateOfBirth DATE,                                       -- Ngày sinh
    Address NVARCHAR(255) NOT NULL,                         -- Địa chỉ
    Password VARCHAR(255) NOT NULL,                         -- Mật khẩu
    RegistrationDate DATETIME NOT NULL,                     -- Ngày đăng ký
    LastLogin DATETIME,                                     -- Ngày đăng nhập lần cuối
    ProfilePicture VARBINARY(2000),                         -- Lưu ảnh dưới dạng nhị phân
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',          -- Trạng thái người dùng
    VerificationCode VARCHAR(10),                           -- Mã xác thực
    IsVerified BIT DEFAULT 0,                               -- Trạng thái xác thực người dùng
    RoleID INT NOT NULL,                                    -- Vai trò người dùng (tham chiếu tới Roles)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian tạo người dùng
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian cập nhật người dùng
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)           -- Liên kết với bảng Roles
);

-- Table Members
CREATE TABLE Members (
    MemberID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),  -- ID của thành viên
    UserID UNIQUEIDENTIFIER UNIQUE NOT NULL,                -- ID người dùng liên kết với bảng UserAccounts
    EmergencyContact NVARCHAR(255) NOT NULL,                -- Thông tin liên lạc khẩn cấp
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',          -- Trạng thái thành viên (1: active, 2: inactive, 3: pending, etc.)
    JoinDate DATETIME NOT NULL,                             -- Ngày gia nhập
    LeaveDate DATETIME,                                     -- Ngày rời khỏi hệ thống (nếu có)
	Notes NVARCHAR(2000),                                   -- Ghi chú thêm
    FOREIGN KEY (UserID) REFERENCES UserAccounts(UserID)    -- Liên kết với bảng UserAccounts
);

-- Table Children
CREATE TABLE Children (
    ChildID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),   -- ID của trẻ
    MemberID UNIQUEIDENTIFIER  NOT NULL,                    -- ID thành viên liên kết với bảng Members
    Name NVARCHAR(255) NOT NULL,                            -- Tên trẻ
    DateOfBirth DATE NOT NULL,                              -- Ngày sinh
    Gender NVARCHAR(20) NOT NULL,                           -- Giới tính (Female, Male, Other)
    BirthWeight FLOAT,                                      -- Cân nặng lúc sinh
    BirthHeight FLOAT,                                      -- Chiều cao lúc sinh
    BloodType VARCHAR(10),                                  -- Nhóm máu
    Allergies NVARCHAR(2000),                               -- Dị ứng (nếu có)
    Notes NVARCHAR(2000),                                   -- Ghi chú thêm
	RelationshipToMember NVARCHAR(50) NOT NULL,             -- Mối quan hệ với member
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',          -- Trạng thái (active, inactive, pending, v.v.)
	CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian tạo thông tin trẻ
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian cập nhật thông tin (nếu có thay đổi)
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID)     -- Liên kết với bảng Members
);

-- Table GrowthRecords
CREATE TABLE GrowthRecords (
    RecordID INT PRIMARY KEY IDENTITY(1,1),               -- ID bản ghi
    ChildID UNIQUEIDENTIFIER NOT NULL,                    -- ID của trẻ
    RecordedBy UNIQUEIDENTIFIER NOT NULL,                 -- ID người ghi nhận
    Weight FLOAT NOT NULL,                                -- Cân nặng của trẻ
    Height FLOAT NOT NULL,                                -- Chiều cao của trẻ
    HeadCircumference FLOAT,                              -- Vòng đầu
    MuscleMass FLOAT,                                     -- Khối lượng cơ bắp
    ChestCircumference FLOAT,                             -- Vòng ngực
    NutritionalStatus NVARCHAR(50),                       -- Tình trạng dinh dưỡng
    FerritinLevel FLOAT,                                  -- Mức ferritin
    Triglycerides FLOAT,                                  -- Mức triglycerides
    BloodSugarLevel FLOAT,                                -- Mức đường huyết
    PhysicalActivityLevel NVARCHAR(50),                   -- Mức độ hoạt động thể chất
    HeartRate INT,                                        -- Nhịp tim
    BloodPressure FLOAT,                                  -- Huyết áp
    BodyTemperature FLOAT,                                -- Nhiệt độ cơ thể
    OxygenSaturation FLOAT,                               -- Mức độ bão hòa oxy
    SleepDuration FLOAT,                                  -- Thời gian ngủ
    Vision NVARCHAR(50),                                  -- Tình trạng thị giác
    Hearing NVARCHAR(50),                                 -- Tình trạng thính giác
    ImmunizationStatus NVARCHAR(2000),                    -- Tình trạng tiêm chủng
    MentalHealthStatus NVARCHAR(50),                      -- Tình trạng sức khỏe tâm thần
    GrowthHormoneLevel FLOAT,                             -- Mức độ hormone tăng trưởng
    AttentionSpan NVARCHAR(50),                           -- Thời gian chú ý
    NeurologicalReflexes NVARCHAR(255),                   -- Phản xạ thần kinh
    DevelopmentalMilestones NVARCHAR(255),                -- Mốc phát triển
    Notes NVARCHAR(2000),                                 -- Ghi chú khác
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',        -- Trạng thái
    Verified BIT DEFAULT 0,                               -- Trạng thái xác nhận
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,         -- Thời gian tạo bản ghi
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,         -- Thời gian cập nhật bản ghi
    FOREIGN KEY (ChildID) REFERENCES Children(ChildID),   -- Liên kết với bảng Children
    FOREIGN KEY (RecordedBy) REFERENCES Members(MemberID) -- Liên kết với bảng UserAccounts
);

-- Table Promotions
CREATE TABLE Promotions (
    PromotionID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(), -- Mã khuyến mãi
    PromotionCode VARCHAR(50) NOT NULL,                       -- Mã khuyến mãi duy nhất
    Description NVARCHAR(2000),                               -- Mô tả chi tiết về khuyến mãi
    DiscountPercent INT NOT NULL,                             -- Phần trăm giảm giá
    MinPurchaseAmount DECIMAL(10, 2),                         -- Mức mua tối thiểu để áp dụng khuyến mãi
    MaxDiscountAmount DECIMAL(10, 2),                         -- Giới hạn giảm giá tối đa
    ApplicablePackageIDs NVARCHAR(2000),                      -- Các gói thành viên áp dụng (nếu có)
    TargetAudience VARCHAR(255),                              -- Đối tượng áp dụng khuyến mãi
    StartDate DATE NOT NULL,                                  -- Ngày bắt đầu khuyến mãi
    EndDate DATE NOT NULL,                                    -- Ngày kết thúc khuyến mãi
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',            -- Trạng thái khuyến mãi (Active, Inactive, Expired, Cancelled)
    RedemptionCount INT DEFAULT 0,                            -- Số lần mã đã được sử dụng
    UsageLimit INT,                                           -- Giới hạn số lần sử dụng khuyến mãi
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,    -- Thời gian tạo khuyến mãi
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,    -- Thời gian cập nhật khuyến mãi
    CreatedBy UNIQUEIDENTIFIER NOT NULL,                      -- Người tạo mã khuyến mãi
    ModifiedBy UNIQUEIDENTIFIER NOT NULL,                     -- Người sửa mã gần đây
    FOREIGN KEY (CreatedBy) REFERENCES UserAccounts(UserID),
    FOREIGN KEY (ModifiedBy) REFERENCES UserAccounts(UserID)
);

-- Table MembershipPackages
CREATE TABLE MembershipPackages (
    PackageID INT PRIMARY KEY IDENTITY(1,1),
    PackageName NVARCHAR(255) NOT NULL,                     -- Tên gói thành viên
    Description NVARCHAR(2000),                             -- Mô tả về gói thành viên
    Price DECIMAL(10, 2) NOT NULL,                          -- Giá gói thành viên
    Currency NVARCHAR(10) DEFAULT 'VND',                    -- Đơn vị tiền tệ của giá gói
    DurationMonths INT NOT NULL,                            -- Thời gian sử dụng gói trong tháng
    TrialPeriodDays INT,                                    -- Thời gian dùng thử (nếu có)
    MaxChildrenAllowed INT NOT NULL,                        -- Số lượng trẻ tối đa được phép trong gói
    SupportLevel NVARCHAR(255),                             -- Mức độ hỗ trợ của gói thành viên
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',          -- Trạng thái của gói thành viên (1: active, 0: inactive, 2: pending)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian tạo gói thành viên
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian cập nhật
);

-- Table PackagePromotions
CREATE TABLE PackagePromotions (
    PackageID INT NOT NULL,
    PromotionID UNIQUEIDENTIFIER NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,                         -- Trạng thái hoạt động của mối quan hệ (1: Active, 0: Inactive)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,   -- Thời gian mối quan hệ này được tạo
	UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (PackageID, PromotionID),
    FOREIGN KEY (PackageID) REFERENCES MembershipPackages(PackageID),
    FOREIGN KEY (PromotionID) REFERENCES Promotions(PromotionID)
);

-- Table Features
CREATE TABLE Features (
    FeatureID INT PRIMARY KEY IDENTITY(1,1),
    FeatureName NVARCHAR(255) NOT NULL,
    Description NVARCHAR(2000),
	Status NVARCHAR(50) NOT NULL DEFAULT 'Active',
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Table PackageFeatures
CREATE TABLE PackageFeatures (
    PackageID INT,                                           -- Gói thành viên
    FeatureID INT,                                           -- Tính năng
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,   -- Thời gian tạo
	UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	Status NVARCHAR(50) NOT NULL DEFAULT 'Active',
    PRIMARY KEY (PackageID, FeatureID),                      -- Khóa chính kép
    FOREIGN KEY (PackageID) REFERENCES MembershipPackages(PackageID),
    FOREIGN KEY (FeatureID) REFERENCES Features(FeatureID)
);

-- Table MemberMemberships
CREATE TABLE MemberMemberships (
    MemberMembershipID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    MemberID UNIQUEIDENTIFIER NOT NULL,                               -- Liên kết với bảng Members
    PackageID INT NOT NULL,                                           -- Liên kết với bảng MembershipPackages
    StartDate DATETIME NOT NULL,                                      -- Ngày bắt đầu gói thành viên
    EndDate DATETIME NOT NULL,                                        -- Ngày kết thúc gói thành viên
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',                    -- Trạng thái của gói thành viên (Active, Inactive, Pending, Suspended, Expired, Canceled, Renewing, Trial)
    IsActive BIT NOT NULL DEFAULT 1,                                  -- Trạng thái hoạt động của gói
    Description NVARCHAR(2000),                                       -- Mô tả gói thành viên
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,            -- Thời gian tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,            -- Thời gian cập nhật
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID),              -- Liên kết với bảng Members
    FOREIGN KEY (PackageID) REFERENCES MembershipPackages(PackageID)  -- Liên kết với bảng MembershipPackages
);

CREATE TABLE Transactions (
    TransactionID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserID UNIQUEIDENTIFIER NOT NULL,
    MemberMembershipID UNIQUEIDENTIFIER NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    Currency NVARCHAR(50) NOT NULL,
    TransactionType NVARCHAR(100) NOT NULL,                      -- Loại giao dịch (ví dụ: Đăng ký, Mua thẻ, v.v.)
    PaymentMethod NVARCHAR(50) NOT NULL,                         -- Cổng thanh toán (VnPay, MoMo, Internet Banking)
    TransactionDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Thời điểm giao dịch (ngày người dùng bắt đầu giao dịch)
    PaymentDate DATETIME,                                        -- Thời điểm thanh toán (ngày thanh toán được xử lý thành công, cập nhật khi Status = 'Success')
    GatewayTransactionID bigint NOT NULL,                        -- Mã giao dịch từ cổng thanh toán (ví dụ: mã giao dịch VnPay, MoMo)
    PaymentStatus NVARCHAR(50) NOT NULL DEFAULT 'Active',        -- Trạng thái thanh toán (Pending, Success, Failed, Cancelled, Refunded)
    Description NVARCHAR(255),                                   -- Mô tả giao dịch
    FOREIGN KEY (UserID) REFERENCES UserAccounts(UserID),
    FOREIGN KEY (MemberMembershipID) REFERENCES MemberMemberships(MemberMembershipID)
);

-- Table Doctors
CREATE TABLE Doctors (
    DoctorID INT PRIMARY KEY IDENTITY(1,1),
    UserID UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    PhoneNumber VARCHAR(20) UNIQUE NOT NULL,
    Degree NVARCHAR(255) NOT NULL,
	HospitalName NVARCHAR(255),                      -- Tên bệnh viện làm việc
    HospitalAddress NVARCHAR(255),                   -- Địa chỉ bệnh viện
    Biography NVARCHAR(2000),
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserID) REFERENCES UserAccounts(UserID)
);

-- Table Specializations
CREATE TABLE Specializations (
    SpecializationID INT PRIMARY KEY IDENTITY(1,1),             -- Sử dụng UNIQUEIDENTIFIER làm khóa chính và tự động tạo GUID
    SpecializationName NVARCHAR(255) NOT NULL,                  -- Tên chuyên ngành không thể null
    Description NVARCHAR(2000),                                 -- Mô tả về chuyên ngành
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',              -- Trạng thái của chuyên ngành (1: active, 0: inactive)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,      -- Thời gian tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP       -- Thời gian cập nhật
);

-- Table DoctorSpecializations
CREATE TABLE DoctorSpecializations (
    DoctorSpecializationID INT PRIMARY KEY IDENTITY(1,1),                       -- ID mối quan hệ bác sĩ-chuyên ngành
    DoctorID INT NOT NULL,                                                      -- ID bác sĩ (liên kết với bảng Doctors)
    SpecializationID INT NOT NULL,                                              -- ID chuyên ngành (liên kết với bảng Specializations)
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',                              -- Trạng thái (1: active, 0: inactive)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,                      -- Thời gian tạo mối quan hệ
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,                      -- Thời gian cập nhật mối quan hệ
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID),                        -- Liên kết với bảng Doctors
    FOREIGN KEY (SpecializationID) REFERENCES Specializations(SpecializationID) -- Liên kết với bảng Specializations
);

-- Table ConsultationRequests
CREATE TABLE ConsultationRequests (
    RequestID INT PRIMARY KEY IDENTITY(1,1),                         -- ID duy nhất cho yêu cầu
    MemberID UNIQUEIDENTIFIER NOT NULL,                              -- Liên kết với bảng Members
    ChildID UNIQUEIDENTIFIER NOT NULL,                               -- Liên kết với bảng Children
	DoctorID INT NOT NULL,											 -- Liên kết với bảng Doctor
    RequestDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,         -- Ngày tạo yêu cầu tư vấn
    Description NVARCHAR(MAX),                                      -- Mô tả chi tiết về yêu cầu tư vấn
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',                  -- Trạng thái yêu cầu: 0 (pending), 1 (approved), 2 (rejected)
    Urgency NVARCHAR(50),                                            -- Mức độ khẩn cấp: low, medium, high
    Attachments VARCHAR(1000),                                       -- Đường dẫn tệp đính kèm
    Category NVARCHAR(100),                                          -- Danh mục tư vấn: ví dụ: dinh dưỡng, phát triển, ...
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,           -- Thời gian yêu cầu được tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,           -- Thời gian cập nhật yêu cầu
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID),             -- Liên kết với bảng Members
    FOREIGN KEY (ChildID) REFERENCES Children(ChildID),              -- Liên kết với bảng Children
	FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)              -- Liên kết với bảng Doctor
);

-- Table ConsultationResponses
CREATE TABLE ConsultationResponses (
    ResponseID INT PRIMARY KEY IDENTITY(1,1),                           -- ID phản hồi duy nhất
    RequestID INT UNIQUE NOT NULL,                                      -- Liên kết với bảng ConsultationRequests
    ResponseDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,           -- Thời gian phản hồi
    Content NVARCHAR(MAX) NOT NULL,                                    -- Nội dung phản hồi (bắt buộc)
    Attachments VARCHAR(1000),                                          -- Đường dẫn tệp đính kèm (không bắt buộc)
    IsHelpful BIT NULL,                                                 -- Đánh giá xem phản hồi có hữu ích không (NULL nếu không đánh giá)
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',                     -- Trạng thái phản hồi: 0 (pending), 1 (answered), 2 (resolved)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,              -- Thời gian tạo phản hồi
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,              -- Thời gian cập nhật phản hồi
    FOREIGN KEY (RequestID) REFERENCES ConsultationRequests(RequestID), -- Liên kết với bảng ConsultationRequests
);

-- Table RatingFeedbacks
CREATE TABLE RatingFeedbacks (
    FeedbackId INT PRIMARY KEY IDENTITY(1,1),                             -- ID phản hồi đánh giá
    UserId UNIQUEIDENTIFIER NOT NULL,                                     -- Liên kết với bảng UserAccounts
    ResponseId INT NOT NULL,                                              -- Liên kết với bảng ConsultationResponses
    Rating INT NOT NULL CHECK (Rating BETWEEN 1 AND 5),                   -- Đánh giá từ 1 đến 5
    Comment NVARCHAR(2000) NULL,                                          -- Nhận xét (không bắt buộc)
    FeedbackDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,             -- Thời gian phản hồi
    FeedbackType NVARCHAR(50) NULL DEFAULT 'general',                     -- Loại phản hồi (không bắt buộc, mặc định là 'general')
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',                       -- Trạng thái: 'Pending', 'Approved', 'Rejected'
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,                -- Thời gian tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,                -- Thời gian cập nhật
    FOREIGN KEY (UserId) REFERENCES UserAccounts(UserId),                 -- Liên kết với bảng UserAccounts
    FOREIGN KEY (ResponseId) REFERENCES ConsultationResponses(ResponseId) -- Liên kết với bảng ConsultationResponses
);

-- Table Diseases
CREATE TABLE Diseases (
    DiseaseID INT PRIMARY KEY IDENTITY(1,1),                 -- ID bệnh
    DiseaseName NVARCHAR(100) NOT NULL,                      -- Tên bệnh
    LowerBoundMale FLOAT NOT NULL,                           -- Giới hạn thấp cho chỉ số đối với nam (cân nặng, chiều cao, BMI)
    UpperBoundMale FLOAT NOT NULL,                           -- Giới hạn cao cho chỉ số đối với nam
    LowerBoundFemale FLOAT NOT NULL,                         -- Giới hạn thấp cho chỉ số đối với nữ
    UpperBoundFemale FLOAT NOT NULL,                         -- Giới hạn cao cho chỉ số đối với nữ
    MinAge INT NOT NULL,                                     -- Độ tuổi nhỏ nhất có thể mắc bệnh
    MaxAge INT NOT NULL,                                     -- Độ tuổi lớn nhất có thể mắc bệnh
    Severity NVARCHAR(50) NOT NULL,                          -- Độ nghiêm trọng (High, Medium, Low)
    DiseaseType NVARCHAR(50) NOT NULL,                       -- Loại bệnh (ví dụ: Béo phì, Suy dinh dưỡng)
    Symptoms NVARCHAR(2000) NOT NULL,                        -- Triệu chứng bệnh
    Treatment NVARCHAR(2000),                                -- Phương pháp điều trị
    Prevention NVARCHAR(2000),                               -- Phương pháp phòng ngừa
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,   -- Thời gian tạo
    LastModified DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,-- Thời gian chỉnh sửa gần nhất
    Description NVARCHAR(2000),                              -- Mô tả về bệnh
    Notes NVARCHAR(2000),                                    -- Ghi chú thêm
    IsActive BIT NOT NULL DEFAULT 1                          -- Trạng thái (Hoạt động hoặc không)
);

-- Table Alerts
CREATE TABLE Alerts (
    AlertID INT PRIMARY KEY IDENTITY(1,1),
    GrowthRecordID INT NOT NULL,                                     -- Liên kết với bảng GrowthRecords
    AlertDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,           -- Ngày giờ cảnh báo
    DiseaseID INT NOT NULL,                                          -- Liên kết với bảng Diseases
    Message NVARCHAR(2000) NOT NULL,                                 -- Nội dung cảnh báo
    IsRead BIT NOT NULL DEFAULT 0,                                   -- Trạng thái đọc
    SeverityLevel NVARCHAR(50),                                      -- Mức độ nghiêm trọng
    IsAcknowledged BIT NOT NULL DEFAULT 0,                           -- Trạng thái xác nhận
    FOREIGN KEY (GrowthRecordID) REFERENCES GrowthRecords(RecordID), -- Liên kết GrowthRecords
    FOREIGN KEY (DiseaseID) REFERENCES Diseases(DiseaseID)           -- Liên kết với Diseases
);

-- Table Milestones
CREATE TABLE Milestones (
    MilestoneID INT PRIMARY KEY IDENTITY(1,1),
    MilestoneName NVARCHAR(255) NOT NULL,                 -- Tên mốc phát triển
    Description NVARCHAR(2000),                           -- Mô tả mốc phát triển
    Importance NVARCHAR(50) NOT NULL DEFAULT 'Medium',    -- Độ quan trọng
    Category NVARCHAR(100),                               -- Nhóm mốc
    MinAge INT NULL,                                      -- Tuổi bắt đầu áp dụng (tháng)
    MaxAge INT NULL,                                      -- Tuổi kết thúc áp dụng (tháng)
    IsPersonal BIT NOT NULL DEFAULT 0,                    -- Phân biệt mốc hệ thống và cá nhân
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,-- Ngày tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP -- Ngày cập nhật
);

-- Table ChildMilestones
CREATE TABLE ChildMilestones (
    ChildID UNIQUEIDENTIFIER NOT NULL,                           -- Liên kết với bảng Children
    MilestoneID INT NOT NULL,                                    -- Liên kết với bảng Milestones
    AchievedDate DATE,                                           -- Ngày đạt mốc
    Status NVARCHAR(50) NOT NULL DEFAULT 'Not Achieved',         -- Trạng thái mốc (Not Achieved, Achieved)
    Notes NVARCHAR(2000),                                        -- Ghi chú
    Guidelines NVARCHAR(2000),                                   -- Hướng dẫn
    Importance NVARCHAR(50) NOT NULL DEFAULT 'Medium',           -- Độ quan trọng
    Category NVARCHAR(100),                                      -- Nhóm mốc
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,       -- Ngày tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,       -- Ngày cập nhật
    PRIMARY KEY (ChildID, MilestoneID),                          -- Khoá chính kết hợp
    FOREIGN KEY (ChildID) REFERENCES Children(ChildID),          -- Liên kết đến bảng Children
    FOREIGN KEY (MilestoneID) REFERENCES Milestones(MilestoneID) -- Liên kết đến bảng Milestones
);

-- Table BlogCategories
CREATE TABLE BlogCategories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),                             -- ID thể loại
    CategoryName NVARCHAR(255) NOT NULL,                                  -- Tên thể loại bài viết (không được NULL)
    Description NVARCHAR(2000),                                           -- Mô tả về thể loại
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,                -- Thời gian tạo thể loại
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,                -- Thời gian cập nhật thể loại
    IsActive BIT NOT NULL DEFAULT 1,                                      -- Trạng thái thể loại (1: Active, 0: Inactive)
    ParentCategoryID INT,                                                 -- ID thể loại cha (nếu có thể loại con)
    FOREIGN KEY (ParentCategoryID) REFERENCES BlogCategories(CategoryID)  -- Liên kết với thể loại cha
);

-- Table Blogs
CREATE TABLE Blogs (
    BlogID INT PRIMARY KEY IDENTITY(1,1),                          -- ID bài viết
    Title NVARCHAR(255) NOT NULL,                                  -- Tiêu đề bài viết
    Content NVARCHAR(MAX) NOT NULL,                               -- Nội dung bài viết
    AuthorID UNIQUEIDENTIFIER NOT NULL,                            -- ID tác giả (admin hoặc người dùng)
	CategoryID INT NOT NULL,                                       -- ID thể loại bài viết
	ImageBlog NVARCHAR(2000) NOT NULL,                             -- URL hoặc đường dẫn tới ảnh
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',                -- Trạng thái bài viết ('Pending', 'Approved', 'Rejected')
	RejectionReason NVARCHAR(2000),                                -- Lý do từ chối (nếu có)
    Tags NVARCHAR(255),                                            -- Các tag của bài viết
    ReferenceSources NVARCHAR(2000),                               -- Các trích dẫn hoặc nguồn tài liệu tham khảo
	CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,         -- Thời gian tạo bài viết
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,         -- Thời gian cập nhật bài viết
    FOREIGN KEY (AuthorID) REFERENCES UserAccounts(UserID),        -- Liên kết với bảng UserAccounts (tác giả)
    FOREIGN KEY (CategoryID) REFERENCES BlogCategories(CategoryID) -- Liên kết với bảng BlogCategories
);

-- Insert Database
-- Insert Roles
INSERT INTO Roles (RoleName, Description, Status) 
VALUES 
('Member', 'Role for regular members who track child growth', 'Active'),
('Doctor', 'Role for doctors to provide consultation and advice', 'Active'),
('Admin', 'Role for administrators who manage the system', 'Active');

GO

-- Insert UserAccounts
-- Admin
INSERT INTO UserAccounts (Username, Email, PhoneNumber, Name, Gender, DateOfBirth, Address, Password, RegistrationDate, RoleID)
VALUES 
('PhucLHSE', 'phuclhse@gmail.com', '0325194357', N'Lê Hoàng Phúc', 'Male', '2003-12-14', N'Bình Định, Việt Nam', 'Admin@123456', GETDATE(), 3);

GO

-- Insert UserAccounts
-- Doctor
INSERT INTO UserAccounts (Username, Email, PhoneNumber, Name, Gender, DateOfBirth, Address, Password, RegistrationDate, RoleID)
VALUES 
('NguyenHNN', 'nguyenhnn@gmail.com', '0902653178', N'Huỳnh Nguyễn Ngọc Nguyên', 'Male', '1985-03-15', N'Bình Dương, Việt Nam', 'Doctor@Nguyen1985', GETDATE(), 2),
('TuanTM1980', 'tuantm@gmail.com', '0903151288', N'Trần Mạnh Tuấn', 'Male', '1980-08-22', N'Đà Nẵng, Việt Nam', 'Doctor@Tuan1980', GETDATE(), 2),
('TuHT1990', 'tuht@gmail.com', '0842139577', N'Huỳnh Thái Tú', 'Male', '1990-05-10', N'TP.HCM, Việt Nam', 'Doctor@Tu1990', GETDATE(), 2),
('ThaoNTT1987', 'thaontt@gmail.com', '0914751925', N'Nguyễn Thị Thanh Thảo', 'Female', '1987-07-30', N'Hải Phòng, Việt Nam', 'Doctor@Thao1987', GETDATE(), 2);

GO

-- Insert UserAccounts
-- Member
INSERT INTO UserAccounts (Username, Email, PhoneNumber, Name, Gender, DateOfBirth, Address, Password, RegistrationDate, RoleID)
VALUES 
('NguyenVA1992', 'nguyenvana1992@gmail.com', '0903000001', N'Nguyễn Văn An', 'Male', '1992-01-10', N'Hà Nội, Việt Nam', 'Member@Nguyen1992', GETDATE(), 1),
('TranTB1995', 'tranthib1995@gmail.com', '0903000002', N'Trần Thị Bông', 'Female', '1995-04-25', N'TP.HCM, Việt Nam', 'Member@Tran1995', GETDATE(), 1),
('LeVC1990', 'levanc1990@gmail.com', '0903000003', N'Lê Văn Cường', 'Male', '1990-09-15', N'Đà Nẵng, Việt Nam', 'Member@Le1990', GETDATE(), 1),
('PhamTD1993', 'phamthid1993@gmail.com', '0903000004', N'Phạm Thị Dung', 'Female', '1993-11-05', N'Hải Phòng, Việt Nam', 'Member@Pham1993', GETDATE(), 1),
('HoangVT1998', 'hoangvt1998@gmail.com', '0903000005', N'Hoàng Văn Tài', 'Male', '1998-06-20', N'Cần Thơ, Việt Nam', 'Member@Hoang1998', GETDATE(), 1),
('DangTT1994', 'dangtt1994@gmail.com', '0903000006', N'Đặng Thị Thùy', 'Female', '1994-03-30', N'Huế, Việt Nam', 'Member@Dang1994', GETDATE(), 1),
('VoVD1996', 'vovd1996@gmail.com', '0903000007', N'Võ Văn Đông', 'Male', '1996-07-18', N'Bình Dương, Việt Nam', 'Member@Vo1996', GETDATE(), 1),
('BuiTH1997', 'buith1997@gmail.com', '0903000008', N'Bùi Thị Hương', 'Female', '1997-12-25', N'Đồng Nai, Việt Nam', 'Member@Bui1997', GETDATE(), 1),
('NgoVS1991', 'ngovs1991@gmail.com', '0903000009', N'Ngô Văn Sơn', 'Male', '1991-05-05', N'Vũng Tàu, Việt Nam', 'Member@Ngovi1991', GETDATE(), 1),
('DinhTN1999', 'dinhtn1999@gmail.com', '0903010010', N'Đinh Thị Nhi', 'Female', '1999-08-12', N'Quảng Ngãi, Việt Nam', 'Member@Dinh1999', GETDATE(), 1),
('PhanVK1993', 'phanvk1993@gmail.com', '0903010011', N'Phan Văn Kha', 'Male', '1993-02-22', N'Phú Quốc, Việt Nam', 'Member@Phan1993', GETDATE(), 1),
('NguyenTL1990', 'nguyentl1990@gmail.com', '0903010012', N'Nguyễn Thị Lý', 'Female', '1990-10-30', N'Nha Trang, Việt Nam', 'Member@Nguyen1990', GETDATE(), 1),
('DoVM1994', 'dovm1994@gmail.com', '0903010013', N'Đỗ Văn Mười', 'Male', '1994-06-08', N'Hải Dương, Việt Nam', 'Member@Do1994', GETDATE(), 1),
('LaTN1992', 'latn1992@gmail.com', '0903010014', N'La Thị Nga', 'Female', '1992-09-21', N'Bắc Ninh, Việt Nam', 'Member@La1992', GETDATE(), 1),
('MacVO1995', 'macvo1995@gmail.com', '0903010015', N'Mạc Văn Ổn', 'Male', '1995-01-17', N'Thanh Hóa, Việt Nam', 'Member@Mac1995', GETDATE(), 1),
('LyTP1997', 'lyt1997@gmail.com', '0903010016', N'Lý Thị Phương', 'Female', '1997-04-05', N'Nghệ An, Việt Nam', 'Member@Ly1997', GETDATE(), 1),
('VuVQ1991', 'vuvq1991@gmail.com', '0903010017', N'Vũ Văn Quốc', 'Male', '1991-07-28', N'Hà Tĩnh, Việt Nam', 'Member@Vu1991', GETDATE(), 1),
('CaoTM1996', 'caotm1996@gmail.com', '0903010018', N'Cao Thị My', 'Female', '1996-11-10', N'Quảng Bình, Việt Nam', 'Member@Cao1996', GETDATE(), 1),
('TanVS1998', 'tanvs1998@gmail.com', '0903010019', N'Tân Văn Sang', 'Male', '1998-02-14', N'Lào Cai, Việt Nam', 'Member@Tan1998', GETDATE(), 1),
('TrinhTT1993', 'trinhtt1993@gmail.com', '0903010020', N'Trịnh Thị Thảo', 'Female', '1993-06-01', N'Yên Bái, Việt Nam', 'Member@Trinh1993', GETDATE(), 1);

GO

-- Insert Membership Packages
INSERT INTO MembershipPackages (PackageName, Description, Price, Currency, DurationMonths, MaxChildrenAllowed, Status)
VALUES
('Free', 'Free membership with basic features', 0.00, 'VND', 1200, 1, 'Active'),
('Standard', 'Standard membership with additional features', 379000.00, 'VND', 3, 2, 'Active'),
('Premium', 'Premium membership with full features', 1279000.00, 'VND', 12, 4, 'Active');

GO

-- Insert Features
INSERT INTO Features (FeatureName, Description, Status)
VALUES
('Update Growth Records', 'Update the growth records of children', 'Active'), -- F1
('Manage/Track Multiple Children', 'Manage and track multiple children', 'Active'), -- F2
('View Basic Growth Charts', 'View basic growth charts (weight, height, BMI)', 'Active'), -- F3
('View Advanced Growth Charts', 'View advanced growth charts', 'Active'), -- F3 Standard-Premium
('Nutrition and Development Alerts', 'Alerts for malnutrition, overweight, and development', 'Active'), -- F4
('Request Doctor Consultation', 'Send a request for doctor consultation', 'Active'), -- F5
('Share Health Data with Doctor', 'Share health data with doctors', 'Active'), -- F6
('Rate and Feedback Consultants', 'Rate and provide feedback to consultants', 'Active'), -- F7
('View Blogs', 'View articles and blogs', 'Active'), -- F8
('Create and Track Milestones', 'Create and track children developmental milestones', 'Active'); -- F9

GO

-- Insert PackageFeatures
INSERT INTO PackageFeatures (PackageID, FeatureID, Status)
VALUES
-- FREE Package
(1, 1, 'Active'), -- F1: Update Growth Records
(1, 3, 'Active'), -- F3: View Basic Growth Charts
(1, 5, 'Active'), -- F4: Nutrition and Development Alerts
(1, 8, 'Active'), -- F8: View Blogs

-- STANDARD Package
(2, 1, 'Active'), -- F1: Update Growth Records
(2, 2, 'Active'), -- F2: Manage/Track Multiple Children
(2, 3, 'Active'), -- F3: View Basic Growth Charts
(2, 4, 'Active'), -- F3 Advanced Charts
(2, 5, 'Active'), -- F4: Nutrition and Development Alerts
(2, 6, 'Active'), -- F6: Share Health Data with Doctor
(2, 7, 'Active'), -- F7: Rate and Feedback Consultants
(2, 8, 'Active'), -- F8: View Blogs
(2, 9, 'Active'), -- F9: Create and Track Milestones

-- PREMIUM Package
(3, 1, 'Active'), -- F1: Update Growth Records
(3, 2, 'Active'), -- F2: Manage/Track Multiple Children
(3, 3, 'Active'), -- F3: View Basic Growth Charts
(3, 4, 'Active'), -- F3 Advanced Charts
(3, 5, 'Active'), -- F4: Nutrition and Development Alerts
(3, 6, 'Active'), -- F6: Share Health Data with Doctor
(3, 7, 'Active'), -- F7: Rate and Feedback Consultants
(3, 8, 'Active'), -- F8: View Blogs
(3, 9, 'Active'); -- F9: Create and Track Milestones

GO
-- Insert Specializations (Chuyên khoa)
INSERT INTO Specializations (SpecializationName, Description, Status)
VALUES
('Pediatrics', N'Chuyên khoa Nhi', 'Active'),
('Endocrinology', N'Chuyên khoa Nội tiết', 'Active'),
('Nutrition', N'Dinh dưỡng và chế độ ăn', 'Active'),
('Cardiology', N'Chuyên khoa Tim mạch', 'Active'),
('Neurology', N'Chuyên khoa Thần kinh', 'Active');

GO

-- Insert Doctors (Bác sĩ từ UserAccounts)
INSERT INTO Doctors (UserID, Name, Email, PhoneNumber, Degree, HospitalName, HospitalAddress, Biography, Status)
VALUES 
((SELECT UserID FROM UserAccounts WHERE Username = 'NguyenHNN'), N'Huỳnh Nguyễn Ngọc Nguyên', 'nguyenhnn@gmail.com', '0902653178', N'Bác sĩ chuyên khoa Nhi', N'Bệnh viện Nhi Đồng 1', N'TP.HCM, Việt Nam', N'Chuyên gia về nhi khoa, có nhiều năm kinh nghiệm trong nghiên cứu phát triển trẻ em.', 'Active'),
((SELECT UserID FROM UserAccounts WHERE Username = 'TuanTM1980'), N'Trần Mạnh Tuấn', 'tuantm@gmail.com', '0903151288', N'Bác sĩ chuyên khoa Nội tiết', N'Bệnh viện Bạch Mai', N'Hà Nội, Việt Nam', N'Bác sĩ chuyên khoa Nội tiết, có kinh nghiệm điều trị các bệnh về hormone tăng trưởng.', 'Active'),
((SELECT UserID FROM UserAccounts WHERE Username = 'TuHT1990'), N'Huỳnh Thái Tú', 'tuht@gmail.com', '0842139577', N'Bác sĩ Dinh dưỡng', N'Viện Dinh dưỡng Quốc gia', N'Hà Nội, Việt Nam', N'Chuyên gia về dinh dưỡng và chế độ ăn uống, giúp trẻ phát triển toàn diện.', 'Active'),
((SELECT UserID FROM UserAccounts WHERE Username = 'ThaoNTT1987'), N'Nguyễn Thị Thanh Thảo', 'thaontt@gmail.com', '0914751925', N'Bác sĩ chuyên khoa Tim mạch', N'Bệnh viện Chợ Rẫy', N'TP.HCM, Việt Nam', N'Chuyên điều trị các bệnh về tim mạch ở trẻ em.', 'Active');

GO

-- Insert Doctor Specializations (Một bác sĩ có thể có nhiều chuyên ngành)
INSERT INTO DoctorSpecializations (DoctorID, SpecializationID, Status)
VALUES 
-- Huỳnh Nguyễn Ngọc Nguyên - Khoa Nhi, Nội tiết
((SELECT DoctorID FROM Doctors WHERE Name = N'Huỳnh Nguyễn Ngọc Nguyên'), 
 (SELECT SpecializationID FROM Specializations WHERE SpecializationName = 'Pediatrics'), 'Active'),
((SELECT DoctorID FROM Doctors WHERE Name = N'Huỳnh Nguyễn Ngọc Nguyên'), 
 (SELECT SpecializationID FROM Specializations WHERE SpecializationName = 'Endocrinology'), 'Active'),

-- Trần Mạnh Tuấn - Nội tiết, Thần kinh
((SELECT DoctorID FROM Doctors WHERE Name = N'Trần Mạnh Tuấn'), 
 (SELECT SpecializationID FROM Specializations WHERE SpecializationName = 'Endocrinology'), 'Active'),
((SELECT DoctorID FROM Doctors WHERE Name = N'Trần Mạnh Tuấn'), 
 (SELECT SpecializationID FROM Specializations WHERE SpecializationName = 'Neurology'), 'Active'),

-- Huỳnh Thái Tú - Dinh dưỡng, Khoa Nhi
((SELECT DoctorID FROM Doctors WHERE Name = N'Huỳnh Thái Tú'), 
 (SELECT SpecializationID FROM Specializations WHERE SpecializationName = 'Nutrition'), 'Active'),
((SELECT DoctorID FROM Doctors WHERE Name = N'Huỳnh Thái Tú'), 
 (SELECT SpecializationID FROM Specializations WHERE SpecializationName = 'Pediatrics'), 'Active'),

-- Nguyễn Thị Thanh Thảo - Tim mạch
((SELECT DoctorID FROM Doctors WHERE Name = N'Nguyễn Thị Thanh Thảo'), 
 (SELECT SpecializationID FROM Specializations WHERE SpecializationName = 'Cardiology'), 'Active');

GO

-- Insert các chương trình khuyến mãi
INSERT INTO Promotions (PromotionCode, Description, DiscountPercent, MinPurchaseAmount, MaxDiscountAmount, StartDate, EndDate, Status, CreatedBy, ModifiedBy)
VALUES
('WELCOME10', N'Giảm 10% cho thành viên mới khi đăng ký lần đầu', 10, 0, 50000, '2025-01-01', '2025-12-31', 'Active', 
 (SELECT UserID FROM UserAccounts WHERE Username = 'PhucLHSE'),
 (SELECT UserID FROM UserAccounts WHERE Username = 'PhucLHSE')),

('FAMILY20', N'Giảm 20% khi đăng ký gói thành viên Premium', 20, 1000000, 200000, '2025-03-01', '2025-06-30', 'Active', 
 (SELECT UserID FROM UserAccounts WHERE Username = 'PhucLHSE'),
 (SELECT UserID FROM UserAccounts WHERE Username = 'PhucLHSE')),

('SPRINGSALE15', N'Giảm 15% khi đăng ký gói Standard hoặc Premium', 15, 500000, 150000, '2025-04-01', '2025-05-31', 'Active', 
 (SELECT UserID FROM UserAccounts WHERE Username = 'PhucLHSE'),
 (SELECT UserID FROM UserAccounts WHERE Username = 'PhucLHSE'));

 GO

-- Lấy PromotionID của từng mã khuyến mãi
DECLARE @PromoWelcome10 UNIQUEIDENTIFIER = (SELECT PromotionID FROM Promotions WHERE PromotionCode = 'WELCOME10');
DECLARE @PromoFamily20 UNIQUEIDENTIFIER = (SELECT PromotionID FROM Promotions WHERE PromotionCode = 'FAMILY20');
DECLARE @PromoSpringSale15 UNIQUEIDENTIFIER = (SELECT PromotionID FROM Promotions WHERE PromotionCode = 'SPRINGSALE15');

-- Insert PackagePromotions
INSERT INTO PackagePromotions (PackageID, PromotionID, IsActive, CreatedAt, UpdatedAt)
VALUES
-- Áp dụng WELCOME10 cho tất cả gói
(1, @PromoWelcome10, 1, GETDATE(), GETDATE()),
(2, @PromoWelcome10, 1, GETDATE(), GETDATE()),
(3, @PromoWelcome10, 1, GETDATE(), GETDATE()),

-- Áp dụng FAMILY20 cho gói Premium
(3, @PromoFamily20, 1, GETDATE(), GETDATE()),

-- Áp dụng SPRINGSALE15 cho Standard và Premium
(2, @PromoSpringSale15, 1, GETDATE(), GETDATE()),
(3, @PromoSpringSale15, 1, GETDATE(), GETDATE());

-- Insert danh sách thành viên Free
INSERT INTO Members (UserID, EmergencyContact, Status, JoinDate, Notes)
VALUES
((SELECT UserID FROM UserAccounts WHERE Username = 'NguyenVA1992'), N'Nguyễn Thị Hoa - 0903123456', 'Active', GETDATE(), N'Theo dõi con trai đầu lòng'),
((SELECT UserID FROM UserAccounts WHERE Username = 'TranTB1995'), N'Trần Văn Hùng - 0912456789', 'Active', GETDATE(), N'Theo dõi con gái mới sinh'),
((SELECT UserID FROM UserAccounts WHERE Username = 'LeVC1990'), N'Lê Thị Lan - 0938567891', 'Active', GETDATE(), N'Muốn có lịch tiêm chủng cho con'),
((SELECT UserID FROM UserAccounts WHERE Username = 'PhamTD1993'), N'Phạm Văn Hiếu - 0987456123', 'Active', GETDATE(), N'Theo dõi biểu đồ phát triển của con gái'),
((SELECT UserID FROM UserAccounts WHERE Username = 'HoangVT1998'), N'Hoàng Minh - 0978564123', 'Active', GETDATE(), N'Quan tâm đến dinh dưỡng của trẻ'),
((SELECT UserID FROM UserAccounts WHERE Username = 'DangTT1994'), N'Đặng Văn Thành - 0903123456', 'Active', GETDATE(), N'Muốn theo dõi sức khỏe tổng thể'),
((SELECT UserID FROM UserAccounts WHERE Username = 'VoVD1996'), N'Võ Thị Mai - 0912567890', 'Active', GETDATE(), N'Con mình bị nhẹ cân, cần theo dõi'),
((SELECT UserID FROM UserAccounts WHERE Username = 'BuiTH1997'), N'Bùi Văn Tiến - 0987543211', 'Active', GETDATE(), N'Muốn biết thông tin về dinh dưỡng cho bé'),
((SELECT UserID FROM UserAccounts WHERE Username = 'NgoVS1991'), N'Ngô Thị Bình - 0923123411', 'Active', GETDATE(), N'Đăng ký theo dõi con nhỏ'),
((SELECT UserID FROM UserAccounts WHERE Username = 'DinhTN1999'), N'Đinh Văn Hiếu - 0943212311', 'Active', GETDATE(), N'Con bị dị ứng thực phẩm, cần tư vấn'),
((SELECT UserID FROM UserAccounts WHERE Username = 'PhanVK1993'), N'Phan Thị Hoa - 0967891234', 'Active', GETDATE(), N'Quan tâm đến phát triển chiều cao cho bé'),
((SELECT UserID FROM UserAccounts WHERE Username = 'NguyenTL1990'), N'Nguyễn Văn Khoa - 0978564123', 'Active', GETDATE(), N'Muốn theo dõi cân nặng của con'),
((SELECT UserID FROM UserAccounts WHERE Username = 'DoVM1994'), N'Đỗ Thị Lan - 0956321789', 'Active', GETDATE(), N'Muốn biết cách tăng đề kháng cho con'),
((SELECT UserID FROM UserAccounts WHERE Username = 'LaTN1992'), N'La Văn Thành - 0945632871', 'Active', GETDATE(), N'Đăng ký theo dõi phát triển toàn diện'),
((SELECT UserID FROM UserAccounts WHERE Username = 'MacVO1995'), N'Mạc Thị Hương - 0921876543', 'Active', GETDATE(), N'Cần theo dõi tiêm chủng cho con');

GO

-- Insert Members có đăng ký gói Standard or Prenium
INSERT INTO Members (UserID, EmergencyContact, Status, JoinDate, Notes)
VALUES
((SELECT UserID FROM UserAccounts WHERE Username = 'LyTP1997'), N'Nguyễn Văn Minh - 0987654321', 'Active', GETDATE(), N'Quan tâm đến dinh dưỡng của bé'),
((SELECT UserID FROM UserAccounts WHERE Username = 'VuVQ1991'), N'Hoàng Thị Mai - 0976543210', 'Active', GETDATE(), N'Muốn theo dõi sức khỏe của con'),
((SELECT UserID FROM UserAccounts WHERE Username = 'CaoTM1996'), N'Phạm Văn Bình - 0965432109', 'Active', GETDATE(), N'Quan tâm đến phát triển thể chất'),
((SELECT UserID FROM UserAccounts WHERE Username = 'TanVS1998'), N'Bùi Thị Hoa - 0954321098', 'Active', GETDATE(), N'Theo dõi lịch tiêm chủng'),
((SELECT UserID FROM UserAccounts WHERE Username = 'TrinhTT1993'), N'Lê Văn Hùng - 0943210987', 'Active', GETDATE(), N'Cần tư vấn về dinh dưỡng');

GO

-- 15 thanh vien Free (Khong nang cap)
INSERT INTO MemberMemberships (MemberID, PackageID, StartDate, EndDate, Status, IsActive, Description)
SELECT MemberID, 1, GETDATE(), DATEADD(MONTH, 1200, GETDATE()), 'Active', 1, N'Gói Free'
FROM Members 
WHERE UserID IN (
    (SELECT UserID FROM UserAccounts WHERE Username = 'NguyenVA1992'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'TranTB1995'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'LeVC1990'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'PhamTD1993'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'HoangVT1998'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'DangTT1994'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'VoVD1996'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'BuiTH1997'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'NgoVS1991'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'DinhTN1999'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'PhanVK1993'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'NguyenTL1990'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'DoVM1994'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'LaTN1992'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'MacVO1995')
);

GO

-- 5 thành viên có gói Free nhưng đã nâng cấp (trạng thái Inactive)
INSERT INTO MemberMemberships (MemberID, PackageID, StartDate, EndDate, Status, IsActive, Description)
SELECT MemberID, 1, GETDATE(), DATEADD(MONTH, 1200, GETDATE()), 'Inactive', 0, N'Gói Free - Không còn sử dụng'
FROM Members 
WHERE UserID IN (
    (SELECT UserID FROM UserAccounts WHERE Username = 'LyTP1997'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'VuVQ1991'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'CaoTM1996'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'TanVS1998'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'TrinhTT1993')
);

GO

-- 5 thanh vien nang cap len Standard hoac Premium
INSERT INTO MemberMemberships (MemberID, PackageID, StartDate, EndDate, Status, IsActive, Description)
SELECT 
    m.MemberID, 
    CASE 
        WHEN u.Username IN (N'LyTP1997', N'VuVQ1991') THEN 2  -- Standard
        WHEN u.Username IN (N'CaoTM1996', N'TanVS1998', N'TrinhTT1993') THEN 3  -- Premium
    END AS PackageID,
    GETDATE(), 
    CASE 
        WHEN u.Username IN (N'LyTP1997', N'VuVQ1991') THEN DATEADD(MONTH, 3, GETDATE())  
        WHEN u.Username IN (N'CaoTM1996', N'TanVS1998', N'TrinhTT1993') THEN DATEADD(MONTH, 12, GETDATE())  
    END AS EndDate,
    'Active', 1, 
    CASE 
        WHEN u.Username IN (N'LyTP1997', N'VuVQ1991') THEN N'Nâng cấp lên Standard'  
        WHEN u.Username IN (N'CaoTM1996', N'TanVS1998', N'TrinhTT1993') THEN N'Nâng cấp lên Premium'  
    END AS Description
FROM UserAccounts u
JOIN Members m ON u.UserID = m.UserID
WHERE u.Username IN (N'LyTP1997', N'VuVQ1991', N'CaoTM1996', N'TanVS1998', N'TrinhTT1993');

GO

-- Insert Transactions Fail
INSERT INTO Transactions (UserID, MemberMembershipID, Amount, Currency, TransactionType, PaymentMethod, TransactionDate, PaymentDate, GatewayTransactionID, PaymentStatus, Description)
SELECT 
    u.UserID, 
    mm.MemberMembershipID, 
    379000.00,  -- Gói Standard
    'VND', 'Subscription', 'VnPay',
    GETDATE(),  -- Thời điểm giao dịch
    NULL,  -- Giao dịch thất bại không có PaymentDate
    300002,  -- Mã giao dịch thất bại
    'Failed',  
    N'Giao dịch thất bại'
FROM UserAccounts u
JOIN Members m ON u.UserID = m.UserID
JOIN MemberMemberships mm ON m.MemberID = mm.MemberID
WHERE u.Username = N'VuVQ1991' AND mm.PackageID = 2 AND mm.IsActive = 1; -- Chỉ lấy gói bị lỗi

GO

-- Insert Transactions Success
INSERT INTO Transactions (UserID, MemberMembershipID, Amount, Currency, TransactionType, PaymentMethod, TransactionDate, PaymentDate, GatewayTransactionID, PaymentStatus, Description)
SELECT DISTINCT 
    u.UserID, 
    mm.MemberMembershipID, 
    CASE 
        WHEN u.Username IN (N'LyTP1997', N'VuVQ1991') THEN 379000.00  
        WHEN u.Username IN (N'CaoTM1996', N'TanVS1998', N'TrinhTT1993') THEN 1279000.00  
    END AS Amount,
    'VND', 'Subscription', 'VnPay',
    GETDATE(),
    CASE 
        WHEN u.Username = N'VuVQ1991' AND mm.PackageID = 2 AND mm.IsActive = 1 THEN NULL  
        ELSE GETDATE()  
    END AS PaymentDate,
    CASE 
        WHEN u.Username = N'VuVQ1991' AND mm.PackageID = 2 AND mm.IsActive = 1 THEN 300002  
        WHEN u.Username = N'VuVQ1991' AND mm.PackageID = 2 AND mm.IsActive = 0 THEN 300003  
        WHEN u.Username = N'LyTP1997' THEN 300001  
        WHEN u.Username = N'CaoTM1996' THEN 300004  
        WHEN u.Username = N'TanVS1998' THEN 300005  
        WHEN u.Username = N'TrinhTT1993' THEN 300006  
    END AS GatewayTransactionID,
    CASE 
        WHEN u.Username = N'VuVQ1991' AND mm.PackageID = 2 AND mm.IsActive = 1 THEN 'Failed'  
        ELSE 'Success'  
    END AS PaymentStatus,
    CASE 
        WHEN u.Username = N'LyTP1997' THEN N'Thanh toán gói Standard 3 tháng'  
        WHEN u.Username = N'VuVQ1991' AND mm.PackageID = 2 AND mm.IsActive = 1 THEN N'Giao dịch thất bại'  
        WHEN u.Username = N'VuVQ1991' AND mm.PackageID = 2 AND mm.IsActive = 0 THEN N'Thanh toán lại sau thất bại'  
        WHEN u.Username = N'CaoTM1996' THEN N'Thanh toán gói Premium 12 tháng'  
        WHEN u.Username = N'TanVS1998' THEN N'Thanh toán gói Premium 12 tháng'  
        WHEN u.Username = N'TrinhTT1993' THEN N'Thanh toán gói Premium 12 tháng'  
    END AS Description
FROM UserAccounts u
JOIN Members m ON u.UserID = m.UserID
JOIN MemberMemberships mm ON m.MemberID = mm.MemberID
WHERE 
    u.Username IN (N'LyTP1997', N'VuVQ1991', N'CaoTM1996', N'TanVS1998', N'TrinhTT1993')
    AND (
        mm.IsActive = 1  -- Chỉ lấy gói đang hoạt động
        OR (u.Username = N'VuVQ1991' AND mm.PackageID = 2) -- Chắc chắn lấy giao dịch thất bại
    );

 GO

 -- Insert Blog Categories (Danh mục cha)
INSERT INTO BlogCategories(CategoryName, Description, IsActive)
VALUES
    ('Getting Pregnant', N'Thông tin và tài liệu dành cho người chuẩn bị mang thai.', 1),
    ('Baby', N'Thông tin dành cho bố mẹ có con sơ sinh và trẻ nhỏ.', 1),
    ('Toddler', N'Nội dung liên quan đến trẻ mới biết đi và sự phát triển ban đầu.', 1),
    ('Child', N'Nội dung cho cha mẹ có con trong độ tuổi mầm non và tiểu học.', 1),
    ('Teenager', N'Thông tin và lời khuyên dành cho cha mẹ có con tuổi teen.', 1);

-- Insert Blog Subcategories (Danh mục con) dựa vào danh mục cha
DECLARE @GettingPregnant INT = (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Getting Pregnant');
DECLARE @Baby INT = (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Baby');
DECLARE @Toddler INT = (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Toddler');
DECLARE @Child INT = (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Child');
DECLARE @Teenager INT = (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Teenager');

INSERT INTO BlogCategories (CategoryName, Description, ParentCategoryID, IsActive)
VALUES
    -- Getting Pregnant (6 subcategories)
    ('Ovulation', N'Kiến thức về rụng trứng', @GettingPregnant, 1),
    ('Fertility', N'Thông tin về khả năng sinh sản', @GettingPregnant, 1),
    ('Pregnancy Tests', N'Các phương pháp thử thai', @GettingPregnant, 1),
    ('Nutrition Before Pregnancy', N'Dinh dưỡng trước khi mang thai', @GettingPregnant, 1),
    ('Exercise & Fitness', N'Tập thể dục và sức khỏe sinh sản', @GettingPregnant, 1),
    ('Common Pregnancy Myths', N'Những quan niệm sai lầm về mang thai', @GettingPregnant, 1),

    -- Baby (7 subcategories)
    ('Breastfeeding', N'Hướng dẫn và lợi ích của việc cho con bú', @Baby, 1),
    ('Sleep Tips', N'Mẹo giúp bé ngủ ngon', @Baby, 1),
    ('Newborn Care', N'Chăm sóc trẻ sơ sinh', @Baby, 1),
    ('Baby Milestones', N'Sự phát triển quan trọng của bé', @Baby, 1),
    ('Vaccination', N'Lịch tiêm phòng cho bé', @Baby, 1),
    ('Babyproofing', N'Làm thế nào để giữ an toàn cho bé', @Baby, 1),
    ('Teething', N'Mọc răng và cách giảm đau cho bé', @Baby, 1),

    -- Toddler (6 subcategories)
    ('Potty Training', N'Cách hướng dẫn trẻ đi vệ sinh', @Toddler, 1),
    ('Nutrition', N'Dinh dưỡng cần thiết cho trẻ', @Toddler, 1),
    ('Preschool', N'Chuẩn bị cho trẻ đi mẫu giáo', @Toddler, 1),
    ('Speech Development', N'Phát triển ngôn ngữ ở trẻ', @Toddler, 1),
    ('Discipline', N'Rèn luyện thói quen cho trẻ', @Toddler, 1),
    ('Sleep Routine', N'Xây dựng thói quen ngủ cho trẻ', @Toddler, 1),

    -- Child (5 subcategories)
    ('Education', N'Phát triển kỹ năng học tập', @Child, 1),
    ('Health Tips', N'Mẹo giữ sức khỏe cho trẻ', @Child, 1),
    ('Outdoor Activities', N'Các hoạt động ngoài trời', @Child, 1),
    ('Screen Time', N'Kiểm soát thời gian sử dụng thiết bị điện tử', @Child, 1),
    ('Emotional Development', N'Phát triển cảm xúc ở trẻ nhỏ', @Child, 1),

    -- Teenager (6 subcategories)
    ('Teen Mental Health', N'Sức khỏe tinh thần cho tuổi teen', @Teenager, 1),
    ('Social Media', N'Ảnh hưởng của mạng xã hội với tuổi teen', @Teenager, 1),
    ('Teen Education', N'Giáo dục dành cho tuổi teen', @Teenager, 1),
    ('Peer Pressure', N'Áp lực từ bạn bè đồng trang lứa', @Teenager, 1),
    ('Career Guidance', N'Định hướng nghề nghiệp cho tuổi teen', @Teenager, 1),
    ('Teen Relationships', N'Các mối quan hệ của tuổi teen', @Teenager, 1);

GO

-- Lấy UserID của Admin Phúc
DECLARE @AdminID UNIQUEIDENTIFIER = (SELECT UserID FROM UserAccounts WHERE Username = 'PhucLHSE');

-- Lấy UserID của các bác sĩ
DECLARE @DoctorNguyenID UNIQUEIDENTIFIER = (SELECT UserID FROM UserAccounts WHERE Username = 'NguyenHNN'); -- Huỳnh Nguyễn Ngọc Nguyên (Nhi khoa)
DECLARE @DoctorTuanID UNIQUEIDENTIFIER = (SELECT UserID FROM UserAccounts WHERE Username = 'TuanTM1980'); -- Trần Mạnh Tuấn (Nội tiết)
DECLARE @DoctorTuID UNIQUEIDENTIFIER = (SELECT UserID FROM UserAccounts WHERE Username = 'TuHT1990'); -- Huỳnh Thái Tú (Dinh dưỡng)
DECLARE @DoctorThaoID UNIQUEIDENTIFIER = (SELECT UserID FROM UserAccounts WHERE Username = 'ThaoNTT1987'); -- Nguyễn Thị Thanh Thảo (Tim mạch)

-- Insert Blogs (Bài viết mẫu)
INSERT INTO Blogs (Title, Content, AuthorID, CategoryID, ImageBlog, Status, Tags, ReferenceSources, CreatedAt, UpdatedAt)
VALUES
    -- Bài viết của Admin
    (N'Ovulation signs and how to track them', 
    N'Ovulation plays an important role in conception. This article helps you identify signs such as cervical mucus, basal body temperature, and ovulation test kits.', 
    @AdminID, (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Ovulation'),
    'https://plus.unsplash.com/premium_photo-1676049342411-c118fe1570b2?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 'Approved', 
    N'ovulation, ovulation tracking, fertility',
    'https://www.mayoclinic.org, https://www.webmd.com',
    GETDATE(), GETDATE()),

    (N'Are home pregnancy tests accurate?', 
    N'A home pregnancy test is a quick method for detecting pregnancy. This article explains how it works, its accuracy, and the best time to take the test.', 
    @AdminID, (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Pregnancy Tests'),
    'https://images.unsplash.com/photo-1533483595632-c5f0e57a1936?q=80&w=2080&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 'Approved', 
    N'pregnancy test, pregnancy detection, home pregnancy test',
    'https://www.healthline.com, https://www.nhs.uk',
    GETDATE(), GETDATE()),

    -- Bài viết của Doctor Huỳnh Nguyễn Ngọc Nguyên (Pediatric Specialist)
    (N'Tips for helping your baby sleep well', 
    N'Sleep is crucial for the development of newborns. Learn methods that can help your baby sleep deeper and avoid waking up abruptly.', 
    @DoctorNguyenID, (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Sleep Tips'),
    'https://images.unsplash.com/photo-1470116945706-e6bf5d5a53ca?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 'Approved', 
    N'baby sleep, sleep tips, deep sleep',
    'https://www.sleepfoundation.org, https://www.aap.org',
    GETDATE(), GETDATE()),

    -- Bài viết của Doctor Huỳnh Thái Tú (Nutrition Specialist)
    (N'Nutrition importance for young children', 
    N'Young children need to be provided with sufficient nutrition for full development. This article lists essential food groups.', 
    @DoctorTuID, (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Nutrition'),
    'https://plus.unsplash.com/premium_photo-1676049342406-20d6e89dc79c?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 'Approved', 
    N'child nutrition, toddler meals, baby food',
    'https://www.nhs.uk, https://www.aap.org',
    GETDATE(), GETDATE()),

    -- Bài viết của Doctor Nguyễn Thị Thanh Thảo (Cardiology & Teen Mental Health Specialist)
    (N'What should parents do when their child enters adolescence?', 
    N'Adolescence is a period when children undergo physical and mental changes. Parents need to understand how to support and accompany their children during this time.', 
    @DoctorThaoID, (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Teen Mental Health'),
    'https://images.unsplash.com/photo-1515488042361-ee00e0ddd4e4?q=80&w=2075&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 'Approved', 
    N'teen mental health, parenting teenagers, mental support',
    'https://www.psychologytoday.com, https://www.nimh.nih.gov',
    GETDATE(), GETDATE());

GO

-- Insert Diseases
INSERT INTO Diseases (DiseaseName, LowerBoundMale, UpperBoundMale, LowerBoundFemale, UpperBoundFemale, 
                      MinAge, MaxAge, Severity, DiseaseType, Symptoms, Treatment, Prevention, 
                      Description, Notes, IsActive)
VALUES
     -- 1. Suy dinh dưỡng nặng (Severe Malnutrition)
    ('Severe Malnutrition', 10.0, 16.0, 9.0, 16.0, 1, 10, 'High', 'Nutritional Deficiency',
     N'Severe weight loss, muscle atrophy, slow development, fatigue',
     N'High-calorie diet, protein-rich foods, medical intervention',
     N'Ensure adequate nutrition, monitor growth',
     N'Severe malnutrition due to prolonged nutritional deficiency.',
     N'Medical intervention is required immediately to prevent dangerous complications.', 1),

    -- 2. Suy dinh dưỡng nhẹ (Mild Malnutrition)
    ('Mild Malnutrition', 16.0, 18.5, 16.0, 18.5, 1, 18, 'Medium', 'Nutritional Deficiency',
     N'Slight underweight, frequent illness, low energy',
     N'Balanced diet, nutritional supplements if needed',
     N'Encourage healthy eating, increase protein intake',
     N'Mild malnutrition, deficiency in some essential micronutrients.',
     N'Common in children with an unbalanced diet.', 1),

    -- 3. Anemia (Thiếu máu)
    ('Anemia', 10.0, 18.5, 9.5, 18.0, 3, 16, 'Medium', 'Blood Disorder',
     'Paleness, fatigue, shortness of breath, dizziness',
     'Iron supplements, iron-rich diet (red meat, leafy greens)',
     'Maintain a diet rich in iron and vitamin C, regular health check-ups',
     'A condition where there is a deficiency of red blood cells or hemoglobin in the blood.',
     'Common in children due to poor diet and rapid growth phases.', 1),

    -- 4. Diabetes Type 1 (Tiểu đường tuýp 1)
    ('Diabetes Type 1', 15.0, 25.0, 14.0, 24.0, 6, 18, 'High', 'Endocrine Disorder',
     'Frequent urination, excessive thirst, weight loss, fatigue',
     'Insulin therapy, carbohydrate management, regular monitoring',
     'Healthy diet, physical activity, routine glucose level checks',
     'A chronic disease where the pancreas produces little or no insulin.',
     'Requires lifelong insulin therapy to regulate blood sugar levels.', 1),

    -- 5. Stunted Growth (Chậm phát triển chiều cao)
    ('Stunted Growth', 12.0, 18.0, 11.0, 17.0, 1, 10, 'Medium', 'Growth Disorder',
     'Short stature, delayed physical development, cognitive impairment',
     'Nutritional supplements, balanced diet, medical monitoring',
     'Ensure adequate food intake, early intervention for nutrient deficiencies',
     'A condition where a child’s growth rate is significantly lower than expected.',
     'Can be caused by poor nutrition, infections, or genetic conditions.', 1),

    -- 6. Asthma (Hen suyễn)
    ('Asthma', 18.0, 28.0, 17.0, 27.0, 4, 18, 'Medium', 'Respiratory Disorder',
     'Shortness of breath, wheezing, coughing, chest tightness',
     'Inhalers, medication, avoiding triggers (allergens, smoke)',
     'Avoid smoking exposure, control allergens at home',
     'A chronic respiratory condition where the airways become inflamed and narrow.',
     'Can be triggered by allergens, pollution, or respiratory infections.', 1),

    -- 7. Rickets (Còi xương)
    ('Rickets', 10.0, 17.0, 9.5, 16.5, 1, 10, 'Medium', 'Bone Disorder',
     'Delayed growth, weak bones, skeletal deformities',
     'Vitamin D and calcium supplements, sunlight exposure',
     'Ensure a diet rich in vitamin D, allow outdoor playtime',
     'A disorder caused by vitamin D, calcium, or phosphate deficiency leading to soft bones.',
     'Common in children with limited sun exposure or poor diet.', 1),

    -- 8. Thừa cân (Overweight)
    ('Overweight', 25.0, 30.0, 24.0, 29.9, 5, 18, 'Medium', 'Nutritional Disorder',
     'Rapid weight gain, easily fatigued, shortness of breath, difficulty moving',
     'Calorie-controlled diet, increased physical activity',
     'Develop healthy eating habits, maintain regular physical exercise',
     'A condition where excess fat accumulates in the body but has not yet reached obesity.',
     'May lead to obesity if not controlled early.', 1),

    -- 9. Béo phì (Obesity)
    ('Obesity', 30.0, 40.0, 30.0, 39.9, 5, 18, 'High', 'Nutritional Disorder',
     'Excessive fat accumulation, difficulty breathing, joint pain, risk of diabetes',
     'Weight-loss diet, regular exercise, medical monitoring',
     'Change eating habits, limit sugar and fat intake',
     'Obesity is a condition where excessive fat is stored in the body, affecting health.',
     'A risk factor for cardiovascular diseases and diabetes.', 1),

    -- 10. Hypertension (Cao huyết áp ở trẻ)
    ('Hypertension', 20.0, 40.0, 19.0, 39.0, 10, 18, 'High', 'Cardiovascular Disorder',
     'High blood pressure, headaches, dizziness, blurred vision',
     'Lifestyle changes, low-salt diet, medical treatment if severe',
     'Encourage exercise, avoid high-sodium and processed foods',
     'A condition where a child has abnormally high blood pressure, increasing cardiovascular risks.',
     'Can be associated with obesity, genetic factors, or poor diet.', 1),

    -- 11. Failure to Thrive (Không phát triển đúng chuẩn)
    ('Failure to Thrive', 10.0, 18.0, 9.5, 17.5, 1, 5, 'High', 'Developmental Disorder',
     'Slow weight gain, delayed milestones, lack of energy',
     'Nutritional therapy, parental guidance, medical monitoring',
     'Early detection, frequent weight monitoring, improved diet',
     'A condition where a child’s growth is significantly below the expected rate.',
     'Requires medical evaluation to determine underlying causes.', 1);

GO

-- Insert Children
INSERT INTO Children (MemberID, Name, DateOfBirth, Gender, BirthWeight, BirthHeight, BloodType, Allergies, Notes, RelationshipToMember, Status, CreatedAt, UpdatedAt)
SELECT MemberID, N'Nguyễn Minh Khang', '2008-06-15', 'Male', 3.2, 50, 'O+', NULL, N'Đang học cấp 3, yêu thích bóng đá', N'Con trai', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'NguyenVA1992')
UNION ALL
SELECT MemberID, N'Trần Gia Hân', '2012-08-20', 'Female', 3.0, 48, 'A+', N'Dị ứng sữa', N'Học sinh tiểu học, thích vẽ tranh', N'Con gái', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'TranTB1995')
UNION ALL
SELECT MemberID, N'Lê Hoàng Nam', '2018-12-10', 'Male', 3.1, 49, 'B-', NULL, N'Bé hiếu động, thích khám phá', N'Con trai', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'LeVC1990')
UNION ALL
SELECT MemberID, N'Phạm Khánh Linh', '2023-03-25', 'Female', 3.4, 51, 'AB+', N'Dị ứng phấn hoa', N'Sơ sinh, cần theo dõi đặc biệt', N'Con gái', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'PhamTD1993')
UNION ALL
SELECT MemberID, N'Hoàng Bảo Long', '2016-11-07', 'Male', 3.6, 52, 'O-', NULL, N'Bé thích vận động, học lớp 1', N'Con trai', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'HoangVT1998')
UNION ALL
SELECT MemberID, N'Đặng Gia Bảo', '2010-02-14', 'Male', 3.2, 49, 'A-', NULL, N'Chơi bóng rổ giỏi, thích khoa học', N'Con trai', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'DangTT1994')
UNION ALL
SELECT MemberID, N'Võ Thảo My', '2015-09-10', 'Female', 3.5, 50, 'B+', N'Dị ứng mèo', N'Yêu thích đọc sách, học giỏi', N'Con gái', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'VoVD1996')
UNION ALL
SELECT MemberID, N'Bùi Phúc An', '2022-01-21', 'Male', 3.1, 48, 'O+', NULL, N'Bé hay cười, thích chơi với mẹ', N'Con trai', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'BuiTH1997')
UNION ALL
SELECT MemberID, N'Mạc Thị Minh Anh', '2008-04-05', 'Female', 3.3, 49, 'A+', NULL, N'Yêu thích ca hát, tham gia đội văn nghệ', N'Con gái', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'MacVO1995')
UNION ALL
SELECT MemberID, N'Ngô Hải Đăng', '2017-07-29', 'Male', 3.4, 50, 'B-', NULL, N'Bé thông minh, thích lắp ráp đồ chơi', N'Con trai', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'NgoVS1991')
UNION ALL
SELECT MemberID, N'Đinh Thị Ngọc Hân', '2011-10-18', 'Female', 3.0, 48, 'O-', N'Dị ứng hải sản', N'Học sinh giỏi, yêu thích tiếng Anh', N'Con gái', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'DinhTN1999')
UNION ALL
SELECT MemberID, N'Phan Nguyễn Gia Huy', '2009-05-23', 'Male', 3.3, 51, 'A+', NULL, N'Đang học cấp 2, thích chơi thể thao', N'Con trai', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'PhanVK1993')
UNION ALL
SELECT MemberID, N'Nguyễn Thị Bảo Trân', '2014-12-01', 'Female', 3.2, 50, 'AB-', NULL, N'Yêu thích hội họa, vẽ rất đẹp', N'Con gái', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'NguyenTL1990')
UNION ALL
SELECT MemberID, N'Đỗ Hoàng Nam Phong', '2007-06-22', 'Male', 3.5, 52, 'B+', NULL, N'Chơi cờ vua rất giỏi, đã đạt giải', N'Con trai', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'DoVM1994')
UNION ALL
SELECT MemberID, N'La Thị Hải Yến', '2013-09-17', 'Female', 3.1, 49, 'A-', NULL, N'Yêu thích thiên nhiên, thích trồng cây', N'Con gái', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'LaTN1992');

GO

-- Thành viên StandardUser1 có 2 con
INSERT INTO Children (MemberID, Name, DateOfBirth, Gender, BirthWeight, BirthHeight, BloodType, Allergies, Notes, RelationshipToMember, Status, CreatedAt, UpdatedAt)
SELECT MemberID, N'Nguyễn Đặng Tuấn Kiệt', '2019-05-10', 'Male', 3.5, 51, 'B+', NULL, N'Bé rất hiếu động', N'Con trai', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'LyTP1997')
UNION ALL
SELECT MemberID, N'Nguyễn Đặng Ngọc Anh', '2022-01-25', 'Female', 3.1, 49, 'O-', NULL, N'Hay khóc đêm', N'Con gái', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'LyTP1997');

GO

-- Thành viên StandardUser2 có 2 con
INSERT INTO Children (MemberID, Name, DateOfBirth, Gender, BirthWeight, BirthHeight, BloodType, Allergies, Notes, RelationshipToMember, Status, CreatedAt, UpdatedAt)
SELECT MemberID, N'Vũ Bùi Thanh Tú', '2020-07-18', 'Male', 3.3, 50, 'A-', N'Dị ứng hải sản', N'Cần theo dõi chế độ ăn', N'Con trai', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'VuVQ1991')
UNION ALL
SELECT MemberID, N'Vũ Huyền Hà My', '2021-09-10', 'Female', 3.2, 49, 'AB-', NULL, N'Rất ngoan', N'Con gái', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'VuVQ1991');

GO

-- Thành viên PremiumUser1 có 2 con
INSERT INTO Children (MemberID, Name, DateOfBirth, Gender, BirthWeight, BirthHeight, BloodType, Allergies, Notes, RelationshipToMember, Status, CreatedAt, UpdatedAt)
SELECT MemberID, N'Phạm Quang Huy', '2015-11-12', 'Male', 3.4, 50, 'O+', NULL, N'Rất thích thể thao', N'Con trai', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'CaoTM1996')
UNION ALL
SELECT MemberID, N'Phạm Mai Chi', '2023-02-15', 'Female', 3.0, 48, 'A+', N'Dị ứng trứng', N'Cẩn thận với thực phẩm chứa trứng', N'Con gái', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'CaoTM1996');

GO

-- Thành viên PremiumUser2 có 2 con
INSERT INTO Children (MemberID, Name, DateOfBirth, Gender, BirthWeight, BirthHeight, BloodType, Allergies, Notes, RelationshipToMember, Status, CreatedAt, UpdatedAt)
SELECT MemberID, N'Tân Lê Tuấn Phong', '2014-06-20', 'Male', 3.8, 52, 'B+', NULL, N'Bé thích lập trình', N'Con trai', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'TanVS1998')
UNION ALL
SELECT MemberID, N'Tân Minh Ngọc', '2019-10-05', 'Female', 3.2, 50, 'AB+', NULL, N'Nhút nhát, cần động viên', N'Con gái', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'TanVS1998');

GO

-- Thành viên PremiumUser3 có 3 con
INSERT INTO Children (MemberID, Name, DateOfBirth, Gender, BirthWeight, BirthHeight, BloodType, Allergies, Notes, RelationshipToMember, Status, CreatedAt, UpdatedAt)
SELECT MemberID, N'Lê Đức Anh', '2010-08-30', 'Male', 3.6, 52, 'O-', NULL, N'Thích bóng đá', N'Con trai', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'TrinhTT1993')
UNION ALL
SELECT MemberID, N'Lê Thị Bảo Ngọc', '2016-04-25', 'Female', 3.5, 50, 'A-', NULL, N'Học giỏi và chăm chỉ', N'Con gái', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'TrinhTT1993')
UNION ALL
SELECT MemberID, N'Lê Trịnh Minh Khoa', '2022-12-10', 'Male', 3.1, 48, 'B+', N'Dị ứng đậu phộng', N'Cần tránh các sản phẩm từ đậu phộng', N'Con trai', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'TrinhTT1993');

GO

--Insert GrowthRecords Nguyễn Minh Khang child
INSERT INTO GrowthRecords (
    ChildID, RecordedBy, Weight, Height, HeadCircumference, MuscleMass, ChestCircumference, 
    NutritionalStatus, FerritinLevel, Triglycerides, BloodSugarLevel, PhysicalActivityLevel, 
    HeartRate, BloodPressure, BodyTemperature, OxygenSaturation, SleepDuration, Vision, 
    Hearing, ImmunizationStatus, MentalHealthStatus, GrowthHormoneLevel, AttentionSpan, 
    NeurologicalReflexes, DevelopmentalMilestones, Notes, Status, Verified, CreatedAt, UpdatedAt
) 
SELECT 
    c.ChildID, 
    m.MemberID, 
    52.0, 168.5, 56.0, 19.0, 85.0, N'Bình thường', 75.0, 1.1, 95.0, N'Cao', 
    72, 120.0, 36.6, 99.2, 7.5, N'Tốt', N'Bình thường', N'Đã tiêm đủ các mũi', N'Ổn định', 
    20.0, N'Dài', N'Bình thường', N'Hoạt động thể thao đều đặn', N'Không có vấn đề sức khỏe', 
    'Active', 1, '2024-01-05', '2024-01-05'
FROM Children c
JOIN Members m ON c.MemberID = m.MemberID
WHERE c.Name = N'Nguyễn Minh Khang'

UNION ALL

SELECT 
    c.ChildID, 
    m.MemberID, 
    53.5, 169.0, 56.2, 19.3, 86.5, N'Bình thường', 76.5, 1.2, 94.8, N'Cao', 
    70, 118.5, 36.5, 99.0, 7.3, N'Tốt', N'Bình thường', N'Đã tiêm đủ các mũi', N'Ổn định', 
    20.5, N'Dài', N'Bình thường', N'Thể lực tốt, ít ốm vặt', N'Không có vấn đề sức khỏe', 
    'Active', 1, '2024-01-25', '2024-01-25'
FROM Children c
JOIN Members m ON c.MemberID = m.MemberID
WHERE c.Name = N'Nguyễn Minh Khang'

UNION ALL

SELECT 
    c.ChildID, 
    m.MemberID, 
    54.2, 169.8, 56.5, 19.7, 87.0, N'Tốt', 78.0, 1.3, 94.5, N'Cao', 
    68, 117.0, 36.4, 98.9, 7.2, N'Tốt', N'Bình thường', N'Đã tiêm đủ các mũi', N'Ổn định', 
    21.0, N'Dài', N'Bình thường', N'Bắt đầu tập gym nhẹ', N'Không có vấn đề sức khỏe', 
    'Active', 1, '2024-02-15', '2024-02-15'
FROM Children c
JOIN Members m ON c.MemberID = m.MemberID
WHERE c.Name = N'Nguyễn Minh Khang'

UNION ALL

SELECT 
    c.ChildID, 
    m.MemberID, 
    55.0, 170.5, 57.0, 20.0, 88.0, N'Tốt', 80.0, 1.4, 94.2, N'Cao', 
    67, 116.5, 36.4, 98.8, 7.0, N'Tốt', N'Bình thường', N'Đã tiêm đủ các mũi', N'Ổn định', 
    21.5, N'Tốt', N'Bình thường', N'Thể trạng ổn định, ăn uống khoa học', N'Không có vấn đề sức khỏe', 
    'Active', 1, '2024-03-05', '2024-03-05'
FROM Children c
JOIN Members m ON c.MemberID = m.MemberID
WHERE c.Name = N'Nguyễn Minh Khang'

UNION ALL

SELECT 
    c.ChildID, 
    m.MemberID, 
    56.0, 171.0, 57.5, 20.5, 89.0, N'Tốt', 82.0, 1.5, 94.0, N'Cao', 
    65, 115.0, 36.3, 98.7, 6.8, N'Tốt', N'Bình thường', N'Đã tiêm đủ các mũi', N'Ổn định', 
    22.0, N'Tốt', N'Bình thường', N'Thể lực tối ưu, không gặp vấn đề sức khỏe', N'Không có vấn đề sức khỏe', 
    'Active', 1, '2024-03-25', '2024-03-25'
FROM Children c
JOIN Members m ON c.MemberID = m.MemberID
WHERE c.Name = N'Nguyễn Minh Khang';

GO

-- Insert GrowthRecords Trần Gia Hân child
INSERT INTO GrowthRecords (
    ChildID, RecordedBy, Weight, Height, HeadCircumference, MuscleMass, ChestCircumference, 
    NutritionalStatus, FerritinLevel, Triglycerides, BloodSugarLevel, PhysicalActivityLevel, 
    HeartRate, BloodPressure, BodyTemperature, OxygenSaturation, SleepDuration, Vision, 
    Hearing, ImmunizationStatus, MentalHealthStatus, GrowthHormoneLevel, AttentionSpan, 
    NeurologicalReflexes, DevelopmentalMilestones, Notes, Status, Verified, CreatedAt, UpdatedAt
) 
SELECT 
    c.ChildID, 
    m.MemberID, 
    27.0, 120.0, 50.0, 10.0, 55.0, N'Bình thường', 65.0, 1.0, 90.0, N'Bình thường',
    80, 110.0, 36.5, 98.0, 9.0, N'Bình thường', N'Bình thường', N'Đã tiêm đầy đủ', N'Ổn định',
    18.0, N'Tốt', N'Bình thường', N'Thể chất phát triển bình thường', N'Không có vấn đề sức khỏe',
    'Active', 1, '2025-01-05', '2025-01-05'
FROM Children c
JOIN Members m ON c.MemberID = m.MemberID
WHERE c.Name = N'Trần Gia Hân'

UNION ALL

SELECT 
    c.ChildID, 
    m.MemberID, 
    27.5, 121.0, 50.5, 10.2, 56.0, N'Bình thường', 66.5, 1.1, 92.0, N'Bình thường',
    82, 112.0, 36.6, 98.5, 9.2, N'Bình thường', N'Bình thường', N'Đã tiêm đầy đủ', N'Ổn định',
    18.5, N'Tốt', N'Bình thường', N'Hoạt động thể thao thường xuyên', N'Không có vấn đề sức khỏe',
    'Active', 1, '2025-02-10', '2025-02-10'
FROM Children c
JOIN Members m ON c.MemberID = m.MemberID
WHERE c.Name = N'Trần Gia Hân'

UNION ALL

SELECT 
    c.ChildID, 
    m.MemberID, 
    26.0, 122.0, 51.0, 10.4, 57.0, N'Bình thường', 67.0, 1.2, 93.0, N'Bình thường',
    85, 115.0, 36.7, 99.0, 9.5, N'Bình thường', N'Bình thường', N'Đã tiêm đầy đủ', N'Ổn định',
    19.0, N'Tốt', N'Bình thường', N'Thể chất phát triển ổn định', N'Không có vấn đề sức khỏe',
    'Active', 1, '2025-03-15', '2025-03-15'
FROM Children c
JOIN Members m ON c.MemberID = m.MemberID
WHERE c.Name = N'Trần Gia Hân';

GO

-- Insert Alerts
INSERT INTO Alerts (GrowthRecordID, DiseaseID, Message, SeverityLevel) 
SELECT 
    gr.RecordID,
    d.DiseaseID,
    N'Phát hiện dấu hiệu ' + d.DiseaseName + N' ở bé Trần Gia Hân. Cần theo dõi và điều chỉnh chế độ ăn uống.',
    d.Severity
FROM GrowthRecords gr
JOIN Diseases d ON d.DiseaseName = 'Mild Malnutrition'
WHERE gr.ChildID = (SELECT ChildID FROM Children WHERE Name = N'Trần Gia Hân')
  AND gr.Weight / POWER(gr.Height / 100, 2) BETWEEN d.LowerBoundFemale AND d.UpperBoundFemale;

CREATE TABLE BmiPercentiles (
    Age INT NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    L FLOAT NOT NULL,
    M FLOAT NOT NULL,
    S FLOAT NOT NULL,
    P01 FLOAT NOT NULL,
    P50 FLOAT NOT NULL,
    P75 FLOAT NOT NULL,
    P99 FLOAT NOT NULL,
    -- Khóa chính là bộ đôi (Age, Gender)
    CONSTRAINT PK_BmiPercentiles PRIMARY KEY (Age, Gender),
    -- Ràng buộc để đảm bảo Gender chỉ nhận giá trị 'Male' hoặc 'Female'
    CONSTRAINT CHK_Gender CHECK (Gender IN ('Male', 'Female'))
);
GO

-- Chèn dữ liệu vào bảng BmiPercentiles cho Male
INSERT INTO BmiPercentiles (Age, Gender, L, M, S, P01, P50, P75, P99)
VALUES
(0, 'Male', -0.0138, 15.7732, 0.0858, 12.58, 16.23, 17.23, 21.23),
(1, 'Male', -0.4115, 16.7981, 0.0801, 13.3, 16.8, 17.7, 21.8),
(2, 'Male', -0.5846, 15.9743, 0.0784, 12.7, 16, 16.9, 20.7),
(3, 'Male', -0.5166, 15.9036, 0.0781, 12.7, 15.9, 16.8, 20.6),
(4, 'Male', -0.3572, 15.6934, 0.0788, 12.4, 15.7, 16.6, 20.2),
(5, 'Male', -0.2884, 15.514, 0.0799, 12.2, 15.5, 16.4, 20),
(6, 'Male', -0.3452, 15.3485, 0.0821, 12, 15.3, 16.2, 20),
(7, 'Male', -0.5303, 15.24, 0.085, 11.9, 15.2, 16.1, 20.2),
(8, 'Male', -0.7387, 15.2641, 0.0839, 12.04, 15.26, 16.17, 20.36),
(9, 'Male', -0.9697, 15.2965, 0.0865, 12.06, 15.3, 16.24, 20.85),
(10, 'Male', -1.246, 15.4832, 0.0907, 12.18, 15.48, 16.5, 21.86),
(11, 'Male', -1.5342, 15.8094, 0.0965, 12.36, 15.81, 16.93, 23.5),
(12, 'Male', -1.7032, 16.2665, 0.1035, 12.6, 16.27, 17.52, 25.81),
(13, 'Male', -1.7862, 16.9392, 0.1107, 12.97, 16.94, 18.35, 28.74),
(14, 'Male', -1.7719, 17.5877, 0.1156, 13.34, 17.59, 19.13, 30.96),
(15, 'Male', -1.6732, 18.6148, 0.1206, 13.94, 18.62, 20.39, 33.36),
(16, 'Male', -1.5403, 19.5217, 0.1235, 14.46, 19.52, 21.34, 34.7),
(17, 'Male', -1.3529, 20.4951, 0.1258, 14.99, 20.5, 22.43, 35.58);
GO

-- Chèn dữ liệu vào bảng BmiPercentiles cho Female
INSERT INTO BmiPercentiles (Age, Gender, L, M, S, P01, P50, P75, P99)
VALUES
(0, 'Female', -0.1087, 16.3163, 0.0904, 12.25, 16.31, 17.31, 21.58),
(1, 'Female', -0.466, 15.8928, 0.0868, 12.29, 15.89, 16.89, 20.97),
(2, 'Female', -0.5684, 15.5258, 0.0849, 12.19, 15.53, 16.46, 20.62),
(3, 'Female', -0.5684, 15.3438, 0.0885, 11.87, 15.34, 16.29, 20.6),
(4, 'Female', -0.6499, 15.2575, 0.0944, 11.65, 15.26, 16.3, 21.06),
(5, 'Female', -1.058, 15.2467, 0.1009, 11.65, 15.25, 16.42, 22.34),
(6, 'Female', -1.2617, 15.3916, 0.1066, 11.66, 15.39, 16.65, 23.63),
(7, 'Female', -1.4214, 15.6811, 0.1128, 11.81, 15.68, 17.03, 25.28),
(8, 'Female', -1.4852, 16.2097, 0.1198, 12.13, 16.21, 17.67, 27.63),
(9, 'Female', -1.4605, 16.9958, 0.1262, 12.58, 16.99, 18.65, 30.08),
(10, 'Female', -1.4119, 17.8001, 0.1305, 12.94, 17.8, 19.58, 32.35),
(11, 'Female', -1.3335, 18.6006, 0.1342, 13.43, 18.6, 20.55, 34.1),
(12, 'Female', -1.238, 19.382, 0.1372, 13.88, 19.38, 21.49, 35.58),
(13, 'Female', -1.1339, 20.1152, 0.1395, 14.24, 20.12, 22.36, 36.52),
(14, 'Female', -1.0257, 20.7982, 0.1412, 14.5, 20.8, 23.17, 37),
(15, 'Female', -0.9142, 21.1212, 0.1425, 14.58, 21.12, 23.54, 37.08),
(16, 'Female', -0.8553, 21.2603, 0.1433, 14.6, 21.26, 23.52, 37.03),
(17, 'Female', -0.7965, 21.3752, 0.144, 14.59, 21.38, 23.65, 36.94);
GO

