﻿// <auto-generated />
using System;
using Krunsave.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Krunsave.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity("Krunsave.Model.Availablefood", b =>
                {
                    b.Property<int>("availableFoodID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("availableUnits");

                    b.Property<string>("cookedDate");

                    b.Property<string>("description");

                    b.Property<int?>("discountPerUnit");

                    b.Property<string>("engName");

                    b.Property<string>("expiryDate");

                    b.Property<int?>("foodTypeID");

                    b.Property<int?>("pricePerUnit");

                    b.Property<int>("storeID");

                    b.Property<string>("thaiName");

                    b.Property<int?>("totalUnits");

                    b.Property<string>("unitType");

                    b.HasKey("availableFoodID");

                    b.HasIndex("storeID");

                    b.ToTable("Availablefoods");
                });

            modelBuilder.Entity("Krunsave.Model.Availablefoodtag", b =>
                {
                    b.Property<int>("foodTagID");

                    b.Property<int>("availableFoodID");

                    b.HasKey("foodTagID", "availableFoodID");

                    b.ToTable("availablefoodtags");
                });

            modelBuilder.Entity("Krunsave.Model.Foodtag", b =>
                {
                    b.Property<int>("foodTagID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("tagName");

                    b.HasKey("foodTagID");

                    b.ToTable("Foodtags");
                });

            modelBuilder.Entity("Krunsave.Model.Foodtype", b =>
                {
                    b.Property<int>("foodTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("category");

                    b.HasKey("foodTypeID");

                    b.ToTable("Foodtypes");
                });

            modelBuilder.Entity("Krunsave.Model.Role", b =>
                {
                    b.Property<int>("roleID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("roleName");

                    b.HasKey("roleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Krunsave.Model.Store", b =>
                {
                    b.Property<int>("storeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("address");

                    b.Property<string>("closeTime");

                    b.Property<string>("email");

                    b.Property<decimal>("lat");

                    b.Property<decimal>("lng");

                    b.Property<string>("managerName");

                    b.Property<string>("openTime");

                    b.Property<string>("phoneNumber");

                    b.Property<string>("storeName");

                    b.Property<int>("storeTypeID");

                    b.Property<int>("userID");

                    b.HasKey("storeID");

                    b.HasIndex("userID");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Krunsave.Model.Storetype", b =>
                {
                    b.Property<int>("storeTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("category");

                    b.HasKey("storeTypeID");

                    b.ToTable("Storetypes");
                });

            modelBuilder.Entity("Krunsave.Model.User", b =>
                {
                    b.Property<int>("userID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("email");

                    b.Property<byte[]>("passwordHash");

                    b.Property<byte[]>("passwordSalt");

                    b.Property<string>("phoneNumber");

                    b.Property<int>("roleID");

                    b.HasKey("userID");

                    b.HasIndex("roleID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Krunsave.Model.Userview", b =>
                {
                    b.Property<int>("userViewID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("availableFoodID");

                    b.Property<int>("storeID");

                    b.Property<int>("userID");

                    b.Property<string>("viewDate");

                    b.Property<string>("viewTime");

                    b.HasKey("userViewID");

                    b.HasIndex("availableFoodID");

                    b.HasIndex("storeID");

                    b.HasIndex("userID");

                    b.ToTable("Userviews");
                });

            modelBuilder.Entity("Krunsave.Model.Availablefood", b =>
                {
                    b.HasOne("Krunsave.Model.Store", "store")
                        .WithMany("availablefoodID")
                        .HasForeignKey("storeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Krunsave.Model.Store", b =>
                {
                    b.HasOne("Krunsave.Model.User", "user")
                        .WithMany("store")
                        .HasForeignKey("userID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Krunsave.Model.User", b =>
                {
                    b.HasOne("Krunsave.Model.Role", "role")
                        .WithMany("user")
                        .HasForeignKey("roleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Krunsave.Model.Userview", b =>
                {
                    b.HasOne("Krunsave.Model.Availablefood", "availableFood")
                        .WithMany("userview")
                        .HasForeignKey("availableFoodID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Krunsave.Model.Store", "store")
                        .WithMany("userview")
                        .HasForeignKey("storeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Krunsave.Model.User", "user")
                        .WithMany("userview")
                        .HasForeignKey("userID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
