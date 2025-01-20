IF EXISTS (SELECT * FROM sys.databases WHERE name = 'SWP391_ChildGrowthTrackingSystem')
BEGIN
    DROP DATABASE SWP391_ChildGrowthTrackingSystem;
END

CREATE DATABASE SWP391_ChildGrowthTrackingSystem;

USE SWP391_ChildGrowthTrackingSystem;

-- Table Roles
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName VARCHAR(255) NOT NULL,                        -- Tên của vai trò (ví dụ: Admin, Member, Doctor)
    Description NVARCHAR(MAX),                             -- Mô tả chi tiết về vai trò
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
    Name VARCHAR(255) NOT NULL,                             -- Tên người dùng
    Gender NVARCHAR(20) NOT NULL,                           -- Giới tính (Female, Male, Other)
    DateOfBirth DATE NOT NULL,                              -- Ngày sinh
    Address VARCHAR(255) NOT NULL,                          -- Địa chỉ
    Password VARCHAR(255) NOT NULL,                         -- Mật khẩu
    RegistrationDate DATETIME NOT NULL,                     -- Ngày đăng ký
    LastLogin DATETIME,                                     -- Ngày đăng nhập lần cuối
    ProfilePicture VARBINARY(MAX),                          -- Lưu ảnh dưới dạng nhị phân
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
    EmergencyContact VARCHAR(255) NOT NULL,                 -- Thông tin liên lạc khẩn cấp
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',          -- Trạng thái thành viên (1: active, 2: inactive, 3: pending, etc.)
    JoinDate DATETIME NOT NULL,                             -- Ngày gia nhập
    LeaveDate DATETIME,                                     -- Ngày rời khỏi hệ thống (nếu có)
	Notes NVARCHAR(MAX),                                    -- Ghi chú thêm
    FOREIGN KEY (UserID) REFERENCES UserAccounts(UserID)    -- Liên kết với bảng UserAccounts
);

-- Table Children
CREATE TABLE Children (
    ChildID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),   -- ID của trẻ
    MemberID UNIQUEIDENTIFIER  NOT NULL,                    -- ID thành viên liên kết với bảng Members
    Name VARCHAR(255) NOT NULL,                             -- Tên trẻ
    DateOfBirth DATE NOT NULL,                              -- Ngày sinh
    Gender NVARCHAR(20) NOT NULL,                           -- Giới tính (Female, Male, Other)
    BirthWeight FLOAT NOT NULL,                             -- Cân nặng lúc sinh
    BirthHeight FLOAT NOT NULL,                             -- Chiều cao lúc sinh
    BloodType VARCHAR(10) NOT NULL,                         -- Nhóm máu
    Allergies NVARCHAR(MAX),                                -- Dị ứng (nếu có)
    Notes NVARCHAR(MAX),                                    -- Ghi chú thêm
	RelationshipToMember VARCHAR(50) NOT NULL,              -- Mối quan hệ với member
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',          -- Trạng thái (active, inactive, pending, v.v.)
	StatusChangeReason NVARCHAR(MAX),                       -- Lý do thay đổi trạng thái (nếu có)
	CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian tạo thông tin trẻ
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian cập nhật thông tin (nếu có thay đổi)
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID)     -- Liên kết với bảng Members
);

-- Table GrowthRecords
CREATE TABLE GrowthRecords (
    RecordID INT PRIMARY KEY IDENTITY(1,1),  -- Khóa chính cho bảng GrowthRecords
    ChildID UNIQUEIDENTIFIER NOT NULL,  -- ID của trẻ
    RecordedBy UNIQUEIDENTIFIER NOT NULL,  -- ID người ghi nhận
    AgeAtRecord INT NOT NULL,  -- Tuổi tại thời điểm ghi nhận
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',  -- Trạng thái (mặc định là 'Active')
    Verified BIT DEFAULT 0,  -- Trạng thái đã xác nhận (mặc định là 0)
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,  -- Thời gian tạo bản ghi
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,  -- Thời gian cập nhật bản ghi
    FOREIGN KEY (ChildID) REFERENCES Children(ChildID),  -- Liên kết với bảng Children
    FOREIGN KEY (RecordedBy) REFERENCES UserAccounts(UserID)  -- Liên kết với bảng UserAccounts
);

-- Table BasicGrowthRecords
CREATE TABLE BasicGrowthRecords (
    BasicRecordID INT PRIMARY KEY IDENTITY(1,1),  -- Khóa chính riêng biệt cho bảng BasicGrowthRecords
    RecordID INT NOT NULL,  -- Khóa ngoại liên kết với GrowthRecords
    Weight FLOAT NOT NULL,  -- Cân nặng của trẻ
    Height FLOAT NOT NULL,  -- Chiều cao của trẻ
    HeadCircumference FLOAT,  -- Vòng đầu
    MuscleMass FLOAT,  -- Khối lượng cơ bắp
    ChestCircumference FLOAT,  -- Vòng ngực
    Notes NVARCHAR(MAX),  -- Ghi chú khác
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,  -- Thời gian tạo bản ghi
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,  -- Thời gian cập nhật bản ghi
    FOREIGN KEY (RecordID) REFERENCES GrowthRecords(RecordID)  -- Liên kết với bảng GrowthRecords
);

-- Table NutritionalRecords
CREATE TABLE NutritionalRecords (
    NutritionalRecordID INT PRIMARY KEY IDENTITY(1,1),  -- Khóa chính riêng biệt cho bảng NutritionalRecords
    RecordID INT NOT NULL,  -- Khóa ngoại liên kết với GrowthRecords
    NutritionalStatus NVARCHAR(50),  -- Tình trạng dinh dưỡng
    FerritinLevel FLOAT,  -- Mức ferritin trong cơ thể
    Triglycerides FLOAT,  -- Mức triglycerides
    BloodSugarLevel FLOAT,  -- Mức đường huyết
    PhysicalActivityLevel NVARCHAR(50),  -- Mức độ hoạt động thể chất
    Notes NVARCHAR(MAX),  -- Ghi chú khác
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,  -- Thời gian tạo bản ghi
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,  -- Thời gian cập nhật bản ghi
    FOREIGN KEY (RecordID) REFERENCES GrowthRecords(RecordID)  -- Liên kết với bảng GrowthRecords
);

-- Table PhysiologicalRecords
CREATE TABLE PhysiologicalRecords (
    PhysiologicalRecordID INT PRIMARY KEY IDENTITY(1,1),  -- Khóa chính riêng biệt cho bảng PhysiologicalRecords
    RecordID INT NOT NULL,  -- Khóa ngoại liên kết với GrowthRecords
    HeartRate INT,  -- Nhịp tim
    BloodPressure FLOAT,  -- Huyết áp
    BodyTemperature FLOAT,  -- Nhiệt độ cơ thể
    OxygenSaturation FLOAT,  -- Mức độ bão hòa oxy trong máu
    SleepDuration FLOAT,  -- Thời gian ngủ
    Vision NVARCHAR(50),  -- Tình trạng thị giác
    Hearing NVARCHAR(50),  -- Tình trạng thính giác
    Notes NVARCHAR(MAX),  -- Ghi chú khác
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,  -- Thời gian tạo bản ghi
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,  -- Thời gian cập nhật bản ghi
    FOREIGN KEY (RecordID) REFERENCES GrowthRecords(RecordID)  -- Liên kết với bảng GrowthRecords
);

-- Table DevelopmentalRecords
CREATE TABLE DevelopmentalRecords (
    DevelopmentalRecordID INT PRIMARY KEY IDENTITY(1,1),  -- Khóa chính riêng biệt cho bảng DevelopmentalRecords
    RecordID INT NOT NULL,  -- Khóa ngoại liên kết với GrowthRecords
    ImmunizationStatus NVARCHAR(MAX),  -- Tình trạng tiêm chủng
    MentalHealthStatus NVARCHAR(50),  -- Tình trạng sức khỏe tâm thần
    GrowthHormoneLevel FLOAT,  -- Mức độ hormone tăng trưởng
    AttentionSpan NVARCHAR(50),  -- Thời gian chú ý
    NeurologicalReflexes NVARCHAR(255),  -- Phản xạ thần kinh
    DevelopmentalMilestones NVARCHAR(255),  -- Mốc phát triển
    Notes NVARCHAR(MAX),  -- Ghi chú khác
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,  -- Thời gian tạo bản ghi
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,  -- Thời gian cập nhật bản ghi
    FOREIGN KEY (RecordID) REFERENCES GrowthRecords(RecordID)  -- Liên kết với bảng GrowthRecords
);

-- Table Promotions
CREATE TABLE Promotions (
    PromotionID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(), -- Mã khuyến mãi
    PromotionCode VARCHAR(50) NOT NULL,                       -- Mã khuyến mãi duy nhất
    Description NVARCHAR(MAX),                                -- Mô tả chi tiết về khuyến mãi
    DiscountPercent INT NOT NULL,                             -- Phần trăm giảm giá
    MinPurchaseAmount DECIMAL(10, 2),                         -- Mức mua tối thiểu để áp dụng khuyến mãi
    MaxDiscountAmount DECIMAL(10, 2),                         -- Giới hạn giảm giá tối đa
    ApplicablePackageIDs NVARCHAR(MAX),                       -- Các gói thành viên áp dụng (nếu có)
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
    PackageName VARCHAR(255) NOT NULL,                      -- Tên gói thành viên
    Description NVARCHAR(MAX),                              -- Mô tả về gói thành viên
    Price DECIMAL(10, 2) NOT NULL,                          -- Giá gói thành viên
    Currency NVARCHAR(10) DEFAULT 'VND',                    -- Đơn vị tiền tệ của giá gói
    DurationMonths INT NOT NULL,                            -- Thời gian sử dụng gói trong tháng
    IsRecurring BIT NOT NULL,                               -- Cho biết liệu gói có tái đăng ký tự động hay không
    TrialPeriodDays INT,                                    -- Thời gian dùng thử (nếu có)
    MaxChildrenAllowed INT NOT NULL,                        -- Số lượng trẻ tối đa được phép trong gói
    SupportLevel NVARCHAR(255),                             -- Mức độ hỗ trợ của gói thành viên
    PromotionID UNIQUEIDENTIFIER,                           -- Liên kết với bảng Promotions
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',          -- Trạng thái của gói thành viên (1: active, 0: inactive, 2: pending)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian tạo gói thành viên
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian cập nhật
    FOREIGN KEY (PromotionID) REFERENCES Promotions(PromotionID)
);

-- Table Features
CREATE TABLE Features (
    FeatureID INT PRIMARY KEY IDENTITY(1,1),
    FeatureName VARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
	Status NVARCHAR(50) NOT NULL DEFAULT 'Active',
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Table PackageFeatures
CREATE TABLE PackageFeatures (
    PackageID INT IDENTITY(1,1),                             -- Gói thành viên
    FeatureID INT,                                           -- Tính năng
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,   -- Thời gian tạo
	Status NVARCHAR(50) NOT NULL DEFAULT 'Active',
    PRIMARY KEY (PackageID, FeatureID),                      -- Khóa chính kép
    FOREIGN KEY (PackageID) REFERENCES MembershipPackages(PackageID),
    FOREIGN KEY (FeatureID) REFERENCES Features(FeatureID)
);

-- Table MemberMemberships
CREATE TABLE MemberMemberships (
    MemberMembershipID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    MemberID UNIQUEIDENTIFIER NOT NULL,                     -- Liên kết với bảng Members
    PackageID INT NOT NULL,                                  -- Liên kết với bảng MembershipPackages
    StartDate DATETIME NOT NULL,                             -- Ngày bắt đầu gói thành viên
    EndDate DATETIME NOT NULL,                               -- Ngày kết thúc gói thành viên
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',           -- Trạng thái của gói thành viên (Active, Inactive, Pending, Suspended, Expired, Canceled, Renewing, Trial)
    IsActive BIT NOT NULL DEFAULT 1,                          -- Trạng thái hoạt động của gói
    Description NVARCHAR(MAX),                               -- Mô tả gói thành viên
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,    -- Thời gian tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,    -- Thời gian cập nhật
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID),     -- Liên kết với bảng Members
    FOREIGN KEY (PackageID) REFERENCES MembershipPackages(PackageID)  -- Liên kết với bảng MembershipPackages
);

CREATE TABLE Transactions (
    TransactionID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserID UNIQUEIDENTIFIER NOT NULL,
    MemberMembershipID UNIQUEIDENTIFIER NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    Currency NVARCHAR(50) NOT NULL,
    TransactionType NVARCHAR(100) NOT NULL,         -- Loại giao dịch (ví dụ: Đăng ký, Mua thẻ, v.v.)
    PaymentMethod NVARCHAR(50) NOT NULL,            -- Cổng thanh toán (VnPay, MoMo, Internet Banking)
    TransactionDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Thời điểm giao dịch (ngày người dùng bắt đầu giao dịch)
    PaymentDate DATETIME,                           -- Thời điểm thanh toán (ngày thanh toán được xử lý thành công, cập nhật khi Status = 'Success')
    GatewayTransactionID VARCHAR(255),              -- Mã giao dịch từ cổng thanh toán (ví dụ: mã giao dịch VnPay, MoMo)
    PaymentStatus NVARCHAR(50) NOT NULL DEFAULT 'Active',-- Trạng thái thanh toán (Pending, Success, Failed, Cancelled, Refunded)
    Description VARCHAR(255),                       -- Mô tả giao dịch
    FOREIGN KEY (UserID) REFERENCES UserAccounts(UserID),
    FOREIGN KEY (MemberMembershipID) REFERENCES MemberMemberships(MemberMembershipID)
);

-- Table Doctors
CREATE TABLE Doctors (
    DoctorID INT PRIMARY KEY IDENTITY(1,1),
    UserID UNIQUEIDENTIFIER NOT NULL,
    Name VARCHAR(255) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    PhoneNumber VARCHAR(20) UNIQUE NOT NULL,
    Degree VARCHAR(255) NOT NULL,
    LicenseNumber VARCHAR(255) NOT NULL,
    Biography NVARCHAR(MAX),
    ProfilePicture VARBINARY(MAX),
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserID) REFERENCES UserAccounts(UserID)
);

-- Table Specializations
CREATE TABLE Specializations (
    SpecializationID INT PRIMARY KEY IDENTITY(1,1),             -- Sử dụng UNIQUEIDENTIFIER làm khóa chính và tự động tạo GUID
    SpecializationName VARCHAR(255) NOT NULL,                   -- Tên chuyên ngành không thể null
    Description NVARCHAR(MAX),                                  -- Mô tả về chuyên ngành
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',              -- Trạng thái của chuyên ngành (1: active, 0: inactive)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,      -- Thời gian tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP       -- Thời gian cập nhật
);

-- Table DoctorSpecializations
CREATE TABLE DoctorSpecializations (
    DoctorSpecializationID INT PRIMARY KEY IDENTITY(1,1),  -- ID mối quan hệ bác sĩ-chuyên ngành
    DoctorID INT NOT NULL,                                 -- ID bác sĩ (liên kết với bảng Doctors)
    SpecializationID INT NOT NULL,                         -- ID chuyên ngành (liên kết với bảng Specializations)
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',         -- Trạng thái (1: active, 0: inactive)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Thời gian tạo mối quan hệ
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Thời gian cập nhật mối quan hệ
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID),   -- Liên kết với bảng Doctors
    FOREIGN KEY (SpecializationID) REFERENCES Specializations(SpecializationID) -- Liên kết với bảng Specializations
);

-- Table Hospitals
CREATE TABLE Hospitals (
    HospitalID INT PRIMARY KEY IDENTITY(1,1), 
    HospitalName VARCHAR(255) NOT NULL,
    Address VARCHAR(255),
    ContactInfo VARCHAR(255),
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active', -- 1: Active, 0: Inactive
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Table DoctorHospitals
CREATE TABLE DoctorHospitals (
    DoctorHospitalID INT PRIMARY KEY IDENTITY(1,1),                -- ID duy nhất cho mối quan hệ
    DoctorID INT NOT NULL,                                         -- Liên kết đến bảng Doctors
    HospitalID INT NOT NULL,                                       -- Liên kết đến bảng Hospitals
    StartDate DATE NOT NULL,                                       -- Ngày bắt đầu làm việc tại bệnh viện
    EndDate DATE,                                                  -- Ngày kết thúc (nếu có)
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',                 -- Trạng thái (1: active, 0: inactive)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,         -- Ngày tạo mối quan hệ
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,         -- Ngày cập nhật mối quan hệ
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID),           -- Ràng buộc khóa ngoại tới Doctors
    FOREIGN KEY (HospitalID) REFERENCES Hospitals(HospitalID)      -- Ràng buộc khóa ngoại tới Hospitals
);

-- Table ConsultationRequests
CREATE TABLE ConsultationRequests (
    RequestID INT PRIMARY KEY IDENTITY(1,1),                         -- ID duy nhất cho yêu cầu
    MemberID UNIQUEIDENTIFIER NOT NULL,                              -- Liên kết với bảng Members
    ChildID UNIQUEIDENTIFIER NOT NULL,                               -- Liên kết với bảng Children
    RequestDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,         -- Ngày tạo yêu cầu tư vấn
    Description NVARCHAR(MAX),                                       -- Mô tả chi tiết về yêu cầu tư vấn
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',                  -- Trạng thái yêu cầu: 0 (pending), 1 (approved), 2 (rejected)
    Urgency NVARCHAR(50),                                            -- Mức độ khẩn cấp: low, medium, high
    Attachments VARCHAR(255),                                        -- Đường dẫn tệp đính kèm
    Category NVARCHAR(100),                                          -- Danh mục tư vấn: ví dụ: dinh dưỡng, phát triển, ...
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,           -- Thời gian yêu cầu được tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,           -- Thời gian cập nhật yêu cầu
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID),             -- Liên kết với bảng Members
    FOREIGN KEY (ChildID) REFERENCES Children(ChildID)               -- Liên kết với bảng Children
);

-- Table ConsultationResponses
CREATE TABLE ConsultationResponses (
    ResponseID INT PRIMARY KEY IDENTITY(1,1),                         -- ID phản hồi duy nhất
    RequestID INT NOT NULL,                                           -- Liên kết với bảng ConsultationRequests
    DoctorID INT NOT NULL,                                            -- Liên kết với bảng Doctors
    ResponseDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,         -- Thời gian phản hồi
    Content NVARCHAR(MAX) NOT NULL,                                   -- Nội dung phản hồi (bắt buộc)
    Attachments VARCHAR(255),                                         -- Đường dẫn tệp đính kèm (không bắt buộc)
    IsHelpful BIT NULL,                                               -- Đánh giá xem phản hồi có hữu ích không (NULL nếu không đánh giá)
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',                   -- Trạng thái phản hồi: 0 (pending), 1 (answered), 2 (resolved)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,            -- Thời gian tạo phản hồi
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,            -- Thời gian cập nhật phản hồi
    FOREIGN KEY (RequestID) REFERENCES ConsultationRequests(RequestID), -- Liên kết với bảng ConsultationRequests
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)              -- Liên kết với bảng Doctors
);

-- Table RatingFeedbacks
CREATE TABLE RatingFeedbacks (
    FeedbackId INT PRIMARY KEY IDENTITY(1,1),                -- ID phản hồi đánh giá
    UserId UNIQUEIDENTIFIER NOT NULL,                        -- Liên kết với bảng UserAccounts
    ResponseId INT NOT NULL,                                 -- Liên kết với bảng ConsultationResponses
    Rating INT NOT NULL CHECK (Rating BETWEEN 1 AND 5),      -- Đánh giá từ 1 đến 5
    Comment NVARCHAR(MAX) NULL,                              -- Nhận xét (không bắt buộc)
    FeedbackDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,-- Thời gian phản hồi
    FeedbackType NVARCHAR(50) NULL DEFAULT 'general',        -- Loại phản hồi (không bắt buộc, mặc định là 'general')
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',          -- Trạng thái: 'Pending', 'Approved', 'Rejected'
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,   -- Thời gian tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,   -- Thời gian cập nhật
    FOREIGN KEY (UserId) REFERENCES UserAccounts(UserId),    -- Liên kết với bảng UserAccounts
    FOREIGN KEY (ResponseId) REFERENCES ConsultationResponses(ResponseId) -- Liên kết với bảng ConsultationResponses
);

-- Table Diseases
CREATE TABLE Diseases (
    DiseaseID INT PRIMARY KEY IDENTITY(1,1),              -- ID bệnh
    DiseaseName VARCHAR(100) NOT NULL,                      -- Tên bệnh
    LowerBound FLOAT NOT NULL,                              -- Giới hạn thấp cho chỉ số (cân nặng, chiều cao, BMI)
    UpperBound FLOAT NOT NULL,                              -- Giới hạn cao cho chỉ số (cân nặng, chiều cao, BMI)
    MinAge INT NOT NULL,                                    -- Độ tuổi nhỏ nhất có thể mắc bệnh
    MaxAge INT NOT NULL,                                    -- Độ tuổi lớn nhất có thể mắc bệnh
    Severity NVARCHAR(50) NOT NULL,                         -- Độ nghiêm trọng (High, Medium, Low)
    DiseaseType NVARCHAR(50) NOT NULL,                      -- Loại bệnh (ví dụ: Béo phì, Suy dinh dưỡng)
    Symptoms NVARCHAR(MAX) NOT NULL,                        -- Triệu chứng bệnh
    Treatment NVARCHAR(MAX),                                -- Phương pháp điều trị
    Prevention NVARCHAR(MAX),                               -- Phương pháp phòng ngừa
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian tạo
    LastModified DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Thời gian chỉnh sửa gần nhất
    Description NVARCHAR(MAX),                              -- Mô tả về bệnh
    Notes NVARCHAR(MAX),                                    -- Ghi chú thêm
    IsActive BIT NOT NULL DEFAULT 1                          -- Trạng thái (Hoạt động hoặc không)
);

-- Table Alerts
CREATE TABLE Alerts (
    AlertID INT PRIMARY KEY IDENTITY(1,1),
    GrowthRecordID INT NOT NULL,                           -- Liên kết với bảng GrowthRecords
    AlertDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Ngày giờ cảnh báo
    DiseaseID INT NOT NULL,                                -- Liên kết với bảng Diseases
    Message NVARCHAR(MAX) NOT NULL,                        -- Nội dung cảnh báo
    IsRead BIT NOT NULL DEFAULT 0,                         -- Trạng thái đọc
    SeverityLevel NVARCHAR(50),                            -- Mức độ nghiêm trọng
    IsAcknowledged BIT NOT NULL DEFAULT 0,                 -- Trạng thái xác nhận
    FOREIGN KEY (GrowthRecordID) REFERENCES GrowthRecords(RecordID), -- Liên kết GrowthRecords
    FOREIGN KEY (DiseaseID) REFERENCES Diseases(DiseaseID) -- Liên kết với Diseases
);

-- Table Milestones
CREATE TABLE Milestones (
    MilestoneID INT PRIMARY KEY IDENTITY(1,1),
    GrowthRecordID INT NOT NULL,                           -- Liên kết với bảng GrowthRecords
    MilestoneName NVARCHAR(255) NOT NULL,                 -- Tên mốc phát triển
    AchievedDate DATE,                                     -- Ngày đạt mốc
    Status NVARCHAR(50) NOT NULL DEFAULT 'Not Achieved',   -- Trạng thái mốc
    Notes NVARCHAR(MAX),                                   -- Ghi chú
    Guidelines NVARCHAR(MAX),                              -- Hướng dẫn
    Importance NVARCHAR(50) NOT NULL DEFAULT 'Medium',     -- Độ quan trọng
    Category NVARCHAR(100),                                -- Nhóm mốc
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Ngày tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Ngày cập nhật
    FOREIGN KEY (GrowthRecordID) REFERENCES GrowthRecords(RecordID) -- Liên kết GrowthRecords
);

-- Table BlogCategories
CREATE TABLE BlogCategories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),                  -- ID thể loại
    CategoryName VARCHAR(255) NOT NULL,                        -- Tên thể loại bài viết (không được NULL)
    Description NVARCHAR(MAX),                                 -- Mô tả về thể loại
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,     -- Thời gian tạo thể loại
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,     -- Thời gian cập nhật thể loại
    IsActive BIT NOT NULL DEFAULT 1,                           -- Trạng thái thể loại (1: Active, 0: Inactive)
    ParentCategoryID INT,                                      -- ID thể loại cha (nếu có thể loại con)
    ThumbnailImage VARCHAR(255),                               -- Hình ảnh đại diện (nếu có)
    FOREIGN KEY (ParentCategoryID) REFERENCES BlogCategories(CategoryID)  -- Liên kết với thể loại cha
);

-- Table Blogs
CREATE TABLE Blogs (
    BlogID INT PRIMARY KEY IDENTITY(1,1),                      -- ID bài viết
    Title VARCHAR(255) NOT NULL,                               -- Tiêu đề bài viết
    Content NVARCHAR(MAX) NOT NULL,                            -- Nội dung bài viết
    AuthorID UNIQUEIDENTIFIER NOT NULL,                        -- ID tác giả (admin hoặc người dùng)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,     -- Thời gian tạo bài viết
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,     -- Thời gian cập nhật bài viết
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',            -- Trạng thái bài viết ('Pending', 'Approved', 'Rejected')
    CategoryID INT NOT NULL,                                   -- ID thể loại bài viết
    Tags VARCHAR(255),                                         -- Các tag của bài viết
    ReferenceSources NVARCHAR(MAX),                           -- Các trích dẫn hoặc nguồn tài liệu tham khảo
    FOREIGN KEY (AuthorID) REFERENCES UserAccounts(UserID),    -- Liên kết với bảng UserAccounts (tác giả)
    FOREIGN KEY (CategoryID) REFERENCES BlogCategories(CategoryID)  -- Liên kết với bảng BlogCategories
);

-- Table BlogApprovals
CREATE TABLE BlogApprovals (
    ApprovalID INT PRIMARY KEY IDENTITY(1,1),                  -- ID duyệt bài (auto-increment)
    BlogID INT NOT NULL,                                       -- ID bài viết cần duyệt
    ApprovalDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian duyệt bài
    ApprovalStatus NVARCHAR(50) NOT NULL DEFAULT 'Needs Revision', -- Trạng thái duyệt bài ('Approved', 'Rejected', 'Needs Revision')
    RejectionReason NVARCHAR(MAX),                            -- Lý do từ chối nếu bài bị từ chối
    FOREIGN KEY (BlogID) REFERENCES Blogs(BlogID)             -- Liên kết với bài viết              
);

-- Table BlogReferences
CREATE TABLE BlogReferences (
    ReferenceID INT PRIMARY KEY IDENTITY(1,1),                -- ID của nguồn tài liệu
    BlogID INT NOT NULL,                                      -- ID bài viết liên quan
    ReferenceText NVARCHAR(MAX) NOT NULL,                     -- Nội dung trích dẫn hoặc nguồn tài liệu tham khảo
    ReferenceURL VARCHAR(255),                                -- URL của nguồn tài liệu (nếu có)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,    -- Thời gian tạo nguồn tài liệu tham khảo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,    -- Thời gian cập nhật nguồn tài liệu tham khảo
    FOREIGN KEY (BlogID) REFERENCES Blogs(BlogID)             -- Liên kết với bài viết
);

-- Insert Database
-- Insert Roles
INSERT INTO Roles (RoleName, Description, Status) 
VALUES 
('Member', 'Role for regular members who track child growth', 'Active'),
('Doctor', 'Role for doctors to provide consultation and advice', 'Active'),
('Admin', 'Role for administrators who manage the system', 'Active');
GO

-- Insert Membership Packages
INSERT INTO MembershipPackages (PackageName, Description, Price, Currency, DurationMonths, IsRecurring, MaxChildrenAllowed, Status)
VALUES
('Free', 'Free membership with basic features', 0.00, 'VND', 12, 0, 1, 'Active'),
('Standard', 'Standard membership with additional features', 150.00, 'VND', 12, 1, 2, 'Active'),
('Premium', 'Premium membership with full features', 500.00, 'VND', 12, 1, 3, 'Active');
GO

-- Insert UserAccounts
INSERT INTO UserAccounts (Username, Email, PhoneNumber, Name, Gender, DateOfBirth, Address, Password, RegistrationDate, RoleID)
VALUES
('admin_user', 'admin@example.com', '0901234567', 'Admin User', 'Male', '1985-05-15', '123 Admin St.', 'password123', GETDATE(), 3),  -- Admin
('member_user_1', 'member1@example.com', '0902345678', 'Member User 1', 'Female', '1990-01-01', '123 Member St.', 'password123', GETDATE(), 1),  -- Member 1
('member_user_2', 'member2@example.com', '0903456789', 'Member User 2', 'Male', '1992-06-10', '456 Member St.', 'password123', GETDATE(), 1),  -- Member 2
('member_user_3', 'member3@example.com', '0904567890', 'Member User 3', 'Female', '1995-10-20', '789 Member St.', 'password123', GETDATE(), 1);  -- Member 3
GO

-- Insert Members
INSERT INTO Members (UserID, EmergencyContact, JoinDate, Status)
VALUES
((SELECT UserID FROM UserAccounts WHERE Username = 'member_user_1'), '123-456-789', GETDATE(), 'Active'),  -- Member 1
((SELECT UserID FROM UserAccounts WHERE Username = 'member_user_2'), '234-567-890', GETDATE(), 'Active'),  -- Member 2
((SELECT UserID FROM UserAccounts WHERE Username = 'member_user_3'), '345-678-901', GETDATE(), 'Active');  -- Member 3
GO

-- Insert Children
INSERT INTO Children (MemberID, Name, DateOfBirth, Gender, BirthWeight, BirthHeight, BloodType, Allergies, RelationshipToMember, Status)
VALUES
((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_1')), 
'Child 1', '2022-01-01', 'Female', 3.2, 50, 'O', NULL, 'Daughter', 'Active'),  -- Member 1
((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_2')), 
'Child 2', '2021-06-15', 'Male', 3.5, 52, 'A', NULL, 'Son', 'Active'),  -- Member 2
((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_3')), 
'Child 3', '2020-12-10', 'Female', 3.0, 48, 'B', NULL, 'Daughter', 'Active'),  -- Member 3
((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_3')), 
'Child 4', '2019-11-25', 'Male', 3.4, 49, 'AB', NULL, 'Son', 'Active');  -- Member 3
GO

-- Insert MemberMemberships
INSERT INTO MemberMemberships (MemberID, PackageID, StartDate, EndDate, Status)
VALUES
((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_1')), 
(SELECT PackageID FROM MembershipPackages WHERE PackageName = 'Free'), GETDATE(), DATEADD(MONTH, 1, GETDATE()), 'Active'),  -- Member 1 - Free

((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_2')), 
(SELECT PackageID FROM MembershipPackages WHERE PackageName = 'Free'), GETDATE(), DATEADD(MONTH, 1, GETDATE()), 'Inactive'),  -- Member 2 - Free, then Standard

((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_2')), 
(SELECT PackageID FROM MembershipPackages WHERE PackageName = 'Standard'), DATEADD(MONTH, 1, GETDATE()), DATEADD(MONTH, 13, GETDATE()), 'Active'),  -- Member 2 - Standard

((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_3')), 
(SELECT PackageID FROM MembershipPackages WHERE PackageName = 'Free'), GETDATE(), DATEADD(MONTH, 1, GETDATE()), 'Inactive'),  -- Member 3 - Free, then Premium

((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_3')), 
(SELECT PackageID FROM MembershipPackages WHERE PackageName = 'Premium'), DATEADD(MONTH, 1, GETDATE()), DATEADD(MONTH, 13, GETDATE()), 'Active');  -- Member 3 - Premium

GO

-- Insert Transactions
INSERT INTO Transactions (UserID, MemberMembershipID, Amount, Currency, TransactionType, PaymentMethod, TransactionDate, PaymentDate, PaymentStatus)
VALUES
((SELECT UserID FROM UserAccounts WHERE Username = 'member_user_2'), 
(SELECT MemberMembershipID FROM MemberMemberships WHERE MemberID = (SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_2')) 
AND PackageID = (SELECT PackageID FROM MembershipPackages WHERE PackageName = 'Standard')), 100.00, 'VND', 'Purchase', 'VnPay', GETDATE(), GETDATE(), 'Success'),

((SELECT UserID FROM UserAccounts WHERE Username = 'member_user_3'), 
(SELECT MemberMembershipID FROM MemberMemberships WHERE MemberID = (SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_3')) 
AND PackageID = (SELECT PackageID FROM MembershipPackages WHERE PackageName = 'Premium')), 200.00, 'VND', 'Purchase', 'MoMo', GETDATE(), GETDATE(), 'Success');
