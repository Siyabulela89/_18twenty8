﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _18TWENTY8.Models;

namespace _18TWENTY8.Migrations
{
    [DbContext(typeof(EighteentwentyeightContext))]
    [Migration("20210518045057_messchnge")]
    partial class messchnge
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("_18TWENTY8.Models.AdditionalSupportBig", b =>
                {
                    b.Property<int>("AdditionalSupportBigID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.HasKey("AdditionalSupportBigID");

                    b.ToTable("AdditionalSupportBig");
                });

            modelBuilder.Entity("_18TWENTY8.Models.AdditionalSupportStorageBig", b =>
                {
                    b.Property<int>("AdditionalSupportStorID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdditionalSupportBigID");

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("UserID");

                    b.HasKey("AdditionalSupportStorID");

                    b.ToTable("AdditionalSupportStorageBig");
                });

            modelBuilder.Entity("_18TWENTY8.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedName");

                    b.HasKey("Id");

                    b.ToTable("ApplicationRole");
                });

            modelBuilder.Entity("_18TWENTY8.Models.ApplicationStatus", b =>
                {
                    b.Property<int>("ApplicationStatusID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("description");

                    b.HasKey("ApplicationStatusID");

                    b.ToTable("ApplicationStatus");
                });

            modelBuilder.Entity("_18TWENTY8.Models.AssignApprove", b =>
                {
                    b.Property<int>("AssAppID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("description");

                    b.HasKey("AssAppID");

                    b.ToTable("AssignApprove");
                });

            modelBuilder.Entity("_18TWENTY8.Models.AssignSisterStatus", b =>
                {
                    b.Property<int>("AssignSisterStatusID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("description");

                    b.HasKey("AssignSisterStatusID");

                    b.ToTable("AssignSisterStatus");
                });

            modelBuilder.Entity("_18TWENTY8.Models.BigSisterAcademic", b =>
                {
                    b.Property<int>("BigSisterAcademicID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BigSisterUserID");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("QualificationDocname");

                    b.Property<string>("Qualificationurl");

                    b.HasKey("BigSisterAcademicID");

                    b.ToTable("BigSisterAcademic");
                });

            modelBuilder.Entity("_18TWENTY8.Models.BigSisterDetail", b =>
                {
                    b.Property<int>("BigSisterDetailID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressStreet");

                    b.Property<string>("AddressStreetlinetwo");

                    b.Property<int>("ArrestedConvictedQ");

                    b.Property<string>("BackgroundQ");

                    b.Property<string>("CVUrl");

                    b.Property<string>("CertifiedID");

                    b.Property<int>("ConfirmMentordurationQ");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateofBirth");

                    b.Property<string>("DetailsArrestedConvictedQ");

                    b.Property<string>("DetailsOnEverbeenMentorQ");

                    b.Property<string>("EmergencyContactNameone");

                    b.Property<string>("EmergencyContactNametwo");

                    b.Property<string>("EmergencyContactNumberone");

                    b.Property<string>("EmergencyContactNumbertwo");

                    b.Property<int>("EverbeenamentorQ");

                    b.Property<string>("Expectationsonlittlesister");

                    b.Property<string>("IDPassport");

                    b.Property<string>("Imageurl");

                    b.Property<int>("InteractionlevelDigCom");

                    b.Property<string>("InteractionlevelDigComother");

                    b.Property<int>("Interactionlevelmeet");

                    b.Property<string>("Interactionlevelmeetother");

                    b.Property<string>("Name");

                    b.Property<string>("Nickname");

                    b.Property<string>("Phonenumber");

                    b.Property<string>("PostalCode");

                    b.Property<int>("ProfileStatusID");

                    b.Property<int>("Province");

                    b.Property<string>("Surname");

                    b.Property<string>("UserID");

                    b.Property<string>("VerifCode");

                    b.Property<string>("VerifCodeComp");

                    b.Property<string>("email");

                    b.Property<string>("howdidyouhearabout");

                    b.Property<string>("prefferedstudentdetails");

                    b.Property<string>("profilestatusreason");

                    b.Property<string>("verifiedRegistration");

                    b.Property<string>("youngerselfQ");

                    b.HasKey("BigSisterDetailID");

                    b.ToTable("BigSisterDetail");
                });

            modelBuilder.Entity("_18TWENTY8.Models.BursaryApplication", b =>
                {
                    b.Property<int>("BursaryID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ApplicationEndDate");

                    b.Property<DateTime>("ApplicationStartDate");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<string>("QualifyingCriteria");

                    b.Property<string>("Title");

                    b.HasKey("BursaryID");

                    b.ToTable("BursaryApplication");
                });

            modelBuilder.Entity("_18TWENTY8.Models.BursaryApplicationRequest", b =>
                {
                    b.Property<int>("BARID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BursaryStatus");

                    b.Property<string>("Comments");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("UserIDApp");

                    b.HasKey("BARID");

                    b.ToTable("BursaryApplicationRequest");
                });

            modelBuilder.Entity("_18TWENTY8.Models.BursaryStatus", b =>
                {
                    b.Property<int>("BursaryStatusID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.HasKey("BursaryStatusID");

                    b.ToTable("BursaryStatus");
                });

            modelBuilder.Entity("_18TWENTY8.Models.FinancialSupport", b =>
                {
                    b.Property<int>("FinancialSupportID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Academictranscript");

                    b.Property<string>("ApplicationReason");

                    b.Property<int>("ApplicationStatusID");

                    b.Property<string>("CVurl");

                    b.Property<string>("CellphoneNr");

                    b.Property<string>("CertifiedID");

                    b.Property<DateTime>("DOB");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Email");

                    b.Property<string>("IdNr_Passport");

                    b.Property<string>("Imgurl");

                    b.Property<string>("LatestStatementfees");

                    b.Property<string>("Name");

                    b.Property<string>("Nickname");

                    b.Property<string>("PersonalBio");

                    b.Property<string>("Proofofregoistrationurl");

                    b.Property<string>("SiteUrl");

                    b.Property<string>("Surname");

                    b.Property<string>("UserID");

                    b.Property<string>("VerifCode");

                    b.Property<string>("VerifCodeComp");

                    b.Property<string>("VideoURl");

                    b.Property<string>("verifiedRegistration");

                    b.HasKey("FinancialSupportID");

                    b.ToTable("FinancialSupport");
                });

            modelBuilder.Entity("_18TWENTY8.Models.Graduates", b =>
                {
                    b.Property<int>("GraduateID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("GraduateName");

                    b.Property<string>("GraduateStoryname");

                    b.Property<string>("GraduatenameandQual");

                    b.Property<string>("Modalpopupid");

                    b.Property<string>("Videourl");

                    b.Property<string>("imageurl");

                    b.Property<string>("pdfstoryurl");

                    b.HasKey("GraduateID");

                    b.ToTable("Graduates");
                });

            modelBuilder.Entity("_18TWENTY8.Models.InformationInterest", b =>
                {
                    b.Property<int>("InformationofInterestID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.HasKey("InformationofInterestID");

                    b.ToTable("InformationInterest");
                });

            modelBuilder.Entity("_18TWENTY8.Models.InformationofStorageBig", b =>
                {
                    b.Property<int>("InformationofinterestStorageID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("InformationofInterestID");

                    b.Property<int>("UserID");

                    b.HasKey("InformationofinterestStorageID");

                    b.ToTable("InformationofStorageBig");
                });

            modelBuilder.Entity("_18TWENTY8.Models.InformationofStorageLittle", b =>
                {
                    b.Property<int>("InformationofinterestStorageID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("InformationofInterestID");

                    b.Property<int>("UserID");

                    b.HasKey("InformationofinterestStorageID");

                    b.ToTable("InformationofStorageLittle");
                });

            modelBuilder.Entity("_18TWENTY8.Models.InteractionLevel", b =>
                {
                    b.Property<int>("InteractionLevelID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.HasKey("InteractionLevelID");

                    b.ToTable("InteractionLevel");
                });

            modelBuilder.Entity("_18TWENTY8.Models.LittleSisterAcademic", b =>
                {
                    b.Property<int>("LittleSisterAcademicID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("LittleSisterUserID");

                    b.Property<string>("QualificationDocname");

                    b.Property<string>("Qualificationurl");

                    b.HasKey("LittleSisterAcademicID");

                    b.ToTable("LittleSisterAcademic");
                });

            modelBuilder.Entity("_18TWENTY8.Models.LittleSisterDetail", b =>
                {
                    b.Property<int>("LittleSisterDetailID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressStreet");

                    b.Property<string>("AddressStreetlinetwo");

                    b.Property<string>("AlternateContact");

                    b.Property<int>("ArrestedConvictedQ");

                    b.Property<string>("BackgroundQ");

                    b.Property<string>("CVurl");

                    b.Property<string>("CertifiedID");

                    b.Property<int>("ConfirmMenteedurationQ");

                    b.Property<string>("CurrentStudyQ");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateofBirth");

                    b.Property<string>("DetailsArrestedConvictedQ");

                    b.Property<string>("DetailsOnEverbeenMenteeQ");

                    b.Property<string>("EmergencyContactNameone");

                    b.Property<string>("EmergencyContactNametwo");

                    b.Property<string>("EmergencyContactNumberone");

                    b.Property<string>("EmergencyContactNumbertwo");

                    b.Property<int>("EverbeenamenteeQ");

                    b.Property<string>("Expectationsonlittlesister");

                    b.Property<string>("IDPassport");

                    b.Property<string>("Imageurl");

                    b.Property<int>("InformationofInterest");

                    b.Property<int>("InteractionlevelDigCom");

                    b.Property<string>("InteractionlevelDigComother");

                    b.Property<int>("Interactionlevelmeet");

                    b.Property<string>("Interactionlevelmeetother");

                    b.Property<string>("Name");

                    b.Property<string>("Nickname");

                    b.Property<string>("Phonenumber");

                    b.Property<string>("PostalCode");

                    b.Property<int>("ProfileStatusID");

                    b.Property<int>("Province");

                    b.Property<string>("Surname");

                    b.Property<string>("UserID");

                    b.Property<string>("VerifCode");

                    b.Property<string>("VerifCodeComp");

                    b.Property<string>("email");

                    b.Property<string>("howdidyouhearaboutQ");

                    b.Property<string>("prefferedMenteedetails");

                    b.Property<string>("profilestatusreason");

                    b.Property<string>("verifiedRegistration");

                    b.HasKey("LittleSisterDetailID");

                    b.ToTable("LittleSisterDetail");
                });

            modelBuilder.Entity("_18TWENTY8.Models.Messaging", b =>
                {
                    b.Property<int>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateSent");

                    b.Property<string>("EmptyMessage");

                    b.Property<string>("Message");

                    b.Property<int>("MsRead");

                    b.Property<string>("ReceiveUserID");

                    b.Property<string>("SendUserId");

                    b.HasKey("MessageID");

                    b.ToTable("Messaging");
                });

            modelBuilder.Entity("_18TWENTY8.Models.OptionalBool", b =>
                {
                    b.Property<int>("YesNoID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.HasKey("YesNoID");

                    b.ToTable("OptionalBool");
                });

            modelBuilder.Entity("_18TWENTY8.Models.PressPublicity", b =>
                {
                    b.Property<int>("PressPubID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<string>("Pathurl");

                    b.Property<int>("PressPubTypeID");

                    b.Property<string>("SiteUrl");

                    b.Property<string>("Title");

                    b.HasKey("PressPubID");

                    b.ToTable("PressPublicity");
                });

            modelBuilder.Entity("_18TWENTY8.Models.PressPubType", b =>
                {
                    b.Property<int>("PressPubTypeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.HasKey("PressPubTypeID");

                    b.ToTable("PressPubType");
                });

            modelBuilder.Entity("_18TWENTY8.Models.ProfileStatus", b =>
                {
                    b.Property<int>("ProfileStatusID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<string>("Role");

                    b.HasKey("ProfileStatusID");

                    b.ToTable("ProfileStatus");
                });

            modelBuilder.Entity("_18TWENTY8.Models.Province", b =>
                {
                    b.Property<int>("ProvinceID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Provincename");

                    b.HasKey("ProvinceID");

                    b.ToTable("Province");
                });

            modelBuilder.Entity("_18TWENTY8.Models.RecognitionLetters", b =>
                {
                    b.Property<int>("RecognitionLetterID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("RecognitionLetterName");

                    b.Property<string>("popupmodalid");

                    b.Property<string>("recogurl");

                    b.HasKey("RecognitionLetterID");

                    b.ToTable("RecognitionLetters");
                });

            modelBuilder.Entity("_18TWENTY8.Models.SisterAssignment", b =>
                {
                    b.Property<int>("SisAssID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssignSisterStatusID");

                    b.Property<int>("BigApproveID");

                    b.Property<string>("BigSisterID");

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("LittleApproveID");

                    b.Property<string>("LittleSisterID");

                    b.HasKey("SisAssID");

                    b.ToTable("SisterAssignment");
                });

            modelBuilder.Entity("_18TWENTY8.Models.Workshops", b =>
                {
                    b.Property<int>("WorkshopID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("WorkshopContent");

                    b.Property<int>("WorkshopFileTypeID");

                    b.Property<string>("WorkshopTitle");

                    b.Property<string>("fileurl");

                    b.HasKey("WorkshopID");

                    b.ToTable("Workshops");
                });

            modelBuilder.Entity("_18TWENTY8.Models.WorkshopsupportDocType", b =>
                {
                    b.Property<int>("WorkshopFileTypeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("FileTypeDescription");

                    b.HasKey("WorkshopFileTypeID");

                    b.ToTable("WorkshopsupportDocType");
                });
#pragma warning restore 612, 618
        }
    }
}
