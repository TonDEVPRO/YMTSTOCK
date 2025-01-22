
using RfqEbr.Models;
using System;
using System.Data.Entity.ModelConfiguration;

public class ProductModelConfig : EntityTypeConfiguration<ProductModel>
{
    public ProductModelConfig()
    {

        // Mapping Properties
        // Table Name
        ToTable("YMTG_NDS_ViewProduct");

        // Primary Key
        HasKey(t => t.Id);


        // กำหนด Primary Key
        HasKey(e => e.Id);

        // การกำหนดคอลัมน์แต่ละฟิลด์
        Property(e => e.Id).HasColumnName("Id").IsRequired();
        Property(e => e.OrderNumber).HasColumnName("OrderNumber").HasMaxLength(255);
        Property(e => e.ProductId).HasColumnName("ProductId");
        Property(e => e.ProductName).HasColumnName("ProductName").HasMaxLength(255);
        Property(e => e.Qty).HasColumnName("Qty");
        Property(e => e.SKUCode).HasColumnName("SKUCode").HasMaxLength(50);
        Property(e => e.Size).HasColumnName("Size").HasMaxLength(50);
        Property(e => e.Color).HasColumnName("Color").HasMaxLength(50);
        Property(e => e.Price).HasColumnName("Price");

        // การกำหนดคอลัมน์ ScreenShots, Url, และตำแหน่ง
        Property(e => e.ScreenShots1).HasColumnName("ScreenShots1").HasMaxLength(500);
        Property(e => e.Url1).HasColumnName("Url1").HasMaxLength(500);
        Property(e => e.SizeScreen1_W).HasColumnName("SizeScreen1_W").HasMaxLength(50);
        Property(e => e.SizeScreen1_H).HasColumnName("SizeScreen1_H").HasMaxLength(50);
        Property(e => e.Topdistance1_Y).HasColumnName("Topdistance1_Y").HasMaxLength(50);
        Property(e => e.Topdistance1_X).HasColumnName("Topdistance1_X").HasMaxLength(50);

        Property(e => e.ScreenShots2).HasColumnName("ScreenShots2").HasMaxLength(500);
        Property(e => e.Url2).HasColumnName("Url2").HasMaxLength(500);
        Property(e => e.SizeScreen2_W).HasColumnName("SizeScreen2_W").HasMaxLength(50);
        Property(e => e.SizeScreen2_H).HasColumnName("SizeScreen2_H").HasMaxLength(50);
        Property(e => e.Topdistance2_Y).HasColumnName("Topdistance2_Y").HasMaxLength(50);
        Property(e => e.Topdistance2_X).HasColumnName("Topdistance2_X").HasMaxLength(50);

        Property(e => e.ScreenShots3).HasColumnName("ScreenShots3").HasMaxLength(500);
        Property(e => e.Url3).HasColumnName("Url3").HasMaxLength(500);
        Property(e => e.SizeScreen3_W).HasColumnName("SizeScreen3_W").HasMaxLength(50);
        Property(e => e.SizeScreen3_H).HasColumnName("SizeScreen3_H").HasMaxLength(50);
        Property(e => e.Topdistance3_Y).HasColumnName("Topdistance3_Y").HasMaxLength(50);
        Property(e => e.Topdistance3_X).HasColumnName("Topdistance3_X").HasMaxLength(50);

        Property(e => e.ScreenShots4).HasColumnName("ScreenShots4").HasMaxLength(500);
        Property(e => e.Url4).HasColumnName("Url4").HasMaxLength(500);
        Property(e => e.SizeScreen4_W).HasColumnName("SizeScreen4_W").HasMaxLength(50);
        Property(e => e.SizeScreen4_H).HasColumnName("SizeScreen4_H").HasMaxLength(50);
        Property(e => e.Topdistance4_Y).HasColumnName("Topdistance4_Y").HasMaxLength(50);
        Property(e => e.Topdistance4_X).HasColumnName("Topdistance4_X").HasMaxLength(50);

        // คอลัมน์เพิ่มเติม
        Property(e => e.PrintingType).HasColumnName("PrintingType");
        Property(e => e.Remark).HasColumnName("Remark").HasMaxLength(500);
        Property(e => e.CreateBy).HasColumnName("CreateBy").HasMaxLength(100);
        Property(e => e.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime");


    }
}














