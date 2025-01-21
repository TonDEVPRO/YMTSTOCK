
using RfqEbr.Models;
using System;
using System.Data.Entity.ModelConfiguration;

public class TypeOrderFromConfig : EntityTypeConfiguration<TypeOrderFrom>
{
    public TypeOrderFromConfig()
    {

        // เพิ่มเติม: หากมี Table Name ที่ต้องการกำหนดเอง
        ToTable("YMTG_NDS_OrderType"); // ชื่อ Table ในฐานข้อมูล


        HasKey(e => e.Code); // กำหนดให้ Code เป็น Primary Key

        Property(e => e.Code)
            .IsRequired(); // กำหนดว่าห้ามเป็น null

        Property(e => e.TypeRecapFrom)
            .IsRequired() // กำหนดว่าห้ามเป็น null
            .HasMaxLength(100); // กำหนดความยาวสูงสุด

        Property(e => e.Description)
            .HasMaxLength(250); // กำหนดความยาวสูงสุด

        Property(e => e.CodeChar)
            .HasMaxLength(50); // กำหนดความยาวสูงสุด

        Property(e => e.TypeRecapShow)
            .HasMaxLength(100); // กำหนดความยาวสูงสุด


    }
}














