/****** Object:  Table [dbo].[Appointment]    Script Date: 3/21/2025 2:36:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[AppointmentId] [int] IDENTITY(1,1) NOT NULL,
	[PaymentDetailId] [int] NULL,
	[UserId] [int] NOT NULL,
	[ChildId] [int] NOT NULL,
	[VaccineId] [int] NULL,
	[VaccinePackageId] [int] NULL,
	[AppointmentDate] [datetime2](7) NOT NULL,
	[Reaction] [nvarchar](max) NULL,
	[AppointmentStatus] [int] NOT NULL,
	[IsActive] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChildProfile]    Script Date: 3/21/2025 2:36:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChildProfile](
	[ChildId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[Gender] [int] NOT NULL,
	[Relationship] [int] NOT NULL,
	[IsActive] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[ModifiedBy] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ChildProfile] PRIMARY KEY CLUSTERED 
(
	[ChildId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Disease]    Script Date: 3/21/2025 2:36:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Disease](
	[DiseaseId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsActive] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Disease] PRIMARY KEY CLUSTERED 
(
	[DiseaseId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 3/21/2025 2:36:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[VaccinePackageId] [int] NULL,
	[AppointmentId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[VaccineId] [int] NULL,
	[PaymentType] [nvarchar](max) NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[PaymentDate] [datetime2](7) NOT NULL,
	[PaymentStatus] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentDetail]    Script Date: 3/21/2025 2:36:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentDetail](
	[PaymentDetailId] [int] IDENTITY(1,1) NOT NULL,
	[PaymentId] [int] NOT NULL,
	[VaccinePackageDetailId] [int] NOT NULL,
	[IsCompleted] [int] NOT NULL,
	[AdministeredDate] [datetime2](7) NULL,
	[Notes] [nvarchar](max) NULL,
	[DoseSequence] [int] NOT NULL,
	[ScheduledDate] [datetime2](7) NULL,
	[VaccinePackageId] [int] NULL,
 CONSTRAINT [PK_PaymentDetail] PRIMARY KEY CLUSTERED 
(
	[PaymentDetailId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/21/2025 2:36:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[PasswordSalt] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Role] [int] NOT NULL,
	[DateOfBirth] [datetime2](7) NULL,
	[IsActive] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vaccine]    Script Date: 3/21/2025 2:36:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vaccine](
	[VaccineId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Origin] [nvarchar](max) NOT NULL,
	[Manufacturer] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[DateOfManufacture] [datetime2](7) NOT NULL,
	[ExpiryDate] [datetime2](7) NOT NULL,
	[IsActive] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Vaccine] PRIMARY KEY CLUSTERED 
(
	[VaccineId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VaccineDisease]    Script Date: 3/21/2025 2:36:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VaccineDisease](
	[VaccineDiseaseId] [int] IDENTITY(1,1) NOT NULL,
	[VaccineId] [int] NOT NULL,
	[DiseaseId] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_VaccineDisease] PRIMARY KEY CLUSTERED 
(
	[VaccineDiseaseId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VaccinePackage]    Script Date: 3/21/2025 2:36:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VaccinePackage](
	[VaccinePackageId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[TotalDoses] [int] NOT NULL,
	[AgeInMonths] [int] NOT NULL,
	[IsActive] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_VaccinePackage] PRIMARY KEY CLUSTERED 
(
	[VaccinePackageId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VaccinePackageDetail]    Script Date: 3/21/2025 2:36:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VaccinePackageDetail](
	[VaccinePackageDetailId] [int] IDENTITY(1,1) NOT NULL,
	[VaccinePackageId] [int] NOT NULL,
	[VaccineId] [int] NOT NULL,
	[IsActive] [int] NOT NULL,
	[DoseNumber] [int] NOT NULL,
 CONSTRAINT [PK_VaccinePackageDetail] PRIMARY KEY CLUSTERED 
(
	[VaccinePackageDetailId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VaccineProfile]    Script Date: 3/21/2025 2:36:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VaccineProfile](
	[VaccineProfileId] [int] IDENTITY(1,1) NOT NULL,
	[VaccineScheduleId] [int] NOT NULL,
	[AppointmentId] [int] NULL,
	[ChildId] [int] NOT NULL,
	[DiseaseId] [int] NOT NULL,
	[VaccinationDate] [datetime2](7) NULL,
	[DoseNumber] [int] NOT NULL,
	[ScheduledDate] [datetime2](7) NOT NULL,
	[IsCompleted] [int] NOT NULL,
	[IsActive] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[ModifiedBy] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_VaccineProfile] PRIMARY KEY CLUSTERED 
(
	[VaccineProfileId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VaccineSchedule]    Script Date: 3/21/2025 2:36:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VaccineSchedule](
	[VaccineScheduleId] [int] IDENTITY(1,1) NOT NULL,
	[DiseaseId] [int] NOT NULL,
	[AgeInMonths] [int] NOT NULL,
	[DoseNumber] [int] NOT NULL,
	[IsActive] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_VaccineSchedule] PRIMARY KEY CLUSTERED 
(
	[VaccineScheduleId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Vaccine] ADD  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_ChildProfile_ChildId] FOREIGN KEY([ChildId])
REFERENCES [dbo].[ChildProfile] ([ChildId])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_ChildProfile_ChildId]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_PaymentDetail_PaymentDetailId] FOREIGN KEY([PaymentDetailId])
REFERENCES [dbo].[PaymentDetail] ([PaymentDetailId])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_PaymentDetail_PaymentDetailId]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_User_UserId]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Vaccine_VaccineId] FOREIGN KEY([VaccineId])
REFERENCES [dbo].[Vaccine] ([VaccineId])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Vaccine_VaccineId]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_VaccinePackage_VaccinePackageId] FOREIGN KEY([VaccinePackageId])
REFERENCES [dbo].[VaccinePackage] ([VaccinePackageId])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_VaccinePackage_VaccinePackageId]
GO
ALTER TABLE [dbo].[ChildProfile]  WITH CHECK ADD  CONSTRAINT [FK_ChildProfile_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ChildProfile] CHECK CONSTRAINT [FK_ChildProfile_User_UserId]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Appointment_AppointmentId] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointment] ([AppointmentId])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Appointment_AppointmentId]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_User_UserId]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Vaccine_VaccineId] FOREIGN KEY([VaccineId])
REFERENCES [dbo].[Vaccine] ([VaccineId])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Vaccine_VaccineId]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_VaccinePackage_VaccinePackageId] FOREIGN KEY([VaccinePackageId])
REFERENCES [dbo].[VaccinePackage] ([VaccinePackageId])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_VaccinePackage_VaccinePackageId]
GO
ALTER TABLE [dbo].[PaymentDetail]  WITH CHECK ADD  CONSTRAINT [FK_PaymentDetail_Payment_PaymentId] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[Payment] ([PaymentId])
GO
ALTER TABLE [dbo].[PaymentDetail] CHECK CONSTRAINT [FK_PaymentDetail_Payment_PaymentId]
GO
ALTER TABLE [dbo].[PaymentDetail]  WITH CHECK ADD  CONSTRAINT [FK_PaymentDetail_VaccinePackage_VaccinePackageId] FOREIGN KEY([VaccinePackageId])
REFERENCES [dbo].[VaccinePackage] ([VaccinePackageId])
GO
ALTER TABLE [dbo].[PaymentDetail] CHECK CONSTRAINT [FK_PaymentDetail_VaccinePackage_VaccinePackageId]
GO
ALTER TABLE [dbo].[PaymentDetail]  WITH CHECK ADD  CONSTRAINT [FK_PaymentDetail_VaccinePackageDetail_VaccinePackageDetailId] FOREIGN KEY([VaccinePackageDetailId])
REFERENCES [dbo].[VaccinePackageDetail] ([VaccinePackageDetailId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PaymentDetail] CHECK CONSTRAINT [FK_PaymentDetail_VaccinePackageDetail_VaccinePackageDetailId]
GO
ALTER TABLE [dbo].[VaccineDisease]  WITH CHECK ADD  CONSTRAINT [FK_VaccineDisease_Disease_DiseaseId] FOREIGN KEY([DiseaseId])
REFERENCES [dbo].[Disease] ([DiseaseId])
GO
ALTER TABLE [dbo].[VaccineDisease] CHECK CONSTRAINT [FK_VaccineDisease_Disease_DiseaseId]
GO
ALTER TABLE [dbo].[VaccineDisease]  WITH CHECK ADD  CONSTRAINT [FK_VaccineDisease_Vaccine_VaccineId] FOREIGN KEY([VaccineId])
REFERENCES [dbo].[Vaccine] ([VaccineId])
GO
ALTER TABLE [dbo].[VaccineDisease] CHECK CONSTRAINT [FK_VaccineDisease_Vaccine_VaccineId]
GO
ALTER TABLE [dbo].[VaccinePackageDetail]  WITH CHECK ADD  CONSTRAINT [FK_VaccinePackageDetail_Vaccine_VaccineId] FOREIGN KEY([VaccineId])
REFERENCES [dbo].[Vaccine] ([VaccineId])
GO
ALTER TABLE [dbo].[VaccinePackageDetail] CHECK CONSTRAINT [FK_VaccinePackageDetail_Vaccine_VaccineId]
GO
ALTER TABLE [dbo].[VaccinePackageDetail]  WITH CHECK ADD  CONSTRAINT [FK_VaccinePackageDetail_VaccinePackage_VaccinePackageId] FOREIGN KEY([VaccinePackageId])
REFERENCES [dbo].[VaccinePackage] ([VaccinePackageId])
GO
ALTER TABLE [dbo].[VaccinePackageDetail] CHECK CONSTRAINT [FK_VaccinePackageDetail_VaccinePackage_VaccinePackageId]
GO
ALTER TABLE [dbo].[VaccineProfile]  WITH CHECK ADD  CONSTRAINT [FK_VaccineProfile_Appointment_AppointmentId] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointment] ([AppointmentId])
GO
ALTER TABLE [dbo].[VaccineProfile] CHECK CONSTRAINT [FK_VaccineProfile_Appointment_AppointmentId]
GO
ALTER TABLE [dbo].[VaccineProfile]  WITH CHECK ADD  CONSTRAINT [FK_VaccineProfile_ChildProfile_ChildId] FOREIGN KEY([ChildId])
REFERENCES [dbo].[ChildProfile] ([ChildId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VaccineProfile] CHECK CONSTRAINT [FK_VaccineProfile_ChildProfile_ChildId]
GO
ALTER TABLE [dbo].[VaccineProfile]  WITH CHECK ADD  CONSTRAINT [FK_VaccineProfile_Disease_DiseaseId] FOREIGN KEY([DiseaseId])
REFERENCES [dbo].[Disease] ([DiseaseId])
GO
ALTER TABLE [dbo].[VaccineProfile] CHECK CONSTRAINT [FK_VaccineProfile_Disease_DiseaseId]
GO
ALTER TABLE [dbo].[VaccineProfile]  WITH CHECK ADD  CONSTRAINT [FK_VaccineProfile_VaccineSchedule_VaccineScheduleId] FOREIGN KEY([VaccineScheduleId])
REFERENCES [dbo].[VaccineSchedule] ([VaccineScheduleId])
GO
ALTER TABLE [dbo].[VaccineProfile] CHECK CONSTRAINT [FK_VaccineProfile_VaccineSchedule_VaccineScheduleId]
GO
ALTER TABLE [dbo].[VaccineSchedule]  WITH CHECK ADD  CONSTRAINT [FK_VaccineSchedule_Disease_DiseaseId] FOREIGN KEY([DiseaseId])
REFERENCES [dbo].[Disease] ([DiseaseId])
GO
ALTER TABLE [dbo].[VaccineSchedule] CHECK CONSTRAINT [FK_VaccineSchedule_Disease_DiseaseId]
GO
